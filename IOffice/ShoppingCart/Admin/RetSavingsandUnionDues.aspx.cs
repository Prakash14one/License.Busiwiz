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

public partial class RetSavingsandUnionDues : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(PageConn.connnn);
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
            fillpaymentcycle();
            Fillwarehouse();

            //  fillPayrolltax();
            ddlstrname_SelectedIndexChanged1(sender, e);
            fillfilteremployee();
            ddlpaycycle.Items.Insert(0, "Select");
            ddlpaycycle.Items[0].Value = "0";
            filltaxyearfilter();
            lblCompany.Text = Session["Cname"].ToString();
            gridfun();
        }

    }
    //protected void fillPayrolltax()
    //{
    //    // strrrr = "select CountryMaster.CountryName,CountryMaster.CountryId,Payrolltax_id,tax_name,start_date,end_date,Status as ActiveStaus,MatchingEmployee,StateMasterTbl.StateName,StateMasterTbl.StateId from PayrolltaxMaster inner join CountryMaster on PayrolltaxMaster.Country_id=CountryMaster.CountryId inner join StateMasterTbl on PayrolltaxMaster.State_id=StateMasterTbl.StateId ";

    //    string str = "  Select [PayrollTaxMaster].[PayRollTax_Id],[PayrollTaxMaster].[Tax_Name]+' : '+ CountryMaster.CountryName+' : '+Convert(nvarchar,PayrolltaxMaster.start_date,101) +' : '+Convert(nvarchar,PayrolltaxMaster.end_date,101) as tax_name " +
    //                 " from [PayrollTaxMaster]  inner join CountryMaster on CountryMaster.CountryId=PayrolltaxMaster.Country_id " +
    //                //"  inner join StateMasterTbl on PayrolltaxMaster.State_id=StateMasterTbl.StateId " +
    //                "  where [PayrollTaxMaster].Status='1' order by [PayrollTaxMaster].[Tax_Name]";
    //    SqlCommand cmd = new SqlCommand(str, con);
    //    SqlDataAdapter adp = new SqlDataAdapter(cmd);
    //    DataSet ds = new DataSet();
    //    adp.Fill(ds);

    //    ddlmname.DataSource = ds;
    //    ddlmname.DataTextField = "tax_name";
    //    ddlmname.DataValueField = "PayRollTax_Id";
    //    ddlmname.DataBind();
    //    ddlmname.Items.Insert(0, "Select");

    //    ddlmname.Items[0].Value = "0";

    //    ddlfilterdedctionname.DataSource = ds;
    //    ddlfilterdedctionname.DataTextField = "tax_name";
    //    ddlfilterdedctionname.DataValueField = "PayRollTax_Id";
    //    ddlfilterdedctionname.DataBind();
    //    ddlfilterdedctionname.Items.Insert(0, "All");

    //    ddlfilterdedctionname.Items[0].Value = "0";
    //}
    protected DataTable select(string qu)
    {
        SqlCommand cmd = new SqlCommand(qu, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        return dt;

    }
    protected void filltaxyear()
    {
        string Datat = "select Distinct CompanyWebsiteAddressMaster.Country from CompanyWebsiteAddressMaster inner join WareHouseMaster on WareHouseMaster.WareHouseId=CompanyWebsiteAddressMaster.CompanyWebsiteMasterId inner join AddressTypeMaster on AddressTypeMaster.AddressTypeMasterId=CompanyWebsiteAddressMaster.AddressTypeMasterId  where WareHouseMaster.WareHouseId='" + ddlstrname.SelectedValue + "' and AddressTypeMaster.Name='Business Address' ";
        DataTable drg = select(Datat);
        if (drg.Rows.Count > 0)
        {
            string stremp = "select TaxYear_Id,TaxYear_Name+':'+Convert (Nvarchar(50), StartDate ,101 ) +':'+ Convert (Nvarchar(50), EndDate ,101 ) as TaxYear_Name  from Tax_Year where CountryId='" + drg.Rows[0]["Country"] + "' and Active='1' ";
            SqlCommand cmdemp = new SqlCommand(stremp, con);
            SqlDataAdapter adpemp = new SqlDataAdapter(cmdemp);
            DataTable dsemp = new DataTable();
            adpemp.Fill(dsemp);
            ddlyear.DataSource = dsemp;
            ddlyear.DataTextField = "TaxYear_Name";
            ddlyear.DataValueField = "TaxYear_Id";
            ddlyear.DataBind();

        }
        ddlyear.Items.Insert(0, "Select");
        ddlyear.Items[0].Value = "0";

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
    protected void fillpaymentcycle()
    {
        //string str1 = " select Id,AccountName,AccountId from AccountMaster where  AccountMaster.Status=1 and  ClassId=13 and AccountMaster.compid = '" + Session["Comid"].ToString() + "' and AccountMaster.Whid='" + ddlstrname.SelectedValue + "'  order by AccountName asc";

        string str = "Select * from Payperiodtype where active='true' order by Name";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);


        ddlfilterpaycycle.DataSource = ds;
        ddlfilterpaycycle.DataTextField = "Name";
        ddlfilterpaycycle.DataValueField = "ID";
        ddlfilterpaycycle.DataBind();
        ddlfilterpaycycle.Items.Insert(0, "All");
        ddlfilterpaycycle.Items[0].Value = "0";
    }


    protected void fillemployee()
    {
        string str = "SELECT distinct EmployeeMaster.EmployeeName,EmployeeMaster.Whid,EmployeeMaster.EmployeeMasterID,EmployeeMaster.DesignationMasterId FROM   EmployeeMaster where EmployeeMaster.Whid='" + ddlstrname.SelectedValue + "' Order by EmployeeName";
        SqlCommand cmd1 = new SqlCommand(str, con);
        SqlDataAdapter adap1 = new SqlDataAdapter(cmd1);
        DataSet ds1 = new DataSet();
        adap1.Fill(ds1);
        ddlemp.DataSource = ds1;
        ddlemp.DataValueField = "EmployeeMasterId";
        ddlemp.DataTextField = "EmployeeName";
        ddlemp.DataBind();
        ddlemp.Items.Insert(0, "Select");
        ddlemp.Items[0].Value = "0";

    }
    protected void fillfilteremployee()
    {
        ddlfilteremp.Items.Clear();
        if (ddlfilterbus.SelectedIndex > 0)
        {
            string str = "SELECT distinct EmployeeMaster.EmployeeName,EmployeeMaster.Whid,EmployeeMaster.EmployeeMasterID,EmployeeMaster.DesignationMasterId FROM   EmployeeMaster where EmployeeMaster.Whid='" + ddlfilterbus.SelectedValue + "'";
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
    public void gridfun()
    {
        lblCompany.Text = "Business : " + ddlfilterbus.SelectedItem.Text;
        lblemp.Text = "Employee : " + Convert.ToString(ddlfilteremp.SelectedItem.Text);
        // lblpay.Text = "Tax Name : " + Convert.ToString(ddlfilterdedctionname.SelectedItem.Text);
        lblyea.Text = "Tax Year : " + Convert.ToString(ddlfilteryear.SelectedItem.Text);
        lblcy.Text = "Pay Cycle : " + Convert.ToString(ddlfilterpaycycle.SelectedItem.Text);

        string dty = "";
        if (ddlfilterbus.SelectedIndex > 0)
        {
            dty += " and WithholdingRequestMasterTbl.BusinessID='" + ddlfilterbus.SelectedValue + "'";
        }
        if (ddlfilteremp.SelectedIndex > 0)
        {
            dty += " and WithholdingRequestMasterTbl.EmployeeID='" + ddlfilteremp.SelectedValue + "'";
        }
        if (ddlfilteryear.SelectedIndex > 0)
        {
            dty += " and WithholdingRequestMasterTbl.YearID='" + ddlfilteryear.SelectedValue + "'";
        }
      
        if (ddlfilterpaycycle.SelectedIndex > 0)
        {
            dty += " and WithholdingRequestMasterTbl.PaycycleID='" + ddlfilterpaycycle.SelectedValue + "'";
        }
        string em = "SELECT distinct Convert(nvarchar,RequestDate,101) as Date,Case When(RRSPRecurring ='1')then'Yes' else 'No' End as RRSPRecurring,Case When(UnionDuesRecurring ='1')then'Yes' else 'No' End as UnionDuesRecurring, WithholdingRequestMasterTbl.Id, Payperiodtype.Name as Paycycle,TaxYear_Name,WarehouseMaster.Name as Wname,EmployeeName,RRSPCotributionREcurringAMT,UnionDuesRecurringAMT FROM Payperiodtype inner join  WithholdingRequestMasterTbl on WithholdingRequestMasterTbl.PaycycleID = Payperiodtype.Id  inner join WarehouseMaster on WarehouseMaster.WarehouseId=WithholdingRequestMasterTbl.BusinessID inner join EmployeeMaster on EmployeeMaster.EmployeeMasterID=WithholdingRequestMasterTbl.EmployeeID  inner join Tax_Year on Tax_Year.TaxYear_Id = WithholdingRequestMasterTbl.YearID  where WareHouseMaster.comid='" + Session["Comid"] + "'" + dty + " Order by WithholdingRequestMasterTbl.Id Desc";
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
        foreach (GridViewRow dts in GridView1.Rows)
        {
            Label lblrrsp = (Label)dts.FindControl("lblrrsp");
            Label lblrrsprec = (Label)dts.FindControl("lblrrsprec");
            Label lblRRSPpay = (Label)dts.FindControl("lblRRSPpay");

            Label lbluni = (Label)dts.FindControl("lbluni");
            Label lblunionrec = (Label)dts.FindControl("lblunionrec");
            Label lblunipay = (Label)dts.FindControl("lblunipay");
            if (lblrrsp.Text.Length <= 0)
            {
                lblrrsprec.Text = "";
                lblRRSPpay.Text = "";
            }
            if (lbluni.Text.Length <= 0)
            {
                lblunionrec.Text = "";
                lblunipay.Text = "";
            }
        }
    }



    protected void ddlstrname_SelectedIndexChanged1(object sender, EventArgs e)
    {
        filltaxyear();
        fillemployee();
        ddlemp_SelectedIndexChanged(sender, e);
    }



    protected void btnsubmit_Click(object sender, EventArgs e)
    {

        lblmsg.Text = "";
        string inc = "";
        string str2 = "";

        str2 = "Select WithholdingRequestMasterTbl.id  from  WithholdingRequestMasterTbl   where  EmployeeID='" + ddlemp.SelectedValue + "' and YearID='" + ddlyear.SelectedValue + "'  and PaycycleID='" + ddlpaycycle.SelectedValue + "'";

        SqlCommand cmd2 = new SqlCommand(str2, con);

        SqlDataAdapter adap2 = new SqlDataAdapter(cmd2);
        DataTable ds2 = new DataTable();
        adap2.Fill(ds2);
        int count2;
        count2 = ds2.Rows.Count;
        if (count2 > 0)
        {
            lblmsg.Visible = true;
            lblmsg.Text = "Sorry, Record already exist";

        }
        else
        {
            inc = "nj";
            string str = "insert into WithholdingRequestMasterTbl(BusinessID,EmployeeID,YearID,RRSPCotributionREcurringAMT,UnionDuesRecurringAMT,RRSPRecurring,UnionDuesRecurring,RequestDate,PaycycleID) values ('" + ddlstrname.SelectedValue + "','" + ddlemp.SelectedValue + "','" + ddlyear.SelectedValue + "','" + txtrssc.Text + "','" + txtUnionDues.Text + "','" + chkrss.Checked + "','" + chkUnionDues.Checked + "','" + txtreqdate.Text + "','" + ddlpaycycle.SelectedValue + "') ";
            SqlCommand cmd1 = new SqlCommand(str, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmd1.ExecuteNonQuery();
            con.Close();

        }


        lblmsg.Visible = true;
        if (inc == "nj")
        {
            gridfun();
            clear();
            lblmsg.Text = "Record inserted successfully";
           
           
        }
        else if (lblmsg.Text.Length != 0)
        {
            lblmsg.Text = "Record not inserted successfully";
        }
       



    }
    protected void clear()
    {
        ddlemp.SelectedIndex = 0;
        txtrssc.Text = "";
        txtUnionDues.Text = "";

        chkUnionDues.Checked = false;
        chkrss.Checked = false;

        txtrssc.Enabled = true;
        txtUnionDues.Enabled = true;

        chkUnionDues.Enabled = true;
        chkrss.Enabled = true;

    }
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }
    protected void btnupdate_Click(object sender, EventArgs e)
    {
        string inc = "";
        string str2 = "";
        str2 = "Select *  from  WithholdingRequestMasterTbl  where  Id='" + ViewState["MasterId"] + "'";


        SqlDataAdapter adch = new SqlDataAdapter(str2, con);
        DataTable dsc = new DataTable();
        adch.Fill(dsc);
        if (dsc.Rows.Count > 0)
        {
           
            str2 = "Select WithholdingRequestMasterTbl.id  from  WithholdingRequestMasterTbl where EmployeeID='" + ddlemp.SelectedValue + "' and YearID='" + ddlyear.SelectedValue + "'  and PaycycleID='" + ddlpaycycle.SelectedValue + "' and WithholdingRequestMasterTbl.Id<>'" + ViewState["MasterId"] + "'";
            
            SqlCommand cmd2 = new SqlCommand(str2, con);

            SqlDataAdapter adap2 = new SqlDataAdapter(cmd2);
            DataTable ds2 = new DataTable();
            adap2.Fill(ds2);
            int count2;
            count2 = ds2.Rows.Count;
            if (count2 > 1)
            {
                lblmsg.Visible = true;
                lblmsg.Text = "Record already exist";
            }
            else
            {
                        inc = "up";

                        string strdet = "Update  WithholdingRequestMasterTbl set BusinessID='" + ddlstrname.SelectedValue + "',EmployeeID='" + ddlemp.SelectedValue + "',YearID='" + ddlyear.SelectedValue + "',RRSPCotributionREcurringAMT='" + txtrssc.Text + "',UnionDuesRecurringAMT='" + txtUnionDues.Text + "',RRSPRecurring='" + chkrss.Checked + "',UnionDuesRecurring='" + txtUnionDues.Text + "',RequestDate='" + txtreqdate.Text + "',PaycycleID='"+ddlpaycycle.SelectedValue+"' where Id='" + ViewState["MasterId"] + "'";
                        SqlCommand cmd1 = new SqlCommand(strdet, con);
                        if (con.State.ToString() != "Open")
                        {
                            con.Open();
                        }
                        cmd1.ExecuteNonQuery();
                        con.Close();

            }

            lblmsg.Visible = true;
            if (inc == "up")
            {
                gridfun();
                clear();
                lblmsg.Text = "Record updated successfully";
                btnsubmit.Visible = true;
                btnupdate.Visible = false;
                

            }
            else
            {
                lblmsg.Text = "Record not updated successfully";
            }

        }



    }

    protected void btncancel_Click(object sender, EventArgs e)
    {

        ddlemp.SelectedIndex = 0;
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
            if (GridView1.Columns[9].Visible == true)
            {
                ViewState["edith"] = "tt";
                GridView1.Columns[9].Visible = false;
            }
            if (GridView1.Columns[10].Visible == true)
            {
                ViewState["deleteh"] = "tt";
                GridView1.Columns[10].Visible = false;
            }

        }
        else
        {



            Button1.Text = "Printable Version";
            Button7.Visible = false;
            if (ViewState["edith"] != null)
            {
                GridView1.Columns[9].Visible = true;
            }
            if (ViewState["deleteh"] != null)
            {
                GridView1.Columns[10].Visible = true;
            }

        }
    }

    protected void ddlfilterbus_SelectedIndexChanged(object sender, EventArgs e)
    {
        filltaxyearfilter();
        fillfilteremployee();
        gridfun();
    }
    protected void filltaxyearfilter()
    {
        string Datat = "select Distinct CompanyWebsiteAddressMaster.Country from CompanyWebsiteAddressMaster inner join WareHouseMaster on WareHouseMaster.WareHouseId=CompanyWebsiteAddressMaster.CompanyWebsiteMasterId inner join AddressTypeMaster on AddressTypeMaster.AddressTypeMasterId=CompanyWebsiteAddressMaster.AddressTypeMasterId  where WareHouseMaster.WareHouseId='" + ddlfilterbus.SelectedValue + "' and AddressTypeMaster.Name='Business Address' ";
        DataTable drg = select(Datat);
        if (drg.Rows.Count > 0)
        {
            string stremp = "select TaxYear_Id,TaxYear_Name+':'+Convert (Nvarchar(50), StartDate ,101 ) +':'+ Convert (Nvarchar(50), EndDate ,101 ) as TaxYear_Name  from Tax_Year where CountryId='" + drg.Rows[0]["Country"] + "' and Active='1' ";
            SqlCommand cmdemp = new SqlCommand(stremp, con);
            SqlDataAdapter adpemp = new SqlDataAdapter(cmdemp);
            DataTable dsemp = new DataTable();
            adpemp.Fill(dsemp);
            ddlfilteryear.DataSource = dsemp;
            ddlfilteryear.DataTextField = "TaxYear_Name";
            ddlfilteryear.DataValueField = "TaxYear_Id";
            ddlfilteryear.DataBind();

        }

        ddlfilteryear.Items.Insert(0, "All");

        ddlfilteryear.Items[0].Value = "0";
    }
    protected void ddlfilteremp_SelectedIndexChanged(object sender, EventArgs e)
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
    protected void ddlfilterdedctionname_SelectedIndexChanged(object sender, EventArgs e)
    {
        gridfun();
    }
    protected void ddlfilteryear_SelectedIndexChanged(object sender, EventArgs e)
    {
        gridfun();
    }
    protected void ddlfilterpaycycle_SelectedIndexChanged(object sender, EventArgs e)
    {
        gridfun();
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
            string strsel = "select WithholdingRequestMasterTbl.*,PaycycleID from WithholdingRequestMasterTbl where WithholdingRequestMasterTbl.Id='" + ViewState["MasterId"] + "'";
            SqlCommand cmdview = new SqlCommand(strsel, con);
            SqlDataAdapter dtpview = new SqlDataAdapter(cmdview);
            DataTable dtview = new DataTable();
            dtpview.Fill(dtview);
            if (dtview.Rows.Count > 0)
            {
                ddlstrname.SelectedIndex = ddlstrname.Items.IndexOf(ddlstrname.Items.FindByValue(dtview.Rows[0]["BusinessID"].ToString()));
                fillemployee();
                filltaxyear();

                ddlemp.SelectedIndex = ddlemp.Items.IndexOf(ddlemp.Items.FindByValue(dtview.Rows[0]["EmployeeID"].ToString()));
                ddlemp_SelectedIndexChanged(sender, e);
                ddlyear.SelectedIndex = ddlyear.Items.IndexOf(ddlyear.Items.FindByValue(dtview.Rows[0]["YearID"].ToString()));

                // ddlmname.SelectedIndex = ddlmname.Items.IndexOf(ddlmname.Items.FindByValue(dtview.Rows[0]["DeductionMasterID"].ToString()));
                if (Convert.ToString(dtview.Rows[0]["PaycycleID"]) != "")
                {
                    ddlpaycycle.SelectedIndex = ddlpaycycle.Items.IndexOf(ddlpaycycle.Items.FindByValue(dtview.Rows[0]["PaycycleID"].ToString()));

                }
                else
                {
                    ddlpaycycle.SelectedIndex = 0;
                }
                txtreqdate.Text = Convert.ToDateTime(dtview.Rows[0]["RequestDate"]).ToShortDateString();
                //if (Convert.ToString(dtview.Rows[0]["RRSPCotributionREcurringAMT"]) != "")
                //{
                    chkrss.Checked = Convert.ToBoolean(dtview.Rows[0]["RRSPRecurring"]);
                    //chkUnionDues.Checked = false;
                    //txtUnionDues.Text = "";
                    //chkUnionDues.Enabled = false;
                    //txtUnionDues.Enabled = false;
                    //chkrss.Enabled = true;
                    //txtrssc.Enabled = true;
                    txtrssc.Text = Convert.ToString(dtview.Rows[0]["RRSPCotributionREcurringAMT"]);
                //}
                //else if (Convert.ToString(dtview.Rows[0]["UnionDuesRecurringAMT"]) != "")
                //{
                    chkUnionDues.Checked = Convert.ToBoolean(dtview.Rows[0]["UnionDuesRecurring"]);
                    //chkrss.Checked = false;
                    //txtrssc.Text = "";
                    //txtrssc.Enabled = false;
                    //chkrss.Enabled = false;
                    //chkUnionDues.Enabled = true;
                    //txtUnionDues.Enabled = true;
                    txtUnionDues.Text = Convert.ToString(dtview.Rows[0]["UnionDuesRecurringAMT"]);
               // }



            }
            // txtrrsp.Text = dtview.Rows[0]["1BasicpersonalAmount"].ToString();
            //  txtF1.Text = dtview.Rows[0]["2ChildAmount"].ToString();
        }
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string st2 = "Delete from WithholdingRequestMasterTbl where Id='" + GridView1.DataKeys[e.RowIndex].Value.ToString() + "' ";
        SqlCommand cmd2 = new SqlCommand(st2, con);
        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        int obs = cmd2.ExecuteNonQuery();
        con.Close();
        if (obs > 0)
        {
            //string st22 = "Delete from WithholdingRequestDetailTbl where WithholdingrequestmasterTblID='" + GridView1.DataKeys[e.RowIndex].Value.ToString() + "' ";
            //SqlCommand cmd22 = new SqlCommand(st22, con);

            //if (con.State.ToString() != "Open")
            //{
            //    con.Open();
            //}
            //cmd22.ExecuteNonQuery();
            //con.Close();
            GridView1.EditIndex = -1;
            gridfun();
            lblmsg.Visible = true;
            lblmsg.Text = "Record succesfully deleted";
        }

    }
    protected void txtrssc_TextChanged(object sender, EventArgs e)
    {

    }
    protected void ddlemp_SelectedIndexChanged(object sender, EventArgs e)
    {
        ddlpaycycle.Items.Clear();
        string str = "Select Distinct Payperiodtype.* from Payperiodtype inner join EmployeePayrollMaster on EmployeePayrollMaster.PayPeriodMasterId=Payperiodtype.ID where Payperiodtype.active='true' and EmployeePayrollMaster.EmpId='" + ddlemp.SelectedValue + "' order by Name";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);

        ddlpaycycle.DataSource = ds;
        ddlpaycycle.DataTextField = "Name";
        ddlpaycycle.DataValueField = "ID";
        ddlpaycycle.DataBind();

    }
}