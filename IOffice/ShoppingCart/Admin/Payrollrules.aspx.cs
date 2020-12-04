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
using System.Globalization;
using System.Data.Sql;

public partial class Payrollrules : System.Web.UI.Page
{
    //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["OnlineAccountConnectionString"].ConnectionString);
    SqlConnection con = new SqlConnection(PageConn.connnn);
    string compid;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["Comid"] == null)
        {
            Response.Redirect("~/Shoppingcart/Admin/ShoppingCartLogin.aspx");
        }
        //PageConn pgcon = new PageConn();
        //con = pgcon.dynconn;
        pagetitleclass pg = new pagetitleclass();
        string strData = Request.Url.ToString();


        char[] separator = new char[] { '/' };

        string[] strSplitArr = strData.Split(separator);
        int i = Convert.ToInt32(strSplitArr.Length);
        string page = strSplitArr[i - 1].ToString();


        Page.Title = pg.getPageTitle(page);
        compid = Session["Comid"].ToString();
        statuslable.Visible = false;
        if (!IsPostBack)
        {
            ViewState["sortOrder"] = "";
           
            Pagecontrol.dypcontrol(Page, page);
            fillwarehouse();
            ddlwarehouse_SelectedIndexChanged(sender, e);
           
          
            btnUpdate.Visible = false;

            //Button6.Visible = false;
        }
    }
    protected void fillwarehouse()
    {
        ddlwarehouse.Items.Clear();
        DataTable ds = ClsStore.SelectStorename();
        if (ds.Rows.Count > 0)
        {
            ddlwarehouse.DataSource = ds;
            ddlwarehouse.DataTextField = "Name";
            ddlwarehouse.DataValueField = "WareHouseId";
            ddlwarehouse.DataBind();


            DataTable dteeed = ClsStore.SelectEmployeewithIdwise();

            if (dteeed.Rows.Count > 0)
            {
                ddlwarehouse.SelectedValue = Convert.ToString(dteeed.Rows[0]["Whid"]);
            }
        }
    }
   

    protected void Button5_Click(object sender, EventArgs e)
    {

        pnlenb.Enabled = false;

        txtconsi.Text = "3";



        CheckBox1_CheckedChanged(sender, e);


        statuslable.Visible = true;
        btnUpdate.Visible = false;
        pnladd.Visible = false;
        btnEdit.Visible = false;
       
    }

    protected void edit()
    {
        pnlenb.Enabled = false;
        string str1 = "Select AttandanceRule.*,WarehouseMaster.Name as StoreName, WarehouseMaster.WareHouseId,AcceptableInTimeDeviationMinutes, " +
                   " AcceptableOutTimeDeviationMinutes  from AttandanceRule inner join WarehouseMaster " +
               " on AttandanceRule.StoreId=WarehouseMaster.WareHouseId where AttandanceRule.StoreId='" + ddlwarehouse.SelectedValue + "' and AttandanceRule.CompId='"+Session["Comid"]+"'";

        SqlCommand cmd = new SqlCommand(str1, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();

        adp.Fill(dt);
        //if (dt.Rows.Count > 0)
        //{
            // ddlwarehouse.SelectedIndex =ddlwarehouse.Items.IndexOf(ddlwarehouse.Items.FindByValue(dt.Rows[0]["StoreId"].ToString()));
            EventArgs e = new EventArgs();
            object sender = new object();
            //ddlwarehouse_SelectedIndexChanged(sender, e);
            if (Convert.ToString(dt.Rows[0]["PayrollAdmin"]) != "")
            {
                ddlemp.SelectedIndex = ddlemp.Items.IndexOf(ddlemp.Items.FindByValue(dt.Rows[0]["PayrollAdmin"].ToString()));
            }
            else
            {
                ddlemp.SelectedIndex = ddlemp.Items.IndexOf(ddlemp.Items.FindByValue(dt.Rows[0]["SeniorEmployeeID"].ToString()));

            }
            if (Convert.ToString(dt.Rows[0]["Generalapprovalrule"]) != "")
            {
                chkgenralrule.Checked = Convert.ToBoolean(dt.Rows[0]["Generalapprovalrule"]);
            }
            ViewState["Id"] = dt.Rows[0]["Attendence_Rule_Id"].ToString();
            if (Convert.ToString(dt.Rows[0]["op2graceperiod"]) == "2")
            {
                rdru2.SelectedIndex = 0;
                // ddlpertype.SelectedValue = Convert.ToString(dt.Rows[0]["op2intancepertype"]);
                txtconsi.Text = "";
                txtconsi.Text = Convert.ToString(dt.Rows[0]["Considerinoutrangeintance"]);

            }
            else
            {
                rdru2.SelectedIndex =1;
            }
            rdru2_SelectedIndexChanged(sender, e);
            lbllateen.Text = dt.Rows[0]["AcceptableInTimeDeviationMinutes"].ToString();
            lblearltmin.Text = dt.Rows[0]["AcceptableOutTimeDeviationMinutes"].ToString();


            if (Convert.ToBoolean(dt.Rows[0]["Considerwithinrangedeviationasstandardtime"].ToString()) == true)
            {
                // pnlconsider.Visible = true;
                // pnlrule2.Visible = true;
                txtconsi.Text = Convert.ToString(dt.Rows[0]["Considerinoutrangeintance"]);

            }
            else
            {
                pnlconsider.Visible = false;
                // pnlrule2.Visible = false;
            }


            if (Convert.ToString(dt.Rows[0]["nooverflunc"]) != "")
            {
                if (Convert.ToBoolean(dt.Rows[0]["nooverflunc"]) == true)
                {
                    checkbox.Checked = true;
                }
            }
            if (Convert.ToString(dt.Rows[0]["howdoovertime"]) != "")
            {
                chkoverhowdo.Checked = true;
                //if (Convert.ToBoolean(dt.Rows[0]["howdoovertime"]) == true)
                if (chkoverhowdo.Checked == true)
                {
                    // chkoverhowdo.Checked = true;
                    if (Convert.ToString(dt.Rows[0]["overtimeruleno"]) == "1")
                    {
                        rd1.Checked = true;

                        rd1_CheckedChanged(sender, e);

                    }
                    if (Convert.ToBoolean(dt.Rows[0]["Overtimepara"]) == true)
                    {

                        if (Convert.ToString(dt.Rows[0]["overtimeruleno"]) == "2")
                        {
                            rd2.Checked = true;

                            rd2_CheckedChanged(sender, e);
                            ddlrempt2.SelectedIndex = ddlrempt2.Items.IndexOf(ddlrempt2.Items.FindByText("Overtimeremunaration"));

                        }
                        else if (Convert.ToString(dt.Rows[0]["overtimeruleno"]) == "3")
                        {
                            remfill();
                            txtRate.Text = dt.Rows[0]["OvertimepaymentRate"].ToString();
                            txthour.Text = dt.Rows[0]["Overtimehours"].ToString();
                            pnloverA.Visible = true;
                            ddlramu.SelectedIndex = ddlramu.Items.IndexOf(ddlramu.Items.FindByText("Overtimeremunaration"));
                            if (Convert.ToString(dt.Rows[0]["overtomeapproval"]) != "")
                            {

                            }
                            if (Convert.ToString(dt.Rows[0]["overtimerulerange"]) != "")
                            {
                                chkoverA.Checked = Convert.ToBoolean(Convert.ToString(dt.Rows[0]["overtimerulerange"]));

                            }
                            rd3.Checked = true;
                            rd3_CheckedChanged(sender, e);
                        }



                    }
                }
            }

            chkoverhowdo_CheckedChanged(sender, e);
            chkappsuper.Checked = false;
            if (Convert.ToString(dt.Rows[0]["overtomeapproval"]) != "")
            {
                if (Convert.ToBoolean(dt.Rows[0]["overtomeapproval"]) == true)
                {
                    chkappsuper.Checked = true;
                }
            }

            btnUpdate.Visible = false;
            btnEdit.Visible = true;

            pnladd.Visible = true;
        //}
        //else
        //{
        //    statuslable.Visible = true;
        //    statuslable.Text = "Attendance rules not set";
        //}
    }

    public void enablecontrol(bool t)
    {


        txtconsi.Enabled = t;

        ddlemp.Enabled = t;
       // ddlwarehouse.Enabled = t;
        txthour.Enabled = t;
        txtRate.Enabled = t;
        ddlramu.Enabled = t;

        pnloverA.Enabled = t;


        rd1.Enabled = t;
        rd2.Enabled = t;
        rd3.Enabled = t;




    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {

        pnlenb.Enabled = true;
        enablecontrol(true);
        btnUpdate.Visible = true;
        btnEdit.Visible = false;
       

    }



 
    protected void btnUpdate_Click(object sender, EventArgs e)
    {



        string str1 = "select * from AttandanceRule where StoreId='" + ddlwarehouse.SelectedValue + "' and Attendence_Rule_Id<>'" + ViewState["Id"] + "'";
        SqlCommand cmd1 = new SqlCommand(str1, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd1);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            statuslable.Visible = true;
            statuslable.Text = "Record already exists";
            Button5_Click(sender, e);
        }
        else
        {  
            string remname = "";
            string str = "";
            SqlCommand cmd = new SqlCommand();
            int opid = 0;
            if (rd3.Checked == true)
            {
                remname = ddlramu.SelectedValue;
                opid = 3;
            }
            else if (rd1.Checked == true)
            {
                opid = 1;
            }
            else if (rd2.Checked == true)
            {
                remname = ddlrempt2.SelectedValue;
                opid = 2;
            }
            string upd = "";
            string granceperi = "";
            if (rdru2.SelectedIndex == 1)
            {
                granceperi = "3000";
            }
            else
            {
                granceperi = txtconsi.Text;
            }
            upd = " ,op2graceperiod='" + rdru2.SelectedValue + "' where [AttandanceRule].Attendence_Rule_Id= '" + ViewState["Id"] + "' ";
            if (opid == 3)
            {
                str = " Update  AttandanceRule Set Generalapprovalrule='"+chkgenralrule.Checked+"',  StoreId ='" + ddlwarehouse.SelectedValue + "' " +
           " ,Considerwithinrangedeviationasstandardtime='" + true + "' " +
           " ,PayrollAdmin='" + ddlemp.SelectedValue + "' " +
             " ,overtimeruleno='" + opid + "',OvertimepaymentRate='" + txtRate.Text + "',Overtimehours='" + txthour.Text + "',Overtimeremunaration='" + remname + "',Overtimepara='1',Considerinoutrangeintance='" + granceperi + "',overtimerulerange='" + checkbox.Checked + "',overtomeapproval='" + chkappsuper.Checked + "',howdoovertime='" + chkoverhowdo.Checked + "',nooverflunc='" + checkbox.Checked + "'";


            }

            else if (opid == 2)
            {

                str = " Update  AttandanceRule Set Generalapprovalrule='" + chkgenralrule.Checked + "', StoreId ='" + ddlwarehouse.SelectedValue + "', " +
            " Considerwithinrangedeviationasstandardtime='" + true + "', " +
            " PayrollAdmin='" + ddlemp.SelectedValue + "',Overtimepara='1',overtimeruleno='" + opid + "',Considerinoutrangeintance='" + granceperi + "',Overtimeremunaration='" + remname + "',overtomeapproval='" + chkappsuper.Checked + "',howdoovertime='" + chkoverhowdo.Checked + "',nooverflunc='" + checkbox.Checked + "'";

            }
            else if (opid == 1)
            {
                str = " Update  AttandanceRule Set Generalapprovalrule='" + chkgenralrule.Checked + "', StoreId ='" + ddlwarehouse.SelectedValue + "', " +
                 " Considerwithinrangedeviationasstandardtime='" + true + "', " +
                 " PayrollAdmin='" + ddlemp.SelectedValue + "',Overtimepara='0',overtimeruleno='" + opid + "',Considerinoutrangeintance='" + granceperi + "',overtomeapproval='" + chkappsuper.Checked + "',howdoovertime='" + chkoverhowdo.Checked + "',nooverflunc='" + checkbox.Checked + "'";

            }
            else
            {
                str = " Update  AttandanceRule Set Generalapprovalrule='" + chkgenralrule.Checked + "', StoreId ='" + ddlwarehouse.SelectedValue + "', " +
                " Considerwithinrangedeviationasstandardtime='" + true + "', " +
                " PayrollAdmin='" + ddlemp.SelectedValue + "' ,Overtimepara='0',overtimeruleno='" + opid + "',Considerinoutrangeintance='" + granceperi + "',overtomeapproval='" + chkappsuper.Checked + "',howdoovertime='" + chkoverhowdo.Checked + "',nooverflunc='" + checkbox.Checked + "'";

            }
            str = str + upd;
            cmd = new SqlCommand(str, con);
            if (con.State.ToString() != "Open")
            {
                con.Open();
            }
            cmd.ExecuteNonQuery();
            con.Close();
            string updetail = "";
           
            if (rdru2.SelectedValue == "2")
            {
                foreach (GridViewRow item in grdpay.Rows)
                {

                    string idgen = grdpay.DataKeys[item.RowIndex].Value.ToString();
                    TextBox txtpaytype = (TextBox)item.FindControl("txtpaytype");


                    string asc = "Select * from AttendancePayperiodtype where Whid='" + ddlwarehouse.SelectedValue + "' and PayperiodtypeIdforrule='" + idgen + "'";
                    SqlCommand cmade = new SqlCommand(asc, con);
                    SqlDataAdapter asass = new SqlDataAdapter(cmade);
                    DataTable dtade = new DataTable();
                    asass.Fill(dtade);
                    SqlCommand cmd8 = new SqlCommand();
                    if (dtade.Rows.Count == 0)
                    {

                        cmd8 = new SqlCommand("insert into AttendancePayperiodtype(PayperiodtypeIdforrule,payruletime,Whid)Values('" + idgen + "','" + txtpaytype.Text + "','" + ddlwarehouse.SelectedValue + "')", con);


                        if (con.State.ToString() != "Open")
                        {
                            con.Open();
                        }
                        cmd8.ExecuteNonQuery();
                        con.Close();
                    }
                    else
                    {
                        cmd8 = new SqlCommand("Update AttendancePayperiodtype Set payruletime='" + txtpaytype.Text + "' where PayperiodtypeIdforrule='" + idgen + "' and Whid='" + ddlwarehouse.SelectedValue + "'", con);


                        if (con.State.ToString() != "Open")
                        {
                            con.Open();
                        }
                        cmd8.ExecuteNonQuery();
                        con.Close();
                    }


                }
            }

            statuslable.Visible = true;
            statuslable.Text = "Record updated successfully";
            
            txtconsi.Text = "3";

            ddlwarehouse_SelectedIndexChanged(sender, e);
        }
    }
   
    protected void ddlwarehouse_SelectedIndexChanged(object sender, EventArgs e)
    {

        string str = "Select   distinct EmployeeMasterID,EmployeeName from EmployeeMaster inner join DesignationMaster on DesignationMaster.DesignationMasterId=EmployeeMaster.DesignationMasterId  where DesignationMaster.DesignationName IN('CEO','Manager','Admin','Accounting Manager','Office Manager') and Whid='" + ddlwarehouse.SelectedValue + "'  order by EmployeeName";
        SqlCommand cmd = new SqlCommand(str, con);

        SqlDataAdapter da = new SqlDataAdapter(cmd);

        DataSet ds = new DataSet();
        da.Fill(ds);

        ddlemp.DataSource = ds;
        ddlemp.DataTextField = "EmployeeName";
        ddlemp.DataValueField = "EmployeeMasterID";
        ddlemp.DataBind();
        //ddlemp.Items.Insert(0, "--Select--");
        //ddlemp.Items[0].Value = "0";



        remfill();
        edit();

    }


    protected void remfill()
    {
        ddlrempt2.Items.Clear();
        ddlramu.Items.Clear();
        string str = "Select   distinct Id,RemunerationName from RemunerationMaster  where Whid='" + ddlwarehouse.SelectedValue + "'  order by RemunerationName";
        SqlCommand cmd = new SqlCommand(str, con);

        SqlDataAdapter da = new SqlDataAdapter(cmd);

        DataTable ds = new DataTable();
        da.Fill(ds);
        if (ds.Rows.Count > 0)
        {
            ddlramu.DataSource = ds;
            ddlramu.DataTextField = "RemunerationName";
            ddlramu.DataValueField = "Id";
            ddlramu.DataBind();
            ddlramu.SelectedIndex = ddlramu.Items.IndexOf(ddlramu.Items.FindByText("Salary&Wages"));
            ddlrempt2.DataSource = ds;
            ddlrempt2.DataTextField = "RemunerationName";
            ddlrempt2.DataValueField = "Id";
            ddlrempt2.DataBind();
            ddlrempt2.SelectedIndex = ddlramu.Items.IndexOf(ddlramu.Items.FindByText("Salary&Wages"));
        }
    }





  
    protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
    {
        //if (CheckBox1.Checked == true)
        //{
        //    pnlrule2.Visible = true;
        //    pnlconsider.Visible = true;
        //}
        //else
        //{
        //    pnlrule2.Visible = false;
        //    pnlconsider.Visible = false;
        //}
    }
    protected void rd2_CheckedChanged(object sender, EventArgs e)
    {
        if (rd2.Checked == true)
        {


            pnloverA.Visible = false;
            ddlrempt2.Visible = false;
            rd1.Checked = false;
            rd3.Checked = false;
        }

    }
    protected void rd1_CheckedChanged(object sender, EventArgs e)
    {

        pnloverA.Visible = false;
        ddlrempt2.Visible = false;
        rd3.Checked = false;
        rd2.Checked = false;

    }
    protected void rd3_CheckedChanged(object sender, EventArgs e)
    {
        if (rd3.Checked == true)
        {

            pnloverA.Visible = true;
            ddlrempt2.Visible = false;
            rd1.Checked = false;
            rd2.Checked = false;
            remfill();
        }


    }
    protected void rdru2_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdru2.SelectedValue == "2")
        {
            pnlconsider.Visible = true;
            string str = "";


            str = "select payperiodtype.Id,Name,Case When(payruletime IS NULL) then '0' else payruletime end as aspayruletime from AttendancePayperiodtype Right Join  payperiodtype on payperiodtype.Id=AttendancePayperiodtype.PayperiodtypeIdforrule and AttendancePayperiodtype.Whid='" + ddlwarehouse.SelectedValue + "' where payperiodtype.Id in(2,3,4,5,10,11,12)   order by  payperiodtype.Id";

            SqlCommand cmd = new SqlCommand(str, con);

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataTable ds = new DataTable();
            da.Fill(ds);
            grdpay.DataSource = ds;
            grdpay.DataBind();


        }
        else
        {
            pnlconsider.Visible = false;
        }
    }
  
    protected void chkoverhowdo_CheckedChanged(object sender, EventArgs e)
    {
        if (chkoverhowdo.Checked == true)
        {
            pnloverop2radio.Visible = true;

        }
        else
        {
            pnloverop2radio.Visible = false;
        }
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


    
}
