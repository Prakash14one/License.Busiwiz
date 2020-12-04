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
using System.Data;
using System.Data.SqlClient;

public partial class ShoppingCart_Admin_Master_Default : System.Web.UI.Page
{
    SqlConnection con;

    string compid;

    protected void Page_Load(object sender, EventArgs e)
    {
        PageConn pgcon = new PageConn();
        con = pgcon.dynconn;

        if (!Page.IsPostBack)
        {
            lblCompany.Text = Session["Cname"].ToString();

            fillstore();
            fillpriceplandate();
            fillemail();
            fillnamenumberaddress();
            fillcurrency();
            filldeprdesg();
            //fillhourperiod();
            fillsalaryperiod();
            fillduration();
            fillvacancy();
            fillvacancytitle();
            fillnewduration();
            fillcountry();
            fillstate();
            fillcity();

            filterbuss();
            filterdept();
            fillvacancytitleoutside();

            Label53.Text = "http://www.ijobcenter.com/Default.aspx?id=" + DropDownList1.SelectedValue;

            Label59.Text = DropDownList1.SelectedItem.Text;

            DateTime thismonthstart = Convert.ToDateTime(System.DateTime.Now.Month.ToString() + "/1/" + System.DateTime.Now.Year.ToString());
            string thismonthstartdate = thismonthstart.ToShortDateString();
            ViewState["thismonthstartdate"] = thismonthstartdate;

            TextBox1.Text = ViewState["thismonthstartdate"].ToString();
            TextBox2.Text = System.DateTime.Now.ToShortDateString();

            fillgrid();
        }
    }
    protected void btnadd_Click(object sender, EventArgs e)
    {
        Button2.Visible = true;
        Pnladdnew.Visible = true;

        if (Pnladdnew.Visible == true)
        {
            btnadd.Visible = false;
            panellink.Visible = false;
        }

        statuslable.Text = "";
        lbllegend.Text = "Add Vacancy";
        txtestartdate.Text = System.DateTime.Now.ToShortDateString();
        txteenddate.Text = System.DateTime.Now.ToShortDateString();

        fillstore();
        fillemail();
        fillnamenumberaddress();
        fillcurrency();
        panelcontact.Visible = false;
        panel1address.Visible = false;
    }

    protected void fillstore()
    {
        ddlStore.Items.Clear();
        DataTable ds = ClsStore.SelectStorename();
        ddlStore.DataSource = ds;
        ddlStore.DataTextField = "Name";
        ddlStore.DataValueField = "WareHouseId";
        ddlStore.DataBind();
        //ddlstore.Items.Insert(0, "Select");

        ViewState["cd"] = "1";
        DataTable dteeed = ClsStore.SelectEmployeewithIdwise();

        if (dteeed.Rows.Count > 0)
        {
           // ddlStore.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);
        }
    }

