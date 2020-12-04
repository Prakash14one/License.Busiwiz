using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
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

public partial class shortlistingofcandidateNew : System.Web.UI.Page
{
    SqlConnection con;
    protected void Page_Load(object sender, EventArgs e)
    {
        PageConn pgcon = new PageConn();
        con = pgcon.dynconn;
        if (!IsPostBack)
        {
            fillvacancy();
            fillcountry();
            Panel2.Visible = true;
            fillgrid();
            Panel2.Visible = false;
            //Button1.Visible = true;
            Panel1.Visible = false;
            Panel3.Visible = false;
           // Button1.Visible = false;
            Label13.Visible = false;
        }

    }
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    public void fillvacancy()
    {
        string str = "select distinct VacancyPositionTitleMaster.ID,VacancyPositionTitle  from VacancyPositionTitleMaster " +

                  "inner join VacancyMasterTbl  on VacancyMasterTbl.vacancypositiontitleid=VacancyPositionTitleMaster.ID  ";
        SqlDataAdapter da = new SqlDataAdapter(str, con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            DropDownList1.DataSource = dt;
            DropDownList1.DataTextField = "VacancyPositionTitle";
            DropDownList1.DataValueField = "ID";
            DropDownList1.DataBind();
        }
        DropDownList1.Items.Insert(0, "-Select-");
        DropDownList1.Items[0].Value = "0";



    }
    protected void fillcountry()
    {
        string qryStr = "select CountryId,CountryName from CountryMaster order by CountryName";
        SqlDataAdapter da = new SqlDataAdapter(qryStr, con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        DropDownList2.DataSource = dt;
        DropDownList2.DataTextField = "CountryName";
        DropDownList2.DataValueField = "CountryId";
        DropDownList2.DataBind();
        DropDownList2.Items.Insert(0, "-Select-");
        DropDownList2.Items[0].Value = "0";
    }
    protected void fillstate()
    {
        DropDownList3.Items.Clear();

        if (DropDownList2.SelectedIndex > 0)
        {
            string qryStr = "select StateId,StateName from StateMasterTbl where CountryId=" + DropDownList2.SelectedValue + " order by StateName";
            SqlDataAdapter da = new SqlDataAdapter(qryStr, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            DropDownList3.DataSource = dt;
            DropDownList3.DataTextField = "StateName";
            DropDownList3.DataValueField = "StateId";
            DropDownList3.DataBind();
        }
        else
        {
            DropDownList3.Items.Insert(0, "-Select-");
            DropDownList3.Items[0].Value = "0";
        }
    }
    protected void fillcity()
    {
        DropDownList4.Items.Clear();

        if (DropDownList3.SelectedIndex > 0)
        {
            string qryStr = "select CityId,CityName from CityMasterTbl where StateId=" + DropDownList3.SelectedValue + " order by CityName";
            SqlDataAdapter da = new SqlDataAdapter(qryStr, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            DropDownList4.DataSource = dt;
            DropDownList4.DataTextField = "CityName";
            DropDownList4.DataValueField = "CityId";
            DropDownList4.DataBind();
        }
        else
        {
            DropDownList4.Items.Insert(0, "-Select-");
            DropDownList4.Items[0].Value = "0";
        }
    }
    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillstate();
    }
    protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillcity();
    }
    //public void fillgrid()
    //{
    //    DataTable dt_s = new DataTable();
    //    string str1 = "";
    //    if (DropDownList1.SelectedValue != "0")
    //    {
    //        str1 += "and VacancyPositionTitleMaster.ID=" + DropDownList1.SelectedValue + "";
    //    }
    //    if (DropDownList2.SelectedIndex > 0)
    //    {
    //        str1 += "and CountryMaster.CountryId=" + DropDownList2.SelectedValue + "";
    //    }
    //    if (DropDownList3.SelectedIndex > 0)
    //    {
    //        str1 += "and StateMasterTbl.StateId=" + DropDownList3.SelectedValue + "";
    //    }
    //    if (DropDownList4.SelectedIndex > 0)
    //    {
    //        str1 += "and CityMasterTbl.CityId=" + DropDownList4.SelectedValue + "";
    //    }
    //        if (CheckBox1.Checked != true)
    //    {
    //        string str = "select distinct  ShortListcandidates.Candidate_id as CandidateID,VacancyPositionTitleMaster.VacancyPositionTitle,VacancyPositionTitleMaster.ID , " +
    //                "candidatemaster.LastName + ' ' + candidatemaster.FirstName + ' ' + candidatemaster.MiddleName as Candidate,  " +
    //               "CityMasterTbl.CityName+'  '+StateMasterTbl.StateName+'  '+CountryMaster.CountryName as location,ShortListcandidates.Note  " +
    //                  "from ShortListcandidates   " +
    //                "inner join candidatemaster on candidatemaster.CandidateId =ShortListcandidates.Candidate_id   " +
    //               "inner join VacancyPositionTitleMaster  on VacancyPositionTitleMaster.ID= ShortListcandidates.Vacancy_id    " +
    //               "inner join  CountryMaster on candidatemaster.CountryId=CountryMaster.CountryId   " +
    //                 "inner join StateMasterTbl on candidatemaster.StateId=StateMasterTbl.StateId  " +
    //                 "inner join CityMasterTbl on candidatemaster.City=CityMasterTbl.CityId   " + str1 + "";
    //    SqlDataAdapter da = new SqlDataAdapter(str,con);
    //    DataTable dt = new DataTable();
    //    da.Fill(dt);
    //    if (dt.Rows.Count > 0)
    //    {

    //            GridView1.DataSource = dt;
    //            GridView1.DataBind();
    //        }
    //        else
    //        {
    //            GridView1.DataSource = "";
    //            GridView1.DataBind();

    //        }
    //        }
    //            else
    //            {

    //                string st1 = "select distinct  ShortListcandidates.Candidate_id as CandidateID,candidatemaster.LastName + ' ' + candidatemaster.FirstName + ' ' + candidatemaster.MiddleName as Candidate, " +
    //                          "VacancyPositionTitleMaster.VacancyPositionTitle,VacancyPositionTitleMaster.ID,ShortListcandidates.candidate_code,  " +
    //                             "ShortListcandidates.test_center_code from [jobcenter.OADB_Developer].[dbo].ShortListcandidates  "+
    //                          "inner join candidatemaster on candidatemaster.CandidateId =ShortListcandidates.Candidate_id  "+
    //                         "inner join VacancyPositionTitleMaster  on VacancyPositionTitleMaster.ID= ShortListcandidates.Vacancy_id  "+
    //                          "inner join CountryMaster on candidatemaster.CountryId=CountryMaster.CountryId  "+
    //                          "inner join StateMasterTbl on candidatemaster.StateId=StateMasterTbl.StateId  "+
    //                          "inner join CityMasterTbl on candidatemaster.City=CityMasterTbl.CityId ";
    //            SqlDataAdapter da=new SqlDataAdapter(st1,con);
    //            DataTable dt=new DataTable();
    //            da.Fill(dt);
    //          if (dt.Rows.Count > 0)
    //        {


    //            GridView2.DataSource = dt;
    //            GridView2.DataBind();
    //        }
    //        else
    //        {
    //            GridView2.DataSource = "";
    //            GridView2.DataBind();

    //        }
    //            }
    //        }




    protected void CheckBox2_CheckedChanged(object sender, EventArgs e)
    {
        //ModalPopupExtender2.Hide();
        //ModalPopupExtender1.Show();
    }
    protected void DropDownList4_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Panel1.Visible = false;
        Button9.Visible = true;
        //Button1.Visible = false;
    }
    protected void Button8_Click(object sender, EventArgs e)
    {
        fillgrid();
    }
    protected void Button9_Click(object sender, EventArgs e)
    {
        Panel1.Visible = true;
        Button9.Visible = false;
       // Button1.Visible = true;
        Label2.Visible = false;
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        DataTable dt_s = new DataTable();
        for (int i = 0; i < GridView1.Rows.Count; i++)
        {

            CheckBox chk = (CheckBox)GridView1.Rows[i].FindControl("CheckBox2");

            if (chk.Checked == true)
            {
                TextBox txtb = (TextBox)GridView1.Rows[i].FindControl("TextBox4");
               // DropDownList ddlstatus = (DropDownList)GridView1.Rows[i].FindControl("ddlstatus");
                LinkButton lnk = (LinkButton)GridView1.Rows[i].FindControl("LinkButton4");
                if (dt_s.Rows.Count < 1)
                {
                    // dt_s.Columns.Add("Candidate_id");
                    // dt_s.Columns.Add("Vacancy_id");

                    // dt_s.Columns.Add("User_id");
                    //dt_s.Columns.Add("Note");
                    //dt_s.Columns.Add("Active");
                    //dt_s.Columns.Add("VacancyPositionTitle");
                    //dt_s.Columns.Add("Candidate");
                    //dt_s.Columns.Add("CandidateID");
                    //dt_s.Columns.Add("Location");
                    //dt_s.Columns.Add("vacancyid");


                    dt_s.Columns.Add("Vacancy_id");
                    dt_s.Columns.Add("Candidate_id");
                    dt_s.Columns.Add("User_id");
                    dt_s.Columns.Add("Note");
                    dt_s.Columns.Add("Active");

                }

                //DataRow dr = dt_s.NewRow();
                //dr["vacancyid"] = GridView1.Rows[i].Cells[6].ToString();
                //dr["CandidateID"] = lnk.ToString();
                //dr["User_id"] = Session["Comid"].ToString();
                //// dr["Note"] = txtb.Text;
                //dr["Active"] = "Yes";

                DataRow dr = dt_s.NewRow();
                dr["Vacancy_id"] = GridView1.Rows[i].Cells[6].Text;
                dr["Candidate_id"] = lnk.Text;
                dr["User_id"] = Session["Comid"].ToString();
                dr["Note"] = txtb.Text;
                dr["Active"] = "Yes";
                dt_s.Rows.Add(dr);

                Session["shortlist"] = dt_s;
               

                //dr["VacancyPositionTitle"] = dt.Rows[k][1].ToString();
                //dr["Candidate"] = dt.Rows[k][3].ToString();
                //dr["Location"] = dt.Rows[k][4].ToString();
                //dr["CandidateID"] = dt.Rows[k]["CandidateID"].ToString();
                //dr["vacancyid"] = dt.Rows[k]["ID"].ToString();




                //dt_s.Rows.Add(dr);

                //Session["shortlist"] = dt_s;


            }

        }
        for (int j = 0; j < dt_s.Rows.Count; j++)
        {
            SqlCommand code = new SqlCommand("SELECT max(candidate_code) as candidate_code  FROM ShortListcandidates ", con);
            SqlDataAdapter ada1 = new SqlDataAdapter();
            ada1.SelectCommand = code;
            DataTable adt = new DataTable();
            ada1.Fill(adt);
            //int cod = Convert.ToInt32(adt.Rows[0][0].ToString());
            if (adt.Rows.Count > 0 && adt.Rows[0][0].ToString() != null)
            {
                Session["candidate_code"] = adt.Rows[0]["candidate_code"].ToString();
            }
            else
            {
                Session["candidate_code"] = 11111111;      //frist time this no and than add 1 number each
            }
            int SyncroniceMax;
            try
            {
                SyncroniceMax = Convert.ToInt32(Session["candidate_code"]) + 1;
            }
            catch
            {
                SyncroniceMax = 11111111;
            }
            SqlCommand code1 = new SqlCommand("SELECT max(test_center_code) as test_center_code  FROM ShortListcandidates", con);
            SqlDataAdapter ada2 = new SqlDataAdapter();
            ada2.SelectCommand = code1;
            DataTable dt1 = new DataTable();
            ada2.Fill(dt1);
            if (dt1.Rows.Count > 0)
            {
                Session["test_center_code"] = dt1.Rows[0]["test_center_code"].ToString();
            }
            else
            {
                Session["test_center_code"] = 11111100;      //frist time this no and than add 1 number each
            }
            int SyncroniceMax1;
            try
            {
                SyncroniceMax1 = Convert.ToInt32(Session["test_center_code"]) + 1;
            }
            catch
            {
                SyncroniceMax1 = 11111100;
            }

            con.Open();
            //  string str = @"insert into ShortListcandidates(Vacancy_id,Candidate_id,User_id,Note,Active) values('" + dt_s.Rows[j][0].ToString() + "','" + dt_s.Rows[j][1].ToString() + "','" + dt_s.Rows[j][2].ToString() + "','" + dt_s.Rows[j][3].ToString() + "'," + dt_s.Rows[j][4].ToString() + ") ";
            //SqlCommand cmd = new SqlCommand("insertShortListcandidates", con);
            //cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.AddWithValue("@Vacancy_id", dt_s.Rows[j]["vacancyid"].ToString());
            //cmd.Parameters.AddWithValue("@Candidate_id", dt_s.Rows[j]["CandidateID"].ToString());
            //cmd.Parameters.AddWithValue("@User_id", dt_s.Rows[j]["User_id"].ToString());
            //cmd.Parameters.AddWithValue("@candidate_code", SyncroniceMax);//  SyncroniceMax
            //cmd.Parameters.AddWithValue("@test_center_code", SyncroniceMax1);
            //cmd.Parameters.AddWithValue("@Note", dt_s.Rows[j]["Note"].ToString());
            //cmd.Parameters.AddWithValue("@Active", dt_s.Rows[j]["Active"].ToString());
            //cmd.ExecuteNonQuery();
            //con.Close();
            //Session["candidate_code"] = "";
            //Session["test_center_code"] = "";
            SqlCommand cmd = new SqlCommand("insertShortListcandidates", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Vacancy_id", dt_s.Rows[j][0].ToString());
            cmd.Parameters.AddWithValue("@Candidate_id", dt_s.Rows[j][1].ToString());
            cmd.Parameters.AddWithValue("@User_id", dt_s.Rows[j][2].ToString());
            cmd.Parameters.AddWithValue("@candidate_code", SyncroniceMax);//  SyncroniceMax
            cmd.Parameters.AddWithValue("@test_center_code", SyncroniceMax1);
            cmd.Parameters.AddWithValue("@Note", dt_s.Rows[j][3].ToString());
            cmd.Parameters.AddWithValue("@Active", dt_s.Rows[j][4].ToString());
            cmd.ExecuteNonQuery();
            con.Close();
            Session["candidate_code"] = "";
            Session["test_center_code"] = "";

        }
        fillgrid();
        Panel2.Visible = false;

        Label13.Text = "Inserted successfully";
        // ModalPopupExtender1.Show();
        // }
    }
    protected void fillgrid()
    {
        DataTable dt_s = new DataTable();
        string str1 = "";
        if (DropDownList1.SelectedValue != "0")
        {
            str1 += "and VacancyPositionTitleMaster.ID=" + DropDownList1.SelectedValue + "";
        }
        if (DropDownList2.SelectedIndex > 0)
        {
            str1 += "and CountryMaster.CountryId=" + DropDownList2.SelectedValue + "";
        }
        if (DropDownList3.SelectedIndex > 0)
        {
            str1 += "and StateMasterTbl.StateId=" + DropDownList3.SelectedValue + "";
        }
        if (DropDownList4.SelectedIndex > 0)
        {
            str1 += "and CityMasterTbl.CityId=" + DropDownList4.SelectedValue + "";
        }
        //if (TextBox1.Text != "" && TextBox2.Text != "")
        //{
        //    str1 += " and CandidateMaster.EffectiveFrom between '" + TextBox1.Text + "' and '" + TextBox2.Text + "'";
        //}
        //if (TextBox3.Text != "")
        //{
        //    str1 += " and  candidatemaster.FirstName  like '%" + TextBox3.Text.Replace("'", "''") + "%'";
        //}
        if (CheckBox1.Checked == true)
        {
            string grid1 = @"select distinct  ShortListcandidates.Candidate_id as CandidateID,candidatemaster.LastName + ' ' + candidatemaster.FirstName + ' ' + candidatemaster.MiddleName as Candidate, " +
                          "VacancyPositionTitleMaster.VacancyPositionTitle,VacancyPositionTitleMaster.ID,ShortListcandidates.candidate_code,  " +
                             "ShortListcandidates.test_center_code from [jobcenter.OADB_Developer].[dbo].ShortListcandidates  " +
                          "inner join candidatemaster on candidatemaster.CandidateId =ShortListcandidates.Candidate_id  " +
                         "inner join VacancyPositionTitleMaster  on VacancyPositionTitleMaster.ID= ShortListcandidates.Vacancy_id  " +
                          "inner join CountryMaster on candidatemaster.CountryId=CountryMaster.CountryId  " +
                          "inner join StateMasterTbl on candidatemaster.StateId=StateMasterTbl.StateId  " +
                          "inner join CityMasterTbl on candidatemaster.City=CityMasterTbl.CityId   where ShortListcandidates.Active='Yes'   " + str1 + "";
            SqlDataAdapter da = new SqlDataAdapter(grid1, con);
            dt_s = new DataTable();
            da.Fill(dt_s);
            if (dt_s.Rows.Count > 0)
            {
                Panel3.Visible = true;
                //Button1.Visible = false;
                Button2.Visible = false;
                Label2.Visible = true;
                // Button9.Visible = true;
                // pnlgrid.Visible = false;
                GridView2.DataSource = dt_s;
                GridView2.DataBind();

            }

            else
            {
                GridView2.DataSource = null;
                GridView2.DataBind();
                Label2.Visible = true;
                // Button9.Visible = true;
                //Panel22.Visible = false;
                //pnlgrid.Visible = false;
            }


        }
        else
        {
            string grid = "select distinct  ShortListcandidates.Candidate_id as CandidateID,VacancyPositionTitleMaster.VacancyPositionTitle,VacancyPositionTitleMaster.ID , " +
                "candidatemaster.LastName + ' ' + candidatemaster.FirstName + ' ' + candidatemaster.MiddleName as Candidate,  " +
               "CityMasterTbl.CityName+'  '+StateMasterTbl.StateName+'  '+CountryMaster.CountryName as location,ShortListcandidates.Note  " +
                  "from ShortListcandidates   " +
                "inner join candidatemaster on candidatemaster.CandidateId =ShortListcandidates.Candidate_id   " +
               "inner join VacancyPositionTitleMaster  on VacancyPositionTitleMaster.ID= ShortListcandidates.Vacancy_id    " +
               "inner join  CountryMaster on candidatemaster.CountryId=CountryMaster.CountryId   " +
                 "inner join StateMasterTbl on candidatemaster.StateId=StateMasterTbl.StateId  " +
                 "inner join CityMasterTbl on candidatemaster.City=CityMasterTbl.CityId  where  VacancyPositionTitleMaster.Active=1  " + str1 + "";
            SqlDataAdapter da = new SqlDataAdapter(grid, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                for (int k = 0; k < dt.Rows.Count; k++)
                {
                    string str3 = @"select Candidate_id,Vacancy_id from ShortListcandidates where Vacancy_id='" + dt.Rows[k][2].ToString() + "' and  ShortListcandidates.User_id='" + Session["Comid"].ToString() + "' and ShortListcandidates.Candidate_id='" + dt.Rows[k][0].ToString() + "'";
                    SqlDataAdapter da1 = new SqlDataAdapter(str3, con);
                    DataTable dt1 = new DataTable();
                    da1.Fill(dt1);
                    if (dt1.Rows.Count < 1)
                    {
                        if (dt_s.Rows.Count < 1)
                        {

                            //dt_s.Columns.Add("image");
                            //dt_s.Columns.Add("CandidateID");
                            dt_s.Columns.Add("VacancyPositionTitle");
                            dt_s.Columns.Add("Candidate");
                            dt_s.Columns.Add("CandidateID");
                            dt_s.Columns.Add("Location");
                            dt_s.Columns.Add("vacancyid");
                            // dt_s.Columns.Add("EffectiveFrom");
                            // dt_s.Columns.Add("DocumentId");
                            //dt_s.Columns.Add("vacancyid");

                            //dt_s.Columns.Add("vid");



                        }
                        DataRow dr = dt_s.NewRow();
                        //dr["image"] = dt.Rows[k][0].ToString();
                        //dr["CandidateID"] = dt.Rows[k][1].ToString();
                        dr["VacancyPositionTitle"] = dt.Rows[k][1].ToString();
                        dr["Candidate"] = dt.Rows[k][3].ToString();
                        dr["Location"] = dt.Rows[k][4].ToString();
                        dr["CandidateID"] = dt.Rows[k]["CandidateID"].ToString();
                        dr["vacancyid"] = dt.Rows[k]["ID"].ToString();
                        //dr["EffectiveFrom"] = dt.Rows[k][6].ToString();
                        // dr["DocumentId"] = dt.Rows[k][7].ToString();
                        // dr["vacancyid"] = dt.Rows[k][8].ToString();

                        //dr["vid"] = dt.Rows[k][10].ToString();


                        dt_s.Rows.Add(dr);


                    }
                }

            }

            if (dt_s.Rows.Count > 0)
            {
                Panel2.Visible = true;
                GridView1.DataSource = dt_s;
                GridView1.DataBind();


                //Panel22.Visible = false;
                //pnlgrid.Visible = true;
                //foreach (GridViewRow ggg in GridView1.Rows)
                //{
                //    Image Image100 = (Image)ggg.FindControl("Image11236");
                //    Image100.ImageUrl = "~\\ShoppingCart\\images\\" + Image100.ImageUrl;
                //}
            }


            else
            {
                Panel2.Visible = false;
                GridView1.DataSource = null;
                GridView1.DataBind();
                Panel3.Visible = false;



                //Panel22.Visible = false;
                //pnlgrid.Visible = false;
            }

        }
    }
            
    protected void Button3_Click(object sender, EventArgs e)
    {
        Panel1.Visible = false;
        fillgrid();
       // Panel3.Visible = true;
      //  Panel2.Visible = false;
        Label2.Visible = true;
       // Button1.Visible = false;
        Panel4.Visible = true;
        Button9.Visible = true;
       
    }
}



    

