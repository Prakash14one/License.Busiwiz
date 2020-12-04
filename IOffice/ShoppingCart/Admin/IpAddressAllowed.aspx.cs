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

public partial class Manage_Ip_Address_Allowed : System.Web.UI.Page
{
    SqlConnection con;
    string compid;
    string page;
    string dynip;
    int p;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Comid"] == null)
        {
            Response.Redirect("~/Login.aspx");
        }

        PageConn pgcon = new PageConn();
        con = pgcon.dynconn;
        pagetitleclass pg = new pagetitleclass();
        string strData = Request.Url.ToString();

        char[] separator = new char[] { '/' };
        compid = Session["comid"].ToString();
        string[] strSplitArr = strData.Split(separator);
        int i = Convert.ToInt32(strSplitArr.Length);
        string page = strSplitArr[i - 1].ToString();
        Page.Title = pg.getPageTitle(page);

        if (!IsPostBack)
        {
            Pagecontrol.dypcontrol(Page, page);
            ViewState["sortOrder"] = "";
            lblCompany.Text = Session["Cname"].ToString();
            rdlist1_SelectedIndexChanged(sender, e);
            rdipright_SelectedIndexChanged(sender, e);
            DataTable dr = new DataTable();
            dr = ClsIp.SelctIpGridfill(ddlfilterbus.SelectedValue, 1, ddlfilteruser.SelectedValue);
            if (dr.Rows.Count == 0)
            {
                dr = ClsIp.SelctIpGridfill("0", 0, "0");
            }
            if (dr.Rows.Count > 0)
            {
                rdlist1.SelectedIndex = 0;
                rdipright.Items[0].Text = "List of IP Addresses Allowed for Any Users";
                rdipright.Items[1].Text = "List of IP Addresses Allowed for Specific Users";
                rdipright.SelectedIndex = 0;
                rdlist1_SelectedIndexChanged(sender, e);
                rdipright_SelectedIndexChanged(sender, e);
            }
            else
            {

                rdlist1_SelectedIndexChanged(sender, e);
            }
            FillGridView1();


        }
        if (IsPostBack)
        {
            dynip = HttpContext.Current.Request.UserHostName;

        }

    }


    protected void FillGridView1()
    {
        if (rdipright.SelectedIndex == 0)
        {
            alluseripcheck();
        }
        else
        {
            specificuseripcheck();
        }

        DataTable dr = new DataTable();
        if (rdipright.SelectedIndex == 1)
        {
            dr = ClsIp.SelctIpGridfill(ddlfilterbus.SelectedValue, 1, ddlfilteruser.SelectedValue);
            Label5.Visible = true;
            grduser.Columns[0].Visible = true;
            grduser.Columns[1].Visible = true;
            grduser.Columns[2].Visible = true;
        }
        else if (rdipright.SelectedIndex == 0)
        {
            dr = ClsIp.SelctIpGridfill("0", 0, "0");
            Label5.Visible = false;
            grduser.Columns[0].Visible = false;
            grduser.Columns[1].Visible = false;
            grduser.Columns[2].Visible = false;
        }
        grduser.DataSource = dr;

        if (dr.Rows.Count == 0)
        {
            btnadtolist.Visible = true;
            // lblalready.Visible = false;
            // pnlcurrentip.Visible = false;
        }

        p = 0;
        for (int i = 0; i < dr.Rows.Count; i++)
        {

            if (p == 0)
            {

                if (dr.Rows[i]["IpAddress"].ToString() == dynip)
                {
                    btnadtolist.Visible = false;
                    //lblalready.Visible = true;
                    // pnlcurrentip.Visible = true;

                    p = 1;
                }
                else
                {
                    btnadtolist.Visible = true;
                    // lblalready.Visible = false;
                    // pnlcurrentip.Visible = false;
                }
            }


        }
        DataView myDataView = new DataView();
        myDataView = dr.DefaultView;

        if (hdnsortExp.Value != string.Empty)
        {
            myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
        }

        grduser.DataBind();
        //if (grduser.Rows.Count > 0)
        //{
        //    pnlusergrid.Visible = true;
        //    pnlms.Visible = false;
        //}
        //else
        //{
        //    pnlusergrid.Visible = false;
        //    pnlms.Visible = true;

        //}

    }


    protected void grduser_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grduser.PageIndex = e.NewPageIndex;
        FillGridView1();
    }
    protected void grduser_Sorting(object sender, GridViewSortEventArgs e)
    {
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




    protected void ImageButton8_Click(object sender, ImageClickEventArgs e)
    {
        statuslable.Visible = false;
        txtIpAddress.Text = "";
    }




    protected void rdlist1_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdlist1.SelectedIndex == 0)
        {
            int ins = ClsIp.InsertIpControlMastertbl(Convert.ToBoolean(1));
            ViewState["ipallowip"] = ins.ToString();
            pnlbtn.Visible = true;
            pnlusercid.Visible = true;
        }
        else if (rdlist1.SelectedIndex == 1)
        {
            int ins = ClsIp.InsertIpControlMastertbl(Convert.ToBoolean(0));
            pnlbtn.Visible = false;
            pnlusercid.Visible = false;

        }
    }
    protected void btnaddn_Click(object sender, EventArgs e)
    {
        pnlusercid.Visible = true;
    }
    protected void rdipright_SelectedIndexChanged(object sender, EventArgs e)
    {
        pnls.Visible = true;
        if (rdipright.SelectedIndex == 0)
        {
            pnlexample1.Visible = true;
            pnlexample2.Visible = false;
            lblCompany.Text = Session["Cname"].ToString();
            lbluserp.Text = "";
            lblheadc.Text = "List of Allowed IP Addresses for Any Admin and Employee Users of the Company";
            Label4.Text = "List of Allowed IP Addresses for Any Admin and Employee Users of the Company";
            lblselecttype.Text = "The following IP Address will be added to the list of allowed IP addresses for all users of your company.";
            lblbusiness.Visible = false;
            pnlusershow.Visible = false;
            pnlfilteruser.Visible = false;
            FillGridView1();

            btnadtolist.Text = "Add to the Allowed IP Address List for All Users";
            btnaddn0.Text = "Add to the Allowed IP Address List for All Users";

            // pnlusershow.Visible = false;

            Button2.Text = "Add to the Allowed IP Address List for All User";

            pnlvs.Visible = false;
            lblselecttype.Visible = false;
            alluseripcheck();

        }
        else if (rdipright.SelectedIndex == 1)
        {
            specificuseripcheck();

            // pnlusershow.Visible = true;
            pnlvs.Visible = false;
            Button2.Text = "Add to the Allowed IP Address List for Specific User";
            btnadtolist.Text = "Add to the Allowed IP Address List for Specific User";
            btnaddn0.Text = "Add to the Allowed IP Address List for Specific User";
            lblselecttype.Visible = false;

            // pnldynmic.Visible = false;
            pnlexample1.Visible = false;
            pnlexample2.Visible = true;

            pnlfilteruser.Visible = true;
            lblselecttype.Text = "The following IP Address will be added to the list of allowed IP addresses for specific users of your company.";

            // lblheadc.Text = "List of Allowed Ip Address for Access to a particular user";
            lblheadc.Text = "List of Allowed IP Addresses for Any Admin and Employee Users of the Company";

            Label4.Text = "List of Allowed IP Addresses for Any Admin and Employee Users of the Company";
            DataTable ds = ClsStore.SelectStorename();
            //  pnlusershow.Visible = true;
            if (ds.Rows.Count > 0)
            {
                ddlbusiness.DataSource = ds;
                ddlbusiness.DataValueField = "WareHouseId";
                ddlbusiness.DataTextField = "Name";

                ddlbusiness.DataBind();
                DataTable dteeed = ClsStore.SelectEmployeewithIdwise();

                if (dteeed.Rows.Count > 0)
                {
                    ddlbusiness.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);
                }
                ddlfilterbus.DataSource = ds;
                ddlfilterbus.DataValueField = "WareHouseId";
                ddlfilterbus.DataTextField = "Name";
                ddlfilterbus.DataBind();
                ddlfilterbus.Items.Insert(0, "All");
                ddlfilterbus.Items[0].Value = "0";
                ddlfilteruser.Items.Insert(0, "All");
                ddlfilteruser.Items[0].Value = "0";
            }
            ddlbusiness_SelectedIndexChanged(sender, e);
            ddlfilterbus_SelectedIndexChanged(sender, e);
        }
    }
    protected void ddlbusiness_SelectedIndexChanged(object sender, EventArgs e)
    {

        ddluser.Items.Clear();
        DataTable ds = ClsIp.SelectUserbywhid(ddlbusiness.SelectedValue);

        if (ds.Rows.Count > 0)
        {
            ddluser.DataSource = ds;
            ddluser.DataValueField = "UserId";
            ddluser.DataTextField = "Username";

            ddluser.DataBind();

        }
        ddluser.Items.Insert(0, "Select");
        ddluser.Items[0].Value = "0";

    }
    protected int ret(string teb)
    {
        int flag = 0;
        if (teb.ToString() != "*")
        {
            if (teb.ToString() != "**" && teb.ToString() != "***")
            {
                string[] separator1 = new string[] { "*" };
                string[] strSplitArr1 = teb.Split(separator1, StringSplitOptions.RemoveEmptyEntries);
                if (strSplitArr1.Length > 1)
                {
                    flag = 1;
                }
                else
                {
                    int lt = 0;
                    if (strSplitArr1.Length == 1)
                    {
                        lt = strSplitArr1[0].Length;
                    }
                    if (lt == teb.Length)
                    {
                        if (Convert.ToInt32(teb) <= 255)
                        {
                        }
                        else
                        {
                            flag = 1;
                        }
                    }
                    else
                    {
                        flag = 1;
                    }
                }

            }
            else
            {
                flag = 1;
            }
        }
        return flag;
    }
    protected void btnaddn0_Click(object sender, EventArgs e)
    {

        int flag = ret(txtIpAddress.Text);
        int flag1 = ret(txtip1.Text);
        int flag2 = ret(txtip2.Text);
        int flag3 = ret(txtip3.Text);
        int inc = 0;
        if (flag == 0 && flag1 == 0 && flag2 == 0 && flag3 == 0)
        {
            string Ipaddd = txtIpAddress.Text + "." + txtip1.Text + "." + txtip2.Text + "." + txtip3.Text;
            DataTable dsp = ClsIp.SelctIpControlMastertblId();
            if (dsp.Rows.Count > 0)
            {
                if (rdipright.SelectedIndex == 0)
                {
                    inc = ClsIp.InsertIpAddress(Convert.ToInt32(dsp.Rows[0]["IpcontrolId"]), Convert.ToBoolean(1), Convert.ToBoolean(0), "0", Ipaddd);
                }
                else if (rdipright.SelectedIndex == 1)
                {
                    inc = ClsIp.InsertIpAddress(Convert.ToInt32(dsp.Rows[0]["IpcontrolId"]), Convert.ToBoolean(0), Convert.ToBoolean(1), ddluser.SelectedValue, Ipaddd);

                }
                if (inc > 0)
                {

                    SqlDataAdapter daq = new SqlDataAdapter("select * from TBLUserLoginIpRestrictionPreference where userid='" + ddluser.SelectedValue + "' and compid='" + Session["Comid"] + "'", con);
                    DataTable dys = new DataTable();
                    daq.Fill(dys);
                    if (dys.Rows.Count > 0)
                    {
                        SqlCommand cmf = new SqlCommand("update TBLUserLoginIpRestrictionPreference set MakeIPRestriction='1' where userid='" + ddluser.SelectedValue + "' and compid='" + Session["Comid"] + "' ", con);
                        con.Open();
                        cmf.ExecuteNonQuery();
                        con.Close();
                    }
                    else
                    {
                        SqlCommand cmf = new SqlCommand("insert into  TBLUserLoginIpRestrictionPreference(compid,userid, MakeIPRestriction)values('" + Session["Comid"] + "','" + ddluser.SelectedValue + "','1') ", con);
                        con.Open();
                        cmf.ExecuteNonQuery();
                        con.Close();
                    }
                    statuslable.Visible = true;
                    statuslable.Text = "Record inserted successfully";
                    txtIpAddress.Text = "";
                    txtip1.Text = "";
                    txtip2.Text = "";
                    txtip3.Text = "";
                    txtIpAddress.Focus();
                    FillGridView1();
                }
                else
                {
                    statuslable.Visible = true;
                    statuslable.Text = "Record alredy existed";

                }
            }
            else
            {
                statuslable.Visible = true;
                statuslable.Text = "Record not inserted";

            }
        }
        else
        {
            statuslable.Visible = true;
            statuslable.Text = "Please enter IP Address range between 0-255 or *";
        }
    }



    protected void grduser_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        // Get the ID of the record to be deleted
        string id = grduser.DataKeys[e.RowIndex].Value.ToString();

        int axd = ClsIp.DeleteIpAddress(Convert.ToInt32(id));

        if (axd > 0)
        {
            statuslable.Visible = true;
            statuslable.Text = "Record deleted successfully";
            FillGridView1();
        }
        else
        {
            statuslable.Visible = true;
            statuslable.Text = "Sorry,record not deleted";
        }
    }



    protected void Button1_Click(object sender, EventArgs e)
    {
        if (Button1.Text == "Printable Version")
        {
            //pnlgrid.ScrollBars = ScrollBars.None;
            //pnlgrid.Height = new Unit("100%");

            Button1.Text = "Hide Printable Version";
            Button7.Visible = true;
            if (grduser.Columns[4].Visible == true)
            {
                ViewState["delHide"] = "tt";
                grduser.Columns[4].Visible = false;
            }
        }
        else
        {

            //pnlgrid.ScrollBars = ScrollBars.Vertical;
            //pnlgrid.Height = new Unit(300);

            Button1.Text = "Printable Version";
            Button7.Visible = false;
            if (ViewState["delHide"] != null)
            {
                grduser.Columns[4].Visible = true;
            }
        }
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        //if (Button2.Text == "Add New IP Address")
        //{
        //    Button2.Text = "Hide New IP Address";
        //    pnlvs.Visible = true;
        //    lbldynip.Text = dynip;

        //    if (rdipright.SelectedIndex == 0)
        //    {
        //        pnlusershow.Visible = false;
        //    }
        //    else
        //    {
        //        pnlusershow.Visible = true;
        //    }

        //}
        //else
        //{
        //    Button2.Text = "Add New IP Address";
        //    pnlvs.Visible = false;

        //    pnlusershow.Visible = false;


        //}
        if (rdipright.SelectedIndex == 0)
        {
            if (Button2.Text == "Add to the Allowed IP Address List for All User")
            {
                Button2.Text = "Hide the Allowed IP Address List for All User";
                pnlvs.Visible = true;
                lbldynip.Text = dynip;
                lblselecttype.Visible = true;

                if (rdipright.SelectedIndex == 0)
                {
                    pnlusershow.Visible = false;
                }
                else
                {
                    pnlusershow.Visible = true;
                }

            }
            else
            {
                Button2.Text = "Add to the Allowed IP Address List for All User";
                pnlvs.Visible = false;

                pnlusershow.Visible = false;
                lblselecttype.Visible = false;


            }
        }
        else
        {

            if (Button2.Text == "Add to the Allowed IP Address List for Specific User")
            {
                Button2.Text = "Hide the Allowed IP Address List for Specific User";
                pnlvs.Visible = true;
                lbldynip.Text = dynip;
                lblselecttype.Visible = true;
                if (rdipright.SelectedIndex == 0)
                {
                    pnlusershow.Visible = false;
                }
                else
                {
                    pnlusershow.Visible = true;
                }
                if (ddluser.SelectedIndex > 0)
                {
                    pnlvs.Visible = true;
                }
                else
                {
                    pnlvs.Visible = false;
                }

            }
            else
            {
                Button2.Text = "Add to the Allowed IP Address List for Specific User";
                pnlvs.Visible = false;

                pnlusershow.Visible = false;
                lblselecttype.Visible = false;

                //if (ddluser.SelectedIndex > 0)
                //{
                //    pnlvs.Visible = true;
                //}
                //else
                //{
                //    pnlvs.Visible = false;
                //}


            }
        }

    }
    protected void ddlfilterbus_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblbusiness.Visible = true;
        lblbusiness.Text = "Business : " + ddlfilterbus.SelectedItem.Text;

        ddlfilteruser.Items.Clear();
        DataTable ds = ClsIp.SelectUserbywhid(ddlfilterbus.SelectedValue);

        if (ds.Rows.Count > 0)
        {
            ddlfilteruser.DataSource = ds;
            ddlfilteruser.DataValueField = "UserId";
            ddlfilteruser.DataTextField = "Username";

            ddlfilteruser.DataBind();

        }
        ddlfilteruser.Items.Insert(0, "All");
        ddlfilteruser.Items[0].Value = "0";
        lbluserp.Text = ddlfilteruser.SelectedItem.Text;
        FillGridView1();

    }
    protected void ddlfilteruser_SelectedIndexChanged(object sender, EventArgs e)
    {
        lbluserp.Text = ddlfilteruser.SelectedItem.Text;

        FillGridView1();
    }
    protected void btnadtolist_Click(object sender, EventArgs e)
    {

        int inc1 = 0;
        DataTable dsp1 = ClsIp.SelctIpControlMastertblId();
        if (dsp1.Rows.Count > 0)
        {
            if (rdipright.SelectedIndex == 0)
            {
                string str45 = "Select IpControldetailtbl.* from IpControlMastertbl inner join IpControldetailtbl on IpControldetailtbl.IpcontrolId= IpControlMastertbl.IpcontrolId  where CID='" + Session["comid"].ToString() + "'  and Ipaddress='" + dynip + "' and IpControldetailtbl.Cidwise='True' and Userwise='False' ";
                SqlCommand cmd45 = new SqlCommand(str45, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd45);
                DataTable ds = new DataTable();
                da.Fill(ds);
                if (ds.Rows.Count > 0)
                {
                    statuslable.Visible = true;
                    statuslable.Text = "Record already exist";
                }
                else
                {
                    inc1 = ClsIp.InsertIpAddress(Convert.ToInt32(dsp1.Rows[0]["IpcontrolId"]), Convert.ToBoolean(1), Convert.ToBoolean(0), "0", dynip);
                    statuslable.Visible = true;
                    statuslable.Text = "Record inserted successfully";
                }



            }
            else
            {
                string str45 = "Select IpControldetailtbl.* from IpControlMastertbl inner join IpControldetailtbl on IpControldetailtbl.IpcontrolId= IpControlMastertbl.IpcontrolId  where CID='" + Session["comid"].ToString() + "'  and Ipaddress='" + dynip + "' and IpControldetailtbl.Cidwise='False' and Userwise='True' and IpControldetailtbl.UserId='" + ddluser.SelectedValue + "' ";
                SqlCommand cmd45 = new SqlCommand(str45, con);
                SqlDataAdapter da = new SqlDataAdapter(cmd45);
                DataTable ds = new DataTable();
                da.Fill(ds);

                if (ds.Rows.Count > 0)
                {
                    statuslable.Visible = true;
                    statuslable.Text = "Record already exist";
                }
                else
                {
                    inc1 = ClsIp.InsertIpAddress(Convert.ToInt32(dsp1.Rows[0]["IpcontrolId"]), Convert.ToBoolean(0), Convert.ToBoolean(1), ddluser.SelectedValue, dynip);

                    statuslable.Visible = true;
                    statuslable.Text = "Record inserted successfully";

                }

            }



            FillGridView1();

        }



    }
    protected void ddluser_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddluser.SelectedIndex > 0)
        {
            pnlvs.Visible = true;
        }
        else
        {
            pnlvs.Visible = false;
        }
        specificuseripcheck();
    }
    protected void alluseripcheck()
    {
        string str45 = "Select IpControldetailtbl.* from IpControlMastertbl inner join IpControldetailtbl on IpControldetailtbl.IpcontrolId= IpControlMastertbl.IpcontrolId  where CID='" + Session["comid"].ToString() + "'  and Ipaddress='" + dynip + "' and IpControldetailtbl.Cidwise='True' and Userwise='False' ";
        SqlCommand cmd45 = new SqlCommand(str45, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd45);
        DataTable ds = new DataTable();
        da.Fill(ds);

        if (ds.Rows.Count > 0)
        {
            pnlcurrentip.Visible = false;

        }
        else
        {


            pnlcurrentip.Visible = true;

        }


    }
    protected void specificuseripcheck()
    {
        string str45 = "Select IpControldetailtbl.* from IpControlMastertbl inner join IpControldetailtbl on IpControldetailtbl.IpcontrolId= IpControlMastertbl.IpcontrolId  where CID='" + Session["comid"].ToString() + "'  and Ipaddress='" + dynip + "' and IpControldetailtbl.Cidwise='False' and Userwise='True' and IpControldetailtbl.UserId='" + ddluser.SelectedValue + "' ";
        SqlCommand cmd45 = new SqlCommand(str45, con);
        SqlDataAdapter da = new SqlDataAdapter(cmd45);
        DataTable ds = new DataTable();
        da.Fill(ds);

        if (ds.Rows.Count > 0)
        {
            pnlcurrentip.Visible = false;
        }
        else
        {

            pnlcurrentip.Visible = true;

        }


    }

}
