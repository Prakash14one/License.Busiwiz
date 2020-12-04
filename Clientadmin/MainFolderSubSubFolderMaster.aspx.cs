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

public partial class MainFolderSubSubFolderMaster : System.Web.UI.Page
{
    string comp;

    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        comp = Session["ClientId"].ToString();
        statuslable.Visible = false;
        if (!IsPostBack)
        {
           

            ViewState["sortOrder"] = "";

            
            fillgrid();
            FillProduct1();
            FillProduct2();
            FillProduct3();
            Button8.Visible = false;
           
        

        }
    }

    

    public void fillgrid()
    {

        string str = "";
       
        if (DropDownList4.SelectedIndex > 0)
        {
            str += " and FolderSubSubCategory.ProductName  = '" + DropDownList4.SelectedValue + "'"; 
        }
        if (ddlSubcat.SelectedIndex > 0)
        {
            str += " and FolderSubCatName.FolderSubId  = '" + ddlSubcat.SelectedValue + "'";
        }
        
            str += " and FolderSubSubCategory.Activestatus = '" + ddlActive.SelectedValue + "'";
        

       
         string select = "select distinct FolderSubSubCategory.FolderSubSubId, FolderCategoryMaster1.FolderCatName ,FolderSubCatName.FolderSubName,FolderSubSubCategory.FolderSubSubName ," +
                       " case  when(FolderSubSubCategory.Activestatus=1 ) then 'Active' else 'Inactive'end  as Active,ProductMaster.ProductName + ':' +  VersionInfoMaster.VersionInfoName as productversion" +
                       " from FolderSubSubCategory " +
                       " inner join FolderSubCatName on FolderSubCatName.FolderSubId=FolderSubSubCategory.FolderSubId " +
                       " inner join  FolderCategoryMaster1 on FolderCategoryMaster1.FolderMasterId=FolderSubCatName.FolderMasterId" +
                       " inner join VersionInfoMaster on FolderSubSubCategory.ProductName=VersionInfoMaster.VersionInfoId  " +
                       " inner join ProductMaster on ProductMaster.ProductId=VersionInfoMaster.ProductId " +
                       " inner join ProductDetail on ProductDetail.ProductId=ProductMaster.ProductId " +
                       " where  ProductMaster.ClientMasterId=" + Session["ClientId"] + " " +
                       " and VersionInfoMaster.Active ='True' and ProductDetail.Active='1'  " + str + " ";



        SqlDataAdapter da = new SqlDataAdapter(select, con);
        DataTable dt = new DataTable();
        da.Fill(dt);

        if (dt.Rows.Count > 0)
        {
            GridView1.DataSource = dt;
            GridView1.DataBind();
        }
        else
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
        }
    

    }

  

    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        //string sgw = "select FolderSubSubName from FolderSubSubCategory where " +
        //    " FolderSubSubName='" + txtInventorySubSubName.Text + "' and FolderSubId='" + ddlInventorySubCatID.SelectedValue + "'";
       
        //SqlCommand cgw = new SqlCommand(sgw, con);
        //SqlDataAdapter adgw = new SqlDataAdapter(cgw);
        //DataTable dtgw = new DataTable();
        //adgw.Fill(dtgw);
        //if (dtgw.Rows.Count > 0)
        //{
        //    statuslable.Visible = true;
        //    statuslable.Text = "This record already exists.";
        //}
        //else
        //{

            string qurty = "insert into FolderSubSubCategory(FolderSubSubName,FolderSubId,Activestatus,ProductName)values('" + txtInventorySubSubName.Text + "'," + int.Parse(ddlInventorySubCatID.SelectedValue.ToString()) + ",'" + ddlstatus.SelectedValue + "','"+DropDownList3.SelectedValue+"')";
            SqlCommand mycmd = new SqlCommand(qurty, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();

            }

            mycmd.ExecuteNonQuery();
            con.Close();

            fillgrid();

            statuslable.Visible = true;
            statuslable.Text = "Record inserted successfully";
            txtInventorySubSubName.Text = "";
            ddlInventorySubCatID.SelectedIndex = 0;
            pnladd.Visible = false;
            lbllegend.Visible = false;
            ddlstatus.SelectedIndex = 0;
        }



    


 
  
    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder; // sortOrder;
        fillgrid();

    }
    protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    public string sortOrder
    {
        get
        {
            if (ViewState["sortOrder"].ToString() == "desc")
            {
                ViewState["sortOrder"] = "asc";
            }
            else
            {
                ViewState["sortOrder"] = "desc";
            }

            return ViewState["sortOrder"].ToString();
        }
        set
        {
            ViewState["sortOrder"] = value;
        }
    }


    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        // int currentRowIndex = Int32.Parse(e.CommandArgument.ToString());
       
        if (e.CommandName == "Edit")
        {

            pnladd.Visible = true;
            Button8.Visible = true;
            ImageButton7.Visible = true;
            lbllegend.Visible = true;
            lbllegend.Text = "Edit Sub Sub Category For Your Item";
            btnadd.Visible = false;
            //btnupdate.Visible = true;
            ImageButton2.Visible = false;
            statuslable.Text = "";
            ViewState["editid"] = Convert.ToInt32(e.CommandArgument);



            string str = "select  distinct FolderSubSubCategory.ProductName ,FolderCategoryMaster1.FolderMasterId,FolderCategoryMaster1.FolderCatName,FolderSubCatName.FolderSubId, " +
                        " FolderSubCatName.FolderSubName,FolderSubSubCategory.FolderSubSubId,FolderSubSubCategory.FolderSubSubName,FolderSubSubCategory.Activestatus " +
                        " from   FolderCategoryMaster1 " +
                         " inner join FolderSubCatName on FolderSubCatName.FolderMasterId=FolderCategoryMaster1.FolderMasterId " +
                         " inner join FolderSubSubCategory on FolderSubSubCategory.FolderSubId=FolderSubCatName.FolderSubId" +
                        " where  FolderSubSubCategory.FolderSubSubId="+ViewState["editid"]+"";

            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                FillProduct1();
                DropDownList3.SelectedValue = dt.Rows[0]["productName"].ToString();
                mainfolder();
                DropDownList1.SelectedValue = dt.Rows[0]["FolderMasterId"].ToString();
                subfolder();
                ddlInventorySubCatID.SelectedValue = dt.Rows[0]["FolderSubId"].ToString();
                ddlstatus.SelectedValue = dt.Rows[0]["Activestatus"].ToString();
                txtInventorySubSubName.Text = dt.Rows[0]["FolderSubSubName"].ToString(); 
            }
        }

            if (e.CommandName == "Delete")
            {
               
                ViewState["editid"] = Convert.ToInt32(e.CommandArgument);

                string qurty2 = "delete from FolderSubSubCategory where FolderSubSubCategory.FolderSubSubId = " + ViewState["editid"] + "";
                SqlCommand mycmd = new SqlCommand(qurty2, con);
                if (con.State.ToString()!= "Open")
                {
                    con.Open();

                }

                mycmd.ExecuteNonQuery();
                con.Close();

                fillgrid();

                statuslable.Visible = true;
                statuslable.Text = "Record Deleted successfully";
               

            
        }
        

    }
    protected DataSet FillSubCategory()
    {

        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

        string s = "SELECT InventorySubCatName, InventoryCategoryMasterId, InventorySubCatId FROM InventorySubCategoryMaster";


        SqlCommand c = new SqlCommand(s, con);
        c.CommandType = CommandType.Text;
        SqlDataAdapter d = new SqlDataAdapter(c);
        DataSet ds0 = new DataSet();
        d.Fill(ds0);
        return ds0;

    }

    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        

           
    }
   
    protected void ImageButton3_Click(object sender, ImageClickEventArgs e)
    {
        ModalPopupExtender1.Hide();
    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {

       




    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        GridView1.EditIndex = -1;
        fillgrid();
    }
    protected void ddlSubCat_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
    }
   
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

     
           

    }
    protected void GridView1_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    {

    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        fillgrid();
    }
    protected void ddlInventorySubCatID_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();//Page_Load(sender, e);
        statuslable.Visible = false;
        FillProduct2();
    }
 

    protected void ImgBtnMove_Click(object sender, EventArgs e)
    {
       
      
    }
    
    protected void ImageButton7_Click(object sender, EventArgs e)
    {
        statuslable.Visible = false;
        ddlInventorySubCatID.SelectedIndex = -1;
        txtInventorySubSubName.Text = "";
        statuslable.Visible = false;
        pnladd.Visible = false;
        lbllegend.Visible = false;
        lbllegend.Text = "Add a New Sub Sub Category For Your Item";
        btnadd.Visible = true;

        ImageButton2.Visible = true;
        //btnupdate.Visible = false;
        GridView1.EditIndex = -1;
        fillgrid();

    }
    

    protected void ddlActive_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
        
    }
    protected void Button661_Click(object sender, EventArgs e)
    {
        
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (Button1.Text == "Printable Version")
        {
            

            GridView1.AllowPaging = false;
            GridView1.PageSize = 1000;
            fillgrid();

            Button1.Text = "Hide Printable Version";
            Button7.Visible = true;
            if (GridView1.Columns[4].Visible == true)
            {
                ViewState["editHide"] = "tt";
                GridView1.Columns[4].Visible = false;
            }
            if (GridView1.Columns[5].Visible == true)
            {
                ViewState["deleHide"] = "tt";
                GridView1.Columns[5].Visible = false;
            }
        }
        else
        {

          

            Button1.Text = "Printable Version";
            Button7.Visible = false;

            GridView1.AllowPaging = true;
            GridView1.PageSize = 10;
            fillgrid();

            if (ViewState["editHide"] != null)
            {
                GridView1.Columns[4].Visible = true;
            }
            if (ViewState["deleHide"] != null)
            {
                GridView1.Columns[5].Visible = true;
            }
        }
    }
    protected void btnadd_Click(object sender, EventArgs e)
    {
        
        ////if (pnladd.Visible == false)
        ////{
        FillProduct1();
        mainfolder();
        subfolder();
        txtInventorySubSubName.Text = "";
        statuslable.Text = "";
        Button8.Visible = false;
        pnladd.Visible = true;
    }
   
    protected void LinkButton97666667_Click(object sender, ImageClickEventArgs e)
    {

        string te = "MainFolderSubFolderMaster.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);


    }
    protected void LinkButton13_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void FillProduct1()
    {

        string cmdstr = " SELECT  distinct  VersionInfoMaster.VersionInfoId, ProductMaster.ProductName + ':' +  VersionInfoMaster.VersionInfoName as productversion  FROM ProductMaster  inner join VersionInfoMaster on ProductMaster.ProductId=VersionInfoMaster.ProductId inner join ProductDetail on ProductDetail.ProductId=ProductMaster.ProductId   where ProductMaster.ClientMasterId='" + Session["ClientId"].ToString() + "' and VersionInfoMaster.Active ='True' and ProductDetail.Active='1'  order  by productversion";
        SqlCommand cmdcln = new SqlCommand(cmdstr, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);
        DropDownList3.DataSource = dtcln;

        DropDownList3.DataValueField = "VersionInfoId";
        DropDownList3.DataTextField = "productversion";
        DropDownList3.DataBind();
        DropDownList3.Items.Insert(0, "-Select-");
        DropDownList3.Items[0].Value = "0";



    }
    protected void mainfolder()
    {

        string cmdstr = "select FolderMasterId,FolderCatName from FolderCategoryMaster1  where Activestatus='Active' and ProductId=" + DropDownList3 .SelectedValue+ "";
        SqlCommand cmdcln = new SqlCommand(cmdstr, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);
        DropDownList1.DataSource = dtcln;

        DropDownList1.DataValueField = "FolderMasterId";
        DropDownList1.DataTextField = "FolderCatName";
        DropDownList1.DataBind();
        DropDownList1.Items.Insert(0, "-Select-");
        DropDownList1.Items[0].Value = "0";



    }
    protected void subfolder()
    {

        string cmdstr = "select  FolderSubId,FolderSubName from FolderSubCatName where Activestatus=1 and FolderMasterId='" + DropDownList1.SelectedValue + "'";
        SqlCommand cmdcln = new SqlCommand(cmdstr, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);
        ddlInventorySubCatID.DataSource = dtcln;

        ddlInventorySubCatID.DataValueField = "FolderSubId";
        ddlInventorySubCatID.DataTextField = "FolderSubName";
        ddlInventorySubCatID.DataBind();
        ddlInventorySubCatID.Items.Insert(0, "-Select-");
        ddlInventorySubCatID.Items[0].Value = "0";



    }






    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        subfolder();
    }
    protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
    {
        mainfolder();
    }
    protected void FillProduct2()
    {

        string cmdstr = "select distinct  FolderSubCatName.FolderSubId,FolderCategoryMaster1.FolderCatName +' -'+FolderSubCatName.FolderSubName as main " +
                        "  from  FolderSubCatName " +
                        "  inner join FolderCategoryMaster1  on FolderCategoryMaster1.FolderMasterId= FolderSubCatName.FolderMasterId where FolderCategoryMaster1.ProductId = '" + DropDownList4.SelectedValue + "' ";
 

        SqlCommand cmdcln = new SqlCommand(cmdstr, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);
        ddlSubcat.DataSource = dtcln;

        ddlSubcat.DataValueField = "FolderSubId";
        ddlSubcat.DataTextField = "main";
        ddlSubcat.DataBind();
        ddlSubcat.Items.Insert(0, "-Select-");
        ddlSubcat.Items[0].Value = "0";



    }
    protected void FillProduct3()
    {

        string cmdstr = " SELECT  distinct  VersionInfoMaster.VersionInfoId, ProductMaster.ProductName + ':' +  VersionInfoMaster.VersionInfoName as productversion  FROM ProductMaster  inner join VersionInfoMaster on ProductMaster.ProductId=VersionInfoMaster.ProductId inner join ProductDetail on ProductDetail.ProductId=ProductMaster.ProductId   where ProductMaster.ClientMasterId='" + Session["ClientId"].ToString() + "' and VersionInfoMaster.Active ='True' and ProductDetail.Active='1'  order  by productversion";
        SqlCommand cmdcln = new SqlCommand(cmdstr, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);
        DropDownList4.DataSource = dtcln;

        DropDownList4.DataValueField = "VersionInfoId";
        DropDownList4.DataTextField = "productversion";
        DropDownList4.DataBind();
        DropDownList4.Items.Insert(0, "-Select-");
        DropDownList4.Items[0].Value = "0";



    }


    protected void Btn_Click(object sender, ImageClickEventArgs e)
    {
       
    }
    protected void llinedit_Click(object sender, ImageClickEventArgs e)
    {
        Button8.Visible = true;
        string str = " ";
        Button lnkbtn = (Button)sender;
        GridViewRow row = (GridViewRow)lnkbtn.NamingContainer;
        int j = Convert.ToInt32(row.RowIndex);
        string i = GridView1.Rows[j].Cells[0].Text;
        ViewState["ID"] = i.ToString();
        string select = "select distinct FolderCategoryMaster1.FolderCatName ,FolderSubCatName.FolderSubName,FolderSubSubCategory.FolderSubSubName ," +
                      " case  when(FolderSubSubCategory.Activestatus=1 ) then 'Active' else 'Inactive'end  as Active,ProductMaster.ProductName + ':' +  VersionInfoMaster.VersionInfoName as productversion" +
                      " from FolderSubSubCategory " +
                      " inner join FolderSubCatName on FolderSubCatName.FolderSubId=FolderSubSubCategory.FolderSubId " +
                      " inner join  FolderCategoryMaster1 on FolderCategoryMaster1.FolderMasterId=FolderSubCatName.FolderMasterId" +
                      " inner join VersionInfoMaster on FolderSubSubCategory.ProductName=VersionInfoMaster.VersionInfoId  " +
                      " inner join ProductMaster on ProductMaster.ProductId=VersionInfoMaster.ProductId " +
                      " inner join ProductDetail on ProductDetail.ProductId=ProductMaster.ProductId " +
                      " where  ProductMaster.ClientMasterId=" + Session["ClientId"] + " " +
                      " and VersionInfoMaster.Active ='True' and ProductDetail.Active='1'  " + str + " ";

        DataTable dt = new DataTable();
        SqlDataAdapter ad = new SqlDataAdapter(str, con);
        ad.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            FillProduct1();
            DropDownList3.SelectedValue = dt.Rows[0]["productversion"].ToString();
            mainfolder();
            DropDownList1.SelectedValue = dt.Rows[0]["FolderCatName"].ToString();
            subfolder();
            ddlInventorySubCatID.SelectedValue = dt.Rows[0]["FolderSubName"].ToString();
            ddlstatus.SelectedValue = dt.Rows[0]["Activestatus"].ToString();
            txtInventorySubSubName.Text = dt.Rows[0]["FolderSubSubName"].ToString();

        }

    }
    protected void Btn_Click1(object sender, ImageClickEventArgs e)//del
    {
        Button lnkbtn = (Button)sender;
        GridViewRow row = (GridViewRow)lnkbtn.NamingContainer;
        int j = Convert.ToInt32(row.RowIndex);
        string i = GridView1.Rows[j].Cells[0].Text;
        ViewState["ID"] = i.ToString();
        string str = "delete from  FolderCategoryMaster1 where ID='" + ViewState["ID"] + "'";
        SqlCommand cmd2 = new SqlCommand(str, con);
        con.Open();
        cmd2.ExecuteNonQuery();
        string id2 = "select distinct FolderSubId from FolderSubCatName ";
        SqlDataAdapter da = new SqlDataAdapter(id2, con);
        DataTable dt = new DataTable();
        da.Fill(dt);
        ViewState["ID1"] = dt.Rows[0][0].ToString();
        string str4 = "delete from  FolderSubCatName where ID='" + ViewState["ID1"] + "'";
        SqlCommand cmd4 = new SqlCommand(str4, con);
        cmd4.ExecuteNonQuery();

        string id3= "select distinct FolderSubSubId from FolderSubSubCategory ";
        SqlDataAdapter da1 = new SqlDataAdapter(id3, con);
        DataTable dt1 = new DataTable();
        da1.Fill(dt1);
        ViewState["ID2"] = dt1.Rows[0][0].ToString();
        string str5 = "delete from  FolderSubSubCategory where ID='" + ViewState["ID2"] + "'";
        SqlCommand cmd5 = new SqlCommand(str5, con);
        cmd5.ExecuteNonQuery();
        con.Close();
        fillgrid();
    }
    protected void Button8_Click(object sender, EventArgs e)
    {
        //string sgw = "select FolderSubSubName from FolderSubSubCategory where " +
        //   " FolderSubSubName='" + txtInventorySubSubName.Text + "' and FolderSubId='" + ddlInventorySubCatID.SelectedValue + "'";

        //SqlCommand cgw = new SqlCommand(sgw, con);
        //SqlDataAdapter adgw = new SqlDataAdapter(cgw);
        //DataTable dtgw = new DataTable();
        //adgw.Fill(dtgw);
        //if (dtgw.Rows.Count > 0)
        //{
        //    statuslable.Visible = true;
        //    statuslable.Text = "This record already exists.";
        //}
        //else
        //{

            string qurty1 = "update  FolderSubSubCategory set FolderSubSubName='" + txtInventorySubSubName.Text + "',FolderSubId=" + int.Parse(ddlInventorySubCatID.SelectedValue.ToString()) + ",Activestatus='" + ddlstatus.SelectedValue + "',ProductName='" + DropDownList3.SelectedValue + "'  where  FolderSubSubCategory.FolderSubSubId=" + ViewState["editid"] + "";
            SqlCommand mycmd = new SqlCommand(qurty1,con);
            if (con.State.ToString()!= "Open")
            {
                con.Open();

            }

            mycmd.ExecuteNonQuery();
            con.Close();

            fillgrid();

            statuslable.Visible = true;
            statuslable.Text = "Record updated successfully";
            txtInventorySubSubName.Text = "";
            ddlInventorySubCatID.SelectedIndex = 0;
            pnladd.Visible = false;
            lbllegend.Visible = false;
            ddlstatus.SelectedIndex = 0;
            btnadd.Visible = true;
        }

    

    protected void Btn_Click2(object sender, ImageClickEventArgs e)
    {

    }
    protected void ddlstatus_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void ddlSubcat_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
    }
}
