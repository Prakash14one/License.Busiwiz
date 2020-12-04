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

public partial class ShoppingCart_Admin_referenceinput : System.Web.UI.Page
{
    SqlConnection con;
    int referenceid;
    DataTable dt5 = new DataTable();

    protected void Page_Load(object sender, EventArgs e)
    {
        PageConn pgcon = new PageConn();
        con = pgcon.dynconn;
       
      
        if (!IsPostBack)
        {
            if (Request.QueryString["referenceid"] != null)
            {

                referenceid = Convert.ToInt32(Request.QueryString["referenceid"]);
                ViewState["referenceid"] = referenceid;
            }
            string id = "select candidateid from MyRefenceTbl inner join Refernce_inputTbl on Refernce_inputTbl.reference_id=MyRefenceTbl.refernceid where Refernce_inputTbl.reference_id='" + referenceid + "'";
            SqlDataAdapter daid = new SqlDataAdapter(id, con);
            DataTable dt27 = new DataTable();
            daid.Fill(dt27);
            ViewState["candidateid"] = dt27.Rows[0][0].ToString();
            string str = "select Refernce_inputTbl.input_type from Refernce_inputTbl where Refernce_inputTbl.reference_id='" + ViewState["referenceid"] + "'";
            SqlDataAdapter da1 = new SqlDataAdapter(str, con);
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);
            if (dt1.Rows.Count > 0)
            {
                string id1 = dt1.Rows[0][0].ToString();
                if (id1.ToString() != "1")
                {
                    Panel4.Visible = true;
                    Panel3.Visible = false;
                }
                else
                {
                    Panel4.Visible = false;
                    Panel3.Visible = true;
                }
                fill();
                fillgrid();
            }
        }

    }
    public void fillgrid()
   {
        string str6 = "select VacancyPositionTitleMaster.ID,VacancyPositionTitleMaster.VacancyPositionTitle,VacancyTypeMaster.Name " +
                          " from VacancyPositionTitleMaster " +
                         " inner join VacancyTypeMaster on VacancyTypeMaster.ID=VacancyPositionTitleMaster.VacancyPositionTypeID " +
                         " inner join CandidateJobTitles on CandidateJobTitles.titleid=VacancyPositionTitleMaster.ID " +
                         "inner join Refernce_inputTbl on Refernce_inputTbl.VacancyID=VacancyPositionTitleMaster.ID  " +
                          " where candidateid= '" + ViewState["candidateid"] + "' and  Refernce_inputTbl.reference_id='" + ViewState["referenceid"] + "' ";
        SqlDataAdapter da = new SqlDataAdapter(str6, con);
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
    public void fill()
    {
        string str2 = "select  distinct CandidatePhotoPath,CandidateMaster.LastName + ' ' + CandidateMaster.FirstName + ' ' + CandidateMaster.MiddleName as canname,CandidateMaster.MobileNo,CityMasterTbl.CityName,StateMasterTbl.StateName,CountryMaster.CountryName,Party_master.Email from CandidateMaster " +
                      "inner join CountryMaster on CountryMaster.CountryId= CandidateMaster.CountryId inner join StateMasterTbl on StateMasterTbl.StateId=CandidateMaster.StateId " +
                      "inner join CityMasterTbl on CityMasterTbl.CityId=CandidateMaster.City inner join Party_master on  Party_master.PartyID=CandidateMaster.PartyID " +
                      "inner join MyRefenceTbl on MyRefenceTbl.candidateid =CandidateMaster.CandidateId where CandidateMaster.CandidateId = '" + ViewState["candidateid"] + "'  and MyRefenceTbl.refernceid='" + ViewState["referenceid"] + "'";
        SqlDataAdapter da = new SqlDataAdapter(str2, con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            string imagin = dt.Rows[0]["CandidatePhotoPath"].ToString();
            photo.Visible = true;
            photo.ImageUrl = "~/Shoppingcart/images/" + imagin;
            candidatename.Text = dt.Rows[0][1].ToString();
            candphone.Text = dt.Rows[0][2].ToString();
            city.Text = dt.Rows[0][3].ToString();
            state.Text = dt.Rows[0][4].ToString();
            country.Text = dt.Rows[0][5].ToString();
            candemail.Text = dt.Rows[0][6].ToString();

            string imagin1 = dt.Rows[0]["CandidatePhotoPath"].ToString();
            Image1.Visible = true;
            Image1.ImageUrl = "~/Shoppingcart/images/" + imagin;
            candidatename1.Text = dt.Rows[0][1].ToString();
            candphone1.Text = dt.Rows[0][2].ToString();
            city1.Text = dt.Rows[0][3].ToString();
            state1.Text = dt.Rows[0][4].ToString();
            country1.Text = dt.Rows[0][5].ToString();
            candemail1.Text = dt.Rows[0][6].ToString();

        }
    }

    public void clr()
    {
        lblmsg.Text = "";
        TextBox1.Text = "";
        ddlpunct.SelectedIndex = 0;
        ddlinter.SelectedIndex = 0;
        ddlwork.SelectedIndex = 0;
        ddldepend.SelectedIndex = 0;

    }
    public void clr1()
    {
        lblmsg.Text = "";
        TextBox2.Text = "";
        ddlpunct1.SelectedIndex = 0;  
        ddlinter1.SelectedIndex = 0;
        ddlwork1.SelectedIndex = 0;
        ddldepend1.SelectedIndex = 0;
        ddlposition.SelectedIndex = 0;
    }

    protected void Button8_Click(object sender, EventArgs e) //submit for general refernce 
    {
        con.Open();
        string str3 = " Update Refernce_inputTbl set remark='" + TextBox1.Text + "', punctuality='" + ddlpunct.SelectedValue + "',dependability='" + ddldepend.SelectedValue + "',integrity='" + ddlinter.SelectedValue + "',work_ethic='" + ddlwork.SelectedValue + "', Response='Yes', Dateandtime='" + DateTime.Today.ToString() + "' where reference_id='" + ViewState["referenceid"] + "'";
        SqlCommand cmd2 = new SqlCommand(str3, con);
        cmd2.ExecuteNonQuery();
        con.Close();
        clr();
        lblmsg.Text = "General Reference For This Candidate Have Saved Successfully";
       
    }
    protected void Button9_Click(object sender, EventArgs e)
    {
        clr();
    }
    protected void Button11_Click(object sender, EventArgs e)
    {

        for (int i = 0; i < GridView1.Rows.Count; i++)
        {
            CheckBox cb = (CheckBox)GridView1.Rows[i].FindControl("CheckBox1");
            if (cb.Checked == true)
            {

                Label lbl = (Label)GridView1.Rows[i].FindControl("Label123");
                if (dt5.Rows.Count < 1)
                {
                    dt5.Columns.Add("ID");
                }
                DataRow dr = dt5.NewRow();
                dr["ID"] = lbl.Text;
                dt5.Rows.Add(dr);
                ViewState["VacancyID"] = dt5;
                ModalPopupExtenderAddnew.Show();
            }
            
        }

    }    
    protected void Button10_Click(object sender, EventArgs e)
    {
        DataTable dt_vac = ViewState["VacancyID"] as DataTable;
        for (int i = 0; i < dt_vac.Rows.Count; i++)
        {
            int count = Convert.ToInt32(dt_vac.Rows[i][0].ToString());
            con.Open();
            string str4 = " Update Refernce_inputTbl set punctuality='" + ddlpunct1.SelectedValue + "',dependability='" + ddldepend1.SelectedValue + "',integrity='" + ddlinter1.SelectedValue + "',work_ethic='" + ddlwork1.SelectedValue + "', Response='Yes', Dateandtime='" + DateTime.Today.ToString() + "',rateofposition='" + ddlposition.SelectedValue + "',remark='" + TextBox2.Text + "' where VacancyID='" + count + "'and reference_id='" + ViewState["referenceid"] + "'";
            SqlCommand cmd3 = new SqlCommand(str4, con);
            cmd3.ExecuteNonQuery();
            con.Close();
            clr1();
            lblmsg.Text = "Reference For A Specific Position(s) For This Candidate Have Saved Successfully";
          
        }
     
    }
    protected void Button12_Click(object sender, EventArgs e)
    {
        clr1();
    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if ((e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate) && (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header))
        {
            e.Row.Cells[0].Visible = false;
        }

    }
}