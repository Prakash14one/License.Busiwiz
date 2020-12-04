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
public partial class AddProduct : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);

    protected void Page_Load(object sender, EventArgs e)
    {
       // chkboxActiveDeactive.Checked=true;
        lblmsg.Text = "";
        if (!IsPostBack)
        {
            ViewState["sortOrder"] = "";
           
            if (Session["Login"] != null)
            {
                if (Session["Login"].ToString() == null)
                {
                    Response.Redirect("Login.aspx");
                }
            }
            else
            {
                Response.Redirect("Login.aspx");
            }
            filldddlsales();
            fillddlpdttype();
            filldddlsub();
            filldddlsubsub();
            //------Fill
            filldddlsalesfilter();
            fillddlpdttypeFilter();
            filldddlsubFilter();
            FillProductfilter();
            FillGrid();
        }
    }
    protected void Clr()
    {
        pnladdnew.Visible = false; 
        ddlsubsubtype.SelectedValue="0";

        txtproductmodelname.Text="";
        txtdescription.Text = "";
        txtspecification.Text="";
        txtDocumentaionURL.Text="";
        txtspecification.Text = "";
         txtGaudeURL.Text="";
        txtTechReferanceURL.Text="";
        txtproductmodelname.Text="";
        txtStartdate.Text="";
        txtEndDate.Text="";
        txtmodelWebURL.Text = "";
        chkboxActiveDeactive.Checked=false;

        txtvideourl.Text = "";
        lbl1.Text = ""; lbl2.Text = ""; lbl3.Text = ""; lbl4.Text = ""; lbl5.Text = ""; lbl6.Text = ""; lbl7.Text = ""; lbl8.Text = ""; lbl9.Text = ""; lbl10.Text = ""; lbl11.Text = ""; lbl12.Text = "";
        img1.ImageUrl = ""; img2.ImageUrl = ""; img3.ImageUrl = ""; img4.ImageUrl = ""; img5.ImageUrl = ""; img6.ImageUrl = ""; img7.ImageUrl = ""; img8.ImageUrl = ""; img9.ImageUrl = ""; img10.ImageUrl = ""; img11.ImageUrl = ""; img12.ImageUrl = "";  
    }

    //-------
    protected void filldddlsales()
    {
        string data = "select * from SalesTypeTbl Where Active='1' ";
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
        DropDownList3.Items.Insert(0, "--Select--");
        DropDownList3.Items[0].Value = "0";
    }
    protected void fillddlpdttype()
    {
        string data = "select * from Product_TypeTbl where SaleTypeTblID=" + DropDownList3.SelectedValue + " and ProductTypeName !='' and Active='1' order by ProductTypeName ";
        SqlCommand cmd = new SqlCommand(data, con);
        SqlDataAdapter sda = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        sda.Fill(dt);
      
            DropDownList2.DataSource = dt;
            DropDownList2.DataTextField = "ProductTypeName";
            DropDownList2.DataValueField = "ID";            
     
        DropDownList2.DataBind();
        DropDownList2.Items.Insert(0, "--Select--");
        DropDownList2.Items[0].Value = "0";

    }

    protected void filldddlsub()
    {
        string data = "select * from Product_SubTypeTbl Where ProductTypeTbl='" + DropDownList2.SelectedValue + "' and Active='1'  and Name !='' order by Name ";
        SqlCommand cmd = new SqlCommand(data, con);
        SqlDataAdapter sda = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        sda.Fill(dt);
       
            DropDownList4.DataSource = dt;
            DropDownList4.DataTextField = "Name";
            DropDownList4.DataValueField = "ID";
        
        DropDownList4.DataBind();
        DropDownList4.Items.Insert(0, "--Select--");
        DropDownList4.Items[0].Value = "0";
    }
    //

    protected void filldddlsubsub()
    {
        string str = "";
        if (DropDownList4.SelectedIndex > 0)
        {
            str = " and ProductSubTypeTblID='" + DropDownList4.SelectedValue + "' ";
        }
              string data = "select * from Product_SubSubTypeTbl Where Name !='' and Active='1' " + str + " order by Name ";
            SqlCommand cmd = new SqlCommand(data, con);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            ddlsubsubtype.DataSource = dt;
            ddlsubsubtype.DataTextField = "Name";
            ddlsubsubtype.DataValueField = "ID";
            ddlsubsubtype.DataBind();
            ddlsubsubtype.Items.Insert(0, "--Select--");
            ddlsubsubtype.Items[0].Value = "0";
    }

    //---Filter------------------------
    protected void filldddlsalesfilter()
    {
        string data = "select * from SalesTypeTbl Where Active='1' ";
        SqlCommand cmd = new SqlCommand(data, con);
        SqlDataAdapter sda = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        sda.Fill(dt);

        ddlsaletypefilter.DataSource = dt;
        ddlsaletypefilter.DataTextField = "Name";
        ddlsaletypefilter.DataValueField = "ID";
        ddlsaletypefilter.DataBind();

        ddlsaletypefilter.Items.Insert(0, "--Select--");
        ddlsaletypefilter.Items[0].Value = "0";
    }
    protected void fillddlpdttypeFilter()
    {
        string data = "select * from Product_TypeTbl where SaleTypeTblID=" + ddlsaletypefilter.SelectedValue + " and ProductTypeName !='' and Active='1' order by ProductTypeName ";
        SqlCommand cmd = new SqlCommand(data, con);
        SqlDataAdapter sda = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        sda.Fill(dt);

        ddlproductypefilter.DataSource = dt;
        ddlproductypefilter.DataTextField = "ProductTypeName";
        ddlproductypefilter.DataValueField = "ID";

        ddlproductypefilter.DataBind();
        ddlproductypefilter.Items.Insert(0, "--Select--");
        ddlproductypefilter.Items[0].Value = "0";

    }

    protected void filldddlsubFilter()
    {
        string data = "select * from Product_SubTypeTbl Where ProductTypeTbl='" + ddlproductypefilter.SelectedValue + "' and Active='1'  and Name !='' order by Name ";
        SqlCommand cmd = new SqlCommand(data, con);
        SqlDataAdapter sda = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        sda.Fill(dt);
        ddlsubtypefilter.DataSource = dt;
        ddlsubtypefilter.DataTextField = "Name";
        ddlsubtypefilter.DataValueField = "ID";
        ddlsubtypefilter.DataBind();
        ddlsubtypefilter.Items.Insert(0, "--Select--");
        ddlsubtypefilter.Items[0].Value = "0";
    }
    protected void FillProductfilter()
    {
        string str = "";
        if (ddlsubtypefilter.SelectedIndex > 0)
        {
            str = " and ProductSubTypeTbl='" + ddlsubtypefilter.SelectedValue + "' ";
        }
        string strcln = " select * from Product_SubSubTypeTbl Where Name !='' and Active='1' " + str + " order by Name ";
        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);
        ddlsubsubfilter.DataSource = dtcln;
        ddlsubsubfilter.DataTextField = "Name";
        ddlsubsubfilter.DataValueField = "ID";
        ddlsubsubfilter.DataBind();
        ddlsubsubfilter.Items.Insert(0, "--Select--");
        ddlsubsubfilter.Items[0].Value = "0";
        ddlsubsubfilter.SelectedIndex = 0;
    }
    
    protected void FillGrid()
    {
        GridView1.DataSource = null;
        GridView1.DataBind();
        string active;
        string deactive;

        string strcln = " select  dbo.Product_Model.*  FROM dbo.Product_Model INNER JOIN dbo.Product_SubSubTypeTbl ON dbo.Product_Model.ProductSubsubTypeId = dbo.Product_SubSubTypeTbl.ID INNER JOIN dbo.Product_SubTypeTbl ON dbo.Product_SubSubTypeTbl.ProductSubTypeTblID = dbo.Product_SubTypeTbl.ID INNER JOIN dbo.Product_TypeTbl ON dbo.Product_SubTypeTbl.ProductTypeTbl = dbo.Product_TypeTbl.ID INNER JOIN dbo.SalesTypeTbl ON dbo.Product_TypeTbl.SaleTypeTblID = dbo.SalesTypeTbl.ID where Product_Model.ProductSubsubTypeId !=''  ";
        
        if (ddlstatus.SelectedItem.Text == "Active")
        {
            active = " and Product_Model.Active='True'";
            strcln += active;
        }
        else if (ddlstatus.SelectedItem.Text == "Deactive")
        {
            deactive = " and Product_Model.Active='False'";
            strcln += deactive;
        }
        //-------------------------------       
        if (ddlsubtypefilter.SelectedIndex > 0)
        {
            strcln += " and dbo.Product_SubTypeTbl.ID=" + ddlsubtypefilter.SelectedValue + "";
        }
        if (ddlproductypefilter.SelectedIndex > 0)
        {
            strcln += " and dbo.Product_TypeTbl.ID=" + ddlproductypefilter.SelectedValue + "";
        }
        if (ddlsaletypefilter.SelectedIndex > 0)
        {
            strcln += " and  dbo.SalesTypeTbl.ID=" + ddlsaletypefilter.SelectedValue + "";
        }       
     //---------------
        if (ddlsubsubfilter.SelectedIndex > 0)
        {
            strcln += " and Product_Model.ProductSubsubTypeId=" + ddlsubsubfilter.SelectedValue + "";
        }
        strcln += " Order By Id Desc ";
        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);
        GridView1.DataSource = dtcln;
        DataView myDataView = new DataView();
        myDataView = dtcln.DefaultView;

        if (hdnsortExp.Value != string.Empty)
        {
            myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
        }
        GridView1.DataBind();
    }
  

    protected void btnSubmit_Click(object sender, EventArgs e)
    {      
               
        try
        {
            if (btnSubmit.Text == "Update")
            {

                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("Insert_Product_Model", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@StatementType", "Update");
                cmd.Parameters.AddWithValue("@ID", ViewState["ID"]);
                cmd.Parameters.AddWithValue("@ProductSubsubTypeId", ddlsubsubtype.SelectedValue);
                cmd.Parameters.AddWithValue("@ProductModelName", txtproductmodelname.Text);
                cmd.Parameters.AddWithValue("@ProductModelDesc", txtdescription.Text);
                cmd.Parameters.AddWithValue("@ProductModelSpecification", txtspecification.Text);
                cmd.Parameters.AddWithValue("@DocumentaionURL", txtDocumentaionURL.Text);
                cmd.Parameters.AddWithValue("@UserGaudeURL", txtGaudeURL.Text);
                cmd.Parameters.AddWithValue("@TechnicalReferanceURL", txtTechReferanceURL.Text);
                cmd.Parameters.AddWithValue("@ProductModelWebURL", txtproductmodelname.Text);
                cmd.Parameters.AddWithValue("@ModelStartDate", txtStartdate.Text);
                cmd.Parameters.AddWithValue("@ModelRetiredDate", txtEndDate.Text);
                cmd.Parameters.AddWithValue("@Active", chkboxActiveDeactive.Checked);
                cmd.ExecuteNonQuery();
                con.Close();
                //-*-------------------------------------------------------------------------------------------------
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmd = new SqlCommand("Product_ProductModelImgTbl_AddDelUpdtSelect", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@StatementType", "Update");                
                cmd.Parameters.AddWithValue("@ProductModelID", ViewState["ID"]);
                cmd.Parameters.AddWithValue("@ImageSmallFront", lbl1.Text);
                cmd.Parameters.AddWithValue("@ImageBiglFront", lbl2.Text);
                cmd.Parameters.AddWithValue("@ImageSmallBack", lbl3.Text);
                cmd.Parameters.AddWithValue("@ImageBiglBack", lbl4.Text);
                cmd.Parameters.AddWithValue("@ImageSmallTop", lbl5.Text);
                cmd.Parameters.AddWithValue("@ImageBigTop", lbl6.Text);
                cmd.Parameters.AddWithValue("@ImageSmallBottom", lbl7.Text);
                cmd.Parameters.AddWithValue("@ImageBigBottom", lbl8.Text);
                cmd.Parameters.AddWithValue("@ImageSmallRight", lbl9.Text);
                cmd.Parameters.AddWithValue("@ImageBigRight", lbl10.Text);
                cmd.Parameters.AddWithValue("@ImageSmallLeft", lbl11.Text);
                cmd.Parameters.AddWithValue("@ImageBigLeft", lbl12.Text);
                cmd.Parameters.AddWithValue("@VideoURL", txtvideourl.Text);
                //------------------------------------------------------------------------------------------------------

                lblmsg.Visible = true;
                lblmsg.Text = "Record updated successfully";
                Clr();
                FillGrid();
                        
            }
            else
            {
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("Insert_Product_Model", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@StatementType", "Insert");
                cmd.Parameters.AddWithValue("@ProductSubsubTypeId", ddlsubsubtype.SelectedValue);
                cmd.Parameters.AddWithValue("@ProductModelName", txtproductmodelname.Text);
                cmd.Parameters.AddWithValue("@ProductModelDesc", txtdescription.Text);
                cmd.Parameters.AddWithValue("@ProductModelSpecification", txtspecification.Text);
                cmd.Parameters.AddWithValue("@DocumentaionURL", txtDocumentaionURL.Text);
                cmd.Parameters.AddWithValue("@UserGaudeURL", txtGaudeURL.Text);
                cmd.Parameters.AddWithValue("@TechnicalReferanceURL", txtTechReferanceURL.Text);
                cmd.Parameters.AddWithValue("@ProductModelWebURL", txtproductmodelname.Text);
                cmd.Parameters.AddWithValue("@ModelStartDate", txtStartdate.Text);
                cmd.Parameters.AddWithValue("@ModelRetiredDate", txtEndDate.Text);
                cmd.Parameters.AddWithValue("@Active", chkboxActiveDeactive.Checked);
                cmd.ExecuteNonQuery();
                con.Close();
                //-------------------------------------------------------
                string strmax = " Select Max(ID) as ID from Product_Model";
                SqlCommand cmdmax = new SqlCommand(strmax, con);
                DataTable dtmax = new DataTable();
                SqlDataAdapter adpmax = new SqlDataAdapter(cmdmax);
                adpmax.Fill(dtmax);
                string id = "";
                if (dtmax.Rows.Count > 0)
                {
                    id = dtmax.Rows[0]["Id"].ToString();
                }
                //------------------------------------------------------------------------------------
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmd = new SqlCommand("Product_ProductModelImgTbl_AddDelUpdtSelect", con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@StatementType", "Insert");
                cmd.Parameters.AddWithValue("@ProductModelID", id);
                cmd.Parameters.AddWithValue("@ImageSmallFront", lbl1.Text);
                cmd.Parameters.AddWithValue("@ImageBiglFront", lbl2.Text);
                cmd.Parameters.AddWithValue("@ImageSmallBack", lbl3.Text);
                cmd.Parameters.AddWithValue("@ImageBiglBack", lbl4.Text);
                cmd.Parameters.AddWithValue("@ImageSmallTop", lbl5.Text);
                cmd.Parameters.AddWithValue("@ImageBigTop", lbl6.Text);
                cmd.Parameters.AddWithValue("@ImageSmallBottom", lbl7.Text);
                cmd.Parameters.AddWithValue("@ImageBigBottom", lbl8.Text);
                cmd.Parameters.AddWithValue("@ImageSmallRight", lbl9.Text);
                cmd.Parameters.AddWithValue("@ImageBigRight", lbl10.Text);
                cmd.Parameters.AddWithValue("@ImageSmallLeft", lbl11.Text);
                cmd.Parameters.AddWithValue("@ImageBigLeft", lbl12.Text);
                cmd.Parameters.AddWithValue("@VideoURL", txtvideourl.Text);
                cmd.ExecuteNonQuery();
                //--------------------------------------------------------------------------------------

                lblmsg.Visible = true;
                lblmsg.Text = "Record inserted successfully";
                Clr();
                FillGrid();
            }
        }
        catch (Exception eerr)
        {
            lblmsg.Visible = true;
            lblmsg.Text = "Error : " + eerr.Message;

        }



    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "edit1")
        {
          
            pnladdnew.Visible = true;
            Label19.Text = "Edit Product Model";
            lblmsg.Text = "";
            GridView1.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            int i = Convert.ToInt32(GridView1.DataKeys[GridView1.SelectedIndex].Value.ToString());        
         
                string strcln = "Select * From Product_Model Where ID='"+i.ToString()+"'";
                hdnProductDetailId.Value = i.ToString();

                SqlCommand cmdcln = new SqlCommand(strcln, con);
                DataTable dtcln = new DataTable();
                SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
                adpcln.Fill(dtcln);               
                if (dtcln.Rows.Count > 0)
                {
                    ViewState["ID"] = dtcln.Rows[0]["ID"].ToString();
                    hdnProductId.Value = dtcln.Rows[0]["ID"].ToString();

                    ddlsubsubtype.SelectedValue = dtcln.Rows[0]["ProductSubsubTypeId"].ToString();
                    txtproductmodelname.Text  = dtcln.Rows[0]["ProductModelName"].ToString();
                    txtdescription.Text = dtcln.Rows[0]["ProductModelDesc"].ToString();
                    txtspecification.Text = dtcln.Rows[0]["ProductModelSpecification"].ToString();
                    txtDocumentaionURL.Text = dtcln.Rows[0]["DocumentaionURL"].ToString();
                    txtGaudeURL.Text = dtcln.Rows[0]["UserGaudeURL"].ToString();
                    txtTechReferanceURL.Text  = dtcln.Rows[0]["TechnicalReferanceURL"].ToString();
                    chkboxActiveDeactive.Checked = Convert.ToBoolean(dtcln.Rows[0]["Active"].ToString());
                    try
                    {
                        txtStartdate.Text = Convert.ToDateTime(dtcln.Rows[0]["ModelStartDate"].ToString()).ToShortDateString();
                        txtEndDate.Text = Convert.ToDateTime(dtcln.Rows[0]["ModelRetiredDate"].ToString()).ToShortDateString();
                    }
                    catch (Exception ex)
                    {
                    }
                    
                    txtproductmodelname.Text = dtcln.Rows[0]["ProductModelWebURL"].ToString();
                    //------------------------

                    strcln = "Select * From Product_ModelImgTbl Where ProductModelID='" + i.ToString() + "'";
                    cmdcln = new SqlCommand(strcln, con);
                    dtcln = new DataTable();
                    adpcln = new SqlDataAdapter(cmdcln);
                    adpcln.Fill(dtcln);
                    if (dtcln.Rows.Count > 0)
                    {
                        lbl1.Text = dtcln.Rows[0]["ImageSmallFront"].ToString();
                        lbl2.Text = dtcln.Rows[0]["ImageBiglFront"].ToString();
                        lbl3.Text = dtcln.Rows[0]["ImageSmallBack"].ToString();
                        lbl4.Text = dtcln.Rows[0]["ImageBiglBack"].ToString();
                        lbl5.Text = dtcln.Rows[0]["ImageSmallTop"].ToString();
                        lbl6.Text = dtcln.Rows[0]["ImageBigTop"].ToString();
                        lbl7.Text = dtcln.Rows[0]["ImageSmallBottom"].ToString();
                        lbl8.Text = dtcln.Rows[0]["ImageBigBottom"].ToString();
                        lbl9.Text = dtcln.Rows[0]["ImageSmallRight"].ToString();
                        lbl10.Text = dtcln.Rows[0]["ImageBigRight"].ToString();
                        lbl11.Text = dtcln.Rows[0]["ImageSmallLeft"].ToString();
                        lbl12.Text = dtcln.Rows[0]["ImageBigLeft"].ToString();
                        txtvideourl.Text = dtcln.Rows[0]["VideoURL"].ToString();
                        img1.ImageUrl = "~\\images\\" + dtcln.Rows[0]["ImageSmallFront"].ToString();
                        img2.ImageUrl = "~\\images\\" + dtcln.Rows[0]["ImageBiglFront"].ToString();
                        img3.ImageUrl = "~\\images\\" + dtcln.Rows[0]["ImageSmallBack"].ToString();
                        img4.ImageUrl = "~\\images\\" + dtcln.Rows[0]["ImageBiglBack"].ToString();
                        img5.ImageUrl = "~\\images\\" + dtcln.Rows[0]["ImageSmallTop"].ToString();
                        img6.ImageUrl = "~\\images\\" + dtcln.Rows[0]["ImageBigTop"].ToString();
                        img7.ImageUrl = "~\\images\\" + dtcln.Rows[0]["ImageSmallBottom"].ToString();
                        img8.ImageUrl = "~\\images\\" + dtcln.Rows[0]["ImageBigBottom"].ToString();
                        img9.ImageUrl = "~\\images\\" + dtcln.Rows[0]["ImageSmallRight"].ToString();
                        img10.ImageUrl = "~\\images\\" + dtcln.Rows[0]["ImageBigRight"].ToString();
                        img11.ImageUrl = "~\\images\\" + dtcln.Rows[0]["ImageSmallLeft"].ToString();
                        img12.ImageUrl = "~\\images\\" + dtcln.Rows[0]["ImageBigLeft"].ToString();
                    }
                    btnSubmit.Text = "Update";                   
            }
        }


        if (e.CommandName == "Delete")
        {
            int mm = Convert.ToInt32(e.CommandArgument);
            string finalstr = "Select * from  Product_BatchMaster where ProductModelID='" + mm + "'";
            SqlCommand cmdcln = new SqlCommand(finalstr, con);
            SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
            DataTable dtcln = new DataTable();
            adpcln.Fill(dtcln);
            if (dtcln.Rows.Count == 0)
            {
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                SqlCommand cmd = new SqlCommand("Insert_Product_Model", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StatementType", "Delete");
                cmd.Parameters.AddWithValue("@ID", mm);
                cmd.ExecuteNonQuery();
                con.Close();
                //----------------------------------------
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmd = new SqlCommand("Product_ProductModelImgTbl_AddDelUpdtSelect", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@StatementType", "Delete");
                cmd.Parameters.AddWithValue("@ID", mm);
                cmd.ExecuteNonQuery();
                con.Close();
                //----------------------------------------------------------------

                lblmsg.Visible = true;
                lblmsg.Text = "Record deleted successfully";
                FillGrid();
            }
            else
            {
                lblmsg.Visible = true;
                lblmsg.Text = "Sorry, You are not allow delete this record,first delete chield record.";
            }
        }
        

    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        FillGrid();
    }
    protected void GridView1_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
    }
    protected void GridView1_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
    }
   
   
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        GridViewRow row = ((LinkButton)sender).Parent.Parent as GridViewRow;
        int rinrow = row.RowIndex;
        Label ctrl = (Label)GridView1.Rows[rinrow].FindControl("Labellink");    
        ClientScript.RegisterStartupScript(this.GetType(), "OpenWin", "<script>OpenNewWin('../"+"http://"+"" + ctrl.Text + "')</script>");
         
    }
    protected void LinkButton11_Click(object sender, EventArgs e)
    {        
        GridViewRow row = ((LinkButton)sender).Parent.Parent as GridViewRow;      
        int rinrow = row.RowIndex;
        Label ctrl = (Label)GridView1.Rows[rinrow].FindControl("Labellink1");       
        ClientScript.RegisterStartupScript(this.GetType(), "OpenWin", "<script>OpenNewWin('../"+"http://"+ "" + ctrl.Text + "')</script>");
        //"http://safestmall.indiaauthentic.com/ShoppingCart/Default.aspx?Cid=111&Wid=4" target="_blank" >
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
        FillGrid();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        btnSubmit.Text = "Submit";
        addnewpanel.Visible = true;
        pnladdnew.Visible = false;
        Label19.Text = "";
    }
   
   
 

    protected void Button1_Click1(object sender, EventArgs e)
    {
        if (Button3.Text == "Printable Version")
        {
            pnlgrid.ScrollBars = ScrollBars.None;
            pnlgrid.Height = new Unit("100%");

            Button3.Text = "Hide Printable Version";
            Button4.Visible = true;
            if (GridView1.Columns[7].Visible == true)
            {
                ViewState["editHide"] = "tt";
                GridView1.Columns[7].Visible = false;
            }
       
        }
        else
        {
            Button3.Text = "Printable Version";
            Button4.Visible = false;
            if (ViewState["editHide"] != null)
            {
                GridView1.Columns[7].Visible = true;
            }
         
        }
    }
    protected void addnewpanel_Click(object sender, EventArgs e)
    {
        pnladdnew.Visible = true;
        addnewpanel.Visible = true;
        lblmsg.Text = "";
        Label19.Text = "Add New Product Model";
    }

    protected void Button5_Click(object sender, EventArgs e)
    {
       
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {


    }

    protected void Button6_Click(object sender, EventArgs e)
    {
      
    }







    //*---------------------------------
    //fileUplod
    public bool extcheck(string filename)
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

    protected void imgsmallfront_Click(object sender, EventArgs e)
    {
        bool valid = extcheck(fu1_smallfront.FileName);

        if (valid == true)
        {

            if (fu1_smallfront.HasFile == true)
            {
                if (Directory.Exists(Server.MapPath("~\\images\\")) == false)
                {
                    Directory.CreateDirectory(Server.MapPath("~\\images\\"));
                }
                //FileUpload13.PostedFile.SaveAs(Server.MapPath("~\\images\\") + FileUpload13.FileName);
                string ext = System.IO.Path.GetExtension(fu1_smallfront.FileName);
                string imgname = txtproductmodelname.Text + "FrontSmall" + ext;

                fu1_smallfront.PostedFile.SaveAs(Server.MapPath("~\\images\\") + imgname);
                img1.ImageUrl = "~\\images\\" + imgname;
                lbl1.Text = imgname;

            }
        }
    }
    protected void imgbigfront_Click(object sender, EventArgs e)
    {
        bool valid = extcheck(fu2_bigfront.FileName);

        if (valid == true)
        {

            if (fu2_bigfront.HasFile == true)
            {
                if (Directory.Exists(Server.MapPath("~\\images\\")) == false)
                {
                    Directory.CreateDirectory(Server.MapPath("~\\images\\"));
                }
                string ext = System.IO.Path.GetExtension(fu2_bigfront.FileName);
                string imgname = txtproductmodelname.Text + "bigfront" + ext;
                fu2_bigfront.PostedFile.SaveAs(Server.MapPath("~\\images\\") + imgname);
                img2.ImageUrl = "~\\images\\" + imgname;
                lbl2.Text = imgname;

            }
        }
    }

    protected void imgsmallback_Click(object sender, EventArgs e)
    {
        bool valid = extcheck(fu3_smallback.FileName);

        if (valid == true)
        {

            if (fu3_smallback.HasFile == true)
            {
                if (Directory.Exists(Server.MapPath("~\\images\\")) == false)
                {
                    Directory.CreateDirectory(Server.MapPath("~\\images\\"));
                }
                string ext = System.IO.Path.GetExtension(fu3_smallback.FileName);
                string imgname = txtproductmodelname.Text + "smallback" + ext;
                fu3_smallback.PostedFile.SaveAs(Server.MapPath("~\\images\\") + imgname);
                img3.ImageUrl = "~\\images\\" + imgname;
                lbl3.Text = imgname;

            }
        }
    }
    protected void imgbigback_Click(object sender, EventArgs e)
    {
        bool valid = extcheck(fu4_bigback.FileName);

        if (valid == true)
        {

            if (fu4_bigback.HasFile == true)
            {
                if (Directory.Exists(Server.MapPath("~\\images\\")) == false)
                {
                    Directory.CreateDirectory(Server.MapPath("~\\images\\"));
                }
                // 
                string ext = System.IO.Path.GetExtension(fu4_bigback.FileName);
                string imgname = txtproductmodelname.Text + "bigback" + ext;
                fu4_bigback.PostedFile.SaveAs(Server.MapPath("~\\images\\") + imgname);
                img4.ImageUrl = "~\\images\\" + imgname;
                lbl4.Text = imgname;
            }
        }
    }

    protected void imgsmalltop_Click(object sender, EventArgs e)
    {
        bool valid = extcheck(fu5_smalltop.FileName);

        if (valid == true)
        {

            if (fu5_smalltop.HasFile == true)
            {
                if (Directory.Exists(Server.MapPath("~\\images\\")) == false)
                {
                    Directory.CreateDirectory(Server.MapPath("~\\images\\"));
                }
                //  
                string ext = System.IO.Path.GetExtension(fu5_smalltop.FileName);
                string imgname = txtproductmodelname.Text + "smalltop" + ext;
                fu5_smalltop.PostedFile.SaveAs(Server.MapPath("~\\images\\") + imgname);
                img5.ImageUrl = "~\\images\\" + imgname;
                lbl5.Text = imgname;

            }
        }
    }
    protected void imgbigtop_Click(object sender, EventArgs e)
    {
        bool valid = extcheck(fu6_bigtop.FileName);

        if (valid == true)
        {

            if (fu6_bigtop.HasFile == true)
            {
                if (Directory.Exists(Server.MapPath("~\\images\\")) == false)
                {
                    Directory.CreateDirectory(Server.MapPath("~\\images\\"));
                }

                string ext = System.IO.Path.GetExtension(fu6_bigtop.FileName);
                string imgname = txtproductmodelname.Text + "bigtop" + ext;
                fu6_bigtop.PostedFile.SaveAs(Server.MapPath("~\\images\\") + imgname);
                img6.ImageUrl = "~\\images\\" + imgname;
                lbl6.Text = imgname;

            }
        }
    }

    protected void imgsmallbottom_Click(object sender, EventArgs e)
    {
        bool valid = extcheck(fu7_smallbottom.FileName);

        if (valid == true)
        {

            if (fu7_smallbottom.HasFile == true)
            {
                if (Directory.Exists(Server.MapPath("~\\images\\")) == false)
                {
                    Directory.CreateDirectory(Server.MapPath("~\\images\\"));
                }

                string ext = System.IO.Path.GetExtension(fu7_smallbottom.FileName);
                string imgname = txtproductmodelname.Text + "smallbottom" + ext;
                fu7_smallbottom.PostedFile.SaveAs(Server.MapPath("~\\images\\") + imgname);
                img7.ImageUrl = "~\\images\\" + imgname;
                lbl7.Text = imgname;

            }
        }
    }
    protected void imgbigbottom_Click(object sender, EventArgs e)
    {
        bool valid = extcheck(fu8_bigbottom.FileName);

        if (valid == true)
        {

            if (fu8_bigbottom.HasFile == true)
            {
                if (Directory.Exists(Server.MapPath("~\\images\\")) == false)
                {
                    Directory.CreateDirectory(Server.MapPath("~\\images\\"));
                }
                string ext = System.IO.Path.GetExtension(fu8_bigbottom.FileName);
                string imgname = txtproductmodelname.Text + "bigbottom" + ext;
                fu8_bigbottom.PostedFile.SaveAs(Server.MapPath("~\\images\\") + imgname);
                img8.ImageUrl = "~\\images\\" + imgname;
                lbl8.Text = imgname;

            }
        }
    }

    protected void imgsmallright_Click(object sender, EventArgs e)
    {
        bool valid = extcheck(fu9_smallright.FileName);

        if (valid == true)
        {

            if (fu9_smallright.HasFile == true)
            {
                if (Directory.Exists(Server.MapPath("~\\images\\")) == false)
                {
                    Directory.CreateDirectory(Server.MapPath("~\\images\\"));
                }

                string ext = System.IO.Path.GetExtension(fu9_smallright.FileName);
                string imgname = txtproductmodelname.Text + "smallright" + ext;
                fu9_smallright.PostedFile.SaveAs(Server.MapPath("~\\images\\") + imgname);
                img9.ImageUrl = "~\\images\\" + imgname;
                lbl9.Text = imgname;

            }
        }
    }
    protected void imgbigright_Click(object sender, EventArgs e)
    {
        bool valid = extcheck(fu10_bigright.FileName);

        if (valid == true)
        {

            if (fu10_bigright.HasFile == true)
            {
                if (Directory.Exists(Server.MapPath("~\\images\\")) == false)
                {
                    Directory.CreateDirectory(Server.MapPath("~\\images\\"));
                }

                string ext = System.IO.Path.GetExtension(fu10_bigright.FileName);
                string imgname = txtproductmodelname.Text + "bigright" + ext;
                fu10_bigright.PostedFile.SaveAs(Server.MapPath("~\\images\\") + imgname);
                img10.ImageUrl = "~\\images\\" + imgname;
                lbl10.Text = imgname;

            }
        }
    }

    protected void imgsmallleft_Click(object sender, EventArgs e)
    {
        bool valid = extcheck(fu11_smallleft.FileName);

        if (valid == true)
        {

            if (fu11_smallleft.HasFile == true)
            {
                if (Directory.Exists(Server.MapPath("~\\images\\")) == false)
                {
                    Directory.CreateDirectory(Server.MapPath("~\\images\\"));
                }
                //  
                string ext = System.IO.Path.GetExtension(fu11_smallleft.FileName);
                string imgname = txtproductmodelname.Text + "smallleft" + ext;
                fu11_smallleft.PostedFile.SaveAs(Server.MapPath("~\\images\\") + imgname);
                img11.ImageUrl = "~\\images\\" + imgname;
                lbl11.Text = imgname;

            }
        }
    }

    protected void imgbigleft_Click(object sender, EventArgs e)
    {
        bool valid = extcheck(fu12_bigleft.FileName);

        if (valid == true)
        {

            if (fu12_bigleft.HasFile == true)
            {
                if (Directory.Exists(Server.MapPath("~\\images\\")) == false)
                {
                    Directory.CreateDirectory(Server.MapPath("~\\images\\"));
                }
                string ext = System.IO.Path.GetExtension(fu12_bigleft.FileName);
                string imgname = txtproductmodelname.Text + "bigleft" + ext;
                fu12_bigleft.PostedFile.SaveAs(Server.MapPath("~\\images\\") + imgname);
                lbl12.Text = imgname;
                img12.ImageUrl = "~\\images\\" + imgname;


            }
        }
    }

    //---Uplod URL

    protected void btnUpload_Click1(object sender, EventArgs e)
    {      
        if (FileUpload1.HasFile)
        {           
            string filename = Path.GetFileName(FileUpload1.FileName);
            FileUpload1.SaveAs(Server.MapPath("~\\images\\") + filename);
            txtDocumentaionURL.Text = "http://license.busiwiz.com/images/" + filename + "";
            if (txtDocumentaionURL.Text != "")
            {
                btnadd.Text = "Change";
                FileUpload1.Visible = false;
                btnUpload.Visible = false;
            }
        }
    }
    protected void btnadd_Click1(object sender, EventArgs e)
    {
        if (btnadd.Text == "Change")
        {
            btnadd.Text = "Add";
        }
        else if (btnadd.Text == "Add")
        {
            FileUpload1.Visible = true;
            btnUpload.Visible = true;
            btnadd.Text = "Close";
        }
        else if (btnadd.Text == "Close")
        {
            btnadd.Text = "Add";
            FileUpload1.Visible = false;
            btnUpload.Visible = false;
        }
    }
    protected void btnUpload_Click2(object sender, EventArgs e)
    {       
        if (FileUpload2.HasFile)
        {
            string filename = Path.GetFileName(FileUpload2.FileName);
            FileUpload2.SaveAs(Server.MapPath("~\\images\\") + filename);
            txtGaudeURL.Text = "http://license.busiwiz.com/images/" + filename + "";
            if (txtGaudeURL.Text != "")
            {
                btnadd2.Text = "Change";
                FileUpload2.Visible = false;
                btnUpload2.Visible = false;
            }
        }
    }
    protected void btnadd_Click2(object sender, EventArgs e)
    {
        if (btnadd2.Text == "Change")
        {
            btnadd2.Text = "Add";
        }
        else if (btnadd2.Text == "Add")
        {
            FileUpload2.Visible = true;
            btnUpload2.Visible = true;
            btnadd2.Text = "Close";
        }
        else if (btnadd2.Text == "Close")
        {
            btnadd2.Text = "Add";
            FileUpload2.Visible = false;
            btnUpload2.Visible = false;
        }
    }
    protected void btnUpload_Click13(object sender, EventArgs e)
    {
        if (FileUpload13.HasFile)
        {
            string filename = Path.GetFileName(FileUpload13.FileName);
            FileUpload13.SaveAs(Server.MapPath("~\\images\\") + filename);
            txtTechReferanceURL.Text = "http://license.busiwiz.com/images/" + filename + "";
            if (txtTechReferanceURL.Text != "")
            {
                btnadd13.Text = "Change";
                FileUpload13.Visible = false;
                Button13.Visible = false;
            }
        }
    }
    protected void btnadd_Click13(object sender, EventArgs e)
    {
        if (btnadd13.Text == "Change")
        {
            btnadd13.Text = "Add";
        }
        else if (btnadd13.Text == "Add")
        {
            FileUpload13.Visible = true;
            Button13.Visible = true;
            btnadd13.Text = "Close";
        }
        else if (btnadd13.Text == "Close")
        {
            btnadd13.Text = "Add";
            FileUpload13.Visible = false;
            Button13.Visible = false;
        }
    }
    //------------------

    protected void btnUpload_Click14(object sender, EventArgs e)
    {
        if (FileUpload14.HasFile)
        {
            string filename = Path.GetFileName(FileUpload14.FileName);
            FileUpload14.SaveAs(Server.MapPath("~\\images\\") + filename);
            txtmodelWebURL.Text = "http://license.busiwiz.com/images/" + filename + "";
            if (txtmodelWebURL.Text!="")
            {
                btnadd14.Text = "Change";
                FileUpload14.Visible = false;
                Button14.Visible = false;
            }
        }
    }
    protected void btnadd_Click14(object sender, EventArgs e)
    {
        if (btnadd14.Text == "Change")
        {
            btnadd14.Text = "Add";
        }
        else if (btnadd14.Text == "Add")
        {
            FileUpload14.Visible = true;
            Button14.Visible = true;
            btnadd14.Text = "Close";
        }
        else if (btnadd14.Text == "Close")
        {
            btnadd14.Text = "Add";
            FileUpload14.Visible = false;
            Button14.Visible = false;
        }
    }
    protected void btnUpload_Click15(object sender, EventArgs e)
    {
        if (FileUpload15.HasFile)
        {
            string filename = Path.GetFileName(FileUpload15.FileName);
            FileUpload15.SaveAs(Server.MapPath("~\\images\\") + filename);
            txtvideourl.Text = "http://license.busiwiz.com/images/" + filename + "";
            if (txtvideourl.Text != "")
            {
                btnadd15.Text = "Change";
                FileUpload15.Visible = false;
                Button15.Visible = false;
            }
        }
    }
    protected void btnadd_Click15(object sender, EventArgs e)
    {
        if (btnadd15.Text == "Change")
        {
            btnadd15.Text = "Add";
        }
        else if  (btnadd15.Text == "Add")
        {
            FileUpload15.Visible = true;
            Button15.Visible = true;
            btnadd15.Text = "Close";
        }
        else if (btnadd15.Text == "Close")
        {
            btnadd15.Text = "Add";
            FileUpload15.Visible = false;
            Button15.Visible = false;
        }
    }

    //----------------------------------
    protected void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillddlpdttype();
    }

    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        filldddlsub();        
    }
 
    protected void DropDownList4_SelectedIndexChanged(object sender, EventArgs e)
    {
        filldddlsubsub();
    }
    protected void DropDownList5_SelectedIndexChanged(object sender, EventArgs e)
    {
        string data = "select ProductSubTypeTblID as ProductSubTypeTblID from Product_SubSubTypeTbl Where  id='" + ddlsubsubtype.SelectedValue + "' ";
        SqlCommand cmd = new SqlCommand(data, con);
        SqlDataAdapter sda = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        sda.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            string subtype = dt.Rows[0]["ProductSubTypeTblID"].ToString();
           data = "    select ProductTypeTbl as ProductTypeTbl from Product_SubTypeTbl Where  id='" + dt.Rows[0]["ProductSubTypeTblID"].ToString() + "' ";
             cmd = new SqlCommand(data, con);
             sda = new SqlDataAdapter(cmd);
             dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
               string prodtype= dt.Rows[0]["ProductTypeTbl"].ToString();
                data = " select SaleTypeTblID as SaleTypeTblID from Product_TypeTbl  Where id='" + dt.Rows[0]["ProductTypeTbl"].ToString() + "' ";
                cmd = new SqlCommand(data, con);
                sda = new SqlDataAdapter(cmd);
                dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    filldddlsales();                   
                    DropDownList3.SelectedValue = dt.Rows[0]["SaleTypeTblID"].ToString();
                    fillddlpdttype();                    
                 DropDownList2.SelectedValue  = prodtype;
                 filldddlsub();                 
                 DropDownList4.SelectedValue = subtype;
                      
                }
            }

        }
    }

    //----------------------

    protected void ddlsaletypefilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillddlpdttypeFilter();
    }
    protected void ddlproductypefilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        filldddlsubFilter();
      }
    protected void ddlsubtypefilter_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillProductfilter();
    }
    protected void ddlstatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        string data = "select ProductSubTypeTblID as ProductSubTypeTblID from Product_SubSubTypeTbl  where id='" + ddlsubsubfilter.SelectedValue + "' ";
        SqlCommand cmd = new SqlCommand(data, con);
        SqlDataAdapter sda = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        sda.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            string subtype = dt.Rows[0]["ProductSubTypeTblID"].ToString();
            data = "    select ProductTypeTbl as ProductTypeTbl from Product_SubTypeTbl Where  id='" + dt.Rows[0]["ProductSubTypeTblID"].ToString() + "' ";
            cmd = new SqlCommand(data, con);
            sda = new SqlDataAdapter(cmd);
            dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                string prodtype = dt.Rows[0]["ProductTypeTbl"].ToString();
                data = " select SaleTypeTblID as SaleTypeTblID from Product_TypeTbl  Where id='" + dt.Rows[0]["ProductTypeTbl"].ToString() + "' ";
                cmd = new SqlCommand(data, con);
                sda = new SqlDataAdapter(cmd);
                dt = new DataTable();
                sda.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    filldddlsalesfilter();                    
                    ddlsaletypefilter.SelectedValue = dt.Rows[0]["SaleTypeTblID"].ToString();
                    fillddlpdttypeFilter();
                    ddlproductypefilter.SelectedValue = prodtype;
                    filldddlsubFilter();
                    ddlsubtypefilter.SelectedValue = subtype;
                }
            }

        }
        FillGrid();
    }

    protected void BtnGo_Click(object sender, EventArgs e)
    {
        FillGrid();
    }
    protected void txtEndDate_TextChanged(object sender, EventArgs e)
    {
        if (Convert.ToDateTime(txtEndDate.Text) < Convert.ToDateTime(txtStartdate.Text))
        {
            lblenddateerror.Text = "The Retired Date must be greater than the start date.";
            txtEndDate.Text = "";
        }
        else
        {
            lblenddateerror.Text = "";
        }
    }
}