    protected void filldeprdesg()
    {
        ddldeptdesg.Items.Clear();
        string str1 = "select DesignationMaster.DesignationMasterId,DepartmentmasterMNC.Departmentname + ':'+DesignationMaster.DesignationName as name FROM DepartmentmasterMNC INNER JOIN DesignationMaster ON DesignationMaster.DeptID = DepartmentmasterMNC.id where Companyid='" + Session["Comid"].ToString() + "' and Whid='" + ddlStore.SelectedValue + "' ORDER BY DepartmentmasterMNC.Departmentname,DesignationMaster.DesignationName ";

        DataTable ds1 = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(str1, con);
        da.Fill(ds1);
        if (ds1.Rows.Count > 0)
        {
            ddldeptdesg.DataSource = ds1;
            ddldeptdesg.DataTextField = "name";
            ddldeptdesg.DataValueField = "DesignationMasterId";
            ddldeptdesg.DataBind();
        }
        ddldeptdesg.Items.Insert(0, "-Select-");
        ddldeptdesg.SelectedItem.Value = "0";
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        //bool access = Usercon11submit("VacancyMasterTbl", "", "ID", "", "", "VacancyMasterTbl.comid", "");

        //if (access == true)
        //{
        string insert = "";

        if (CheckBox2.Checked == false)
        {
            foreach (GridViewRow gfhh in GridView5.Rows)
            {
                 Label labelcountryid = (Label)gfhh.FindControl("labelcountryid");
                  Label labelstateid = (Label)gfhh.FindControl("labelstateid");
                 Label labelcityid = (Label)gfhh.FindControl("labelcityid");

                insert = "insert into VacancyMasterTbl([BusinessID],[DesignationID],[vacancypositiontypeid],[vacancypositiontitleid],[noofvacancy],[startdate],[enddate],[currencyid],[salaryamount],[salaryperperiodid],[worktimings],[hours],[hoursperperiodid],[vacancyftptid],[status],[contactname],[contactEmail],[contactphoneno],[contactAddress],[applybyemail],[applybyphone],[applybyvisit],[applyonline],[countryid],[stateid],[cityid],[vacancyduration],[comid]) values ('" + ddlStore.SelectedValue + "','" + ddldeptdesg.SelectedValue + "','" + ddlvacancytype.SelectedValue + "','" + ddlvacancyposition.SelectedValue + "','" + txtvacancy.Text + "','" + txtestartdate.Text + "','" + txteenddate.Text + "','" + ddlcurrency.SelectedValue + "','" + txtmysalary.Text + "','" + ddlsalperiod.SelectedValue + "','" + txtworktimings.Text + "','" + txtnoofhours.Text + "','" + ddlhourperiod.SelectedValue + "','" + ddlduration.SelectedValue + "','" + ddlstatus.SelectedValue + "','" + txtpername.Text + "','" + ddlemail.SelectedItem.Text + "','" + txtphoneno.Text + "','" + txtconaddress.Text + "','" + chkemail.Checked + "','" + chkphone.Checked + "','" + chkvisit.Checked + "','" + chkonline.Checked + "','" + labelcountryid.Text + "','" + labelstateid.Text + "','" + labelcityid.Text + "','" + DropDownList3.SelectedValue + "','" + Session["Comid"].ToString() + "')";
             //   insert = "insert into VacancyMasterTbl([BusinessID],[DesignationID],[vacancypositiontypeid],[vacancypositiontitleid],[noofvacancy],[startdate],[enddate],[currencyid],[salaryamount],[salaryperperiodid],[worktimings],[hours],[hoursperperiodid],[vacancyftptid],[status],[contactname],[contactEmail],[contactphoneno],[contactAddress],[applybyemail],[applybyphone],[applybyvisit],[applyonline],[countryid],[stateid],[cityid],[vacancyduration],[comid]) values ('" + ddlStore.SelectedValue + "','" + ddldeptdesg.SelectedValue + "','" + ddlvacancytype.SelectedValue + "','" + ddlvacancyposition.SelectedValue + "','" + txtvacancy.Text + "','" + txtestartdate.Text + "','" + txteenddate.Text + "','" + ddlcurrency.SelectedValue + "','" + txtmysalary.Text + "','" + ddlsalperiod.SelectedValue + "','" + txtworktimings.Text + "','" + txtnoofhours.Text + "','" + ddlhourperiod.SelectedValue + "','" + ddlduration.SelectedValue + "','" + ddlstatus.SelectedValue + "','" + txtpername.Text + "','" + ddlemail.SelectedItem.Text + "','" + txtphoneno.Text + "','" + txtconaddress.Text + "','" + chkemail.Checked + "','" + chkphone.Checked + "','" + chkvisit.Checked + "','" + chkonline.Checked + "','" + "India" + "','" + "Gujart" + "','" + labelcityid.Text + "','" + DropDownList3.SelectedValue + "','" + Session["Comid"].ToString() + "')";

                SqlCommand cmd = new SqlCommand(insert, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmd.ExecuteNonQuery();
                con.Close();

                SqlDataAdapter daa = new SqlDataAdapter("select max(ID) as ID from VacancyMasterTbl", con);
                DataTable dta = new DataTable();
                daa.Fill(dta);

                if (dta.Rows.Count > 0)
                {
                    //SqlCommand cmmx = new SqlCommand("insert into VacancyRegistrationApproval([Comid],[VacancyID],[ApprovalRequired],[ApprovalRequiredDate],[ApprovalStatus],[ApprovalNote]) values('" + Session["Comid"].ToString() + "','" + dta.Rows[0]["ID"].ToString() + "','0','" + DateTime.Now.ToShortDateString() + "','Unapproved','')", con);
                    //if (con.State.ToString() != "Open")
                    //{
                    //    con.Open();
                    //}
                    //cmmx.ExecuteNonQuery();
                    //con.Close();

                    SqlCommand cmm = new SqlCommand("insert into VacancyDetailTbl values('" + dta.Rows[0]["ID"].ToString() + "','" + txtjobfunction.Text + "','" + txtqualireq.Text + "','" + txttermcond.Text + "')", con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmm.ExecuteNonQuery();
                    con.Close();
                }
            }
        }
        if (CheckBox2.Checked == true)
        {
            SqlCommand cmd1 = new SqlCommand("insert into VacancyPositionTitleMaster values('" + ddlvacancytype.SelectedValue + "','" + txtnewvacancy.Text + "',0)", con);
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
                foreach (GridViewRow gfhh in GridView5.Rows)
                {
                    Label labelcountryid = (Label)gfhh.FindControl("labelcountryid");
                    Label labelstateid = (Label)gfhh.FindControl("labelstateid");
                    Label labelcityid = (Label)gfhh.FindControl("labelcityid");

                    insert = "insert into VacancyMasterTbl([BusinessID],[DesignationID],[vacancypositiontypeid],[vacancypositiontitleid],[noofvacancy],[startdate],[enddate],[currencyid],[salaryamount],[salaryperperiodid],[worktimings],[hours],[hoursperperiodid],[vacancyftptid],[status],[contactname],[contactEmail],[contactphoneno],[contactAddress],[applybyemail],[applybyphone],[applybyvisit],[applyonline],[countryid],[stateid],[cityid],[vacancyduration],[comid]) values('" + ddlStore.SelectedValue + "','" + ddldeptdesg.SelectedValue + "','" + ddlvacancytype.SelectedValue + "','" + dt1.Rows[0]["ID"].ToString() + "','" + txtvacancy.Text + "','" + txtestartdate.Text + "','" + txteenddate.Text + "','" + ddlcurrency.SelectedValue + "','" + txtmysalary.Text + "','" + ddlsalperiod.SelectedValue + "','" + txtworktimings.Text + "','" + txtnoofhours.Text + "','" + ddlhourperiod.SelectedValue + "','" + ddlduration.SelectedValue + "','" + ddlstatus.SelectedValue + "','" + txtpername.Text + "','" + ddlemail.SelectedItem.Text + "','" + txtphoneno.Text + "','" + txtconaddress.Text + "','" + chkemail.Checked + "','" + chkphone.Checked + "','" + chkvisit.Checked + "','" + chkonline.Checked + "','" + labelcountryid.Text + "','" + labelstateid.Text + "','" + labelcityid.Text + "','" + DropDownList3.SelectedValue + "','" + Session["Comid"].ToString() + "')";

                    SqlCommand cmd = new SqlCommand(insert, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmd.ExecuteNonQuery();
                    con.Close();

                    SqlDataAdapter daa = new SqlDataAdapter("select max(ID) as ID from VacancyMasterTbl", con);
                    DataTable dta = new DataTable();
                    daa.Fill(dta);

                    if (dta.Rows.Count > 0)
                    {
                        //SqlCommand cmmx = new SqlCommand("insert into VacancyRegistrationApproval([Comid],[VacancyID],[ApprovalRequired],[ApprovalRequiredDate],[ApprovalStatus],[ApprovalNote]) values('" + Session["Comid"].ToString() + "','" + dta.Rows[0]["ID"].ToString() + "','0','" + DateTime.Now.ToShortDateString() + "','Unapproved','')", con);
                        //if (con.State.ToString() != "Open")
                        //{
                        //    con.Open();
                        //}
                        //cmmx.ExecuteNonQuery();
                        //con.Close();

                        SqlCommand cmm = new SqlCommand("insert into VacancyDetailTbl values('" + dta.Rows[0]["ID"].ToString() + "','" + txtjobfunction.Text + "','" + txtqualireq.Text + "','" + txttermcond.Text + "')", con);
                        if (con.State.ToString() != "Open")
                        {
                            con.Open();
                        }
                        cmm.ExecuteNonQuery();
                        con.Close();
                    }
                }
            }

            fillvacancytitle();
        }

        fillgrid();
        clear();
        statuslable.Text = "Record inserted successfully.";
        btnadd.Visible = true;
        panellink.Visible = true;
        Pnladdnew.Visible = false;

        Label52.Text = "http://www.ijobcenter.com/Default.aspx?ID=" + ddlStore.SelectedValue;

        ModalPopupExtender1222.Show();
        //}
        //else
        //{
        //    statuslable.Text = "";
        //    statuslable.Text = "Sorry, You are not allowed to publish any more vacancy as your price plan allows only " + ViewState["mynum"].ToString() + " vacancies at a time. If you wish to publish any more vacancy, you have to upgrade price plan from <a href=http://members.busiwiz.com target=_blank style=\"color:Blue\"> members.busiwiz.com</a>";
        //}
    }

    protected void fillgrid()
    {
        string st1 = "";

        if (DropDownList1.SelectedIndex > 0)
        {
            st1 += " and VacancyMasterTbl.BusinessID='" + DropDownList1.SelectedValue + "'";
        }

        if (DropDownList2.SelectedIndex > 0)
        {
            st1 += " and VacancyMasterTbl.DesignationID='" + DropDownList2.SelectedValue + "'";
        }

        if (DropDownList4.SelectedIndex > 0)
        {
            st1 += " and VacancyMasterTbl.vacancypositiontitleid='" + DropDownList4.SelectedValue + "'";
        }

        if (ddlstatus_search.SelectedIndex > 0)
        {
            st1 += " and VacancyMasterTbl.status='" + ddlstatus_search.SelectedValue + "'";
        }

        //string str = "select distinct VacancyMasterTbl.*,VacancyFTPT.Name as Term,VacancyMasterTbl.ID as candidate,VacancyMasterTbl.ID,case when (VacancyMasterTbl.status='1') then 'Active' else 'Inactive' End as Statuslabel,WareHouseMaster.Name as wname,DepartmentmasterMNC.Departmentname + ':' + DesignationMaster.DesignationName as dname,VacancyTypeMaster.Name as vname,VacancyPositionTitleMaster.VacancyPositionTitle as vtitle from VacancyMasterTbl inner join WareHouseMaster on WareHouseMaster.WareHouseId=VacancyMasterTbl.businessid  inner join DesignationMaster on DesignationMaster.DesignationMasterId=VacancyMasterTbl.DesignationID inner join DepartmentmasterMNC on DesignationMaster.DeptID=DepartmentmasterMNC.id inner join VacancyTypeMaster on VacancyTypeMaster.ID=VacancyMasterTbl.vacancypositiontypeid inner join VacancyPositionTitleMaster  on VacancyPositionTitleMaster.ID=VacancyMasterTbl.vacancypositiontitleid inner join VacancyFTPT on VacancyFTPT.Id=VacancyMasterTbl.vacancyftptid  where ((VacancyMasterTbl.startdate >= '" + TextBox1.Text + "' OR VacancyMasterTbl.startdate <= '" + TextBox2.Text + "') OR (VacancyMasterTbl.enddate >= '" + TextBox1.Text + "' OR VacancyMasterTbl.enddate <= '" + TextBox2.Text + "')) " + st1 + "";

        string str = "select distinct VacancyMasterTbl.*,VacancyFTPT.Name as Term,VacancyMasterTbl.ID as candidate,VacancyMasterTbl.ID,case when (VacancyMasterTbl.status='1') then 'Active' else 'Inactive' End as Statuslabel,cast(VacancyMasterTbl.salaryamount as float) as salary,SalaryPerPeriodMaster.Name as sss,LEFT(VacancyDetailTbl.JobFunction,100) as JobFunction,LEFT(VacancyDetailTbl.QualificationRequirements,25) as QualificationRequirements,VacancyDetailTbl.TermsandConditions,CurrencyMaster.CurrencyName as ccc,VacancyFTPT.Name as vvv,WareHouseMaster.Name as wname,DepartmentmasterMNC.Departmentname + ':' + DesignationMaster.DesignationName as dname,VacancyTypeMaster.Name as vname,VacancyPositionTitleMaster.VacancyPositionTitle as vtitle from VacancyMasterTbl inner join WareHouseMaster on WareHouseMaster.WareHouseId=VacancyMasterTbl.businessid  inner join DesignationMaster on DesignationMaster.DesignationMasterId=VacancyMasterTbl.DesignationID inner join DepartmentmasterMNC on DesignationMaster.DeptID=DepartmentmasterMNC.id inner  join VacancyTypeMaster on VacancyTypeMaster.ID=VacancyMasterTbl.vacancypositiontypeid inner join VacancyDetailTbl on VacancyDetailTbl.ID=VacancyMasterTbl.ID inner join CurrencyMaster on CurrencyMaster.CurrencyId=VacancyMasterTbl.currencyid inner join VacancyPositionTitleMaster  on VacancyPositionTitleMaster.ID=VacancyMasterTbl.vacancypositiontitleid inner join VacancyFTPT on VacancyFTPT.Id=VacancyMasterTbl.vacancyftptid inner join SalaryPerPeriodMaster on SalaryPerPeriodMaster.ID=VacancyMasterTbl.salaryperperiodid where ((VacancyMasterTbl.startdate >= '" + TextBox1.Text + "' and VacancyMasterTbl.startdate <= '" + TextBox2.Text + "') OR (VacancyMasterTbl.enddate >= '" + TextBox1.Text + "' and VacancyMasterTbl.enddate <= '" + TextBox2.Text + "')) " + st1 + "";

        SqlDataAdapter da = new SqlDataAdapter(str, con);
        DataTable dt = new DataTable();
        da.Fill(dt);

        GridView1.DataSource = dt;
        GridView1.DataBind();

        foreach (GridViewRow gdr in GridView1.Rows)
        {
            LinkButton LinkButton1 = (LinkButton)gdr.FindControl("LinkButton1");

            Label Label51 = (Label)gdr.FindControl("Label51");

            Label Label472 = (Label)gdr.FindControl("Label472");

            if (Convert.ToDateTime(Label472.Text) < Convert.ToDateTime(DateTime.Now.ToShortDateString()))
            {
                LinkButton1.Enabled = false;
            }

            string stmm = "select count(CandidateMaster.CandidateId) as CandidateId from CandidateMaster inner join VacancyAppReceive on CandidateMaster.CandidateId=VacancyAppReceive.candidateid inner join VacancyMasterTbl on VacancyAppReceive.vacancyid=VacancyMasterTbl.ID inner join CandidateVacancyStatus on CandidateVacancyStatus.CandidateID=CandidateMaster.Candidateid inner join CandidateApplicationStatusMasterIJ on CandidateApplicationStatusMasterIJ.id=CandidateVacancyStatus.StatusID and CandidateVacancyStatus.VacancyID=VacancyMasterTbl.ID where VacancyMasterTbl.Id='" + Label51.Text + "'";

            SqlDataAdapter dafg = new SqlDataAdapter(stmm, con);
            DataTable dtfg = new DataTable();
            dafg.Fill(dtfg);

            LinkButton1.Text = dtfg.Rows[0]["CandidateId"].ToString();

        }
    }

    protected void filterbuss()
    {
        DropDownList1.Items.Clear();
        DataTable ds = ClsStore.SelectStorename();
        DropDownList1.DataSource = ds;
        DropDownList1.DataTextField = "Name";
        DropDownList1.DataValueField = "WareHouseId";
        DropDownList1.DataBind();

        DropDownList1.Items.Insert(0, "All");
        DropDownList1.Items[0].Value = "0";

        DataTable dteeed = ClsStore.SelectEmployeewithIdwise();

        if (dteeed.Rows.Count > 0)
        {
            DropDownList1.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);
        }
    }

    protected void fillcurrency()
    {
        ddlcurrency.Items.Clear();
        string str1 = "select distinct CurrencyMaster.CurrencyName,CurrencyMaster.CurrencyId from CurrencyMaster inner join WareHouseMaster on WareHouseMaster.CurrencyId=CurrencyMaster.CurrencyId";

        DataTable ds12 = new DataTable();
        SqlDataAdapter da2 = new SqlDataAdapter(str1, con);
        da2.Fill(ds12);
        if (ds12.Rows.Count > 0)
        {
            ddlcurrency.DataSource = ds12;
            ddlcurrency.DataTextField = "CurrencyName";
            ddlcurrency.DataValueField = "CurrencyId";
            ddlcurrency.DataBind();
        }
        //ddlcurrency.Items.Insert(0, "ALL");
        //ddlcurrency.SelectedItem.Value = "0";
    }

    protected void fillsalaryperiod()
    {
        ddlsalperiod.Items.Clear();
        string str1 = "select ID,Name from SalaryPerPeriodMaster";

        DataTable ds13 = new DataTable();
        SqlDataAdapter da3 = new SqlDataAdapter(str1, con);
        da3.Fill(ds13);
        if (ds13.Rows.Count > 0)
        {
            ddlsalperiod.DataSource = ds13;
            ddlsalperiod.DataTextField = "Name";
            ddlsalperiod.DataValueField = "ID";
            ddlsalperiod.DataBind();
        }
    }


    protected void fillduration()
    {
        ddlduration.Items.Clear();
        string str1 = "select Id,Name from VacancyFTPT";

        DataTable ds13 = new DataTable();
        SqlDataAdapter da3 = new SqlDataAdapter(str1, con);
        da3.Fill(ds13);
        if (ds13.Rows.Count > 0)
        {
            ddlduration.DataSource = ds13;
            ddlduration.DataTextField = "Name";
            ddlduration.DataValueField = "Id";
            ddlduration.DataBind();
        }

        ddlduration.SelectedIndex = ddlduration.Items.IndexOf(ddlduration.Items.FindByText("Full Time"));
    }


    protected void fillvacancy()
    {
        ddlvacancytype.Items.Clear();

        SqlDataAdapter dav = new SqlDataAdapter("select ID,Name from VacancyTypeMaster", con);
        DataTable dtv = new DataTable();
        dav.Fill(dtv);

        if (dtv.Rows.Count > 0)
        {
            ddlvacancytype.DataSource = dtv;
            ddlvacancytype.DataTextField = "Name";
            ddlvacancytype.DataValueField = "ID";
            ddlvacancytype.DataBind();
        }
        ddlvacancytype.Items.Insert(0, "-Select-");
        ddlvacancytype.Items[0].Value = "0";

        ddlvacancytype.SelectedIndex = ddlvacancytype.Items.IndexOf(ddlvacancytype.Items.FindByText("Permanent"));
    }

    protected void fillvacancytitle()
    {
        ddlvacancyposition.Items.Clear();

        SqlDataAdapter dav = new SqlDataAdapter("select ID,VacancyPositionTitle from VacancyPositionTitleMaster where VacancyPositionTypeID='" + ddlvacancytype.SelectedValue + "' and Active=1", con);
        DataTable dtv = new DataTable();
        dav.Fill(dtv);

        if (dtv.Rows.Count > 0)
        {
            ddlvacancyposition.DataSource = dtv;
            ddlvacancyposition.DataTextField = "VacancyPositionTitle";
            ddlvacancyposition.DataValueField = "ID";
            ddlvacancyposition.DataBind();
        }
        ddlvacancyposition.Items.Insert(0, "-Select-");
        ddlvacancyposition.Items[0].Value = "0";
    }
    protected void ddlvacancytype_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillvacancytitle();
    }
    protected void CheckBox2_CheckedChanged(object sender, EventArgs e)
    {
        if (CheckBox2.Checked == true)
        {
            ddlvacancyposition.Enabled = false;
            panel11.Visible = true;

        }
        if (CheckBox2.Checked == false)
        {
            panel11.Visible = false;
            ddlvacancyposition.Enabled = true;

        }
    }

