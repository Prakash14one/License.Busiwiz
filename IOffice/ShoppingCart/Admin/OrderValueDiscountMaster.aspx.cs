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
public partial class Admin_OrderValueDiscountMaster : System.Web.UI.Page
{
    string compid;
    //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["OnlineAccountConnectionString"].ConnectionString);
    SqlConnection con;
    protected void Page_Load(object sender, EventArgs e)
    {
        PageConn pgcon = new PageConn();
        con = pgcon.dynconn;
        if (Session["Comid"] == null)
        {
            Response.Redirect("~/Shoppingcart/Admin/ShoppingCartLogin.aspx");
        }
        compid = Session["comid"].ToString();

        pagetitleclass pg = new pagetitleclass();
        string strData = Request.Url.ToString();

        char[] separator = new char[] { '/' };

        string[] strSplitArr = strData.Split(separator);
        int i = Convert.ToInt32(strSplitArr.Length);
        string page = strSplitArr[i - 1].ToString();

        compid = Session["Comid"].ToString();
        Page.Title = pg.getPageTitle(page);
        ModalPopupExtender1222.Hide();

        Label1.Visible = false;

        if (!IsPostBack)
        {
            Pagecontrol.dypcontrol(Page, page);
            ViewState["sortOrder"] = "";
            //string strwh = "SELECT WareHouseId,Name,Address,CurrencyId FROM WareHouseMaster where comid='" + compid + "' and WareHouseMaster.Status='" + 1 + "'";
            //SqlCommand cmdwh = new SqlCommand(strwh, con);
            //SqlDataAdapter adpwh = new SqlDataAdapter(cmdwh);
            //DataTable dtwh = new DataTable();
            //adpwh.Fill(dtwh);



            //ddlWarehouse.DataSource = dtwh;
            //ddlWarehouse.DataTextField = "Name";
            //ddlWarehouse.DataValueField = "WareHouseId";

            //ddlWarehouse.DataBind();


            //DropDownList1.DataSource = dtwh;
            //DropDownList1.DataTextField = "Name";
            //DropDownList1.DataValueField = "WareHouseId";

            //DropDownList1.DataBind();
            //DropDownList1.Items.Insert(0, "ALL");
            //DropDownList1.Items[0].Value = "0";
            tXtEffectiveStartdate.Text = System.DateTime.Now.ToShortDateString();
            txtenddate.Text = System.DateTime.Now.ToShortDateString();

            fillstore();
            fillstorewithfilter();
            filldatagrid();

        }
    }




