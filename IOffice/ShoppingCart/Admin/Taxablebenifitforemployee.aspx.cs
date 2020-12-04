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

public partial class Taxablebenifitforemployee : System.Web.UI.Page
{
    SqlConnection con=new SqlConnection(PageConn.connnn);
    protected void Page_Load(object sender, EventArgs e)
    {
        //PageConn pgcon = new PageConn();
        //con = pgcon.dynconn;
        pagetitleclass pg = new pagetitleclass();
        string strData = Request.Url.ToString();

        char[] separator = new char[] { '/' };

        string[] strSplitArr = strData.Split(separator);
        int i = Convert.ToInt32(strSplitArr.Length);
        string page = strSplitArr[i - 1].ToString();


        Page.Title = pg.getPageTitle(page);
        if (!IsPostBack)
        {
            txtreqdate.Text = DateTime.Now.ToShortDateString();
            ViewState["sortOrder"] = "";
            Pagecontrol.dypcontrol(Page, page);
           
            Fillwarehouse();
            fillemployee();
            fillfilteremployee();
            filltaxben();
            fillfiltertaxben();
            txttaxablename_SelectedIndexChanged(sender, e);
                   gridfun();
        }

    }
  
    protected void Fillwarehouse()
    {

       
      DataTable ds = ClsStore.SelectStorename();
      if (ds.Rows.Count > 0)
      {
          ddlstrname.DataSource = ds;
          ddlstrname.DataValueField = "WareHouseId";
          ddlstrname.DataTextField = "Name";
          ddlstrname.DataBind();
          ddlfilterbus.DataSource = ds;
          ddlfilterbus.DataValueField = "WareHouseId";
          ddlfilterbus.DataTextField = "Name";
          ddlfilterbus.DataBind();
          ddlfilterbus.Items.Insert(0, "All");
          ddlfilterbus.Items[0].Value = "0";
          DataTable dteeed = ClsStore.SelectEmployeewithIdwise();

          if (dteeed.Rows.Count > 0)
          {

              ddlstrname.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);


          }
      }
        
    }



    protected void fillemployee()
    {
        string str = "SELECT distinct EmployeeMaster.EmployeeName,EmployeeMaster.Whid,EmployeeMaster.EmployeeMasterID,EmployeeMaster.DesignationMasterId FROM   EmployeeMaster where EmployeeMaster.Whid='" + ddlstrname.SelectedValue + "' order by EmployeeMaster.EmployeeName";
        SqlCommand cmd1 = new SqlCommand(str, con);
        SqlDataAdapter adap1 = new SqlDataAdapter(cmd1);
        DataSet ds1 = new DataSet();
        adap1.Fill(ds1);
        ddlemp.DataSource = ds1;
        ddlemp.DataValueField = "EmployeeMasterId";
        ddlemp.DataTextField = "EmployeeName";
        ddlemp.DataBind();
        ddlemp.Items.Insert(0, "-Select-");
        ddlemp.Items[0].Value = "0";

    }
    protected void filltaxben()
    {
        string str = "SELECT distinct Taxablebenifitname,Id FROM   TaxablebenifitMasterTbl where Status='1' and Whid='" + ddlstrname.SelectedValue + "' order by Taxablebenifitname";
        SqlCommand cmd1 = new SqlCommand(str, con);
        SqlDataAdapter adap1 = new SqlDataAdapter(cmd1);
        DataSet ds1 = new DataSet();
        adap1.Fill(ds1);
        ddltaxbenifitname.DataSource = ds1;
        ddltaxbenifitname.DataValueField = "Id";
        ddltaxbenifitname.DataTextField = "Taxablebenifitname";
        ddltaxbenifitname.DataBind();
        ddltaxbenifitname.Items.Insert(0, "-Select-");
        ddltaxbenifitname.Items[0].Value = "0";

    }
    protected void fillfilteremployee()
    {
        ddlfilteremp.Items.Clear();
        if (ddlfilterbus.SelectedIndex > 0)
        {
            string str = "SELECT distinct EmployeeMaster.EmployeeName,EmployeeMaster.Whid,EmployeeMaster.EmployeeMasterID,EmployeeMaster.DesignationMasterId FROM   EmployeeMaster where EmployeeMaster.Whid='" + ddlfilterbus.SelectedValue + "' order by EmployeeMaster.EmployeeName";
            SqlCommand cmd1 = new SqlCommand(str, con);
            SqlDataAdapter adap1 = new SqlDataAdapter(cmd1);
            DataSet ds1 = new DataSet();
            adap1.Fill(ds1);
            ddlfilteremp.DataSource = ds1;
            ddlfilteremp.DataValueField = "EmployeeMasterId";
            ddlfilteremp.DataTextField = "EmployeeName";
            ddlfilteremp.DataBind();
        }
        ddlfilteremp.Items.Insert(0, "All");
        ddlfilteremp.Items[0].Value = "0";

    }
    protected void fillfiltertaxben()
    {
        ddlfiltertaxpay.Items.Clear();
        if (ddlfilterbus.SelectedIndex > 0)
        {
            string str = "SELECT distinct Taxablebenifitname,Id FROM   TaxablebenifitMasterTbl where  Status='1' and Whid='" + ddlfilterbus.SelectedValue + "' order by Taxablebenifitname";
            SqlCommand cmd1 = new SqlCommand(str, con);
            SqlDataAdapter adap1 = new SqlDataAdapter(cmd1);
            DataSet ds1 = new DataSet();
            adap1.Fill(ds1);
            ddlfiltertaxpay.DataSource = ds1;
            ddlfiltertaxpay.DataValueField = "Id";
            ddlfiltertaxpay.DataTextField = "Taxablebenifitname";
            ddlfiltertaxpay.DataBind();
        }
        ddlfiltertaxpay.Items.Insert(0, "All");
        ddlfiltertaxpay.Items[0].Value = "0";

    }
    public void gridfun()
    {
        lblCompany.Text = "Business : " +ddlfilterbus.SelectedItem.Text;
        lblemp.Text = "Employee : " + Convert.ToString(ddlfilteremp.SelectedItem.Text);
        lbltax.Text = "Tax Benefits : " + Convert.ToString(ddlfiltertaxpay.SelectedItem.Text);
       
        string dty = "";
        if (ddlfilterbus.SelectedIndex > 0)
        {
            dty += " and TaxablebenifitforemployeeTbl.Whid='" + ddlfilterbus.SelectedValue + "'";
        }
        if (ddlfilteremp.SelectedIndex > 0)
        {
            dty += " and TaxablebenifitforemployeeTbl.EmployeeId='" + ddlfilteremp.SelectedValue + "'";
        }
        if (ddlfiltertaxpay.SelectedIndex > 0)
        {
            dty += " and TaxablebenifitforemployeeTbl.TaxablebanifilId='" + ddlfiltertaxpay.SelectedValue + "'";
        }
        dty += " and TaxablebenifitforemployeeTbl.Status='" + ddlfilterstatus.SelectedValue + "'";

        string em = "SELECT dISTINCT  Case when(TaxablebenifitforemployeeTbl.Status='1') then 'Active' else 'Inactive' end as Status,  TaxablebenifitforemployeeTbl.ID, WarehouseMaster.Name as Wname,A1.Taxablebenifitname,A2.EmployeeName,TaxablebenifitforemployeeTbl.Amount,Convert(Nvarchar,TaxablebenifitforemployeeTbl.Date,101) as Date,Case When(RecrringType='1')then'Yes' else 'No' End as RecrringType   FROM  WarehouseMaster  inner join TaxablebenifitforemployeeTbl on TaxablebenifitforemployeeTbl.Whid= WarehouseMaster.WarehouseId inner join TaxablebenifitMasterTbl as A1 on A1.Id=TaxablebenifitforemployeeTbl.TaxablebanifilId  inner join EmployeeMaster as A2 on A2.EmployeeMasterId = TaxablebenifitforemployeeTbl.EmployeeId where A1.Status='1' and WareHouseMaster.comid='" + Session["Comid"] + "'" + dty + "  ORDER BY TaxablebenifitforemployeeTbl.ID dESC";
        SqlCommand cmd3 = new SqlCommand(em, con);
        SqlDataAdapter adap3 = new SqlDataAdapter(cmd3);
        DataTable ds3 = new DataTable();

        adap3.Fill(ds3);

        GridView1.DataSource = ds3;
        if (ds3.Rows.Count > 0)
        {
            DataView myDataView = new DataView();
            myDataView = ds3.DefaultView;
            if (hdnsortExp.Value != string.Empty)
            {
                myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
            }

        }
        GridView1.DataBind();
      
    }
  


    protected void ddlstrname_SelectedIndexChanged1(object sender, EventArgs e)
    {
        fillemployee();
        filltaxben();
        lblmsg.Text = "";
    }


  
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        string payval = "";
        if (ddlrecringbenifit.SelectedIndex == 0)
        {
            payval = " and Amount=" + txtamt.Text;
        }
        else if (ddlrecringbenifit.SelectedIndex == 1)
        {
          payval = " and Date=" + txtreqdate.Text;
        }
        string str = " select * from TaxablebenifitforemployeeTbl where Whid='" + ddlstrname.SelectedValue + "'  and [EmployeeId] = '" + ddlemp.SelectedValue + "' and [TaxablebanifilId] = '" + ddltaxbenifitname.SelectedValue + "' " + payval;
        
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable ds = new DataTable();
        adp.Fill(ds);
        if (ds.Rows.Count > 0)
        {
            lblmsg.Visible = true;
            lblmsg.Text = "Record already exists..";
        }
        else
        {
            string strd = "";
            if (ddlrecringbenifit.SelectedIndex == 0)
            {
                strd = "insert into TaxablebenifitforemployeeTbl(Whid,EmployeeId,TaxablebanifilId,Amount,RecrringType,Status) values ('" + ddlstrname.SelectedValue + "','" + ddlemp.Text + "','" + ddltaxbenifitname.SelectedValue + "','" + txtamt.Text + "','" + ddlrecringbenifit.SelectedValue + "','"+ddlstatus.SelectedValue+"') ";
        
            }
            else if (ddlrecringbenifit.SelectedIndex == 1)
            {
                strd = "insert into TaxablebenifitforemployeeTbl(Whid,EmployeeId,TaxablebanifilId,Amount,RecrringType,Date,Status) values ('" + ddlstrname.SelectedValue + "','" + ddlemp.Text + "','" + ddltaxbenifitname.SelectedValue + "','" + txtamt.Text + "','" + ddlrecringbenifit.SelectedValue + "','" + txtreqdate.Text + "','" + ddlstatus.SelectedValue + "') ";
        
            }
                SqlCommand cmd1d = new SqlCommand(strd, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmd1d.ExecuteNonQuery();

            lblmsg.Visible = true;

            lblmsg.Text = "Record inserted successfully";
            gridfun();
            clear();
        }
          

    }
    protected void clear()
    {
        ddlemp.SelectedIndex = 0;
       
        ddltaxbenifitname.SelectedIndex = 0;
        txtamt.Text = "";
        ddlstatus.SelectedIndex = 0;
    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
      
    }
    protected void btnupdate_Click(object sender, EventArgs e)
    {
       
        string payval = "";
        if (ddlrecringbenifit.SelectedIndex == 0)
        {
            payval = " and Amount=" + txtamt.Text;
        }
        else if (ddlrecringbenifit.SelectedIndex == 1)
        {
            payval = " and Date=" + txtreqdate.Text;
        }
        string str = " select * from TaxablebenifitforemployeeTbl where Whid='" + ddlstrname.SelectedValue + "'  and Id<>'" + ViewState["MasterId"] + "'  and [EmployeeId] = '" + ddlemp.SelectedValue + "' and [TaxablebanifilId] = '" + ddltaxbenifitname.SelectedValue + "' " + payval;

        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable ds = new DataTable();
        adp.Fill(ds);
        if (ds.Rows.Count > 0)
        {
            lblmsg.Visible = true;
            lblmsg.Text = "Record already exists..";
        }
        else
        {
            string strd = "";
            if (ddlrecringbenifit.SelectedIndex == 0)
            {
                strd = "Update  TaxablebenifitforemployeeTbl Set Status='" + ddlstatus.SelectedValue + "', Whid='" + ddlstrname.SelectedValue + "',EmployeeId='" + ddlemp.Text + "',TaxablebanifilId='" + ddltaxbenifitname.SelectedValue + "',Amount='" + txtamt.Text + "' where  Id='" + ViewState["MasterId"] + "'";

            }
            else if (ddlrecringbenifit.SelectedIndex == 1)
            {
                strd = "Update  TaxablebenifitforemployeeTbl Set Status='" + ddlstatus.SelectedValue + "', Whid='" + ddlstrname.SelectedValue + "',EmployeeId='" + ddlemp.Text + "',TaxablebanifilId='" + ddltaxbenifitname.SelectedValue + "',Amount='" + txtamt.Text + "',Date='" + txtreqdate.Text + "' where  Id='" + ViewState["MasterId"] + "'";

            }
            
                                
                                SqlCommand cmd1d = new SqlCommand(strd, con);
                                if (con.State.ToString() != "Open")
                                {
                                    con.Open();
                                }
                                cmd1d.ExecuteNonQuery();
                                con.Close();
                                lblmsg.Visible = true;
                                ddlrecringbenifit.Enabled = true;
            lblmsg.Text = "Record updated successfully";
            btnsubmit.Visible = true;
            btnupdate.Visible = false;
            gridfun();
            clear();

        }



    }

    protected void btncancel_Click(object sender, EventArgs e)
    {
     
        
        lblmsg.Text = "";
        ddlrecringbenifit.Enabled = true;
        btnsubmit.Visible = true;
  
        btnupdate.Visible = false;
        btncancel.Visible = false;
        clear();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (Button1.Text == "Printable Version")
        {
            pnlgrid.ScrollBars = ScrollBars.None;
            pnlgrid.Height = new Unit("100%");

            Button1.Text = "Hide Printable Version";
            Button7.Visible = true;
            if (GridView1.Columns[7].Visible == true)
            {
                ViewState["edith"] = "tt";
                GridView1.Columns[7].Visible = false;
            }
            if (GridView1.Columns[8].Visible == true)
            {
                ViewState["deleteh"] = "tt";
                GridView1.Columns[8].Visible = false;
            }
           
        }
        else
        {



            Button1.Text = "Printable Version";
            Button7.Visible = false;
            if (ViewState["edith"] != null)
            {
                GridView1.Columns[7].Visible = true;
            }
            if (ViewState["deleteh"] != null)
            {
                GridView1.Columns[8].Visible = true;
            }
            
        }
    }

    protected void ddlfilterbus_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillfilteremployee();
        fillfiltertaxben();
        gridfun();
    }

   
    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder;
        gridfun();

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
        if (e.CommandName == "Select")
        {
            lblmsg.Text = "";
            GridView1.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            ViewState["MasterId"] = GridView1.SelectedDataKey.Value;

            btnsubmit.Visible = false;
            btnupdate.Visible = true;
            string strsel = "select TaxablebenifitforemployeeTbl.* from TaxablebenifitforemployeeTbl where Id='" + ViewState["MasterId"] + "'";
            SqlCommand cmdview = new SqlCommand(strsel, con);
            SqlDataAdapter dtpview = new SqlDataAdapter(cmdview);
            DataTable dtview = new DataTable();
            dtpview.Fill(dtview);
            if (dtview.Rows.Count > 0)
            {
                ddlstrname.SelectedIndex = ddlstrname.Items.IndexOf(ddlstrname.Items.FindByValue(dtview.Rows[0]["Whid"].ToString()));
                fillemployee();
                ddlemp.SelectedIndex = ddlemp.Items.IndexOf(ddlemp.Items.FindByValue(dtview.Rows[0]["EmployeeId"].ToString()));
                filltaxben();
                ddltaxbenifitname.SelectedIndex = ddltaxbenifitname.Items.IndexOf(ddltaxbenifitname.Items.FindByValue(dtview.Rows[0]["TaxablebanifilId"].ToString()));
                ddlstatus.SelectedIndex = ddlstatus.Items.IndexOf(ddlstatus.Items.FindByValue(Convert.ToInt32(dtview.Rows[0]["Status"]).ToString()));

               
           
              if( Convert.ToString(dtview.Rows[0]["RecrringType"])=="True")
              {
                     ddlrecringbenifit.SelectedIndex=0;
              }
              else
              {
                   ddlrecringbenifit.SelectedIndex=1;
                   txtreqdate.Text = Convert.ToDateTime(dtview.Rows[0]["Date"]).ToShortDateString();
              }
              ddlrecringbenifit.Enabled = false;
              txttaxablename_SelectedIndexChanged(sender, e);
              txtamt.Text = Convert.ToString(dtview.Rows[0]["Amount"]);

               


            }
         
        }
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string st2 = "Delete from TaxablebenifitforemployeeTbl where Id='" + GridView1.DataKeys[e.RowIndex].Value.ToString() + "' ";
        SqlCommand cmd2 = new SqlCommand(st2, con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
       int obs=cmd2.ExecuteNonQuery();
        con.Close();
        if (obs > 0)
        {
           
            GridView1.EditIndex = -1;
            gridfun();
            lblmsg.Visible = true;
            lblmsg.Text = "Record deleted succesfully";
        }
       
    }

    protected void txttaxablename_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlrecringbenifit.SelectedIndex == 0)
        {
            pnldate.Visible = false;
            lblamt.Text = "Amount per paycycle";
           
        }
        else if (ddlrecringbenifit.SelectedIndex == 1)
        {
            pnldate.Visible = true;
            lblamt.Text = "Amount";
        }
    }
    protected void ddlfilteremp_SelectedIndexChanged(object sender, EventArgs e)
    {
        gridfun();
    }
    protected void ddlfiltertaxpay_SelectedIndexChanged(object sender, EventArgs e)
    {
        gridfun();
    }
    protected void ddlfilterstatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        gridfun();

    }
}