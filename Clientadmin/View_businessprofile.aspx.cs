using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Web.Services;
using System.Data.SqlClient;
using System.Web.UI.WebControls.WebParts;



public partial class BusinessProfile : System.Web.UI.Page
{
    SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["conn1"].ToString());
    public static int id = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        lblVersion.Text = "This PageVersion Is V1  Date:29-Oct-2015 Develop By @Pk";
        if (!IsPostBack)
        {
            if (Request.QueryString["id"] != null)
            {
                id = Convert.ToInt32(Request.QueryString["id"]);
                string str = " SELECT EmployeeDataEntry.businessdataId, CountryMaster.CountryName, StateMaster.StateName, CityMaster.CityName, EmployeeDataEntry.BusinessName, " +
                             " EmployeeDataEntry.Phone, EmployeeDataEntry.mobile, EmployeeDataEntry.Email, EmployeeDataEntry.BusinessDetails, Forum.ForumOrder                    " +
                             " FROM CountryMaster INNER JOIN EmployeeDataEntry ON CountryMaster.ID = EmployeeDataEntry.Country INNER JOIN                                            " +
                             " StateMaster ON EmployeeDataEntry.State = StateMaster.ID INNER JOIN                                                                                    " +
                             " CityMaster ON EmployeeDataEntry.City = CityMaster.ID INNER JOIN                                                                                       " +
                             " Forum ON EmployeeDataEntry.Category = Forum.ForumOrderID   WHERE  EmployeeDataEntry.businessdataId =" + id + "";
                SqlDataAdapter da = new SqlDataAdapter(str, connection);
                DataTable dt = new DataTable();
                da.Fill(dt);


                if (dt.Rows.Count > 0)
                {
                    name.Text = dt.Rows[0][4].ToString();
                    category.Text = dt.Rows[0][9].ToString();
                    countrydata.Text = dt.Rows[0][1].ToString();
                    statedata.Text = dt.Rows[0][2].ToString();
                    citydata.Text = dt.Rows[0][3].ToString();
                    business_phone_number.Text = dt.Rows[0][5].ToString();
                    business_mobile_phone.Text = dt.Rows[0][6].ToString();
                    email_id.Text = dt.Rows[0][7].ToString();
                    business_details.Text = dt.Rows[0][8].ToString();

                }
                // }
            }

        }
    }
}