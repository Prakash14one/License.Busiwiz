using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;

using System.IO.Compression;
using System.IO;
using Ionic.Zip;
using System.Net;
using System.Security.Cryptography;



public partial class PortalCategory : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);
    public static string encstr = "";


    protected void Page_Load(object sender, EventArgs e)
    {
        
        Page.Form.Attributes.Add("enctype", "multipart/form-data");
        if (!IsPostBack)
        {
            ViewState["sortOrder"] = "";
            if (Session["Login"] != null)
            {
                if (Session["Login"].ToString() == null)
                {
                    Response.Redirect("~/Login.aspx");
                }
            }
            else
            {
                Response.Redirect("~/Login.aspx");
            }
            FillProduct();
            fillportal();

            fillddlcategoryType();
            fillddlcategorySubType();

            fillgrid();
            FillProductsearch();
            fillddlcategoryTypeFilter();
        }
    }
    protected void fillddlcategoryType()
    {
        ddlcategoryType.Items.Clear();
        string strcln = " SELECT distinct * FROM PriceplanCategoryType where Active='1'  order  by ID";
        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);
        ddlcategoryType.DataSource = dtcln;
        ddlcategoryType.DataTextField = "CategoryType";
        ddlcategoryType.DataValueField = "ID";
        ddlcategoryType.DataBind();
        ddlcategoryType.Items.Insert(0, "-Select-");
        ddlcategoryType.Items[0].Value = "0";
    }       
    protected void ddlcategoryType_SelectedIndexChanged(object sender, EventArgs e)
    {       
         fillddlcategorySubType();               
        if (ddlcategoryType.SelectedValue == "1" || ddlcategoryType.SelectedValue == "2" || ddlcategoryType.SelectedValue == "3" || ddlcategoryType.SelectedValue == "4" || ddlcategoryType.SelectedValue == "5")
        {
            pnlradio.Visible = true;
        }
        else
        {
            pnlradio.Visible = false;
        }
    }
    protected void fillddlcategorySubType()
    {
        ddlcategorysubType.Items.Clear();
        string strcln = " SELECT distinct * FROM PriceplanCategorySubType where PriceplanCategoryTypeID='" + ddlcategoryType.SelectedValue+ "' and Active='1'  order  by ID";
        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);
        ddlcategorysubType.DataSource = dtcln;
        ddlcategorysubType.DataTextField = "Name";
        ddlcategorysubType.DataValueField = "ID";
        ddlcategorysubType.DataBind();
        ddlcategorysubType.Items.Insert(0, "-Select-");
        ddlcategorysubType.Items[0].Value = "0";
    }
    protected void ddlcategorysubType_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlcategorysubType.SelectedValue == "1")
        {
            pnlnoofcompany.Visible = true;
        }
        else
        {
            pnlnoofcompany.Visible = false;
            txtmaxshare.Text = "";
        }

    }
   
    protected void FillProduct()
    {
        DataTable dtcln = selectBZ(" SELECT distinct ProductMaster.ProductId,ProductMaster.ProductName  as productversion FROM dbo.ProductMaster INNER JOIN dbo.VersionInfoMaster ON dbo.ProductMaster.ProductId = dbo.VersionInfoMaster.ProductId INNER JOIN dbo.ProductDetail ON dbo.ProductDetail.VersionNo = dbo.VersionInfoMaster.VersionInfoName INNER JOIN dbo.PortalMasterTbl ON dbo.ProductMaster.ProductId = dbo.PortalMasterTbl.ProductId    where ClientMasterId=" + Session["ClientId"].ToString() + " and ProductDetail.Active ='True' order  by productversion");
        ddlproductversion.DataSource = dtcln;
        ddlproductversion.DataValueField = "ProductId";
        ddlproductversion.DataTextField = "productversion";
        ddlproductversion.DataBind();
    }
    protected void fillportal()
    {
        DataTable dtcln = selectBZ(" select * from PortalMasterTbl where ProductId='" + ddlproductversion.SelectedValue + "' ");
        ddlportalname.DataSource = dtcln;
        ddlportalname.DataValueField = "Id";
        ddlportalname.DataTextField = "PortalName";
        ddlportalname.DataBind();
    }


    //-----------------Filter//**********-----------------------********-------------
    protected void FillProductsearch()
    {
        DataTable dtcln = selectBZ("SELECT distinct ProductMaster.ProductId,ProductMaster.ProductName  as productversion FROM  dbo.ProductMaster INNER JOIN dbo.VersionInfoMaster ON dbo.ProductMaster.ProductId = dbo.VersionInfoMaster.ProductId INNER JOIN dbo.ProductDetail ON dbo.ProductDetail.VersionNo = dbo.VersionInfoMaster.VersionInfoName INNER JOIN dbo.PortalMasterTbl ON dbo.ProductMaster.ProductId = dbo.PortalMasterTbl.ProductId INNER JOIN dbo.Priceplancategory ON dbo.PortalMasterTbl.Id = dbo.Priceplancategory.PortalId    where ClientMasterId=" + Session["ClientId"].ToString() + " and ProductDetail.Active ='True' order  by productversion");
        ddlstsearchproduct.DataSource = dtcln;
        ddlstsearchproduct.DataValueField = "ProductId";
        ddlstsearchproduct.DataTextField = "productversion";
        ddlstsearchproduct.DataBind();
        ddlstsearchproduct.Items.Insert(0, "All");
        ddlstsearchproduct.Items[0].Value = "0";
        ddlstsearchportal.Items.Insert(0, "All");
        ddlstsearchportal.Items[0].Value = "0";

    }
    protected void fillportalsearch()
    {
        DataTable dtcln = selectBZ(" select * from PortalMasterTbl where ProductId='" + ddlstsearchproduct.SelectedValue + "' ");

        ddlstsearchportal.DataSource = dtcln;
        ddlstsearchportal.DataValueField = "Id";
        ddlstsearchportal.DataTextField = "PortalName";
        ddlstsearchportal.DataBind();
        ddlstsearchportal.Items.Insert(0, "All");
        ddlstsearchportal.Items[0].Value = "0";
    }
    protected void ddlstsearchproduct_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblmsg.Text = "";
        fillportalsearch();

    }
    protected void fillddlcategoryTypeFilter()
    {
        ddlCategoryTypeFilter.Items.Clear();
        string strcln = " SELECT distinct * FROM PriceplanCategoryType where Active='1'  order  by ID";
        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);
        ddlCategoryTypeFilter.DataSource = dtcln;
        ddlCategoryTypeFilter.DataTextField = "CategoryType";
        ddlCategoryTypeFilter.DataValueField = "ID";
        ddlCategoryTypeFilter.DataBind();
        ddlCategoryTypeFilter.Items.Insert(0, "-Select-");
        ddlCategoryTypeFilter.Items[0].Value = "0";
    }
    protected void btngo_Click(object sender, EventArgs e)
    {
        lblmsg.Text = "";
        fillgrid();
    }
    //******************************************************************************
    protected void Button1_Click1(object sender, EventArgs e)
    {       
        lblmsg.Text = "";
        if (Button3.Text == "Printable Version")
        {
            Button3.Text = "Hide Printable Version";
            Button4.Visible = true;

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
            Button3.Text = "Printable Version";
            Button4.Visible = false;

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
    protected DataTable selectBZ(string str)
    {
        SqlCommand cmdclnccdweb = new SqlCommand(str, con);
        DataTable dtclnccdweb = new DataTable();
        SqlDataAdapter adpclnccdweb = new SqlDataAdapter(cmdclnccdweb);
        adpclnccdweb.Fill(dtclnccdweb);
        return dtclnccdweb;
    }

    protected void Button1_Click(object sender, EventArgs e)
    {
        DataTable dtsvr = selectBZ(" select * from Priceplancategory   where PortalId='" + ddlportalname.SelectedValue + "' and CategoryName='" + txtcodetypecategory.Text + "'");
         if (dtsvr.Rows.Count > 0)
         {
             lblmsg.Visible = true;
             lblmsg.Text = "Record already exist";
         }
         else
         {

            // SqlCommand cmdsq = new SqlCommand("Insert into Priceplancategory(CategoryName,PortalId,Status)Values('" + txtcodetypecategory.Text + "','" + ddlportalname.SelectedValue + "','" + ddlstatus.SelectedValue + "')", con);
             SqlCommand cmdsq = new SqlCommand("Priceplancategory_AddDeleteUpdate", con);
             cmdsq.CommandType = CommandType.StoredProcedure;
             cmdsq.Parameters.AddWithValue("@StatementType", "Insert");
             cmdsq.Parameters.AddWithValue("@CategoryName", txtcodetypecategory.Text);
             cmdsq.Parameters.AddWithValue("@PortalId", ddlportalname.SelectedValue);
             cmdsq.Parameters.AddWithValue("@Status", ddlstatus.SelectedValue);
             cmdsq.Parameters.AddWithValue("@Franchise", chkupload.Checked);
             cmdsq.Parameters.AddWithValue("@Outbound_emailAccess_Step3", RadioButtonList2.SelectedValue );
             cmdsq.Parameters.AddWithValue("@Free_Trial_products_Step4", RadioButtonList3.SelectedValue);
             cmdsq.Parameters.AddWithValue("@Option_to_Accept_Online_Payments", RadioButtonList1.SelectedValue);
             cmdsq.Parameters.AddWithValue("@Server_Mandatory_onC3server", RadioButtonList1.SelectedValue);
             cmdsq.Parameters.AddWithValue("@CategoryType", ddlcategoryType.SelectedItem.Text);
             cmdsq.Parameters.AddWithValue("@perpetual_free_priceplan", false);
             cmdsq.Parameters.AddWithValue("@CategoryTypeID", ddlcategoryType.SelectedValue);
             cmdsq.Parameters.AddWithValue("@CategoryTypeSubID", ddlcategorysubType.SelectedValue);
            // cmdsq.Parameters.AddWithValue("@CategoryTypeSubID", ddlcategorysubType.SelectedValue);
             cmdsq.Parameters.AddWithValue("@NumberofActiveCompanySharedwith", txtmaxshare.Text);//Numberof ActiveCompanySharedwith
              
             //
            
             if (con.State.ToString() != "Open")
             {
                 con.Open();
             }
             cmdsq.ExecuteNonQuery();
             con.Close();

             txtcodetypecategory.Text = "";
             ddlstatus.SelectedIndex = 0;
             lblmsg.Visible = true;
             lblmsg.Text = "Record Inserted successfully";

             ddlstatus.SelectedIndex = 0;
             lbllegend1.Text = "";
             pnladdnew.Visible = false;
             btnadddd.Visible = true;

             fillgrid();
         }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        DataTable dtsvr = selectBZ(" select * from Priceplancategory   where PortalId='" + ddlportalname.SelectedValue + "' and CategoryName='" + txtcodetypecategory.Text + "' and   ID <> '" + ViewState["id"].ToString() + "' ");
        if (dtsvr.Rows.Count > 0)
        {
            lblmsg.Visible = true;
            lblmsg.Text = "Record already exist";
        }
        else
        {
            SqlCommand cmdsq = new SqlCommand("Priceplancategory_AddDeleteUpdate", con);
            cmdsq.CommandType = CommandType.StoredProcedure;
            cmdsq.Parameters.AddWithValue("@StatementType", "Update");
            cmdsq.Parameters.AddWithValue("@CategoryName", txtcodetypecategory.Text);
            cmdsq.Parameters.AddWithValue("@PortalId", ddlportalname.SelectedValue);
            cmdsq.Parameters.AddWithValue("@Status", ddlstatus.SelectedValue);
            cmdsq.Parameters.AddWithValue("@Franchise", chkupload.Checked);
            cmdsq.Parameters.AddWithValue("@ID", ViewState["id"].ToString());
            cmdsq.Parameters.AddWithValue("@Outbound_emailAccess_Step3", RadioButtonList2.SelectedValue);
            cmdsq.Parameters.AddWithValue("@Free_Trial_products_Step4", RadioButtonList3.SelectedValue);
            cmdsq.Parameters.AddWithValue("@Option_to_Accept_Online_Payments", RadioButtonList1.SelectedValue);  //Server_Mandatory_onC3server 
            cmdsq.Parameters.AddWithValue("@Server_Mandatory_onC3server", "");
            cmdsq.Parameters.AddWithValue("@CategoryType", ddlcategoryType.SelectedItem.Text);
            cmdsq.Parameters.AddWithValue("@CategoryTypeID", ddlcategoryType.SelectedValue);
            cmdsq.Parameters.AddWithValue("@perpetual_free_priceplan", false);
            cmdsq.Parameters.AddWithValue("@CategoryTypeSubID", ddlcategorysubType.SelectedValue);
            cmdsq.Parameters.AddWithValue("@NumberofActiveCompanySharedwith", txtmaxshare.Text);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmdsq.ExecuteNonQuery();
            con.Close();
            txtcodetypecategory.Text = "";
            lblmsg.Visible = true;
            lblmsg.Text = "Record updates successfully";

            ddlstatus.SelectedIndex = 0;
            lbllegend1.Text = "";
            pnladdnew.Visible = false;
            btnadddd.Visible = true;
            Button2.Visible = false;

            fillgrid();
        }
    }

    //protected void fillgrid()
    //{
    //    DataTable dtsvr = selectBZ(" select Priceplancategory.*,PortalMasterTbl.PortalName,ProductMaster.ProductName from Priceplancategory inner join PortalMasterTbl on PortalMasterTbl.Id=Priceplancategory.PortalId inner join ProductMaster on ProductMaster.ProductId=PortalMasterTbl.ProductId where ProductMaster.ClientMasterId='" + Session["ClientId"] + "'  ");

    //    GridView1.DataSource = dtsvr;
    //    GridView1.DataBind();

    //}
    public void fillgrid()
    {
        string str = " select Priceplancategory.*,PortalMasterTbl.PortalName,ProductMaster.ProductId, ProductMaster.ProductName,dbo.PriceplanCategoryType.CategoryType AS CategoryTypeName , dbo.PriceplanCategorySubType.Name as SubTypeName FROM  " +
           " dbo.PriceplanCategorySubType RIGHT OUTER JOIN dbo.PriceplanCategoryType RIGHT OUTER JOIN dbo.Priceplancategory INNER JOIN dbo.PortalMasterTbl INNER JOIN dbo.ProductMaster ON dbo.PortalMasterTbl.ProductId = dbo.ProductMaster.ProductId ON dbo.Priceplancategory.PortalId = dbo.PortalMasterTbl.Id ON  dbo.PriceplanCategoryType.ID = dbo.Priceplancategory.CategoryTypeID ON dbo.PriceplanCategorySubType.ID = dbo.Priceplancategory.CategoryTypeSubID " +
         " where ProductMaster.ClientMasterId='" + Session["ClientId"] + "'";

        string str1 = "";
        string str2 = "";
        string str3 = "";

        if (ddlstsearchproduct.SelectedIndex > 0)
        {
            str1 = " and ProductMaster.ProductId='" + ddlstsearchproduct.SelectedValue + "'";
        }
        if (ddlstsearchportal.SelectedIndex > 0)
        {
            str2 = " and Priceplancategory.PortalId='" + ddlstsearchportal.SelectedValue + "'";
        }
        if (ddlstsearchstatus.SelectedIndex > 0)
        {
            str3 = " and Priceplancategory.Status='" + ddlstsearchstatus.SelectedValue + "'";
        }
        if (ddlCategoryTypeFilter.SelectedIndex > 0)
        {
            str3 = " and Priceplancategory.CategoryTypeID='" + ddlCategoryTypeFilter.SelectedValue + "'";
        }
        string strfinal = str + str1 + str2 + str3 + "Order By Priceplancategory.ID";
        SqlCommand cmdcln = new SqlCommand(strfinal, con);
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        DataTable dtcln = new DataTable();
        adpcln.Fill(dtcln);
        GridView1.DataSource = dtcln;
        DataView myDataView = new DataView();
        myDataView = dtcln.DefaultView;

        if (hdnsortExp.Value != string.Empty)
        {
            myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
        }
        GridView1.DataBind();
        //GridView1.DataSource = dtcln;
        //GridView1.DataBind();
    }
    protected void ddlproductversion_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillportal();
    }
   
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        lblmsg.Text = "";
        if (e.CommandName == "edit")
        {
            DataTable dtsvr = selectBZ("select Priceplancategory.*,ProductMaster.ProductId from Priceplancategory inner join PortalMasterTbl on PortalMasterTbl.Id=Priceplancategory.PortalId inner join ProductMaster on ProductMaster.ProductId=PortalMasterTbl.ProductId where Priceplancategory.ID='" + e.CommandArgument.ToString() + "' ");
            if (dtsvr.Rows.Count > 0)
            {
                //ViewState["id"] = e.CommandArgument.ToString();
                ViewState["id"] = dtsvr.Rows[0]["ID"].ToString();

                FillProduct();
                ddlproductversion.SelectedIndex = ddlproductversion.Items.IndexOf(ddlproductversion.Items.FindByValue(dtsvr.Rows[0]["ProductId"].ToString()));

                fillportal();
                ddlportalname.SelectedIndex = ddlportalname.Items.IndexOf(ddlportalname.Items.FindByValue(dtsvr.Rows[0]["PortalId"].ToString()));

                txtcodetypecategory.Text = dtsvr.Rows[0]["CategoryName"].ToString();
                string st = dtsvr.Rows[0]["Status"].ToString();
                if (st == "True")
                {
                    ddlstatus.SelectedValue = "1";
                }
                if (st == "False")
                {
                    ddlstatus.SelectedValue = "0";
                }
                try
                {
                    string stStep3 = dtsvr.Rows[0]["Outbound_emailAccess_Step3"].ToString();
                    if (stStep3 == "True")
                    {
                        RadioButtonList2.SelectedValue = "1";
                    }
                    else
                    {
                        RadioButtonList2.SelectedValue = "0";
                    }
                }
                catch (Exception ex)
                {
                    RadioButtonList2.SelectedValue = "0";
                }
                try
                {
                    string stStep4 = dtsvr.Rows[0]["Free_Trial_products_Step4"].ToString();
                    if (stStep4 == "True")
                    {
                        RadioButtonList3.SelectedValue = "1";
                    }
                    else
                    {
                        RadioButtonList3.SelectedValue = "0";
                    }
                }
                catch (Exception ex)
                {
                    RadioButtonList3.SelectedValue = "0";
                }
                try
                {
                    string stPayment = dtsvr.Rows[0]["Free_Trial_products_Step4"].ToString();
                    if (stPayment == "True")
                    {
                        RadioButtonList1.SelectedValue = "1";
                    }
                    else
                    {
                        RadioButtonList1.SelectedValue = "0";
                    }
                }
                catch (Exception ex)
                {
                    RadioButtonList1.SelectedValue = "0";
                }
                  
                try
                {
                    chkupload.Checked = Convert.ToBoolean(dtsvr.Rows[0]["Franchise"].ToString());
                }
                catch (Exception ex)
                {
                    chkupload.Checked = false;
                }
                try
                {
                  //  ddlcategoryType.SelectedValue = dtsvr.Rows[0]["CategoryType"].ToString();
                }
                catch (Exception ex)
                {
                   
                }
                fillddlcategoryType();
                 try
                {
                    ddlcategoryType.SelectedValue = dtsvr.Rows[0]["CategoryTypeID"].ToString();
                }
                catch (Exception ex)
                {                   
                }
                 fillddlcategorySubType();
                 try
                 {
                     ddlcategorysubType.SelectedValue = dtsvr.Rows[0]["CategoryTypeSubID"].ToString();

                     if (ddlcategorysubType.SelectedValue == "1")
                     {
                         pnlnoofcompany.Visible = true;
                         txtmaxshare.Text = dtsvr.Rows[0]["NumberofActiveCompanySharedwith"].ToString();
                     }
                     else
                     {
                         pnlnoofcompany.Visible = false;
                     }

                 }
                 catch (Exception ex)
                 {
                 }   
                lbllegend1.Text = "Edit Portal Price Plan Category";
                pnladdnew.Visible = true;          
                btnadddd.Visible = false;
                Button1.Visible = false;
                Button2.Visible = true;
                ddlproductversion.Enabled = false;
                ddlportalname.Enabled = false;
            }
        }
        if (e.CommandName == "delete")
        {
            DataTable dtsvr = selectBZ(" select * from PricePlanMaster where PriceplancatId='" + e.CommandArgument.ToString() + "' ");

            if (dtsvr.Rows.Count > 0)
            {
                lblmsg.Text = "Sorry,This Category record exist in PricePlan";
            }
            else
            {
                             
                SqlCommand cmd = new SqlCommand("Priceplancategory_AddDeleteUpdate", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StatementType", "Delete");
                cmd.Parameters.AddWithValue("@ID", e.CommandArgument.ToString());
                con.Open();
                cmd.ExecuteNonQuery();
                lblmsg.Visible = true;
                lblmsg.Text = "Record deleted successfully";
                ddlstatus.SelectedIndex = 0;
                lbllegend1.Text = "";
                pnladdnew.Visible = false;
                btnadddd.Visible = true;
                fillgrid();
            }
        }
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        fillgrid();
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

    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder;
        fillgrid();
    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
   
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lbl1 = (Label)e.Row.FindControl("lblstatus");
            Label lbl2 = (Label)e.Row.FindControl("lblproductid2");
            Label lbl3 = (Label)e.Row.FindControl("lblproductid3");
            if (lbl1.Text == "True")
            {
                lbl2.Visible = true;
                lbl3.Visible = false;
            }
            if (lbl1.Text == "False")
            {
                lbl3.Visible = true;
                lbl2.Visible = false;
            }
        }
    }
    protected void btncancel_Click(object sender, EventArgs e)
    {
        ddlstatus.SelectedIndex = 0;
        lblmsg.Text = "";
        lbllegend1.Text = "";
        pnladdnew.Visible = false;
        btnadddd.Visible = true;
    }
    protected void btnadddd_Click(object sender, EventArgs e)
    {
        Button1.Visible = true;
        lblmsg.Text = "";
        lbllegend1.Text = "Add New Portal Price Plan Category";
        pnladdnew.Visible = true;
        Button2.Visible = false;
        txtcodetypecategory.Text = "";
        ddlstatus.SelectedIndex = 0;
        btnadddd.Visible = false;
        ddlproductversion.Enabled = true;
        ddlportalname.Enabled = true;
    }

    protected DataTable select(string str)
    {
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter dtp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        dtp.Fill(dt);

        return dt;

    }
    protected void btndosyncro_Click(object sender, EventArgs e)
    {
        string strcln = " SELECT distinct ProductMaster.ProductId ,ProductDetail.Active,VersionInfoMaster.VersionInfoId,ProductMaster.ProductName + ' : ' + VersionInfoMaster.VersionInfoName as productversion FROM ProductMaster inner join ProductDetail on ProductDetail.ProductId=ProductMaster.ProductId inner join VersionInfoMaster on ProductMaster.ProductId=VersionInfoMaster.ProductId where ClientMasterId=" + Session["ClientId"].ToString() + " and ProductDetail.Active='1' and ProductMaster.ProductId='"+ ddlproductversion.SelectedValue +"'  order  by productversion";
        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtclnvers = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtclnvers);
        string productid="";
        if (dtclnvers.Rows.Count > 0)
       {
           
       }

        int transf = 1;


        DataTable dt1 = select(" SELECT DISTINCT SatelliteSyncronisationrequiringTablesMaster.Id FROM ClientProductTableMaster INNER JOIN SatelliteSyncronisationrequiringTablesMaster ON ClientProductTableMaster.Id = SatelliteSyncronisationrequiringTablesMaster.TableID where SatelliteSyncronisationrequiringTablesMaster.Status='1' and ClientProductTableMaster.TableName='Priceplancategory' ");
        if (dt1.Rows.Count > 0)
        {
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                string datetim = DateTime.Now.ToString();
                string arqid = dt1.Rows[i]["Id"].ToString();

                string str22 = "Insert Into SyncronisationrequiredTbl(SatelliteSyncronisationrequiringTablesMasterID,DateandTime)Values('" + arqid + "','" + Convert.ToDateTime(datetim) + "')";
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                SqlCommand cmn = new SqlCommand(str22, con);
                cmn.ExecuteNonQuery();
                con.Close();

                DataTable dt121 = select("SELECT Max(ID) as ID from SyncronisationrequiredTbl where SatelliteSyncronisationrequiringTablesMasterID='" + arqid + "'");

                if (Convert.ToString(dt121.Rows[0]["ID"]) != "")
                {
                    DataTable dtcln = select("SELECT Distinct ServerMasterTbl.Id FROM ServerMasterTbl inner join ServerAssignmentMasterTbl on ServerAssignmentMasterTbl.ServerId=ServerMasterTbl.Id inner join  PricePlanMaster on PricePlanMaster.PricePlanId=ServerAssignmentMasterTbl.PricePlanId    where ServerMasterTbl.Status='1' and ServerAssignmentMasterTbl.Active='1' and PricePlanMaster.active='1' ");

                    for (int j = 0; j < dtcln.Rows.Count; j++)
                    {
                        if (con.State.ToString() != "Open")
                        {
                            con.Open();
                        }

                        string str223 = "Insert Into SateliteServerRequiringSynchronisationMasterTbl(SyncronisationrequiredTBlID,[servermasterID],[SynchronisationSuccessful],[SynchronisationSuccessfulDatetime])Values('" + dt121.Rows[0]["ID"] + "','" + dtcln.Rows[j]["Id"] + "','0','" + DateTime.Now.ToString() + "')";
                        SqlCommand cmn3 = new SqlCommand(str223, con);
                        cmn3.ExecuteNonQuery();
                        con.Close();
                      //  transf = Convert.ToInt32(rdsync.SelectedValue);
                    }
                }


            }

        }
        else
        {
            lblmsg.Visible = true;   
            lblmsg.Text = "Table Not Found ";
        }
        if (transf > 0)
        {
            string te = "SyncData.aspx";
            ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

        }


       
    }
}
