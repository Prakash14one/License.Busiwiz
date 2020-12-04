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
public partial class ClientPricePlanDetail : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        lblmsg.Text = "";
        if (!Page.IsPostBack)
        {
            if (Request.QueryString["pid"] != null)
            {
                Panel1.Visible = false;
                btnSubmit.Visible = false;
                filldata();
            }
            fillservicetype();
            FillGrid();

            
        }
    }
    protected void filldata()
    {
        string strc1 = " select * from  ClientPricePlanMaster where ClientPricePlanId='" +Request.QueryString["pid"]   + "'";
                SqlCommand cmdc1 = new SqlCommand(strc1, con);
                DataTable dtc1 = new DataTable();
                SqlDataAdapter adpc1 = new SqlDataAdapter(cmdc1);
                adpc1.Fill(dtc1);
                if (dtc1.Rows.Count > 0)
                {
                    txtPlanName.Text = dtc1.Rows[0]["PricePlanName"].ToString();
                    txtPlanDesc.Text = dtc1.Rows[0]["PricePlanDesc"].ToString();
                    chkboxActiveDeactive.Checked = Convert.ToBoolean(dtc1.Rows[0]["Active"].ToString());
                    txtStartdate.Text = dtc1.Rows[0]["StartDate"].ToString();
                    txtEndDate.Text = dtc1.Rows[0]["EndDate"].ToString();
                   
                    txtPlanAmount.Text = dtc1.Rows[0]["pricePlanAmount"].ToString();
                    txtqty.Text = dtc1.Rows[0]["PlanQTY"].ToString();
                    txt1name.Text = dtc1.Rows[0]["tax1name"].ToString();
                    txt1perc.Text = dtc1.Rows[0]["tax1perc"].ToString();
                    txt2name.Text = dtc1.Rows[0]["tax2name"].ToString();
                    txt2perc.Text = dtc1.Rows[0]["tax2perc"].ToString();
                    ddlservicestype.SelectedIndex = ddlservicestype.Items.IndexOf(ddlservicestype.Items.FindByValue(dtc1.Rows[0]["ServiceTypeId"].ToString()));
                    txtlpe.Text = dtc1.Rows[0]["ServiceLicensePeriod"].ToString();
                    txtbscontroller.Text = dtc1.Rows[0]["noofbusiwizcontroller"].ToString();
                }
    }
    protected void fillservicetype()
    {
        string strcln = " SELECT * FROM ServiceType order by servicetype";


        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);
       ddlservicestype.DataSource = dtcln;
       
       ddlservicestype.DataValueField = "id";
       ddlservicestype.DataTextField = "ServiceType";
      
   
       ddlservicestype.DataBind();
       ddlservicestype.Items.Insert(0, "-Select-");
       ddlservicestype.SelectedIndex = 0;
    }
    protected void FillGrid()
    {
        string strcln = " SELECT     ClientPricePlanMaster.* " +
" FROM         ClientPricePlanMaster ";                  
                       

        SqlCommand cmdcln = new SqlCommand(strcln, con);
        DataTable dtcln = new DataTable();
        SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
        adpcln.Fill(dtcln);
        GridView1.DataSource = dtcln;
        GridView1.DataBind();
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (btnSubmit.Text == "Update")
        {

            string str = "update   ClientPricePlanMaster set  PricePlanName='" + txtPlanName.Text + "' ,PricePlanDesc='" + txtPlanDesc.Text + "', Active = '" + chkboxActiveDeactive.Checked.ToString() + "' ,StartDate='" + txtStartdate.Text + "',EndDate='" + txtEndDate.Text + "', pricePlanAmount='" + txtPlanAmount.Text + "',PlanQTY='" + txtqty.Text + "',tax1name='" + txt1name.Text + "',tax1perc='" + txt1perc.Text + "',tax2name='" + txt2name.Text + "',tax2perc='" + txt2perc.Text + "',ServiceTypeId='" + ddlservicestype.SelectedValue + "',ServiceLicensePeriod='" + txtlpe.Text + "',noofbusiwizcontroller='"+txtbscontroller.Text+"'  where ClientPricePlanId= '" + hdnPricePlanId.Value.ToString() + "'";

            SqlCommand cmd = new SqlCommand(str, con);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            lblmsg.Visible = true;
            lblmsg.Text = "Record updated sucessfully";
            FillGrid();
            ClearAll();
        }
        else
        {

            try
            {
                string str = "INSERT INTO ClientPricePlanMaster(PricePlanName,PricePlanDesc, Active ,StartDate,EndDate, pricePlanAmount,PlanQTY,tax1name,tax1perc,tax2name,tax2perc,ServiceTypeId,ServiceLicensePeriod,noofbusiwizcontroller) " +
                             "VALUES('" + txtPlanName.Text + "','" + txtPlanDesc.Text + "',1,'" + txtStartdate.Text + "','" + txtEndDate.Text + "','" + txtPlanAmount.Text + "','" + txtqty.Text + "','" + txt1name.Text + "','" + txt1perc.Text + "','" + txt2name.Text + "','" + txt2perc.Text + "','"+ ddlservicestype.SelectedValue +"','"+ txtlpe.Text+"','"+txtbscontroller.Text+"')";
                SqlCommand cmd = new SqlCommand(str, con);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
                lblmsg.Visible = true;
                lblmsg.Text = "Record Inserted sucessfully";
                FillGrid();
                ClearAll();
            }
            catch (Exception eerr)
            {
                lblmsg.Visible = true;
                lblmsg.Text = "Error : " + eerr.Message;
            }
        }
    }
    protected void ClearAll()
    {
        txtEndDate.Text = "";
        txtPlanAmount.Text = "";
        txtPlanDesc.Text = "";
        txtPlanName.Text = "";
        txtStartdate.Text = "";
        txtqty.Text = "";
        txt1name.Text = "";
        txt1perc.Text = "";
        txt2name.Text = "";
        txt2perc.Text = "";
        ddlservicestype.SelectedIndex = 0;
        txtlpe.Text = "";
        txtbscontroller.Text = "";
        btnSubmit.Text = "Submit";
     //   ddlProductname.SelectedIndex = 0;
    }
    protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "edit1")
        {
            GridView1.SelectedIndex = Convert.ToInt32(e.CommandArgument);
            int i = Convert.ToInt32(GridView1.SelectedDataKey.Value);
            string strcln = " SELECT * from  ClientPricePlanMaster  where ClientPricePlanId= " + i.ToString();

            SqlCommand cmdcln = new SqlCommand(strcln, con);
            DataTable dtcln = new DataTable();
            SqlDataAdapter adpcln = new SqlDataAdapter(cmdcln);
            adpcln.Fill(dtcln);
            if (dtcln.Rows.Count > 0)
            {
                hdnPricePlanId.Value = i.ToString();
                txtPlanName.Text = dtcln.Rows[0]["PricePlanName"].ToString();
                txtPlanDesc.Text = dtcln.Rows[0]["PricePlanDesc"].ToString();
                chkboxActiveDeactive.Checked = Convert.ToBoolean(dtcln.Rows[0]["Active"].ToString());
                txtStartdate.Text = dtcln.Rows[0]["StartDate"].ToString();
                txtEndDate.Text = dtcln.Rows[0]["EndDate"].ToString();
             //   ddlProductname.SelectedIndex = ddlProductname.Items.IndexOf(ddlProductname.Items.FindByValue(dtcln.Rows[0]["ProductId"].ToString()));
                txtPlanAmount.Text = dtcln.Rows[0]["pricePlanAmount"].ToString();
                txtqty.Text = dtcln.Rows[0]["PlanQTY"].ToString();
                txt1name.Text = dtcln.Rows[0]["tax1name"].ToString();
                txt1perc.Text = dtcln.Rows[0]["tax1perc"].ToString();
                txt2name.Text = dtcln.Rows[0]["tax2name"].ToString();
                txt2perc.Text = dtcln.Rows[0]["tax2perc"].ToString();
                ddlservicestype.SelectedIndex = ddlservicestype.Items.IndexOf(ddlservicestype.Items.FindByValue(dtcln.Rows[0]["ServiceTypeId"].ToString()));
                txtlpe.Text = dtcln.Rows[0]["ServiceLicensePeriod"].ToString();
                btnSubmit.Text = "Update";

            }
            GridView1.DataSource = dtcln;
            GridView1.DataBind();
            //  Response.Redirect("CustomerEdit.aspx?CompanyId=" + i + "");
        }
    }
}
