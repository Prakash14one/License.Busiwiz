using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

public partial class ProjectAllocation : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        ViewState["empname"] = ddlemployee.SelectedValue.ToString();
        fillemployee();
    }


    protected void fillemployee()
    {
        string strcln = " SELECT * from EmployeeMaster where ClientId='" + Session["ClientId"] + "' order by Name  ";
        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);

        ddlemployee.DataSource = dtcln;
        ddlemployee.DataValueField = "Id";
        ddlemployee.DataTextField = "Name";
        ddlemployee.DataBind();
        ddlemployee.Items.Insert(0, "---Select Employee---");

    }
    protected void BtnSubmit_Click(object sender, EventArgs e)
    {

        string strcln = "Insert Into ProjectMaster (ProjectMaster_ProjectTitle,ProjectMaster_ProjectDescription,ProjectMaster_Employee_Id,ProjectMaster_StartDate,ProjectMaster_EndDate,ProjectMaster_TargetEndDate) Values ('" + txtproname.Text + "','" + txtedescription.Text + "','" +  ViewState["empname"]  + "','" + Convert.ToDateTime(txtstartdate.Text)+ "','" + Convert.ToDateTime(txtenddate.Text)+ "','" +Convert.ToDateTime(txttargetenddate.Text) +"')";
       // string strcln = "Insert Into ProjectMaster (ProjectMaster_ProjectTitle,ProjectMaster_ProjectDescription,ProjectMaster_Employee_Id) Values ('" + txtproname.Text + "','" + txtedescription.Text + "','" + ViewState["empname"] + "')";
        
        SqlCommand cmd = new SqlCommand(strcln, con);
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        lblmsg.Visible = true;
        lblmsg.Text = "Record has been Inserted Successfully";
        con.Close();
    }
    protected void Button2_Click(object sender, EventArgs e)
    {

    }
}