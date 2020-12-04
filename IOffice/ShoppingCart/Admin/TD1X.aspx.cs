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

public partial class ShoppingCart_Admin_TD1X : System.Web.UI.Page
{
   // SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
    SqlConnection con;
    protected void Page_Load(object sender, EventArgs e)
    {
        PageConn pgcon = new PageConn();
        con = pgcon.dynconn;
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
            txtdate.Text = DateTime.Now.ToShortDateString();
            Fillwarehouse();
            fillPayrolltax();
            filltaxyear();
            fillempddl();
          
            fillfilteremployee();
           // fillgrid();
          //  filterstore();
            
           // Fillddlbatchname();
          //  Filteremployee();
            fillgrid();
           
        }
    }
    protected void Fillwarehouse()
    {


        DataTable ds = ClsStore.SelectStorename();
        if (ds.Rows.Count > 0)
        {
            ddlstore.DataSource = ds;
            ddlstore.DataValueField = "WareHouseId";
            ddlstore.DataTextField = "Name";
            ddlstore.DataBind();
            ddlfilterbus.DataSource = ds;
            ddlfilterbus.DataValueField = "WareHouseId";
            ddlfilterbus.DataTextField = "Name";
            ddlfilterbus.DataBind();
            ddlfilterbus.Items.Insert(0, "All");
            ddlfilterbus.Items[0].Value = "0";
            DataTable dteeed = ClsStore.SelectEmployeewithIdwise();

            if (dteeed.Rows.Count > 0)
            {

                ddlstore.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);


            }
        }

    }
    protected void fillfilteremployee()
    {
        ddlfilteremp.Items.Clear();
        if (ddlfilterbus.SelectedIndex > 0)
        {
            string str = "select EmployeeMaster.EmployeeMasterID,EmployeeMaster.EmployeeName+':'+DesignationMaster.DesignationName+':'+cast(EmployeeMaster.EmployeeMasterID as Nvarchar(50)) as EmployeeName   from EmployeeMaster inner join DesignationMaster on EmployeeMaster.DesignationMasterId=DesignationMaster.DesignationMasterId where EmployeeMaster.Whid='" + ddlfilterbus.SelectedValue + "'";
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
    protected void fillempddl()
    {
        ddlemployee.Items.Clear();
        string stremp = "select EmployeeMaster.EmployeeMasterID,EmployeeMaster.EmployeeName+':'+DesignationMaster.DesignationName+':'+cast(EmployeeMaster.EmployeeMasterID as Nvarchar(50)) as EmployeeName   from EmployeeMaster inner join DesignationMaster on EmployeeMaster.DesignationMasterId=DesignationMaster.DesignationMasterId where EmployeeMaster.Whid='" + ddlstore.SelectedValue + "' ";
        SqlCommand cmdemp = new SqlCommand(stremp, con);
        SqlDataAdapter adpemp = new SqlDataAdapter(cmdemp);
        DataTable dsemp = new DataTable();
        adpemp.Fill(dsemp);
        if (dsemp.Rows.Count > 0)
        {
            ddlemployee.DataSource = dsemp;
            ddlemployee.DataTextField = "EmployeeName";
            ddlemployee.DataValueField = "EmployeeMasterID";
            ddlemployee.DataBind();
        }
        ddlemployee.Items.Insert(0, "-Select-");

        ddlemployee.Items[0].Value = "0";

    }
    protected void filltaxyear()
    {
        ddlemployee.Items.Clear();
        string stremp = "select TaxYear_Id,TaxYear_Name+':'+Convert (Nvarchar(50), StartDate ,101 ) +':'+ Convert (Nvarchar(50), EndDate ,101 ) as TaxYear_Name  from Tax_Year where Active='1' ";
        SqlCommand cmdemp = new SqlCommand(stremp, con);
        SqlDataAdapter adpemp = new SqlDataAdapter(cmdemp);
        DataTable dsemp = new DataTable();
        adpemp.Fill(dsemp);
        ddltaxyear.DataSource = dsemp;
        ddltaxyear.DataTextField = "TaxYear_Name";
        ddltaxyear.DataValueField = "TaxYear_Id";
        ddltaxyear.DataBind();
        ddltaxyear.Items.Insert(0, "-Select-");

        ddltaxyear.Items[0].Value = "0";


        ddlfilteryear.DataSource = dsemp;
        ddlfilteryear.DataTextField = "TaxYear_Name";
        ddlfilteryear.DataValueField = "TaxYear_Id";
        ddlfilteryear.DataBind();
        ddlfilteryear.Items.Insert(0, "All");

        ddlfilteryear.Items[0].Value = "0";
    }
    protected void fillPayrolltax()
    {
        ddlemployee.Items.Clear();
        string str = "  Select [PayrollTaxMaster].[PayRollTax_Id],[PayrollTaxMaster].[Tax_Name]+' : '+ CountryMaster.CountryName+' : '+StateMasterTbl.StateName +' : '+Convert(nvarchar,PayrolltaxMaster.start_date,101) +' : '+Convert(nvarchar,PayrolltaxMaster.end_date,101) as tax_name " +
                     " from [PayrollTaxMaster]  inner join CountryMaster on CountryMaster.CountryId=PayrolltaxMaster.Country_id " +
                    "  inner join StateMasterTbl on PayrolltaxMaster.State_id=StateMasterTbl.StateId " +
                    "  where [PayrollTaxMaster].Status='1' order by [PayrollTaxMaster].[Tax_Name]";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);

        ddpayrolldeduction.DataSource = ds;
        ddpayrolldeduction.DataTextField = "tax_name";
        ddpayrolldeduction.DataValueField = "PayRollTax_Id";
        ddpayrolldeduction.DataBind();
        ddpayrolldeduction.Items.Insert(0, "-Select-");

        ddpayrolldeduction.Items[0].Value = "0";
        ddlfilterdedctionname.DataSource = ds;
        ddlfilterdedctionname.DataTextField = "tax_name";
        ddlfilterdedctionname.DataValueField = "PayRollTax_Id";
        ddlfilterdedctionname.DataBind();
        ddlfilterdedctionname.Items.Insert(0, "All");

        ddlfilterdedctionname.Items[0].Value = "0";

    }
    protected void ddlstore_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillempddl();
     
    }

    protected void fillempdetail()
    {
        string stremp = "select * from EmployeePayrollMaster where EmpId='" + ddlemployee.SelectedValue + "' ";
        SqlCommand cmdemp = new SqlCommand(stremp, con);
        SqlDataAdapter adpemp = new SqlDataAdapter(cmdemp);
        DataTable dsemp = new DataTable();
        adpemp.Fill(dsemp);
        if (dsemp.Rows.Count > 0)
        {
            lbllastname.Text = Convert.ToString(dsemp.Rows[0]["LastName"]);
            lblfirstname.Text = Convert.ToString(dsemp.Rows[0]["FirstName"]);
            lblemid.Text = Convert.ToString(ddlemployee.SelectedValue);
            lblsocialno.Text = Convert.ToString(dsemp.Rows[0]["SocialSecurityNo"]);

        }
        else
        {
            lbllastname.Text = "";
            lblfirstname.Text = "";
            lblsocialno.Text = "";
        }
        string str = " select EmployeeMaster.*,User_master.zipcode,CityMasterTbl.CityName from EmployeeMaster  inner join User_master on User_master.PartyID=EmployeeMaster.PartyID Left Join CityMasterTbl on CityMasterTbl.CityId=EmployeeMaster.City where EmployeeMaster.EmployeeMasterID='" + ddlemployee.SelectedValue + "'";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable ds = new DataTable();
        adp.Fill(ds);
        if (ds.Rows.Count > 0)
        {
            lbladdress.Text = Convert.ToString(ds.Rows[0]["Address"]);

            if (lblfirstname.Text.Length <= 0)
            {
                lblfirstname.Text = Convert.ToString(ds.Rows[0]["EmployeeName"]);
            }

        }
        else
        {
            lbladdress.Text = "";


        }
        

    }
    protected void Button3_Click(object sender, EventArgs e)
    {

        string str = " select * from TD1XTBL where EmployeeId='" + ddlemployee.SelectedValue + "' and TaxYearId='" + ddltaxyear.SelectedValue + "' and [PayrollTaxMasterId] = '" + ddpayrolldeduction.SelectedValue + "' and Whid='" + ddlstore.SelectedValue + "'  ";
        
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable ds = new DataTable();
        adp.Fill(ds);
        if (ds.Rows.Count > 0)
        {
            lblmsg.Visible = true;
            lblmsg.Text = "Record Already Exists..";
        }
        else
        {


            Decimal totalamt = 0;

            if (txtcommision.Text != "")
            {
                totalamt += Convert.ToDecimal(txtcommision.Text);
            }
            if (txtsalarywages.Text != "")
            {
                totalamt += Convert.ToDecimal(txtsalarywages.Text);
            }
            txttotalrem.Text = totalamt.ToString();
            txtgrandtotal.Text = totalamt.ToString();
            if (txtsublastcomm.Text != "")
            {
                totalamt -= Convert.ToDecimal(txtsublastcomm.Text);
            }
            
            txtgrantcomencome.Text = Convert.ToString(totalamt);
          
            if (totalamt <= 0)
            {
                ModapupExtende22.Show();
            }
            else
            {

                String avginsert = "Insert Into TD1XTBL (EmployeeId,TaxYearId,PayrollTaxMasterId,Date,Whid,Commision,[SalaryofVages],[TotalRem],[SubLastCommision],[EstannNetCommIncome],[Signature]) " +
                                   " values ('" + ddlemployee.SelectedValue + "','" + ddltaxyear.SelectedValue + "','" + ddpayrolldeduction.SelectedValue + "','" +txtdate.Text + "','" + ddlstore.SelectedValue + "','" + txtcommision.Text + "','" + txtsalarywages.Text + "','" + txttotalrem.Text + "','" + txtsublastcomm.Text + "','" + txtgrantcomencome.Text + "','" + txtsign.Text + "')";
                SqlCommand cmdavg = new SqlCommand(avginsert, con);
                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmdavg.ExecuteNonQuery();
                con.Close();
             

                clearall();
              
                lblmsg.Visible = true;
                lblmsg.Text = "Record Inserted Successfully";
                fillgrid();
            }
        }

    }

    protected void fillgrid()
    {
        GridView1.DataSource = null;
        GridView1.DataBind();
        lblCompany.Text = Session["Cname"].ToString() + " - " + ddlfilterbus.SelectedItem.Text;
        lblemp.Text = "Employee : " + Convert.ToString(ddlfilteremp.SelectedItem.Text);
        lblpay.Text = "Tax Name : " + Convert.ToString(ddlfilterdedctionname.SelectedItem.Text);
        lblyea.Text = "Tax Year : " + Convert.ToString(ddlfilteryear.SelectedItem.Text);
        
        string dty = "";
        if (ddlfilterbus.SelectedIndex > 0)
        {
            dty += " and TD1XTBL.Whid='" + ddlfilterbus.SelectedValue + "'";
        }
        if (ddlfilteremp.SelectedIndex > 0)
        {
            dty += " and TD1XTBL.EmployeeId='" + ddlfilteremp.SelectedValue + "'";
        }
        if (ddlfilteryear.SelectedIndex > 0)
        {
            dty += " and TD1XTBL.TaxYearId='" + ddlfilteryear.SelectedValue + "'";
        }
        if (ddlfilterdedctionname.SelectedIndex > 0)
        {
            dty += " and TD1XTBL.PayrollTaxMasterId='" + ddlfilterdedctionname.SelectedValue + "'";
        }

        string str = "select Distinct TD1XTBL.Id, TD1XTBL.EstannNetCommIncome,TD1XTBL.SubLastCommision,TD1XTBL.Commision, TD1XTBL.SalaryofVages,Convert(nvarchar, TD1XTBL.Date,101) as Date,EmployeeMaster.EmployeeName,WareHouseMaster.Name as WName,TaxYear_Name from TD1XTBL inner join   EmployeeMaster on EmployeeMaster.EmployeeMasterID=TD1XTBL.EmployeeId inner join WareHouseMaster on TD1XTBL.Whid=WareHouseMaster.WareHouseId inner join Tax_Year on Tax_Year.TaxYear_Id = TD1XTBL.TaxYearId inner join PayrollTaxMaster on [PayrollTaxMaster].[PayRollTax_Id]=TD1XTBL.PayrollTaxMasterId where WareHouseMaster.comid='" + Session["Comid"].ToString() + "'" + dty;
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable ds = new DataTable();
        adp.Fill(ds);
        GridView1.DataSource = ds;
        if (ds.Rows.Count > 0)
        {
            DataView myDataView = new DataView();
            myDataView = ds.DefaultView;
            if (hdnsortExp.Value != string.Empty)
            {
                myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
            }

        }
        GridView1.DataBind();
 
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
       
        if (e.CommandName == "Select")
        {
            GridView1.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            ViewState["MasterId"] = GridView1.SelectedDataKey.Value;

            Button3.Visible = false;
            Button4.Visible = true;
            string strsel = "select * from TD1XTBL where Id='" + ViewState["MasterId"] + "'";
            SqlCommand cmdview = new SqlCommand(strsel, con);
            SqlDataAdapter dtpview = new SqlDataAdapter(cmdview);
            DataTable dtview = new DataTable();
            dtpview.Fill(dtview);
            if (dtview.Rows.Count > 0)
            {
                ddlstore.SelectedIndex = ddlstore.Items.IndexOf(ddlstore.Items.FindByValue(dtview.Rows[0]["Whid"].ToString()));
                fillempddl();
                ddlemployee.SelectedIndex = ddlemployee.Items.IndexOf(ddlemployee.Items.FindByValue(dtview.Rows[0]["EmployeeId"].ToString()));

                ddltaxyear.SelectedIndex = ddltaxyear.Items.IndexOf(ddltaxyear.Items.FindByValue(dtview.Rows[0]["TaxYearId"].ToString()));

                ddpayrolldeduction.SelectedIndex = ddpayrolldeduction.Items.IndexOf(ddpayrolldeduction.Items.FindByValue(dtview.Rows[0]["PayrollTaxMasterId"].ToString()));

                txtcommision.Text = Convert.ToString(dtview.Rows[0]["Commision"]);
                txtsalarywages.Text = Convert.ToString(dtview.Rows[0]["SalaryofVages"]);
                txttotalrem.Text = Convert.ToString(dtview.Rows[0]["TotalRem"]);
                txtgrandtotal.Text = Convert.ToString(dtview.Rows[0]["TotalRem"]);
                txtsublastcomm.Text = Convert.ToString(dtview.Rows[0]["SubLastCommision"]);
                txtgrantcomencome.Text = Convert.ToString(dtview.Rows[0]["EstannNetCommIncome"]);
               
                txtsign.Text = Convert.ToString(dtview.Rows[0]["Signature"]);
                txtdate.Text = Convert.ToString(dtview.Rows[0]["Date"]);
               
            }
        }
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    { string stra = "select PartytTypeMaster.PartType from EmployeeMaster inner join Party_Master on Party_Master.PartyId=EmployeeMaster.PartyId inner join PartytTypeMaster on PartytTypeMaster.PartyTypeId=Party_master.PartyTypeId where EmployeeMaster.EmployeeMasterId='" + Session["EmployeeId"] + "' ";
        SqlDataAdapter adp = new SqlDataAdapter(stra, con);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            if (Convert.ToString(dt.Rows[0]["PartType"]) == "Admin")
            {
                string st2 = "Delete from TD1XTBL where Id='" + GridView1.DataKeys[e.RowIndex].Value.ToString() + "' ";
                SqlCommand cmd2 = new SqlCommand(st2, con);

                if (con.State.ToString() != "Open")
                {
                    con.Open();
                }
                cmd2.ExecuteNonQuery();
                con.Close();
                GridView1.EditIndex = -1;
                fillgrid();
                lblmsg.Visible = true;
                lblmsg.Text = "Record deleted succesfully ";
            }
            else
            {
                GridView1.EditIndex = -1;
                fillgrid();
                lblmsg.Visible = true;
                lblmsg.Text = "Sorry,You are not permitted to delete this record";
            }
        }
        else
        {
            GridView1.EditIndex = -1;
            fillgrid();
            lblmsg.Visible = true;
            lblmsg.Text = "Sorry,You are not permitted to delete this record";
        }
    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        string stra = "select PartytTypeMaster.PartType from EmployeeMaster inner join Party_Master on Party_Master.PartyId=EmployeeMaster.PartyId inner join PartytTypeMaster on PartytTypeMaster.PartyTypeId=Party_master.PartyTypeId where EmployeeMaster.EmployeeMasterId='" + Session["EmployeeId"] + "' ";
        SqlDataAdapter adpa = new SqlDataAdapter(stra, con);
        DataTable dta = new DataTable();
        adpa.Fill(dta);
        if (dta.Rows.Count > 0)
        {
            if (Convert.ToString(dta.Rows[0]["PartType"]) == "Admin")
            {


                string str = " select * from TD1XTBL where EmployeeId='" + ddlemployee.SelectedValue + "' and TaxYearId='" + ddltaxyear.SelectedValue + "' and [PayrollTaxMasterId] = '" + ddpayrolldeduction.SelectedValue + "' and Whid='" + ddlstore.SelectedValue + "' and Id<>'" + ViewState["MasterId"] + "' ";

                SqlCommand cmd = new SqlCommand(str, con);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                DataTable ds = new DataTable();
                adp.Fill(ds);
                if (ds.Rows.Count > 0)
                {
                    lblmsg.Visible = true;
                    lblmsg.Text = "Record Already Exists..";
                }
                else
                {
                    Button3.Visible = true;
                    Button4.Visible = false;



                    Decimal totalamt = 0;

                    if (txtcommision.Text != "")
                    {
                        totalamt += Convert.ToDecimal(txtcommision.Text);
                    }
                    if (txtsalarywages.Text != "")
                    {
                        totalamt += Convert.ToDecimal(txtsalarywages.Text);
                    }
                    txttotalrem.Text = totalamt.ToString();
                    txtgrandtotal.Text = totalamt.ToString();
                    if (txtsublastcomm.Text != "")
                    {
                        totalamt -= Convert.ToDecimal(txtsublastcomm.Text);
                    }

                    txtgrantcomencome.Text = Convert.ToString(totalamt);
                    if (totalamt <= 0)
                    {
                        ModapupExtende22.Show();
                    }
                    else
                    {

                        String avginsert = "Update TD1XTBL set EmployeeId='" + ddlemployee.SelectedValue + "',TaxYearId='" + ddltaxyear.SelectedValue + "',PayrollTaxMasterId='" + ddpayrolldeduction.SelectedValue + "',Date='" + txtdate.Text + "',Whid='" + ddlstore.SelectedValue + "',Commision='" + txtcommision.Text + "',[SalaryofVages]='" + txtsalarywages.Text + "',[TotalRem]='" + txttotalrem.Text + "',[SubLastCommision]='" + txtsublastcomm.Text + "',[EstannNetCommIncome]='" + txtgrantcomencome.Text + "',[Signature]='" + txtsign.Text + "' where Id='" + ViewState["MasterId"] + "' ";

                        SqlCommand cmdavg = new SqlCommand(avginsert, con);
                        if (con.State.ToString() != "Open")
                        {
                            con.Open();
                        }
                        cmdavg.ExecuteNonQuery();
                        con.Close();

                        clearall();

                        lblmsg.Visible = true;
                        lblmsg.Text = "Record Updated Successfully";
                        Button3.Visible = true;
                        Button4.Visible = false;
                        fillgrid();
                    }


                }
            }
            else
            {
                clearall();
                Button3.Visible = true;
                Button4.Visible = false;
                lblmsg.Visible = true;
                lblmsg.Text = "Sorry,You are not permitted to change this record";
            }
        }
        else
        {
            clearall();
            Button3.Visible = true;
            Button4.Visible = false;
            lblmsg.Visible = true;
            lblmsg.Text = "Sorry,You are not permitted to change this record";
        }
    }
  

   
  
    protected void clearall()
    {
      
        ddlemployee.SelectedIndex = 0;
        ddltaxyear.SelectedIndex = 0;
        ddpayrolldeduction.SelectedIndex = 0;
        txtcommision.Text = "";
        txtsalarywages.Text = "";
        txtsign.Text = "";
        txtgrandtotal.Text = "";
        txtgrantcomencome.Text = "";
        txttotalrem.Text = "";
        txtsublastcomm.Text = "";
       
        txtdate.Text = DateTime.Now.ToShortDateString();
    }
    protected void Button5_Click(object sender, EventArgs e)
    {
        Button3.Visible = true;
        Button4.Visible = false;
        clearall();
        lblmsg.Visible = false;
    }

    protected void ddlfilterbus_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillfilteremployee();
        fillgrid();
    }
    protected void ddlfilteremp_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
    }
    protected void ddlfilteryear_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();
    }
    protected void ddlfilterdedctionname_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillgrid();

    }
  
   
    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder;
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
    protected void Button1_Click(object sender, EventArgs e)
    {
        if (Button1.Text == "Printable Version")
        {
           // fillempdetail();
            pnlsh.Visible = true;
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


            pnlsh.Visible = false;
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
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }


    protected void ddlemployee_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillempdetail();
    }
}
