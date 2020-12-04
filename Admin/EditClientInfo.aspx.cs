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

public partial class EditClientInfo : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            fillplan();
            FillddlCountry();
            if (Request.QueryString["ClientId"] != null)
            {
                FillInfo();
                btnSubmit.Visible = false;
                btnUpdate.Visible = true;
            }
            else
            {
                btnUpdate.Visible = false;
                btnSubmit.Visible = true;
            }
        }
    }
    protected void  fillplan()
    {
        string str = " SELECT  * from ClientPricePlanMaster ";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);
        ddlsubscriptionplan.DataSource = ds;
         
         ddlsubscriptionplan.DataBind();
         ddlsubscriptionplan.Items.Insert(0, "-Select-");
         ddlsubscriptionplan.Items[0].Value = "0";
    }
    protected void FillInfo()
    {
        string str = " SELECT     ClientMaster.ClientMasterId, ClientMaster.CompanyName, ClientMaster.Address1, ClientMaster.Address2, ClientMaster.CountryId, ClientMaster.StateId, " +
                     " ClientMaster.City, ClientMaster.Zipcode, ClientMaster.ContactPersonName, ClientMaster.Fax1, ClientMaster.Fax2, ClientMaster.Email1, ClientMaster.Email2, " +
                     " ClientMaster.Phone1, ClientMaster.Phone2, ClientMaster.ClientURL, ClientMaster.CustomerSupportURL, ClientMaster.SalesCustomerSupportURL, " +
                     " ClientMaster.SalesPhoneNo, ClientMaster.SalesFaxNo, ClientMaster.SalesEmail, ClientMaster.AfterSalesSupportPhoneNo, ClientMaster.AfterSalesSupportFaxNo, " +
                     " ClientMaster.AfterSalesSupportEmail, ClientMaster.TechSupportPhoneNo, ClientMaster.TechSupportFaxNo, ClientMaster.TechSupportEmail, ClientMaster.FTP, " +
                     " ClientMaster.FTPUserName, ClientMaster.FTPPassword, ClientMaster.LoginName, ClientMaster.LoginPassword, CountryMaster.CountryId AS Expr1, " +
                     " CountryMaster.CountryName, CountryMaster.Country_Code, StateMasterTbl.StateId AS Expr2, StateMasterTbl.StateName, StateMasterTbl.CountryId AS Expr3, " +
                     " StateMasterTbl.State_Code , ClientMaster.ClientPricePlanId FROM         StateMasterTbl INNER JOIN " +
                      " CountryMaster ON StateMasterTbl.CountryId = CountryMaster.CountryId RIGHT OUTER JOIN " +
                      " ClientMaster ON StateMasterTbl.StateId = ClientMaster.StateId AND CountryMaster.CountryId = ClientMaster.CountryId " +
                             " WHERE (ClientMaster.ClientMasterId = '" + Convert.ToInt32(Request.QueryString["ClientId"].ToString()) + "')";
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);
        if (ds.Tables[0].Rows.Count > 0)
        {
            txtCompanyName.Text = ds.Tables[0].Rows[0]["CompanyName"].ToString();
            txtAdrs1.Text = ds.Tables[0].Rows[0]["Address1"].ToString();
            txtAdrs2.Text = ds.Tables[0].Rows[0]["Address2"].ToString();
ddlCountry.SelectedIndex = ddlCountry.Items.IndexOf(ddlCountry.Items.FindByValue( ds.Tables[0].Rows[0]["CountryId"].ToString()) );
FillddlState();
ddlState.SelectedIndex = ddlState.Items.IndexOf(ddlState.Items.FindByValue(ds.Tables[0].Rows[0]["StateId"].ToString()));
            //txtCountry.Text = ds.Tables[0].Rows[0]["CountryName"].ToString();

            //txtState.Text = ds.Tables[0].Rows[0]["StateName"].ToString();

            txtCity.Text = ds.Tables[0].Rows[0]["City"].ToString();
            txtZipcode.Text = ds.Tables[0].Rows[0]["Zipcode"].ToString();
            txtContactPerson.Text = ds.Tables[0].Rows[0]["ContactPersonName"].ToString();
            txtFax1.Text = ds.Tables[0].Rows[0]["Fax1"].ToString();
            txtFax2.Text = ds.Tables[0].Rows[0]["Fax2"].ToString();
            txtEmail1.Text = ds.Tables[0].Rows[0]["Email1"].ToString();
            txtEmail2.Text = ds.Tables[0].Rows[0]["Email2"].ToString();
            txtPhone1.Text = ds.Tables[0].Rows[0]["Phone1"].ToString();
            txtPhone2.Text = ds.Tables[0].Rows[0]["Phone2"].ToString();

            txtClientUrl.Text = ds.Tables[0].Rows[0]["ClientURL"].ToString();
            txtCustSupportURL.Text = ds.Tables[0].Rows[0]["CustomerSupportURL"].ToString();
            txtSalesSupportURL.Text = ds.Tables[0].Rows[0]["SalesCustomerSupportURL"].ToString();
            txtSalesPhoneNO.Text = ds.Tables[0].Rows[0]["SalesPhoneNo"].ToString();
            txtSalesFaxNo.Text = ds.Tables[0].Rows[0]["SalesFaxNo"].ToString();
            txtSalesEmail.Text = ds.Tables[0].Rows[0]["SalesEmail"].ToString();
            txtSalesPhoneNO.Text = ds.Tables[0].Rows[0]["SalesPhoneNo"].ToString();
            txtafterSalesSupPhoneNO.Text = ds.Tables[0].Rows[0]["AfterSalesSupportPhoneNo"].ToString();
            txtAfterSalesSupFaxNo.Text = ds.Tables[0].Rows[0]["AfterSalesSupportFaxNo"].ToString();
            txtAfterSalesSupFaxNo.Text = ds.Tables[0].Rows[0]["AfterSalesSupportFaxNo"].ToString();
            txtAfterSalesSupFaxNo.Text = ds.Tables[0].Rows[0]["AfterSalesSupportFaxNo"].ToString();
            txtAfterSalesSupEmail.Text = ds.Tables[0].Rows[0]["AfterSalesSupportEmail"].ToString();
            txtTechSupportPhoneNo.Text = ds.Tables[0].Rows[0]["TechSupportPhoneNo"].ToString();
            txtTechSupportFaxNo.Text = ds.Tables[0].Rows[0]["TechSupportFaxNo"].ToString();
            txtTechSupEmail.Text = ds.Tables[0].Rows[0]["TechSupportEmail"].ToString();
            txtFTP.Text = ds.Tables[0].Rows[0]["FTP"].ToString();
            txtFTPUserName.Text = ds.Tables[0].Rows[0]["FTPUserName"].ToString();
            txtFTPPassword.Text = ds.Tables[0].Rows[0]["FTPPassword"].ToString();
            txtLoginName.Text = ds.Tables[0].Rows[0]["LoginName"].ToString();
            txtLoginPassword.Text = ds.Tables[0].Rows[0]["LoginPassword"].ToString();

            ddlsubscriptionplan.SelectedIndex = ddlsubscriptionplan.Items.IndexOf(ddlsubscriptionplan.Items.FindByValue(ds.Tables[0].Rows[0]["ClientPricePlanId"].ToString()));

        }
    }
    protected void FillddlCountry()
    {
        string strcountry = "SELECT CountryId,CountryName,Country_Code FROM CountryMaster";
        SqlCommand cmdcountry = new SqlCommand(strcountry, con);
        DataTable dtcountry = new DataTable();
        SqlDataAdapter adpcountry = new SqlDataAdapter(cmdcountry);
        adpcountry.Fill(dtcountry);
        ddlCountry.DataSource = dtcountry;  
        ddlCountry.DataTextField = "CountryName";
        ddlCountry.DataValueField = "CountryId";
        ddlCountry.DataBind();
        ddlCountry.Items.Insert(0, "-Select-");
        ddlCountry.Items[0].Value = "0";
      }
    protected void FillddlState()
    {
        string strstate = "SELECT StateId,StateName,CountryId,State_Code  FROM StateMasterTbl where CountryId ="+Convert.ToInt32(ddlCountry.SelectedValue)+" ";
        SqlCommand cmdstate= new SqlCommand(strstate, con);
        DataTable dtstate = new DataTable();
        SqlDataAdapter adpstate = new SqlDataAdapter(cmdstate);
        adpstate.Fill(dtstate);
        ddlState.DataSource = dtstate;       
        ddlState.DataTextField = "StateName";
        ddlState.DataValueField = "StateId";
        ddlState.DataBind();
        ddlState.Items.Insert(0, "-Select-");
        ddlState.Items[0].Value = "0";
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            string str = "INSERT INTO ClientMaster " +
               "(CompanyName,Address1,Address2,CountryId,StateId,City,Zipcode,ContactPersonName,Fax1,Fax2,Email1,Email2,Phone1,Phone2 ," +
               " ClientURL,CustomerSupportURL,SalesCustomerSupportURL,SalesPhoneNo,SalesFaxNo,SalesEmail,AfterSalesSupportPhoneNo,AfterSalesSupportFaxNo,AfterSalesSupportEmail " +
               ",TechSupportPhoneNo,TechSupportFaxNo,TechSupportEmail,FTP,FTPUserName,FTPPassword, LoginName,LoginPassword , ClientPricePlanId)" +
               "VALUES('" + txtCompanyName.Text + "','" + txtAdrs1.Text + "','" + txtAdrs2.Text + "'," + Convert.ToInt32(ddlCountry.SelectedValue) + "," + Convert.ToInt32(ddlState.SelectedValue) + ",'" + txtCity.Text + "','" + txtZipcode.Text + "','" + txtContactPerson.Text + "','" + txtFax1.Text + "','" + txtFax2.Text + "','" + txtEmail1.Text + "','" + txtEmail2.Text + "','" + txtPhone1.Text + "','" + txtPhone2.Text + "'," +
               "'" + txtClientUrl.Text + "','" + txtCustSupportURL.Text + "','" + txtSalesSupportURL.Text + "','" + txtSalesPhoneNO.Text + "','" + txtSalesFaxNo.Text + "' , '" + txtSalesEmail.Text + "', '" + txtafterSalesSupPhoneNO.Text + "' ,'" + txtAfterSalesSupFaxNo.Text + "', '" + txtAfterSalesSupEmail.Text + "'," +
               "'" + txtTechSupportPhoneNo.Text + "' , '" + txtTechSupportFaxNo.Text + "','" + txtTechSupEmail.Text + "','" + txtFTP.Text + "','" + txtFTPUserName.Text + "','" + txtFTPPassword.Text + "','" +  txtLoginName.Text  + "','" + txtLoginPassword.Text + "','"+ ddlsubscriptionplan.SelectedItem.Value.ToString() +"')";
            SqlCommand cmd = new SqlCommand(str, con);
            DataTable dt = new DataTable();
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            lblmsg.Visible = true;
            lblmsg.Text = "Client is created Successfully.";
            ClearAll();
            str = "SELECT   top 1 ClientMasterId from ClientMaster  order by ClientMasterId desc";
          cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);
            if(ds.Tables[0].Rows.Count > 0)
            {
               Response.Redirect("OrderClientInfo.aspx?ClientId="+ ds.Tables[0].Rows[0]["ClientmasterId"].ToString() ) ;
            }
        }
        catch (Exception err)
        {
            lblmsg.Visible = true;
            lblmsg.Text = "Error ;" +err.Message ;
        }
    }
    protected void ClearAll()
    {
        txtCompanyName.Text = "";
        txtAdrs1.Text = "";
        txtAdrs2.Text = " ";
        ddlCountry.SelectedIndex = 0; 
        //ddlState.SelectedIndex = 0 ;
        ddlState.DataSource = null;
        ddlState.DataBind();
        txtCity.Text = "";
        txtZipcode.Text = "";
        txtContactPerson.Text = "";
        txtFax1.Text = "";
        txtFax2.Text = "";
        txtEmail1.Text = "";
        txtEmail2.Text = "";
        txtPhone1.Text = "";
        txtPhone2.Text = "";
        txtClientUrl.Text = "";
        txtCustSupportURL.Text  = "" ;
        txtSalesSupportURL.Text = "";
        txtSalesPhoneNO.Text = "";
        txtSalesFaxNo.Text = "";
        txtSalesEmail.Text = "";
        txtafterSalesSupPhoneNO.Text = "";
        txtAfterSalesSupFaxNo.Text = "";
        txtAfterSalesSupEmail.Text = "";
        txtTechSupportPhoneNo.Text = "";
        txtTechSupportFaxNo.Text = "";
        txtTechSupEmail.Text = "";
        txtFTP.Text = "";
        txtFTPUserName.Text = "";
        txtFTPPassword.Text ="";                          
    }
    protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillddlState();
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        try
        {
            string str = "update   ClientMaster  set " +
               "CompanyName='" + txtCompanyName.Text + "',Address1='" + txtAdrs1.Text + "',Address2 ='" + txtAdrs2.Text + "', CountryId=" + Convert.ToInt32(ddlCountry.SelectedValue) + ",StateId=" + Convert.ToInt32(ddlState.SelectedValue) + ",City ='" + txtCity.Text + "' ,Zipcode='" + txtZipcode.Text + "',ContactPersonName ='" + txtContactPerson.Text + "',Fax1='" + txtFax1.Text + "',Fax2='" + txtFax2.Text + "',Email1='" + txtEmail1.Text + "',Email2='" + txtEmail2.Text + "',Phone1='" + txtPhone1.Text + "',Phone2='" + txtPhone2.Text + "' ," +
               " ClientURL='" + txtClientUrl.Text + "',CustomerSupportURL ='" + txtCustSupportURL.Text + "',SalesCustomerSupportURL ='" + txtSalesSupportURL.Text + "' ,SalesPhoneNo ='" + txtSalesPhoneNO.Text + "', SalesFaxNo ='" + txtSalesFaxNo.Text + "'  ,SalesEmail ='" + txtSalesEmail.Text + "', AfterSalesSupportPhoneNo ='" + txtafterSalesSupPhoneNO.Text + "' ,AfterSalesSupportFaxNo ='" + txtAfterSalesSupFaxNo.Text + "',AfterSalesSupportEmail ='" + txtAfterSalesSupEmail.Text + "'  " +
               ",TechSupportPhoneNo='" + txtTechSupportPhoneNo.Text + "',TechSupportFaxNo='" + txtTechSupportFaxNo.Text + "',TechSupportEmail ='" + txtTechSupEmail.Text + "',FTP='" + txtFTP.Text + "',FTPUserName='" + txtFTPUserName.Text + "',FTPPassword='" + txtFTPPassword.Text + "', LoginName='" + txtLoginName.Text + "',LoginPassword ='" + txtLoginPassword.Text + "' ,ClientPricePlanId= '" + ddlsubscriptionplan.SelectedItem.Value.ToString() + "'" +
               "where ClientMasterId =" + Request.QueryString["ClientId"].ToString();
            SqlCommand cmd = new SqlCommand(str, con);
            DataTable dt = new DataTable();
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();
            lblmsg.Visible = true;
            lblmsg.Text = "Client is Updated Successfully.";        
            Response.Redirect("ClientList.aspx");            
        }
        catch (Exception err)
        {
            lblmsg.Visible = true;
            lblmsg.Text = "Error ;" + err.Message;
        }
    }
    protected void ddlsubscriptionplan_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlsubscriptionplan.SelectedIndex > 0)
        {
            string str = "SELECT *  FROM  ClientPricePlanMaster " +
                         " WHERE (ClientPricePlanId = '" + Convert.ToInt32(ddlsubscriptionplan.SelectedItem.Value.ToString()) + "')";
            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            adp.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                lblamt.Text = ds.Tables[0].Rows[0]["PricePlanAmount"].ToString();
            }
        }
        else
        {
            lblamt.Text = "";
        }
    }
}
