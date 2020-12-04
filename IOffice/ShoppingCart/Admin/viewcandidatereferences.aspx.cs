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
using System.IO;
using System.Text;
using System.Net;
using System.Data.SqlClient;
using System.Globalization;
using System.Net.Mail;
using System.Security.Cryptography;

public partial class ShoppingCart_Admin_viewcandidatereferences : System.Web.UI.Page
{
    SqlConnection con;
    int referenceid;
    protected void Page_Load(object sender, EventArgs e)
    {
        PageConn pgcon = new PageConn();
        con = pgcon.dynconn;
        //referenceid =15;
        if (!IsPostBack)
        {
            if (Request.QueryString["refernceid"] != null)
            {
                referenceid = Convert.ToInt32(Request.QueryString["refernceid"]);
                ViewState["referenceid"] = referenceid;
            }
            string id = "select candidateid from MyRefenceTbl inner join Refernce_inputTbl on Refernce_inputTbl.reference_id=MyRefenceTbl.refernceid where Refernce_inputTbl.reference_id='" + ViewState["referenceid"] + "'";
            SqlDataAdapter daid = new SqlDataAdapter(id, con);
            DataTable dt27 = new DataTable();
            daid.Fill(dt27);
            ViewState["candidateid"] =dt27.Rows[0][0].ToString();
            candidate();
            fillgrid();
            fillavg();
        }


    }
    public void candidate()
    {
        //string cand = "select CandidateMaster.LastName + ' ' + CandidateMaster.FirstName + ' ' + CandidateMaster.MiddleName as canname,MobileNo,CandidatePhotoPath,Party_master.Email "+
        //              "from CandidateMaster "+
        //              "inner join Party_master on  Party_master.PartyID=CandidateMaster.PartyID "+
        //              "where CandidateId";

        String cand = "select  distinct CandidatePhotoPath,CandidateMaster.LastName + ' ' + CandidateMaster.FirstName + ' ' + CandidateMaster.MiddleName as canname,CandidateMaster.MobileNo,Party_master.Email,VacancyPositionTitleMaster.VacancyPositionTitle from CandidateMaster "+
                      "inner join Party_master on  Party_master.PartyID=CandidateMaster.PartyID "+
                      "inner join VacancyPositionTitleMaster on VacancyPositionTitleMaster.ID=CandidateMaster.Jobpositionid "+
                      "inner join MyRefenceTbl on MyRefenceTbl.candidateid =CandidateMaster.CandidateId "+
                      "where CandidateMaster.CandidateId  = '" + ViewState["candidateid"] + "'  and MyRefenceTbl.refernceid='" + ViewState["referenceid"] + "'";
        SqlCommand cmd = new SqlCommand(cand,con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            string imagin = dt.Rows[0]["CandidatePhotoPath"].ToString();
            ImageButton48.Visible = true;
            ImageButton48.ImageUrl = "~/Shoppingcart/images/" + imagin;
            Label10.Text = dt.Rows[0][1].ToString();
            Label11.Text = dt.Rows[0][2].ToString();
            Label12.Text = dt.Rows[0][3].ToString();
            Label13.Text = dt.Rows[0][4].ToString();
        }
    }

    public void fillavg()
    {
        string str = "select AVG(punctuality) as punctuality ,AVG(dependability) as dependability,AVG(integrity) as integrity, AVG(work_ethic) as work_ethic FROM Refernce_inputTbl where reference_id='" + ViewState["referenceid"] + "'";
         SqlCommand cmd = new SqlCommand(str,con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            Label15.Text = dt.Rows[0][0].ToString();
            Label16.Text = dt.Rows[0][1].ToString();
            Label17.Text = dt.Rows[0][2].ToString();
            Label18.Text = dt.Rows[0][3].ToString();
            

        }

    }
    public void fillgrid()
    {
        string str1 = "select  distinct MyRefenceTbl.refernceid,MyRefenceTbl.Name,CityMasterTbl.CityName,CountryMaster.CountryName from MyRefenceTbl inner join CountryMaster on CountryMaster.CountryId=MyRefenceTbl.countryid " +
                      "inner join CityMasterTbl on CityMasterTbl.CityId=MyRefenceTbl.cityid where MyRefenceTbl.candidateid='" + ViewState["candidateid"] + "' and MyRefenceTbl.refernceid='" + ViewState["referenceid"] + "'";
        SqlDataAdapter da = new SqlDataAdapter(str1, con);
        DataTable dt = new DataTable();

        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
        else
        {
            GridView1.DataSource = "";
            GridView1.DataBind();
        }

    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if ((e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate) && (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header))
        {
            e.Row.Cells[1].Visible = false;



        }

    }

    public void fillopinion()
    {
        string str2 = "select distinct remark ,punctuality,dependability,integrity,work_ethic from Refernce_inputTbl where reference_id='" + ViewState["referenceid"] + "' and remark!='' ";
        SqlCommand cmd = new SqlCommand(str2, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            TextBox2.Text=dt.Rows[0][0].ToString();
            Label23.Text=dt.Rows[0][1].ToString();
            Label24.Text=dt.Rows[0][2].ToString();
            Label25.Text=dt.Rows[0][3].ToString();
            Label26.Text = dt.Rows[0][3].ToString();
        }


    }
    protected void RadioButton1_CheckedChanged(object sender, EventArgs e)
    {
        RadioButton cb = (RadioButton)GridView1.Rows[0].FindControl("RadioButton1");
        if (cb.Checked == true)
        {
            Panel6.Visible = true;
            Label19.Visible = false;
            Label5.Visible = false;
            fillopinion();
        }

    }
    //protected void chkSelect_CheckedChanged(object sender, EventArgs e)
    //{

    //    CheckBox cb = (CheckBox)GridView1.Rows[0].FindControl("chkSelect");
    //    if (cb.Checked== true)
    //     {
    //         Panel6.Visible = true;
    //         fillopinion();
    //     }

    //}
}