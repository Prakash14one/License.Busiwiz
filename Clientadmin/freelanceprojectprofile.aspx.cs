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

public partial class ShoppingCart_Admin_freelanceprojectprofile : System.Web.UI.Page
{
    SqlConnection con;
    string id;
    protected void Page_Load(object sender, EventArgs e)
    {
        PageConn pgcon = new PageConn();
        con = pgcon.dynconn;
        Session["Comid"] = "jobcenter";
        if (!Page.IsPostBack)
        {
            if (Request.QueryString["Id"] != null)
            {
                id = Convert.ToString(Request.QueryString["Id"]);
                id = ClsEncDesc.Decrypted(id.ToString());

                string inf = "select TblFreeLanceProjectMaster.* from TblFreeLanceProjectMaster where FreeLanceProject_Id ='" + id + "'";

                SqlDataAdapter dainf = new SqlDataAdapter(inf, con);
                DataTable dtinf = new DataTable();
                dainf.Fill(dtinf);

                //ViewState["whid"] = Convert.ToString(dtinf.Rows[0]["BusinessID"]);
                //ViewState["vacancypositiontypeid"] = Convert.ToString(dtinf.Rows[0]["vacancypositiontypeid"]);
                //ViewState["vacancypositiontitleid"] = Convert.ToString(dtinf.Rows[0]["vacancypositiontitleid"]);

                string strii =  " SELECT DISTINCT dbo.TblFreeLanceProjectMaster.FreeLanceProject_Id, dbo.TblFreeLanceProjectMaster.FreeLanceProject_DeptId, dbo.TblFreeLanceProjectMaster.FreeLanceProject_ProjectTitle, dbo.TblFreeLanceProjectMaster.FreeLanceProject_ProjectDescription, dbo.TblFreeLanceProjectMaster.FreeLanceProject_StartDate, dbo.TblFreeLanceProjectMaster.FreeLanceProject_EndDate, dbo.TblFreeLanceProjectMaster.FreeLanceProject_TargetEndDate, dbo.TblFreeLanceProjectMaster.FreeLanceProject_ProjectStatus, dbo.TblFreeLanceProjectMaster.FreeLanceProject_ProjectStatus_Id, " +
                                " dbo.TblFreeLanceProjectMaster.Active, dbo.TblFreeLanceProjectMaster.BusinessID, dbo.TblFreeLanceProjectMaster.DesignationID, dbo.TblFreeLanceProjectMaster.ProjectADVTDisplayStartDate, dbo.TblFreeLanceProjectMaster.ProjectADVTDisplayEndDate, dbo.TblFreeLanceProjectMaster.CurrencyID, dbo.TblFreeLanceProjectMaster.SalaryAmount, dbo.TblFreeLanceProjectMaster.SalaryPerPeriodID, dbo.TblFreeLanceProjectMaster.ExpectedProjectHours, dbo.TblFreeLanceProjectMaster.ProjectDuration, dbo.TblFreeLanceProjectMaster.Hours, dbo.TblFreeLanceProjectMaster.HoursPerPeriodID, dbo.TblFreeLanceProjectMaster.comid, dbo.TblFreelanceProjectDetail.ID, dbo.TblFreelanceProjectDetail.QualificationRequirements, " +
                                " dbo.TblFreelanceProjectDetail.TermsAndConditions, dbo.TblFreelanceProjectDetail.ProjectDescription, dbo.VacancyTypeMaster.Name " + 
                                " FROM   dbo.TblFreeLanceProjectMaster LEFT OUTER JOIN  dbo.TblFreelanceProjectDetail ON dbo.TblFreeLanceProjectMaster.FreeLanceProject_Id = dbo.TblFreelanceProjectDetail.FreelanceProjectMasterID LEFT OUTER JOIN  dbo.VacancyTypeMaster ON dbo.TblFreeLanceProjectMaster.FreeLanceProject_DeptId = dbo.VacancyTypeMaster.ID LEFT OUTER JOIN  dbo.WareHouseMaster ON dbo.TblFreeLanceProjectMaster.comid = dbo.WareHouseMaster.comid " +                    
                                " where TblFreeLanceProjectMaster.FreeLanceProject_Id='" + id + "'"; 
                SqlDataAdapter da = new SqlDataAdapter(strii, con);
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    lblprojectcatagory.Text = Convert.ToString(dt.Rows[0]["Name"]);

                    lblprojecttitle.Text = Convert.ToString(dt.Rows[0]["FreeLanceProject_ProjectTitle"]);

                    lblprojectstartdate.Text = Convert.ToString(dt.Rows[0]["FreeLanceProject_StartDate"]);

                    lblprojectenddate.Text = Convert.ToString(dt.Rows[0]["FreeLanceProject_TargetEndDate"]);
                    try
                    {
                         
                    }
                    catch (Exception ex)
                    {
                    }

                    lblprojectduration.Text = Convert.ToString(dt.Rows[0]["ProjectDuration"]);

                    lblprojectdurationmax.Text = Convert.ToString(dt.Rows[0]["ExpectedProjectHours"]);

                    lblNo.Text = Convert.ToString(dt.Rows[0]["Hours"]);

                    lblAmount.Text = Convert.ToString(dt.Rows[0]["SalaryAmount"]);

                    //lblDay1.Text = Convert.ToString(dt.Rows[0]["Hours"]);

                    lblprojectDesc.Text = Convert.ToString(dt.Rows[0]["FreeLanceProject_ProjectDescription"]);

                    lblqualifi.Text = Convert.ToString(dt.Rows[0]["QualificationRequirements"]);

                    lblTandC.Text = Convert.ToString(dt.Rows[0]["TermsAndConditions"]);
                }
                SqlDataAdapter dasl = new SqlDataAdapter("select CurrencyMaster.CurrencyName from CurrencyMaster inner join TblFreeLanceProjectMaster on TblFreeLanceProjectMaster.CurrencyID = CurrencyMaster.CurrencyId where FreeLanceProject_Id='" + id + "'", con);
                DataTable dtsl = new DataTable();
                dasl.Fill(dtsl);

                if (dtsl.Rows.Count > 0)
                {
                    lblCurrency.Text = Convert.ToString(dtsl.Rows[0]["CurrencyName"]);
                }
                SqlDataAdapter dasl11 = new SqlDataAdapter("select SalaryPerPeriodMaster.Name from SalaryPerPeriodMaster inner join TblFreeLanceProjectMaster on TblFreeLanceProjectMaster. SalaryPerPeriodID = SalaryPerPeriodMaster.ID where FreeLanceProject_Id='" + id + "'", con);
                DataTable dtsl11 = new DataTable();
                dasl11.Fill(dtsl11);

                if (dtsl11.Rows.Count > 0)
                {
                    lblDay.Text = Convert.ToString(dtsl11.Rows[0]["Name"]);
                }

                //SqlDataAdapter day123 = new SqlDataAdapter("select SalaryPerPeriodMaster.Name from SalaryPerPeriodMaster inner join TblFreeLanceProjectMaster on TblFreeLanceProjectMaster. SalaryPerPeriodID = SalaryPerPeriodMaster.ID where FreeLanceProject_Id='" + id + "'", con);
                //DataTable day123 = new DataTable();
                //dasl11.Fill(day123);

                //if (dtsl11.Rows.Count > 0)
                //{
                //    lblDay1.Text = Convert.ToString(dtsl11.Rows[0]["Name"]);
                //}
                fillattach();
            }

            
        }
    }



    protected void fillattach()
    {
        gridFileAttach.DataSource = null;
        //TblFreeLanceProjectDocument (DocumentMaster_ProjectMaster_Id,DocumentTitle,DocumentFileName,DocumentUploadDate,Doc) Values ('" + ViewState["promaxid"] + "','" + name + "','" + filename + "','" + DateTime.Now + "','" + filenamedoc + "')";
        string strSQL = "SELECT DocumentMaster_ProjectMaster_Id,DocumentTitle as Title ,DocumentFileName as AudioURL ,DocumentUploadDate, Doc as PDFURL FROM TblFreeLanceProjectDocument WHERE DocumentMaster_ProjectMaster_Id = " + id;
        con.Open();
        DataTable dtFiles = new DataTable();
        SqlCommand cmd = new SqlCommand(strSQL, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        da.Fill(dtFiles);
        if (dtFiles.Rows.Count > 0)
        {
            gridFileAttach.DataSource = dtFiles;
            gridFileAttach.DataBind();
            gridFileAttach.Visible = true;
           
        }
        else
        {

           
        }

    }
    protected void gridFileAttach_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Download")
        {
            String mm = "http://members.ijobcenter.com/Attach/" + Convert.ToString(e.CommandArgument);

            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + mm + "');", true);
        }
    }
    protected void Button10_Click(object sender, EventArgs e)
    {
        Panel3.Visible = true;
        Panel2.Visible = false;
        Panel4.Visible = false;
    }
    protected void Button11_Click(object sender, EventArgs e)
    {
        Panel3.Visible = false;
        Panel2.Visible = false;
        Panel4.Visible = true;
    }

    protected void Button12_Click(object sender, EventArgs e)
    {
        Panel3.Visible = false;
        Panel2.Visible = true;
        Panel4.Visible = false;
    }
}