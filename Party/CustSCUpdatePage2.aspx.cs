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
//using System.Web.Mail;
using System.Net.Mail;
using System.Net;
using System.Text;


public partial class customer_CustSCUpdatePage2 : System.Web.UI.Page
{
    SqlConnection con ;

    

    int i;
    protected void Page_Load(object sender, EventArgs e)
    {
        PageConn pgcon = new PageConn();
        con = pgcon.dynconn;
        

        SqlCommand cm = new SqlCommand("SELECT     MAX(CustomerServiceCallMasterId) AS Id FROM         CustomerServiceCallMaster", con);
        cm.CommandType = CommandType.Text;
        SqlDataAdapter sadp = new SqlDataAdapter(cm);
        DataSet ds2 = new DataSet();
        sadp.Fill(ds2);
        int s = Convert.ToInt32(ds2.Tables[0].Rows[0]["Id"]) + 1;
     

        if (!IsPostBack)
        {

           
          

            ViewState["sortOrder"] = "";
            if (Convert.ToString(Request.QueryString["id"]) != "")
            {
                string st = " SELECT     CustomerServiceCallMaster.CustomerServiceCallMasterId, CustomerServiceCallMaster.Entrydate ," +
                       "   CustomerServiceCallMaster.ProblemTitle , CustomerServiceCallMaster.AssigendEmployeeID,  " +
                       " CustomerServiceCallMaster.ProblemDescription , CustomerServiceCallMaster.ProblemType , " +
                       " CustomerServiceCallMaster.CustomerId, CustomerServiceCallMaster.ServiceDateandTime, CustomerServiceCallMaster.ServiceStatusId , " +
                       " CustomerServiceCallMaster.ServiceNotes , ServiceStatusMaster.StatusName,User_master.Username , " +
                       " Party_master.Compname, Party_master.Contactperson, Party_master.Contactperson as comp " +
                       " FROM  CustomerServiceCallMaster INNER JOIN  " +
                       "    ServiceStatusMaster ON CustomerServiceCallMaster.ServiceStatusId = ServiceStatusMaster.StatusId  " +
                       " INNER JOIN User_master on User_master.UserID=CustomerServiceCallMaster.CustomerId INNER JOIN " +
                             " Party_master ON User_master.PartyID = Party_master.PartyID " +
                       " WHERE CustomerServiceCallMasterId = '" + Convert.ToString(Request.QueryString["id"]) + "' " +
                       " and (CustomerServiceCallMaster.CustomerDelete is null or  CustomerServiceCallMaster.CustomerDelete = 'False') ";
    

                SqlCommand cmd9 = new SqlCommand(st, con);
                cmd9.CommandType = CommandType.Text;
                SqlDataAdapter adp9 = new SqlDataAdapter(cmd9);
                DataSet ds9 = new DataSet();
                adp9.Fill(ds9);

                if (ds9.Tables[0].Rows.Count > 0)
                {
                    lblRef.Text = ds9.Tables[0].Rows[0]["CustomerServiceCallMasterId"].ToString();
                    lblEnttydate.Text = Convert.ToDateTime(ds9.Tables[0].Rows[0]["Entrydate"].ToString()).ToShortDateString();
                    lblServicedate.Text = ds9.Tables[0].Rows[0]["ServiceDateandTime"].ToString();
                   
                    lblCompStatus.Text = ds9.Tables[0].Rows[0]["StatusName"].ToString();
                    lblcustomerid.Text = ds9.Tables[0].Rows[0]["CustomerId"].ToString();
                    lblCustomerName.Text = ds9.Tables[0].Rows[0]["comp"].ToString();
                    lblproblemtitle.Text = ds9.Tables[0].Rows[0]["ProblemTitle"].ToString();
                    lblProblemdiscription.Text = ds9.Tables[0].Rows[0]["ProblemDescription"].ToString();
                   
                }
                customername();
              //  lblcustomername1.Text = Session["Customername"].ToString();
                fillgrid();

            }
        }
    }
    protected void customername()
    {
        SqlCommand cmd4 = new SqlCommand("SELECT     Name FROM         User_master WHERE     UserID = '" + Convert.ToString(Session["id"]) + "'", con);
        cmd4.CommandType = CommandType.Text;
        cmd4.Parameters.AddWithValue("@UserId", Convert.ToInt32(Session["Id"]));
        SqlDataAdapter da4 = new SqlDataAdapter(cmd4);
        DataSet ds4 = new DataSet();
        da4.Fill(ds4);

       // Session["Customername"] = ds4.Tables[0].Rows[0]["Name"].ToString();


    }

    protected void fillgrid()
    {




        SqlCommand cmd = new SqlCommand("select ServiceCallDetailTbl.*,EmployeeMaster.EmployeeMasterID as Uname,EmployeePayrollMaster.EmployeeNo from ServiceCallDetailTbl inner join CustomerServiceCallMaster on CustomerServiceCallMaster.CustomerServiceCallMasterId=ServiceCallDetailTbl.ServiceCallId inner join User_master on User_master.UserID=ServiceCallDetailTbl.AssignedUserId inner join Party_master on Party_master.PartyID=User_master.PartyID inner join EmployeeMaster on EmployeeMaster.PartyID=Party_master.PartyID left outer join EmployeePayrollMaster on EmployeePayrollMaster.EmpId=EmployeeMaster.EmployeeMasterID where ServiceCallDetailTbl.ServiceCallId='" + Convert.ToInt32(lblRef.Text) + "' order by ServiceCallDetailTbl.Id,ServiceCallDetailTbl.ServiceProvidedDate,EmployeePayrollMaster.EmployeeNo,ServiceCallDetailTbl.ServiceDoneNote ", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt1 = new DataTable();
        da.Fill(dt1);
        if (dt1.Rows.Count > 0)
        {
           

            GridView1.DataSource = dt1;
            DataView myDataView = new DataView();
            myDataView = dt1.DefaultView;

            if (hdnsortExp.Value != string.Empty)
            {
                myDataView.Sort = string.Format("{0} {1}", hdnsortExp.Value, hdnsortDir.Value);
            }


            GridView1.DataBind();
        }
        else
        {
            GridView1.DataSource = null;
            GridView1.DataBind();
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




   
    protected void Button2_Click(object sender, EventArgs e)
    {
        Response.Redirect("customerafterlogin.aspx");
    }
    protected void Button1_Click1(object sender, EventArgs e)
    {
        if (Button1.Text == "Printable Version")
        {
            pnlgrid.ScrollBars = ScrollBars.None;
            pnlgrid.Height = new Unit("100%");

            Button1.Text = "Hide Printable Version";
            Button7.Visible = true;
          

        }
        else
        {

          

            Button1.Text = "Printable Version";
            Button7.Visible = false;
         


        }
    }
    protected void GridView1_Sorting(object sender, GridViewSortEventArgs e)
    {
        hdnsortExp.Value = e.SortExpression.ToString();
        hdnsortDir.Value = sortOrder; // sortOrder;
        fillgrid();
    }
}

