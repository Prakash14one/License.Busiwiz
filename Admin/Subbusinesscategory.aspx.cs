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
public partial class Admin_Subbusinesscategory : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        lblmsg.Visible = false;
        if (!IsPostBack)
        {
            Businessmaster();
            fillgrid();
        }
    }
    protected void Businessmaster()
    {
        string strcln = "SELECT * from BusinessCategorytbl order by businesscategoryname";
        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);
        ddldivicetype.DataSource = dtcln;
        ddldivicetype.DataValueField = "Id";
        ddldivicetype.DataTextField = "businesscategoryname";
        ddldivicetype.DataBind();
        ddldivicetype.Items.Insert(0, "-Select-");
        ddldivicetype.Items[0].Value = "0";
    }
    protected void fillgrid()
    {
        try
        {
            GridView1.DataSource = null;


            string strcln = "SELECT  Subbusinesscategorytbl.*, BusinessCategorytbl.businesscategoryname from Subbusinesscategorytbl inner join BusinessCategorytbl  on Subbusinesscategorytbl.businesscategoryId=BusinessCategorytbl.Id";
            SqlCommand cmdcln = new SqlCommand(strcln, con);
            DataTable dtcln = new DataTable();
            SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
            adpcln.Fill(dtcln);
            GridView1.DataSource = dtcln;

            GridView1.DataBind();
        }
        catch
        {
        }
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {

            GridView1.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            int i = Convert.ToInt32(GridView1.DataKeys[GridView1.SelectedIndex].Value.ToString());
            ViewState["ID"] = i.ToString();
            string strcln = "";
            if (e.CommandName == "Delete")
            {
                strcln = "delete from Subbusinesscategorytbl where Id='" + i.ToString() + "'";

                SqlCommand cmd = new SqlCommand(strcln, con);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                fillgrid();
            }
            if (e.CommandName == "edit1")
            {
              

                strcln = "Select * from Subbusinesscategorytbl where Id='" + i.ToString() + "'";
                SqlCommand cmdcln = new SqlCommand(strcln, con);
                DataTable dtcln = new DataTable();
                SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
                adpcln.Fill(dtcln);
                if (dtcln.Rows.Count > 0)
                    ddldivicetype.SelectedIndex = ddldivicetype.Items.IndexOf(ddldivicetype.Items.FindByValue(dtcln.Rows[0]["businesscategoryId"].ToString()));
                Txtaddname.Text = dtcln.Rows[0]["subcategoryname"].ToString();
                txtdesc.Text = dtcln.Rows[0]["subcategorydesc"].ToString();
                btnadd.Text = "Update";
            }
        }

        catch
        {
        }
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        fillgrid();
    }

    protected void btnadd_Click(object sender, EventArgs e)
    {
        string str = "";
        try
        {

            if (btnadd.Text == "Update")
            {
                str = "Update Subbusinesscategorytbl Set subcategoryname='" + Txtaddname.Text.Trim() + "',subcategorydesc='" + txtdesc.Text + "',businesscategoryId='" + ddldivicetype.SelectedValue + "' where ID='" + ViewState["ID"] + "'";

                lblmsg.Text = "Record Updated Successfully.";
            }
            else
            {

                str = "INSERT INTO Subbusinesscategorytbl(subcategoryname,subcategorydesc,businesscategoryId)values('" + Txtaddname.Text.Trim() + "','" + txtdesc.Text + "','" + ddldivicetype.SelectedValue + "')";
                lblmsg.Text = "Record insert Successfully.";
            }



            SqlCommand cmd = new SqlCommand(str, con);
            DataTable dt = new DataTable();
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            fillgrid();
            Txtaddname.Text = "";
            txtdesc.Text = "";
            ddldivicetype.SelectedIndex = 0;
            btnadd.Text = "Submit";
            lblmsg.Visible = true;
        }
        catch
        {
        }

    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }
}
