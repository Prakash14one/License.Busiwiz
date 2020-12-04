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

public partial class TaxablebenifitMaster : System.Web.UI.Page
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
           
            ViewState["sortOrder"] = "";
            Pagecontrol.dypcontrol(Page, page);
           
            Fillwarehouse();

            fillaccount();
           
            lblCompany.Text = Session["Cname"].ToString();
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
 
   
  
    protected void fillaccount()
    {
        string str = "SELECT distinct Id,AccountName FROM   AccountMaster where ClassId='13' and Whid='" + ddlstrname.SelectedValue + "' and Status='1' Order by AccountName";
        SqlCommand cmd1 = new SqlCommand(str, con);
        SqlDataAdapter adap1 = new SqlDataAdapter(cmd1);
        DataSet ds1 = new DataSet();
        adap1.Fill(ds1);
        ddlacccredit.DataSource = ds1;
        ddlacccredit.DataValueField = "Id";
        ddlacccredit.DataTextField = "AccountName";
        ddlacccredit.DataBind();
        ddlacccredit.Items.Insert(0, "-Select-");
        ddlacccredit.Items[0].Value = "0";
        ddlaccdebit.DataSource = ds1;
        ddlaccdebit.DataValueField = "Id";
        ddlaccdebit.DataTextField = "AccountName";
        ddlaccdebit.DataBind();
        ddlaccdebit.Items.Insert(0, "-Select-");
        ddlaccdebit.Items[0].Value = "0";
    
    }
  
    public void gridfun()
    {
        lblCompany.Text=" Busines : " +ddlfilterbus.SelectedItem.Text;
      
        string dty = "";
        if (ddlfilterbus.SelectedIndex > 0)
        {
            dty += " and TaxablebenifitMasterTbl.Whid='" + ddlfilterbus.SelectedValue + "'";
        }
        dty += " and TaxablebenifitMasterTbl.Status='" + ddlfilterstatus.SelectedValue + "'";

        string em = "SELECT dISTINCT Case when(TaxablebenifitMasterTbl.Status='1') then 'Active' else 'Inactive' end as Status, TaxablebenifitMasterTbl.ID, WarehouseMaster.Name as Wname,TaxablebenifitMasterTbl.Taxablebenifitname,A1.AccountName as accdebit,A2.AccountName as acccredit FROM  WarehouseMaster  inner join TaxablebenifitMasterTbl on TaxablebenifitMasterTbl.Whid= WarehouseMaster.WarehouseId inner join AccountMaster as A1 on A1.Id=TaxablebenifitMasterTbl.AccDebitId  inner join AccountMaster as A2 on A2.Id = TaxablebenifitMasterTbl.AccCreditId where WareHouseMaster.comid='" + Session["Comid"] + "'" + dty + "  ORDER BY taxablebenifitMasterTbl.ID dESC";
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
        fillaccount();
        lblmsg.Text = "";
    }


  
    protected void btnsubmit_Click(object sender, EventArgs e)
    {

        string str = " select * from TaxablebenifitMasterTbl where Whid='" + ddlstrname.SelectedValue + "'  and [Taxablebenifitname] = '" + txttaxablename.Text + "' ";
        
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

            string strd = "insert into TaxablebenifitMasterTbl(Whid,Taxablebenifitname,AccDebitId,AccCreditId,Status) values ('" + ddlstrname.SelectedValue + "','" + txttaxablename.Text + "','" + ddlaccdebit.SelectedValue + "','"+ddlacccredit.SelectedValue+"','"+ddlstatus.SelectedValue+"') ";
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
        ddlaccdebit.SelectedIndex = 0;
        txttaxablename.Text = "";
        ddlacccredit.SelectedIndex = 0;
        ddlstatus.SelectedIndex = 0;
       
    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {
      
    }
    protected void btnupdate_Click(object sender, EventArgs e)
    {
        string str = " select * from TaxablebenifitMasterTbl where Whid='" + ddlstrname.SelectedValue + "'  and [Taxablebenifitname] = '" + txttaxablename.Text + "' and Id<>'" + ViewState["MasterId"] + "' ";
        
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
            String strd = "Update  TaxablebenifitMasterTbl Set  Status='" + ddlstatus.SelectedValue + "', Whid='" + ddlstrname.SelectedValue + "',Taxablebenifitname='" + txttaxablename.Text + "',AccDebitId='" + ddlaccdebit.SelectedValue + "',AccCreditId='" + ddlacccredit.SelectedValue + "' where  Id='" + ViewState["MasterId"] + "'";

                                
                                SqlCommand cmd1d = new SqlCommand(strd, con);
                                if (con.State.ToString() != "Open")
                                {
                                    con.Open();
                                }
                                cmd1d.ExecuteNonQuery();
                                con.Close();
                                lblmsg.Visible = true;
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
      
        btnsubmit.Visible = true;
  
        btnupdate.Visible = false;
        btncancel.Visible = true;
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
            if (GridView1.Columns[5].Visible == true)
            {
                ViewState["edith"] = "tt";
                GridView1.Columns[5].Visible = false;
            }
            if (GridView1.Columns[6].Visible == true)
            {
                ViewState["deleteh"] = "tt";
                GridView1.Columns[6].Visible = false;
            }
           
        }
        else
        {



            Button1.Text = "Printable Version";
            Button7.Visible = false;
            if (ViewState["edith"] != null)
            {
                GridView1.Columns[5].Visible = true;
            }
            if (ViewState["deleteh"] != null)
            {
                GridView1.Columns[6].Visible = true;
            }
            
        }
    }

    protected void ddlfilterbus_SelectedIndexChanged(object sender, EventArgs e)
    {
     
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
            string strsel = "select TaxablebenifitMasterTbl.* from TaxablebenifitMasterTbl where Id='" + ViewState["MasterId"] + "'";
            SqlCommand cmdview = new SqlCommand(strsel, con);
            SqlDataAdapter dtpview = new SqlDataAdapter(cmdview);
            DataTable dtview = new DataTable();
            dtpview.Fill(dtview);
            if (dtview.Rows.Count > 0)
            {
                ddlstrname.SelectedIndex = ddlstrname.Items.IndexOf(ddlstrname.Items.FindByValue(dtview.Rows[0]["Whid"].ToString()));
                fillaccount();
                ddlacccredit.SelectedIndex = ddlacccredit.Items.IndexOf(ddlacccredit.Items.FindByValue(dtview.Rows[0]["AccCreditId"].ToString()));

                ddlaccdebit.SelectedIndex = ddlaccdebit.Items.IndexOf(ddlaccdebit.Items.FindByValue(dtview.Rows[0]["AccDebitId"].ToString()));

                txttaxablename.Text = Convert.ToString(dtview.Rows[0]["Taxablebenifitname"]);

                if (Convert.ToString(dtview.Rows[0]["Status"]) != "")
                {
                    ddlstatus.SelectedValue = Convert.ToString(Convert.ToInt32(dtview.Rows[0]["Status"]));
                }


            }
         
        }
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string st2 = "Delete from TaxablebenifitMasterTbl where Id='" + GridView1.DataKeys[e.RowIndex].Value.ToString() + "' ";
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
    protected void lbllnk_Click(object sender, EventArgs e)
    {
       // lbladdventext.Text = "";
        ModalPopupExtender2.Show();
    }
    protected void lnk2_Click(object sender, EventArgs e)
    {
       // lbladdventext.Text = "";
        ModalPopupExtender2.Show();
    }
    protected void ddlfilterstatus_SelectedIndexChanged(object sender, EventArgs e)
    {
        gridfun();
    }
}