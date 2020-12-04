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
using System.IO;
using System.Drawing;
public partial class ProductType : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        Page.Form.Attributes.Add("enctype", "multipart/form-data");
      //  Page.Form.Attributes.Add("enctype", "multipart/form-data");
        if( !IsPostBack)
        {
            fillddltype();
            fillddltypefilter();
            fillgrid1();
        }
    }

    void fillgrid1()
    {
        string strcln = " ";

        if (ddlstatus.SelectedItem.Text == "Active")
        {
            strcln += " and Product_SubTypeTbl.Active='True'";

        }
        else if (ddlstatus.SelectedItem.Text == "Inactive")
        {
            strcln += " and Product_SubTypeTbl.Active='False'";

        }
        if (ddlproducttypeFiltr.SelectedIndex > 0)
        {
            strcln += " and Product_SubTypeTbl.ProductTypeTbl='" + ddlproducttypeFiltr.SelectedValue + "'";

        }
        string data3 = "select Product_SubTypeTbl.ID, Product_TypeTbl.ProductTypeName, Product_SubTypeTbl.Name,Product_SubTypeTbl.Active,Product_SubTypeTbl.InactiveDate ,Product_SubTypeTbl.ImageFileName from Product_SubTypeTbl inner join Product_TypeTbl on Product_SubTypeTbl.ProductTypeTbl=Product_TypeTbl.ID  Where Product_SubTypeTbl.ID!='' " + strcln + " order by Product_SubTypeTbl.ID desc";
        SqlCommand cmd1 = new SqlCommand(data3, con);
        SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
        DataTable dt1 = new DataTable();

        sda1.Fill(dt1);
        GridView1.DataSource = dt1;
        GridView1.DataBind();
        {

        }
    }
    protected void BtnGo_Click(object sender, EventArgs e)
    {
        fillgrid1();
    }
    void fillddltype()
    
    {
        string data = "select * from Product_TypeTbl Where ProductTypeName !='' and Active='1' order by ProductTypeName ";
        SqlCommand cmd = new SqlCommand(data, con);
        SqlDataAdapter sda = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        sda.Fill(dt);
      
            Pdtypeddl.DataSource = dt;
            Pdtypeddl.DataTextField = "ProductTypeName";
            Pdtypeddl.DataValueField = "ID";
            Pdtypeddl.DataBind();        
        Pdtypeddl.Items.Insert(0, "--Select--");
        Pdtypeddl.Items[0].Value = "0";
    }

    void fillddltypefilter()
    {
        string data = "select * from Product_TypeTbl Where ProductTypeName !='' and Active='1' order by ProductTypeName ";
        SqlCommand cmd = new SqlCommand(data, con);
        SqlDataAdapter sda = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        sda.Fill(dt);

        ddlproducttypeFiltr.DataSource = dt;
        ddlproducttypeFiltr.DataTextField = "ProductTypeName";
        ddlproducttypeFiltr.DataValueField = "ID";
        ddlproducttypeFiltr.DataBind();
        ddlproducttypeFiltr.Items.Insert(0, "--Select--");
        ddlproducttypeFiltr.Items[0].Value = "0";
    }
    public bool ext111(string filename)
    {
        string[] validFileTypes = { "jpeg", "bmp", "gif", "png", "jpg" };

        string ext = System.IO.Path.GetExtension(filename);

        bool isValidFile = true;

        for (int i = 0; i < validFileTypes.Length; i++)
        {

            if (ext == "." + filename)
            {

                isValidFile = true;

                break;

            }

        }
        return isValidFile;
    }
 
   
    protected void Btnsave_Click(object sender, EventArgs e)
    {        
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        SqlCommand cmd = new SqlCommand("Product_SubTypeTbl_AddDelUpdtSelect", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@StatementType", "Insert");
        cmd.Parameters.AddWithValue("@Name", TextBox1.Text);
        cmd.Parameters.AddWithValue("@ProductTypeTbl", Pdtypeddl.SelectedValue);
        cmd.Parameters.AddWithValue("@Active", chkboxActiveDeactive.Checked);
        cmd.Parameters.AddWithValue("@InactiveDate", txtEndDate.Text);
        cmd.Parameters.AddWithValue("@ImageFileName", ViewState["url"]);
        cmd.ExecuteNonQuery();
        con.Close();
        lblmsg.Visible = true;
        lblmsg.Text = "Record inserted successfully.";
        Clr();
        fillgrid1();
    }
    protected void Clr()
    {
        TextBox1.Text = "";
        chkboxActiveDeactive.Checked = false;
        Panel1.Visible = false;
    }   
    protected void Btnupdate_Click(object sender, EventArgs e)
    {
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        SqlCommand cmd = new SqlCommand("Product_SubTypeTbl_AddDelUpdtSelect", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@StatementType", "Update");
        cmd.Parameters.AddWithValue("@ID", ViewState["id"]);
        cmd.Parameters.AddWithValue("@Name", TextBox1.Text);
        cmd.Parameters.AddWithValue("@ProductTypeTbl", Pdtypeddl.SelectedValue);
        cmd.Parameters.AddWithValue("@Active", chkboxActiveDeactive.Checked);
        cmd.Parameters.AddWithValue("@InactiveDate", txtEndDate.Text);
        cmd.Parameters.AddWithValue("@ImageFileName", ViewState["url"]);
        cmd.ExecuteNonQuery();
        con.Close();
        lblmsg.Visible = true;
        lblmsg.Text = "Record updated successfully.";
        Clr();
        fillgrid1();
    }
    protected void Btncancel_Click(object sender, EventArgs e)
    {
        Panel1.Visible = false;
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        if (FileUpload1.HasFile)
        {
            bool valid = ext111(FileUpload1.FileName);

            if (valid == true)
            {
                FileUpload1.PostedFile.SaveAs(Server.MapPath("~\\images\\") + FileUpload1.FileName);

                Image1.Visible = true;
                Image1.ImageUrl = "~/images/" + FileUpload1.FileName.ToString();
                ViewState["url"] = FileUpload1.FileName.ToString();
               // Label15.Text = FileUpload1.FileName.ToString();
                lblmsg.Text = "";
            }

            else
            {
                lblmsg.Visible = true;
                lblmsg.Text = "Invalid File Type. Please upload an image file in one of the following formats: bmp, gif, png, jpg, jpeg, ipg.";
            }

        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Btnsave.Visible = true;
        Btnupdate.Visible = false;
        TextBox1.Text = "";
        Image1.Visible = false;
        Pdtypeddl.SelectedValue = "0";
        chkboxActiveDeactive.Checked = false;
        txtEndDate.Text = "";
        Panel1.Visible = true; lblmsg.Visible = false;
    }
    protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
    {
        Btnsave.Visible = false;
        ImageButton lnkbtn = (ImageButton)sender;

        GridViewRow row = (GridViewRow)lnkbtn.NamingContainer;
        int j = Convert.ToInt32(row.RowIndex);

        Label id = (Label)GridView1.Rows[j].FindControl("Label15");
        string finalstr = "Select * from  Product_SubTypeTbl where ID='" + id.Text + "'";
        SqlCommand cmdcln = new SqlCommand(finalstr, con);
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        DataTable dtcln = new DataTable();
        adpcln.Fill(dtcln);
        ViewState["id"] = id.Text;
        if (dtcln.Rows.Count > 0)
        {
            TextBox1.Text = dtcln.Rows[0]["Name"].ToString();

            try
            {
                chkboxActiveDeactive.Checked = Convert.ToBoolean(dtcln.Rows[0]["Active"].ToString());
            }
            catch (Exception ex)
            {
            }  

            //DropDownList1.SelectedValue = dtcln.Rows[0]["Active"].ToString();
            DateTime dd = Convert.ToDateTime(dtcln.Rows[0]["InactiveDate"].ToString());
            string dd1 = dd.ToString("MM/dd/yyyy");
            txtEndDate.Text = dd1;
            Image1.ImageUrl = "~\\images\\" + dtcln.Rows[0]["ImageFileName"].ToString();
            Label15.Text = dtcln.Rows[0]["ImageFileName"].ToString();
            Pdtypeddl.SelectedValue = dtcln.Rows[0]["ProductTypeTbl"].ToString();
            Panel1.Visible = true;
            Btnsave.Visible = false;
            Btnupdate.Visible = true;
            lblmsg.Text = ""; 

        }


       
    }
    protected void imgdelete_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton lnkbtn = (ImageButton)sender;

        GridViewRow row = (GridViewRow)lnkbtn.NamingContainer;
        int j = Convert.ToInt32(row.RowIndex);

        Label id = (Label)GridView1.Rows[j].FindControl("Label15");
        string finalstr = "Select * from  Product_SubSubTypeTbl where ProductSubTypeTblID='" + id.Text + "'";
        SqlCommand cmdcln = new SqlCommand(finalstr, con);
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        DataTable dtcln = new DataTable();
        adpcln.Fill(dtcln);
        if (dtcln.Rows.Count == 0)
        {
            //string finalstr1 = "delete from  Product_SubTypeTbl where ID='" + id.Text + "'";
            //SqlCommand cmdcln1 = new SqlCommand(finalstr1, con);
            //con.Open();
            //cmdcln1.ExecuteNonQuery();
            //con.Close();
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            SqlCommand cmd = new SqlCommand("Product_SubTypeTbl_AddDelUpdtSelect", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@StatementType", "Delete");
            cmd.Parameters.AddWithValue("@ID", id.Text);
            cmd.ExecuteNonQuery();
            con.Close();
            lblmsg.Visible = true;
            lblmsg.Text = "Record Deleted Sucessfully";
            fillgrid1();
        }       
        else
        {
             lblmsg.Visible = true;
            lblmsg.Text = "Sorry, You are not allow delete this record,first delete chield record.";
        }
        
       
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        fillgrid1();

    }
}