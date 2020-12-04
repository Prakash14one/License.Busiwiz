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

public partial class MakePayment : System.Web.UI.Page
{
    protected string amount = "";
    protected string orderno = "";
  //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ifilecabinateConnectionString"].ConnectionString);
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["LicenseBusiwizConnectionString"].ConnectionString);
 
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            int orderid = Convert.ToInt32(Request.QueryString["id"]);
            //int orderid = 8;
            orderno = orderid.ToString();
            filldata(orderid);
            string str = "  select * from  OrderPaymentSatus where  (OrderId='" + orderid + "') ";
            SqlCommand cmd = new SqlCommand(str, con);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adp.Fill(dt);
            if (dt.Rows.Count > 0)
            {
            }
            else
            {
                insertpayment(orderid);
            }
        }
        lblmsg.Visible = false;
    }
    public void filldata(int orderid)
    {
        //string str="SELECT     OrderMaster.OrderId, OrderMaster.CompanyName, OrderMaster.ContactPerson, OrderMaster.ContactPersonDesignation, OrderMaster.Address, OrderMaster.phone,  "+
        //              " OrderMaster.Email, OrderMaster.PlanId, OrderMaster.fax, OrderMaster.CompanyId, PricePlanMaster.PlanName, OrderMaster.AdminId, OrderMaster.Domain, "+
        //              " PricePlanMaster.PricePerMonth "+
        //                " FROM         OrderMaster INNER JOIN "+
        //              " PricePlanMaster ON OrderMaster.PlanId = PricePlanMaster.PlanId "+
        //            " WHERE     (OrderMaster.OrderId = '" + orderid + "')";
//        string str = "SELECT     OrderMaster.*, PricePlanView.*" + 
//" FROM         OrderMaster LEFT OUTER JOIN " + 
//                      " PricePlanView ON OrderMaster.PlanId = PricePlanView.PricePlanDetailId " +
//                       " WHERE     (OrderMaster.OrderId = '" + orderid + "')";
//        string str = "SELECT     OrderMaster.OrderId, OrderMaster.CompanyName, OrderMaster.ContactPerson, OrderMaster.ContactPersonDesignation, OrderMaster.Address, OrderMaster.Email, " +
//                      " OrderMaster.phone, OrderMaster.fax, OrderMaster.PlanId, OrderMaster.AdminId, OrderMaster.Password, OrderMaster.Domain, OrderMaster.HostId, " +
//                      " PricePlanView.PricePlanDetailId, PricePlanView.PricePlanId, PricePlanView.PricePlanTypeId, PricePlanView.Amount, PricePlanView.PlanTypeName, " +
//                      " PricePlanView.PricePlanName, PricePlanView.PlanName, PricePlanView.PlanName1, CompanyMaster.CompanyId, PricePlanView.PlanDetail " +
//", CompanyMaster.CompanyLoginId FROM         OrderMaster LEFT OUTER JOIN " +
//                      " PricePlanView ON OrderMaster.PlanId = PricePlanView.PricePlanDetailId FULL OUTER JOIN" +
//                      " CompanyMaster ON OrderMaster.OrderId = CompanyMaster.OrderId " +
//                       " WHERE     (OrderMaster.OrderId = '" + orderid + "')";
string str = "SELECT     OrderMaster.OrderId, OrderMaster.CompanyName, OrderMaster.ContactPerson, OrderMaster.ContactPersonDesignation, OrderMaster.Address, OrderMaster.Email, " + 
                      " OrderMaster.phone, OrderMaster.fax, OrderMaster.PlanId, OrderMaster.AdminId, OrderMaster.Password, OrderMaster.Domain, OrderMaster.HostId, " + 
                      " CompanyMaster.CompanyId, OrderMaster.CompanyLoginId, PricePlanMaster.* " + 
" FROM         CompanyMaster LEFT OUTER JOIN " + 
                      " PricePlanMaster ON CompanyMaster.PricePlanId = PricePlanMaster.PricePlanId RIGHT OUTER JOIN " + 
                      " OrderMaster ON CompanyMaster.OrderId = OrderMaster.OrderId " + 
                        " WHERE     (OrderMaster.OrderId = '" + orderid + "')";
         
       
        SqlCommand cmd = new SqlCommand(str, con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        lbladdress.Text = dt.Rows[0]["Address"].ToString();
        lblAdminUID.Text = dt.Rows[0]["AdminId"].ToString();
        lblamt.Text = dt.Rows[0]["PricePlanAmount"].ToString() +"$";
        lblcompanyname.Text = dt.Rows[0]["CompanyName"].ToString();
        lblCompId.Text = dt.Rows[0]["CompanyLoginId"].ToString();
        lblCOnDesi.Text = dt.Rows[0]["ContactPersonDesignation"].ToString();
        lblcontPerson.Text = dt.Rows[0]["ContactPerson"].ToString();
       // lblDomain.Text = dt.Rows[0]["Domain"].ToString();
        lblemail.Text = dt.Rows[0]["Email"].ToString();
        lblfax.Text = dt.Rows[0]["fax"].ToString();
        lblphone.Text = dt.Rows[0]["phone"].ToString();
        lblplan.Text = dt.Rows[0]["PricePlanName"].ToString();
        amount = dt.Rows[0]["PricePlanAmount"].ToString();
        if (dt.Rows[0]["HostId"].ToString() == "False")
        {
           // lblDomain.Visible = true;
           // pnlServer.Visible = false;
        }
        else if (dt.Rows[0]["HostId"].ToString() == "True")
        {
           // lblDomain.Visible =false ;
            //pnlServer.Visible = true;
           // pnlDomain.Visible = false;
            str = "SELECT     * from HostDetail where CompanyId ='"+  dt.Rows[0]["CompanyId"].ToString() + "'"; 
            SqlCommand cmd1 = new SqlCommand(str, con);
            SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
            DataTable dt1 = new DataTable();
            adp1.Fill(dt1);
            if(dt1.Rows.Count > 0)
            {
                //lblSqlServerName.Text = dt1.Rows[0]["SqlServerName"].ToString();
                //lblSqlServerUName.Text = dt1.Rows[0]["SqlServerUName"].ToString();
                //lblSqlServerUPassword.Text = dt1.Rows[0]["SqlServerUPassword"].ToString();
                //lblDataBaseName.Text =  dt1.Rows[0]["DatabaseName"].ToString();               
            }        
        }
    }
    protected void submit_Click(object sender, ImageClickEventArgs e)
    {}
    public void insertpayment(int orderid)
    {
        string str = " INSERT INTO OrderPaymentSatus " +
                     " (OrderId, PaymentStatus, TransactionID) " +
                     " VALUES     ('"+ orderid +"','Completed','') ";
        SqlCommand cmd = new SqlCommand(str, con);
        con.Open();
        cmd.ExecuteNonQuery();
        con.Close();
    }
}
