﻿using System;
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

                string inf = "select vacancymastertbl.* from vacancymastertbl where  vacancymastertbl.ID=" + id;

                SqlDataAdapter dainf = new SqlDataAdapter(inf, con);
                DataTable dtinf = new DataTable();
                dainf.Fill(dtinf);

                ViewState["whid"] = Convert.ToString(dtinf.Rows[0]["BusinessID"]);
                ViewState["vacancypositiontypeid"] = Convert.ToString(dtinf.Rows[0]["vacancypositiontypeid"]);
                ViewState["vacancypositiontitleid"] = Convert.ToString(dtinf.Rows[0]["vacancypositiontitleid"]);
             
                string strii = "select distinct VacancyMasterTbl.*,case when (VacancyMasterTbl.status='1') then 'Active' else 'Inactive' End as Statuslabel,WareHouseMaster.Name as wname,DepartmentmasterMNC.Departmentname + ' - ' + DesignationMaster.DesignationName as dname,VacancyTypeMaster.Name as vname,VacancyPositionTitleMaster.VacancyPositionTitle as vtitle from VacancyMasterTbl inner join WareHouseMaster on WareHouseMaster.WareHouseId=VacancyMasterTbl.businessid  inner join DesignationMaster on DesignationMaster.DesignationMasterId=VacancyMasterTbl.DesignationID inner join DepartmentmasterMNC on DesignationMaster.DeptID=DepartmentmasterMNC.id inner join VacancyTypeMaster on VacancyTypeMaster.ID=VacancyMasterTbl.vacancypositiontypeid inner join VacancyPositionTitleMaster  on VacancyPositionTitleMaster.ID=VacancyMasterTbl.vacancypositiontitleid where VacancyMasterTbl.ID='" + id + "'";
                SqlDataAdapter da = new SqlDataAdapter(strii, con);
                DataTable dt = new DataTable();
                da.Fill(dt);


                if (dt.Rows.Count > 0)
                {
                    lblbusiness.Text = Convert.ToString(dt.Rows[0]["wname"]);

                    lblvactype.Text = Convert.ToString(dt.Rows[0]["vname"]);

                    lblvactitle.Text = Convert.ToString(dt.Rows[0]["vtitle"]);

                    lblemail.Text = Convert.ToString(dt.Rows[0]["contactEmail"]);

                    lblpername.Text = Convert.ToString(dt.Rows[0]["contactname"]);

                    lblphonno.Text = Convert.ToString(dt.Rows[0]["contactphoneno"]);

                    lbladdress.Text = Convert.ToString(dt.Rows[0]["contactAddress"]);

                    lblsal2.Text = Convert.ToString(dt.Rows[0]["salaryamount"]);

                    if (Convert.ToBoolean(dt.Rows[0]["applybyemail"]) == true)
                    {
                        pansdsdssdd.Visible = true;
                    }
                    else
                    {
                        pansdsdssdd.Visible = false;
                    }
                    if (Convert.ToBoolean(dt.Rows[0]["applybyphone"]) == true)
                    {
                        Panel1.Visible = true;
                    }
                    else
                    {
                        Panel1.Visible = false;
                    }
                    if (Convert.ToBoolean(dt.Rows[0]["applybyvisit"]) == true)
                    {
                        Panel2.Visible = true;
                    }
                    else
                    {
                        Panel2.Visible = false;
                    }
                    if (Convert.ToBoolean(dt.Rows[0]["applyonline"]) == true)
                    {
                        Panel3.Visible = true;
                    }
                    else
                    {
                        Panel3.Visible = false;
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



    protected void LinkButton1_Click(object sender, EventArgs e)
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

        //ViewState["whid"] = Convert.ToString(dtinf.Rows[0]["BusinessID"]);
        //ViewState["vacancypositiontypeid"] = Convert.ToString(dtinf.Rows[0]["vacancypositiontypeid"]);
        //ViewState["vacancypositiontitleid"] = Convert.ToString(dtinf.Rows[0]["vacancypositiontitleid"]);

        string insert = "insert into VacancyTemp values('" + a + "','" + ViewState["vacancypositiontypeid"] + "','" + ViewState["vacancypositiontitleid"] + "','" + ViewState["whid"] + "')";

        SqlCommand cmd = new SqlCommand(insert, con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmd.ExecuteNonQuery();
        con.Close();        

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
}
