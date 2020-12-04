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

public partial class ShoppingCart_Admin_CanadaFederalTd2 : System.Web.UI.Page
{
    // SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
    SqlConnection con;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Comid"] == null)
        {
            Response.Redirect("~/Shoppingcart/Admin/ShoppingCartLogin.aspx");
        }
        PageConn pgcon = new PageConn();
        con = pgcon.dynconn;
        pagetitleclass pg = new pagetitleclass();
        string strData = Request.Url.ToString();

        char[] separator = new char[] { '/' };
        // compid = Session["comid"].ToString();
        string[] strSplitArr = strData.Split(separator);
        int i = Convert.ToInt32(strSplitArr.Length);
        string page = strSplitArr[i - 1].ToString();


        Page.Title = pg.getPageTitle(page);

        // lblmsg.Visible = false;
        if (!IsPostBack)
        {
            Pagecontrol.dypcontrol(Page, page);

            fillstore();
            fillempddl();
            filltaxyear();
            fillPayrolltax();
            fillgrid();
            filterstore();
            DropDownList2.Items.Insert(0, "All");
            DropDownList2.Items[0].Value = "0";


            DropDownList3.Items.Insert(0, "All");
            DropDownList3.Items[0].Value = "0";

        }
    }
    protected void fillstore()
    {
        string str = "select WareHouseId,Name from WareHouseMaster WHERE comid='" + Session["Comid"].ToString() + "'and [WareHouseMaster].Status='1' order by Name";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);
        ddlstore.DataSource = ds;
        ddlstore.DataTextField = "Name";
        ddlstore.DataValueField = "WareHouseId";
        ddlstore.DataBind();

    }
    protected void fillempddl()
    {
        string stremp = "select EmployeeMaster.EmployeeMasterID,EmployeeMaster.EmployeeName+':'+DesignationMaster.DesignationName+':'+cast(EmployeeMaster.EmployeeMasterID as Nvarchar(50)) as EmployeeName   from EmployeeMaster inner join DesignationMaster on EmployeeMaster.DesignationMasterId=DesignationMaster.DesignationMasterId where EmployeeMaster.Whid='" + ddlstore.SelectedValue + "' ";
        SqlCommand cmdemp = new SqlCommand(stremp, con);
        SqlDataAdapter adpemp = new SqlDataAdapter(cmdemp);
        DataSet dsemp = new DataSet();
        adpemp.Fill(dsemp);
        ddlemployee.DataSource = dsemp;
        ddlemployee.DataTextField = "EmployeeName";
        ddlemployee.DataValueField = "EmployeeMasterID";
        ddlemployee.DataBind();
        ddlemployee.Items.Insert(0, "-Select-");

        ddlemployee.Items[0].Value = "0";

    }
    protected void filltaxyear()
    {
        string stremp = "select TaxYear_Id,TaxYear_Name+':'+Convert (Nvarchar(50), StartDate ,101 ) +':'+ Convert (Nvarchar(50), EndDate ,101 ) as TaxYear_Name  from Tax_Year where Active='1' ";
        SqlCommand cmdemp = new SqlCommand(stremp, con);
        SqlDataAdapter adpemp = new SqlDataAdapter(cmdemp);
        DataSet dsemp = new DataSet();
        adpemp.Fill(dsemp);
        ddltaxyear.DataSource = dsemp;
        ddltaxyear.DataTextField = "TaxYear_Name";
        ddltaxyear.DataValueField = "TaxYear_Id";
        ddltaxyear.DataBind();

    }
    protected void fillPayrolltax()
    {

        string str = "  Select [PayrollTaxMaster].[PayRollTax_Id],[PayrollTaxMaster].[Tax_Name]+' : '+ CountryMaster.CountryName+' : '+StateMasterTbl.StateName +' : '+Convert(nvarchar,PayrolltaxMaster.start_date,101) +' : '+Convert(nvarchar,PayrolltaxMaster.end_date,101) as tax_name " +
                     " from [PayrollTaxMaster]  inner join CountryMaster on CountryMaster.CountryId=PayrolltaxMaster.Country_id " +
                    "  inner join StateMasterTbl on StateMastertbl.StateId=CountryMaster.CountryId " +
                    "  where [PayrollTaxMaster].Status='1' order by [PayrollTaxMaster].[Tax_Name]";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);

        ddpayrolldeduction.DataSource = ds;
        ddpayrolldeduction.DataTextField = "tax_name";
        ddpayrolldeduction.DataValueField = "PayRollTax_Id";
        ddpayrolldeduction.DataBind();


    }
    protected void ddlstore_SelectedIndexChanged(object sender, EventArgs e)
    {
        fillempddl();
        filltaxyear();
        fillPayrolltax();
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
            lblintial.Text = dsemp.Rows[0]["Intials"].ToString();
            lblempno.Text = dsemp.Rows[0]["EmployeeNo"].ToString();
            lbldateofbirth.Text = dsemp.Rows[0]["DateOfBirth"].ToString();
            lblsocialno.Text = dsemp.Rows[0]["SocialSecurityNo"].ToString();

        }
        string str = " select EmployeeMaster.*,User_master.zipcode from EmployeeMaster  inner join User_master on User_master.PartyID=EmployeeMaster.PartyID where EmployeeMaster.EmployeeMasterID='" + ddlemployee.SelectedValue + "'";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable ds = new DataTable();
        adp.Fill(ds);
        if (ds.Rows.Count > 0)
        {
            lbladdress.Text = ds.Rows[0]["Address"].ToString();
            lblpincode.Text = ds.Rows[0]["zipcode"].ToString();
        }



    }
    protected void Button3_Click(object sender, EventArgs e)
    {

        string str = " select * from CanadaFederalTd2 where EmployeeId='" + ddlemployee.SelectedValue + "' and Whid='" + ddlstore.SelectedValue + "'  ";

        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable ds = new DataTable();
        adp.Fill(ds);
        if (ds.Rows.Count > 0)
        {
            lblmsg.Visible = true;
            lblmsg.Text = "Record Already Exists.";
        }
        else
        {


            double item1 = 0;
            double item2 = 0;
            double item3 = 0;
            double item4 = 0;
            double item5 = 0;
            double item6 = 0;
            double item7 = 0;
            double item8 = 0;
            double item9 = 0;
            double item10 = 0;
            double item11 = 0;
            //double item12 = 0;

            if (TextBox15.Text != "")
            {
                item1 = Convert.ToDouble(TextBox15.Text);
            }
            if (TextBox14.Text != "")
            {
                item2 = Convert.ToDouble(TextBox14.Text);
            }
            if (TextBox13.Text != "")
            {
                item3 = Convert.ToDouble(TextBox13.Text);
            }
            if (TextBox12.Text != "")
            {
                item4 = Convert.ToDouble(TextBox12.Text);
            }
            if (TextBox11.Text != "")
            {
                item5 = Convert.ToDouble(TextBox11.Text);
            }
            if (TextBox10.Text != "")
            {
                item6 = Convert.ToDouble(TextBox10.Text);
            }
            if (TextBox9.Text != "")
            {
                item7 = Convert.ToDouble(TextBox9.Text);
            }
            if (TextBox8.Text != "")
            {
                item8 = Convert.ToDouble(TextBox8.Text);
            }
            if (TextBox7.Text != "")
            {
                item9 = Convert.ToDouble(TextBox7.Text);
            }
            if (TextBox6.Text != "")
            {
                item10 = Convert.ToDouble(TextBox6.Text);
            }
            if (TextBox5.Text != "")
            {
                item11 = Convert.ToDouble(TextBox5.Text);
            }
            //if (TextBox4.Text != "")
            //{
            //    item12 = Convert.ToDouble(TextBox4.Text);
            //}
            double temp = item1 + item2 + item3 + item4 + item5 + item6 + item7 + item8 + item9 + item10 + item11;
            TextBox4.Text = temp.ToString();




            String avginsert = "Insert Into CanadaFederalTd2 (EmployeeId,TaxTearId,PayrollTaxMasterId,Date,Whid,compid,[1BasicpersonalAmount],[2ChildAmount],[3AgeAmount],[4PensionIncomeAmount],[5TutionEducationtextbookAmount],[6DisabilityAmount],[7Spouseorcommonlawamount],[8CaregiverAmount],[9Amountforinfirmdependantsage18orolder],[10Amountstransferredfromyourspouseorcommonlawpartner],[11Amountstransferred],[12TotalClaimAmount],[13Morethanoneemployerorpayer],[14Signature],[15SignatureDate],[21taxyear]) " +
                               " values ('" + ddlemployee.SelectedValue + "','" + ddltaxyear.SelectedValue + "','" + ddpayrolldeduction.SelectedValue + "','" + System.DateTime.Now.ToShortDateString() + "','" + ddlstore.SelectedValue + "','" + Session["Comid"].ToString() + "','" + TextBox15.Text + "','" + TextBox14.Text + "','" + TextBox13.Text + "','" + TextBox12.Text + "','" + TextBox11.Text + "','" + TextBox10.Text + "','" + TextBox9.Text + "','" + TextBox8.Text + "','" + TextBox7.Text + "','" + TextBox6.Text + "','" + TextBox5.Text + "','" + TextBox4.Text + "','" + CheckBox2.Checked + "','" + TextBox17.Text + "','" + TextBox16.Text + "','" + TextBox20.Text + "')";
            SqlCommand cmdavg = new SqlCommand(avginsert, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmdavg.ExecuteNonQuery();
            con.Close();

            fillgrid();
            clearall();
            lblmsg.Visible = true;
            lblmsg.Text = "Record inserted successfully";
        }

    }

    protected void fillgrid()
    {
        string str = "select CanadaFederalTd2.*,EmployeeMaster.EmployeeName,WareHouseMaster.Name as WName from CanadaFederalTd2 inner join EmployeePayrollMaster on EmployeePayrollMaster.EmpId=CanadaFederalTd2.EmployeeId inner join EmployeeMaster on EmployeeMaster.EmployeeMasterID=CanadaFederalTd2.EmployeeId inner join WareHouseMaster on CanadaFederalTd2.Whid=WareHouseMaster.WareHouseId where CanadaFederalTd2.compid='" + Session["Comid"].ToString() + "' order by EmployeePayrollMaster.LastName";
        //select EmployeePayrollMaster.LastName+':'+EmployeePayrollMaster.FirstName+':'+EmployeePayrollMaster.Intials as EmployeeName from EmployeePayrollMaster
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);
        GridView1.DataSource = ds;
        GridView1.DataBind();

    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        GridView1.SelectedIndex = Convert.ToInt32(e.CommandArgument);
        ViewState["MasterId"] = GridView1.SelectedDataKey.Value;

        if (e.CommandName == "vi")
        {
            Button3.Visible = false;
            Button4.Visible = true;

            string strsel = "select * from CanadaFederalTd2 where Id='" + ViewState["MasterId"] + "'";
            SqlCommand cmdview = new SqlCommand(strsel, con);
            SqlDataAdapter dtpview = new SqlDataAdapter(cmdview);
            DataTable dtview = new DataTable();
            dtpview.Fill(dtview);
            fillstore();
            ddlstore.SelectedIndex = ddlstore.Items.IndexOf(ddlstore.Items.FindByValue(dtview.Rows[0]["Whid"].ToString()));
            fillempddl();
            ddlemployee.SelectedIndex = ddlemployee.Items.IndexOf(ddlemployee.Items.FindByValue(dtview.Rows[0]["EmployeeId"].ToString()));
            filltaxyear();
            ddltaxyear.SelectedIndex = ddltaxyear.Items.IndexOf(ddltaxyear.Items.FindByValue(dtview.Rows[0]["TaxTearId"].ToString()));
            fillPayrolltax();
            ddpayrolldeduction.SelectedIndex = ddpayrolldeduction.Items.IndexOf(ddpayrolldeduction.Items.FindByValue(dtview.Rows[0]["PayrollTaxMasterId"].ToString()));

            TextBox15.Text = dtview.Rows[0]["1BasicpersonalAmount"].ToString();
            TextBox14.Text = dtview.Rows[0]["2ChildAmount"].ToString();
            TextBox13.Text = dtview.Rows[0]["3AgeAmount"].ToString();
            TextBox12.Text = dtview.Rows[0]["4PensionIncomeAmount"].ToString();
            TextBox11.Text = dtview.Rows[0]["5TutionEducationtextbookAmount"].ToString();
            TextBox10.Text = dtview.Rows[0]["6DisabilityAmount"].ToString();
            TextBox9.Text = dtview.Rows[0]["7Spouseorcommonlawamount"].ToString();
            TextBox8.Text = dtview.Rows[0]["8CaregiverAmount"].ToString();
            TextBox7.Text = dtview.Rows[0]["9Amountforinfirmdependantsage18orolder"].ToString();
            TextBox6.Text = dtview.Rows[0]["10Amountstransferredfromyourspouseorcommonlawpartner"].ToString();
            TextBox5.Text = dtview.Rows[0]["11Amountstransferred"].ToString();
            TextBox4.Text = dtview.Rows[0]["12TotalClaimAmount"].ToString();
            // TextBox3.Text = dtview.Rows[0]["13TotalClaimAmount"].ToString();
            // TextBox2.Text = dtview.Rows[0]["17Deductionforlivinginaprescribedzone"].ToString();
            // TextBox1.Text = dtview.Rows[0]["18Additionaltaxtobededucted"].ToString();
            TextBox17.Text = dtview.Rows[0]["14Signature"].ToString();
            TextBox16.Text = dtview.Rows[0]["15SignatureDate"].ToString();

            //  CheckBox1.Checked = Convert.ToBoolean(dtview.Rows[0]["14Morethanoneemployerorpayer"].ToString());
            CheckBox2.Checked = Convert.ToBoolean(dtview.Rows[0]["13Morethanoneemployerorpayer"].ToString());
            // CheckBox3.Checked = Convert.ToBoolean(dtview.Rows[0]["16Nonresidents"].ToString());
        }
    }
    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
        string st2 = "Delete from CanadaFederalTd2 where Id='" + GridView1.DataKeys[e.RowIndex].Value.ToString() + "' ";
        SqlCommand cmd2 = new SqlCommand(st2, con);

        if (con.State.ToString() != "Open")
        {
            con.Open();
        }
        cmd2.ExecuteNonQuery();
        con.Close();
        GridView1.EditIndex = -1;
        fillgrid();
        // lblmsg.Visible = true;
        // lblmsg.Text = "Record Succesfully Deleted";
    }
    protected void Button4_Click(object sender, EventArgs e)
    {

        string str = " select * from CanadaFederalTd2 where EmployeeId='" + ddlemployee.SelectedValue + "' and Whid='" + ddlstore.SelectedValue + "' and Id<>'" + ViewState["MasterId"] + "' ";

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


            double item1 = 0;
            double item2 = 0;
            double item3 = 0;
            double item4 = 0;
            double item5 = 0;
            double item6 = 0;
            double item7 = 0;
            double item8 = 0;
            double item9 = 0;
            double item10 = 0;
            double item11 = 0;
            //double item12 = 0;

            if (TextBox15.Text != "")
            {
                item1 = Convert.ToDouble(TextBox15.Text);
            }
            if (TextBox14.Text != "")
            {
                item2 = Convert.ToDouble(TextBox14.Text);
            }
            if (TextBox13.Text != "")
            {
                item3 = Convert.ToDouble(TextBox13.Text);
            }
            if (TextBox12.Text != "")
            {
                item4 = Convert.ToDouble(TextBox12.Text);
            }
            if (TextBox11.Text != "")
            {
                item5 = Convert.ToDouble(TextBox11.Text);
            }
            if (TextBox10.Text != "")
            {
                item6 = Convert.ToDouble(TextBox10.Text);
            }
            if (TextBox9.Text != "")
            {
                item7 = Convert.ToDouble(TextBox9.Text);
            }
            if (TextBox8.Text != "")
            {
                item8 = Convert.ToDouble(TextBox8.Text);
            }
            if (TextBox7.Text != "")
            {
                item9 = Convert.ToDouble(TextBox7.Text);
            }
            if (TextBox6.Text != "")
            {
                item10 = Convert.ToDouble(TextBox6.Text);
            }
            if (TextBox5.Text != "")
            {
                item11 = Convert.ToDouble(TextBox5.Text);
            }
            //if (TextBox4.Text != "")
            //{
            //    item12 = Convert.ToDouble(TextBox4.Text);
            //}
            double temp = item1 + item2 + item3 + item4 + item5 + item6 + item7 + item8 + item9 + item10 + item11;
            TextBox4.Text = temp.ToString();

            String avginsert = "Update CanadaFederalTd2 set EmployeeId='" + ddlemployee.SelectedValue + "',TaxTearId='" + ddltaxyear.SelectedValue + "',PayrollTaxMasterId='" + ddpayrolldeduction.SelectedValue + "',Date='" + System.DateTime.Now.ToShortDateString() + "',Whid='" + ddlstore.SelectedValue + "',compid='" + Session["Comid"].ToString() + "',[1BasicpersonalAmount]='" + TextBox15.Text + "',[2ChildAmount]='" + TextBox14.Text + "',[3AgeAmount]='" + TextBox13.Text + "',[4PensionIncomeAmount]='" + TextBox12.Text + "',[5TutionEducationtextbookAmount]='" + TextBox11.Text + "',[6DisabilityAmount]='" + TextBox10.Text + "',[7Spouseorcommonlawamount]='" + TextBox9.Text + "',[8CaregiverAmount]='" + TextBox8.Text + "',[9Amountforinfirmdependantsage18orolder]='" + TextBox7.Text + "',[10Amountstransferredfromyourspouseorcommonlawpartner]='" + TextBox6.Text + "',[11Amountstransferred]='" + TextBox5.Text + "',[12TotalClaimAmount]='" + TextBox4.Text + "',[13Morethanoneemployerorpayer]='" + CheckBox2.Checked + "',[14Signature]='" + TextBox17.Text + "',[15SignatureDate]='" + TextBox16.Text + "',[21taxyear]='" + TextBox20.Text + "' where Id='" + ViewState["MasterId"] + "' ";

            SqlCommand cmdavg = new SqlCommand(avginsert, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmdavg.ExecuteNonQuery();
            con.Close();
            fillgrid();
            clearall();
            lblmsg.Visible = true;
            lblmsg.Text = "Record updated Successfully";
            Button3.Visible = true;
            Button4.Visible = false;
        }
    }
    protected void filterstore()
    {
        string str = "select WareHouseId,Name from WareHouseMaster WHERE comid='" + Session["Comid"].ToString() + "'and [WareHouseMaster].Status='1' order by Name";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);
        DropDownList1.DataSource = ds;
        DropDownList1.DataTextField = "Name";
        DropDownList1.DataValueField = "WareHouseId";
        DropDownList1.DataBind();

    }
    protected void Filteremployee()
    {
        string stremp = "select EmployeeMaster.EmployeeMasterID,EmployeeMaster.EmployeeName+':'+DesignationMaster.DesignationName+':'+cast(EmployeeMaster.EmployeeMasterID as Nvarchar(50)) as EmployeeName   from EmployeeMaster inner join DesignationMaster on EmployeeMaster.DesignationMasterId=DesignationMaster.DesignationMasterId inner join EmployeeBatchMaster on EmployeeBatchMaster.Batchmasterid=BatchMaster.ID where EmployeeBatchMaster.Batchmasterid='" + DropDownList2.SelectedValue + "' ";
        SqlCommand cmdemp = new SqlCommand(stremp, con);
        SqlDataAdapter adpemp = new SqlDataAdapter(cmdemp);
        DataSet dsemp = new DataSet();
        adpemp.Fill(dsemp);
        DropDownList3.DataSource = dsemp;
        DropDownList3.DataTextField = "EmployeeName";
        DropDownList3.DataValueField = "EmployeeMasterID";
        DropDownList3.DataBind();

        //DropDownList3.Items.Insert(0, "-Select-");

        //DropDownList3.Items[0].Value = "0";

    }

    protected void Button6_Click(object sender, EventArgs e)
    {
        string str = "select CanadaFederalTd2.*,EmployeeMaster.EmployeeName,WareHouseMaster.Name as WName from CanadaFederalTd2 inner join EmployeePayrollMaster on EmployeePayrollMaster.EmpId=CanadaFederalTd2.EmployeeId inner join EmployeeMaster on EmployeeMaster.EmployeeMasterID=CanadaFederalTd2.EmployeeId inner join WareHouseMaster on CanadaFederalTd2.Whid=WareHouseMaster.WareHouseId where CanadaFederalTd2.compid='" + Session["Comid"].ToString() + "' and CanadaFederalTd2.EmployeeId='" + DropDownList3.SelectedValue + "' and CanadaFederalTd2.Date between '" + TextBox18.Text + "' and '" + TextBox19.Text + "' order by EmployeePayrollMaster.LastName";
        //select EmployeePayrollMaster.LastName+':'+EmployeePayrollMaster.FirstName+':'+EmployeePayrollMaster.Intials as EmployeeName from EmployeePayrollMaster
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);
        GridView1.DataSource = ds;
        GridView1.DataBind();

    }
    protected void Fillddlbatchname()
    {

        string str = "select ID,Name from BatchMaster where WHID='" + DropDownList1.SelectedValue + "' and Status='" + 1 + "' ";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);
        DropDownList2.DataSource = ds;
        DropDownList2.DataTextField = "Name";
        DropDownList2.DataValueField = "ID";

        DropDownList2.DataBind();


    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {
        Fillddlbatchname();

    }
    protected void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
    {
        Filteremployee();
    }
    protected void clearall()
    {
        ddlstore.SelectedIndex = 0;
        ddlemployee.SelectedIndex = 0;
        ddltaxyear.SelectedIndex = 0;
        ddpayrolldeduction.SelectedIndex = 0;
        TextBox15.Text = "";
        TextBox14.Text = "";
        TextBox13.Text = "";
        TextBox12.Text = "";
        TextBox11.Text = "";
        TextBox10.Text = "";
        TextBox9.Text = "";
        TextBox8.Text = "";
        TextBox7.Text = "";
        TextBox6.Text = "";
        TextBox5.Text = "";
        TextBox4.Text = "";

        TextBox17.Text = "";
        TextBox16.Text = "";
        TextBox20.Text = "";
        lblmsg.Text = "";
        Button3.Visible = true;
        Button4.Visible = false;

    }
}
