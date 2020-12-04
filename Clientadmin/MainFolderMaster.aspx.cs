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

public partial class MainFolderMaster : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
        Session["ClientId"] = "35";
        {

            if (!IsPostBack)
            {
                BindData();
                FillProduct();
                FillProduct1();
                addnewpanel.Visible = true;
            }
        }
    }

    protected void BindData()
    {
        DataSet ds = new DataSet();
        DataTable FromTable = new DataTable();
        con.Open();
        
           string str1="";
            if(DropDownList2.SelectedIndex>0)
            {
                str1+="and FolderCategoryMaster1.ProductId='" + DropDownList2.SelectedValue + "'";
            }
        str1+="and Activestatus='" + Drepaqctive.SelectedValue +"'";


        string cmdstr = "Select FolderCatName,FolderMasterId,Activestatus, ProductMaster.ProductName + ':' +  VersionInfoMaster.VersionInfoName as productversion    from FolderCategoryMaster1   inner join VersionInfoMaster on FolderCategoryMaster1.ProductId=VersionInfoMaster.VersionInfoId 	inner join ProductMaster on ProductMaster.ProductId=VersionInfoMaster.ProductId    inner join ProductDetail on ProductDetail.ProductId=ProductMaster.ProductId  where ProductMaster.ClientMasterId=" + Session["ClientId"].ToString() + " "+str1+"";

        SqlCommand cmd = new SqlCommand(cmdstr, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        adp.Fill(ds);
        cmd.ExecuteNonQuery();
        FromTable = ds.Tables[0];
        if (FromTable.Rows.Count > 0)
        {
            gvEmployeeDetails.DataSource = FromTable;
            gvEmployeeDetails.DataBind();
        }
        else
        {
            FromTable.Rows.Add(FromTable.NewRow());
            gvEmployeeDetails.DataSource = FromTable;
            gvEmployeeDetails.DataBind();
            int TotalColumns = gvEmployeeDetails.Rows[0].Cells.Count;
            gvEmployeeDetails.Rows[0].Cells.Clear();
            gvEmployeeDetails.Rows[0].Cells.Add(new TableCell());
            gvEmployeeDetails.Rows[0].Cells[0].ColumnSpan = TotalColumns;
            gvEmployeeDetails.Rows[0].Cells[0].Text = "No records Found";
        }
        ds.Dispose();
        con.Close();
        addnewpanel.Visible = true;
    }






    //   protected void gvEmployeeDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    //   {
    //     Label lblEmpID = (Label)gvEmployeeDetails.Rows[e.RowIndex].FindControl(" FolderMasterId");

    //   con.Open();
    ////  string cmdstr = "delete  * from  FolderCategoryMaster1 where FolderMasterId=@FolderMasterId";

    //   string cmdstr = "delete FolderMasterId from FolderCategoryMaster1  ";
    //       SqlCommand cmd = new SqlCommand(cmdstr, con);
    //        DataTable dtcln = new DataTable();

    //       cmd.ExecuteNonQuery();

    //       BindData();
    //       Label12.Text = "Record deleted successfully";

    //   }
    protected void gvEmployeeDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        //if (e.CommandName.Equals("ADD"))
        //{
        //    TextBox txtAddEmpID = (TextBox)gvEmployeeDetails.FooterRow.FindControl("txtAddEmpID");
        //    TextBox txtAddName = (TextBox)gvEmployeeDetails.FooterRow.FindControl("txtAddName");
        //    TextBox txtAddDesignation = (TextBox)gvEmployeeDetails.FooterRow.FindControl("txtAddDesignation");
        //    TextBox txtAddCity = (TextBox)gvEmployeeDetails.FooterRow.FindControl("txtAddCity");
        //    // TextBox txtAddCountry = (TextBox)gvEmployeeDetails.FooterRow.FindControl("txtAddCountry");

        //    con.Open();
        //    string cmdstr = "insert into FolderCategoryMaster1(FolderMasterId,FolderCatName,ProductId,Activestatus) values(@FolderMasterId,@FolderCatName,@ProductId,@Activestatus)";
        //    SqlCommand cmd = new SqlCommand(cmdstr, con);
        //    cmd.Parameters.AddWithValue("@FolderMasterId", txtAddEmpID.Text);
        //    cmd.Parameters.AddWithValue("@ProductId", txtAddDesignation.Text);
        //    cmd.Parameters.AddWithValue("@FolderCatName", txtAddName.Text);
        //    cmd.Parameters.AddWithValue("@Activestatus", txtAddCity.Text);
        //    // cmd.Parameters.AddWithValue("@country", txtAddCountry.Text);
        //    // cmd.ExecuteNonQuery();
        //    con.Close();
        //    BindData();

        //}
        if (e.CommandName == "Delete")
        {
            Int64 delid1 = Convert.ToInt64(e.CommandArgument);
            con.Open();
            string cmdstr = "delete  from  FolderCategoryMaster1 where FolderMasterId=" + delid1 + "";
            SqlCommand cmd = new SqlCommand(cmdstr, con);
            cmd.ExecuteNonQuery();
            con.Close();
            BindData();
            //Response.Redirect("MainFolderMaster.aspx");
            lblmsg.Text = "Record deleted successfully";
        }
        if (e.CommandName == "Edit")
        {

            Button1.Visible = false;
            Button5.Visible = true;
            
            pnlmonthgoal.Visible = true;
            Int64 delid1 = Convert.ToInt64(e.CommandArgument);
            ViewState["id"] = delid1;
            string cmdstr = "select *  from  FolderCategoryMaster1 where FolderMasterId=" + delid1 + "";
            SqlCommand cmd = new SqlCommand(cmdstr, con);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable ds = new DataTable();
            adp.Fill(ds);

            if (ds.Rows.Count > 0)
            {
                txtmainfolder.Text = ds.Rows[0]["FolderCatName"].ToString();

                Drproductversion.SelectedValue = ds.Rows[0]["ProductId"].ToString();
                ddlstatus.SelectedValue = ds.Rows[0]["Activestatus"].ToString();
                addnewpanel.Visible = true;
               
            }
        }
    }
    protected void gvEmployeeDetails_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        //TextBox txtAddEmpID = (TextBox)gvEmployeeDetails.FooterRow.FindControl("txtAddEmpID");
        //TextBox txtAddName = (TextBox)gvEmployeeDetails.FooterRow.FindControl("txtAddName");
        //TextBox txtAddDesignation = (TextBox)gvEmployeeDetails.FooterRow.FindControl("txtAddDesignation");
        //TextBox txtAddCity = (TextBox)gvEmployeeDetails.FooterRow.FindControl("txtAddCity");
        //// TextBox txtEditCountry = (TextBox)gvEmployeeDetails.Rows[e.RowIndex].FindControl("txtEditCountry");

        //con.Open();
        //string cmdstr = "update FolderCategoryMaster1 set FolderCatName=@FolderCatName,ProductId=@ProductId,Activestatus=@Activestatus where FolderMasterId=@FolderMasterId";

        //SqlCommand cmd = new SqlCommand(cmdstr, con);
        //cmd.Parameters.AddWithValue("@FolderMasterId", txtAddEmpID.Text);
        //cmd.Parameters.AddWithValue("@ProductId", ddlstatus.Text);
        //cmd.Parameters.AddWithValue("@FolderCatName", txtmainfolder.Text);
        //cmd.Parameters.AddWithValue("@Activestatus", Drproductversion.Text);
        //// cmd.Parameters.AddWithValue("@country", txtEditCountry.Text);
        //cmd.ExecuteNonQuery();
        //con.Close();

        //gvEmployeeDetails.EditIndex = -1;
        //BindData();

    }
    protected void gvEmployeeDetails_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        gvEmployeeDetails.EditIndex = -1;
        BindData();
    }
    protected void gvEmployeeDetails_RowEditing(object sender, GridViewEditEventArgs e)
    {
        gvEmployeeDetails.EditIndex = e.NewEditIndex;
        BindData();
    }




    protected void FillProduct()
    {

        string cmdstr = " SELECT  distinct  VersionInfoMaster.VersionInfoId, ProductMaster.ProductName + ':' +  VersionInfoMaster.VersionInfoName as productversion  FROM ProductMaster  inner join VersionInfoMaster on ProductMaster.ProductId=VersionInfoMaster.ProductId inner join ProductDetail on ProductDetail.ProductId=ProductMaster.ProductId   where ProductMaster.ClientMasterId='" + Session["ClientId"].ToString() + "' and VersionInfoMaster.Active ='True' and ProductDetail.Active='1'  order  by productversion";
        SqlCommand cmdcln = new SqlCommand(cmdstr, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);
        Drproductversion.DataSource = dtcln;

        Drproductversion.DataValueField = "VersionInfoId";
        Drproductversion.DataTextField = "productversion";
        Drproductversion.DataBind();
        Drproductversion.Items.Insert(0, "-Select-");
        Drproductversion.Items[0].Value = "0";



    }
    protected void txtmainfolder_TextChanged(object sender, EventArgs e)
    {

    }
    protected void Drepaqctive_SelectedIndexChanged(object sender, EventArgs e)
    {

        BindData();
      //  string cmdstr = " select Activestatus from FolderCategoryMaster1 where " + " Activestatus='" + Drepaqctive.SelectedValue + "' ";
        //string cmdstr = "Select Activestatus, ProductMaster.ProductName + ':' +  VersionInfoMaster.VersionInfoName as productversion    from FolderCategoryMaster1   inner join VersionInfoMaster on FolderCategoryMaster1.ProductId=VersionInfoMaster.VersionInfoId 	inner join ProductMaster on ProductMaster.ProductId=VersionInfoMaster.ProductId    inner join ProductDetail on ProductDetail.ProductId=ProductMaster.ProductId  where ProductMaster.ClientMasterId=" + Session["ClientId"].ToString() + and  "'" Activestatus='" + Drepaqctive.SelectedValue +"'; ";


        //SqlCommand cmd = new SqlCommand(cmdstr, con);
        //SqlDataAdapter da = new SqlDataAdapter(cmd);
        //DataTable dt = new DataTable();
        //da.Fill(dt);

        //gvEmployeeDetails.DataSource = dt;
        //gvEmployeeDetails.DataBind();
        //BindData();
    }



    protected void FillProduct1()
    {

        string cmdstr = " SELECT  distinct  VersionInfoMaster.VersionInfoId, ProductMaster.ProductName + ':' +  VersionInfoMaster.VersionInfoName as productversion  FROM ProductMaster  inner join VersionInfoMaster on ProductMaster.ProductId=VersionInfoMaster.ProductId inner join ProductDetail on ProductDetail.ProductId=ProductMaster.ProductId   where ProductMaster.ClientMasterId='" + Session["ClientId"].ToString() + "' and VersionInfoMaster.Active ='True' and ProductDetail.Active='1'  order  by productversion";
        SqlCommand cmdcln = new SqlCommand(cmdstr, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);
        DropDownList2.DataSource = dtcln;

        DropDownList2.DataValueField = "VersionInfoId";
        DropDownList2.DataTextField = "productversion";
        DropDownList2.DataBind();
        DropDownList2.Items.Insert(0, "-Select-");
        DropDownList2.Items[0].Value = "0";



    }
    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {

        //string cmdstr = "Select FolderMasterId,FolderCatName,Activestatus, ProductMaster.ProductName + ':' +  VersionInfoMaster.VersionInfoName as productversion    from FolderCategoryMaster1   inner join VersionInfoMaster on FolderCategoryMaster1.ProductId=VersionInfoMaster.VersionInfoId 	inner join ProductMaster on ProductMaster.ProductId=VersionInfoMaster.ProductId    inner join ProductDetail on ProductDetail.ProductId=ProductMaster.ProductId  where ProductMaster.ClientMasterId=" + Session["ClientId"].ToString() + "  ";

        //SqlCommand cmd = new SqlCommand(cmdstr, con);
        //SqlDataAdapter da = new SqlDataAdapter(cmd);
        //DataTable dt = new DataTable();
        //da.Fill(dt);
        //if (dt.Rows.Count > 0)
        //{
        //    gvEmployeeDetails.DataSource = dt;
        //    gvEmployeeDetails.DataBind();
        //}
        //else
        //{
        //    gvEmployeeDetails.DataSource = "";
        //    gvEmployeeDetails.DataBind();
        //}
        BindData();
    }

    protected void Button1_Click(object sender, EventArgs e)
    {


        con.Open();
        string cmdstr = "insert into FolderCategoryMaster1(FolderCatName,ProductId,Activestatus) values(@FolderCatName,@ProductId,@Activestatus)";
        SqlCommand cmd = new SqlCommand(cmdstr, con);
        // cmd.Parameters.AddWithValue("@FolderMasterId",lblEmpID.Text);
        //cmd.Parameters.AddWithValue("@productversion", Drproductversion.Text);
        cmd.Parameters.AddWithValue("@ProductId", Drproductversion.SelectedValue);
        cmd.Parameters.AddWithValue("@FolderCatName", txtmainfolder.Text);
        cmd.Parameters.AddWithValue("@Activestatus", ddlstatus.Text);
        //cmd.Parameters.AddWithValue("@country", txtAddCountry.Text);
        cmd.ExecuteNonQuery();
        // Label12("Dot Net Perls is awesome.");
        con.Close();

        BindData();
        lblmsg.Text = "Record saved successfully";
        addnewpanel.Visible = true;
        pnlmonthgoal.Visible = false;


    }

    protected void Button2_Click(object sender, EventArgs e)
    {
        addnewpanel.Visible = true;
        pnlmonthgoal.Visible = false;
        addnewpanel.Visible = true;
    }
    protected void addnewpanel_Click(object sender, EventArgs e)
    {
        pnlmonthgoal.Visible = true;
        addnewpanel.Visible = false;
        Button5.Visible = false;
        Button1.Visible = true;
        addnewpanel.Visible = true;
        txtmainfolder.Text = "";
        Drproductversion.SelectedIndex = 0;
        ddlstatus.SelectedIndex = 0;
    }
    protected void Button5_Click(object sender, EventArgs e)
    {
        
        con.Open();
        string cmdstr = "update FolderCategoryMaster1 set FolderCatName='" + txtmainfolder.Text + "',ProductId=" + Drproductversion.SelectedValue + ",Activestatus='" + ddlstatus .SelectedValue+ "' where FolderMasterId=" + ViewState["id"] + "";

        SqlCommand cmd = new SqlCommand(cmdstr, con);
       
        cmd.ExecuteNonQuery();
        con.Close();

        pnlmonthgoal.Visible = false;
        BindData();
        lblmsg.Text = "Record updated successfully";
        addnewpanel.Visible = true;
    }



    protected void imgbtnDelete_Click(object sender, ImageClickEventArgs e)
    {
        ImageButton lnk = (ImageButton)sender;
        GridViewRow gvr = (GridViewRow)lnk.NamingContainer;
        int j = Convert.ToInt32(gvr.RowIndex);
        string id=gvEmployeeDetails.Rows[j].Cells[0].Text;
        Label lbl = (Label)gvr.FindControl("Label31");
        string cmdstr = "delete  from  FolderCategoryMaster1 where FolderMasterId=" + lbl.Text +  "";
        SqlCommand cmd = new SqlCommand(cmdstr, con);
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
        BindData();
        //Response.Redirect("MainFolderMaster.aspx");
        lblmsg.Text = "Record deleted successfully";
    }
    protected void gvEmployeeDetails_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if((e.Row.RowState==DataControlRowState.Normal ||  e.Row.RowState==DataControlRowState.Alternate) && (e.Row.RowType==DataControlRowType.DataRow || e.Row.RowType==DataControlRowType.Header))
        {
            e.Row.Cells[0].Visible = false;
        }
    }
    protected void imgbtnEdit_Click(object sender, ImageClickEventArgs e)
    {
        
    }
   
}




 