    protected void clear()
    {
        Button2.Visible = true;
        //duplicatetbemail.Text = "";
        txtconaddress.Text = "";
        txteenddate.Text = "";
        ddlemail.SelectedIndex = 0;
        //txtemailid.Text = "";
        txtestartdate.Text = "";
        txtjobfunction.Text = "";
        txtmysalary.Text = "";
        txtnewvacancy.Text = "";
        txtnoofhours.Text = "";
        txtpername.Text = "";
        txtphoneno.Text = "";
        txtqualireq.Text = "";
        txttermcond.Text = "";
        txtvacancy.Text = "";
        txtworktimings.Text = "";
        chkemail.Checked = false;
        chkonline.Checked = false;
        chkphone.Checked = false;
        chkvisit.Checked = false;
        //CheckBox1.Checked = false;
        ddlstatus.SelectedIndex = 0;
        CheckBox2.Checked = false;

        ddlduration.SelectedIndex = 0;
        ddlhourperiod.SelectedIndex = 0;
        ddlcurrency.SelectedIndex = 0;
        ddlsalperiod.SelectedIndex = 0;
        ddlvacancytype.SelectedIndex = 0;
        ddlvacancyposition.SelectedIndex = 0;

        ddlCountry.Items.Clear();
        fillcountry();
        ddlCountry.SelectedIndex = 0;

        ddlState.Items.Clear();
        fillstate();
        ddlState.SelectedIndex = 0;

        ddlCity.Items.Clear();
        fillcity();
        ddlCity.SelectedIndex = 0;

        ddlStore.Items.Clear();
        fillstore();
        ddldeptdesg.Items.Clear();
        filldeprdesg();

        lbllegend.Text = "";

        ViewState["datavac"] = "";

    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        panelgr.Visible = false;

        if (e.CommandName == "Vii")
        {
            int mcv = Convert.ToInt32(e.CommandArgument);
            string te = "CandidateListIJ.aspx?id=" + mcv;
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
        }

        if (e.CommandName == "Edit")
        {
            Button2.Visible = false;

            CheckBox2.Checked = false;
            CheckBox2_CheckedChanged(sender, e);

            int mm = Convert.ToInt32(e.CommandArgument);
            ViewState["updateid"] = mm;

            lbllegend.Text = "Edit Vacancy";

            Pnladdnew.Visible = true;
            btnadd.Visible = false;
            panellink.Visible = false;

            statuslable.Text = "";
            btnsubmit.Visible = false;
            btnupdate.Visible = true;

            SqlDataAdapter da1 = new SqlDataAdapter("select * from VacancyMasterTbl where ID=" + mm, con);
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);

            if (dt1.Rows.Count > 0)
            {
                fillstore();
                ddlStore.SelectedIndex = ddlStore.Items.IndexOf(ddlStore.Items.FindByValue(dt1.Rows[0]["BusinessID"].ToString()));

                filldeprdesg();
                ddldeptdesg.SelectedIndex = ddldeptdesg.Items.IndexOf(ddldeptdesg.Items.FindByValue(dt1.Rows[0]["DesignationID"].ToString()));

                fillvacancy();
                ddlvacancytype.SelectedIndex = ddlvacancytype.Items.IndexOf(ddlvacancytype.Items.FindByValue(dt1.Rows[0]["vacancypositiontypeid"].ToString()));

                fillnewduration();
                DropDownList3.SelectedIndex = DropDownList3.Items.IndexOf(DropDownList3.Items.FindByValue(dt1.Rows[0]["vacancyduration"].ToString()));

                SqlDataAdapter dav = new SqlDataAdapter("select * from VacancyPositionTitleMaster where ID='" + dt1.Rows[0]["vacancypositiontitleid"].ToString() + "' ", con);
                DataTable dtv = new DataTable();
                dav.Fill(dtv);

                if (dtv.Rows.Count > 0)
                {
                    if (Convert.ToBoolean(dtv.Rows[0]["Active"].ToString()) == true)
                    {

                        fillvacancytitle();
                        ddlvacancyposition.SelectedIndex = ddlvacancyposition.Items.IndexOf(ddlvacancyposition.Items.FindByValue(dt1.Rows[0]["vacancypositiontitleid"].ToString()));

                        CheckBox2.Checked = false;
                        panel11.Visible = false;

                    }
                    else
                    {
                        fillvacancytitle();
                        ddlvacancyposition.SelectedIndex = ddlvacancyposition.Items.IndexOf(ddlvacancyposition.Items.FindByValue(dt1.Rows[0]["vacancypositiontitleid"].ToString()));
                        ddlvacancyposition.Enabled = false;

                        CheckBox2.Checked = true;
                        panel11.Visible = true;

                        txtnewvacancy.Text = dtv.Rows[0]["VacancyPositionTitle"].ToString();

                    }

                    ViewState["vacancypositiontitleid"] = dt1.Rows[0]["vacancypositiontitleid"].ToString();
                }

                txtvacancy.Text = dt1.Rows[0]["noofvacancy"].ToString();

                fillcountry();
                ddlCountry.SelectedIndex = ddlCountry.Items.IndexOf(ddlCountry.Items.FindByValue(dt1.Rows[0]["countryid"].ToString()));

                fillstate();
                ddlState.SelectedIndex = ddlState.Items.IndexOf(ddlState.Items.FindByValue(dt1.Rows[0]["stateid"].ToString()));

                fillcity();
                ddlCity.SelectedIndex = ddlCity.Items.IndexOf(ddlCity.Items.FindByValue(dt1.Rows[0]["cityid"].ToString()));


                txtestartdate.Text = Convert.ToDateTime(dt1.Rows[0]["startdate"].ToString()).ToShortDateString();
                txteenddate.Text = Convert.ToDateTime(dt1.Rows[0]["enddate"].ToString()).ToShortDateString();

                chkemail.Checked = Convert.ToBoolean(dt1.Rows[0]["applybyemail"].ToString());
                chkonline.Checked = Convert.ToBoolean(dt1.Rows[0]["applybyphone"].ToString());
                chkphone.Checked = Convert.ToBoolean(dt1.Rows[0]["applybyvisit"].ToString());
                chkvisit.Checked = Convert.ToBoolean(dt1.Rows[0]["applyonline"].ToString());

                txtpername.Text = dt1.Rows[0]["contactname"].ToString();

                fillemail();
                ddlemail.SelectedIndex = ddlemail.Items.IndexOf(ddlemail.Items.FindByText(dt1.Rows[0]["contactEmail"].ToString()));
                //ddlemail.SelectedItem.Text = dt1.Rows[0]["contactEmail"].ToString();

                txtphoneno.Text = dt1.Rows[0]["contactphoneno"].ToString();
                txtconaddress.Text = dt1.Rows[0]["contactAddress"].ToString();

                if (Convert.ToBoolean(dt1.Rows[0]["status"].ToString()) == true)
                {
                    ddlstatus.SelectedValue = "1";
                }
                else
                {
                    ddlstatus.SelectedValue = "0";
                }

                fillcurrency();
                ddlcurrency.SelectedIndex = ddlcurrency.Items.IndexOf(ddlcurrency.Items.FindByValue(dt1.Rows[0]["currencyid"].ToString()));

                txtmysalary.Text = dt1.Rows[0]["salaryamount"].ToString();

                fillsalaryperiod();
                ddlsalperiod.SelectedIndex = ddlsalperiod.Items.IndexOf(ddlsalperiod.Items.FindByValue(dt1.Rows[0]["salaryperperiodid"].ToString()));

                txtworktimings.Text = dt1.Rows[0]["worktimings"].ToString();
                txtnoofhours.Text = dt1.Rows[0]["hours"].ToString();

                fillduration();
                ddlduration.SelectedIndex = ddlduration.Items.IndexOf(ddlduration.Items.FindByValue(dt1.Rows[0]["vacancyftptid"].ToString()));

                ddlhourperiod.SelectedIndex = ddlhourperiod.Items.IndexOf(ddlhourperiod.Items.FindByValue(dt1.Rows[0]["hoursperperiodid"].ToString()));
            }
            SqlDataAdapter da11 = new SqlDataAdapter("select * from VacancyDetailTbl where vacancymasterid=" + mm, con);
            DataTable dt11 = new DataTable();
            da11.Fill(dt11);

            if (dt11.Rows.Count > 0)
            {
                txtjobfunction.Text = dt11.Rows[0]["JobFunction"].ToString();
                txtqualireq.Text = dt11.Rows[0]["QualificationRequirements"].ToString();
                txttermcond.Text = dt11.Rows[0]["TermsandConditions"].ToString();
            }
        }

