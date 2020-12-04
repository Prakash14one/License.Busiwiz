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


public partial class ShoppingCart_Admin_Master_Default : System.Web.UI.Page
{
    SqlConnection con;

    string compid;

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {
            fillsqlconn();

            string sess = "select warehousemaster.warehouseid from warehousemaster where warehousemaster.comid='" + Convert.ToString(Session["Comid"]) + "'";

            SqlDataAdapter daza123 = new SqlDataAdapter(sess, con);
            DataTable dtza123 = new DataTable();
            daza123.Fill(dtza123);

            if (dtza123.Rows.Count > 0)
            {
                SqlDataAdapter da11 = new SqlDataAdapter("select LogoUrl from CompanyWebsitMaster where whid='" + dtza123.Rows[0]["warehouseid"].ToString() + "'", con);
                DataTable dt11 = new DataTable();
                da11.Fill(dt11);

                if (dt11.Rows.Count > 0)
                {
                    Img1.ImageUrl = "~/ShoppingCart/images/" + dt11.Rows[0]["LogoUrl"].ToString();
                }

                SqlDataAdapter daza = new SqlDataAdapter("select warehousemaster.Name,CityMasterTbl.CityName,StateMasterTbl.StateName,CountryMaster.CountryName,CompanyWebsiteAddressMaster.TollFree1,CompanyWebsiteAddressMaster.Address1,CompanyWebsiteAddressMaster.Zip,CompanyWebsiteAddressMaster.Address2,CompanyWebsiteAddressMaster.Phone1,CompanyWebsiteAddressMaster.Phone2,CompanyWebsiteAddressMaster.Email from CompanyWebsiteAddressMaster inner join warehousemaster on warehousemaster.warehouseid=CompanyWebsiteAddressMaster.CompanyWebsiteMasterId inner join CityMasterTbl on CityMasterTbl.CityId=CompanyWebsiteAddressMaster.City inner join StateMasterTbl on StateMasterTbl.StateId=CompanyWebsiteAddressMaster.State inner join CountryMaster on CountryMaster.CountryId=CompanyWebsiteAddressMaster.Country where CompanyWebsiteAddressMaster.CompanyWebsiteMasterId='" + dtza123.Rows[0]["warehouseid"].ToString() + "'", con);
                DataTable dtza = new DataTable();
                daza.Fill(dtza);

                if (dtza.Rows.Count > 0)
                {
                    lblcompanyname.Text = dtza.Rows[0]["Name"].ToString();

                    lbladdress1.Text = "";

                    if (Convert.ToString(dtza.Rows[0]["TollFree1"]) != "")
                    {
                        lbladdress1.Text += "Tollfree : " + dtza.Rows[0]["TollFree1"].ToString();
                    }
                    if (Convert.ToString(dtza.Rows[0]["Phone1"]) != "" && Convert.ToString(dtza.Rows[0]["TollFree1"]) != "")
                    {
                        lbladdress1.Text += " , Ph : " + dtza.Rows[0]["Phone1"].ToString();
                    }
                    else
                    {
                        lbladdress1.Text += "Ph : " + dtza.Rows[0]["Phone1"].ToString();
                    }
                    //lbladdress1.Text = "Tollfree : " + dtza.Rows[0]["TollFree1"].ToString() + " , Ph : " + dtza.Rows[0]["Phone1"].ToString();

                    if (Convert.ToString(dtza.Rows[0]["Email"]) != "")
                    {
                        lblphoneno.Text = "Email : " + dtza.Rows[0]["Email"].ToString();
                    }
                }
            }

            fillcountry();
            fillstate();
            fillcity();

            fillcountry1();
            fillstate1();
            fillcity1();

            if (Request.QueryString["Id"] != null)
            {
                int id = 0;

                id = Convert.ToInt32(Request.QueryString["Id"]);
                ViewState["loginid"] = id;

                SqlDataAdapter da11 = new SqlDataAdapter("select LogoUrl from CompanyWebsitMaster where whid='" + id + "'", con);
                DataTable dt11 = new DataTable();
                da11.Fill(dt11);

                if (dt11.Rows.Count > 0)
                {
                    Img1.ImageUrl = "~/ShoppingCart/images/" + dt11.Rows[0]["LogoUrl"].ToString();
                }

                SqlDataAdapter daza = new SqlDataAdapter("select warehousemaster.Name,CityMasterTbl.CityName,CompanyWebsiteAddressMaster.TollFree1,StateMasterTbl.StateName,CountryMaster.CountryName,CompanyWebsiteAddressMaster.Address1,CompanyWebsiteAddressMaster.Zip,CompanyWebsiteAddressMaster.Address2,CompanyWebsiteAddressMaster.Phone1,CompanyWebsiteAddressMaster.Phone2,CompanyWebsiteAddressMaster.Email from CompanyWebsiteAddressMaster inner join warehousemaster on warehousemaster.warehouseid=CompanyWebsiteAddressMaster.CompanyWebsiteMasterId inner join CityMasterTbl on CityMasterTbl.CityId=CompanyWebsiteAddressMaster.City inner join StateMasterTbl on StateMasterTbl.StateId=CompanyWebsiteAddressMaster.State inner join CountryMaster on CountryMaster.CountryId=CompanyWebsiteAddressMaster.Country where CompanyWebsiteAddressMaster.CompanyWebsiteMasterId='" + id + "'", con);
                DataTable dtza = new DataTable();
                daza.Fill(dtza);

                if (dtza.Rows.Count > 0)
                {
                    lbladdress1.Text = "";

                    if (Convert.ToString(dtza.Rows[0]["TollFree1"]) != "")
                    {
                        lbladdress1.Text += "Tollfree : " + dtza.Rows[0]["TollFree1"].ToString();
                    }
                    if (Convert.ToString(dtza.Rows[0]["Phone1"]) != "" && Convert.ToString(dtza.Rows[0]["TollFree1"]) != "")
                    {
                        lbladdress1.Text += " , Ph : " + dtza.Rows[0]["Phone1"].ToString();
                    }
                    else
                    {
                        lbladdress1.Text += "Ph : " + dtza.Rows[0]["Phone1"].ToString();
                    }
                    //lbladdress1.Text = "Tollfree : " + dtza.Rows[0]["TollFree1"].ToString() + " , Ph : " + dtza.Rows[0]["Phone1"].ToString();

                    if (Convert.ToString(dtza.Rows[0]["Email"]) != "")
                    {
                        lblphoneno.Text = "Email : " + dtza.Rows[0]["Email"].ToString();
                    }

                    //lblcompanyname.Text = dtza.Rows[0]["Name"].ToString();
                    //lbladdress1.Text = dtza.Rows[0]["Address1"].ToString();
                    //lbltollfreeno.Text = dtza.Rows[0]["Address2"].ToString();
                    //lblphoneno.Text = dtza.Rows[0]["Phone1"].ToString();
                    //lblemail.Text = dtza.Rows[0]["Email"].ToString();
                    //lblcs.Text = dtza.Rows[0]["CityName"].ToString() + "," + dtza.Rows[0]["StateName"].ToString() + "," + dtza.Rows[0]["CountryName"].ToString();
                }

                panelreg.Visible = true;
                paneltop.Visible = false;

                String str1 = "select distinct VacancyMasterTbl.*,SalaryPerPeriodMaster.Name as sss,LEFT(VacancyDetailTbl.JobFunction,100) as JobFunction,LEFT(VacancyDetailTbl.QualificationRequirements,100) as QualificationRequirements,CityMasterTbl.CityName,StateMasterTbl.StateName,CountryMaster.CountryName,VacancyDetailTbl.TermsandConditions,CurrencyMaster.CurrencyName as ccc,VacancyFTPT.Name as vvv,case when (VacancyMasterTbl.status='1') then 'Active' else 'Inactive' End as Statuslabel,WareHouseMaster.Name as wname,DepartmentmasterMNC.Departmentname + ':' + DesignationMaster.DesignationName as dname,VacancyTypeMaster.Name as vname,VacancyPositionTitleMaster.VacancyPositionTitle as vtitle from VacancyMasterTbl inner join WareHouseMaster on WareHouseMaster.WareHouseId=VacancyMasterTbl.businessid  inner join DesignationMaster on DesignationMaster.DesignationMasterId=VacancyMasterTbl.DesignationID   inner join VacancyDetailTbl on VacancyDetailTbl.ID=VacancyMasterTbl.ID   inner join CityMasterTbl on CityMasterTbl.CityId=VacancyMasterTbl.cityid  inner join StateMasterTbl on StateMasterTbl.StateId=VacancyMasterTbl.stateid   inner join CountryMaster on CountryMaster.CountryId=VacancyMasterTbl.countryid inner join DepartmentmasterMNC on DesignationMaster.DeptID=DepartmentmasterMNC.id inner join VacancyTypeMaster on VacancyTypeMaster.ID=VacancyMasterTbl.vacancypositiontypeid inner join VacancyPositionTitleMaster  on VacancyPositionTitleMaster.ID=VacancyMasterTbl.vacancypositiontitleid inner join VacancyFTPT on VacancyMasterTbl.vacancyftptid=VacancyFTPT.Id inner join CurrencyMaster on CurrencyMaster.CurrencyId=VacancyMasterTbl.currencyid inner join SalaryPerPeriodMaster on SalaryPerPeriodMaster.ID=VacancyMasterTbl.salaryperperiodid  where VacancyMasterTbl.BusinessID='" + id + "' and VacancyMasterTbl.status='1' and VacancyMasterTbl.enddate>='" + System.DateTime.Now.ToShortDateString() + "'";

                SqlDataAdapter da = new SqlDataAdapter(str1, con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                GridView1.DataSource = dt;
                GridView1.DataBind();

            }
            else if (Request.QueryString["cityid"] != null)
            {
                fillbussss();
                paneltop.Visible = false;
                fillvacancy11();
                fillduration11();
                panelreg.Visible = true;
                fillgrid();
            }
            else
            {
                fillbussss();

                fillvacancy11();
                fillduration11();

                fillgrid();
            }
        }
    }

