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
        //if (Session["Comid"] == null)
        //{
        //    Response.Redirect("~/Shoppingcart/Admin/ShoppingCartLogin.aspx");
        //}
        pagetitleclass pg = new pagetitleclass();

        string strData = Request.Url.ToString();

        char[] separator = new char[] { '/' };
        //  compid = Session["Comid"].ToString();
        string[] strSplitArr = strData.Split(separator);
        int i = Convert.ToInt32(strSplitArr.Length);
        string page = strSplitArr[i - 1].ToString();


        //    Page.Title = pg.getPageTitle(page);

        PageConn pgcon = new PageConn();
        con = pgcon.dynconn;

        if (!Page.IsPostBack)
        {
            Pagecontrol.dypcontrol(Page, page);


            if (Request.QueryString["Id"] != null)
            {
                int id = Convert.ToInt32(Request.QueryString["Id"]);

                string strii = "select distinct VacancyMasterTbl.*,case when (VacancyMasterTbl.status='1') then 'Active' else 'Inactive' End as Statuslabel,WareHouseMaster.Name as wname,DepartmentmasterMNC.Departmentname + ' - ' + DesignationMaster.DesignationName as dname,VacancyTypeMaster.Name as vname,VacancyPositionTitleMaster.VacancyPositionTitle as vtitle from VacancyMasterTbl inner join WareHouseMaster on WareHouseMaster.WareHouseId=VacancyMasterTbl.businessid  inner join DesignationMaster on DesignationMaster.DesignationMasterId=VacancyMasterTbl.DesignationID inner join DepartmentmasterMNC on DesignationMaster.DeptID=DepartmentmasterMNC.id inner join VacancyTypeMaster on VacancyTypeMaster.ID=VacancyMasterTbl.vacancypositiontypeid inner join VacancyPositionTitleMaster  on VacancyPositionTitleMaster.ID=VacancyMasterTbl.vacancypositiontitleid where VacancyMasterTbl.ID='" + id + "'";
                SqlDataAdapter da = new SqlDataAdapter(strii, con);
                DataTable dt = new DataTable();
                da.Fill(dt);


                if (dt.Rows.Count > 0)
                {
                    lblbusiness.Text = Convert.ToString(dt.Rows[0]["wname"]);

                    //lbldepdes.Text = Convert.ToString(dt.Rows[0]["dname"]);

                    //lblvactype.Text = Convert.ToString(dt.Rows[0]["vname"]);

                    lblvactitle.Text = Convert.ToString(dt.Rows[0]["vtitle"]);

                    //lblnovac.Text = Convert.ToString(dt.Rows[0]["noofvacancy"]);

                    //lblstart.Text = Convert.ToDateTime(dt.Rows[0]["startdate"].ToString()).ToShortDateString();

                    //lblend.Text = Convert.ToDateTime(dt.Rows[0]["enddate"].ToString()).ToShortDateString();

                    lblemail.Text = Convert.ToString(dt.Rows[0]["contactEmail"]);

                    lblpername.Text = Convert.ToString(dt.Rows[0]["contactname"]);

                    lblphonno.Text = Convert.ToString(dt.Rows[0]["contactphoneno"]);

                    lbladdress.Text = Convert.ToString(dt.Rows[0]["contactAddress"]);

                    //lblschedule.Text = Convert.ToString(dt.Rows[0]["worktimings"]);

                    lblsal2.Text = Convert.ToString(dt.Rows[0]["salaryamount"]);
                    

                  //  lblhour.Text = Convert.ToString(dt.Rows[0]["hours"]);

                    if (Convert.ToBoolean(dt.Rows[0]["applybyemail"]) == true)
                    {
                        lblbyemail.Visible = true;
                    }
                    else
                    {
                        lblbyemail.Visible = false;
                    }
                    if (Convert.ToBoolean(dt.Rows[0]["applybyphone"]) == true)
                    {
                        lblphonno.Visible = true;
                    }
                    else
                    {
                        lblphonno.Visible = false;
                    }
                    if (Convert.ToBoolean(dt.Rows[0]["applybyvisit"]) == true)
                    {
                        Label13.Visible = true;
                    }
                    else
                    {
                        Label13.Visible = false;
                    }
                    if (Convert.ToBoolean(dt.Rows[0]["applyonline"]) == true)
                    {
                        Label20.Visible = true;
                    }
                    else
                    {
                        Label20.Visible = false;
                    }

                }

                SqlDataAdapter dac = new SqlDataAdapter("select [CityMasterTbl].[CityName],StateMasterTbl.[StateName],CountryMaster.[CountryName] from [CityMasterTbl] inner join VacancyMasterTbl on VacancyMasterTbl.cityid=[CityMasterTbl].[CityId] inner join StateMasterTbl on StateMasterTbl.StateId=VacancyMasterTbl.stateid inner join CountryMaster on CountryMaster.CountryId=VacancyMasterTbl.countryid where VacancyMasterTbl.ID='" + id + "'", con);
                DataTable dtc = new DataTable();
                dac.Fill(dtc);

                if (dtc.Rows.Count > 0)
                {
                    lblcountry.Text = Convert.ToString(dtc.Rows[0]["CountryName"]);

                    lblstate.Text = Convert.ToString(dtc.Rows[0]["StateName"]);

                    lblcity.Text = Convert.ToString(dtc.Rows[0]["CityName"]);
                }

                SqlDataAdapter dac1 = new SqlDataAdapter("select VacancyDetailTbl.* from VacancyDetailTbl inner join VacancyMasterTbl on VacancyMasterTbl.ID=VacancyDetailTbl.vacancymasterid where VacancyMasterTbl.ID='" + id + "'", con);
                DataTable dtc1 = new DataTable();
                dac1.Fill(dtc1);

                if (dtc1.Rows.Count > 0)
                {
                    lbljobfun.Text = Convert.ToString(dtc1.Rows[0]["JobFunction"]);

                    lblqualifi.Text = Convert.ToString(dtc1.Rows[0]["QualificationRequirements"]);

                    lblTandC.Text = Convert.ToString(dtc1.Rows[0]["TermsandConditions"]);
                }

                SqlDataAdapter dac11 = new SqlDataAdapter("select Name from VacancyFTPT inner join VacancyMasterTbl on VacancyMasterTbl.vacancyftptid=VacancyFTPT.Id where VacancyMasterTbl.ID='" + id + "'", con);
                DataTable dtc11 = new DataTable();
                dac11.Fill(dtc11);

                if (dtc11.Rows.Count > 0)
                {
                    lblduration.Text = Convert.ToString(dtc11.Rows[0]["Name"]);
                }

                SqlDataAdapter dasl = new SqlDataAdapter("select CurrencyMaster.CurrencyName from CurrencyMaster inner join VacancyMasterTbl on VacancyMasterTbl.currencyid=CurrencyMaster.CurrencyId where VacancyMasterTbl.ID='" + id + "'", con);
                DataTable dtsl = new DataTable();
                dasl.Fill(dtsl);

                if (dtsl.Rows.Count > 0)
                {
                    lblsal1.Text = Convert.ToString(dtsl.Rows[0]["CurrencyName"]);
                }

                SqlDataAdapter dasl11 = new SqlDataAdapter("select SalaryPerPeriodMaster.Name from SalaryPerPeriodMaster inner join VacancyMasterTbl on VacancyMasterTbl.salaryperperiodid=SalaryPerPeriodMaster.ID where VacancyMasterTbl.ID='" + id + "'", con);
                DataTable dtsl11 = new DataTable();
                dasl11.Fill(dtsl11);

                if (dtsl11.Rows.Count > 0)
                {
                    lblsal3.Text = Convert.ToString(dtsl11.Rows[0]["Name"]);
                }

            }
        }
    }



}