        if (e.CommandName == "Delete")
        {
            int mm1 = Convert.ToInt32(e.CommandArgument);

            SqlCommand cmdd = new SqlCommand("delete from VacancyMasterTbl where ID=" + mm1, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmdd.ExecuteNonQuery();
            con.Close();

            SqlCommand cmdd1 = new SqlCommand("delete from VacancyDetailTbl where vacancymasterid=" + mm1, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmdd1.ExecuteNonQuery();
            con.Close();

            fillgrid();
            clear();
            statuslable.Text = "Record deleted successfully";

        }

        if (e.CommandName == "View1")
        {
            int hh = Convert.ToInt32(e.CommandArgument);

            string te = "VacancyDetail.aspx?id=" + hh;
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
        }


        if (e.CommandName == "View")
        {

            int mm2 = Convert.ToInt32(e.CommandArgument);

            lbllegend.Text = "View Vacancy";

            Pnladdnew.Visible = true;
            btnadd.Visible = false;
            panellink.Visible = false;

            statuslable.Text = "";
            btnsubmit.Visible = false;
            btnupdate.Visible = false;

            SqlDataAdapter da1 = new SqlDataAdapter("select * from VacancyMasterTbl where ID=" + mm2, con);
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);

            if (dt1.Rows.Count > 0)
            {
                fillstore();
                ddlStore.SelectedIndex = ddlStore.Items.IndexOf(ddlStore.Items.FindByValue(dt1.Rows[0]["BusinessID"].ToString()));
                ddlStore.Enabled = false;

                filldeprdesg();
                ddldeptdesg.SelectedIndex = ddldeptdesg.Items.IndexOf(ddldeptdesg.Items.FindByValue(dt1.Rows[0]["DesignationID"].ToString()));
                ddldeptdesg.Enabled = false;

                fillvacancy();
                ddlvacancytype.SelectedIndex = ddlvacancytype.Items.IndexOf(ddlvacancytype.Items.FindByValue(dt1.Rows[0]["vacancypositiontypeid"].ToString()));
                ddlvacancytype.Enabled = false;

                fillnewduration();
                DropDownList3.SelectedIndex = DropDownList3.Items.IndexOf(DropDownList3.Items.FindByValue(dt1.Rows[0]["vacancyduration"].ToString()));
                DropDownList3.Enabled = false;

                //  fillvacancytitle();
                // ddlvacancyposition.SelectedIndex = ddlvacancyposition.Items.IndexOf(ddlvacancyposition.Items.FindByValue(dt1.Rows[0]["vacancypositiontitleid"].ToString()));

                SqlDataAdapter dav = new SqlDataAdapter("select * from VacancyPositionTitleMaster where ID='" + dt1.Rows[0]["vacancypositiontitleid"].ToString() + "' ", con);
                DataTable dtv = new DataTable();
                dav.Fill(dtv);

                if (dtv.Rows.Count > 0)
                {
                    if (Convert.ToBoolean(dtv.Rows[0]["Active"].ToString()) == true)
                    {

                        fillvacancytitle();
                        ddlvacancyposition.SelectedIndex = ddlvacancyposition.Items.IndexOf(ddlvacancyposition.Items.FindByValue(dt1.Rows[0]["vacancypositiontitleid"].ToString()));

                        CheckBox2.Checked = false;
                        panel11.Visible = false;

                    }
                    else
                    {
                        fillvacancytitle();
                        ddlvacancyposition.SelectedIndex = ddlvacancyposition.Items.IndexOf(ddlvacancyposition.Items.FindByValue(dt1.Rows[0]["vacancypositiontitleid"].ToString()));
                        ddlvacancyposition.Enabled = false;

                        CheckBox2.Checked = true;
                        panel11.Visible = true;

                        txtnewvacancy.Text = dtv.Rows[0]["VacancyPositionTitle"].ToString();

                    }


                }
                ddlvacancyposition.Enabled = false;
                txtnewvacancy.Enabled = false;

                txtvacancy.Text = dt1.Rows[0]["noofvacancy"].ToString();
                txtvacancy.Enabled = false;

                fillcountry();
                ddlCountry.SelectedIndex = ddlCountry.Items.IndexOf(ddlCountry.Items.FindByValue(dt1.Rows[0]["countryid"].ToString()));
                ddlCountry.Enabled = false;

                fillstate();
                ddlState.SelectedIndex = ddlState.Items.IndexOf(ddlState.Items.FindByValue(dt1.Rows[0]["stateid"].ToString()));
                ddlState.Enabled = false;

                fillcity();
                ddlCity.SelectedIndex = ddlCity.Items.IndexOf(ddlCity.Items.FindByValue(dt1.Rows[0]["cityid"].ToString()));
                ddlCity.Enabled = false;


                txtestartdate.Text = Convert.ToDateTime(dt1.Rows[0]["startdate"].ToString()).ToShortDateString();
                txteenddate.Text = Convert.ToDateTime(dt1.Rows[0]["enddate"].ToString()).ToShortDateString();

                txtestartdate.Enabled = false;
                txteenddate.Enabled = false;

                chkemail.Checked = Convert.ToBoolean(dt1.Rows[0]["applybyemail"].ToString());
                chkonline.Checked = Convert.ToBoolean(dt1.Rows[0]["applybyphone"].ToString());
                chkphone.Checked = Convert.ToBoolean(dt1.Rows[0]["applybyvisit"].ToString());
                chkvisit.Checked = Convert.ToBoolean(dt1.Rows[0]["applyonline"].ToString());

                chkemail.Enabled = false;
                chkonline.Enabled = false;
                chkphone.Enabled = false;
                chkvisit.Enabled = false;

                ImageButton1.Enabled = false;
                ImageButton2.Enabled = false;

                imgempadd.Enabled = false;
                imgemprefresh.Enabled = false;

                txtpername.Text = dt1.Rows[0]["contactname"].ToString();
                fillemail();
                ddlemail.SelectedIndex = ddlemail.Items.IndexOf(ddlemail.Items.FindByText(dt1.Rows[0]["contactEmail"].ToString()));
                txtphoneno.Text = dt1.Rows[0]["contactphoneno"].ToString();
                txtconaddress.Text = dt1.Rows[0]["contactAddress"].ToString();

                txtpername.Enabled = false;
                //txtemailid.Enabled = false;
                ddlemail.Enabled = false;
                txtphoneno.Enabled = false;
                txtconaddress.Enabled = false;

                if (Convert.ToBoolean(dt1.Rows[0]["status"].ToString()) == true)
                {
                    ddlstatus.SelectedValue = "1";
                }
                else
                {
                    ddlstatus.SelectedValue = "0";
                }
                ddlstatus.Enabled = false;

                fillcurrency();
                ddlcurrency.SelectedIndex = ddlcurrency.Items.IndexOf(ddlcurrency.Items.FindByValue(dt1.Rows[0]["currencyid"].ToString()));
                ddlcurrency.Enabled = false;

                txtmysalary.Text = dt1.Rows[0]["salaryamount"].ToString();
                txtmysalary.Enabled = false;

                fillsalaryperiod();
                ddlsalperiod.SelectedIndex = ddlsalperiod.Items.IndexOf(ddlsalperiod.Items.FindByValue(dt1.Rows[0]["salaryperperiodid"].ToString()));
                ddlsalperiod.Enabled = false;

                txtworktimings.Text = dt1.Rows[0]["worktimings"].ToString();
                txtnoofhours.Text = dt1.Rows[0]["hours"].ToString();

                txtworktimings.Enabled = false;
                txtnoofhours.Enabled = false;

                fillduration();
                ddlduration.SelectedIndex = ddlduration.Items.IndexOf(ddlduration.Items.FindByValue(dt1.Rows[0]["vacancyftptid"].ToString()));
                ddlduration.Enabled = false;

                ddlhourperiod.SelectedIndex = ddlhourperiod.Items.IndexOf(ddlhourperiod.Items.FindByValue(dt1.Rows[0]["hoursperperiodid"].ToString()));
                ddlhourperiod.Enabled = false;

                CheckBox2.Enabled = false;
            }
            SqlDataAdapter da11 = new SqlDataAdapter("select * from VacancyDetailTbl where vacancymasterid=" + mm2, con);
            DataTable dt11 = new DataTable();
            da11.Fill(dt11);

            if (dt11.Rows.Count > 0)
            {
                txtjobfunction.Text = dt11.Rows[0]["JobFunction"].ToString();
                txtjobfunction.Enabled = false;
                txtqualireq.Text = dt11.Rows[0]["QualificationRequirements"].ToString();
                txtqualireq.Enabled = false;
                txttermcond.Text = dt11.Rows[0]["TermsandConditions"].ToString();
                txttermcond.Enabled = false;
            }

        }
    }

