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

public partial class ShoppingCart_Admin_CanadaFaderalTD : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            ViewState["sortOrder"] = "";
            txtdate.Text = DateTime.Now.ToShortDateString();
            Fillwarehouse();
            fillPayrolltax();
            filltaxyear();
            fillempddl();
            fillemployer();
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
        fillemployer();
    }
    protected void fillemployer()
    {
        string str = " Select CompanyWebsitMaster.Sitename,CompanyWebsiteAddressMaster.ContactPersonName,CompanyWebsiteAddressMaster.Phone1 from WarehouseMaster inner join CompanyWebsitMaster on CompanyWebsitMaster.WHID=WarehouseMaster.WarehouseId Right join CompanyWebsiteAddressMaster on CompanyWebsiteAddressMaster.CompanyWebsiteMasterId=CompanyWebsitMaster.companyWebsiteMasterId where WarehouseMaster.WarehouseId='" + ddlstore.SelectedValue + "'";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable ds = new DataTable();
        adp.Fill(ds);
        if (ds.Rows.Count > 0)
        {
            lblemployername.Text = Convert.ToString(ds.Rows[0]["Sitename"]);
            lblemployerCperson.Text = Convert.ToString(ds.Rows[0]["ContactPersonName"]);
            lblemployerPno.Text = Convert.ToString(ds.Rows[0]["Phone1"]);
          
        }
        else
        {
            lblemployername.Text = "";
            lblemployerCperson.Text = "";
            lblemployerPno.Text = "";
          

        }
        
    }
    protected void ddlemployee_SelectedIndexChanged(object sender, EventArgs e)
    {
        string stremp = "select * from EmployeePayrollMaster where EmpId='" + ddlemployee.SelectedValue + "' ";
        SqlCommand cmdemp = new SqlCommand(stremp, con);
        SqlDataAdapter adpemp = new SqlDataAdapter(cmdemp);
        DataTable dsemp = new DataTable();
        adpemp.Fill(dsemp);
        if (dsemp.Rows.Count > 0)
        {
            lbllastname.Text = dsemp.Rows[0]["LastName"].ToString();
            lblfirstname.Text = dsemp.Rows[0]["FirstName"].ToString();

            lblsocialno.Text = dsemp.Rows[0]["SocialSecurityNo"].ToString();

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
            lblcity.Text = Convert.ToString(ds.Rows[0]["CityName"]);
            lblpostalcode.Text = Convert.ToString(ds.Rows[0]["zipcode"]);
            lblbussinessph.Text = Convert.ToString(ds.Rows[0]["ContactNo"]);
            if (lblfirstname.Text.Length <= 0)
            {
                lblfirstname.Text = Convert.ToString(ds.Rows[0]["EmployeeName"]);
            }

        }
        else
        {
            lbladdress.Text = "";
            lblcity.Text = "";
            lblpostalcode.Text = "";
            lblbussinessph.Text = "";

        }
        


    }
    protected void Button3_Click(object sender, EventArgs e)
    {

        string str = " select * from F1_F2_TaxDedctionTbl where EmployeeId='" + ddlemployee.SelectedValue + "' and TaxYearId='" + ddltaxyear.SelectedValue + "' and [PayrollTaxMasterId] = '" + ddpayrolldeduction.SelectedValue + "' and Whid='" + ddlstore.SelectedValue + "'  ";
        
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

            if (txtlumpamt.Text != "")
            {
                totalamt += Convert.ToDecimal(txtlumpamt.Text);
            }
            if (txtrrsp.Text != "")
            {
                totalamt += Convert.ToDecimal(txtrrsp.Text);
            }
            if (txtF1.Text != "")
            {
                totalamt += Convert.ToDecimal(txtF1.Text);
            }
            if (txtF2.Text != "")
            {
                totalamt += Convert.ToDecimal(txtF2.Text);
            }
            if (txtempexp.Text != "")
            {
                totalamt += Convert.ToDecimal(txtempexp.Text);
            }
            if (txtcarrycharge.Text != "")
            {
                totalamt += Convert.ToDecimal(txtcarrycharge.Text);
            }
            if (txtotheramt.Text != "")
            {
                totalamt += Convert.ToDecimal(txtotheramt.Text);
            }
            txtgrossdedact.Text = Convert.ToString(totalamt);
            if (txtsubamt.Text != "")
            {
                totalamt -= Convert.ToDecimal(txtsubamt.Text);
            }
            txtfinalamt.Text = Convert.ToString(totalamt);
            if (totalamt <= 0)
            {
                ModapupExtende22.Show();
            }
            else
            {

                String avginsert = "Insert Into F1_F2_TaxDedctionTbl (EmployeeId,TaxYearId,PayrollTaxMasterId,Date,Whid,Salary,[LumpSum],[LumpSumAmt],[LumpSumDetail],[RRSP],[F1],[F2],[F2RecieptName],[F2SocialINCNo],[EmpExpense],[CarryAndInterestExpLoan],[Other],[OtherSpecification],[SubTractAMT],[Signature],[TotalAmt]) " +
                                   " values ('" + ddlemployee.SelectedValue + "','" + ddltaxyear.SelectedValue + "','" + ddpayrolldeduction.SelectedValue + "','" +txtdate.Text + "','" + ddlstore.SelectedValue + "','" + chksal.Checked + "','" + chklumpsum.Checked + "','" + txtlumpamt.Text + "','" + txtlumpdetail.Text + "','" + txtrrsp.Text + "','" + txtF1.Text + "','" + txtF2.Text + "','" + txtF2supportrecieptname.Text + "','" + txtF2supportrecieptsin.Text + "','" + txtempexp.Text + "','" + txtcarrycharge.Text + "','" + txtotheramt.Text + "','" + txtspecific.Text + "','" + txtsubamt.Text + "','" + txtsign.Text + "','" + totalamt + "')";
                SqlCommand cmdavg = new SqlCommand(avginsert, con);
                con.Open();
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
        lblCompany.Text = Session["Cname"].ToString() + " - " + ddlfilterbus.SelectedItem.Text;
        lblemp.Text = "Employee : " + Convert.ToString(ddlfilteremp.SelectedItem.Text);
        lblpay.Text = "Tax Name : " + Convert.ToString(ddlfilterdedctionname.SelectedItem.Text);
        lblyea.Text = "Tax Year : " + Convert.ToString(ddlfilteryear.SelectedItem.Text);
        
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
        if (ddlfilterdedctionname.SelectedIndex > 0)
        {
            dty += " and WithholdingRequestMasterTbl.DeductionMasterID='" + ddlfilterdedctionname.SelectedValue + "'";
        }

        string str = "select Distinct F1_F2_TaxDedctionTbl.Id, F1_F2_TaxDedctionTbl.TotalAmt, Convert(nvarchar, F1_F2_TaxDedctionTbl.Date,101) as Date,EmployeeMaster.EmployeeName,WareHouseMaster.Name as WName,TaxYear_Name from F1_F2_TaxDedctionTbl inner join   EmployeeMaster on EmployeeMaster.EmployeeMasterID=F1_F2_TaxDedctionTbl.EmployeeId inner join WareHouseMaster on F1_F2_TaxDedctionTbl.Whid=WareHouseMaster.WareHouseId inner join Tax_Year on Tax_Year.TaxYear_Id = F1_F2_TaxDedctionTbl.TaxYearId inner join PayrollTaxMaster on [PayrollTaxMaster].[PayRollTax_Id]=F1_F2_TaxDedctionTbl.PayrollTaxMasterId where WareHouseMaster.comid='" + Session["Comid"].ToString() + "'";
        //select EmployeePayrollMaster.LastName+':'+EmployeePayrollMaster.FirstName+':'+EmployeePayrollMaster.Intials as EmployeeName from EmployeePayrollMaster
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
            string strsel = "select * from F1_F2_TaxDedctionTbl where Id='" + ViewState["MasterId"] + "'";
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
                chksal.Checked = Convert.ToBoolean(dtview.Rows[0]["Salary"]);
                chklumpsum.Checked = Convert.ToBoolean(dtview.Rows[0]["LumpSum"]);

                txtlumpamt.Text = Convert.ToString(dtview.Rows[0]["LumpSumAmt"]);
                txtlumpdetail.Text = Convert.ToString(dtview.Rows[0]["LumpSumDetail"]);
                txtrrsp.Text = Convert.ToString(dtview.Rows[0]["RRSP"]);
                txtF1.Text = Convert.ToString(dtview.Rows[0]["F1"]);
                txtF2.Text = Convert.ToString(dtview.Rows[0]["F2"]);
                txtF2supportrecieptname.Text = Convert.ToString(dtview.Rows[0]["F2RecieptName"]);
                txtF2supportrecieptsin.Text = Convert.ToString(dtview.Rows[0]["F2SocialINCNo"]);
                txtempexp.Text = Convert.ToString(dtview.Rows[0]["EmpExpense"]);

                txtcarrycharge.Text = Convert.ToString(dtview.Rows[0]["CarryAndInterestExpLoan"]);
                txtotheramt.Text = Convert.ToString(dtview.Rows[0]["Other"]);
                txtspecific.Text = Convert.ToString(dtview.Rows[0]["OtherSpecification"]);
                 txtsubamt.Text = Convert.ToString(dtview.Rows[0]["SubTractAMT"]);
                txtfinalamt.Text = Convert.ToString(dtview.Rows[0]["TotalAmt"]);
                txtsign.Text = Convert.ToString(dtview.Rows[0]["Signature"]);
                txtdate.Text = Convert.ToString(dtview.Rows[0]["Date"]);
                if (Convert.ToString(dtview.Rows[0]["SubTractAMT"]) != "")
                {
                    txtgrossdedact.Text = Convert.ToString(Convert.ToDecimal(dtview.Rows[0]["TotalAmt"]) - Convert.ToDecimal(dtview.Rows[0]["SubTractAMT"]));
                }
                else
                {
                     txtgrossdedact.Text = Convert.ToString(dtview.Rows[0]["TotalAmt"]);
                }
               // txtrrsp.Text = dtview.Rows[0]["1BasicpersonalAmount"].ToString();
              //  txtF1.Text = dtview.Rows[0]["2ChildAmount"].ToString();
            }
        }
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string st2 = "Delete from F1_F2_TaxDedctionTbl where Id='" + GridView1.DataKeys[e.RowIndex].Value.ToString() + "' ";
        SqlCommand cmd2 = new SqlCommand(st2, con);
       
        con.Open();
        cmd2.ExecuteNonQuery();
        con.Close();
        GridView1.EditIndex = -1;
        fillgrid();
        lblmsg.Visible = true;
        lblmsg.Text = "Record succesfully deleted";
    }
    protected void Button4_Click(object sender, EventArgs e)
    {

        string str = " select * from F1_F2_TaxDedctionTbl where EmployeeId='" + ddlemployee.SelectedValue + "' and TaxYearId='" + ddltaxyear.SelectedValue + "' and [PayrollTaxMasterId] = '" + ddpayrolldeduction.SelectedValue + "' and Whid='" + ddlstore.SelectedValue + "' and Id<>'" + ViewState["MasterId"] + "' ";
        
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

            if (txtlumpamt.Text != "")
            {
                totalamt += Convert.ToDecimal(txtlumpamt.Text);
            }
            if (txtrrsp.Text != "")
            {
                totalamt += Convert.ToDecimal(txtrrsp.Text);
            }
            if (txtF1.Text != "")
            {
                totalamt += Convert.ToDecimal(txtF1.Text);
            }
            if (txtF2.Text != "")
            {
                totalamt += Convert.ToDecimal(txtF2.Text);
            }
            if (txtempexp.Text != "")
            {
                totalamt += Convert.ToDecimal(txtempexp.Text);
            }
            if (txtcarrycharge.Text != "")
            {
                totalamt += Convert.ToDecimal(txtcarrycharge.Text);
            }
            if (txtotheramt.Text != "")
            {
                totalamt += Convert.ToDecimal(txtotheramt.Text);
            }
            txtgrossdedact.Text = Convert.ToString(totalamt);
            if (txtsubamt.Text != "")
            {
                totalamt -= Convert.ToDecimal(txtsubamt.Text);
            }
            txtfinalamt.Text = Convert.ToString(totalamt);
            if (totalamt <= 0)
            {
                ModapupExtende22.Show();
            }
            else
            {

                String avginsert = "Update F1_F2_TaxDedctionTbl set EmployeeId='" + ddlemployee.SelectedValue + "',TaxYearId='" + ddltaxyear.SelectedValue + "',PayrollTaxMasterId='" + ddpayrolldeduction.SelectedValue + "',Date='" + txtdate.Text + "',Whid='" + ddlstore.SelectedValue + "',Salary='" + chksal.Checked + "',[LumpSum]='" + chksal.Checked + "',[LumpSumAmt]='" + txtlumpamt.Text + "',[LumpSumDetail]='" + txtlumpdetail.Text + "',[RRSP]='" + txtrrsp.Text + "',[F1]='" + txtF1.Text + "',[F2]='" + txtF2.Text + "',[F2RecieptName]='" + txtF2supportrecieptname.Text + "',[F2SocialINCNo]='" + txtF2supportrecieptsin.Text + "',[EmpExpense]='" + txtempexp.Text + "',[CarryAndInterestExpLoan]='" + txtcarrycharge.Text + "',[Other]='" + txtotheramt.Text + "',[OtherSpecification]='" + txtspecific.Text + "',[SubTractAMT]='" + txtsubamt.Text + "',[Signature]='" + txtsign.Text + "',[TotalAmt]='" +totalamt + "' where Id='" + ViewState["MasterId"] + "' ";

                SqlCommand cmdavg = new SqlCommand(avginsert, con);
                con.Open();
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
  

   
  
    protected void clearall()
    {
      
        ddlemployee.SelectedIndex = 0;
        ddltaxyear.SelectedIndex = 0;
        ddpayrolldeduction.SelectedIndex = 0;
        txtlumpamt.Text = "";
        txtlumpdetail.Text = "";
        txtrrsp.Text = "";
        txtF1.Text = "";
        txtF2.Text = "";
        txtF2supportrecieptname.Text = "";
        txtF2supportrecieptsin.Text = "";
        txtempexp.Text = "";

        txtcarrycharge.Text = "";
        txtotheramt.Text = "";
        txtspecific.Text = "";
        txtgrossdedact.Text = "";
        txtsubamt.Text = "";
        txtfinalamt.Text = "";
        txtsign.Text = "";
        txtdate.Text = DateTime.Now.ToShortDateString();
    }
    protected void Button5_Click(object sender, EventArgs e)
    {
        Button3.Visible = true;
        Button4.Visible = false;
        clearall();
        lblmsg.Visible = false;
    }

    protected void chksal_CheckedChanged(object sender, EventArgs e)
    {
        if (chksal.Checked == true)
        {
            Panel5.Visible = false;
        }
        
    }
    protected void chklumpsum_CheckedChanged(object sender, EventArgs e)
    {
        if (chklumpsum.Checked == true)
        {
            Panel5.Visible = true;
        }
        else
        {
            Panel5.Visible = false;
        }
    }
    protected void ddlfilterbus_SelectedIndexChanged(object sender, EventArgs e)
    {
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
    protected void ddltaxyear_SelectedIndexChanged(object sender, EventArgs e)
    {
        lblyear.Text = Convert.ToString(ddltaxyear.SelectedItem.Text);
    }
    protected void txtsubamt_TextChanged(object sender, EventArgs e)
    {

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
    protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
    {

    }

   
}
