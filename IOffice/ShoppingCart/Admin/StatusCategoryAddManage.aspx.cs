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

public partial class Add_Inventory_Master : System.Web.UI.Page
{
    //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
    SqlConnection con;
    string compid;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Comid"] == null)
        {
            Response.Redirect("~/Shoppingcart/Admin/ShoppingCartLogin.aspx");
        }

        PageConn pgcon = new PageConn();
        con = pgcon.dynconn;
        GridView1.PagerSettings.Visible = true;
        pagetitleclass pg = new pagetitleclass();
        string strData = Request.Url.ToString();

        char[] separator = new char[] { '/' };

        string[] strSplitArr = strData.Split(separator);
        int i = Convert.ToInt32(strSplitArr.Length);
        string page = strSplitArr[i - 1].ToString();

        compid = Session["Comid"].ToString();
        Page.Title = pg.getPageTitle(page);
        // Label1.Visible = false;
        if (!IsPostBack)
        {

            Pagecontrol.dypcontrol(Page, page);
            ViewState["sortOrder"] = "";


            // ModalPopupExtender1.Hide();
            //ModalPopupExtender1222.Hide();
            FillGridView1();
        }
    }

    protected void FillGridView1()
    {

        lblCompany.Text = Session["Cname"].ToString();
        string str = " SELECT StatusCategory,StatusCategoryMasterId FROM StatusCategory where compid='" + compid + "' order by StatusCategory";
        //string strfillgrid = "SELECT StatusCategory,StatusCategoryMasterId FROM StatusCategory order by StatusCategory";
        SqlCommand cmdfillgrid = new SqlCommand(str, con);
        SqlDataAdapter adpfillgrid = new SqlDataAdapter(cmdfillgrid);
        DataTable dtfill = new DataTable();
        adpfillgrid.Fill(dtfill);



        DataView myDataView = new DataView();
        myDataView = dtfill.DefaultView;

        if (hdnsortExp.Value != string.Empty)
        {
            myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
        }
        GridView1.DataSource = myDataView;
        GridView1.DataBind();

    }

    protected void ImageButton1_Click(object sender, EventArgs e)
    {
        try
        {
            string str1 = "SELECT StatusCategory FROM StatusCategory where StatusCategory='" + txtdegnation.Text + "' and compid='" + compid + "'  order by StatusCategory";
            SqlCommand cmd1 = new SqlCommand(str1, con);
            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
            DataTable dt1 = new DataTable();
            da1.Fill(dt1);
            if (dt1.Rows.Count > 0)
            {
                statuslable.Visible = true;
                statuslable.Text = "Record already exists";
            }
            else
            {


                string str = "insert into StatusCategory  values ('" + txtdegnation.Text + "','" + compid + "')";
              
                SqlCommand mycmd = new SqlCommand(str, con);

                if (con.State.ToString() != "Open")
                {
                    con.Open();

                }

                mycmd.ExecuteNonQuery();
                con.Close();
                statuslable.Visible = true;
                statuslable.Text = "Record inserted successfully";
                FillGridView1();

                txtdegnation.Text = "";

                pnladd.Visible = false;
                lbllegend.Text = "";
                btnadd.Visible = true;
                

                string strmax = "SELECT Max(StatusCategoryMasterId) as StatusCategoryMasterId FROM StatusCategory ";
                SqlCommand cmdmax = new SqlCommand(strmax, con);
                SqlDataAdapter damax = new SqlDataAdapter(cmdmax);
                DataTable dtmax = new DataTable();
                damax.Fill(dtmax);
                if (dtmax.Rows.Count > 0)
                {

                    if (CheckBox1.Checked == true)
                    {
                        ViewState["Masterid"] = dtmax.Rows[0]["StatusCategoryMasterId"].ToString();
                        string te = "StatusMasterAddManage.aspx?Id=" + ViewState["Masterid"].ToString();
                        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);


                    }
                }
                CheckBox1.Checked = true;

            }
        }
        catch (Exception ererer)
        {
            statuslable.Visible = true;
            statuslable.Text = "error ;" + ererer.Message;

        }
    }

    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
        ViewState["id"] = GridView1.DataKeys[e.NewEditIndex].Value.ToString();
        //Label lbldpid = (Label)GridView1.Rows[e.NewEditIndex].FindControl("lblCatId");
        
        SqlCommand cmdedit = new SqlCommand("select EditAllowed,StatusCategoryId from Fixeddata where StatusCategoryId='" +  ViewState["id"] + "'", con);
        SqlDataAdapter dtpedit = new SqlDataAdapter(cmdedit);
        DataTable dtedit = new DataTable();
        dtpedit.Fill(dtedit);
        if (dtedit.Rows.Count > 0)
        {
            if (dtedit.Rows[0]["EditAllowed"].ToString() == "0")
            {
                string str = "Select * from StatusCategory where StatusCategoryMasterId='" + ViewState["id"] + "'";
                SqlCommand cmd = new SqlCommand(str, con);
                SqlDataAdapter adpt = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                adpt.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    statuslable.Visible = false;
                    
                    pnladd.Visible = true;
                    btnadd.Visible = false;
                    lbllegend.Text = "Edit New Category";
                    CheckBox1.Visible = false;
                    txtdegnation.Text = dt.Rows[0]["StatusCategory"].ToString();
                    btnupdate.Visible = true;
                    ImageButton1.Visible = false;
                }
               
            }
            else
            {
                
                statuslable.Text = "Sorry, you cannot edit this record.";                    
                
            }
        }
        else
        {
            string str = "Select * from StatusCategory where StatusCategoryMasterId='" + ViewState["id"] + "'";
            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataAdapter adpt = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adpt.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                statuslable.Visible = false;

                pnladd.Visible = true;
                btnadd.Visible = false;
                lbllegend.Text = "Edit New Category";

                CheckBox1.Visible = false;
                txtdegnation.Text = dt.Rows[0]["StatusCategory"].ToString();
                btnupdate.Visible = true;
                ImageButton1.Visible = false;
            }
        }
        
       

    }
    
    
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PagerSettings.Visible = true;
        GridView1.PageIndex = e.NewPageIndex;
        FillGridView1();
    }
    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        GridView1.PagerSettings.Visible = true;
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder;

        FillGridView1();
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
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        //GridView1.PagerSettings.Visible = true;
        //Label lbldpid1 = (Label)GridView1.Rows[e.RowIndex].FindControl("lblCatId");
        //if (lbldpid1.Text != "")
        //{
        //    SqlCommand cmddel = new SqlCommand("select DeleteAllowed,StatusCategoryId from Fixeddata where StatusCategoryId='" + lbldpid1.Text + "'", con);
        //    SqlDataAdapter dtpdel = new SqlDataAdapter(cmddel);
        //    DataTable dtdel = new DataTable();
        //    dtpdel.Fill(dtdel);
        //    if (dtdel.Rows.Count > 0)
        //    {
        //        if (dtdel.Rows[0]["DeleteAllowed"].ToString() == "0")
        //        {
        //            ModalPopupExtender1222.Show();
        //        }
        //        else
        //        {
        //            ModalPopupExtender2.Show();
        //        }
        //    }
        //    else
        //    {
        //        ModalPopupExtender1222.Show();
        //    }
        //}
        //ModalPopupExtender1222.Show();
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Delete")
        {
            GridView1.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            ViewState["Id"] = Convert.ToInt32(e.CommandArgument);
            SqlCommand cmdedit = new SqlCommand("select StatusCategoryMasterId from StatusMaster where StatusCategoryMasterId='" + ViewState["Id"] + "'", con);
            SqlDataAdapter dtpedit = new SqlDataAdapter(cmdedit);
            DataTable dtedit = new DataTable();
            dtpedit.Fill(dtedit);
            if (dtedit.Rows.Count == 0)
            {
                SqlCommand cmd = new SqlCommand("delete  from StatusCategory where [StatusCategoryMasterId]=" + ViewState["Id"] + " ", con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();

                }

                cmd.ExecuteNonQuery();
                con.Close();
                txtdegnation.Text = "";
                statuslable.Visible = true;
                statuslable.Text = "Record deleted successfully";
                FillGridView1();

               
            }
            else
            {
                statuslable.Visible = true;
               //statuslable.Text = "This category contains Status, please delete status before trying again.";
                statuslable.Text = "This category contains a status. Please change the status from the " + "<a href=\"StatusMasterAddManage.aspx\" style=\"font-size:14px; color:red; \" target=\"_blank\">" + "\"Status: Add, Manage\" " + "</a>page before deleting.";
            }
        }

    }

    //protected void ImageButton2_Click(object sender, EventArgs e)
    //{
    //    SqlCommand cmdedit = new SqlCommand("select StatusCategoryMasterId from StatusMaster where StatusCategoryMasterId='" + ViewState["Id"] + "'", con);
    //    SqlDataAdapter dtpedit = new SqlDataAdapter(cmdedit);
    //    DataTable dtedit = new DataTable();
    //    dtpedit.Fill(dtedit);
    //    if (dtedit.Rows.Count == 0)
    //    {
    //        SqlCommand cmd = new SqlCommand("delete  from StatusCategory where [StatusCategoryMasterId]=" + ViewState["Id"] + " ", con);
    //        if (con.State.ToString() != "Open")
    //        {
    //            con.Open();

    //        }

    //        cmd.ExecuteNonQuery();
    //        con.Close();
    //        txtdegnation.Text = "";
    //        statuslable.Visible = true;
    //        statuslable.Text = "Record Deleted Successfully";
    //        FillGridView1();

    //        GridView1.SelectedIndex = -1;
    //    }
    //    else
    //    {
    //        statuslable.Visible = true;
    //        statuslable.Text = "You dont allowed permite for delete record, first Child record delete";
    //    }

    //}
    //protected void ImageButton5_Click(object sender, EventArgs e)
    //{
    //    statuslable.Text = "";
    //    statuslable.Visible = false;
    //}
    protected void Button2_Click(object sender, EventArgs e)
    {
        txtdegnation.Text = "";
        statuslable.Visible = false;
        pnladd.Visible = false;
        lbllegend.Text = "";
        btnadd.Visible = true;
        
        btnupdate.Visible = false;
        CheckBox1.Visible = true;       
        ImageButton1.Visible = true;

    }

   
    
    protected void Button4_Click(object sender, EventArgs e)
    {
        if (Button4.Text == "Printable Version")
        {
            pnlgrid.ScrollBars = ScrollBars.None;
            pnlgrid.Height = new Unit("100%");

            Button4.Text = "Hide Printable Version";
            Button1.Visible = true;
            if (GridView1.Columns[1].Visible == true)
            {
                ViewState["editHide"] = "tt";
                GridView1.Columns[1].Visible = false;
            }
            if (GridView1.Columns[2].Visible == true)
            {
                ViewState["deleteHide"] = "tt";
                GridView1.Columns[2].Visible = false;
            }
        }
        else
        {
            //pnlgrid.ScrollBars = ScrollBars.Vertical;
           // pnlgrid.Height = new Unit(150);

            Button4.Text = "Printable Version";
            Button1.Visible = false;
            if (ViewState["editHide"] != null)
            {
                GridView1.Columns[1].Visible = true;
            }
            if (ViewState["deleteHide"] != null)
            {
                GridView1.Columns[2].Visible = true;
            }
        }
    }
    protected void btnadd_Click(object sender, EventArgs e)
    {
        if (pnladd.Visible == false)
        {
            pnladd.Visible = true;
            lbllegend.Text = "Add New Category";
        }
        else
        {
            pnladd.Visible = false;
            lbllegend.Text = "";
        }
        btnadd.Visible = false;
        statuslable.Text = "";
    }
    protected void btnupdate_Click(object sender, EventArgs e)
    {
        try
        {
            //  Label catid = (Label)GridView1.Rows[e.RowIndex].FindControl("lblCatId");
            SqlCommand cmdup = new SqlCommand("select StatusCategory from StatusCategory where StatusCategory='" + txtdegnation.Text + "' and compid='" + compid + "' and StatusCategoryMasterId<>'" + ViewState["id"] + "'", con);
            SqlDataAdapter dtpup = new SqlDataAdapter(cmdup);
            DataTable dtup = new DataTable();
            dtpup.Fill(dtup);
            if (dtup.Rows.Count > 0)
            {
                statuslable.Visible = true;
                statuslable.Text = "Record already exists";
            }
            else
            {
                string sr51 = ("update StatusCategory set StatusCategory='" + txtdegnation.Text + "'  where StatusCategoryMasterId='" + ViewState["id"] + "' and compid='" + compid + "'");
                SqlCommand cmd801 = new SqlCommand(sr51, con);

                if (con.State.ToString() != "Open")
                {
                    con.Open();

                }
                cmd801.ExecuteNonQuery();
                con.Close();

                FillGridView1();
                statuslable.Visible = true;
                statuslable.Text = "Record updated successfully";
                pnladd.Visible = false;
                txtdegnation.Text = "";
                lbllegend.Text = "";
                btnadd.Visible = true;
                btnupdate.Visible = false;
                ImageButton1.Visible = true;
                CheckBox1.Visible = true;
            }

        }
        catch (Exception ert)
        {
            statuslable.Visible = true;
            statuslable.Text = "Error :" + ert.Message;

        }
    }
}