    protected void btnupdate_Click(object sender, EventArgs e)
    {
      //  bool access = Usercon11update("VacancyMasterTbl", "", "ID", "", "", "VacancyMasterTbl.comid", "");

        //if (access == true)
        //{
        string update = "";

        if (CheckBox2.Checked == false)
        {
            update = "update VacancyMasterTbl set  BusinessID='" + ddlStore.SelectedValue + "',DesignationID='" + ddldeptdesg.SelectedValue + "',vacancypositiontypeid='" + ddlvacancytype.SelectedValue + "',vacancypositiontitleid='" + ddlvacancyposition.SelectedValue + "',noofvacancy='" + txtvacancy.Text + "',startdate='" + txtestartdate.Text + "',enddate='" + txteenddate.Text + "',currencyid='" + ddlcurrency.SelectedValue + "',salaryamount='" + txtmysalary.Text + "',salaryperperiodid='" + ddlsalperiod.SelectedValue + "',worktimings='" + txtworktimings.Text + "',hours='" + txtnoofhours.Text + "',hoursperperiodid='" + ddlhourperiod.SelectedValue + "',vacancyftptid='" + ddlduration.SelectedValue + "',status='" + ddlstatus.SelectedValue + "',contactname='" + txtpername.Text + "',contactEmail='" + ddlemail.SelectedItem.Text + "',contactphoneno='" + txtphoneno.Text + "',contactAddress='" + txtconaddress.Text + "',applybyemail='" + chkemail.Checked + "',applybyphone='" + chkphone.Checked + "',applybyvisit='" + chkvisit.Checked + "',applyonline='" + chkonline.Checked + "',countryid='" + ddlCountry.SelectedValue + "',stateid='" + ddlState.SelectedValue + "',cityid='" + ddlCity.SelectedValue + "',vacancyduration='" + DropDownList3.SelectedValue + "',comid='" + Session["Comid"].ToString() + "' where ID='" + ViewState["updateid"].ToString() + "'";
        }
        if (CheckBox2.Checked == true)
        {
            SqlCommand cmd1 = new SqlCommand("update VacancyPositionTitleMaster set VacancyPositionTypeID='" + ddlvacancytype.SelectedValue + "',VacancyPositionTitle='" + txtnewvacancy.Text + "',Active='0' where ID='" + ViewState["vacancypositiontitleid"] + "' ", con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmd1.ExecuteNonQuery();
            con.Close();

            update = "update VacancyMasterTbl set BusinessID='" + ddlStore.SelectedValue + "',DesignationID='" + ddldeptdesg.SelectedValue + "',vacancypositiontypeid='" + ddlvacancytype.SelectedValue + "',vacancypositiontitleid='" + ViewState["vacancypositiontitleid"] + "',noofvacancy='" + txtvacancy.Text + "',startdate='" + txtestartdate.Text + "',enddate='" + txteenddate.Text + "',currencyid='" + ddlcurrency.SelectedValue + "',salaryamount='" + txtmysalary.Text + "',salaryperperiodid='" + ddlsalperiod.SelectedValue + "',worktimings='" + txtworktimings.Text + "',hours='" + txtnoofhours.Text + "',hoursperperiodid='" + ddlhourperiod.SelectedValue + "',vacancyftptid='" + ddlduration.SelectedValue + "',status='" + ddlstatus.SelectedValue + "',contactname='" + txtpername.Text + "',contactEmail='" + ddlemail.SelectedItem.Text + "',contactphoneno='" + txtphoneno.Text + "',contactAddress='" + txtconaddress.Text + "',applybyemail='" + chkemail.Checked + "',applybyphone='" + chkphone.Checked + "',applybyvisit='" + chkvisit.Checked + "',applyonline='" + chkonline.Checked + "',countryid='" + ddlCountry.SelectedValue + "',stateid='" + ddlState.SelectedValue + "',cityid='" + ddlCity.SelectedValue + "',vacancyduration='" + DropDownList3.SelectedValue + "',comid='" + Session["Comid"].ToString() + "' where ID='" + ViewState["updateid"].ToString() + "'";

            fillvacancytitle();
        }
        SqlCommand cmd = new SqlCommand(update, con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmd.ExecuteNonQuery();
        con.Close();

        //SqlCommand cmmx = new SqlCommand("update VacancyRegistrationApproval set [ApprovalRequired]='0',[ApprovalRequiredDate]='" + DateTime.Now.ToShortDateString() + "',[ApprovalStatus]='Unapproved',[ApprovalNote]='' where VacancyID='" + ViewState["updateid"].ToString() + "'", con);
        //if (con.State.ToString() != "Open")
        //{
        //    con.Open();
        //}
        //cmmx.ExecuteNonQuery();
        //con.Close();

        SqlCommand cmm = new SqlCommand("update VacancyDetailTbl set JobFunction='" + txtjobfunction.Text + "',QualificationRequirements='" + txtqualireq.Text + "',TermsandConditions='" + txttermcond.Text + "' where vacancymasterid='" + ViewState["updateid"].ToString() + "'", con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmm.ExecuteNonQuery();
        con.Close();

        fillgrid();
        clear();
        statuslable.Text = "Record updated successfully";
        btnsubmit.Visible = true;
        btnupdate.Visible = false;
        btnadd.Visible = true;
        panellink.Visible = true;
        Pnladdnew.Visible = false;

        //}
        //else
        //{
        //    statuslable.Text = "";
        //    statuslable.Text = "Sorry, You are not allowed to publish any more vacancy as your price plan allows only " + ViewState["updf"].ToString() + " vacancies at a time. If you wish to publish any more vacancy, you have to upgrade price plan from <a href=http://members.busiwiz.com target=_blank style=\"color:Blue\"> members.busiwiz.com</a>";
        //}


    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        clear();
        btnadd.Visible = true;
        panellink.Visible = true;
        Pnladdnew.Visible = false;
        lbllegend.Text = "";
        statuslable.Text = "";
        btnsubmit.Visible = true;
        btnupdate.Visible = false;


        ddlStore.Enabled = true;
        ddldeptdesg.Enabled = true;
        ddlvacancytype.Enabled = true;
        ddlvacancyposition.Enabled = true;
        txtvacancy.Enabled = true;
        txtestartdate.Enabled = true;
        txteenddate.Enabled = true;
        chkemail.Enabled = true;
        chkonline.Enabled = true;
        chkphone.Enabled = true;
        chkvisit.Enabled = true;
        txtpername.Enabled = true;
        //        txtemailid.Enabled = true;
        ddlemail.Enabled = true;
        txtphoneno.Enabled = true;
        txtconaddress.Enabled = true;
        ddlstatus.Enabled = true;
        ddlcurrency.Enabled = true;
        txtmysalary.Enabled = true;
        ddlsalperiod.Enabled = true;
        txtworktimings.Enabled = true;
        txtnoofhours.Enabled = true;
        ddlduration.Enabled = true;
        ddlhourperiod.Enabled = true;
        CheckBox2.Enabled = true;
        txtjobfunction.Enabled = true;
        txtqualireq.Enabled = true;
        txttermcond.Enabled = true;

        ddlCountry.Enabled = true;
        ddlState.Enabled = true;
        ddlCity.Enabled = true;
    }

    protected void btncancel0_Click(object sender, EventArgs e)
    {
        if (btncancel0.Text == "Printable Version")
        {
            btncancel0.Text = "Hide Printable Version";
            Button7.Visible = true;

            GridView1.AllowPaging = false;
            GridView1.PageSize = 10000;
            fillgrid();

            if (GridView1.Columns[9].Visible == true)
            {
                ViewState["Docs"] = "tt";
                GridView1.Columns[9].Visible = false;
            }
            if (GridView1.Columns[10].Visible == true)
            {
                ViewState["edith"] = "tt";
                GridView1.Columns[10].Visible = false;
            }
            if (GridView1.Columns[11].Visible == true)
            {
                ViewState["edith1"] = "tt";
                GridView1.Columns[11].Visible = false;
            }
            if (GridView1.Columns[8].Visible == true)
            {
                ViewState["edith12"] = "tt";
                GridView1.Columns[8].Visible = false;
            }
        }
        else
        {
            btncancel0.Text = "Printable Version";
            Button7.Visible = false;

            GridView1.AllowPaging = true;
            GridView1.PageSize = 15;
            fillgrid();

            if (ViewState["Docs"] != null)
            {
                GridView1.Columns[9].Visible = true;
            }
            if (ViewState["edith"] != null)
            {
                GridView1.Columns[10].Visible = true;
            }
            if (ViewState["edith1"] != null)
            {
                GridView1.Columns[11].Visible = true;
            }
            if (ViewState["edith12"] != null)
            {
                GridView1.Columns[8].Visible = true;
            }
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
    protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillstate();
    }
    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillcity();
    }

    protected void fillemail()
    {
        ddlemail.Items.Clear();

        SqlDataAdapter dav = new SqlDataAdapter("select ID,EmailID from InOutCompanyEmail where Whid='" + ddlStore.SelectedValue + "'", con);
        DataTable dtv = new DataTable();
        dav.Fill(dtv);

        if (dtv.Rows.Count > 0)
        {
            ddlemail.DataSource = dtv;
            ddlemail.DataTextField = "EmailID";
            ddlemail.DataValueField = "ID";
            ddlemail.DataBind();
        }
        ddlemail.Items.Insert(0, "-Select-");
        ddlemail.Items[0].Value = "0";
    }
    protected void ddlStore_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillpriceplandate();
        filldeprdesg();
        fillemail();
        fillnamenumberaddress();
    }

    protected void fillnamenumberaddress()
    {
        txtconaddress.Text = "";
        txtpername.Text = "";
        txtphoneno.Text = "";

        // SqlDataAdapter daaa = new SqlDataAdapter("select Address from WareHouseMaster where WareHouseId='" + ddlStore.SelectedValue + "'", con);
        SqlDataAdapter daaa = new SqlDataAdapter("select address1,phone1,ContactPersonName from CompanyWebsiteAddressMaster where CompanyWebsiteMasterId='" + ddlStore.SelectedValue + "'", con);
        DataTable dtaa = new DataTable();
        daaa.Fill(dtaa);

        if (dtaa.Rows.Count > 0)
        {
            txtconaddress.Text = dtaa.Rows[0]["address1"].ToString();
            txtpername.Text = dtaa.Rows[0]["ContactPersonName"].ToString();
            txtphoneno.Text = dtaa.Rows[0]["phone1"].ToString();
        }
    }
    protected void imgempadd_Click(object sender, ImageClickEventArgs e)
    {
        string te = "Addcompanyemail.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
    }
    protected void imgemprefresh_Click(object sender, ImageClickEventArgs e)
    {
        fillemail();
    }
    protected void fillpriceplandate()
    {
        SqlCommand cmd = new SqlCommand("select PricePlanMaster.* from CompanyMaster inner join PricePlanMaster on PricePlanMaster.ProductId=CompanyMaster.ProductId where  CompanyMaster.CompanyLoginId='" + Session["Comid"].ToString() + "'  and CompanyMaster.active='1'", PageConn.licenseconn());
        SqlDataAdapter dtp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        dtp.Fill(dt);

        if (dt.Rows.Count > 0)
        {
            ViewState["StartDate"] = dt.Rows[0]["StartDate"].ToString();
            ViewState["EndDate"] = dt.Rows[0]["EndDate"].ToString();

        }


    }
    protected void ImageButton6_Click(object sender, ImageClickEventArgs e)
    {
        string te = "VacancyPositionTitle.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
    }
    protected void ImageButton7_Click(object sender, ImageClickEventArgs e)
    {
        fillvacancytitle();
    }
    protected void chkphone_CheckedChanged(object sender, EventArgs e)
    {
        if (chkphone.Checked == true)
        {
            panelcontact.Visible = true;
        }
        else
        {
            panelcontact.Visible = false;
        }
    }
    protected void chkvisit_CheckedChanged(object sender, EventArgs e)
    {
        if (chkvisit.Checked == true)
        {
            panel1address.Visible = true;
        }
        else
        {
            panel1address.Visible = false;
        }
    }
    protected void ddldeptdesg_SelectedIndexChanged(object sender, EventArgs e)
    {
        string str = "select distinct VacancyMasterTbl.*,case when (VacancyMasterTbl.status='1') then 'Active' else 'Inactive' End as Statuslabel,VacancyDetailTbl.JobFunction,VacancyDetailTbl.QualificationRequirements,VacancyDetailTbl.TermsandConditions from VacancyMasterTbl inner join WareHouseMaster on WareHouseMaster.WareHouseId=VacancyMasterTbl.businessid inner join DesignationMaster on DesignationMaster.DesignationMasterId=VacancyMasterTbl.DesignationID inner join DepartmentmasterMNC on DesignationMaster.DeptID=DepartmentmasterMNC.id inner join VacancyTypeMaster on VacancyTypeMaster.ID=VacancyMasterTbl.vacancypositiontypeid inner join VacancyPositionTitleMaster  on VacancyPositionTitleMaster.ID=VacancyMasterTbl.vacancypositiontitleid inner join VacancyDetailTbl on VacancyDetailTbl.vacancymasterid=VacancyMasterTbl.ID where DesignationID = '" + ddldeptdesg.SelectedValue + "'";

        SqlDataAdapter da = new SqlDataAdapter(str, con);
        DataTable dt = new DataTable();
        da.Fill(dt);

        if (dt.Rows.Count > 0)
        {
            ddlvacancytype.SelectedIndex = ddlvacancytype.Items.IndexOf(ddlvacancytype.Items.FindByValue(dt.Rows[0]["vacancypositiontypeid"].ToString()));

            fillvacancytitle();

            ddlvacancyposition.SelectedIndex = ddlvacancyposition.Items.IndexOf(ddlvacancyposition.Items.FindByValue(dt.Rows[0]["vacancypositiontitleid"].ToString()));

            ddlduration.SelectedIndex = ddlduration.Items.IndexOf(ddlduration.Items.FindByValue(dt.Rows[0]["vacancyftptid"].ToString()));

            ddlCountry.SelectedIndex = ddlCountry.Items.IndexOf(ddlCountry.Items.FindByValue(dt.Rows[0]["countryid"].ToString()));

            fillstate();

            ddlState.SelectedIndex = ddlState.Items.IndexOf(ddlState.Items.FindByValue(dt.Rows[0]["stateid"].ToString()));

            fillcity();

            ddlCity.SelectedIndex = ddlCity.Items.IndexOf(ddlCity.Items.FindByValue(dt.Rows[0]["cityid"].ToString()));

            txtvacancy.Text = dt.Rows[0]["noofvacancy"].ToString();

            //txtestartdate.Text = dt.Rows[0]["startdate"].ToString();

            //txteenddate.Text = dt.Rows[0]["enddate"].ToString();

            txtjobfunction.Text = dt.Rows[0]["JobFunction"].ToString();

            txtqualireq.Text = dt.Rows[0]["QualificationRequirements"].ToString();

            txttermcond.Text = dt.Rows[0]["TermsandConditions"].ToString();
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        fillgrid();
    }
    protected void ddlstatus_search_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        filterdept();
    }

    protected void filterdept()
    {
        DropDownList2.Items.Clear();

        string str1 = "select DesignationMaster.DesignationMasterId,DepartmentmasterMNC.Departmentname + ':'+DesignationMaster.DesignationName as name FROM DepartmentmasterMNC INNER JOIN DesignationMaster ON DesignationMaster.DeptID = DepartmentmasterMNC.id where Companyid='" + Session["Comid"].ToString() + "' and Whid='" + DropDownList1.SelectedValue + "' ORDER BY DepartmentmasterMNC.Departmentname,DesignationMaster.DesignationName ";
        DataTable ds1 = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(str1, con);
        da.Fill(ds1);
        if (ds1.Rows.Count > 0)
        {
            DropDownList2.DataSource = ds1;
            DropDownList2.DataTextField = "name";
            DropDownList2.DataValueField = "DesignationMasterId";
            DropDownList2.DataBind();
        }
        DropDownList2.Items.Insert(0, "All");
        DropDownList2.SelectedItem.Value = "0";
    }

    public void fillnewduration()
    {
        DropDownList3.Items.Clear();

        string str1 = "select Name,ID from VacancyDuration where Active='1' ";
        DataTable ds1 = new DataTable();
        SqlDataAdapter da = new SqlDataAdapter(str1, con);
        da.Fill(ds1);
        if (ds1.Rows.Count > 0)
        {
            DropDownList3.DataSource = ds1;
            DropDownList3.DataTextField = "Name";
            DropDownList3.DataValueField = "ID";
            DropDownList3.DataBind();
        }
        DropDownList3.Items.Insert(0, "-Select-");
        DropDownList3.SelectedItem.Value = "0";
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        DataTable dtvac = new DataTable();

        if (Convert.ToString(ViewState["datavac"]) == "")
        {
            DataRow Drow = dtvac.NewRow();

            DataColumn Dcom = new DataColumn();

            Dcom.DataType = System.Type.GetType("System.String");
            Dcom.ColumnName = "Country";
            Dcom.AllowDBNull = true;
            Dcom.Unique = false;
            Dcom.ReadOnly = false;

            DataColumn Dcom1 = new DataColumn();
            Dcom1.DataType = System.Type.GetType("System.String");
            Dcom1.ColumnName = "State";
            Dcom1.AllowDBNull = true;
            Dcom1.Unique = false;
            Dcom1.ReadOnly = false;

            DataColumn Dcom2 = new DataColumn();
            Dcom2.DataType = System.Type.GetType("System.String");
            Dcom2.ColumnName = "City";
            Dcom2.AllowDBNull = true;
            Dcom2.Unique = false;
            Dcom2.ReadOnly = false;

            DataColumn Dcom3 = new DataColumn();
            Dcom3.DataType = System.Type.GetType("System.String");
            Dcom3.ColumnName = "CountryID";
            Dcom3.AllowDBNull = true;
            Dcom3.Unique = false;
            Dcom3.ReadOnly = false;

            DataColumn Dcom4 = new DataColumn();
            Dcom4.DataType = System.Type.GetType("System.String");
            Dcom4.ColumnName = "StateID";
            Dcom4.AllowDBNull = true;
            Dcom4.Unique = false;
            Dcom4.ReadOnly = false;

            DataColumn Dcom5 = new DataColumn();
            Dcom5.DataType = System.Type.GetType("System.String");
            Dcom5.ColumnName = "CityID";
            Dcom5.AllowDBNull = true;
            Dcom5.Unique = false;
            Dcom5.ReadOnly = false;

            dtvac.Columns.Add(Dcom);
            dtvac.Columns.Add(Dcom1);
            dtvac.Columns.Add(Dcom2);
            dtvac.Columns.Add(Dcom3);
            dtvac.Columns.Add(Dcom4);
            dtvac.Columns.Add(Dcom5);

            Drow["Country"] = ddlCountry.SelectedItem.Text;
            Drow["CountryID"] = ddlCountry.SelectedValue;

            Drow["State"] = ddlState.SelectedItem.Text;
            Drow["StateID"] = ddlState.SelectedValue;

            Drow["City"] = ddlCity.SelectedItem.Text;
            Drow["CityID"] = ddlCity.SelectedValue;

            dtvac.Rows.Add(Drow);
            ViewState["datavac"] = dtvac;

            panelgr.Visible = true;
        }
        else
        {
            dtvac = (DataTable)ViewState["datavac"];

            int flag = 0;

            foreach (DataRow dr in dtvac.Rows)
            {
                string countr = dr["Country"].ToString();
                string stat = dr["State"].ToString();
                string cit = dr["City"].ToString();

                if (countr == ddlCountry.SelectedItem.Text && stat == ddlState.SelectedItem.Text && cit == ddlCity.SelectedItem.Text)
                {
                    statuslable.Text = "Record already exist";
                    flag = 1;
                    break;
                }
            }
            if (flag == 0)
            {
                DataRow Drow = dtvac.NewRow();

                Drow["Country"] = ddlCountry.SelectedItem.Text;
                Drow["CountryID"] = ddlCountry.SelectedValue;

                Drow["State"] = ddlState.SelectedItem.Text;
                Drow["StateID"] = ddlState.SelectedValue;

                Drow["City"] = ddlCity.SelectedItem.Text;
                Drow["CityID"] = ddlCity.SelectedValue;

                dtvac.Rows.Add(Drow);
                ViewState["datavac"] = dtvac;
            }
        }

        GridView5.DataSource = ViewState["datavac"];
        GridView5.DataBind();
    }

    protected void GridView5_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "del")
        {
            GridView5.SelectedIndex = Convert.ToInt32(e.CommandArgument);

            DataTable dt = new DataTable();
            dt = (DataTable)ViewState["datavac"];

            dt.Rows[Convert.ToInt32(GridView5.SelectedIndex.ToString())].Delete();

            dt.AcceptChanges();

            GridView5.DataSource = dt;
            GridView5.DataBind();

            ViewState["datavac"] = dt;

            statuslable.Text = "Record deleted successfully.";
        }
    }

