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

public partial class Productsubsub_type : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {        
        Page.Form.Attributes.Add("enctype", "multipart/form-data");
        if (!IsPostBack)
        {
            fillddlpdttype();
            productsubtype();
            fillddltypefilter();
            productsubtypefilter();
            fillgrid();
            Panel1.Visible = false;
            Panel2.Visible = true;           
        }
    }
    protected void fillddlpdttype()
    {
        string data = "select * from Product_TypeTbl where  ProductTypeName !='' and Active='1' order by ProductTypeName ";//SaleTypeTblID=" + DropDownList3.SelectedValue + " and
        SqlCommand cmd = new SqlCommand(data, con);
        SqlDataAdapter sda = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        sda.Fill(dt);
        ddlprodtype.DataSource = dt;
        ddlprodtype.DataTextField = "ProductTypeName";
        ddlprodtype.DataValueField = "ID";
        ddlprodtype.DataBind();
        ddlprodtype.Items.Insert(0, "--Select--");
        ddlprodtype.Items[0].Value = "0";
    }
    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        productsubtype();
    }
    public void productsubtype()
    {
        string strcln = " ";

        if (ddlprodtype.SelectedIndex > 0)
        {
            strcln += " and Product_SubTypeTbl.ProductTypeTbl='" + ddlprodtype.SelectedValue + "'";
        }
        string finalstr = "Select * from  Product_SubTypeTbl where Name!='' and Active='1' " + strcln + " Order By Name  ";
        SqlCommand cmdcln = new SqlCommand(finalstr, con);
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        DataTable dtcln = new DataTable();
        adpcln.Fill(dtcln);       
            ddlsubtype.DataSource = dtcln;
            ddlsubtype.DataValueField = "ID";
            ddlsubtype.DataTextField = "Name";
            ddlsubtype.DataBind();
            ddlsubtype.Items.Insert(0, "--Select--");
            ddlsubtype.Items[0].Value = "0";
    }
    //-----------------------
    protected void fillddltypefilter()
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
    public void productsubtypefilter()
    {
        string strcln = " ";

        if (ddlproducttypeFiltr.SelectedIndex >0)
        {
            strcln += " and Product_SubTypeTbl.ProductTypeTbl='" + ddlproducttypeFiltr.SelectedValue +"'";
        }
        string finalstr = "Select * from  Product_SubTypeTbl where Name!='' " + strcln + " and Active='1' Order By Name  ";
        SqlCommand cmdcln = new SqlCommand(finalstr, con);
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        DataTable dtcln = new DataTable();
        adpcln.Fill(dtcln);
        ddlsubtypefilter.DataSource = dtcln;
        ddlsubtypefilter.DataValueField = "ID";
        ddlsubtypefilter.DataTextField = "Name";
        ddlsubtypefilter.DataBind();
        ddlsubtypefilter.Items.Insert(0, "--Select--");
        ddlsubtypefilter.Items[0].Value = "0";
    }
    protected void ddlproductypefilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        productsubtypefilter();
    }
    //--------------------------
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
        if (ddlproducttypeFiltr.SelectedIndex > 0)
        {
            strcln += " and  dbo.Product_SubTypeTbl.ProductTypeTbl='" + ddlproducttypeFiltr.SelectedValue + "'";

        }
        string finalstr = "Select  Product_SubTypeTbl.Name as subname,Product_SubSubTypeTbl.* from  dbo.Product_SubSubTypeTbl INNER JOIN dbo.Product_SubTypeTbl ON dbo.Product_SubSubTypeTbl.ProductSubTypeTblID = dbo.Product_SubTypeTbl.ID INNER JOIN dbo.Product_TypeTbl ON dbo.Product_SubTypeTbl.ProductTypeTbl = dbo.Product_TypeTbl.ID Where dbo.Product_SubSubTypeTbl.ID!='' " + strcln + " order by ID DESC";
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

    protected void Btupload_Click(object sender, EventArgs e)
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
                Label19.Text = FileUpload1.FileName;
                Image1.Visible = true;
                //Label19.Visible = true;

            }
        }
    }
    protected void Clr()
    {
        txtsubsub.Text = "";
        chkboxActiveDeactive.Checked = false;
        ddlsubtype.SelectedValue = "0";
        Panel1.Visible = false;
        txtEndDate.Text = ""; 
    }   
    protected void Btnsave_Click(object sender, EventArgs e)
    {        
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        SqlCommand cmd = new SqlCommand("ProductSubSubTypeTbl_AddDelUpdtSelect", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@StatementType", "Insert");        
        cmd.Parameters.AddWithValue("@Name", txtsubsub.Text);
        cmd.Parameters.AddWithValue("@ProductSubTypeTblID", ddlsubtype.SelectedValue);
        cmd.Parameters.AddWithValue("@Active", chkboxActiveDeactive.Checked);
        cmd.Parameters.AddWithValue("@InactiveDate", txtEndDate.Text);
        cmd.Parameters.AddWithValue("@ImageFileName", ViewState["url"]);
        cmd.ExecuteNonQuery();
        con.Close();
        lblmsg.Visible = true;
        lblmsg.Text = "Record inserted successfully.";
        Clr();
        fillgrid();
    }

    protected void Btnupdate_Click(object sender, EventArgs e)
    {
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        SqlCommand cmd = new SqlCommand("ProductSubSubTypeTbl_AddDelUpdtSelect", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@StatementType", "Update");
        cmd.Parameters.AddWithValue("@ID", ViewState["id"]);
        cmd.Parameters.AddWithValue("@Name", txtsubsub.Text);
        cmd.Parameters.AddWithValue("@ProductSubTypeTblID", ddlsubtype.SelectedValue);
        cmd.Parameters.AddWithValue("@Active", chkboxActiveDeactive.Checked);
        cmd.Parameters.AddWithValue("@InactiveDate", txtEndDate.Text);
        cmd.Parameters.AddWithValue("@ImageFileName", ViewState["url"]);
        cmd.ExecuteNonQuery();
        con.Close();
        lblmsg.Visible = true;
        lblmsg.Text = "Record Updated successfully.";
        Clr();
        fillgrid();
    }
    protected void Btncancel_Click(object sender, EventArgs e)
    {
        Panel1.Visible = false;
        Panel2.Visible = true;
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        Panel1.Visible = true;
        Panel2.Visible = false;
        Btnsave.Visible = true;
        Btnupdate.Visible = false;
        lblmsg.Text = "";
        txtsubsub.Text = "";
        txtEndDate.Text = "";
        chkboxActiveDeactive.Checked = false; 
        ddlsubtype.SelectedValue = "0";
        Panel2.Visible = true;
        Image1.Visible = false;
        Label19.Visible = false;
       // TextBox3.Text = DateTime.Now.ToString(CalendarExtender2.Format);

        
    }
    protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
    {

        ImageButton lnkbtn = (ImageButton)sender;

        GridViewRow row = (GridViewRow)lnkbtn.NamingContainer;
        int j = Convert.ToInt32(row.RowIndex);

        Label id = (Label)GridView1.Rows[j].FindControl("Label19");
        string finalstr = "Select * from  Product_SubSubTypeTbl where ID='" + id.Text + "'";
        SqlCommand cmdcln = new SqlCommand(finalstr, con);
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        DataTable dtcln = new DataTable();
        adpcln.Fill(dtcln);
        ViewState["id"] = id.Text;
        if (dtcln.Rows.Count > 0)
        {
            txtsubsub.Text = dtcln.Rows[0]["Name"].ToString();

              string  finalstrn = "Select * from  Product_SubTypeTbl where ID='" + dtcln.Rows[0]["ProductSubTypeTblID"].ToString() + "'";
              SqlCommand cmdclnn = new SqlCommand(finalstrn, con);
              SqlDataAdapter adpclnn = new SqlDataAdapter(cmdclnn);
              DataTable dtclnn = new DataTable();
               adpclnn.Fill(dtclnn);
        
                if (dtcln.Rows.Count > 0)
                {
                    fillddlpdttype();
                    ddlprodtype.SelectedValue = dtclnn.Rows[0]["ProductTypeTbl"].ToString();
                }
            productsubtype();
            ddlsubtype.SelectedValue = dtcln.Rows[0]["ProductSubTypeTblID"].ToString();
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
            Label19.Text = dtcln.Rows[0]["ImageFileName"].ToString();
            Panel1.Visible = true;
            Btnsave.Visible = false;
            Btnupdate.Visible = true;
            Image1.Visible = true;
            lblmsg.Text = ""; 
            //Label19.Visible = true;
        }
    }
    protected void imgdelete_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton lnkbtn = (ImageButton)sender;

        GridViewRow row = (GridViewRow)lnkbtn.NamingContainer;
        int j = Convert.ToInt32(row.RowIndex);

        Label id = (Label)GridView1.Rows[j].FindControl("Label19");
        string finalstr = "Select * from  Product_Model where ProductSubsubTypeId='" + id.Text + "'";
        SqlCommand cmdcln = new SqlCommand(finalstr, con);
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        DataTable dtcln = new DataTable();
        adpcln.Fill(dtcln);
        if (dtcln.Rows.Count == 0)
        {
            //string finalstr1 = "delete from  Product_SubSubTypeTbl where ID='" + id.Text + "'";
            //SqlCommand cmdcln1 = new SqlCommand(finalstr1, con);
            //con.Open();
            //cmdcln1.ExecuteNonQuery();
            //con.Close();
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            SqlCommand cmd = new SqlCommand("ProductSubSubTypeTbl_AddDelUpdtSelect", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@StatementType", "Delete");
            cmd.Parameters.AddWithValue("@ID", id.Text);
            cmd.ExecuteNonQuery();
            con.Close();
            lblmsg.Visible = true;  
            lblmsg.Text = "Record Deleted Sucessfully";
            fillgrid();
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
        fillgrid();
    }
   
}