    protected void ImageButton5_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/ShoppingCart/Admin/wzApplyVolumeDiscount.aspx");
    }
    protected void ImageButton6_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("~/ShoppingCart/Admin/wzPackingHandlingChargesMaster.aspx");
    }
    protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    {
        ModalPopupExtender1222.Hide();
    }
    //protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    //{
    //    GridView1.PageIndex = e.NewPageIndex;
    //    GridView1.DataBind();

    //}

    //*************************

    protected decimal isdecimalornot(string ck)
    {
        decimal ick = 0;
        try
        {
            ick = Convert.ToDecimal(ck);
            return ick;
        }
        catch
        {
            return ick;
        }
        //return ick;
    }
    protected void Button1_Click(object sender, EventArgs e)
    {

         int flag = 0;
         if (RadioButton1.Checked ==true)
                {

                    if (Convert.ToDecimal(txtSchemaValueDiscount.Text) > Convert.ToDecimal(100))
                        {
                            flag = 1;
                        }
                  
                }
         if (flag == 0)
         {
             int discountflag = 0;

             if (txtMinValQuantity.Text != "" && txtMaxValQuantity.Text != "")
             {
                 decimal MaxValue1 = Convert.ToDecimal(isdecimalornot(txtMaxValQuantity.Text));

                 decimal MinValue1 = Convert.ToDecimal(isdecimalornot(txtMinValQuantity.Text));

                 if (MinValue1 > MaxValue1)
                 {
                     discountflag = 1;
                 }
             }
             if (discountflag == 0)
             {

                 string str11 = "select Convert(nvarchar,StartDate,101)   as StartDate,EndDate from [ReportPeriod] where Compid = '" + compid + "' and Active='1'  and Whid='" + ddlWarehouse.SelectedValue + "'";
                 SqlCommand cmd11 = new SqlCommand(str11, con);
                 SqlDataAdapter da = new SqlDataAdapter(cmd11);
                 DataTable dt11 = new DataTable();
                 da.Fill(dt11);
                 if (dt11.Rows.Count > 0)
                 {
                     if (Convert.ToDateTime(tXtEffectiveStartdate.Text) < Convert.ToDateTime(dt11.Rows[0][0].ToString()))
                     {
                         lblstartdate1.Text = dt11.Rows[0][0].ToString();
                         ModalPopupExtender1222.Show();

                     }

                     else
                     {

                         int a, b;
                         DateTime date = Convert.ToDateTime(System.DateTime.Now.ToShortDateString());
                         if (ddlstatus.SelectedIndex == 1)
                         {

                             a = 1;
                         }
                         else { a = 0; }

                         if (RadioButton1.Checked == true)
                         {
                             b = 1;
                         }
                         else
                         {
                             b = 0;
                         }


                         decimal c1 = Convert.ToDecimal(isdecimalornot(txtMaxValQuantity.Text));

                         decimal b1 = Convert.ToDecimal(isdecimalornot(txtMinValQuantity.Text));
                         if (txtMaxValQuantity.Text == "")
                         {
                             c1 = b1 + 1;
                         }
                         //if (b1 > c1)
                         //{

                         //    Label1.Visible = true;
                         //    Label1.Text = "Minimum Quantity must be less than Maximum Quantity";

                         //}

                         //else
                         //{
                         DateTime dt = Convert.ToDateTime(tXtEffectiveStartdate.Text);
                         DateTime dt1 = Convert.ToDateTime(txtenddate.Text);

                         if (dt < Convert.ToDateTime(DateTime.Now.ToShortDateString()))
                         {
                             Label1.Visible = true;
                             Label1.Text = "Start date must be the current date, or greater than the current date";
                         }


                         else if (dt1 < dt)
                         {

                             Label1.Visible = true;
                             Label1.Text = "End date must be the current date, or greater than the current date";


                         }
                         else
                         {




                             try
                             {
                                 string str111 = "Select * from OrderValueDiscountMaster where SchemeName='" + txtSchemaName.Text + "' and Whid='" + ddlWarehouse.SelectedValue + "'";
                                 SqlCommand cmd111 = new SqlCommand(str111, con);
                                 SqlDataAdapter da1 = new SqlDataAdapter(cmd111);
                                 DataTable dt111 = new DataTable();
                                 da1.Fill(dt111);
                                 if (dt111.Rows.Count == 0)
                                 {
                                     string strinsr = "   INSERT INTO OrderValueDiscountMaster " +
                                                     "(SchemeName,ValueDiscount,MinValue,MaxValue,StartDate,EndDate,Active,IsPercentage,ApplyOnlineSales,ApplyRetailSales,Whid) " +
                                                     " VALUES  ('" + txtSchemaName.Text + "','" + isdecimalornot(txtSchemaValueDiscount.Text) + "','" + isdecimalornot(txtMinValQuantity.Text) + "', " +
                                                     " '" + txtMaxValQuantity.Text + "','" + tXtEffectiveStartdate.Text + "','" + txtenddate.Text + "', " +
                                                     " '" + ddlstatus.SelectedValue + "', '" + b.ToString() + "','" + chkonline.Checked + "','" + chkretail.Checked + "','" + ddlWarehouse.SelectedValue + "')  ";
                                     SqlCommand cmd = new SqlCommand(strinsr, con);

                                     if (con.State.ToString() != "Open")
                                     {
                                         con.Open();
                                     }
                                     cmd.ExecuteNonQuery();
                                     con.Close();
                                     Label1.Visible = true;
                                     Label1.Text = "Record inserted successfully";

                                     addinventoryroom.Visible = false;
                                     btnaddroom.Visible = true;

                                    
                                     clean();
                                     filldatagrid();
                                     ddlstatus.SelectedIndex = 0;
                                 }
                                 else
                                 {
                                     Label1.Visible = true;
                                     Label1.Text = "Record already exists ";
                                 }
                             }
                             catch (Exception ex)
                             {
                                 Label1.Visible = true;
                                 Label1.Text = "Error";
                             }
                             finally { }

                         }
                         
                     }
                 }
             }
             else
             {
                 Label1.Visible = true;
                 Label1.Text = "Minimum Order Value must be less than Maximum Order Value";

             }
         }
         else
         {
             Label1.Visible = true;
             Label1.Text = "Order value discount percentage cannot be greater than 100%";
         }
    }
    protected int isintornot(string ck)
    {
        int i = 0;
        try
        {
            i = Convert.ToInt32(ck);
            return i;
        }
        catch
        {
            return i;
        }

    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        clean();
        addinventoryroom.Visible = false;
        btnaddroom.Visible = true;
        tXtEffectiveStartdate.Enabled = true;
        ImageButton1.Enabled = true;
        ddlstatus.SelectedIndex = 0;
    }
    protected void ImageButton1_Click(object sender, ImageClickEventArgs e)
    {

    }
    //protected void ImageButton2_Click(object sender, ImageClickEventArgs e)
    //{
    //    ModalPopupExtender1222.Hide();
    //}
    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        GridView1.PageIndex = e.NewPageIndex;
        GridView1.DataBind();

    }


    public void clean()
    {
        imgupdate.Visible = false;
        ImageButton3.Visible = true;


        ddlWarehouse.Enabled = true;
        ddlWarehouse.SelectedIndex = 0;
        ddlWarehouse.Enabled = true;
        chkonline.Checked = false;
        chkretail.Checked = false;
        txtenddate.Text = "";
        txtMaxValQuantity.Text = "";
        txtMinValQuantity.Text = "";
        txtSchemaName.Text = "";
        txtSchemaValueDiscount.Text = "";
        tXtEffectiveStartdate.Text = "";
        txtenddate.Text = "";

    }
    public void filldatagrid()
    {
        string filldatagrid = "";
        GridView1.DataSource = null;
        GridView1.DataBind();
        lblCompany.Text = Session["Cname"].ToString();
        if (DropDownList1.SelectedIndex > 0)
        {
            lblBusiness.Text = DropDownList1.SelectedItem.Text;
            filldatagrid = "SELECT WareHouseMaster.Name, OrderValueDiscountMaster.*,Case When(Active =1) then 'Active' else 'Inactive' end as Status FROM OrderValueDiscountMaster inner join WareHouseMaster on WareHouseMaster.WareHouseId=OrderValueDiscountMaster.Whid where OrderValueDiscountMaster.Whid='" + DropDownList1.SelectedValue + "' and WareHouseMaster.Status='" + 1 + "' order by WareHouseMaster.Name, OrderValueDiscountMaster.SchemeName,OrderValueDiscountMaster.MinValue,OrderValueDiscountMaster.MaxValue,OrderValueDiscountMaster.StartDate,OrderValueDiscountMaster.EndDate";
        }
        else
        {
            lblBusiness.Text = "All";
            filldatagrid = "SELECT WareHouseMaster.Name, OrderValueDiscountMaster.*,Case When(Active =1) then 'Active' else 'Inactive' end as Status FROM OrderValueDiscountMaster inner join WareHouseMaster on WareHouseMaster.WareHouseId=OrderValueDiscountMaster.Whid where  WareHouseMaster.comid='" + compid + "' and WareHouseMaster.Status='" + 1 + "' order by WareHouseMaster.Name, OrderValueDiscountMaster.SchemeName,OrderValueDiscountMaster.MinValue,OrderValueDiscountMaster.MaxValue,OrderValueDiscountMaster.StartDate,OrderValueDiscountMaster.EndDate";
        }
        SqlDataAdapter adp = new SqlDataAdapter(filldatagrid, con);
        DataTable ds = new DataTable();
        adp.Fill(ds);
        GridView1.DataSource = ds;
        DataView myDataView = new DataView();
        myDataView = ds.DefaultView; ;

        if (hdnsortExp.Value != string.Empty)
        {
            myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
        }


        GridView1.DataBind();
        foreach (GridViewRow item in GridView1.Rows)
        {
            Label lblsign = (Label)item.FindControl("lblsign");
            Label lblpersign = (Label)item.FindControl("lblpersign");
            CheckBox txtchisper = (CheckBox)item.FindControl("txtchisper");
            if (txtchisper.Checked == true)
            {
                lblsign.Visible = false;
                lblpersign.Visible = true;
            }
            else
            {
                lblsign.Visible = true;
                lblpersign.Visible = false;
            }
        }

    }

    //protected void ddlWarehouse_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    filldatagrid();
    //}

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        imgupdate.Visible = true;
        ImageButton3.Visible = false;

        if (e.CommandName == "Delete")
        {

            imgupdate.Visible = false;
            ImageButton3.Visible = true;
            Label1.Visible = true;
            Label1.Text = "Record deleted successfully ";
        }
        if (e.CommandName == "Select")
        {

            addinventoryroom.Visible = true;
            btnaddroom.Visible = false;

            GridView1.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            string s = "select * from OrderValueDiscountMaster where OrderValueDiscountMasterId = '" + GridView1.SelectedIndex + "' ";

            SqlCommand cmd = new SqlCommand(s, con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds);

            ddlWarehouse.SelectedIndex = ddlWarehouse.Items.IndexOf(ddlWarehouse.Items.FindByValue(ds.Tables[0].Rows[0]["Whid"].ToString()));
            ddlWarehouse.Enabled = false;
            chkonline.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["ApplyOnlineSales"].ToString());
            chkretail.Checked = Convert.ToBoolean(ds.Tables[0].Rows[0]["ApplyRetailSales"].ToString());
            ViewState["orderid"] = GridView1.SelectedIndex;

            txtSchemaName.Text = ds.Tables[0].Rows[0]["SchemeName"].ToString();
            txtSchemaValueDiscount.Text = ds.Tables[0].Rows[0]["ValueDiscount"].ToString();


            if (ds.Tables[0].Rows[0]["IsPercentage"].ToString() == "True")
            {
                RadioButton2.Checked = false;
                RadioButton1.Checked = true;

            }
            else if (ds.Tables[0].Rows[0]["IsPercentage"].ToString() == "False")
            {
                RadioButton1.Checked = false;
                RadioButton2.Checked = true;
            }
            if (ds.Tables[0].Rows[0]["Active"].ToString() == "True")
            {
                ddlstatus.SelectedIndex = 0;
            }
            else if (ds.Tables[0].Rows[0]["Active"].ToString() == "False")
            {
                ddlstatus.SelectedIndex = 1;
            }
            txtMaxValQuantity.Text = ds.Tables[0].Rows[0]["MaxValue"].ToString();
            txtMinValQuantity.Text = ds.Tables[0].Rows[0]["MinValue"].ToString();

            tXtEffectiveStartdate.Text = ds.Tables[0].Rows[0]["StartDate"].ToString();
            txtenddate.Text = ds.Tables[0].Rows[0]["EndDate"].ToString();

            ViewState["OldEndDate"] = ds.Tables[0].Rows[0]["EndDate"].ToString();
            tXtEffectiveStartdate.Enabled = false;
            ImageButton1.Enabled = false;

        }
       
    }
    protected void imgupdate_Click(object sender, EventArgs e)
    {
        int flag = 0;
        if (RadioButton1.Checked == true)
        {

            if (Convert.ToDecimal(txtSchemaValueDiscount.Text) > Convert.ToDecimal(100))
            {
                flag = 1;
            }

        }
        if (flag == 0)
        {
            int discountflag = 0;

            if (txtMinValQuantity.Text != "" && txtMaxValQuantity.Text != "")
            {
                decimal MaxValue1 = Convert.ToDecimal(isdecimalornot(txtMaxValQuantity.Text));

                decimal MinValue1 = Convert.ToDecimal(isdecimalornot(txtMinValQuantity.Text));

                if (MinValue1 > MaxValue1)
                {
                    discountflag = 1;
                }
            }
            if (discountflag == 0)
            {

                int active = 0;

                int per = 0;

                if (RadioButton1.Checked == true)
                {
                    per = 1;
                }
                else if (RadioButton2.Checked == true)
                {
                    per = 0;
                }

                if (ddlstatus.SelectedIndex == 0)
                {
                    active = 1;

                }
                else
                {
                    active = 0;
                }


                decimal c1 = Convert.ToDecimal(isdecimalornot(txtMaxValQuantity.Text));
                decimal b1 = Convert.ToDecimal(isdecimalornot(txtMinValQuantity.Text));
                if (txtMaxValQuantity.Text == "")
                {
                    c1 = b1 + 1;
                }
                //if (b1 > c1)
                //{

                //    Label1.Visible = true;
                //    Label1.Text = "Minimum Quantity Must be less than Maximum Quantity";

                //}

                //else
                //{
                DateTime dt = Convert.ToDateTime(tXtEffectiveStartdate.Text);
                DateTime dt1 = Convert.ToDateTime(txtenddate.Text);

                DateTime t1 = Convert.ToDateTime(ViewState["OldEndDate"].ToString());
                DateTime t2 = Convert.ToDateTime(txtenddate.Text);



                if (t1 == t2)
                {
                    string check = "select * from OrderValueDiscountMaster where [SchemeName]='" + txtSchemaName.Text + "' and  Whid='" + ddlWarehouse.SelectedValue + "' and OrderValueDiscountMasterId <>'" + ViewState["orderid"] + "' ";
                    SqlDataAdapter spa = new SqlDataAdapter(check, con);
                    DataTable dts = new DataTable();
                    spa.Fill(dts);
                    if (dts.Rows.Count == 0)
                    {
                        string s2 = "update OrderValueDiscountMaster set  SchemeName = '" + txtSchemaName.Text + "' , ValueDiscount = '" + txtSchemaValueDiscount.Text + "' , IsPercentage = '" + per + "' , Active = '" + ddlstatus.SelectedValue + "' ,MinValue = '" + txtMinValQuantity.Text + "' , MaxValue = '" + txtMaxValQuantity.Text + "' , EndDate = '" + txtenddate.Text + "' ,ApplyRetailSales='" + chkretail.Checked + "',ApplyOnlineSales='" + chkonline.Checked + "' Where  OrderValueDiscountMasterId ='" + ViewState["orderid"] + "' ";

                        SqlCommand cmd = new SqlCommand(s2, con);
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        DataSet ds = new DataSet();
                        da.Fill(ds);


                        Label1.Visible = true;
                        Label1.Text = "Record updated successfully";

                        imgupdate.Visible = false;
                        ImageButton3.Visible = true;

                        addinventoryroom.Visible = false;
                        btnaddroom.Visible = true;

                        tXtEffectiveStartdate.Enabled = true;
                        ImageButton1.Enabled = true;
                    }
                    else
                    {
                        Label1.Visible = true;
                        Label1.Text = "Record already exists";
                        imgupdate.Visible = false;
                        ImageButton3.Visible = true;

                    }
                    txtSchemaName.Text = "";
                    ddlWarehouse.Enabled = true;
                    ddlWarehouse.SelectedIndex = 0;
                    chkonline.Checked = false;
                    chkretail.Checked = false;
                    txtMaxValQuantity.Text = "";
                    txtMinValQuantity.Text = "";
                    txtenddate.Text = "";
                    txtSchemaValueDiscount.Text = "";
                    tXtEffectiveStartdate.Text = "";
                    RadioButton1.Checked = false;
                    RadioButton2.Checked = false;
                    ddlstatus.SelectedIndex = 0;
                    filldatagrid();

                }
                else
                {
                    if (dt1 < Convert.ToDateTime(DateTime.Now.ToShortDateString()))
                    {
                        Label1.Visible = true;
                        Label1.Text = "End date must be the current date, or greater than the current date";
                    }
                    else if (dt1 < dt)
                    {

                        Label1.Visible = true;
                        Label1.Text = "End date must be the current date, or greater than the current date";


                    }
                    else
                    {
                        string check = "select * from OrderValueDiscountMaster where [SchemeName]='" + txtSchemaName.Text + "' and  Whid='" + ddlWarehouse.SelectedValue + "' and OrderValueDiscountMasterId <>'" + ViewState["orderid"] + "' ";
                        SqlDataAdapter spa = new SqlDataAdapter(check, con);
                        DataTable dts = new DataTable();
                        spa.Fill(dts);
                        if (dts.Rows.Count == 0)
                        {
                            string s2 = "update OrderValueDiscountMaster set  SchemeName = '" + txtSchemaName.Text + "' , ValueDiscount = '" + txtSchemaValueDiscount.Text + "' , IsPercentage = '" + per + "' , Active = '" + ddlstatus.SelectedValue + "' ,MinValue = '" + txtMinValQuantity.Text + "' , MaxValue = '" + txtMaxValQuantity.Text + "' , EndDate = '" + txtenddate.Text + "' ,ApplyRetailSales='" + chkretail.Checked + "',ApplyOnlineSales='" + chkonline.Checked + "' Where  OrderValueDiscountMasterId ='" + ViewState["orderid"] + "' ";

                            SqlCommand cmd = new SqlCommand(s2, con);
                            SqlDataAdapter da = new SqlDataAdapter(cmd);
                            DataSet ds = new DataSet();
                            da.Fill(ds);


                            Label1.Visible = true;
                            Label1.Text = "Record updated successfully";

                            imgupdate.Visible = false;
                            ImageButton3.Visible = true;

                            addinventoryroom.Visible = false;
                            btnaddroom.Visible = true;

                            tXtEffectiveStartdate.Enabled = true;
                            ImageButton1.Enabled = true;
                        }
                        else
                        {
                            Label1.Visible = true;
                            Label1.Text = "Record already exists";
                            imgupdate.Visible = false;
                            ImageButton3.Visible = true;

                        }
                        txtSchemaName.Text = "";
                        ddlWarehouse.Enabled = true;
                        ddlWarehouse.SelectedIndex = 0;
                        chkonline.Checked = false;
                        chkretail.Checked = false;
                        txtMaxValQuantity.Text = "";
                        txtMinValQuantity.Text = "";
                        txtenddate.Text = "";
                        txtSchemaValueDiscount.Text = "";
                        tXtEffectiveStartdate.Text = "";
                        RadioButton1.Checked = false;
                        RadioButton2.Checked = false;
                        ddlstatus.SelectedIndex = 0;
                        filldatagrid();

                    }
                }


            }
            else
            {
                Label1.Visible = true;
                Label1.Text = "Minimum Order Value must be less than Maximum Order Value";

            }
        }
        else
        {
            Label1.Visible = true;
            Label1.Text = "Order value discount percentage cannot be greater than 100%";
        }
    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        int i = Convert.ToInt32(GridView1.DataKeys[e.RowIndex].Value);
        string strinsr = "delete from  OrderValueDiscountMaster where OrderValueDiscountMasterId='" + i.ToString() + "'  ";
        SqlCommand cmd = new SqlCommand(strinsr, con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmd.ExecuteNonQuery();
        con.Close();
        Label1.Visible = true;
        Label1.Text = "Record deleted successfully";
        filldatagrid();


    }
    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder; // sortOrder;
        filldatagrid();
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
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        filldatagrid();
    }
    protected void Button1_Click1(object sender, EventArgs e)
    {
        if (Button2.Text == "Printable Version")
        {
            Panel2.ScrollBars = ScrollBars.None;
            Panel2.Height = new Unit("100%");

            Button2.Text = "Hide Printable Version";
            Button7.Visible = true;
            if (GridView1.Columns[11].Visible == true)
            {
                ViewState["editHide"] = "tt";
                GridView1.Columns[11].Visible = false;
            }
            if (GridView1.Columns[12].Visible == true)
            {
                ViewState["deleHide"] = "tt";
                GridView1.Columns[12].Visible = false;
            }

        }
        else
        {

            Panel2.ScrollBars = ScrollBars.Vertical;
            Panel2.Height = new Unit(250);

            Button2.Text = "Printable Version";
            Button7.Visible = false;
            if (ViewState["editHide"] != null)
            {
                GridView1.Columns[11].Visible = true;
            }
            if (ViewState["deleHide"] != null)
            {
                GridView1.Columns[12].Visible = true;
            }

        }
    }
    protected void btnaddroom_Click(object sender, EventArgs e)
    {
        if (addinventoryroom.Visible == false)
        {
            addinventoryroom.Visible = true;

        }
        else if (addinventoryroom.Visible == true)
        {
            addinventoryroom.Visible = false;

        }
        //statuslable.Text = "";
        btnaddroom.Visible = false;
    }
    protected void fillstore()
    {
        ddlWarehouse.Items.Clear();
        DataTable ds = ClsStore.SelectStorename();
        ddlWarehouse.DataSource = ds;
        ddlWarehouse.DataTextField = "Name";
        ddlWarehouse.DataValueField = "WareHouseId";
        ddlWarehouse.DataBind();



        DataTable dteeed = ClsStore.SelectEmployeewithIdwise();

        if (dteeed.Rows.Count > 0)
        {
            ddlWarehouse.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);
        }


    }
    protected void fillstorewithfilter()
    {
        DropDownList1.Items.Clear();
        DataTable ds = ClsStore.SelectStorename();
        DropDownList1.DataSource = ds;
        DropDownList1.DataTextField = "Name";
        DropDownList1.DataValueField = "WareHouseId";
        DropDownList1.DataBind();
        DropDownList1.Items.Insert(0, "All");
        DropDownList1.Items[0].Value = "0";



    }
}