    protected void fillvacancytitleoutside()
    {
        DropDownList4.Items.Clear();

        SqlDataAdapter dav = new SqlDataAdapter("select ID,VacancyPositionTitle from VacancyPositionTitleMaster where Active=1", con);
        DataTable dtv = new DataTable();
        dav.Fill(dtv);

        if (dtv.Rows.Count > 0)
        {
            DropDownList4.DataSource = dtv;
            DropDownList4.DataTextField = "VacancyPositionTitle";
            DropDownList4.DataValueField = "ID";
            DropDownList4.DataBind();
        }

        DropDownList4.Items.Insert(0, "All");
        DropDownList4.Items[0].Value = "0";
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        fillgrid();
    }

    public Boolean Usercon11update(string tablename, string tblforeignkeyid, String countId, string std, string etd, String CID, String Inque)
    {
        HttpContext.Current.Session["msgdata"] = "";
        PageConn pgcon = new PageConn();

        SqlConnection con = pgcon.dynconn;
        Boolean ins = true;
        SqlCommand cmd = new SqlCommand();
        DataTable dt = new DataTable();

        SqlCommand cmdt = new SqlCommand();
        DataTable dtt = new DataTable();

        string adf = "select Distinct NameofRest, ClientProductTableMaster.TableName,Priceplanrestrecordallowtbl.RecordsAllowed,PriceplanrestrictionTbl.FieldrestrictionSet,PriceplanrestrictionTbl.RestrictfieldId from ClientProductTableMaster inner join PriceplanrestrictionTbl on PriceplanrestrictionTbl.TableId=ClientProductTableMaster.Id inner join " +
        " Priceplanrestrecordallowtbl on Priceplanrestrecordallowtbl.PriceplanRestrictiontblId=PriceplanrestrictionTbl.Id  where ClientProductTableMaster.TableName='" + ClsEncDesc.EncDyn(tablename) + "'  and " +
        " Priceplanrestrecordallowtbl.PricePlanId='" + ClsEncDesc.EncDyn(HttpContext.Current.Session["PriceId"].ToString()) + "'";

        cmd = new SqlCommand(adf, PageConn.busclient());

        SqlDataAdapter ad = new SqlDataAdapter(cmd);
        ad.Fill(dt);

        foreach (DataRow item in dt.Rows)
        {
            string filterd = "";

            int noofrecord = Convert.ToInt32(ClsEncDesc.DecDyn(Convert.ToString(item["RecordsAllowed"])));

            ViewState["updf"] = noofrecord;

            if (ClsEncDesc.DecDyn(Convert.ToString(item["RestrictfieldId"])) != "")
            {
                filterd = " and " + ClsEncDesc.DecDyn(Convert.ToString(item["FieldrestrictionSet"])) + "='" + ClsEncDesc.DecDyn(Convert.ToString(item["RestrictfieldId"])) + "'";
            }
            try
            {
                string adrt = "Select Count(*) from  " + tablename + " where " + CID + "='" + HttpContext.Current.Session["Comid"].ToString() + "' and status='1' ";

                cmdt = new SqlCommand(adrt, con);
                SqlDataAdapter adt = new SqlDataAdapter(cmdt);
                adt.Fill(dtt);

                if (Convert.ToString(dtt.Rows[0][0]) != "")
                {
                    if ((noofrecord <= Convert.ToInt32(dtt.Rows[0][0])) || (noofrecord < Convert.ToInt32(dtt.Rows[0][0]) && ddlstatus.SelectedValue == "1"))
                    {
                        if (ddlstatus.SelectedValue == "0")
                        {
                            ins = true;
                        }
                        else
                        {
                            ins = false;
                        }
                        HttpContext.Current.Session["msgdata"] = "You have reached the limit of alllowed records for" + "<" + Convert.ToString(item["NameofRest"]) + ">" + ". Please upgrade your plan to increase the limit.";
                        break;
                    }
                }
            }
            catch
            {
                ins = true;
            }
        }
        return ins;
    }

