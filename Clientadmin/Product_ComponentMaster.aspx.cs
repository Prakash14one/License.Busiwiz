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

public partial class ProductComponentMaster : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);
    SqlConnection iofficecon = new SqlConnection();
    protected void Page_Load(object sender, EventArgs e)
    {
        PageConn pgcon = new PageConn();
        iofficecon = pgcon.dynconn;
        if (!IsPostBack)
        {
            suppliername();
            brandname();
            //getmaxvalue();
            fillgrid();
            Panel1.Visible = false;
            Panel2.Visible = true;
        }
    }
    public void getmaxvalue()
    {
        con.Open();
        SqlCommand cmd = new SqlCommand("select max(ID) from ProductComponentMasterTbl", con);
        TextBox27.Text = cmd.ExecuteScalar().ToString();
        con.Close();
        con.Open();
        SqlCommand cmd1 = new SqlCommand("select max(ID) from ProductComponentDetailTbl", con);
        TextBox29.Text = cmd1.ExecuteScalar().ToString();
        con.Close();
    }
    public void suppliername()
    {
        string finalstr = "Select * from SupplierMasterTbl ";
        finalstr = " SELECT DISTINCT dbo.Party_master.Compname ,dbo.Party_master.Compname + ':'+ dbo.Party_master.Contactperson as CompanyName, dbo.PartyTypeCategoryMasterTbl.PartyCategoryName, dbo.PartytTypeMaster.PartType, dbo.Party_master.PartyID FROM dbo.Party_master INNER JOIN dbo.PartytTypeMaster ON dbo.Party_master.PartyTypeId = dbo.PartytTypeMaster.PartyTypeId INNER JOIN dbo.PartyTypeCategoryMasterTbl ON dbo.PartytTypeMaster.PartyCategoryId = dbo.PartyTypeCategoryMasterTbl.PartyTypeCategoryMasterId WHERE        (dbo.PartytTypeMaster.PartType = 'Vendor')  and dbo.PartyTypeCategoryMasterTbl.Active='1'  Order By dbo.Party_master.Compname ";
        SqlCommand cmdcln = new SqlCommand(finalstr, iofficecon);
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        DataTable dtcln = new DataTable();
        adpcln.Fill(dtcln);
        DropDownList2.DataSource = dtcln;
        DropDownList2.DataValueField = "PartyID";
        DropDownList2.DataTextField = "CompanyName";
        DropDownList2.DataBind();
        DropDownList2.Items.Insert(0, "All");
        DropDownList2.Items[0].Value = "0";
        DropDownList2.DataBind();
    }
    public void brandname()
    {
        string finalstr = "Select * from BrandMasterTbl ";
        SqlCommand cmdcln = new SqlCommand(finalstr, con);
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        DataTable dtcln = new DataTable();
        adpcln.Fill(dtcln);
        DropDownList4.DataSource = dtcln;
        DropDownList4.DataValueField = "ID";
        DropDownList4.DataTextField = "Name";
        DropDownList4.DataBind();
        DropDownList4.Items.Insert(0, "All");
        DropDownList4.Items[0].Value = "0";
        DropDownList4.DataBind();
 
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

    public void productmaster()
    {
        con.Open();
        string finalstr = "insert into ProductComponentMasterTbl(ComponentName,ImageFileName,Active)values('" + TextBox28.Text + "','" + Label66.Text + "','" + chkboxActiveDeactive.Checked + "')";
        SqlCommand cmdcln = new SqlCommand(finalstr, con);
        
        cmdcln.ExecuteNonQuery();
        string data9 = "select MAX(ID) as id1 from  ProductComponentMasterTbl";
        SqlCommand cmd9 = new SqlCommand(data9,con);
        SqlDataAdapter sda9 = new SqlDataAdapter(cmd9);
        DataTable dt9 = new DataTable();
        sda9.Fill(dt9);
        if(dt9.Rows.Count>0)
        {
        ViewState["id1"]=dt9.Rows[0]["id1"].ToString();
        }

        con.Close();
    }
    public void productdetail()
    {
        string finalstr1 = "insert into ProductComponentDetailTbl(ProductComponentMasterTblID,BrandID,Specification,Size,ModelNumber,VendorProductPageURL,ImageSmallFront,ImageSMallBack,ImageSmallTOP,ImageSmallBottom,ImageSmallRight,ImageSmallLeft,VideoURL,ImageBiglFront,ImageBiglBack,ImageBigTOP,ImageBigBottom,ImageBigRight,ImageSBigLeft)values('" + ViewState["id1"] + "','" + DropDownList4.SelectedValue + "','" + TextBox9.Text + "','" + TextBox10.Text + "','" + TextBox11.Text + "','" + TextBox12.Text + "','" + Label54.Text + "','" + Label55.Text + "','" + Label56.Text + "','" + Label57.Text + "','" + Label58.Text + "','" + Label59.Text + "','" + TextBox19.Text + "','" + Label60.Text + "','" + Label61.Text + "','" + Label62.Text + "','" + Label63.Text + "','" + Label64.Text + "','" + Label65.Text + "')";
        SqlCommand cmdcln1 = new SqlCommand(finalstr1, con);
        con.Open();
        cmdcln1.ExecuteNonQuery();

        string data10 = "select MAX(ID) as id2 from  ProductComponentDetailTbl";
        SqlCommand cmd10 = new SqlCommand(data10, con);
        SqlDataAdapter sda10 = new SqlDataAdapter(cmd10);
        DataTable dt10 = new DataTable();
        sda10.Fill(dt10);
        if (dt10.Rows.Count > 0)
        {
            ViewState["id2"] = dt10.Rows[0]["id2"].ToString();
        }
        con.Close();
    }
    public void productdetailprice()
    {

        string finalstr2 = "insert into ProductComponentDetailPriceTbl(ProductComponentDetailTblID,BatchNumber,CostPrice,VolumeDiscoutnAvailablePercent,VolumeDiscoutnAvailableAmount,EffectveStartDate,EffectiveEndDate,SupplierID,SellPrice,MarkUpPercentageovereffectivecost)values('" + ViewState["id2"] + "','" + TextBox1.Text + "','" + TextBox2.Text + "','" + TextBox3.Text + "','" + TextBox4.Text + "','" + TextBox5.Text + "','" + TextBox6.Text + "','" + DropDownList2.SelectedValue + "','" + TextBox7.Text + "','" + TextBox8.Text + "')";
        SqlCommand cmdcln2 = new SqlCommand(finalstr2, con);
        con.Open();
        cmdcln2.ExecuteNonQuery();
        con.Close();

    }
    protected void Btnsave_Click(object sender, EventArgs e)
    {
        productmaster();
        productdetail();
        productdetailprice();

        fillgrid();
        Label11.Text = "Record Inserted Sucessfully";
        Panel1.Visible = false;
        Panel2.Visible = true;
        Btnupdate.Visible = false;
        //Label11.Text = "";
        //TextBox1.Text = "";
        //TextBox3.Text = "";
        //DropDownList1.SelectedIndex = 0;
        //DropDownList2.SelectedIndex = 0;
        //Panel2.Visible = true;
        //Image1.ImageUrl = "~\\images\\download.png";
        //Label15.Text = "download.png";
    }

    protected void imgsmallfront_Click(object sender, EventArgs e)
    {
        bool valid = ext111(FileUpload13.FileName);

        if (valid == true)
        {

            if (FileUpload13.HasFile == true)
            {
                if (Directory.Exists(Server.MapPath("~\\images\\")) == false)
                {
                    Directory.CreateDirectory(Server.MapPath("~\\images\\"));
                }
                FileUpload13.PostedFile.SaveAs(Server.MapPath("~\\images\\") + FileUpload13.FileName);
                Image1.ImageUrl = "~\\images\\" + FileUpload13.FileName;
                Label54.Text = FileUpload13.FileName;

            }
        }
    }

    protected void imgsmallback_Click(object sender, EventArgs e)
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
                Image2.ImageUrl = "~\\images\\" + FileUpload1.FileName;
                Label55.Text = FileUpload1.FileName;

            }
        }
    }

    protected void imgsmalltop_Click(object sender, EventArgs e)
    {
        bool valid = ext111(FileUpload14.FileName);

        if (valid == true)
        {

            if (FileUpload14.HasFile == true)
            {
                if (Directory.Exists(Server.MapPath("~\\images\\")) == false)
                {
                    Directory.CreateDirectory(Server.MapPath("~\\images\\"));
                }
                FileUpload14.PostedFile.SaveAs(Server.MapPath("~\\images\\") + FileUpload14.FileName);
                Image3.ImageUrl = "~\\images\\" + FileUpload14.FileName;
                Label56.Text = FileUpload14.FileName;

            }
        }
    }
    protected void imgsmallbottom_Click(object sender, EventArgs e)
    {
        bool valid = ext111(FileUpload15.FileName);

        if (valid == true)
        {

            if (FileUpload15.HasFile == true)
            {
                if (Directory.Exists(Server.MapPath("~\\images\\")) == false)
                {
                    Directory.CreateDirectory(Server.MapPath("~\\images\\"));
                }
                FileUpload15.PostedFile.SaveAs(Server.MapPath("~\\images\\") + FileUpload15.FileName);
                Image4.ImageUrl = "~\\images\\" + FileUpload15.FileName;
                Label57.Text = FileUpload15.FileName;

            }
        }
    }
    protected void imgsmallright_Click(object sender, EventArgs e)
    {
        bool valid = ext111(FileUpload16.FileName);

        if (valid == true)
        {

            if (FileUpload16.HasFile == true)
            {
                if (Directory.Exists(Server.MapPath("~\\images\\")) == false)
                {
                    Directory.CreateDirectory(Server.MapPath("~\\images\\"));
                }
                FileUpload16.PostedFile.SaveAs(Server.MapPath("~\\images\\") + FileUpload16.FileName);
                Image5.ImageUrl = "~\\images\\" + FileUpload16.FileName;
                Label58.Text = FileUpload16.FileName;

            }
        }
    }
    protected void imgsmallleft_Click(object sender, EventArgs e)
    {
        bool valid = ext111(FileUpload17.FileName);

        if (valid == true)
        {

            if (FileUpload17.HasFile == true)
            {
                if (Directory.Exists(Server.MapPath("~\\images\\")) == false)
                {
                    Directory.CreateDirectory(Server.MapPath("~\\images\\"));
                }
                FileUpload17.PostedFile.SaveAs(Server.MapPath("~\\images\\") + FileUpload17.FileName);
                Image6.ImageUrl = "~\\images\\" + FileUpload17.FileName;
                Label59.Text = FileUpload17.FileName;

            }
        }
    }
    protected void imgbigfront_Click(object sender, EventArgs e)
    {
        bool valid = ext111(FileUpload18.FileName);

        if (valid == true)
        {

            if (FileUpload18.HasFile == true)
            {
                if (Directory.Exists(Server.MapPath("~\\images\\")) == false)
                {
                    Directory.CreateDirectory(Server.MapPath("~\\images\\"));
                }
                FileUpload18.PostedFile.SaveAs(Server.MapPath("~\\images\\") + FileUpload18.FileName);
                Image7.ImageUrl = "~\\images\\" + FileUpload18.FileName;
                Label60.Text = FileUpload18.FileName;

            }
        }
    }
    protected void imgbigback_Click(object sender, EventArgs e)
    {
        bool valid = ext111(FileUpload19.FileName);

        if (valid == true)
        {

            if (FileUpload19.HasFile == true)
            {
                if (Directory.Exists(Server.MapPath("~\\images\\")) == false)
                {
                    Directory.CreateDirectory(Server.MapPath("~\\images\\"));
                }
                FileUpload19.PostedFile.SaveAs(Server.MapPath("~\\images\\") + FileUpload19.FileName);
                Image8.ImageUrl = "~\\images\\" + FileUpload19.FileName;
                Label61.Text = FileUpload19.FileName;

            }
        }
    }
    protected void imgbigtop_Click(object sender, EventArgs e)
    {
        bool valid = ext111(FileUpload20.FileName);

        if (valid == true)
        {

            if (FileUpload20.HasFile == true)
            {
                if (Directory.Exists(Server.MapPath("~\\images\\")) == false)
                {
                    Directory.CreateDirectory(Server.MapPath("~\\images\\"));
                }
                FileUpload20.PostedFile.SaveAs(Server.MapPath("~\\images\\") + FileUpload20.FileName);
                Image9.ImageUrl = "~\\images\\" + FileUpload20.FileName;
                Label62.Text = FileUpload20.FileName;

            }
        }
    }
    protected void imgbigbottom_Click(object sender, EventArgs e)
    {
        bool valid = ext111(FileUpload21.FileName);

        if (valid == true)
        {

            if (FileUpload21.HasFile == true)
            {
                if (Directory.Exists(Server.MapPath("~\\images\\")) == false)
                {
                    Directory.CreateDirectory(Server.MapPath("~\\images\\"));
                }
                FileUpload21.PostedFile.SaveAs(Server.MapPath("~\\images\\") + FileUpload21.FileName);
                Image10.ImageUrl = "~\\images\\" + FileUpload21.FileName;
                Label63.Text = FileUpload21.FileName;

            }
        }
    }
    protected void imgbigright_Click(object sender, EventArgs e)
    {
        bool valid = ext111(FileUpload22.FileName);

        if (valid == true)
        {

            if (FileUpload22.HasFile == true)
            {
                if (Directory.Exists(Server.MapPath("~\\images\\")) == false)
                {
                    Directory.CreateDirectory(Server.MapPath("~\\images\\"));
                }
                FileUpload22.PostedFile.SaveAs(Server.MapPath("~\\images\\") + FileUpload22.FileName);
                Image11.ImageUrl = "~\\images\\" + FileUpload22.FileName;
                Label64.Text = FileUpload22.FileName;

            }
        }
    }
    protected void imgbigleft_Click(object sender, EventArgs e)
    {
        bool valid = ext111(FileUpload23.FileName);

        if (valid == true)
        {

            if (FileUpload23.HasFile == true)
            {
                if (Directory.Exists(Server.MapPath("~\\images\\")) == false)
                {
                    Directory.CreateDirectory(Server.MapPath("~\\images\\"));
                }
                FileUpload23.PostedFile.SaveAs(Server.MapPath("~\\images\\") + FileUpload23.FileName);
                Image12.ImageUrl = "~\\images\\" + FileUpload23.FileName;
                Label65.Text = FileUpload23.FileName;

            }
        }
    }
    protected void imgfilename_Click(object sender, EventArgs e)
    {
        bool valid = ext111(FileUpload25.FileName);

        if (valid == true)
        {

            if (FileUpload25.HasFile == true)
            {
                if (Directory.Exists(Server.MapPath("~\\images\\")) == false)
                {
                    Directory.CreateDirectory(Server.MapPath("~\\images\\"));
                }
                FileUpload25.PostedFile.SaveAs(Server.MapPath("~\\images\\") + FileUpload25.FileName);
                Image13.ImageUrl = "~\\images\\" + FileUpload25.FileName;
                Label66.Text = FileUpload25.FileName;

            }
        }
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        //fillgrid();
    }
    void fillgrid()
    {
        string fstr ="select * from ProductComponentMasterTbl inner join ProductComponentDetailTbl on ProductComponentDetailTbl.ProductComponentMasterTblID=ProductComponentMasterTbl.ID inner join ProductComponentDetailPriceTbl on ProductComponentDetailPriceTbl.ProductComponentDetailTblID=ProductComponentDetailTbl.ID ";
        SqlCommand cmd7 = new SqlCommand(fstr, con);
        SqlDataAdapter sda7 = new SqlDataAdapter(cmd7);
        DataTable dt7 = new DataTable();
        sda7.Fill(dt7);       
            GridView1.DataSource = dt7;
        GridView1.DataBind();
       


    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Panel1.Visible = true;
        Btnsave.Visible = true;
        Btnupdate.Visible = false;
        Label11.Text = "";
        TextBox1.Text = "";
        TextBox2.Text = "";
        TextBox3.Text = "";
        TextBox4.Text = "";
        TextBox5.Text = "";
        TextBox6.Text = "";
        TextBox7.Text = "";
        TextBox8.Text = "";
        TextBox27.Text = "";
        TextBox9.Text = "";
        TextBox10.Text = "";
        TextBox11.Text = "";
        TextBox12.Text = "";
        TextBox19.Text = "";
        TextBox28.Text = "";
        TextBox29.Text = "";
        DropDownList4.SelectedIndex = 0;
        chkboxActiveDeactive.Checked = false; 
        DropDownList2.SelectedIndex = 0;
        Panel2.Visible = true;
        Image1.ImageUrl = "~\\images\\download.png";
        Label54.Text = "download.png";
        Image2.ImageUrl = "~\\images\\download.png";
        Label55.Text = "download.png";
        Image3.ImageUrl = "~\\images\\download.png";
        Label56.Text = "download.png";
        Image4.ImageUrl = "~\\images\\download.png";
        Label57.Text = "download.png";
        Image5.ImageUrl = "~\\images\\download.png";
        Label58.Text = "download.png";
        Image6.ImageUrl = "~\\images\\download.png";
        Label59.Text = "download.png";
        Image7.ImageUrl = "~\\images\\download.png";
        Label60.Text = "download.png";
        Image8.ImageUrl = "~\\images\\download.png";
        Label61.Text = "download.png";
        Image9.ImageUrl = "~\\images\\download.png";
        Label62.Text = "download.png";
        Image10.ImageUrl = "~\\images\\download.png";
        Label63.Text = "download.png";
        Image11.ImageUrl = "~\\images\\download.png";
        Label64.Text = "download.png";
        Image12.ImageUrl = "~\\images\\download.png";
        Label65.Text = "download.png";
        Image13.ImageUrl = "~\\images\\download.png";
        Label66.Text = "download.png";
    }
    protected void Btncancel_Click(object sender, EventArgs e)
    {
        Panel1.Visible = false;
        Panel2.Visible = true;
    }
    protected void imgdelete_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton lnkbtn = (ImageButton)sender;

        GridViewRow row = (GridViewRow)lnkbtn.NamingContainer;
        int j = Convert.ToInt32(row.RowIndex);

        Label id = (Label)GridView1.Rows[j].FindControl("Label15");

        ViewState["id1"]=id.Text;
        string finalstr = "Select * from ProductComponentMasterTbl  where ID='" + id.Text + "'";
        SqlCommand cmdcln = new SqlCommand(finalstr, con);
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        DataTable dtcln = new DataTable();
        adpcln.Fill(dtcln);
        if (dtcln.Rows.Count > 0)
        {
            string id00=dtcln.Rows[0]["ID"].ToString();
            ViewState["id2"] = id00;
            string data99 = "select * from ProductComponentDetailTbl where ProductComponentMasterTblID='"+id00+"'";
            SqlCommand cm00 = new SqlCommand(data99, con);
            SqlDataAdapter sda00 = new SqlDataAdapter(cm00);
            DataTable dt099 = new DataTable();
            sda00.Fill(dt099);
            if (dt099.Rows.Count > 0)
            {
                string mm = dt099.Rows[0]["ID"].ToString();
                ViewState["id3"] = mm;
                string str77 = "delete   from ProductComponentDetailPriceTbl where ProductComponentDetailTblID='"+mm+"'";
                SqlCommand dinu = new SqlCommand(str77,con);
                con.Open();
                dinu.ExecuteNonQuery();
                con.Close();
                
            }
            string str771 = "delete   from ProductComponentDetailTbl where ProductComponentMasterTblID='" + ViewState["id2"] + "'";
            SqlCommand dinu1 = new SqlCommand(str771, con);

            con.Open();
            dinu1.ExecuteNonQuery();
            con.Close();

        }
        string str7711 = "delete   from ProductComponentMasterTbl   where ID='" + ViewState["id1"] + "'";
        SqlCommand dinu11 = new SqlCommand(str7711, con);

        con.Open();
        dinu11.ExecuteNonQuery();
        con.Close();
        Label11.Visible = true;
        Label11.Text = "Record Deleted Sucessfully";
        fillgrid();
    
    }
    protected void Btnupdate_Click(object sender, EventArgs e)
    {
        con.Open();
        string finalstr = "update ProductComponentMasterTbl set ComponentName='" + TextBox28.Text + "',ImageFileName='" + Label66.Text + "',Active='" + chkboxActiveDeactive.Checked + "' where ID='" + ViewState["id"].ToString() + "'";
        SqlCommand cmdcln = new SqlCommand(finalstr, con);

        cmdcln.ExecuteNonQuery();
        string data9 = "select MAX(ID) as id1 from  ProductComponentMasterTbl";
        SqlCommand cmd9 = new SqlCommand(data9, con);
        SqlDataAdapter sda9 = new SqlDataAdapter(cmd9);
        DataTable dt9 = new DataTable();
        sda9.Fill(dt9);
        if (dt9.Rows.Count > 0)
        {
            ViewState["id1"] = dt9.Rows[0]["id1"].ToString();
        }

        con.Close();

        string finalstr1 = "update ProductComponentDetailTbl set BrandID='" + DropDownList4.SelectedValue + "',Specification='" + TextBox9.Text + "',Size='" + TextBox10.Text + "',ModelNumber='" + TextBox11.Text + "',VendorProductPageURL='" + TextBox12.Text + "',ImageSmallFront='" + Label54.Text + "',ImageSMallBack='" + Label55.Text + "',ImageSmallTOP='" + Label56.Text + "',ImageSmallBottom='" + Label57.Text + "',ImageSmallRight='" + Label58.Text + "',ImageSmallLeft='" + Label59.Text + "',VideoURL='" + TextBox19.Text + "',ImageBiglFront='" + Label60.Text + "',ImageBiglBack='" + Label61.Text + "',ImageBigTOP='" + Label62.Text + "',ImageBigBottom='" + Label63.Text + "',ImageBigRight='" + Label64.Text + "',ImageSBigLeft='" + Label65.Text + "' where ProductComponentMasterTblID='" + ViewState["id1"].ToString() + "'";
        SqlCommand cmdcln1 = new SqlCommand(finalstr1, con);
        con.Open();
        cmdcln1.ExecuteNonQuery();

        string data10 = "select MAX(ID) as id2 from  ProductComponentDetailTbl";
        SqlCommand cmd10 = new SqlCommand(data10, con);
        SqlDataAdapter sda10 = new SqlDataAdapter(cmd10);
        DataTable dt10 = new DataTable();
        sda10.Fill(dt10);
        if (dt10.Rows.Count > 0)
        {
            ViewState["id2"] = dt10.Rows[0]["id2"].ToString();
        }
        con.Close();

        string finalstr2 = "update ProductComponentDetailPriceTbl set BatchNumber='" + TextBox1.Text + "',CostPrice='" + TextBox2.Text + "',VolumeDiscoutnAvailablePercent='" + TextBox3.Text + "',VolumeDiscoutnAvailableAmount='" + TextBox4.Text + "',EffectveStartDate='" + TextBox5.Text + "',EffectiveEndDate='" + TextBox6.Text + "',SupplierID='" + DropDownList2.SelectedValue + "',SellPrice='" + TextBox7.Text + "',MarkUpPercentageovereffectivecost='" + TextBox8.Text + "' where ProductComponentDetailTblID='" + ViewState["id2"].ToString() + "'";
        SqlCommand cmdcln2 = new SqlCommand(finalstr2, con);
        con.Open();
        cmdcln2.ExecuteNonQuery();
        con.Close();
        Label11.Text = "Record updated Sucessfully";
        Panel1.Visible = false;
        Panel2.Visible = true;
        fillgrid();

    }
    protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
    {

        ImageButton lnkbtn = (ImageButton)sender;

        GridViewRow row = (GridViewRow)lnkbtn.NamingContainer;
        int j = Convert.ToInt32(row.RowIndex);

        Label id = (Label)GridView1.Rows[j].FindControl("Label15");
        ViewState["id"] = id.Text;
        string finalstr = "Select * from  ProductComponentMasterTbl where ID='" + id.Text + "'";
        SqlCommand cmdcln = new SqlCommand(finalstr, con);
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        DataTable dtcln = new DataTable();
        adpcln.Fill(dtcln);
        if (dtcln.Rows.Count > 0)
        {
            TextBox28.Text = dtcln.Rows[0]["ComponentName"].ToString();
            Image13.ImageUrl = "~\\images\\" + dtcln.Rows[0]["ImageFileName"].ToString();
            Label66.Text = dtcln.Rows[0]["ImageFileName"].ToString();         

            try
            {
                chkboxActiveDeactive.Checked = Convert.ToBoolean(dtcln.Rows[0]["Active"].ToString());
            }
            catch (Exception ex)
            {
            }
            string id00 = dtcln.Rows[0]["ID"].ToString();

            ViewState["id2"] = id00;
            string data99 = "select * from ProductComponentDetailTbl where ProductComponentMasterTblID='" + id00 + "'";
            SqlCommand cm00 = new SqlCommand(data99, con);
            SqlDataAdapter sda00 = new SqlDataAdapter(cm00);
            DataTable dt099 = new DataTable();
            sda00.Fill(dt099);
            if (dt099.Rows.Count > 0)
            {
                DropDownList4.SelectedValue = dt099.Rows[0]["BrandID"].ToString();
                TextBox9.Text = dt099.Rows[0]["Specification"].ToString();
                TextBox10.Text = dt099.Rows[0]["Size"].ToString();
                TextBox11.Text = dt099.Rows[0]["ModelNumber"].ToString();
                TextBox12.Text = dt099.Rows[0]["VendorProductPageURL"].ToString();
                Image1.ImageUrl = "~\\images\\" + dt099.Rows[0]["ImageSmallFront"].ToString();
                Label54.Text = dt099.Rows[0]["ImageSmallFront"].ToString();
                Image2.ImageUrl = "~\\images\\" + dt099.Rows[0]["ImageSMallBack"].ToString();
                Label55.Text = dt099.Rows[0]["ImageSMallBack"].ToString();
                Image3.ImageUrl = "~\\images\\" + dt099.Rows[0]["ImageSmallTOP"].ToString();
                Label56.Text = dt099.Rows[0]["ImageSmallTOP"].ToString();
                Image4.ImageUrl = "~\\images\\" + dt099.Rows[0]["ImageSmallBottom"].ToString();
                Label57.Text = dt099.Rows[0]["ImageSmallBottom"].ToString();
                Image5.ImageUrl = "~\\images\\" + dt099.Rows[0]["ImageSmallRight"].ToString();
                Label58.Text = dt099.Rows[0]["ImageSmallRight"].ToString();
                Image6.ImageUrl = "~\\images\\" + dt099.Rows[0]["ImageSmallLeft"].ToString();
                Label59.Text = dt099.Rows[0]["ImageSmallLeft"].ToString();
                TextBox19.Text = dt099.Rows[0]["VideoURL"].ToString();
                Image7.ImageUrl = "~\\images\\" + dt099.Rows[0]["ImageBiglFront"].ToString();
                Label60.Text = dt099.Rows[0]["ImageBiglFront"].ToString();
                Image8.ImageUrl = "~\\images\\" + dt099.Rows[0]["ImageBiglBack"].ToString();
                Label61.Text = dt099.Rows[0]["ImageBiglBack"].ToString();
                Image9.ImageUrl = "~\\images\\" + dt099.Rows[0]["ImageBigTOP"].ToString();
                Label62.Text = dt099.Rows[0]["ImageBigTOP"].ToString();
                Image10.ImageUrl = "~\\images\\" + dt099.Rows[0]["ImageBigBottom"].ToString();
                Label63.Text = dt099.Rows[0]["ImageBigBottom"].ToString();
                Image11.ImageUrl = "~\\images\\" + dt099.Rows[0]["ImageBigRight"].ToString();
                Label64.Text = dt099.Rows[0]["ImageBigRight"].ToString();
                Image12.ImageUrl = "~\\images\\" + dt099.Rows[0]["ImageSBigLeft"].ToString();
                Label65.Text = dt099.Rows[0]["ImageSBigLeft"].ToString();
                string mm = dt099.Rows[0]["ID"].ToString();
                ViewState["id3"] = mm;
                string str77 = "select *   from ProductComponentDetailPriceTbl where ProductComponentDetailTblID='" + mm + "'";
                SqlCommand dinu = new SqlCommand(str77, con);
                SqlDataAdapter sda0909 = new SqlDataAdapter(dinu);
                DataTable dt676 = new DataTable();
                sda0909.Fill(dt676);
                if (dt676.Rows.Count > 0)
                {
                    TextBox1.Text = dt676.Rows[0]["BatchNumber"].ToString();
                    TextBox2.Text = dt676.Rows[0]["CostPrice"].ToString();
                    TextBox3.Text = dt676.Rows[0]["VolumeDiscoutnAvailablePercent"].ToString();
                    TextBox4.Text = dt676.Rows[0]["VolumeDiscoutnAvailableAmount"].ToString();
                    TextBox5.Text = dt676.Rows[0]["EffectveStartDate"].ToString();
                    TextBox6.Text = dt676.Rows[0]["EffectiveEndDate"].ToString();
                    DropDownList2.SelectedValue = dt676.Rows[0]["SupplierID"].ToString();
                    TextBox7.Text = dt676.Rows[0]["SellPrice"].ToString();
                    TextBox8.Text = dt676.Rows[0]["MarkUpPercentageovereffectivecost"].ToString();
                    Panel1.Visible = true;
                    Btnsave.Visible = false;
                    Btnupdate.Visible = true;


                }
                

            }


        }

    }
}