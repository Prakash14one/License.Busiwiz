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

public partial class DiscountMaster : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            lblVersion.Text = "This PageVersion Is 1  Date:26-April-2016 Developed By vinod";
            DiscountType();
            FillGridView1();



        }



    }



    protected void DiscountType()
    {

        string strcln = " SELECT  DiscountTypeName,ID from DiscountType";
        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);
        DropDownList1.DataSource = dtcln;

        DropDownList1.DataValueField = "ID";
        DropDownList1.DataTextField = "DiscountTypeName";
        DropDownList1.DataBind();
        DropDownList1.Items.Insert(0, "Select");
       
        DropDownList1.Items[0].Value = "0";


    }


    protected void btnsave_Click(object sender, EventArgs e)
   {
     
            string sgw1 = "insert into DiscountMaster(DiscountName,DiscountTypeID,DiscountPercentage,DiscoutAmount,Active) values('" + txtDiscountName.Text + "','" + DropDownList1.SelectedValue + "','" + txtDiscountPercentage.Text + "','" + txtDiscoutAmount.Text + "','" + drpstatus.SelectedValue + "')";
            SqlCommand mycmd = new SqlCommand(sgw1, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            mycmd.ExecuteNonQuery();
            con.Close();
         
            statuslable.Visible = true;
            statuslable.Text = "Record inserted successfully";
            FillGridView1();

            txtDiscountName.Text = "";
            DropDownList1.SelectedIndex=0;

            txtDiscoutAmount.Text = "";
            txtDiscountPercentage.Text = "";
            drpstatus.SelectedIndex = 0;


            pnladd.Visible = false;
         
       btnadd.Visible = true;
           
        
    }
    protected void FillGridView1()
    {
        string strcln = " select DiscountMaster.ID,DiscountMaster.DiscountName,DiscountMaster.DiscountPercentage,DiscountMaster.Active,DiscountMaster.DiscoutAmount ,DiscountType.DiscountTypeName " +
                        " from DiscountMaster inner join DiscountType on DiscountType.ID=DiscountMaster.DiscountTypeID";

        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);
        if (dtcln.Rows.Count > 0)
        {
            GridView1.DataSource = dtcln;
            GridView1.DataBind();
        }
        else
        {
            GridView1.DataSource = "";
            GridView1.DataBind();
            statuslable.Visible = false;

        }
    }
    protected void Btn_Click(object sender, ImageClickEventArgs e)
    {
     
        ImageButton lnkbtn = (ImageButton)sender;
        GridViewRow row = (GridViewRow)lnkbtn.NamingContainer;
        int j = Convert.ToInt32(row.RowIndex);
        Label res = (Label)GridView1.Rows[j].FindControl("lblDiscountType");

        string sr4 = ("delete from DiscountMaster where ID = '"+ res.Text +"'");
        SqlCommand cmd8 = new SqlCommand(sr4, con);

        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmd8.ExecuteNonQuery();
        con.Close();

        statuslable.Visible = true;
        statuslable.Text = "Record deleted successfully";

        FillGridView1();
    }
    protected void llinedit_Click(object sender, ImageClickEventArgs e) //imagebutton//
    {
        btnsave.Visible = false;

        ImageButton lnkbtn1 = (ImageButton)sender;
        GridViewRow row = (GridViewRow)lnkbtn1.NamingContainer;
        int j = Convert.ToInt32(row.RowIndex);
        Label res = (Label)GridView1.Rows[j].FindControl("lblDiscountType");
        string strcln = " select * from DiscountMaster where DiscountMaster.ID='" + res.Text + "' ";
        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);
        if (dtcln.Rows.Count > 0)
        {
            txtDiscountName.Text = dtcln.Rows[0]["DiscountName"].ToString();
            txtDiscountPercentage.Text = dtcln.Rows[0]["DiscountPercentage"].ToString();
            txtDiscoutAmount.Text = dtcln.Rows[0]["DiscoutAmount"].ToString();
            DropDownList1.SelectedValue = dtcln.Rows[0]["DiscountTypeID"].ToString();
            drpstatus.SelectedValue = dtcln.Rows[0]["Active"].ToString();
        }
        pnladd.Visible = true;



   
    }
    protected void Update_Click(object sender, EventArgs e)
    {



        string sgw1 = "update DiscountMaster set DiscountName='" + txtDiscountName.Text + "',DiscountTypeID='" + DropDownList1.SelectedValue + "',DiscountPercentage='" + txtDiscountPercentage.Text + "',DiscoutAmount='" + txtDiscoutAmount.Text + "',Active='" + drpstatus.SelectedValue + "'";



        SqlCommand mycmd = new SqlCommand(sgw1, con);

        if (con.State.ToString() != "Open")
        {
            con.Open();

        }

        mycmd.ExecuteNonQuery();
        con.Close();

   
        pnladd.Visible = false;

        statuslable.Visible = true;
        statuslable.Text = "Record Updated successfully";
        FillGridView1();

        txtDiscountName.Text = "";
        DropDownList1.SelectedIndex = 0;

        txtDiscoutAmount.Text = "";
        txtDiscountPercentage.Text = "";
        drpstatus.SelectedIndex = 0;

    }
    protected void btnadd_Click(object sender, EventArgs e)
    {
        pnladd.Visible = true;
        btnupdate.Visible = true;
        btnadd.Visible = false;
        statuslable.Visible = false;
        btnsave.Visible = true;
        buttonupdate.Visible = false;
    }
    protected void btnupdate_Click(object sender, EventArgs e)
    {
        pnladd.Visible = false;
        btnadd.Visible = true;
        statuslable.Visible = false;

    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {


        if (e.CommandName == "Edit")
        {
            buttonupdate.Visible = true;
            int ii = Convert.ToInt32(e.CommandArgument);
         
            string strcln = " select DiscountMaster.DiscountName,DiscountMaster.DiscountPercentage,DiscountType.ID,DiscountMaster.Active,DiscountMaster.DiscoutAmount ,DiscountType.DiscountTypeName " +
                           " from DiscountMaster inner join DiscountType on DiscountType.ID=DiscountMaster.DiscountTypeID where DiscountMaster.ID='" + ii + "' ";
            SqlCommand cmdcln = new SqlCommand(strcln, con);
            DataTable dtcln = new DataTable();
            SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
            adpcln.Fill(dtcln);
            if (dtcln.Rows.Count > 0)
            {
                txtDiscountName.Text = dtcln.Rows[0]["DiscountName"].ToString();
                txtDiscountPercentage.Text = dtcln.Rows[0]["DiscountPercentage"].ToString();
                txtDiscoutAmount.Text = dtcln.Rows[0]["DiscoutAmount"].ToString();
                DropDownList1.SelectedValue = dtcln.Rows[0]["ID"].ToString();
                drpstatus.SelectedValue = dtcln.Rows[0]["Active"].ToString();
            }
            pnladd.Visible = true;
        }
    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }
    protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {

    }
    protected void GridView1_RowUpdated(object sender, GridViewUpdatedEventArgs e)
    {

    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        FillGridView1();
    }
}