    public Boolean Usercon11submit(string tablename, string tblforeignkeyid, String countId, string std, string etd, String CID, String Inque)
    {
        HttpContext.Current.Session["msgdata"] = "";
        PageConn pgcon = new PageConn();

        SqlConnection con = pgcon.dynconn;
        Boolean ins = true;
        SqlCommand cmd = new SqlCommand();
        DataTable dt = new DataTable();

        SqlCommand cmdt = new SqlCommand();
        DataTable dtt = new DataTable();

        string adf = "select Distinct NameofRest, ClientProductTableMaster.TableName,Priceplanrestrecordallowtbl.RecordsAllowed,PriceplanrestrictionTbl.FieldrestrictionSet,PriceplanrestrictionTbl.RestrictfieldId from ClientProductTableMaster inner join PriceplanrestrictionTbl on PriceplanrestrictionTbl.TableId=ClientProductTableMaster.Id inner join " +
        " Priceplanrestrecordallowtbl on Priceplanrestrecordallowtbl.PriceplanRestrictiontblId=PriceplanrestrictionTbl.Id  where ClientProductTableMaster.TableName='" + ClsEncDesc.EncDyn(tablename) + "'  and " +
        " Priceplanrestrecordallowtbl.PricePlanId='" + ClsEncDesc.EncDyn(HttpContext.Current.Session["PriceId"].ToString()) + "'";

        cmd = new SqlCommand(adf, PageConn.busclient());

        SqlDataAdapter ad = new SqlDataAdapter(cmd);
        ad.Fill(dt);

        foreach (DataRow item in dt.Rows)
        {
            string filterd = "";

            int noofrecord = Convert.ToInt32(ClsEncDesc.DecDyn(Convert.ToString(item["RecordsAllowed"])));

            ViewState["mynum"] = noofrecord;

            if (ClsEncDesc.DecDyn(Convert.ToString(item["RestrictfieldId"])) != "")
            {
                filterd = " and " + ClsEncDesc.DecDyn(Convert.ToString(item["FieldrestrictionSet"])) + "='" + ClsEncDesc.DecDyn(Convert.ToString(item["RestrictfieldId"])) + "'";
            }
            try
            {
                string adrt = "Select Count(*) from  " + tablename + " where " + CID + "='" + HttpContext.Current.Session["Comid"].ToString() + "' and status='1' ";

                cmdt = new SqlCommand(adrt, con);
                SqlDataAdapter adt = new SqlDataAdapter(cmdt);
                adt.Fill(dtt);

                if (Convert.ToString(dtt.Rows[0][0]) != "")
                {
                    if (noofrecord <= Convert.ToInt32(dtt.Rows[0][0]))
                    {
                        if (ddlstatus.SelectedValue == "0")
                        {
                            ins = true;
                        }
                        else
                        {
                            ins = false;
                        }
                        HttpContext.Current.Session["msgdata"] = "You have reached the limit of alllowed records for" + "<" + Convert.ToString(item["NameofRest"]) + ">" + ". Please upgrade your plan to increase the limit.";
                        break;
                    }
                }
            }
            catch
            {
                ins = true;
            }
        }
        return ins;
    }
}