    protected void fillsqlconn()
    {
        string[] separator1 = new string[] { "." };
        string[] strSplitArr1 = Request.Url.Host.Split(separator1, StringSplitOptions.RemoveEmptyEntries);

        if (strSplitArr1.Length > 0)
        {
            if (con != null)
            {
                if (Convert.ToString(con.ConnectionString) == "")
                {
                    Session["Comid"] = strSplitArr1[0].ToString();
                    PageConn cnd = new PageConn();
                    con = new SqlConnection(PageConn.connnn);
                }
            }
            else
            {
                // Session["Comid"] = "1133";
                Session["Comid"] = strSplitArr1[0].ToString();
                PageConn cnd = new PageConn();
                con = new SqlConnection(PageConn.connnn);
            }
        }

    }

    protected void btnadd_Click(object sender, EventArgs e)
    {
        Pnladdnew.Visible = true;
        if (Pnladdnew.Visible == true)
        {
            btnadd.Visible = false;
        }
        //statuslable.Text = "";
        //  lbllegend.Text = "Add Vacancy";
        txtestartdate.Text = System.DateTime.Now.ToShortDateString();
        txteenddate.Text = System.DateTime.Now.ToShortDateString();
    }

    protected void fillstore()
    {
        ddlStore.Items.Clear();

        SqlDataAdapter da = new SqlDataAdapter("SELECT Distinct WareHouseId,Name  FROM WareHouseMaster inner join EmployeeWarehouseRights on EmployeeWarehouseRights.Whid=WareHouseMaster.WareHouseId where comid = @CID and WareHouseMaster.Status='1' and EmployeeWarehouseRights.AccessAllowed='True' order by name", con);
        DataTable ds = new DataTable();
        da.Fill(ds);

        //   DataTable ds = ClsStore.SelectStorename();
        ddlStore.DataSource = ds;
        ddlStore.DataTextField = "Name";
        ddlStore.DataValueField = "WareHouseId";
        ddlStore.DataBind();
        //ddlstore.Items.Insert(0, "Select");

        //ViewState["cd"] = "1";
        //DataTable dteeed = ClsStore.SelectEmployeewithIdwise();

        //if (dteeed.Rows.Count > 0)
        //{
        //    ddlStore.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);
        //}
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
        if (ViewState["StartDate"] != null && ViewState["EndDate"] != null)
        {
            if (Convert.ToDateTime(txtestartdate.Text) >= Convert.ToDateTime(ViewState["StartDate"]) && Convert.ToDateTime(txtestartdate.Text) <= Convert.ToDateTime(ViewState["EndDate"]) && Convert.ToDateTime(txteenddate.Text) >= Convert.ToDateTime(ViewState["StartDate"]) && Convert.ToDateTime(txteenddate.Text) <= Convert.ToDateTime(ViewState["EndDate"]))
            {
                string insert = "";
                if (CheckBox2.Checked == false)
                {
                    insert = "insert into VacancyMasterTbl values('" + ddlStore.SelectedValue + "','" + ddldeptdesg.SelectedValue + "','" + ddlvacancytype.SelectedValue + "','" + ddlvacancyposition.SelectedValue + "','" + txtvacancy.Text + "','" + txtestartdate.Text + "','" + txteenddate.Text + "','" + ddlcurrency.SelectedValue + "','" + txtmysalary.Text + "','" + ddlsalperiod.SelectedValue + "','" + txtworktimings.Text + "','" + txtnoofhours.Text + "','" + ddlhourperiod.SelectedValue + "','" + ddlduration.SelectedValue + "','" + ddlstatus.SelectedValue + "','" + txtpername.Text + "','" + ddlemail.SelectedItem.Text + "','" + txtphoneno.Text + "','" + txtconaddress.Text + "','" + chkemail.Checked + "','" + chkphone.Checked + "','" + chkvisit.Checked + "','" + chkonline.Checked + "','" + ddlCountry.SelectedValue + "','" + ddlState.SelectedValue + "','" + ddlCity.SelectedValue + "')";
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

                        insert = "insert into VacancyMasterTbl values('" + ddlStore.SelectedValue + "','" + ddldeptdesg.SelectedValue + "','" + ddlvacancytype.SelectedValue + "','" + dt1.Rows[0]["ID"].ToString() + "','" + txtvacancy.Text + "','" + txtestartdate.Text + "','" + txteenddate.Text + "','" + ddlcurrency.SelectedValue + "','" + txtmysalary.Text + "','" + ddlsalperiod.SelectedValue + "','" + txtworktimings.Text + "','" + txtnoofhours.Text + "','" + ddlhourperiod.SelectedValue + "','" + ddlduration.SelectedValue + "','" + ddlstatus.SelectedValue + "','" + txtpername.Text + "','" + ddlemail.SelectedItem.Text + "','" + txtphoneno.Text + "','" + txtconaddress.Text + "','" + chkemail.Checked + "','" + chkphone.Checked + "','" + chkvisit.Checked + "','" + chkonline.Checked + "','" + ddlCountry.SelectedValue + "','" + ddlState.SelectedValue + "','" + ddlCity.SelectedValue + "')";

                    }

                    fillvacancytitle();
                }
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
                    SqlCommand cmm = new SqlCommand("insert into VacancyDetailTbl values('" + dta.Rows[0]["ID"].ToString() + "','" + txtjobfunction.Text + "','" + txtqualireq.Text + "','" + txttermcond.Text + "')", con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmm.ExecuteNonQuery();
                    con.Close();
                }

                fillgrid();
                clear();
                //statuslable.Text = "Record inserted successfully";
                btnadd.Visible = true;
                Pnladdnew.Visible = false;

            }
            else
            {
                //statuslable.Visible = true;
                //statuslable.Text = "Invalid date. Date is outside of subscription period. Please insert a date within your subscription period.";
            }

        }
        else
        {
            //statuslable.Visible = true;
            //statuslable.Text = "Invalid date. Date is outside of subscription period. Please insert a date within your subscription period.";
        }
    }

    protected void fillgrid()
    {
        string st1 = "";

        if (ddlstore1.SelectedIndex > 0)
        {
            st1 += " and VacancyMasterTbl.BusinessID='" + ddlstore1.SelectedValue + "'";
        }
        if (ddlcountry1.SelectedIndex > 0)
        {
            st1 += " and VacancyMasterTbl.countryid='" + ddlcountry1.SelectedValue + "'";
        }
        if (ddlstate1.SelectedIndex > 0)
        {
            st1 += " and VacancyMasterTbl.stateid='" + ddlstate1.SelectedValue + "'";
        }
        if (ddlcity1.SelectedIndex > 0)
        {
            st1 += " and VacancyMasterTbl.cityid='" + ddlcity1.SelectedValue + "'";
        }
        if (DropDownList1.SelectedIndex > 0)
        {
            st1 += " and VacancyMasterTbl.vacancypositiontypeid='" + DropDownList1.SelectedValue + "'";
        }
        if (DropDownList2.SelectedIndex > 0)
        {
            st1 += " and VacancyMasterTbl.vacancyftptid='" + DropDownList2.SelectedValue + "'";
        }
        if (Request.QueryString["cityid"] != null)
        {
            st1 += " and VacancyMasterTbl.cityid='" + Request.QueryString["cityid"].ToString() + "'";
        }

        String str1 = "select distinct VacancyMasterTbl.*,SalaryPerPeriodMaster.Name as sss,LEFT(VacancyDetailTbl.JobFunction,100) as JobFunction,LEFT(VacancyDetailTbl.QualificationRequirements,100) as QualificationRequirements,CityMasterTbl.CityName,StateMasterTbl.StateName,CountryMaster.CountryName,VacancyDetailTbl.TermsandConditions,CurrencyMaster.CurrencyName as ccc,VacancyFTPT.Name as vvv,case when (VacancyMasterTbl.status='1') then 'Active' else 'Inactive' End as Statuslabel,WareHouseMaster.Name as wname,DepartmentmasterMNC.Departmentname + ':' + DesignationMaster.DesignationName as dname,VacancyTypeMaster.Name as vname,VacancyPositionTitleMaster.VacancyPositionTitle as vtitle from VacancyMasterTbl inner join WareHouseMaster on WareHouseMaster.WareHouseId=VacancyMasterTbl.businessid  inner join DesignationMaster on DesignationMaster.DesignationMasterId=VacancyMasterTbl.DesignationID   inner join VacancyDetailTbl on VacancyDetailTbl.ID=VacancyMasterTbl.ID   inner join CityMasterTbl on CityMasterTbl.CityId=VacancyMasterTbl.cityid  inner join StateMasterTbl on StateMasterTbl.StateId=VacancyMasterTbl.stateid   inner join CountryMaster on CountryMaster.CountryId=VacancyMasterTbl.countryid inner join DepartmentmasterMNC on DesignationMaster.DeptID=DepartmentmasterMNC.id inner join VacancyTypeMaster on VacancyTypeMaster.ID=VacancyMasterTbl.vacancypositiontypeid inner join VacancyPositionTitleMaster  on VacancyPositionTitleMaster.ID=VacancyMasterTbl.vacancypositiontitleid inner join VacancyFTPT on VacancyMasterTbl.vacancyftptid=VacancyFTPT.Id inner join CurrencyMaster on CurrencyMaster.CurrencyId=VacancyMasterTbl.currencyid inner join SalaryPerPeriodMaster on SalaryPerPeriodMaster.ID=VacancyMasterTbl.salaryperperiodid  where VacancyMasterTbl.status='1' and VacancyMasterTbl.enddate>='" + System.DateTime.Now.ToShortDateString() + "' " + st1 + "";

        SqlDataAdapter da = new SqlDataAdapter(str1, PageConn.connnn);
        DataTable dt = new DataTable();
        da.Fill(dt);

        GridView1.DataSource = dt;
        GridView1.DataBind();
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
        //   filldeprdesg();

        //    lbllegend.Text = "";

    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {
            CheckBox2.Checked = false;
            CheckBox2_CheckedChanged(sender, e);

            int mm = Convert.ToInt32(e.CommandArgument);
            ViewState["updateid"] = mm;

            //      lbllegend.Text = "Edit Vacancy";

            Pnladdnew.Visible = true;
            btnadd.Visible = false;

            //statuslable.Text = "";
            btnsubmit.Visible = false;
            btnupdate.Visible = true;

            SqlDataAdapter da1 = new SqlDataAdapter("select * from VacancyMasterTbl where ID=" + mm, con);
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);

            if (dt1.Rows.Count > 0)
            {
                fillstore();
                ddlStore.SelectedIndex = ddlStore.Items.IndexOf(ddlStore.Items.FindByValue(dt1.Rows[0]["BusinessID"].ToString()));

                //   filldeprdesg();
                ddldeptdesg.SelectedIndex = ddldeptdesg.Items.IndexOf(ddldeptdesg.Items.FindByValue(dt1.Rows[0]["DesignationID"].ToString()));

                fillvacancy();
                ddlvacancytype.SelectedIndex = ddlvacancytype.Items.IndexOf(ddlvacancytype.Items.FindByValue(dt1.Rows[0]["vacancypositiontypeid"].ToString()));


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
            //statuslable.Text = "Record deleted successfully";

        }

        if (e.CommandName == "View")
        {

            int mm2 = Convert.ToInt32(e.CommandArgument);

            //   lbllegend.Text = "View Vacancy";

            Pnladdnew.Visible = true;
            btnadd.Visible = false;

            //statuslable.Text = "";
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

                //    filldeprdesg();
                ddldeptdesg.SelectedIndex = ddldeptdesg.Items.IndexOf(ddldeptdesg.Items.FindByValue(dt1.Rows[0]["DesignationID"].ToString()));
                ddldeptdesg.Enabled = false;

                fillvacancy();
                ddlvacancytype.SelectedIndex = ddlvacancytype.Items.IndexOf(ddlvacancytype.Items.FindByValue(dt1.Rows[0]["vacancypositiontypeid"].ToString()));
                ddlvacancytype.Enabled = false;

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

                imgempadd.Visible = false;
                imgemprefresh.Visible = false;

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

        if (e.CommandName == "View1")
        {
            int dk = Convert.ToInt32(e.CommandArgument);
            string te = "vacancyprofile.aspx?id=" + dk;
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
        }

        if (e.CommandName == "link")
        {
            int linkk = Convert.ToInt32(e.CommandArgument);

            string te = "vacancyprofile.aspx?id=" + linkk;
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
        }


        if (e.CommandName == "link1")
        {
            int linkk1 = Convert.ToInt32(e.CommandArgument);

            string te = "vacancyprofile.aspx?id=" + linkk1;
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
        }


        if (e.CommandName == "link2")
        {
            int linkk2 = Convert.ToInt32(e.CommandArgument);

            string te = "vacancyprofile.aspx?id=" + linkk2;
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
        }


        if (e.CommandName == "link3")
        {
            int linkk3 = Convert.ToInt32(e.CommandArgument);

            string te = "vacancyprofile.aspx?id=" + linkk3;
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
        }
    }
    protected void btnupdate_Click(object sender, EventArgs e)
    {
        if (ViewState["StartDate"] != null && ViewState["EndDate"] != null)
        {
            if (Convert.ToDateTime(txtestartdate.Text) >= Convert.ToDateTime(ViewState["StartDate"]) && Convert.ToDateTime(txtestartdate.Text) <= Convert.ToDateTime(ViewState["EndDate"]) && Convert.ToDateTime(txteenddate.Text) >= Convert.ToDateTime(ViewState["StartDate"]) && Convert.ToDateTime(txteenddate.Text) <= Convert.ToDateTime(ViewState["EndDate"]))
            {
                string update = "";
                if (CheckBox2.Checked == false)
                {
                    update = "update VacancyMasterTbl set  BusinessID='" + ddlStore.SelectedValue + "',DesignationID='" + ddldeptdesg.SelectedValue + "',vacancypositiontypeid='" + ddlvacancytype.SelectedValue + "',vacancypositiontitleid='" + ddlvacancyposition.SelectedValue + "',noofvacancy='" + txtvacancy.Text + "',startdate='" + txtestartdate.Text + "',enddate='" + txteenddate.Text + "',currencyid='" + ddlcurrency.SelectedValue + "',salaryamount='" + txtmysalary.Text + "',salaryperperiodid='" + ddlsalperiod.SelectedValue + "',worktimings='" + txtworktimings.Text + "',hours='" + txtnoofhours.Text + "',hoursperperiodid='" + ddlhourperiod.SelectedValue + "',vacancyftptid='" + ddlduration.SelectedValue + "',status='" + ddlstatus.SelectedValue + "',contactname='" + txtpername.Text + "',contactEmail='" + ddlemail.SelectedItem.Text + "',contactphoneno='" + txtphoneno.Text + "',contactAddress='" + txtconaddress.Text + "',applybyemail='" + chkemail.Checked + "',applybyphone='" + chkphone.Checked + "',applybyvisit='" + chkvisit.Checked + "',applyonline='" + chkonline.Checked + "',countryid='" + ddlCountry.SelectedValue + "',stateid='" + ddlState.SelectedValue + "',cityid='" + ddlCity.SelectedValue + "' where ID='" + ViewState["updateid"].ToString() + "'";
                }
                if (CheckBox2.Checked == true)
                {
                    // SqlCommand cmd1 = new SqlCommand("insert into VacancyPositionTitleMaster values('" + ddlvacancytype.SelectedValue + "','" + txtnewvacancy.Text + "',0)", con);
                    SqlCommand cmd1 = new SqlCommand("update VacancyPositionTitleMaster set VacancyPositionTypeID='" + ddlvacancytype.SelectedValue + "',VacancyPositionTitle='" + txtnewvacancy.Text + "',Active='0' where ID='" + ViewState["vacancypositiontitleid"] + "' ", con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmd1.ExecuteNonQuery();
                    con.Close();

                    //SqlDataAdapter da1 = new SqlDataAdapter("select max(ID) as ID from VacancyPositionTitleMaster", con);
                    //DataTable dt1 = new DataTable();
                    //da1.Fill(dt1);

                    //if (dt1.Rows.Count > 0)
                    //{

                    update = "update VacancyMasterTbl set  BusinessID='" + ddlStore.SelectedValue + "',DesignationID='" + ddldeptdesg.SelectedValue + "',vacancypositiontypeid='" + ddlvacancytype.SelectedValue + "',vacancypositiontitleid='" + ViewState["vacancypositiontitleid"] + "',noofvacancy='" + txtvacancy.Text + "',startdate='" + txtestartdate.Text + "',enddate='" + txteenddate.Text + "',currencyid='" + ddlcurrency.SelectedValue + "',salaryamount='" + txtmysalary.Text + "',salaryperperiodid='" + ddlsalperiod.SelectedValue + "',worktimings='" + txtworktimings.Text + "',hours='" + txtnoofhours.Text + "',hoursperperiodid='" + ddlhourperiod.SelectedValue + "',vacancyftptid='" + ddlduration.SelectedValue + "',status='" + ddlstatus.SelectedValue + "',contactname='" + txtpername.Text + "',contactEmail='" + ddlemail.SelectedItem.Text + "',contactphoneno='" + txtphoneno.Text + "',contactAddress='" + txtconaddress.Text + "',applybyemail='" + chkemail.Checked + "',applybyphone='" + chkphone.Checked + "',applybyvisit='" + chkvisit.Checked + "',applyonline='" + chkonline.Checked + "',countryid='" + ddlCountry.SelectedValue + "',stateid='" + ddlState.SelectedValue + "',cityid='" + ddlCity.SelectedValue + "' where ID='" + ViewState["updateid"].ToString() + "'";

                    //}

                    fillvacancytitle();
                }
                SqlCommand cmd = new SqlCommand(update, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmd.ExecuteNonQuery();
                con.Close();


                SqlCommand cmm = new SqlCommand("update VacancyDetailTbl set JobFunction='" + txtjobfunction.Text + "',QualificationRequirements='" + txtqualireq.Text + "',TermsandConditions='" + txttermcond.Text + "' where vacancymasterid='" + ViewState["updateid"].ToString() + "'", con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmm.ExecuteNonQuery();
                con.Close();


                fillgrid();
                clear();
                //statuslable.Text = "Record updated successfully";
                btnsubmit.Visible = true;
                btnupdate.Visible = false;
                btnadd.Visible = true;
                Pnladdnew.Visible = false;
            }
            else
            {
                //statuslable.Visible = true;
                //statuslable.Text = "Invalid date. Date is outside of subscription period. Please insert a date within your subscription period.";
            }
        }
        else
        {
            //statuslable.Visible = true;
            //statuslable.Text = "Invalid date. Date is outside of subscription period. Please insert a date within your subscription period.";
        }

    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        clear();
        //  btnadd.Visible = true;
        Pnladdnew.Visible = false;
        //   lbllegend.Text = "";
        //statuslable.Text = "";
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



    protected void fillcountry()
    {
        string qryStr = "select CountryId,CountryName from CountryMaster order by CountryName";

        ddlCountry.DataSource = (DataTable)select(qryStr);
        fillddlOther(ddlCountry, "CountryName", "CountryId");
        ddlCountry.Items.Insert(0, "-Select-");
        ddlCountry.Items[0].Value = "0";
    }

    protected void fillcountry1()
    {
        string qryStr = "select CountryId,CountryName from CountryMaster where countryid in (select countryid from VacancyMasterTbl) order by CountryName";

        ddlcountry1.DataSource = (DataTable)select(qryStr);
        fillddlOther(ddlcountry1, "CountryName", "CountryId");
        ddlcountry1.Items.Insert(0, "All");
        ddlcountry1.Items[0].Value = "0";
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

    protected void fillstate1()
    {
        ddlstate1.Items.Clear();
        if (ddlcountry1.SelectedIndex > 0)
        {
            string qryStr = "select StateId,StateName from StateMasterTbl where CountryId=" + ddlcountry1.SelectedValue + " and StateId in (select stateid from VacancyMasterTbl) order by StateName";
            ddlstate1.DataSource = (DataTable)select(qryStr);
            fillddlOther(ddlstate1, "StateName", "StateId");
            ddlstate1.Items.Insert(0, "All");
            ddlstate1.Items[0].Value = "0";
        }
        else
        {
            ddlstate1.Items.Insert(0, "All");
            ddlstate1.Items[0].Value = "0";
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

    protected void fillcity1()
    {
        ddlcity1.Items.Clear();

        if (ddlstate1.SelectedIndex > 0)
        {
            string qryStr = "select CityId,CityName from CityMasterTbl where StateId=" + ddlstate1.SelectedValue + " and CityId in (select cityid from VacancyMasterTbl) order by CityName";
            ddlcity1.DataSource = (DataTable)select(qryStr);
            fillddlOther(ddlcity1, "CityName", "CityId");
            ddlcity1.Items.Insert(0, "All");
            ddlcity1.Items[0].Value = "0";
        }
        else
        {
            ddlcity1.Items.Insert(0, "All");
            ddlcity1.Items[0].Value = "0";
        }
    }

    protected DataTable select(string qu)
    {
        con = new SqlConnection(PageConn.connnn);

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
        //    fillpriceplandate();
        //   filldeprdesg();
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

    protected void Button1_Click(object sender, EventArgs e)
    {
        RadioButtonList1.SelectedValue = "1";

        foreach (GridViewRow gr in GridView1.Rows)
        {
            CheckBox cb = (CheckBox)gr.FindControl("chkMsg");

            if (cb.Checked == true)
            {
                ViewState["ff22"] = "1";
                Label4.Text = "";
            }
        }
        if (ViewState["ff22"] == "1")
        {
            panelpopo.Visible = true;
            panel2.Visible = true;
            panel1.Visible = false;

            ModalPopupExtender12.Show();
        }
        else
        {
            Label4.Text = "Please select atleast one job to apply.";
        }
    }

    protected void Button2_Click(object sender, EventArgs e)
    {
    }

    protected void fillbussss()
    {
        //string ins = "select distinct WareHouseMaster.Name + ' : ' + CityMasterTbl.CityName + ' : ' + StateMasterTbl.State_Code + ' : ' + CountryMaster.Country_Code as wname,WareHouseMaster.WareHouseId from CompanyWebsiteAddressMaster inner join WareHouseMaster on CompanyWebsiteAddressMaster.CompanyWebsiteMasterId=WareHouseMaster.WareHouseId inner join CountryMaster on CountryMaster.CountryId=CompanyWebsiteAddressMaster.Country inner join StateMasterTbl on StateMasterTbl.StateId=CompanyWebsiteAddressMaster.State inner join CityMasterTbl on CityMasterTbl.CityId=CompanyWebsiteAddressMaster.City where WareHouseMaster.Status='1' and [CompanyWebsiteAddressMaster].[AddressTypeMasterId]='1' order by wname";

        string ins = "select distinct WareHouseMaster.Name as wname,WareHouseMaster.WareHouseId from WareHouseMaster where WareHouseMaster.Status='1' and WareHouseMaster.comid='" + Session["Comid"].ToString() + "' order by WareHouseMaster.Name asc";

        SqlDataAdapter da = new SqlDataAdapter(ins, con);
        DataTable dt = new DataTable();
        da.Fill(dt);

        pnlbus.Visible = true;

        if (dt.Rows.Count > 0)
        {
            ddlstore1.DataSource = dt;
            ddlstore1.DataTextField = "wname";
            ddlstore1.DataValueField = "WareHouseId";
            ddlstore1.DataBind();
        }
        ddlstore1.Items.Insert(0, "All");
        ddlstore1.Items[0].Value = "0";
    }

    protected void Button3_Click(object sender, EventArgs e)
    {
        if (ddlstore1.SelectedIndex > 0)
        {
            SqlDataAdapter dass = new SqlDataAdapter("select VacancyMasterTbl.vacancypositiontitleid from VacancyMasterTbl where VacancyMasterTbl.BusinessID='" + ddlstore1.SelectedValue + "'", PageConn.connnn);
            DataTable dtss = new DataTable();
            dass.Fill(dtss);

            if (dtss.Rows.Count > 0)
            {
                panelreg.Visible = true;
                //TextBox5.Enabled = false;
                //Button3.Enabled = false;
                //Button2.Enabled = false;
                //ddlstore1.Enabled = false;

                SqlDataAdapter da11 = new SqlDataAdapter("select LogoUrl from CompanyWebsitMaster where whid='" + ddlstore1.SelectedValue + "'", PageConn.connnn);
                DataTable dt11 = new DataTable();
                da11.Fill(dt11);

                if (dt11.Rows.Count > 0)
                {
                    Img1.ImageUrl = "~/ShoppingCart/images/" + dt11.Rows[0]["LogoUrl"].ToString();
                }

                SqlDataAdapter daza = new SqlDataAdapter("select warehousemaster.Name,CityMasterTbl.CityName,CompanyWebsiteAddressMaster.TollFree1,StateMasterTbl.StateName,CountryMaster.CountryName,CompanyWebsiteAddressMaster.Address1,CompanyWebsiteAddressMaster.Zip,CompanyWebsiteAddressMaster.Address2,CompanyWebsiteAddressMaster.Phone1,CompanyWebsiteAddressMaster.Phone2,CompanyWebsiteAddressMaster.Email from CompanyWebsiteAddressMaster inner join warehousemaster on warehousemaster.warehouseid=CompanyWebsiteAddressMaster.CompanyWebsiteMasterId inner join CityMasterTbl on CityMasterTbl.CityId=CompanyWebsiteAddressMaster.City inner join StateMasterTbl on StateMasterTbl.StateId=CompanyWebsiteAddressMaster.State inner join CountryMaster on CountryMaster.CountryId=CompanyWebsiteAddressMaster.Country where CompanyWebsiteAddressMaster.CompanyWebsiteMasterId='" + ddlstore1.SelectedValue + "'", PageConn.connnn);
                DataTable dtza = new DataTable();
                daza.Fill(dtza);

                if (dtza.Rows.Count > 0)
                {
                    lblcompanyname.Text = dtza.Rows[0]["Name"].ToString();

                    lbladdress1.Text = "";

                    if (Convert.ToString(dtza.Rows[0]["TollFree1"]) != "")
                    {
                        lbladdress1.Text += "Tollfree : " + dtza.Rows[0]["TollFree1"].ToString();
                    }
                    if (Convert.ToString(dtza.Rows[0]["Phone1"]) != "" && Convert.ToString(dtza.Rows[0]["TollFree1"]) != "")
                    {
                        lbladdress1.Text += " , Ph : " + dtza.Rows[0]["Phone1"].ToString();
                    }
                    else
                    {
                        lbladdress1.Text += "Ph : " + dtza.Rows[0]["Phone1"].ToString();
                    }
                    //lbladdress1.Text = "Tollfree : " + dtza.Rows[0]["TollFree1"].ToString() + " , Ph : " + dtza.Rows[0]["Phone1"].ToString();

                    if (Convert.ToString(dtza.Rows[0]["Email"]) != "")
                    {
                        lblphoneno.Text = "Email : " + dtza.Rows[0]["Email"].ToString();
                    }
                    //lblcompanyname.Text = dtza.Rows[0]["Name"].ToString();
                    //lbladdress1.Text = dtza.Rows[0]["Address1"].ToString();
                    //lbltollfreeno.Text = dtza.Rows[0]["Address2"].ToString();
                    //lblphoneno.Text = dtza.Rows[0]["Phone1"].ToString();
                    //lblemail.Text = dtza.Rows[0]["Email"].ToString();
                    //lblcs.Text = dtza.Rows[0]["CityName"].ToString() + "," + dtza.Rows[0]["StateName"].ToString() + "," + dtza.Rows[0]["CountryName"].ToString();
                }
                fillgrid();
                Label18.Text = "";
            }
            else
            {
                panelreg.Visible = false;
                Label18.Text = "No Vacancies available for this Business.";
            }
        }
        else
        {
            panelreg.Visible = true;
            fillgrid();
            Label18.Text = "";

            //panelreg.Visible = false;
            //Label18.Text = "No Vacancies available for this Business.";
        }
    }

    protected void Button4_Click(object sender, EventArgs e)
    {

    }
    protected void Button7_Click(object sender, EventArgs e)
    {
        con = new SqlConnection(PageConn.connnn);

        string str = "select Login_master.username,Login_master.password,Party_master.PartyID FROM Login_master inner join User_master on User_master.UserID = Login_master.UserID  inner join Party_master on Party_master.PartyID = User_master.PartyID inner join EmployeeMaster on EmployeeMaster.PartyID=Party_master.PartyID  where Login_master.username = '" + TextBox1.Text + "' and Login_master.password='" + ClsEncDesc.Encrypted(TextBox4.Text) + "'";

        SqlDataAdapter da11 = new SqlDataAdapter(str, con);
        DataTable dt11 = new DataTable();
        da11.Fill(dt11);

        if (dt11.Rows.Count > 0)
        {
            ModalPopupExtender12.Show();

            if (Convert.ToString(dt11.Rows[0]["PartyID"]) != "")
            {
                SqlDataAdapter dapar = new SqlDataAdapter("select CandidateMaster.CandidateId from CandidateMaster where PartyID='" + Convert.ToString(dt11.Rows[0]["PartyID"]) + "'", con);
                DataTable dtpar = new DataTable();
                dapar.Fill(dtpar);

                if (dtpar.Rows.Count > 0)
                {
                    string msedd = "";

                    foreach (GridViewRow grd in GridView1.Rows)
                    {
                        CheckBox cb = (CheckBox)grd.FindControl("chkMsg");
                        Label LabelLogin = (Label)grd.FindControl("LabelLogin");
                        LinkButton LinkButton1 = (LinkButton)grd.FindControl("LinkButton1");

                        LinkButton1.Text = LinkButton1.Text;

                        if (cb.Checked == true)
                        {


                            SqlDataAdapter dafff = new SqlDataAdapter("select * from VacancyAppReceive where [candidateid]='" + dtpar.Rows[0]["CandidateId"].ToString() + "' and [vacancyid]='" + LabelLogin.Text + "'", con);
                            DataTable dtfff = new DataTable();
                            dafff.Fill(dtfff);

                            if (dtfff.Rows.Count > 0)
                            {
                                panelpopo.Visible = false;
                                panel1.Visible = true;
                                panel2.Visible = false;

                                Label33.Text = "you have already applied for the following job positions.";

                                ViewState["maks"] = "11";
                            }
                            else
                            {
                                msedd += LinkButton1.Text + " , ";

                                string vaca = "insert into VacancyAppReceive values('" + dtpar.Rows[0]["CandidateId"].ToString() + "','" + LabelLogin.Text + "')";

                                SqlCommand cmdvac = new SqlCommand(vaca, con);
                                if (con.State.ToString() != "Open")
                                {
                                    con.Open();
                                }
                                cmdvac.ExecuteNonQuery();
                                con.Close();

                                string myemail = "select MasterEmailId from CompanyWebsitMaster where whid='" + ViewState["loginid"] + "'";

                                SqlDataAdapter daff1 = new SqlDataAdapter(myemail, con);
                                DataTable dtff1 = new DataTable();
                                daff1.Fill(dtff1);

                                if (dtff1.Rows.Count > 0)
                                {
                                    lblemails.Text = dtff1.Rows[0]["MasterEmailId"].ToString();
                                }

                                panelpopo.Visible = false;
                                panel1.Visible = true;
                                panel2.Visible = false;

                                ViewState["maks"] = "22";
                            }
                        }
                    }

                    if (ViewState["maks"].ToString() == "11")
                    {
                        panelpopo.Visible = true;
                        panel1.Visible = false;
                        panel2.Visible = true;

                        Label83.Visible = true;
                        Label83.Text = "You have already applied for the selected position/s.";
                    }
                    if (ViewState["maks"].ToString() == "22")
                    {
                        int msle = msedd.Length;
                        int mdss = msle - 2;
                        Label33.Text = msedd.Substring(0, mdss);
                    }

                    if (ViewState["maks"].ToString() == "11" && ViewState["maks"].ToString() == "22")
                    {
                        Label83.Visible = true;
                        Label83.Text = "You have already applied for some of the selected position/s.";

                        panelpopo.Visible = false;
                        panel1.Visible = true;
                        panel2.Visible = false;

                        int msle = msedd.Length;
                        int mdss = msle - 2;
                        Label33.Text = msedd.Substring(0, mdss);
                    }

                    ViewState["maks"] = "";
                }
            }
        }
        else
        {
            ModalPopupExtender12.Show();

            Label83.Visible = true;
            Label83.Text = "User Name or Password possibly incorrect.";
        }
    }

    protected void Button8_Click(object sender, EventArgs e)
    {
        Label83.Text = "";
        TextBox1.Text = "";
        TextBox4.Text = "";
        ModalPopupExtender12.Show();
    }

    protected void Button5_Click(object sender, EventArgs e)
    {
        string te = "ShoppingCartLogin.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
    }
    protected void RadioButtonList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        con = new SqlConnection(PageConn.connnn);

        if (RadioButtonList1.SelectedValue == "0")
        {
            SqlDataAdapter da2 = new SqlDataAdapter("select max(ID) as ID from VacancyTemp", con);
            DataTable dt2 = new DataTable();
            da2.Fill(dt2);

            int a = 1;

            if (dt2.Rows.Count > 0)
            {
                if (Convert.ToString(dt2.Rows[0]["ID"]) != "")
                {
                    a = Convert.ToInt32(dt2.Rows[0]["ID"]) + 1;
                }
            }

            foreach (GridViewRow gr in GridView1.Rows)
            {
                CheckBox cb = (CheckBox)gr.FindControl("chkMsg");
                Label type = (Label)gr.FindControl("lblvacpositiontype");
                Label title = (Label)gr.FindControl("lblvacpositiontitle");
                Label busi = (Label)gr.FindControl("Label3");
                Label LabelLogin = (Label)gr.FindControl("LabelLogin");

                if (cb.Checked == true)
                {
                    ViewState["ff"] = "1";

                    string insert = "insert into VacancyTemp values('" + a + "','" + type.Text + "','" + title.Text + "','" + busi.Text + "','" + LabelLogin.Text + "')";

                    SqlCommand cmd = new SqlCommand(insert, con);
                    if (con.State.ToString() != "Open")
                    {
                        con.Open();
                    }
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }

            if (ViewState["ff"] == "1")
            {
                Label4.Text = "";

                SqlDataAdapter da = new SqlDataAdapter("select max(ID) as ID from VacancyTemp", con);
                DataTable dt = new DataTable();
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    ViewState["Masterid"] = dt.Rows[0]["ID"].ToString();
                    string te = "candidateapplicationregistration.aspx?Id=" + ViewState["Masterid"].ToString();
                    ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);
                }
            }
            else
            {
                Label4.Text = "Please Select Atleast One Job to Apply.";
            }
        }
        else
        {
            ModalPopupExtender12.Show();
            panelpopo.Visible = true;
        }
    }

    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        con = new SqlConnection(PageConn.connnn);

        GridView1.PageIndex = e.NewPageIndex;

        if (Request.QueryString["Id"] != null)
        {
            int id = Convert.ToInt32(Request.QueryString["Id"]);

            String str1 = "select distinct VacancyMasterTbl.*,SalaryPerPeriodMaster.Name as sss,LEFT(VacancyDetailTbl.JobFunction,100) as JobFunction,LEFT(VacancyDetailTbl.QualificationRequirements,100) as QualificationRequirements,CityMasterTbl.CityName,StateMasterTbl.StateName,CountryMaster.CountryName,VacancyDetailTbl.TermsandConditions,CurrencyMaster.CurrencyName as ccc,VacancyFTPT.Name as vvv,case when (VacancyMasterTbl.status='1') then 'Active' else 'Inactive' End as Statuslabel,WareHouseMaster.Name as wname,DepartmentmasterMNC.Departmentname + ':' + DesignationMaster.DesignationName as dname,VacancyTypeMaster.Name as vname,VacancyPositionTitleMaster.VacancyPositionTitle as vtitle from VacancyMasterTbl inner join WareHouseMaster on WareHouseMaster.WareHouseId=VacancyMasterTbl.businessid  inner join DesignationMaster on DesignationMaster.DesignationMasterId=VacancyMasterTbl.DesignationID   inner join VacancyDetailTbl on VacancyDetailTbl.ID=VacancyMasterTbl.ID   inner join CityMasterTbl on CityMasterTbl.CityId=VacancyMasterTbl.cityid  inner join StateMasterTbl on StateMasterTbl.StateId=VacancyMasterTbl.stateid   inner join CountryMaster on CountryMaster.CountryId=VacancyMasterTbl.countryid inner join DepartmentmasterMNC on DesignationMaster.DeptID=DepartmentmasterMNC.id inner join VacancyTypeMaster on VacancyTypeMaster.ID=VacancyMasterTbl.vacancypositiontypeid inner join VacancyPositionTitleMaster  on VacancyPositionTitleMaster.ID=VacancyMasterTbl.vacancypositiontitleid inner join VacancyFTPT on VacancyMasterTbl.vacancyftptid=VacancyFTPT.Id inner join CurrencyMaster on CurrencyMaster.CurrencyId=VacancyMasterTbl.currencyid inner join SalaryPerPeriodMaster on SalaryPerPeriodMaster.ID=VacancyMasterTbl.salaryperperiodid  where VacancyMasterTbl.BusinessID='" + id + "' and VacancyMasterTbl.status='1' and VacancyMasterTbl.enddate>='" + System.DateTime.Now.ToShortDateString() + "'";
            SqlDataAdapter da = new SqlDataAdapter(str1, con);
            DataTable dt = new DataTable();
            da.Fill(dt);

            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
        else
        {
            fillgrid();
        }
    }

    protected void Button6_Click(object sender, EventArgs e)
    {
        int ii = 0;

        foreach (GridViewRow gr in GridView1.Rows)
        {
            CheckBox chkMsg11 = (CheckBox)gr.FindControl("chkMsg11");

            if (chkMsg11.Checked == true)
            {
                ii = ii + 1;

                ViewState["chkMsg11"] = "1";
                Label4.Text = "";
            }
        }
        if (ViewState["chkMsg11"] == "1")
        {
            if (ii > 5)
            {
                Label4.Text = "You can not select more than 5 Jobs for Sending mail.";
            }
            else
            {
                ModalPopupExtenderAddnew.Show();
            }
        }
        else
        {
            Label4.Text = "Please select atleast one job to send mail.";
        }
    }

    protected void Button9_Click(object sender, EventArgs e)
    {
        con = new SqlConnection(PageConn.connnn);

        string str1 = "";
        string str2 = "";
        string str3 = "";
        string str4 = "";
        string str5 = "";

        string str11 = "";
        string str22 = "";
        string str33 = "";
        string str44 = "";
        string str55 = "";

        string businessid = "";
        string cityid1 = "";

        foreach (GridViewRow gg in GridView1.Rows)
        {
            Label LabelLogin = (Label)gg.FindControl("LabelLogin");

            LinkButton LinkButton1 = (LinkButton)gg.FindControl("LinkButton1");

            CheckBox chkMsg11 = (CheckBox)gg.FindControl("chkMsg11");

            Label Label3 = (Label)gg.FindControl("Label3");

            Label Label50 = (Label)gg.FindControl("Label50");

            if (chkMsg11.Checked == true)
            {
                if (businessid == "")
                {
                    businessid = Label3.Text;
                }
                if (cityid1 == "")
                {
                    cityid1 = Label50.Text;
                }
                if (str1 == "" && str11 == "")
                {
                    str1 = LabelLogin.Text;
                    str11 = LinkButton1.Text;
                }
                if (str2 == "" && str22 == "" && str1 != LabelLogin.Text && str11 != LinkButton1.Text)
                {
                    str2 = LabelLogin.Text;
                    str22 = LinkButton1.Text;
                }
                if (str3 == "" && str33 == "" && str2 != "" && str22 != "")
                {
                    if (str2 != LabelLogin.Text && str22 != LinkButton1.Text)
                    {
                        str3 = LabelLogin.Text;
                        str33 = LinkButton1.Text;
                    }
                }
                if (str4 == "" && str44 == "" && str3 != "" && str33 != "")
                {
                    if (str3 != LabelLogin.Text && str33 != LinkButton1.Text)
                    {
                        str4 = LabelLogin.Text;
                        str44 = LinkButton1.Text;
                    }
                }
                if (str5 == "" && str55 == "" && str4 != "" && str44 != "")
                {
                    if (str4 != LabelLogin.Text && str44 != LinkButton1.Text)
                    {
                        str5 = LabelLogin.Text;
                        str55 = LinkButton1.Text;
                    }
                }
            }
        }

        string ADDRESSEX = "";
        string logg = "";
        string business = "";

        if (Request.QueryString["Id"] != null)
        {
            ADDRESSEX = "SELECT distinct CompanyMaster.CompanyLogo, CompanyMaster.CompanyName,CompanyWebsitMaster.Sitename,CompanyWebsitMaster.MasterEmailId,CompanyWebsitMaster.EmailMasterLoginPassword,CompanyWebsitMaster.OutGoingMailServer, CompanyWebsitMaster.EmailSentDisplayName,CompanyWebsitMaster.SiteUrl,CompanyWebsiteAddressMaster.Address1,CompanyWebsiteAddressMaster.Address2,CompanyWebsiteAddressMaster.Phone1, CompanyWebsiteAddressMaster.Phone2, CompanyWebsiteAddressMaster.TollFree1, CompanyWebsiteAddressMaster.Fax,CompanyWebsiteAddressMaster.Email,CompanyMaster.CompanyId,CompanyWebsitMaster.WHid FROM  CompanyMaster LEFT OUTER JOIN AddressTypeMaster RIGHT OUTER JOIN CompanyWebsiteAddressMaster ON AddressTypeMaster.AddressTypeMasterId = CompanyWebsiteAddressMaster.AddressTypeMasterId RIGHT OUTER JOIN CompanyWebsitMaster ON CompanyWebsiteAddressMaster.CompanyWebsiteMasterId = CompanyWebsitMaster.CompanyWebsiteMasterId ON CompanyMaster.CompanyId = CompanyWebsitMaster.CompanyId where CompanyMaster.Compid='" + Session["comid"] + "' and WHId='" + Convert.ToInt32(Request.QueryString["Id"]) + "'";

            logg = "select LogoUrl from CompanyWebsitMaster where whid='" + Convert.ToInt32(Request.QueryString["Id"]) + "'";

            business = "select Warehousemaster.Name,CompanyWebsitMaster.OutGoingMailServer,CompanyWebsitMaster.MasterEmailId,CompanyWebsitMaster.EmailMasterLoginPassword,CompanyWebsiteAddressMaster.City,CompanyWebsiteAddressMaster.TollFree2,CompanyWebsiteAddressMaster.Zip,CompanyWebsiteAddressMaster.Address1,CompanyWebsiteAddressMaster.Address2,CompanyWebsiteAddressMaster.Phone1,CompanyWebsiteAddressMaster.Email,CountryMaster.CountryName,StateMasterTbl.StateName,CityMasterTbl.CityName,CompanyWebsitMaster.Sitename From Warehousemaster inner join CompanyWebsiteAddressMaster on CompanyWebsiteAddressMaster.CompanyWebsiteMasterId=Warehousemaster.WarehouseID inner join CompanyWebsitMaster on CompanyWebsitMaster.WHId=CompanyWebsiteAddressMaster.CompanyWebsiteMasterId inner join CountryMaster on CountryMaster.CountryId=CompanyWebsiteAddressMaster.Country inner join StateMasterTbl on StateMasterTbl.StateId=CompanyWebsiteAddressMaster.State inner join CityMasterTbl on CityMasterTbl.CityId=CompanyWebsiteAddressMaster.City where WarehouseID='" + Convert.ToInt32(Request.QueryString["Id"]) + "' and CompanyWebsiteAddressMaster.AddressTypeMasterId='1'";
        }
        else
        {
            ADDRESSEX = "SELECT distinct CompanyMaster.CompanyLogo, CompanyMaster.CompanyName,CompanyWebsitMaster.Sitename,CompanyWebsitMaster.MasterEmailId,CompanyWebsitMaster.EmailMasterLoginPassword,CompanyWebsitMaster.OutGoingMailServer, CompanyWebsitMaster.EmailSentDisplayName,CompanyWebsitMaster.SiteUrl,CompanyWebsiteAddressMaster.Address1,CompanyWebsiteAddressMaster.Address2,CompanyWebsiteAddressMaster.Phone1, CompanyWebsiteAddressMaster.Phone2, CompanyWebsiteAddressMaster.TollFree1, CompanyWebsiteAddressMaster.Fax,CompanyWebsiteAddressMaster.Email,CompanyMaster.CompanyId,CompanyWebsitMaster.WHid FROM  CompanyMaster LEFT OUTER JOIN AddressTypeMaster RIGHT OUTER JOIN CompanyWebsiteAddressMaster ON AddressTypeMaster.AddressTypeMasterId = CompanyWebsiteAddressMaster.AddressTypeMasterId RIGHT OUTER JOIN CompanyWebsitMaster ON CompanyWebsiteAddressMaster.CompanyWebsiteMasterId = CompanyWebsitMaster.CompanyWebsiteMasterId ON CompanyMaster.CompanyId = CompanyWebsitMaster.CompanyId where CompanyMaster.Compid='" + Session["comid"] + "' and WHId='" + businessid + "'";

            logg = "select LogoUrl from CompanyWebsitMaster where whid='" + businessid + "'";

            business = "select Warehousemaster.Name,CompanyWebsitMaster.OutGoingMailServer,CompanyWebsitMaster.MasterEmailId,CompanyWebsitMaster.EmailMasterLoginPassword,CompanyWebsiteAddressMaster.City,CompanyWebsiteAddressMaster.TollFree2,CompanyWebsiteAddressMaster.Zip,CompanyWebsiteAddressMaster.Address1,CompanyWebsiteAddressMaster.Address2,CompanyWebsiteAddressMaster.Phone1,CompanyWebsiteAddressMaster.Email,CountryMaster.CountryName,StateMasterTbl.StateName,CityMasterTbl.CityName,CompanyWebsitMaster.Sitename From Warehousemaster inner join CompanyWebsiteAddressMaster on CompanyWebsiteAddressMaster.CompanyWebsiteMasterId=Warehousemaster.WarehouseID inner join CompanyWebsitMaster on CompanyWebsitMaster.WHId=CompanyWebsiteAddressMaster.CompanyWebsiteMasterId inner join CountryMaster on CountryMaster.CountryId=CompanyWebsiteAddressMaster.Country inner join StateMasterTbl on StateMasterTbl.StateId=CompanyWebsiteAddressMaster.State inner join CityMasterTbl on CityMasterTbl.CityId=CompanyWebsiteAddressMaster.City where WarehouseID='" + businessid + "' and CompanyWebsiteAddressMaster.AddressTypeMasterId='1'";
        }

        SqlCommand cmd = new SqlCommand(ADDRESSEX, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable ds = new DataTable();
        adp.Fill(ds);

        SqlDataAdapter dalog = new SqlDataAdapter(logg, con);
        DataTable dtlog = new DataTable();
        dalog.Fill(dtlog);

        SqlDataAdapter dabus = new SqlDataAdapter(business, con);
        DataTable dtbus = new DataTable();
        dabus.Fill(dtbus);

        StringBuilder HeadingTable = new StringBuilder();

        HeadingTable.Append("<table width=\"100%\"> ");

        HeadingTable.Append("<tr><td width=\"50%\" style=\" align=\"left\" > <img src=\"http://" + Request.Url.Host.ToString() + "/Shoppingcart/images/" + dtlog.Rows[0]["LogoUrl"].ToString() + "\" \"border=\"0\" Width=\"90\" Height=\"80\" / > </td></tr>  ");

        HeadingTable.Append("</table> ");

        string welcometext = getWelcometext();

        string loginurl = Request.Url.Host.ToString() + "/Shoppingcart/Admin/ResetPasswordUser.aspx";

        string AccountInfo = "Your Friend " + TextBox2.Text + " has sent you the following Job postings for your refernce.<br><br>";

        if (str1 != "" && str11 != "")
        {
            AccountInfo += "<a href= " + Request.Url.Host.ToString() + "/VacancyProfile.aspx?ID=" + str1 + " target=_blank > " + str11 + " </a><br>";
        }
        if (str2 != "" && str22 != "")
        {
            AccountInfo += "<a href= " + Request.Url.Host.ToString() + "/VacancyProfile.aspx?ID=" + str2 + " target=_blank > " + str22 + " </a><br>";
        }
        if (str3 != "" && str33 != "")
        {
            AccountInfo += "<a href= " + Request.Url.Host.ToString() + "/VacancyProfile.aspx?ID=" + str3 + " target=_blank > " + str33 + " </a><br>";
        }
        if (str4 != "" && str44 != "")
        {
            AccountInfo += "<a href= " + Request.Url.Host.ToString() + "/VacancyProfile.aspx?ID=" + str4 + " target=_blank > " + str44 + " </a><br>";
        }
        if (str5 != "" && str55 != "")
        {
            AccountInfo += "<a href= " + Request.Url.Host.ToString() + "/VacancyProfile.aspx?ID=" + str5 + " target=_blank > " + str55 + " </a><br>";
        }

        if (Request.QueryString["Id"] != null)
        {
            AccountInfo += "<br><br>You can view all the vacancy details by clicking <a href= " + Request.Url.Host.ToString() + "/Vacancy.aspx?ID=" + Convert.ToInt32(Request.QueryString["Id"]) + " target=_blank > here </a> OR Paste the following link to your browser.<br><br> " + Request.Url.Host.ToString() + "/Vacancy.aspx?ID=" + Convert.ToInt32(Request.QueryString["Id"]) + "<br><br>You can also view many other vacancies open in your city by clicking <a href= " + Request.Url.Host.ToString() + "/Vacancy.aspx?cityid=" + cityid1 + " target=_blank > here </a> OR Paste the following link to your browser.<br><br> " + Request.Url.Host.ToString() + "/Vacancy.aspx?cityid=" + cityid1 + "";
        }
        else
        {
            AccountInfo += "<br><br>You can view all the vacancy details by clicking <a href= " + Request.Url.Host.ToString() + "/Vacancy.aspx?ID=" + businessid + " target=_blank > here </a> OR Paste the following link to your browser.<br><br> " + Request.Url.Host.ToString() + "/Vacancy.aspx?ID=" + businessid + "<br><br>You can also view many other vacancies open in your city by clicking <a href= " + Request.Url.Host.ToString() + "/Vacancy.aspx?cityid=" + cityid1 + " target=_blank > here </a> OR Paste the following link to your browser.<br><br> " + Request.Url.Host.ToString() + "/Vacancy.aspx?cityid=" + cityid1 + "";
        }

        string Accountdetail = "<br>";

        DataTable dtbussadmin = new DataTable();

        if (Request.QueryString["Id"] != null)
        {
            dtbussadmin = select("select employeemaster.employeename from employeemaster where whid='" + Convert.ToInt32(Request.QueryString["Id"]) + "'");
        }
        else
        {
            dtbussadmin = select("select employeemaster.employeename from employeemaster where whid='" + businessid + "'");
        }

        string body = "<br>" + HeadingTable + "<br><br> Dear <strong><span style=\"color: #996600\"> " + TextBox6.Text + " </span></strong>,<br><br>" + welcometext.ToString() + " <br>" + AccountInfo.ToString() + "<br> " + Accountdetail.ToString() + " Thanking you<br><br>Sincerely,</span><br><br>" + Convert.ToString(dtbussadmin.Rows[0]["employeename"]) + "<br><b>" + dtbus.Rows[0]["Name"].ToString() + "</b><br>" + dtbus.Rows[0]["Address1"].ToString() + "<br>" + dtbus.Rows[0]["CityName"].ToString() + " , " + dtbus.Rows[0]["StateName"].ToString() + " , " + dtbus.Rows[0]["CountryName"].ToString() + " , " + dtbus.Rows[0]["Zip"].ToString() + "<br>Phone: " + dtbus.Rows[0]["Phone1"].ToString() + "<br>Email:" + dtbus.Rows[0]["Email"].ToString() + "<br>" + dtbus.Rows[0]["TollFree2"].ToString() + "";

        try
        {
            MailAddress to = new MailAddress(TextBox7.Text);
            MailAddress from = new MailAddress("" + dtbus.Rows[0]["MasterEmailId"] + "", "" + TextBox2.Text + "");
            MailMessage objEmail = new MailMessage(from, to);

            objEmail.Subject = " Referral from " + TextBox2.Text + " regarding Job vacancy at " + dtbus.Rows[0]["Name"].ToString() + " ";

            objEmail.Body = body.ToString();
            objEmail.IsBodyHtml = true;
            objEmail.Priority = MailPriority.Normal;
            SmtpClient client = new SmtpClient();

            client.Credentials = new NetworkCredential("" + dtbus.Rows[0]["MasterEmailId"] + "", "" + dtbus.Rows[0]["EmailMasterLoginPassword"] + "");

            client.Host = dtbus.Rows[0]["OutGoingMailServer"].ToString();
            client.Send(objEmail);

            Label43.Text = "Email sent successfully.";

            TextBox2.Text = "";
            TextBox3.Text = "";
            TextBox6.Text = "";
            TextBox7.Text = "";

            ModalPopupExtenderAddnew.Show();
        }
        catch (Exception exx)
        {
            Label43.Text = "Failure sending mail.";
            ModalPopupExtenderAddnew.Show();
        }
    }

    public string getWelcometext()
    {
        con = new SqlConnection(PageConn.connnn);

        string str = "SELECT EmailContentMaster.EmailContent, EmailContentMaster.EntryDate, CompanyWebsitMaster.SiteUrl, EmailTypeMaster.EmailTypeId " +
                    " FROM CompanyWebsitMaster INNER JOIN " +
                      " EmailContentMaster ON CompanyWebsitMaster.CompanyWebsiteMasterId = EmailContentMaster.CompanyWebsiteMasterId INNER JOIN " +
                      " EmailTypeMaster ON EmailContentMaster.EmailTypeId = EmailTypeMaster.EmailTypeId " +
                    " WHERE     (EmailTypeMaster.EmailTypeId = 1)  and (EmailTypeMaster.Compid='" + Session["comid"].ToString() + "')" +
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

    protected void Button10_Click(object sender, EventArgs e)
    {
        TextBox2.Text = "";
        TextBox3.Text = "";

        TextBox6.Text = "";
        TextBox7.Text = "";

        Label43.Text = "";

        ModalPopupExtenderAddnew.Hide();
    }

    protected void ddlcountry1_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillstate1();
    }
    protected void ddlstate1_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillcity1();
    }
    protected void chkMsg11_CheckedChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow gr in GridView1.Rows)
        {
            CheckBox chkMsg11 = (CheckBox)gr.FindControl("chkMsg11");

            if (chkMsg11.Checked == true)
            {
                ViewState["chkMsg11"] = "1";
                Label4.Text = "";
            }
        }
        if (ViewState["chkMsg11"] == "1")
        {
            Button Button6 = (Button)GridView1.HeaderRow.FindControl("Button6");

            Button6.Enabled = true;
        }
    }

    protected void chkMsg_CheckedChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow gr in GridView1.Rows)
        {
            CheckBox chkMsg = (CheckBox)gr.FindControl("chkMsg");

            if (chkMsg.Checked == true)
            {
                ViewState["chkMsg"] = "1";
                Label4.Text = "";
            }
        }
        if (ViewState["chkMsg"] == "1")
        {
            Button Button1 = (Button)GridView1.HeaderRow.FindControl("Button1");

            Button1.Enabled = true;
        }
    }

    protected void fillvacancy11()
    {
        DropDownList1.Items.Clear();

        SqlDataAdapter dav = new SqlDataAdapter("select ID,Name from VacancyTypeMaster", con);
        DataTable dtv = new DataTable();
        dav.Fill(dtv);

        if (dtv.Rows.Count > 0)
        {
            DropDownList1.DataSource = dtv;
            DropDownList1.DataTextField = "Name";
            DropDownList1.DataValueField = "ID";
            DropDownList1.DataBind();
        }
        DropDownList1.Items.Insert(0, "All");
        DropDownList1.Items[0].Value = "0";

        //DropDownList1.SelectedIndex = DropDownList1.Items.IndexOf(DropDownList1.Items.FindByText("Permanent"));
    }

    protected void fillduration11()
    {
        DropDownList2.Items.Clear();

        string str1 = "select Id,Name from VacancyFTPT";

        DataTable ds13 = new DataTable();
        SqlDataAdapter da3 = new SqlDataAdapter(str1, con);
        da3.Fill(ds13);
        if (ds13.Rows.Count > 0)
        {
            DropDownList2.DataSource = ds13;
            DropDownList2.DataTextField = "Name";
            DropDownList2.DataValueField = "Id";
            DropDownList2.DataBind();
        }
        DropDownList2.Items.Insert(0, "All");
        DropDownList2.Items[0].Value = "0";
        //DropDownList2.SelectedIndex = DropDownList2.Items.IndexOf(DropDownList2.Items.FindByText("Full Time"));
    }
}
