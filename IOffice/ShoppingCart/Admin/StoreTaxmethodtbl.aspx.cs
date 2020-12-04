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

public partial class ShoppingCart_Admin_StoreTaxmethodtbl : System.Web.UI.Page
{
   // SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
    SqlConnection con;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Comid"] == null)
        {
            Response.Redirect("~/Shoppingcart/Admin/ShoppingCartLogin.aspx");
        }
        PageConn pgcon = new PageConn();
        con = pgcon.dynconn; 
        pagetitleclass pg = new pagetitleclass();
        string strData = Request.Url.ToString();

        char[] separator = new char[] { '/' };

        string[] strSplitArr = strData.Split(separator);
        int i = Convert.ToInt32(strSplitArr.Length);
        string page = strSplitArr[i - 1].ToString();

        
        Page.Title = pg.getPageTitle(page);
        if (!IsPostBack)
        {           
            fillstore();
            ddlSearchByStore_SelectedIndexChanged(sender, e);
        }
      
    }
    protected void fillstore()
    {
        

        ddlSearchByStore.Items.Clear();
        DataTable ds = ClsStore.SelectStorename();
        ddlSearchByStore.DataSource = ds;
        ddlSearchByStore.DataTextField = "Name";
        ddlSearchByStore.DataValueField = "WareHouseId";
        ddlSearchByStore.DataBind();


        DataTable dteeed = ClsStore.SelectEmployeewithIdwise();

        if (dteeed.Rows.Count > 0)
        {
            ddlSearchByStore.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);
        }

    }
    
    //protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    //{
       
       
    //}
    protected void ddlSearchByStore_SelectedIndexChanged(object sender, EventArgs e)
    {
        //if (ddlSearchByStore.SelectedIndex == 0)
        //{
        //    rd2.Checked = false;
        //    rd1.Checked = false;
        //    rd0.Checked = true;
        //    ImageButton1.Visible = false;
        //    ImageButton9.Visible = false;
        //    imgrate.Visible = false;
        //    imgchange.Visible = false;
        //    rd0.Enabled = true;
        //    rd1.Enabled = true;
        //    rd2.Enabled = true;
        //}
        //else
        //{


            string str1 = "select * from StorTaxmethodtbl where Storeid='" + ddlSearchByStore.SelectedValue + "'";

            DataTable ds1 = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(str1, con);
            da.Fill(ds1);
            if (ds1.Rows.Count > 0)
            {
                if (ds1.Rows[0]["Fixedtaxforall"].ToString() != "False")
                {
                    rd1.Checked = false;
                    rd2.Checked = false;
                    rd0.Checked = true;
                    
                    ImageButton1.Visible = false;
                    imgchange.Visible = true;
                    imgrate.Visible = true;
                    rd0.Enabled = false;
                    rd1.Enabled = false;
                    rd2.Enabled = false;
                }
                else if (ds1.Rows[0]["Fixedtaxdependingonstate"].ToString() != "False")
                {
                    rd0.Checked = false;
                    rd2.Checked = false;
                    rd1.Checked = true;
                    ImageButton1.Visible = false;
                    imgchange.Visible = true;
                    imgrate.Visible = true;
                    rd0.Enabled = false;
                    rd1.Enabled = false;
                    rd2.Enabled = false;
                }
                else if (ds1.Rows[0]["Variabletax"].ToString() != "False")
                {
                    rd0.Checked = false;
                    rd1.Checked = false;
                    rd2.Checked = true;
                    ImageButton1.Visible = false;
                    imgchange.Visible = true;
                    imgrate.Visible = true;
                    rd0.Enabled = false;
                    rd1.Enabled = false;
                    rd2.Enabled = false;
                }
                if (rd0.Checked == true)
                {
                    pnllabel.Visible = true;
                   
                    lbltax.Text = rd0.Text;
                    pnlchange.Visible = false;
                }
                if (rd1.Checked == true)
                {
                    pnllabel.Visible = true;
                    lbltax.Text = rd1.Text;
                    pnlchange.Visible = false;
                }
                if (rd2.Checked == true)
                {
                    pnllabel.Visible = true;
                    lbltax.Text = rd2.Text;
                    pnlchange.Visible = false;
                }
            }
            else
            {
                rd2.Checked = false;
                rd1.Checked = false;
                rd0.Checked = true;
                ImageButton1.Visible = true;
                imgchange.Visible = false;
                rd0.Enabled = true;
                rd1.Enabled = true;
                rd2.Enabled = true;
                pnllabel.Visible = false;
                pnlchange.Visible = true;
                imgrate.Visible = false;
            }
       // }
    }
    //protected void ImageButton9_Click(object sender, ImageClickEventArgs e)
    //{
        
    //}

    protected void inupdata(string spro,string msg)
    {
        string url = "";
     Boolean val=true;
        Boolean nval=false;
        SqlCommand cmd = new SqlCommand(spro, con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Storeid", ddlSearchByStore.SelectedValue.ToString());
        
        if (rd0.Checked == true)
        {
            cmd.Parameters.AddWithValue("@Fixedtaxforall", val);
            cmd.Parameters.AddWithValue("@Fixedtaxdependingonstate", nval);
            cmd.Parameters.AddWithValue("@Variabletax", nval);
                url = "TaxMasterAndMoreInfo_New.aspx?wid=" + ddlSearchByStore.SelectedValue + "";
        }
        else if (rd1.Checked == true)
        {
            cmd.Parameters.AddWithValue("@Fixedtaxdependingonstate", val);
            cmd.Parameters.AddWithValue("@Fixedtaxforall", nval);
             cmd.Parameters.AddWithValue("@Variabletax", nval);
             url = "Fixedtaxdependingonstate.aspx?wid=" + ddlSearchByStore.SelectedValue + "";
        }
        else if (rd2.Checked == true)
        {
            cmd.Parameters.AddWithValue("@Variabletax", val);
            cmd.Parameters.AddWithValue("@Fixedtaxdependingonstate", nval);
            cmd.Parameters.AddWithValue("@Fixedtaxforall", nval);
             url = "TaxMasterWithInv.aspx?wid="+ddlSearchByStore.SelectedValue+"";
        }
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmd.ExecuteNonQuery();
        con.Close();
        Label1.Visible = true;
        Label1.Text = msg;
        imgrate.Visible = true;
        imgchange.Visible = true;
        ImageButton9.Visible = false;
        //Response.Redirect(url);
   }
    protected void imgrate_Click(object sender, EventArgs e)
    {
        string url = "";
       
       
        if (rd0.Checked == true)
        {
            url = "TaxMasterAndMoreInfo_New.aspx?wid=" + ddlSearchByStore.SelectedValue + "&wz=nwz";
           
            
        }
        else if (rd1.Checked == true)
        {
            url = "Fixedtaxdependingonstate.aspx?wid=" + ddlSearchByStore.SelectedValue + "&wz=nwz";
        }
        else if (rd2.Checked == true)
        {

            url = "TaxMasterWithInv.aspx?wid=" + ddlSearchByStore.SelectedValue + "&wz=nwz";
        }
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + url + "');", true);
     
        //Response.Redirect(url);
    }
    //protected void imgchange_Click(object sender, ImageClickEventArgs e)
    //{
       

    //}
    protected void imgchange_Click(object sender, EventArgs e)
    {
        pnlchange.Visible = true;
        rd0.Enabled = true;
        rd1.Enabled = true;
        rd2.Enabled = true;
        ImageButton9.Visible = true;
        imgchange.Visible = false;
        imgrate.Visible = false;
        pnllabel.Visible = false;
        // lbl.Visible = false;
       // lbltax.Visible = false;
    }
    protected void ImageButton9_Click(object sender, EventArgs e)
    {
        if (ddlSearchByStore.SelectedIndex == -1)
        {
            Label1.Text = "Please Select Business Name";
        }
        else
        {
            inupdata("Sp_Update_StorTaxmethodtbl", "Record updated successfully");
            rd0.Enabled = false;
            rd1.Enabled = false;
            rd2.Enabled = false;
            ddlSearchByStore_SelectedIndexChanged(sender, e);
        }
    }
    protected void ImageButton1_Click(object sender, EventArgs e)
    {
        if (ddlSearchByStore.SelectedIndex == -1)
        {
            Label1.Text = "Please Select Business Name";
        }
        else
        {
            inupdata("Sp_Insert_StorTaxmethodtbl", " Record inserted successfully");
            ImageButton1.Visible = false;
            rd0.Enabled = false;
            rd1.Enabled = false;
            rd2.Enabled = false;
            
            ddlSearchByStore_SelectedIndexChanged(sender, e);
        }
        
    }
}