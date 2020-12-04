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

public partial class Add_Employee_Barcode : System.Web.UI.Page
{
    SqlConnection conn;
    string compid;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Comid"] == null)
        {
            Response.Redirect("~/Shoppingcart/Admin/ShoppingCartLogin.aspx");
        }

        PageConn pgcon = new PageConn();
        conn = pgcon.dynconn;
        pagetitleclass pg = new pagetitleclass();
        string strData = Request.Url.ToString();

        char[] separator = new char[] { '/' };

        string[] strSplitArr = strData.Split(separator);
        int i = Convert.ToInt32(strSplitArr.Length);
        string page = strSplitArr[i - 1].ToString();

        compid = Session["Comid"].ToString();
        Page.Title = pg.getPageTitle(page);


        if (!IsPostBack)
        {
            Pagecontrol.dypcontrol(Page, page);
            ViewState["sortOrder"] = "";

            lblCompany.Text = Session["Cname"].ToString();

            fillstore();

            fillemployee();

            filldata();
        }

    }
    protected void fillstore()
    {
        DataTable ds = ClsStore.SelectStorename();
        DropDownList1.DataSource = ds;
        DropDownList1.DataTextField = "Name";
        DropDownList1.DataValueField = "WareHouseId";
        DropDownList1.DataBind();

        DataTable dteeed = ClsStore.SelectEmployeewithIdwise();

        if (dteeed.Rows.Count > 0)
        {
            DropDownList1.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);
        }

        ddshorting.DataSource = ds;
        ddshorting.DataTextField = "Name";
        ddshorting.DataValueField = "WareHouseId";
        ddshorting.DataBind();
        ddshorting.Items.Insert(0, "All");
        ddshorting.Items[0].Value = "0";

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

    public void filldata()
    {
        string sg90 = "";
        string st1 = "";
        //sg90 = " Select distinct [EmployeeBarcodeMaster].[EmpBarcodeMasterID],EmployeePayrollMaster.EmployeeNo,[EmployeeBarcodeMaster].[Barcode],EmployeeBarcodeMaster.Employeecode,EmployeeBarcodeMaster.Biometricno,EmployeeBarcodeMaster.blutoothid,[EmployeeBarcodeMaster].Whid, " +
        //               " EmployeeMaster.EmployeeName,EmployeeMaster.EmployeeMasterID,WareHouseMaster.Name,WareHouseMaster.WareHouseId ,case when (EmployeeBarcodeMaster.Active = '1') then 'Active' else 'Inactive' end as Active" +
        //               " from [EmployeeBarcodeMaster] inner join EmployeeMaster on EmployeeMaster.EmployeeMasterID= [EmployeeBarcodeMaster].Employee_Id inner join EmployeePayrollMaster on EmployeePayrollMaster.Empid=EmployeeBarcodeMaster.Employee_Id inner join WareHouseMaster on EmployeeMaster.Whid= WareHouseMaster.WareHouseId  where WareHouseMaster.comid='" + Session["comid"] + "' ";


    //    sg90 = "Select distinct [EmployeeBarcodeMaster].[EmpBarcodeMasterID],User_master.username,EmployeePayrollMaster.EmployeeNo,[EmployeeBarcodeMaster].[Barcode],EmployeeBarcodeMaster.Employeecode,EmployeeBarcodeMaster.Biometricno,EmployeeBarcodeMaster.blutoothid,[EmployeeBarcodeMaster].Whid,EmployeeMaster.EmployeeName,EmployeeMaster.EmployeeMasterID,WareHouseMaster.Name,WareHouseMaster.WareHouseId,case when (EmployeeBarcodeMaster.Active = '1') then 'Active' else 'Inactive' end as Active from [EmployeeBarcodeMaster] inner join EmployeeMaster on EmployeeMaster.EmployeeMasterID= [EmployeeBarcodeMaster].Employee_Id inner join User_master on User_master.partyid=employeemaster.[PartyID] inner join EmployeePayrollMaster on EmployeePayrollMaster.Empid=EmployeeBarcodeMaster.Employee_Id inner join WareHouseMaster on EmployeeMaster.Whid= WareHouseMaster.WareHouseId where WareHouseMaster.comid='" + Session["comid"] + "'";

        sg90 = "   Select distinct [EmployeeBarcodeMaster].[EmpBarcodeMasterID],User_master.username,EmployeePayrollMaster.EmployeeNo,[EmployeeBarcodeMaster].[Barcode],EmployeeBarcodeMaster.Employeecode,EmployeeBarcodeMaster.Biometricno,EmployeeBarcodeMaster.blutoothid,[EmployeeBarcodeMaster].Whid,EmployeeMaster.EmployeeName,EmployeeMaster.EmployeeMasterID,WareHouseMaster.Name,WareHouseMaster.WareHouseId ,case when (EmployeeBarcodeMaster.Active = '1') then 'Active' else 'Inactive' end as Active    from [EmployeeBarcodeMaster] inner join EmployeeMaster on EmployeeMaster.EmployeeMasterID= [EmployeeBarcodeMaster].Employee_Id  inner join User_master on User_master.partyid=employeemaster.[PartyID] left join EmployeePayrollMaster on EmployeePayrollMaster.Empid=EmployeeBarcodeMaster.Employee_Id inner join WareHouseMaster on EmployeeMaster.Whid= WareHouseMaster.WareHouseId  where WareHouseMaster.comid='" + Session["comid"] + "'";

        if (ddshorting.SelectedIndex > 0)
        {
            st1 = " and EmployeeMaster.Whid='" + ddshorting.SelectedValue + "'";
        }
        lblBusiness.Text = ddshorting.SelectedItem.Text;
        sg90 = sg90 + st1 + "order by WareHouseMaster.Name, EmployeeMaster.EmployeeName";
        SqlCommand cmd23490 = new SqlCommand(sg90, conn);
        SqlDataAdapter adp23490 = new SqlDataAdapter(cmd23490);
        DataTable dt23490 = new DataTable();

        adp23490.Fill(dt23490);
        grdTempData.DataSource = dt23490;

        //DataView myDataView = new DataView();
        //myDataView = dt23490.DefaultView;


        DataView myDataView = new DataView();
        myDataView = dt23490.DefaultView;

        if (hdnsortExp.Value != string.Empty)
        {
            myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
        }


        //if (hdnsortExp.Value != string.Empty)
        //{
        //    myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
        //}
        grdTempData.DataBind();

    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        pnlhideshow.Visible = false;
        lblmsg.Text = "";
        cleartext();
    }

    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillemployee();
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        string sg90 = "Select [EmployeeBarcodeMaster].[Employee_Id] from [EmployeeBarcodeMaster] where ( [EmployeeBarcodeMaster].[Employee_Id] = '" + DropDownList2.SelectedValue + "' or EmployeeBarcodeMaster.Barcode='" + TextBox1.Text + "' )";
        SqlCommand cmd23490 = new SqlCommand(sg90, conn);
        SqlDataAdapter adp23490 = new SqlDataAdapter(cmd23490);
        DataTable dt23490 = new DataTable();
        adp23490.Fill(dt23490);
        if (dt23490.Rows.Count > 0)
        {
            lblmsg.Visible = true;
            lblmsg.Text = "Record already exist";

        }
        else
        {
            string str = "Insert Into EmployeeBarcodeMaster(Employee_Id,Barcode,Active,Whid)values('" + DropDownList2.SelectedValue + "','" + TextBox1.Text + "','" + ddlstatus.SelectedValue + "','" + DropDownList1.SelectedValue + "')";
            SqlCommand cmd = new SqlCommand(str, conn);
            if (conn.State.ToString() != "Open")
            {
                conn.Open();
            }
            cmd.ExecuteNonQuery();
            conn.Close();
            lblmsg.Visible = true;
            lblmsg.Text = "Record inserted successfully";
            filldata();

            cleartext();
        }
    }

    public void cleartext()
    {
        TextBox1.Text = "";
        txtemployeecode.Text = "";
        txtbiometricid.Text = "";
        txtbluetoothid.Text = "";
        ddlstatus.SelectedIndex = 0;

    }

    protected void Button5_Click(object sender, EventArgs e)
    {

    }
    //protected void grdTempData_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    //{
    //grdTempData.EditIndex = -1;
    //filldata();
    //}

    protected void grdTempData_RowCommand(object sender, GridViewCommandEventArgs e)
    {

        //if (e.CommandName == "Delete")
        //{
        //   // grdTempData.SelectedIndex = Convert.ToInt32(e.CommandArgument);
        //   // ViewState["Id"] = grdTempData.SelectedDataKey.Value;

        //}
    }
    protected void grdTempData_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        ViewState["Id"] = grdTempData.DataKeys[e.RowIndex].Value;
        SqlCommand cmd = new SqlCommand("delete  from EmployeeBarcodeMaster where EmpBarcodeMasterID=" + ViewState["Id"] + "", conn);
        conn.Open();
        cmd.ExecuteNonQuery();
        conn.Close();
        lblmsg.Visible = true;
        lblmsg.Text = "Record deleted successfully";
        grdTempData.EditIndex = -1;
        filldata();
    }
    //protected void grdTempData_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
    // {
    //    grdTempData.PageIndex = e.NewSelectedIndex;
    //    filldata();
    // }

    protected void grdTempData_RowEditing(object sender, GridViewEditEventArgs e)
    {
        lblmsg.Text = "";
        ViewState["Id"] = grdTempData.DataKeys[e.NewEditIndex].Value.ToString();
     //   string str12 = "select EmployeeBarcodeMaster.*,employeepayrollmaster.employeeno from EmployeeBarcodeMaster inner join employeepayrollmaster on employeepayrollmaster.EmpId=EmployeeBarcodeMaster.Employee_Id where EmpBarcodeMasterID='" + ViewState["Id"] + "'";

        string str12 = "select EmployeeBarcodeMaster.*,User_master.username,employeepayrollmaster.employeeno from EmployeeBarcodeMaster inner join employeepayrollmaster on employeepayrollmaster.EmpId=EmployeeBarcodeMaster.Employee_Id inner join employeemaster on employeemaster.[EmployeeMasterID]=EmployeeBarcodeMaster.Employee_Id inner join User_master on User_master.partyid=employeemaster.[PartyID] where EmpBarcodeMasterID='" + ViewState["Id"] + "'";

        SqlCommand cmd1 = new SqlCommand(str12, conn);
        SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
        DataTable ds1 = new DataTable();
        adp1.Fill(ds1);

       

        if (ds1.Rows.Count > 0)
        {
            lbladd.Text = "Edit Employee Barcode";
            Button3.Visible = false;
            btnupdate.Visible = true;

            pnlhideshow.Visible = true;
            btnshowbarcode.Visible = false;
            fillstore();
            DropDownList1.SelectedIndex = DropDownList1.Items.IndexOf(DropDownList1.Items.FindByValue(ds1.Rows[0]["Whid"].ToString()));
            fillemployee();
            DropDownList2.SelectedIndex = DropDownList2.Items.IndexOf(DropDownList2.Items.FindByValue(ds1.Rows[0]["Employee_Id"].ToString()));

            TextBox1.Text = ds1.Rows[0]["Barcode"].ToString();
            lblusername.Text = ds1.Rows[0]["username"].ToString();
            txtempnumber.Text = ds1.Rows[0]["EmployeeNo"].ToString();
            txtemployeecode.Text = ds1.Rows[0]["Employeecode"].ToString();
            txtbiometricid.Text = ds1.Rows[0]["Biometricno"].ToString();
            txtbluetoothid.Text = ds1.Rows[0]["blutoothid"].ToString();
            if (ds1.Rows[0]["Active"].ToString() == "False")
            {
                ddlstatus.SelectedIndex = ddlstatus.Items.IndexOf(ddlstatus.Items.FindByValue("0"));
            }
            else
            {
                ddlstatus.SelectedIndex = ddlstatus.Items.IndexOf(ddlstatus.Items.FindByValue("1"));
            }

        }
        //grdTempData.EditIndex = e.NewEditIndex;

        //filldata();
        //Label whmid = (Label)grdTempData.Rows[grdTempData.EditIndex].FindControl("lblwhid");
        //DropDownList wh = (DropDownList)grdTempData.Rows[grdTempData.EditIndex].FindControl("ddwarehouse");

        //string wh1 = "select Name,WareHouseID from WareHouseMaster where comid='" + Session["comid"] + "'and Status='" + 1 + "' order by name";

        //SqlCommand cmdwh = new SqlCommand(wh1, conn);
        //SqlDataAdapter daeh = new SqlDataAdapter(cmdwh);
        //DataSet dswh = new DataSet();
        //daeh.Fill(dswh);

        //wh.DataSource = dswh;
        //wh.DataTextField = "Name";
        //wh.DataValueField = "WareHouseID";

        //wh.DataBind();
        //wh.SelectedIndex = wh.Items.IndexOf(wh.Items.FindByValue(whmid.Text));
        //Session["wh"] = wh.SelectedValue;

        //Label lblempid = (Label)grdTempData.Rows[grdTempData.EditIndex].FindControl("lblempid");
        //DropDownList ddlemp = (DropDownList)grdTempData.Rows[grdTempData.EditIndex].FindControl("ddlemp");

        //string str1 = "select [EmployeeMaster].[EmployeeMasterID],[EmployeeMaster].[EmployeeName] from [EmployeeMaster] where Whid='" + Session["wh"] + "'  order by EmployeeName";

        //SqlCommand cmdwh1 = new SqlCommand(str1, conn);
        //SqlDataAdapter daeh1 = new SqlDataAdapter(cmdwh1);
        //DataSet dswh1 = new DataSet();
        //daeh1.Fill(dswh1);
        //ddlemp.DataSource = dswh1;
        //ddlemp.DataTextField = "EmployeeName";
        //ddlemp.DataValueField = "EmployeeMasterID";
        //ddlemp.DataBind();
        //ddlemp.SelectedIndex = ddlemp.Items.IndexOf(ddlemp.Items.FindByValue(lblempid.Text));
        //TextBox txtbarcode = (TextBox)grdTempData.Rows[grdTempData.EditIndex].FindControl("txtBarcode");
        //CheckBox chkact = (CheckBox)grdTempData.Rows[grdTempData.EditIndex].FindControl("chkAcive1");

    }

    // protected void grdTempData_RowUpdating(object sender, GridViewUpdateEventArgs e)
    //{
    //DropDownList emp = (DropDownList)grdTempData.Rows[grdTempData.EditIndex].FindControl("ddlemp");
    //DropDownList store = (DropDownList)grdTempData.Rows[grdTempData.EditIndex].FindControl("ddwarehouse");
    //TextBox txtbar = (TextBox)grdTempData.Rows[grdTempData.EditIndex].FindControl("txtBarcode");
    //CheckBox chk = (CheckBox)grdTempData.Rows[grdTempData.EditIndex].FindControl("chkAcive1");

    //string strrrr = "Select [EmployeeBarcodeMaster].[Employee_Id] from [EmployeeBarcodeMaster] where ( [EmployeeBarcodeMaster].[Employee_Id] = '" + emp.SelectedValue + "' or EmployeeBarcodeMaster.Barcode='" + txtbar.Text + "') and  EmpBarcodeMasterID <>'" + ViewState["Id"] + "'";
    //// string strrrr = "select  [EmployeeId],[Id] from [EmployeeTaxMaster] " +
    ////    " where [Whid]='" + store.SelectedValue + "' and EmployeeId= '" + emp.SelectedValue + "' and  Id <>'" + ViewState["Id"] + "' ";

    //SqlCommand cmd1 = new SqlCommand(strrrr, conn);

    //SqlDataAdapter adp = new SqlDataAdapter(cmd1);
    //DataTable ds = new DataTable();
    //adp.Fill(ds);
    //if (ds.Rows.Count == 0)
    //{
    //    string str = "Update EmployeeBarcodeMaster set Whid='" + store.SelectedValue.ToString() + "',Employee_Id ='" + emp.SelectedValue + "',Barcode='" + txtbar.Text + "',[Active]='" + chk.Checked + "',Employeecode='" + txtemployeecode.Text + "',Biometricno='" + txtbiometricid.Text + "',blutoothid='"+txtbluetoothid.Text+"' where EmpBarcodeMasterID=" + ViewState["Id"] + "";
    //    SqlCommand cmd = new SqlCommand(str, conn);
    //    if (conn.State.ToString() != "Open")
    //    {
    //        conn.Open();
    //    }
    //    cmd.ExecuteNonQuery();
    //    conn.Close();
    //    lblmsg.Visible = true;
    //    lblmsg.Text = "Record updated successfully";
    //    //GridView1.DataSource = (DataSet)fillgrid();
    //    //GridView1.DataBind();

    //    grdTempData.EditIndex = -1;
    //    filldata();
    //}
    //else
    //{
    //    lblmsg.Visible = true;
    //    lblmsg.Text = "Record already used";
    //}

    // }
    protected void grdTempData_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdTempData.PageIndex = e.NewPageIndex;
        filldata();
    }
    protected void grdTempData_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder; // sortOrder;
        filldata();
    }
    protected void ddshorting_SelectedIndexChanged(object sender, EventArgs e)
    {
        filldata();
    }
    protected void ImageButton49_Click(object sender, ImageClickEventArgs e)
    {
        string te = "EmployeeMaster.aspx";
        ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "window.open('" + te + "');", true);

    }
    protected void ImageButton48_Click(object sender, ImageClickEventArgs e)
    {
        fillemployee();
    }
    protected void fillemployee()
    {
        DataTable dsemp = ClsStore.SelectEmployeewithBusinessId(DropDownList1.SelectedValue);
        DropDownList2.DataSource = dsemp;
        DropDownList2.DataTextField = "EmployeeName";
        DropDownList2.DataValueField = "EmployeeMasterID";
        DropDownList2.DataBind();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (Button1.Text == "Printable Version")
        {
            pnlgrid.ScrollBars = ScrollBars.None;
            pnlgrid.Height = new Unit("100%");

            Button1.Text = "Hide Printable Version";
            Button2.Visible = true;
            //if (grdTempData.Columns[7].Visible == true)
            //{
            //    ViewState["statusHide"] = "tt";
            //    grdTempData.Columns[7].Visible = false;
            //}
            if (grdTempData.Columns[8].Visible == true)
            {
                ViewState["editHide"] = "tt";
                grdTempData.Columns[8].Visible = false;
            }
            //if (grdTempData.Columns[5].Visible == true)
            //{
            //    ViewState["deleteHide"] = "tt";
            //    grdTempData.Columns[5].Visible = false;
            //}


        }
        else
        {

            Button1.Text = "Printable Version";
            Button2.Visible = false;
            //if (ViewState["statusHide"] != null)
            //{
            //    grdTempData.Columns[7].Visible = true;
            //}
            if (ViewState["editHide"] != null)
            {
                grdTempData.Columns[8].Visible = true;
            }
            //if (ViewState["deleteHide"] != null)
            //{
            //    grdTempData.Columns[5].Visible = true;
            //}
        }
    }
    protected void btnupdate_Click(object sender, EventArgs e)
    {
        int biometricno = 0;
        int bluetoothno = 0;
        int empcode = 0;

        string strbiometricno = "Select * from EmployeeBarcodeMaster where Biometricno='" + txtbiometricid.Text + "' and  EmpBarcodeMasterID <>'" + ViewState["Id"] + "'";
        SqlCommand cmdbiometricno = new SqlCommand(strbiometricno, conn);
        SqlDataAdapter adpbiometricno = new SqlDataAdapter(cmdbiometricno);
        DataTable dsbiometricno = new DataTable();
        adpbiometricno.Fill(dsbiometricno);
        if (dsbiometricno.Rows.Count > 0)
        {
            biometricno = 1;

        }

        string strbluetoothno = "Select * from EmployeeBarcodeMaster where blutoothid='" + txtbluetoothid.Text + "' and  EmpBarcodeMasterID <>'" + ViewState["Id"] + "'";
        SqlCommand cmdbluetoothno = new SqlCommand(strbluetoothno, conn);
        SqlDataAdapter adpbluetoothno = new SqlDataAdapter(cmdbluetoothno);
        DataTable dsbluetoothno = new DataTable();
        adpbluetoothno.Fill(dsbluetoothno);
        if (dsbluetoothno.Rows.Count > 0)
        {
            bluetoothno = 1;

        }

        string strempcodecheck = " select * from EmployeeBarcodeMaster inner join EmployeeMaster on EmployeeMaster.EmployeeMasterID=EmployeeBarcodeMaster.Employee_Id inner join Party_master on Party_master.PartyID=EmployeeMaster.PartyID where EmployeeBarcodeMaster.Employeecode='" + txtemployeecode + "' and Party_master.id='" + Session["comid"] + "' and  EmployeeBarcodeMaster.EmpBarcodeMasterID <>'" + ViewState["Id"] + "'";
        SqlCommand cmdempcodecheck = new SqlCommand(strempcodecheck, conn);
        SqlDataAdapter adpempcodecheck = new SqlDataAdapter(cmdempcodecheck);
        DataTable dsempcodecheck = new DataTable();
        adpempcodecheck.Fill(dsempcodecheck);
        if (dsempcodecheck.Rows.Count > 0)
        {
            empcode = 1;
        }



        if (biometricno == 1 || bluetoothno == 1 || empcode == 1)
        {
            if (biometricno == 1)
            {
                lblmsg.Visible = true;
                lblmsg.Text = "This Bio-metric Id is already in use";


            }
            if (bluetoothno == 1)
            {
                lblmsg.Visible = true;
                lblmsg.Text = "This Bluetooth Id is already in use";

            }
            if (empcode == 1)
            {
                lblmsg.Visible = true;
                lblmsg.Text = "This Employee Code is already in use";

            }

        }
        else
        {




            string strrrr = "Select [EmployeeBarcodeMaster].[Employee_Id] from [EmployeeBarcodeMaster] where ( [EmployeeBarcodeMaster].[Employee_Id] = '" + DropDownList2.SelectedValue + "' or EmployeeBarcodeMaster.Barcode='" + TextBox1.Text + "') and  EmpBarcodeMasterID <>'" + ViewState["Id"] + "'";
            SqlCommand cmd1 = new SqlCommand(strrrr, conn);
            SqlDataAdapter adp = new SqlDataAdapter(cmd1);
            DataTable ds = new DataTable();
            adp.Fill(ds);

            if (ds.Rows.Count == 0)
            {
                string str = "Update EmployeeBarcodeMaster set Whid='" + DropDownList1.SelectedValue + "',Employee_Id ='" + DropDownList2.SelectedValue + "',Barcode='" + TextBox1.Text + "',[Active]='" + ddlstatus.SelectedValue + "',Employeecode='" + txtemployeecode.Text + "',Biometricno='" + txtbiometricid.Text + "',blutoothid='" + txtbluetoothid.Text + "' where EmpBarcodeMasterID=" + ViewState["Id"] + "";
                SqlCommand cmd = new SqlCommand(str, conn);
                if (conn.State.ToString() != "Open")
                {
                    conn.Open();
                }
                cmd.ExecuteNonQuery();
                conn.Close();
                lblmsg.Visible = true;
                lblmsg.Text = "Record updated successfully";


                //grdTempData.EditIndex = -1;
                filldata();
                cleartext();

                Button3.Visible = false;
                btnupdate.Visible = false;
                pnlhideshow.Visible = false;
                btnshowbarcode.Visible = false;
            }
            else
            {
                lblmsg.Visible = true;
                lblmsg.Text = "This Barcode is already in use";
            }
        }
        //else
        //{
        //    lblmsg.Visible = true;
        //    lblmsg.Text = "Record already used";
        //}

    }
    protected void btnshowbarcode_Click(object sender, EventArgs e)
    {
        if (pnlhideshow.Visible == false)
        {
            pnlhideshow.Visible = true;

        }
        else if (pnlhideshow.Visible == true)
        {
            pnlhideshow.Visible = false;

        }
        lblmsg.Text = "";
        btnshowbarcode.Visible = false;
    }
}