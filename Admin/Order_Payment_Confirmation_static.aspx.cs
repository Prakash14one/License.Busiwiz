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


public partial class Admin_Order_Payment_Confirmation_static : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {

        lblmsg.Text = "";
        if (!IsPostBack)
        {
            string strcln = "Select * from ClientMaster";
            SqlCommand cmdcln = new SqlCommand(strcln, con);
            DataTable dtcln = new DataTable();
            SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
            adpcln.Fill(dtcln);
            ddlcname.DataSource = dtcln;
            ddlcname.DataValueField = "ClientMasterId";
            ddlcname.DataTextField = "CompanyName";
            ddlcname.DataBind();
            ddlcname.Items.Insert(0, "-Select-");
            ddlcname.Items[0].Value = "0";
            DDL_fillOrder();
            fillcompany();
            fillgrid();
        }
    }
    protected void DDL_fillOrder()
    {

        //string strcln = "SELECT distinct ProductMaster.ProductId, VersionInfoMaster.VersionInfoId,ProductMaster.ProductName + ' : ' + VersionInfoMaster.VersionInfoName as productversion FROM ProductMaster  inner join VersionInfoMaster on ProductMaster.ProductId=VersionInfoMaster.ProductId inner join ProductDetail on ProductDetail.VersionNo=VersionInfoMaster.VersionInfoName    where ProductDetail.Active ='True' order  by productversion";
        string strcln = "SELECT CompanyMaster.CompanyId, CompanyMaster.CompanyName, CompanyMaster.ContactPerson, CompanyMaster.CompanyWebsite, CompanyMaster.date, CompanyMaster.PlanId, CompanyMaster.Address, CompanyMaster.Email, CompanyMaster.pincode, CompanyMaster.phone, CompanyMaster.CompanyLoginId, CompanyMaster.active, CompanyMaster.PricePlanId, CompanyMaster.ProductId,  CompanyMaster.MobileNo, PricePlanMaster.PricePlanName, CompanyMaster.OrderId FROM     CompanyMaster INNER JOIN       OrderMaster ON CompanyMaster.OrderId = OrderMaster.OrderId INNER JOIN PricePlanMaster ON CompanyMaster.PricePlanId = PricePlanMaster.PricePlanId Where OrderMaster.Status=0";
        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);
        Ddl_orderdetail.DataSource = dtcln;
        Ddl_orderdetail.DataValueField = "OrderId";
        Ddl_orderdetail.DataTextField = "OrderId";
        Ddl_orderdetail.DataBind();
        Ddl_orderdetail.Items.Insert(0, "-Select-");
        Ddl_orderdetail.Items[0].Value = "0";
    }

    private void Fill_compDetail()
    {
        string strcomp="SELECT CompanyMaster.CompanyId, CompanyMaster.CompanyName, CompanyMaster.ContactPerson, CompanyMaster.CompanyWebsite, CompanyMaster.date, CompanyMaster.PlanId, CompanyMaster.Address, CompanyMaster.Email, CompanyMaster.pincode, CompanyMaster.phone, CompanyMaster.CompanyLoginId, CompanyMaster.active, CompanyMaster.PricePlanId, CompanyMaster.ProductId, CompanyMaster.city, CompanyMaster.MobileNo, PricePlanMaster.PricePlanName, ProductMaster.ProductName, PortalMasterTbl.PortalName, CompanyMaster.OrderId FROM     CompanyMaster INNER JOIN  ProductMaster ON CompanyMaster.ProductId = ProductMaster.ProductId INNER JOIN  PortalMasterTbl ON ProductMaster.ProductId = PortalMasterTbl.ProductId INNER JOIN OrderMaster ON CompanyMaster.OrderId = OrderMaster.OrderId INNER JOIN PricePlanMaster ON CompanyMaster.PricePlanId = PricePlanMaster.PricePlanId where CompanyMaster.OrderId=" + Ddl_orderdetail.SelectedValue + " ";
        SqlCommand cmdcln = new SqlCommand(strcomp, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);
}
    protected void fillcompany()
    {
        string strcln = "SELECT CompanyMaster.CompanyId, CompanyMaster.CompanyName, CompanyMaster.ContactPerson, CompanyMaster.CompanyWebsite, CompanyMaster.date, CompanyMaster.PlanId, CompanyMaster.Address, CompanyMaster.Email, CompanyMaster.pincode, CompanyMaster.phone, CompanyMaster.CompanyLoginId, CompanyMaster.active, CompanyMaster.PricePlanId, CompanyMaster.ProductId, CompanyMaster.city, CompanyMaster.MobileNo, PricePlanMaster.PricePlanName, ProductMaster.ProductName, PortalMasterTbl.PortalName, CompanyMaster.OrderId FROM     CompanyMaster INNER JOIN  ProductMaster ON CompanyMaster.ProductId = ProductMaster.ProductId INNER JOIN  PortalMasterTbl ON ProductMaster.ProductId = PortalMasterTbl.ProductId INNER JOIN OrderMaster ON CompanyMaster.OrderId = OrderMaster.OrderId INNER JOIN PricePlanMaster ON CompanyMaster.PricePlanId = PricePlanMaster.PricePlanIdok   where ProductDetail.Active ='True'  order by productversion";
        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);
        ddlcomname.DataSource = dtcln;
        ddlcomname.DataValueField = "CompanyId";
        ddlcomname.DataTextField = "productversion";
        ddlcomname.DataBind();
        ddlcomname.Items.Insert(0, "-Select-");
        ddlcomname.Items[0].Value = "0";
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {
        string str = "";

        try
        {
            int counter = 0;
            string stpid = "select distinct Clientpriceplanorderdetailtbl.ClientPricePlanID  from clientpriceplanordermastertbl inner join Clientpriceplanorderdetailtbl on clientpriceplanordermastertbl.ID=Clientpriceplanorderdetailtbl.ClientpriceplanordermasterID   INNER JOIN ClientPricePlanMaster on ClientPricePlanMaster.ClientPricePlanId=Clientpriceplanorderdetailtbl.ClientPricePlanID  where ClientMasterID='" + ddlcname.SelectedValue + "'";

            SqlCommand cmdpid = new SqlCommand(stpid, con);
            DataTable dtpid = new DataTable();
            SqlDataAdapter adpid = new SqlDataAdapter(cmdpid);
            adpid.Fill(dtpid);
            for (int i = 0; i <= dtpid.Rows.Count - 1; i++)
            {
                string stpid1 = "Select noofbusiwizcontroller from ClientPricePlanMaster where ClientPricePlanId='" + dtpid.Rows[i]["ClientPricePlanID"].ToString() + "'";
                SqlCommand cmdpid1 = new SqlCommand(stpid1, con);
                DataTable dtpid1 = new DataTable();
                SqlDataAdapter adpid1 = new SqlDataAdapter(cmdpid1);
                adpid1.Fill(dtpid1);
                if (dtpid1.Rows.Count > 0)
                {
                    if (dtpid1.Rows[0]["noofbusiwizcontroller"].ToString() != "")
                    {
                        counter = counter + Convert.ToInt32(dtpid1.Rows[0]["noofbusiwizcontroller"]);
                    }

                }

            }
            string strcln = "Select  COUNT(ClinetMasterID) from BusiControllerMasterTBl where ClinetMasterID='" + ddlcname.SelectedValue + "'";
            SqlCommand cmdcln = new SqlCommand(strcln, con);
            con.Open();
            int ctotal = Convert.ToInt32(cmdcln.ExecuteScalar());
            con.Close();
            if (ctotal > counter)
            {
                lblmsg.Text = "You have reached maximum number of Busiwiz Controllers allowed by all he plans you have subscribed. Kindly buy  new service to allow further setup of busiwiz controller.";
            }
            else
            {

                if (btnsubmit.Text == "Update")
                {
                    str = "Update BusiControllerMasterTBl Set ClinetMasterID='" + ddlcname.SelectedValue + "',ClientName='" + ddlcname.SelectedItem.Text + "',BusiControllerApplicationURL='" + txtbasicurl.Text + "',DatabaseServerNameOrIp='" + txtdatabaseserverip.Text + "',Port='" + txtPort.Text + "',UserID='" + txtUserID.Text + "',Password='" + txtpassword.Text + "',Active='" + chkactive.Checked + "',DatabaseName='" + txtdtname.Text + "',versioninfomasterid='" + Ddl_orderdetail.SelectedValue + "',compnaymasterid='" + ddlcomname.SelectedValue + "' where ID='" + ViewState["ID"] + "'";

                    lblmsg.Text = "Busiwiz Controller Updated Successfully.";
                }
                else
                {
                    if (ddlcname.SelectedItem.Text != "-Select-")
                    {
                        str = "INSERT INTO BusiControllerMasterTBl " +
                              "(ClinetMasterID,ClientName,BusiControllerApplicationURL,DatabaseServerNameOrIp,Port,UserID,Password,Active,DatabaseName,versioninfomasterid,compnaymasterid)" +
                              "VALUES('" + ddlcname.SelectedValue + "','" + ddlcname.SelectedItem.Text + "','" + txtbasicurl.Text + "','" + txtdatabaseserverip.Text + "','" + txtPort.Text + "','" + txtUserID.Text + "','" + txtpassword.Text + "','" + chkactive.Checked + "','" + txtdtname.Text + "','" + Ddl_orderdetail.SelectedValue + "','" + ddlcomname.SelectedValue + "')";
                        lblmsg.Text = "Busiwiz Controller insert Successfully.";
                    }
                }
            }
            SqlCommand cmd = new SqlCommand(str, con);
            DataTable dt = new DataTable();
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            fillgrid();
            btnsubmit.Text = "Submit";

            clindata();

        }
        catch
        {
        }
    }
    protected void fillgrid()
    {
        try
        {
            GridView1.DataSource = null;


            string strcln = "Select * from BusiControllerMasterTBl order by ID";
            SqlCommand cmdcln = new SqlCommand(strcln, con);
            DataTable dtcln = new DataTable();
            SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
            adpcln.Fill(dtcln);
            GridView1.DataSource = dtcln;

            GridView1.DataBind();
        }
        catch
        {
        }
    }
    protected void clindata()
    {
        txtbasicurl.Text = "";
        txtdatabaseserverip.Text = "";
        txtPort.Text = "";
        txtUserID.Text = "";
        txtpassword.Text = "";
        txtdtname.Text = "";
        ddlcname.SelectedIndex = 0;
        chkactive.Checked = false;
        ddlcomname.SelectedIndex = 0;
        Ddl_orderdetail.SelectedIndex = 0;
        txtpassword.Attributes.Clear();
    }

    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {

            if (e.CommandName == "edit1")
            {
                ViewState["ID"] = "";
                GridView1.SelectedIndex = Convert.ToInt32(e.CommandArgument);
                int i = Convert.ToInt32(GridView1.DataKeys[GridView1.SelectedIndex].Value.ToString());
                ViewState["ID"] = i.ToString();
                string strcln =
                hdnProductDetailId.Value = i.ToString();
                strcln = "Select * from BusiControllerMasterTBl where ID='" + i.ToString() + "'";
                SqlCommand cmdcln = new SqlCommand(strcln, con);
                DataTable dtcln = new DataTable();
                SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
                adpcln.Fill(dtcln);
                if (dtcln.Rows.Count > 0)
                {

                    txtbasicurl.Text = dtcln.Rows[0]["BusiControllerApplicationURL"].ToString();
                    txtdatabaseserverip.Text = dtcln.Rows[0]["DatabaseServerNameOrIp"].ToString();
                    txtPort.Text = dtcln.Rows[0]["Port"].ToString();
                    txtUserID.Text = dtcln.Rows[0]["UserID"].ToString();

                    txtdtname.Text = dtcln.Rows[0]["DatabaseName"].ToString();
                    chkactive.Checked = Convert.ToBoolean(dtcln.Rows[0]["Active"].ToString());
                    ddlcname.SelectedIndex = ddlcname.Items.IndexOf(ddlcname.Items.FindByValue(dtcln.Rows[0]["ClinetMasterID"].ToString()));
                    ddlcomname.SelectedIndex = ddlcomname.Items.IndexOf(ddlcomname.Items.FindByValue(dtcln.Rows[0]["compnaymasterid"].ToString()));
                    Ddl_orderdetail.SelectedIndex = Ddl_orderdetail.Items.IndexOf(Ddl_orderdetail.Items.FindByValue(dtcln.Rows[0]["versioninfomasterid"].ToString()));
                    string pass = dtcln.Rows[0]["Password"].ToString();

                    txtpassword.Attributes.Add("Value", pass);

                    btnsubmit.Text = "Update";
                }
            }
        }
        catch
        {
        }
    }
    protected void Ddl_orderdetail_SelectedIndexChanged(object sender, EventArgs e)
    {
        Fill_compDetail();
    }
}

