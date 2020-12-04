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
//using System.Windows.Forms;

public partial class MainFolderSubFolderMaster : System.Web.UI.Page
{

    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["ClientId"] = "35";
        //  pagetitleclass pg = new pagetitleclass();
        //string strData = Request.Url.ToString();

        //char[] separator = new char[] { '/' };

        //string[] strSplitArr = strData.Split(separator);
        //int i = Convert.ToInt32(strSplitArr.Length);
        //string page = strSplitArr[i - 1].ToString();


        //Page.Title = pg.getPageTitle(page);

       // statuslable.Visible = false;
        //Session["pagename"] = "WizardInventoryCategoryMaster.aspx";
        //Session["pnl1"] = "8";

        if (!IsPostBack)
        {
            FillProduct();
            BindDataonGridOFSubMainFolder();

           // FillMainFOlder();
            FillProductdown();
            
        }
    }

    protected void BindDataonGridOFSubMainFolder()
    {
        DataSet ds = new DataSet();
        DataTable FromTable = new DataTable();
        con.Open();

        string str1 = "";
        if (DrpFilterProduct.SelectedIndex > 0)
        {
            str1 += "and FolderSubCatName.ProductId='" + DrpFilterProduct.SelectedValue + "'";
        }



      
            str1 += "and FolderSubCatName.Activestatus='" + DrpStatus.SelectedValue + "'";
            if (drpmainfolderFilter.SelectedIndex > 0)
            {
                str1 += "and FolderSubCatName.FolderMasterId='" + drpmainfolderFilter.SelectedValue + "'";

            }

            str1 += "and FolderSubCatName.Activestatus='" + DrpStatus.SelectedValue + "'";    /// as u said a i add


            string cmdstr = "Select FolderSubName,FolderSubCatName.FolderSubId, FolderCategoryMaster1.FolderMasterId,case when(FolderSubCatName.Activestatus=1) then 'Active' else 'Inactive' end as Activestatus,FolderCatName, ProductMaster.ProductName + ':' +  VersionInfoMaster.VersionInfoName  as productversion    from FolderSubCatName  inner join VersionInfoMaster on   FolderSubCatName.ProductId=VersionInfoMaster.VersionInfoId 	inner join ProductMaster on    ProductMaster.ProductId=VersionInfoMaster.ProductId    inner join ProductDetail on ProductDetail.ProductId=ProductMaster.ProductId INNER JOIN FolderCategoryMaster1 ON FolderCategoryMaster1.FolderMasterId = FolderSubCatName.FolderMasterId   where ProductMaster.ClientMasterId=" + Session["ClientId"].ToString() + " " + str1 + "";
            //string cmdstr = "Select FolderSubName,FolderMasterId,Activestatus, ProductMaster.ProductName + ':' +  VersionInfoMaster.VersionInfoName as productversion    from FolderSubCatName  inner join VersionInfoMaster on FolderSubCatName.ProductId=VersionInfoMaster.VersionInfoId 	inner join ProductMaster on ProductMaster.ProductId=VersionInfoMaster.ProductId    inner join ProductDetail on ProductDetail.ProductId=ProductMaster.ProductId  where ProductMaster.ClientMasterId=" + Session["ClientId"].ToString() + " " + str1 + "";

            SqlCommand cmd = new SqlCommand(cmdstr, con);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            adp.Fill(ds);
            cmd.ExecuteNonQuery();
            FromTable = ds.Tables[0];
            if (FromTable.Rows.Count > 0)
            {
                GridMainSUbFolder.DataSource = FromTable;
                GridMainSUbFolder.DataBind();
                Label2.Visible = true;
            }
            else
            {
                GridMainSUbFolder.DataSource = null;
                GridMainSUbFolder.DataBind();
            }

            Label2.Visible = true;
            ds.Dispose();
            con.Close();
        }
    protected void  DrpFilterProduct_SelectedIndexChanged(object sender, EventArgs e)
       {
           FillMainFOlderdown();
         BindDataonGridOFSubMainFolder();
    
         }



    protected void btnsubmit_Click(object sender, EventArgs e)
    {

        con.Open();
        string cmdstr = "insert into FolderSubCatName(FolderSubName,FolderMasterId,ProductId,Activestatus) values(@FolderSubName,@FolderMasterId,@ProductId,@Activestatus)";
        SqlCommand cmd = new SqlCommand(cmdstr, con);
        // cmd.Parameters.AddWithValue("@FolderMasterId",lblEmpID.Text);
        cmd.Parameters.AddWithValue("@FolderSubName", TextSubFolder.Text);
        cmd.Parameters.AddWithValue("@ProductId", drpFillProduct.Text);
        cmd.Parameters.AddWithValue("@FolderMasterId", DrpMainFolder.Text);
        cmd.Parameters.AddWithValue("@Activestatus", ddlstatus.Text);
        //cmd.Parameters.AddWithValue("@country", txtAddCountry.Text);
        cmd.ExecuteNonQuery();
        // Label12("Dot Net Perls is awesome.");
        con.Close();
        Label2.Text = "Record saved successfully";
        BindDataonGridOFSubMainFolder();
       
        pnladd.Visible = false;
        TextSubFolder.Text = "";
       // drpFillProduct.Text = "";
        //DrpMainFolder.Text = "";
       // ddlstatus.Text = "";
      //  pnlmonthgoal.Visible = false;





    }
    protected void ImageButton7_Click(object sender, EventArgs e)
    {
        //statuslable.Visible = true;
        pnladd.Visible = false;
        //lbllegend.Visible = true;
        //btnadd.Visible = true;

        //Button3.Visible = true;
       
    }


    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillMainFOlder();
    }
    protected void FillProduct()
    {

        string cmdstr = " SELECT  distinct  VersionInfoMaster.VersionInfoId, ProductMaster.ProductName + ':' +  VersionInfoMaster.VersionInfoName as productversion  FROM ProductMaster  inner join VersionInfoMaster on ProductMaster.ProductId=VersionInfoMaster.ProductId inner join ProductDetail on ProductDetail.ProductId=ProductMaster.ProductId   where ProductMaster.ClientMasterId='" + Session["ClientId"].ToString() + "' and VersionInfoMaster.Active ='True' and ProductDetail.Active='1'  order  by productversion";
        SqlCommand cmdcln = new SqlCommand(cmdstr, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);
        drpFillProduct.DataSource = dtcln;

        drpFillProduct.DataValueField = "VersionInfoId";
        drpFillProduct.DataTextField = "productversion";
        drpFillProduct.DataBind();
        drpFillProduct.Items.Insert(0, "-Select-");
        drpFillProduct.Items[0].Value = "0";



    }

    protected void FillMainFOlder()
 {
        //string sgw = "select InventorySubCatName from InventorySubCategoryMaster where  " +
        //          " InventorySubCatName='" + txtInventorySubCatName.Text + "' and InventoryCategoryMasterId='" + ddlInventoryCategoryMasterId.SelectedValue + "' ";

     string cmdstr = " SELECT FolderMasterId , FolderCatName from FolderCategoryMaster1  where Activestatus='Active' and ProductId=" + drpFillProduct.SelectedValue + " ";
        SqlCommand cmdcln = new SqlCommand(cmdstr, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);
        DrpMainFolder.DataSource = dtcln;

        DrpMainFolder.DataValueField = "FolderMasterId";
        DrpMainFolder.DataTextField = "FolderCatName";
        DrpMainFolder.DataBind();
        DrpMainFolder.Items.Insert(0, "-Select-");
        DrpMainFolder.Items[0].Value = "0";

        

    }
    protected void FillProductdown()
    {

        string cmdstr = " SELECT  distinct  VersionInfoMaster.VersionInfoId, ProductMaster.ProductName + ':' +  VersionInfoMaster.VersionInfoName as productversion  FROM ProductMaster  inner join VersionInfoMaster on ProductMaster.ProductId=VersionInfoMaster.ProductId inner join ProductDetail on ProductDetail.ProductId=ProductMaster.ProductId   where ProductMaster.ClientMasterId='" + Session["ClientId"].ToString() + "' and VersionInfoMaster.Active ='True' and ProductDetail.Active='1'  order  by productversion";
        SqlCommand cmdcln = new SqlCommand(cmdstr, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);
        DrpFilterProduct.DataSource = dtcln;

        DrpFilterProduct.DataValueField = "VersionInfoId";
        DrpFilterProduct.DataTextField = "productversion";
        DrpFilterProduct.DataBind();
        DrpFilterProduct.Items.Insert(0, "-Select-");
        DrpFilterProduct.Items[0].Value = "0";



    }

    protected void FillMainFOlderdown()
    {

        string cmdstr = " SELECT FolderMasterId , FolderCatName from FolderCategoryMaster1 where  ProductId=" + DrpFilterProduct .SelectedValue+ " ";
        SqlCommand cmdcln = new SqlCommand(cmdstr, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);
        drpmainfolderFilter.DataSource = dtcln;

        drpmainfolderFilter.DataValueField = "FolderMasterId";
        drpmainfolderFilter.DataTextField = "FolderCatName";
        drpmainfolderFilter.DataBind();
        drpmainfolderFilter.Items.Insert(0, "-Select-");
        drpmainfolderFilter.Items[0].Value = "0";



    }


    protected void btnadd_Click(object sender, EventArgs e)
    {
        pnladd.Visible = true;
        statuslable.Visible = false;
        Button3.Visible = true;

        btnupdate.Visible = false;
        statuslable.Text = "";


      drpFillProduct.SelectedIndex = 0;
       //DrpMainFolder.SelectedIndex = 0;
        TextSubFolder.Text = "";




    }
    protected void GridMainSUbFolder_RowEditing(object sender, GridViewEditEventArgs e)
    {

     //   BindDataonGridOFSubMainFolder();
        GridMainSUbFolder.EditIndex = -1;

        btnadd.Visible = true;




    }
    protected void GridMainSUbFolder_RowCommand(object sender, GridViewCommandEventArgs e)
    {


        if (e.CommandName == "Delete")
        {
            ViewState["editid"] = Convert.ToInt64(e.CommandArgument);
            // ViewState["editid"] = Convert.ToInt32(e.CommandArgument);
            string cmdstr = "delete  from  FolderSubCatName where FolderSubId= " + ViewState["editid"] + "";
            // SqlCommand mycmd = new SqlCommand(qurty2, con);
            SqlCommand cmd = new SqlCommand(cmdstr, con);

            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmd.ExecuteNonQuery();
            con.Close();
            //  Response.Redirect("MainFolderSubFolderMaster.aspx");
            // statuslable.Visible = true;
            // statuslable.Text = "Record Deleted successfully";
            BindDataonGridOFSubMainFolder();
            statuslable.Visible = true;
            Response.Redirect("MainFolderSubFolderMaster.aspx");
            statuslable.Text = "Record deleted successfully";


        }
        if (e.CommandName == "Edit")
        {
            btnupdate.Visible = true;
            Label2.Text = "";
            Int64 delid1 = Convert.ToInt64(e.CommandArgument);
            ViewState["id"] = delid1;
            string cmdstr = "select *  from  FolderSubCatName where FolderSubId=" + delid1 + "";
            SqlCommand cmd = new SqlCommand(cmdstr, con);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable ds = new DataTable();

            adp.Fill(ds);


            if (ds.Rows.Count > 0)
            {
                string chk = ds.Rows[0]["Activestatus"].ToString();
                if (chk == "True")
                {
                    //chk123.Checked = true;
                    ddlstatus.SelectedValue = "1";
                }
                else
                {
                    //chk123.Checked = false;
                    ddlstatus.SelectedValue = "0";
                }
                TextSubFolder.Text = ds.Rows[0]["FolderSubName"].ToString();
                drpFillProduct.SelectedValue = ds.Rows[0]["ProductId"].ToString();
                ddlstatus.SelectedValue = ds.Rows[0]["Activestatus"].ToString();
               // DrpMainFolder.Text = ds.Rows[0]["FolderMasterId"].ToString();

                FillMainFOlder();
                DrpMainFolder.SelectedValue = ds.Rows[0]["FolderMasterId"].ToString();


                BindDataonGridOFSubMainFolder();
    
                //btnupdate.Visible = true;
             



            }
        }



    }
    protected void GridMainSUbFolder_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
      //  con.Open();
      //  string cmdstr = "update FolderSubCatName  set FolderSubName='" + TextSubFolder.Text + "',FolderMasterId=" + DrpMainFolder.SelectedValue + ",ProductId=" + drpFillProduct.SelectedValue + ",Activestatus='" + ddlstatus.SelectedValue + "' where FolderMasterId=" + ViewState["id"] + "";

      //  SqlCommand cmd = new SqlCommand(cmdstr, con);

      //  cmd.ExecuteNonQuery();
      //  con.Close();

      //// pnlmonthgoal.Visible = false;
      //  BindDataonGridOFSubMainFolder();

      //  Label2.Text = "Record updated successfully";
      //  pnladd.Visible = true;
      //  btnupdate.Visible = true;
      //  Button3.Visible = false;
      //  BindDataonGridOFSubMainFolder();

      // BindDataonGridOFSubMainFolder();
        //update
    }
    protected void GridMainSUbFolder_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        //BindDataonGridOFSubMainFolder();
     //   GridMainSUbFolder.EditIndex = -1;
        //delete
    }
    protected void GridMainSUbFolder_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        //BindDataonGridOFSubMainFolder();
        GridMainSUbFolder.EditIndex = -1;//cancel
    }
    protected void Btn_Click(object sender, ImageClickEventArgs e)
    {

    }
    protected void btnupdate_Click(object sender, EventArgs e)
    {
        con.Open();
        string cmdstr = "update FolderSubCatName  set FolderSubName='" + TextSubFolder.Text + "',FolderMasterId=" + DrpMainFolder.SelectedValue + ",ProductId=" + drpFillProduct.SelectedValue + ",Activestatus='" + ddlstatus.SelectedValue + "' where FolderSubId=" + ViewState["id"] + "";

        SqlCommand cmd = new SqlCommand(cmdstr, con);

        cmd.ExecuteNonQuery();
        con.Close();

        
        BindDataonGridOFSubMainFolder();
        pnladd.Visible = false;
        Label2.Text = "Record updated successfully";
      

      
    }
   
    protected void drpmainfolderFilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindDataonGridOFSubMainFolder();
    }
    protected void DrpStatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindDataonGridOFSubMainFolder();
    }
    protected void imgbtnDelete_Click(object sender, ImageClickEventArgs e)
    {

        ImageButton lnk = (ImageButton)sender;
        GridViewRow gvr = (GridViewRow)lnk.NamingContainer;
        int j = Convert.ToInt32(gvr.RowIndex);
        string id = GridMainSUbFolder.Rows[j].Cells[0].Text;
        Label lbl = (Label)gvr.FindControl("Label31");
        string cmdstr = "delete  from  FolderSubCatName where FolderSubId= " + lbl.Text + "";
        SqlCommand cmd = new SqlCommand(cmdstr, con);
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        BindDataonGridOFSubMainFolder();
        //Response.Redirect("MainFolderMaster.aspx");
        Label2.Text = "Record deleted successfully";















       // GridMainSUbFolder.EditIndex = -1;
    }
    protected void imgbtnEdit_Click(object sender, ImageClickEventArgs e)
    {
        if (pnladd.Visible == false)
        {
            Button3.Visible = false;
            pnladd.Visible = true;
            lbllegend.Visible = true;
        }
        else
        {
            Button3.Visible = false;
            pnladd.Visible = false;
            lbllegend.Visible = true;
        }
        btnadd.Visible = false;
       // statuslable.Text = "";
        GridMainSUbFolder.EditIndex = -1;
    }
    protected void DrpMainFolder_SelectedIndexChanged(object sender, EventArgs e)
    {
       
    }
}
