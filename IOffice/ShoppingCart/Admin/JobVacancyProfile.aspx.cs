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

    protected void Page_Load(object sender, EventArgs e)
    {

        PageConn pgcon = new PageConn();
        con = pgcon.dynconn;

        if (!Page.IsPostBack)
        {
            if (Request.QueryString["ID"] != null && Request.QueryString["CID"] != null)
            {
                int id = Convert.ToInt32(ClsEncDesc.Decrypted(Request.QueryString["ID"].ToString()));
                int cid = Convert.ToInt32(ClsEncDesc.Decrypted(Request.QueryString["CID"].ToString()));


                string str = "select companyjobposition.companyid,companyjobposition.jobtypeid,companyjobposition.jobtitleid,companyjobposition.skills,companyjobposition.notes,companyjobposition.datetime,companyjobposition.sms,companyjobposition.email,companyjobposition.phone,companyjobposition.online," +
                    " VacancyPositionTitleMaster.VacancyPositionTitle from companyjobposition" +
                    " inner join VacancyPositionTitleMaster ON   VacancyPositionTitleMaster.ID=companyjobposition.jobtitleid where  companyjobposition.companyid='" + cid.ToString() + "'";

                SqlCommand cmd = new SqlCommand(str, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);

                // lblduration.Text=dt.Rows[0]["
                lblvactitle.Text = dt.Rows[0]["VacancyPositionTitle"].ToString();

                //string st = "select VacancyMasterTbl.*,[CityMasterTbl].[CityName],StateMasterTbl.[StateName],CountryMaster.[CountryName],"+
                //" VacancyTypeMaster.ID,VacancyTypeMaster.Name from VacancyMasterTbl inner join VacancyTypeMaster  on VacancyTypeMaster.ID=VacancyMasterTbl.vacancypositiontypeid"+
                //" inner join CityMasterTbl on CityMasterTbl.CityId=VacancyMasterTbl.cityid"+
                //" inner join StateMasterTbl on StateMasterTbl.StateId=VacancyMasterTbl.stateid"+
                //" inner join CountryMaster on CountryMaster.CountryId=VacancyMasterTbl.countryid"+
                //" where VacancyMasterTbl.comid='" + cid.ToString() + "'";
                string st = "select companyjoblocation.*,[CityMasterTbl].[CityName],StateMasterTbl.[StateName],CountryMaster.[CountryName]" +
                    " from companyjoblocation" +
                 " inner join CityMasterTbl on CityMasterTbl.CityId=companyjoblocation.city" +
                " inner join StateMasterTbl on StateMasterTbl.StateId=companyjoblocation.state" +
                " inner join CountryMaster on CountryMaster.CountryId=companyjoblocation.country" +
                " where companyjoblocation.companyid='" + cid.ToString() + "'";

                SqlDataAdapter dat = new SqlDataAdapter(st, con);
                DataTable dta = new DataTable();
                dat.Fill(dta);
                lblcountry.Text = dta.Rows[0]["CountryName"].ToString();
                lblstate.Text = dta.Rows[0]["StateName"].ToString();
                lblcity.Text = dta.Rows[0]["CityName"].ToString();

                string st2 = "select companyjobposition.*,VacancyTypeMaster.Name,VacancyPositionTitleMaster.VacancyPositionTitle from companyjobposition" +
                    " inner join VacancyTypeMaster on VacancyTypeMaster.ID=companyjobposition.jobtypeid" +
                    " inner join VacancyPositionTitleMaster on VacancyPositionTitleMaster.ID=companyjobposition.jobtitleid" +
                    " where companyjobposition.companyid='" + cid.ToString() + "'";
                SqlDataAdapter da2 = new SqlDataAdapter(st2, con);
                DataTable dt2 = new DataTable();
                da2.Fill(dt2);

                lblvactype.Text = dt2.Rows[0]["Name"].ToString();
                lblscriptreq.Text = dt2.Rows[0]["skills"].ToString();
                lblothertermscondition.Text = dt2.Rows[0]["notes"].ToString();
                if (Convert.ToBoolean(dt2.Rows[0]["email"]) == true)
                {
                   // CheckBox3.Visible = true;
                    lblemail.Visible = true;
                }
                else
                {
                    //CheckBox3.Visible = false;
                    lblemail.Visible = false;
                }
                if (Convert.ToBoolean(dt2.Rows[0]["phone"]) == true)
                {
                   // CheckBox1.Visible = true;
                    lblphone.Visible = true;
                }
                else
                {
                   // CheckBox1.Visible = false;
                    lblphone.Visible = false;
                }
                if (Convert.ToBoolean(dt2.Rows[0]["sms"]) == true)
                {
                    //CheckBox4.Visible = true;
                    lblsms.Visible = true;
                }
                else
                {
                    //CheckBox4.Visible = false;
                    lblsms.Visible = false;
                }
                if (Convert.ToBoolean(dt2.Rows[0]["online"]) == true)
                {
                  //  CheckBox2.Visible = true;
                    lblapplyonline.Visible = true;
                }
                else
                {
                    //CheckBox2.Visible = false;
                    lblapplyonline.Visible = false;
                }

                string st3 = "select companyremuneration2.*,CurrencyMaster.CurrencyName from companyremuneration2 inner join  CurrencyMaster on CurrencyMaster.CurrencyId=companyremuneration2.currency where companyid='" + cid.ToString() + "'";
                SqlDataAdapter da3 = new SqlDataAdapter(st3, con);
                DataTable dt3 = new DataTable();
                da3.Fill(dt3);
                lblsal1.Text = dt3.Rows[0]["CurrencyName"].ToString();
                lblsal2.Text = dt3.Rows[0]["amount"].ToString();               
                if (dt3.Rows[0]["period"].ToString() == "0")
                {
                    lblsal3.Text = "Hour";
                }
                if (dt3.Rows[0]["period"].ToString() == "1")
                {
                    lblsal3.Text = "Day";
                }
                if (dt3.Rows[0]["period"].ToString() == "2")
                {
                    lblsal3.Text = "Week";
                }
                if (dt3.Rows[0]["period"].ToString() == "3")
                {
                    lblsal3.Text = "Month";
                }
               

                string st4 = "select companyjobtimings.*,TimeZoneMaster.Name+':'+TimeZoneMaster.ShortName+':'+TimeZoneMaster.gmt AS BatchTimeZone from companyjobtimings" +
                    " inner join TimeZoneMaster on TimeZoneMaster.ID=companyjobtimings.timezone   where companyjobtimings.companyid='" + cid.ToString() + "' and TimeZoneMaster.status='1'";
                SqlDataAdapter da4 = new SqlDataAdapter(st4, con);
                DataTable dt4 = new DataTable();
                da4.Fill(dt4);
                lblfrmtim.Text = dt4.Rows[0]["fromtime"].ToString();               
                if (dt4.Rows[0]["fromAMPM"].ToString() == "False")
                {
                    lblfrmampm.Text = "AM";
                }
                if (dt4.Rows[0]["fromAMPM"].ToString() == "True")
                {
                    lblfrmampm.Text = "PM";
                }
                lbltotim.Text = dt4.Rows[0]["totime"].ToString();
              
                if (dt4.Rows[0]["toAMPM"].ToString() == "False")
                {
                    lbltoampm.Text = "AM";
                }
                if (dt4.Rows[0]["toAMPM"].ToString() == "True")
                {
                    lbltoampm.Text = "PM";
                }

                lbltimzone.Text = dt4.Rows[0]["BatchTimeZone"].ToString();

                lbllastjob.Text = dt4.Rows[0]["days"].ToString();

                //string st1 = "select VacancyDetailTbl.* from VacancyDetailTbl inner join VacancyMasterTbl on VacancyMasterTbl.ID=VacancyDetailTbl.vacancymasterid where VacancyMasterTbl.comid='" + cid.ToString() + "'";
                //SqlDataAdapter da1 = new SqlDataAdapter(st1, con);
                //DataTable dt1 = new DataTable();
                //da1.Fill(dt1);
                //lbljobfun.Text = dt1.Rows[0]["JobFunction"].ToString();
                //lblqualifi.Text = dt1.Rows[0]["QualificationRequirements"].ToString();
                //lblTandC.Text = dt1.Rows[0]["TermsandConditions"].ToString();
            }
        }
    }
}
