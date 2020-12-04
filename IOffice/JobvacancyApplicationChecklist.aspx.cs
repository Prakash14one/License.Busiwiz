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
using System.Net;
using System.Net.Mail;
using System.Text;

public partial class ShoppingCart_Admin_JobvacancyApplicationChecklist : System.Web.UI.Page
{
    SqlConnection con;
    protected void Page_Load(object sender, EventArgs e)
    {
        PageConn pgcon = new PageConn();
        con = pgcon.dynconn;
        if (!IsPostBack)
        {
            title();
            Fillgrid();
           
           
        }
    }
    public void title()
    {


        string dav = @"select distinct    VacancyPositionTitleMaster.ID,VacancyPositionTitleMaster.VacancyPositionTitle +' , '+ CAST(startdate AS VARCHAR(20)) as VacancyPositionTitle from VacancyPositionTitleMaster" +
                          " inner join VacancyMasterTbl on VacancyMasterTbl.vacancypositiontitleid=VacancyPositionTitleMaster.ID" +
                          " where VacancyMasterTbl.comid='" + Session["Comid"].ToString() + "' ";
            
            SqlDataAdapter da = new SqlDataAdapter(dav, con);
            DataTable dt = new DataTable();
            da.Fill(dt);
           

            if (dt.Rows.Count > 0)
            {
                DropDownList1.DataSource = dt;
                DropDownList1.DataTextField = "VacancyPositionTitle";
                DropDownList1.DataValueField = "ID";
                DropDownList1.DataBind();
            }
           

            string sel = "select vacancypositiontitleid from VacancyMasterTbl order by ID desc";
            SqlCommand cmd1 = new SqlCommand(sel,con);
            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
            DataTable dt34 = new DataTable();
            da1.Fill(dt34);
            string id = dt34.Rows[0][0].ToString();
             DropDownList1.SelectedValue=id;

        
     }
    protected void Button9_Click(object sender, EventArgs e)
    {
        Button8.Visible = true;
        Button11.Visible = false;
        Panel5.Visible = true;
        string ss1 = "select VacancyPositionTitle from VacancyPositionTitleMaster where ID=" + DropDownList1 .SelectedValue+ "";
        SqlDataAdapter da3 = new SqlDataAdapter(ss1, con);
        DataTable dt1 = new DataTable();
        da3.Fill(dt1);

        Label7.Text = dt1.Rows[0][0].ToString();

        string ss = " select count(CompanyID) from JobvacancyChecklistQuestions where JobvacancyID= " + DropDownList1.SelectedValue + " and CompanyID='" + Session["Comid"].ToString() + "'";
        SqlDataAdapter da = new SqlDataAdapter(ss, con);
        DataTable dt = new DataTable();
        da.Fill(dt);

        if (dt.Rows.Count > 0)
        {
            int i = Convert.ToInt32(dt.Rows[0][0].ToString());
            int j = i + 1;
            Label9.Text = Convert.ToString(j);
        }
        else
        {
            Label9.Text ="1";
        }
        TextBox1.Text = "";
       //RadioButtonList1.SelectedValue = "";
    }
    protected void Button8_Click(object sender, EventArgs e)
    {
        con.Open();
        string ins = "  insert into JobvacancyChecklistQuestions(CompanyID,JobvacancyID,QuestionNumber,QuestionText,CorrectAnswer) values('" + Session["Comid"] + "'," + DropDownList1.SelectedValue + "," + Label9.Text + ",'"+TextBox1.Text+"',"+RadioButtonList1.SelectedValue+")";
        SqlCommand cmd = new SqlCommand(ins,con);
        cmd.ExecuteNonQuery();
        con.Close();
        Panel5.Visible = false;
        Fillgrid();
    }
    public void Fillgrid()
    {
        string str1 = "";

        string select = "select ID,JobvacancyChecklistQuestions.QuestionNumber,JobvacancyChecklistQuestions.QuestionText,case when(JobvacancyChecklistQuestions.CorrectAnswer=1) then 'Yes' else 'No' end as CorrectAnswer from JobvacancyChecklistQuestions where CompanyID='" + Session["Comid"].ToString() + "' and JobvacancyID=" + DropDownList1.SelectedValue + "";

        SqlDataAdapter da = new SqlDataAdapter(select, con);
        DataTable dt = new DataTable();
        da.Fill(dt);

        if (dt.Rows.Count > 0)
        {
            GridView2.DataSource = dt;
            GridView2.DataBind();
        }
        else
        {
            GridView2.DataSource = null;
            GridView2.DataBind();
        }
        
    }
    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {
        string dav = @"select   distinct VacancyPositionTitleMaster.ID,VacancyPositionTitleMaster.VacancyPositionTitle +' , '+ CAST(startdate AS VARCHAR(20)) as VacancyPositionTitle from VacancyPositionTitleMaster" +
                         " inner join VacancyMasterTbl on VacancyMasterTbl.vacancypositiontitleid=VacancyPositionTitleMaster.ID" +
                         " where VacancyMasterTbl.comid='" + Session["Comid"].ToString() + "' and VacancyPositionTitleMaster.Active=1";

        SqlDataAdapter da = new SqlDataAdapter(dav, con);
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
    protected void Button10_Click(object sender, EventArgs e)
    {
        TextBox1.Text = "";
        Panel5.Visible = false;
    }
   
    protected void Button11_Click(object sender, EventArgs e)
    {
        con.Open();
        string sf = "update JobvacancyChecklistQuestions set QuestionText='" + TextBox1.Text + "',CorrectAnswer='" + RadioButtonList1.SelectedValue + "'  where ID=" + ViewState["id"] + "";
        SqlCommand cmd34 = new SqlCommand(sf, con);
        cmd34.ExecuteNonQuery();
        con.Close();
        Panel5.Visible = false;
        Fillgrid();
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        Fillgrid();
    }
    protected void GridView2_RowCommand(object sender, GridViewCommandEventArgs e)
    {
       
    }
    protected void ImageButton48_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton lnkbtn = (ImageButton)sender;
        GridViewRow row = (GridViewRow)lnkbtn.NamingContainer;
        int j = Convert.ToInt32(row.RowIndex);
        string id = GridView2.Rows[j].Cells[5].Text;

        
        ViewState["id"] = id;
        string dd = "select * from JobvacancyChecklistQuestions where ID=" + id + "";
        SqlDataAdapter da = new SqlDataAdapter(dd, con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            Label9.Text = dt.Rows[0]["QuestionNumber"].ToString();
            TextBox1.Text = dt.Rows[0]["QuestionText"].ToString();
            RadioButtonList1.SelectedValue = dt.Rows[0]["CorrectAnswer"].ToString();

            string ss1 = "select VacancyPositionTitle from VacancyPositionTitleMaster where ID=" + dt.Rows[0]["JobvacancyID"].ToString() + "";
            SqlDataAdapter da3 = new SqlDataAdapter(ss1, con);
            DataTable dt1 = new DataTable();
            da3.Fill(dt1);
            Label7.Text = dt1.Rows[0][0].ToString();
            Panel5.Visible = true;
            Button8.Visible = false;
            Button11.Visible = true;
        }
        
    }
    protected void ImageButton49_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton lnkbtn = (ImageButton)sender;
        GridViewRow row = (GridViewRow)lnkbtn.NamingContainer;
        int j = Convert.ToInt32(row.RowIndex);
        string id = GridView2.Rows[j].Cells[5].Text;
       
        ViewState["id"] = id;
        con.Open();
        string del = "delete from JobvacancyChecklistQuestions where ID=" + id + "";
        SqlCommand cmd34 = new SqlCommand(del, con);
        cmd34.ExecuteNonQuery();
        con.Close();
        Fillgrid();

    }
    protected void GridView2_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if ((e.Row.RowState == DataControlRowState.Normal || e.Row.RowState == DataControlRowState.Alternate) && (e.Row.RowType == DataControlRowType.DataRow || e.Row.RowType == DataControlRowType.Header))
        {
            e.Row.Cells[5].Visible = false;


        }
    }
}