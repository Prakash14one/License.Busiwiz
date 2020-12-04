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


public partial class IOffice_ShoppingCart_Admin_OperatingSystemsAddManage : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Session["Comid"] == null)
        {
            Response.Redirect("~/Login.aspx");
        }
        if (!IsPostBack)
        {
            fillgrid();


        }


    }
    public void fillgrid()
    {

        string selct = "select * from OperatingSystemsMaster ";
        SqlCommand cmd = new SqlCommand(selct, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        if (dt.Rows.Count > 0)
        {

            GridView1.DataSource = dt;
            GridView1.DataBind();
 
        }
    
    }

    protected void Button9_Click(object sender, EventArgs e)
    {
        Button4.Visible = true;
        con.Open();
        string stwr = "select * from OperatingSystemsMaster where Operatingsystemname='" + TextBox7.Text + "' and Type='"+DropDownList1.SelectedItem.Text+"' and id='"+TextBox6.Text+"'";
        SqlCommand cmd1= new SqlCommand(stwr,con);
        SqlDataAdapter da = new SqlDataAdapter(cmd1);
        DataTable dtbn = new DataTable();
        da.Fill(dtbn);
        if (dtbn.Rows.Count == 0)
        {
            string str = "insert into OperatingSystemsMaster values('" + TextBox6.Text + "','" + TextBox7.Text + "','" + DropDownList1.SelectedItem.Text + "')";
            SqlCommand cmd = new SqlCommand(str, con);
            cmd.ExecuteNonQuery();
            fillgrid();
            Panel1.Visible = false;
            lblmsg.Text = "Record inserted successfully";
            con.Close();
        }
        else
        {
            lblmsg.Text = "Record is already exist";
            Panel1.Visible = false;
        }
       
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Panel1.Visible = true;
        Button4.Visible = false;

    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        lblmsg.Text = "";
        if (e.CommandName == "Edit")
        {
        }
        if (e.CommandName == "Delete")
        {
            con.Open();
            string str = "delete from OperatingSystemsMaster where  Operatingid='" + e.CommandArgument.ToString() + "'";
            SqlCommand cmd = new SqlCommand(str, con);
            cmd.ExecuteNonQuery();
            fillgrid();

            lblmsg.Text = "Record delete successfully";
            fillgrid();
            con.Close();
        }




    }


  protected void  Button2_Click(object sender, EventArgs e)
{
    con.Open();
    string str = "update OperatingSystemsMaster set Operatingsystemname='" + TextBox7.Text + "',Type='" + DropDownList1.SelectedItem.Text + "' where Operatingid='" + Session["id"].ToString() + "'";
    SqlCommand cmd = new SqlCommand(str, con);
    cmd.ExecuteNonQuery();
    fillgrid();
    Panel1.Visible = false;
    lblmsg.Text = "Record updated successfully";
    con.Close();

}

  protected void Button3_Click(object sender, EventArgs e)
  {
      //ddlProductname.SelectedIndex = 0;
        ////ddlpagetype.SelectedIndex = 0;
        //ddlMainMenu.SelectedIndex = 0;
        //ddlSubmenu.SelectedIndex = 0;
      TextBox7.Text = "";
      TextBox6.Text = "";
       
  }
  protected void imgbtnedit_Click(object sender, ImageClickEventArgs e)
  {

      ImageButton img = (ImageButton)sender;
      GridViewRow row = (GridViewRow)img.NamingContainer;
      int j = Convert.ToInt32(row.RowIndex);
      Label ld = (Label)GridView1.Rows[j].FindControl("lblid");
      Panel1.Visible = true;
      Button9.Visible = false;
      Button2.Visible = true;



      Session["id"] = ld.Text;
      SqlCommand cmd = new SqlCommand("SELECT * from OperatingSystemsMaster where Operatingid='" + ld.Text + "'", con);
      SqlDataAdapter da = new SqlDataAdapter(cmd);
      DataTable dtbn = new DataTable();
      da.Fill(dtbn);
      if (dtbn.Rows.Count > 0)
      {

          TextBox6.Text = dtbn.Rows[0]["Id"].ToString();
          TextBox7.Text = dtbn.Rows[0]["Operatingsystemname"].ToString();
          if (dtbn.Rows[0]["Type"].ToString() == "32 bit")
          {
              DropDownList1.SelectedValue = "0";
          }
          else {
              DropDownList1.SelectedValue = "1";
          
          }
      }
  }
}