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
        if( !IsPostBack)
        {
            salestype();
            salestypefilters();
            fillgrid();      
            
        }
    }
    protected void Clr()
    {
        chkboxActiveDeactive.Checked = false;
       
        txtproductType.Text = "";
        txtEndDate.Text = "";
        pnladdnew.Visible = false;
        Label14.Text = ""; 
    }  
    public void salestype()
    {
        string finalstr = "Select * from SalesTypeTbl where Active='1' Order By Name ";
        SqlCommand cmdcln = new SqlCommand(finalstr, con);
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        DataTable dtcln = new DataTable();
        adpcln.Fill(dtcln);       
            ddlsalestype.DataSource = dtcln;
            ddlsalestype.DataValueField = "ID";
            ddlsalestype.DataTextField = "Name";
            ddlsalestype.DataBind();
            ddlsalestype.Items.Insert(0, "--Select--");
            ddlsalestype.Items[0].Value = "0";
    }
     public void salestypefilters()
    {
        string finalstr = "Select * from SalesTypeTbl where Active='1' Order By Name  ";
        SqlCommand cmdcln = new SqlCommand(finalstr, con);
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        DataTable dtcln = new DataTable();
        adpcln.Fill(dtcln);
        ddlsalestypeFiltr.DataSource = dtcln;
        ddlsalestypeFiltr.DataValueField = "ID";
        ddlsalestypeFiltr.DataTextField = "Name";
        ddlsalestypeFiltr.DataBind();
        ddlsalestypeFiltr.Items.Insert(0, "--Select--");
        ddlsalestypeFiltr.Items[0].Value = "0";
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
    public void fillgrid()
    {
        string strcln = " ";

        if (ddlstatus.SelectedItem.Text == "Active")
        {
            strcln += " and Product_TypeTbl.Active='True'";
            
        }
        else if (ddlstatus.SelectedItem.Text == "Inactive")
        {
            strcln += " and Product_TypeTbl.Active='False'";
           
        }
        if (ddlsalestypeFiltr.SelectedIndex >0)
        {
            strcln += " and Product_TypeTbl.SaleTypeTblID='"+ddlsalestypeFiltr.SelectedValue +"'";

        }
        string finalstr = "Select  Product_TypeTbl.ID ,Product_TypeTbl.ProductTypeName,Product_TypeTbl.Active,InactiveDate,ImageFileName,SalesTypeTbl.Name from  Product_TypeTbl INNER JOIN SalesTypeTbl on Product_TypeTbl.SaleTypeTblID =SalesTypeTbl.ID Where Product_TypeTbl.ID!='' " + strcln + " order by ID DESC";
        SqlCommand cmdcln = new SqlCommand(finalstr, con);
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        DataTable dtcln = new DataTable();
        adpcln.Fill(dtcln);      
            GridView1.DataSource = dtcln;
            GridView1.DataBind();

        

    }
    protected void BtnGo_Click(object sender, EventArgs e)
    {
        fillgrid();
    }

    protected void Btnsave_Click(object sender, EventArgs e)
    {

        
              if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
              SqlCommand cmd = new SqlCommand("Product_TypeTbl_AddDelUpdtSelect", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StatementType", "Insert");
                cmd.Parameters.AddWithValue("@ProductTypeName", txtproductType.Text);
                cmd.Parameters.AddWithValue("@Active", chkboxActiveDeactive.Checked);
                cmd.Parameters.AddWithValue("@InactiveDate", txtEndDate.Text);
                cmd.Parameters.AddWithValue("@ImageFileName", Label18.Text);
                cmd.Parameters.AddWithValue("@SaleTypeTblID", ddlsalestype.SelectedValue);                
                cmd.ExecuteNonQuery();
                con.Close();
                lblmsg.Visible = true;
                lblmsg.Text = "Record Inserted successfully";
                Clr();
        //string finalstr = "insert into Product_TypeTbl(ProductTypeName,Active,InactiveDate,ImageFileName,SaleTypeTblID)values('" + txtproductType.Text + "','" + chkboxActiveDeactive.Checked + "','" + txtEndDate.Text + "', '" + Label18.Text + "','" + ddlsalestype.SelectedValue + "')";
        //SqlCommand cmdcln = new SqlCommand(finalstr, con);
        //con.Open();
        //cmdcln.ExecuteNonQuery();
        //con.Close();
        //Label16.Text = "Record Inserted Sucessfully";
          
        fillgrid(); 
    
        
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        bool valid = ext111(FileUpload1.FileName);
        if (valid == true)
        {

            if (FileUpload1.HasFile == true)
            {
                if (Directory.Exists(Server.MapPath("~\\images\\")) == false)
                {
                    Directory.CreateDirectory(Server.MapPath("~\\images\\"));
                }
                FileUpload1.PostedFile.SaveAs(Server.MapPath("~\\images\\") + FileUpload1.FileName);
                Image1.ImageUrl = "~\\images\\" + FileUpload1.FileName;
                Label18.Text = FileUpload1.FileName;
                Image1.Visible = true; ;
                //Label18.Visible = true;
            }
       }
      
    }
    protected void Btncancel_Click(object sender, EventArgs e)
    {
        pnladdnew.Visible = false; 
        Panel2.Visible = true;

    }
    protected void addnewpanel_Click(object sender, EventArgs e)
    {
        pnladdnew.Visible = true;
        Btnsave.Visible = true;
        Btnupdate.Visible = false;

        chkboxActiveDeactive.Checked = false; 
        lblmsg.Text = "";
        txtproductType.Text = "";
        txtEndDate.Text = "";
        ddlsalestype.SelectedIndex = 0;        
        Panel2.Visible = true;
        Image1.Visible = false;
        Label18.Visible = false;
        txtEndDate.Text = DateTime.Now.ToString(CalendarExtender2.Format);

    }
    protected void imgdelete_Click(object sender, ImageClickEventArgs e)
    {
        
        ImageButton lnkbtn = (ImageButton)sender;

        GridViewRow row = (GridViewRow)lnkbtn.NamingContainer;
        int j = Convert.ToInt32(row.RowIndex);

        Label id = (Label)GridView1.Rows[j].FindControl("Label18");
        string finalstr = "Select * from  Product_SubTypeTbl where ProductTypeTbl='" + id.Text + "'";
        SqlCommand cmdcln = new SqlCommand(finalstr, con);
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        DataTable dtcln = new DataTable();
        adpcln.Fill(dtcln);
        if (dtcln.Rows.Count == 0)
        {
            //string finalstr1 = "delete from  Product_TypeTbl where ID='" + id.Text + "'";
            //SqlCommand cmdcln1 = new SqlCommand(finalstr1, con);
            //con.Open();
            //cmdcln1.ExecuteNonQuery();
            //con.Close();
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            SqlCommand cmd = new SqlCommand("Product_TypeTbl_AddDelUpdtSelect", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@StatementType", "Delete");
            cmd.Parameters.AddWithValue("@ID", id.Text);
            cmd.ExecuteNonQuery();
            con.Close();
            lblmsg.Text = "Record Deleted Sucessfully";
            lblmsg.Visible = true;
            fillgrid();
        }
        else
        {
            lblmsg.Visible = true;
            lblmsg.Text = "Sorry, You are not allow delete this record,first delete chield record.";
        }
       
    }
    protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
    {
      
        ImageButton lnkbtn = (ImageButton)sender;
        GridViewRow row = (GridViewRow)lnkbtn.NamingContainer;
        int j = Convert.ToInt32(row.RowIndex);

        Label id = (Label)GridView1.Rows[j].FindControl("Label18");
        string finalstr = "Select * from  Product_TypeTbl where ID='" + id.Text + "'";
        SqlCommand cmdcln = new SqlCommand(finalstr, con);
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        DataTable dtcln = new DataTable();
        adpcln.Fill(dtcln);
        ViewState["id"]= id.Text;
        if (dtcln.Rows.Count > 0)
        {
            txtproductType.Text = dtcln.Rows[0]["ProductTypeName"].ToString();
            salestype();
            ddlsalestype.SelectedValue = dtcln.Rows[0]["SaleTypeTblID"].ToString();
            
            try
            {
                chkboxActiveDeactive.Checked = Convert.ToBoolean(dtcln.Rows[0]["Active"].ToString());
            }
            catch (Exception ex)
            {
            }                    
           
            DateTime dd = Convert.ToDateTime(dtcln.Rows[0]["InactiveDate"].ToString());
            string dd1 = dd.ToString("MM/dd/yyyy");
            txtEndDate.Text = dd1;
            Image1.ImageUrl = "~\\images\\" + dtcln.Rows[0]["ImageFileName"].ToString();
            Label18.Text = dtcln.Rows[0]["ImageFileName"].ToString();
            pnladdnew.Visible = true;
            Btnsave.Visible = false;
            Btnupdate.Visible = true;
            Image1.Visible = true;
            //Label18.Visible = true;

        }

    }
    protected void Btnupdate_Click(object sender, EventArgs e)
    {
        //string finalstr = "Select * from  Product_TypeTbl where ID='" + ViewState["id"].ToString() + "'";
        //SqlCommand cmdcln = new SqlCommand(finalstr, con);
        //SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        //DataTable dtcln = new DataTable();
        //adpcln.Fill(dtcln);
        //if (dtcln.Rows.Count > 0)
        //{
        //    string finalstr1 = "update Product_TypeTbl  set ProductTypeName='" + txtproductType.Text + "',Active='" + chkboxActiveDeactive.Checked + "',InactiveDate='" + txtEndDate.Text + "',ImageFileName='" + Label18.Text + "',SaleTypeTblID ='" + ddlsalestype.SelectedValue + "' where ID='" + ViewState["id"].ToString() + "'";
        //    SqlCommand cmdcln1 = new SqlCommand(finalstr1, con);
        //    con.Open();
        //    cmdcln1.ExecuteNonQuery();
        //    con.Close();
        //}
        //Label16.Text = "Record updated Sucessfully";
        //Panel1.Visible = false;
        //Panel2.Visible = true;
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        SqlCommand cmd = new SqlCommand("Product_TypeTbl_AddDelUpdtSelect", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@StatementType", "Update");
        cmd.Parameters.AddWithValue("@ID", ViewState["id"]);
        cmd.Parameters.AddWithValue("@ProductTypeName", txtproductType.Text);
        cmd.Parameters.AddWithValue("@Active", chkboxActiveDeactive.Checked);
        cmd.Parameters.AddWithValue("@InactiveDate", txtEndDate.Text);
        cmd.Parameters.AddWithValue("@ImageFileName", Label18.Text);
        cmd.Parameters.AddWithValue("@SaleTypeTblID", ddlsalestype.SelectedValue);
        cmd.ExecuteNonQuery();
        con.Close();
        lblmsg.Visible = true;
        lblmsg.Text = "Record Updeted successfully";
        Clr();
        fillgrid();     
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        fillgrid();
    }


}


