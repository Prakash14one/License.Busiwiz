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

public partial class ProductMaster : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
       // Page.Form.Enctype = "multipart/form-data";


        Page.Form.Attributes.Add("enctype", "multipart/form-data");
        if (!IsPostBack)
        {
            fillddlpdttype();
            filldddlsales();
            filldddlsub();
            filldddlsubsub();
            fillgrid();
        }
    }
    public void  fillgrid()
    {
        string data3 = "select * from ProductMasterTbl order by ProductMasterTbl.ProductMasterID desc ";
        SqlCommand cmd1 = new SqlCommand(data3, con);
        SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
        DataTable dt1 = new DataTable();

        sda1.Fill(dt1);

        GridView1.DataSource = dt1;
        GridView1.DataBind();
       
   
   }
    void fillddlpdttype()
    {
        string data = "select * from ProductTypeTbl where ProductTypeName !='' order by ProductTypeName ";
        SqlCommand cmd = new SqlCommand(data, con);
        SqlDataAdapter sda = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        sda.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            DropDownList2.DataSource = dt;
            DropDownList2.DataTextField = "ProductTypeName";
            DropDownList2.DataValueField = "ID";
            DropDownList2.DataBind();
        }
        DropDownList2.Items.Insert(0, "-Select-");
        DropDownList2.Items[0].Value = "0";
    
    }
    void filldddlsales()
    {
        string data = "select * from SalesTypeTbl ";
        SqlCommand cmd = new SqlCommand(data, con);
        SqlDataAdapter sda = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        sda.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            DropDownList3.DataSource = dt;
            DropDownList3.DataTextField = "Name";
            DropDownList3.DataValueField = "ID";
            DropDownList3.DataBind();
        }
        DropDownList3.Items.Insert(0, "-Select-");
        DropDownList3.Items[0].Value = "0";
    }
    void filldddlsub()
    {
        string data = "select * from ProductSubTypeTbl Where Name !='' order by Name ";
        SqlCommand cmd = new SqlCommand(data, con);
        SqlDataAdapter sda = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        sda.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            DropDownList4.DataSource = dt;
            DropDownList4.DataTextField = "Name";
            DropDownList4.DataValueField = "ID";
            DropDownList4.DataBind();
        }
        DropDownList4.Items.Insert(0, "-Select-");
        DropDownList4.Items[0].Value = "0";
    }
    void filldddlsubsub()
    {
        string data = "select * from ProductSubSubTypeTbl Where Name !='' order by Name ";
        SqlCommand cmd = new SqlCommand(data, con);
        SqlDataAdapter sda = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        sda.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            DropDownList5.DataSource = dt;
            DropDownList5.DataTextField = "Name";
            DropDownList5.DataValueField = "ID";
            DropDownList5.DataBind();
        }
        DropDownList5.Items.Insert(0, "-Select-");
        DropDownList5.Items[0].Value = "0";
    }

    protected void Btnsave_Click(object sender, EventArgs e)
    {
        con.Open();
        string datr = "insert into ProductMasterTbl (ProductName,ProductStatus,ProductStartDate,ProductRetiredate,ProductTypeID,SalesTypeID,ProductSubTypeID,ProductSubSubTypeID,ProductStockQty) values ('" + TextBox1.Text + "','" + DropDownList1.SelectedItem.Text + "','" + TextBox4.Text + "','" + TextBox3.Text + "','" + DropDownList2.SelectedValue + "','" + DropDownList3.SelectedValue + "','" + DropDownList4.SelectedValue + "','" + DropDownList5.SelectedValue + "','" + TextBox5.Text + "')";
        SqlCommand cmd = new SqlCommand(datr, con);
        SqlDataAdapter sda = new SqlDataAdapter(cmd);
        cmd.ExecuteNonQuery();
        string data2 = " select max(ProductMasterID) as id from ProductMasterTbl ";
        SqlCommand cmd2 = new SqlCommand(data2, con);
        SqlDataAdapter sda2 = new SqlDataAdapter(cmd2);
        DataTable dt5 = new DataTable();
        sda2.Fill(dt5);
        if (dt5.Rows.Count > 0)
        {
            string id = dt5.Rows[0]["id"].ToString();

            string title = TextBox6.Text.ToString(); ;
            string smallimg = Label26.Text.ToString();
            string largimg= Label25.Text.ToString();
            string insert = "insert into ProductPictureTbl (ProductMasterTbl,SmallImageName,LargeImageName,PictureTitle) values ('" + id + "','" + smallimg + "','" + largimg + "','"+title+"')";
            SqlCommand cmd11 = new SqlCommand(insert, con);
            SqlDataAdapter sda11 = new SqlDataAdapter(cmd11);
            

            string insert1 = "insert into ProductPriceTbl (ProductMasterID,ListPrice,SalesPrice,EffectiveDate) values ('" + id + "','" + TextBox7.Text + "','" + TextBox8.Text + "','" + TextBox9.Text + "')";
            SqlCommand cmd111 = new SqlCommand(insert1, con);
            SqlDataAdapter sda111 = new SqlDataAdapter(cmd111);
            cmd11.ExecuteNonQuery();
            cmd111.ExecuteNonQuery();

        }

        


       
        Panel1.Visible = false;
        Label16.Visible = true;
        Label16.Text = "Record inserted succesfully";
        Button1.Visible = true;
        fillgrid();
    }
    protected void Btnupdate_Click(object sender, EventArgs e)
    {
        con.Open();
        string ab = DropDownList1.SelectedItem.Text;
        //('" + TextBox1.Text + "','" + DropDownList1.SelectedItem.Text + "','" + TextBox4.Text + "','" + TextBox3.Text + "','" + DropDownList2.SelectedValue + "','" + DropDownList3.SelectedValue + "','" + DropDownList4.SelectedValue + "','" + DropDownList5.SelectedValue + "','" + TextBox5.Text + "')";
        string data = "update ProductMasterTbl set ProductName='" + TextBox1.Text + "',ProductStatus='"+ab+"',ProductStartDate='" + TextBox4.Text + "',ProductRetiredate='" + TextBox3.Text + "',ProductTypeID='" + DropDownList2.SelectedValue + "',SalesTypeID='" + DropDownList3.SelectedValue + "',ProductSubTypeID='" + DropDownList4.SelectedValue + "',ProductSubSubTypeID='" + DropDownList5.SelectedValue + "',ProductStockQty='" + TextBox5.Text + "' where ProductMasterID='" + ViewState["id"].ToString() + "'";
        SqlCommand cmd1 = new SqlCommand(data, con);
        SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);


        string data9 = "update ProductPictureTbl set SmallImageName='" + Label26.Text + "',LargeImageName='" + Label25.Text + "',PictureTitle='" + TextBox6.Text + "' where ProductMasterTbl='" + ViewState["id"].ToString() + "'";
        SqlCommand cmd19 = new SqlCommand(data9, con);
        SqlDataAdapter sda19 = new SqlDataAdapter(cmd19);

        string data91 = "update ProductPriceTbl set ListPrice='" + TextBox7.Text + "',SalesPrice='" + TextBox8.Text + "',EffectiveDate='" + TextBox9.Text + "' where ProductMasterID='" + ViewState["id"].ToString() + "'";
        SqlCommand cmd191 = new SqlCommand(data91, con);
        SqlDataAdapter sda191 = new SqlDataAdapter(cmd191);

        cmd1.ExecuteNonQuery();
        cmd19.ExecuteNonQuery();
        cmd191.ExecuteNonQuery();
        Panel1.Visible = false;
        Label16.Visible = true;
        Label16.Text = "Record Updated successfully ";
        fillgrid();
    }
    protected void Btncancel_Click(object sender, EventArgs e)
    {
    //{
    //    RequiredFieldValidator1.Enabled = false;
    //    RequiredFieldValidator2.Enabled = false;
    //    RequiredFieldValidator3.Enabled = false;
    //    RequiredFieldValidator5.Enabled = false;
    //    RequiredFieldValidator6.Enabled = false;
    //    RequiredFieldValidator1.Enabled = false;
    //    RequiredFieldValidator1.Enabled = false;
    //    RegularExpressionValidator1.Enabled = false;
    //    RegularExpressionValidator2.Enabled = false;
    //    RegularExpressionValidator3.Enabled = false;
        Label16.Visible = false;
        Panel1.Visible = false;
        Panel2.Visible = true;
        Button1.Visible = true;
        
    }
    protected void Button2_Click(object sender, EventArgs e)
    {

    }
    protected void Button1_Click(object sender, EventArgs e)
    {

        Button1.Visible = false;
        Btnsave.Visible = true;
        Btnupdate.Visible = false;
        Panel1.Visible = true;
        TextBox1.Text = "";
        TextBox3.Text = "";
        TextBox4.Text = "";
        TextBox5.Text = "";
        TextBox6.Text = "";
        TextBox7.Text = "";
        TextBox8.Text = "";
        TextBox9.Text = "";
        fillddlpdttype();
        filldddlsales();
        filldddlsub();
        filldddlsubsub();
    }
    protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
    {
        Panel1.Visible = true;
        Btnsave.Visible = false;
        ImageButton lnkbtn = (ImageButton)sender;

        GridViewRow row = (GridViewRow)lnkbtn.NamingContainer;
        int j = Convert.ToInt32(row.RowIndex);

        Label id = (Label)GridView1.Rows[j].FindControl("Label15");
        string data = "select * from ProductMasterTbl where  ProductMasterTbl.ProductMasterID='" + id.Text + "'";

        //string data = "select * from ProductMasterTbl inner join ProductTypeTbl on ProductMasterTbl.ProductTypeID=ProductTypeTbl.ID " +
        //   "  inner join SalesTypeTbl on ProductMasterTbl.SalesTypeID =SalesTypeTbl.ID   " +
        //   " inner join  ProductSubTypeTbl  on    ProductMasterTbl.ProductSubTypeID=ProductSubTypeTbl.ID    " +
        //   "  inner join ProductSubSubTypeTbl on     ProductMasterTbl.ProductSubSubTypeID=  ProductSubSubTypeTbl.ID " +
        //   " where  ProductMasterTbl.ProductMasterID='" + id.Text + "'";


        SqlCommand cmd = new SqlCommand(data,con);
        SqlDataAdapter sda = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        sda.Fill(dt);
        if (dt.Rows.Count > 0)
        {
          //  me,ProductStatus,ProductStartDate,ProductRetiredate,ProductTypeID,SalesTypeID,ProductSubTypeID,ProductSubSubTypeID,ProductStockQty) values ('" + TextBox1.Text + "','" + DropDownList1.SelectedItem.Text + "','" + TextBox4.Text + "','" + TextBox3.Text + "','" + DropDownList2.SelectedValue + "','" + DropDownList3.SelectedValue + "','" + DropDownList4.SelectedValue + "','" + DropDownList5.SelectedValue + "','" + TextBox5.Text + "')";
            TextBox1.Text = dt.Rows[0]["ProductName"].ToString();

            string str = dt.Rows[0]["ProductStatus"].ToString();
            
            if (str == "Active")
            {
                DropDownList1.SelectedIndex = 0;
               

            }
            if (str == "Inactive")
            {
                DropDownList1.SelectedIndex = 1;
                
            }
          
            //DropDownList1.SelectedValue = dt.Rows[0]["ProductStatus"].ToString();
            TextBox3.Text = dt.Rows[0]["ProductRetiredate"].ToString();
            TextBox4.Text = dt.Rows[0]["ProductStartDate"].ToString();
            DropDownList2.SelectedValue = dt.Rows[0]["ProductTypeID"].ToString();
            
            DropDownList4.SelectedValue = dt.Rows[0]["ProductSubTypeID"].ToString();
            DropDownList5.SelectedValue = dt.Rows[0]["ProductSubSubTypeID"].ToString();
            TextBox5.Text = dt.Rows[0]["ProductStockQty"].ToString();
            DropDownList3.SelectedValue = dt.Rows[0]["SalesTypeID"].ToString();
            Btnupdate.Visible = true;
            ViewState["id"] = dt.Rows[0]["ProductMasterID"].ToString();
            if( ViewState["id"]!="")
            {
                {
                    string data77 = "select * from ProductPictureTbl where ProductMasterTbl='" + ViewState["id"].ToString() + "'";
                    SqlCommand cmd77 = new SqlCommand(data77, con);
                    SqlDataAdapter sda77 = new SqlDataAdapter(cmd77);
                    DataTable dt77 = new DataTable();
                    sda77.Fill(dt77);
                    if (dt77.Rows.Count > 0)
                    {
                        Image11235.Visible = true;
                        Image11236.Visible = true;
                        TextBox6.Text = dt77.Rows[0]["PictureTitle"].ToString();
                        Image11235.ImageUrl = "~/images/" + dt77.Rows[0]["SmallImageName"].ToString();
                        Image11236.ImageUrl = "~/images/" + dt77.Rows[0]["LargeImageName"].ToString();

                    }
                }
                {
                    string data777 = "select * from ProductPriceTbl where ProductMasterID='" + ViewState["id"].ToString() + "'";
                    SqlCommand cmd777 = new SqlCommand(data777, con);
                    SqlDataAdapter sda777 = new SqlDataAdapter(cmd777);
                    DataTable dt777 = new DataTable();
                    sda777.Fill(dt777);
                    if (dt777.Rows.Count > 0)
                    {
                        TextBox7.Text = dt777.Rows[0]["ListPrice"].ToString();
                        TextBox8.Text = dt777.Rows[0]["SalesPrice"].ToString();
                        TextBox9.Text = dt777.Rows[0]["EffectiveDate"].ToString();
                    }
                }

            }
            

        
        
        }


    }
    protected void imgdelete_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton lnkbtn = (ImageButton)sender;

        GridViewRow row = (GridViewRow)lnkbtn.NamingContainer;
        int j = Convert.ToInt32(row.RowIndex);

        Label id = (Label)GridView1.Rows[j].FindControl("Label15");
        string finalstr = "Select * from ProductMasterTbl where ProductMasterID='" + id.Text + "'";
        SqlCommand cmdcln = new SqlCommand(finalstr, con);
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        DataTable dtcln = new DataTable();
        adpcln.Fill(dtcln);
        if (dtcln.Rows.Count > 0)
        {
            string finalstr1 = "delete from  ProductMasterTbl where ProductMasterID='" + id.Text + "'";
            SqlCommand cmdcln1 = new SqlCommand(finalstr1, con);
            string data55 = "delete from ProductPictureTbl where ProductMasterTbl='" + id.Text + "'";
            SqlCommand cmd555 = new SqlCommand(data55, con);
            string data555 = "delete from ProductPriceTbl where ProductMasterID='" + id.Text + "'";
            SqlCommand cmd5555 = new SqlCommand(data555, con);
            con.Open();
            cmd555.ExecuteNonQuery();
            cmd5555.ExecuteNonQuery();
            cmdcln1.ExecuteNonQuery();
            con.Close();

        }
        Label16.Visible = true;
        Label16.Text = "Record Deleted Sucessfully";
        fillgrid();
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
    protected void Button2_Click1(object sender, EventArgs e)
    {
        if (FileUpload1.HasFile)
        {
            bool valid = ext111(FileUpload1.FileName);

            if (valid == true)
            {
                FileUpload1.PostedFile.SaveAs(Server.MapPath("~\\images\\") + FileUpload1.FileName);

                Image11235.Visible = true;
                Image11235.ImageUrl = "~/images/" + FileUpload1.FileName.ToString();
                ViewState["url"] = FileUpload1.FileName.ToString();
               Label26.Text = FileUpload1.FileName.ToString();

            }

            else
            {
                Label16.Visible = true;
                Label16.Text = "Invalid File Type. Please upload an image file in one of the following formats: bmp, gif, png, jpg, jpeg, ipg.";
            }

        }

        if (FileUpload2.HasFile)
        {
            bool valid = ext111(FileUpload2.FileName);

            if (valid == true)
            {
                FileUpload2.PostedFile.SaveAs(Server.MapPath("~\\images\\") + FileUpload2.FileName);
                Image11236.Visible = true;
                Image11236.ImageUrl = "~/images/" + FileUpload2.FileName.ToString();
                ViewState["url"] = FileUpload2.FileName.ToString();
                Label25.Text = FileUpload2.FileName.ToString();

            }

            else
            {
                Label16.Visible = true;
                Label16.Text = "Invalid File Type. Please upload an image file in one of the following formats: bmp, gif, png, jpg, jpeg, ipg.";
            }

        }
    }
    protected void Button3_Click(object sender, EventArgs e)
    {

    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        fillgrid();

    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{

        //    Label lbl = (e.Row.FindControl("Label2") as Label);
        //    if (lbl.Text == "0")
        //    {
        //        lbl.Text = "Active";

        //    }
        //    else {
        //        lbl.Text = "Inactive";
        //    }


        //}
    }
    protected void GridView1_DataBound(object sender, EventArgs e)
    {
      
    }
}