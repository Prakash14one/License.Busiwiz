using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
/// <summary>
/// Summary description for CompanyWizard
/// </summary>
public class CompanyWizard1
{
    //SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["OnlineAccountConnectionString"].ConnectionString);
    SqlConnection con = new SqlConnection(PageConn.connnn);
	public CompanyWizard1()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public DataTable SelectCompanyInfo()
    {
        string str = "SELECT     CompanyMaster.CompanyId, CompanyMaster.CompanyName, CompanyMaster.AdminName, CompanyMaster.StateTaxNumber,  "+
                      " CompanyMaster.IRSNumber, CompanyMaster.YearEndingDate, CompanyMaster.FirstYearStartDate, CompanyMaster.StartDateOfAccountYear,  "+
                      "CompanyMaster.PaypalEmailId, CompanyMaster.CompanyLogo, CompanyAddressMaster.Address1,  "+
                      "CompanyAddressMaster.CompanyName AS Expr1, CompanyAddressMaster.Address2, CompanyAddressMaster.Phone,  "+
                      "CompanyAddressMaster.Country, CompanyAddressMaster.City, CompanyAddressMaster.State, CompanyAddressMaster.PinCode,  "+
                      " CompanyAddressMaster.Email, CompanyAddressMaster.Fax, CompanyAddressMaster.ContactPersonDesignation, "+
                      " CompanyAddressMaster.ContactPersonNAme, CompanyAddressMaster.WebSite "+
                        " FROM         CompanyMaster INNER JOIN "+
                      " CompanyAddressMaster ON CompanyMaster.CompanyId = CompanyAddressMaster.CompanyMasterId";
        SqlCommand cmd = new SqlCommand(str,con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        return dt;

    }
    public int CompanyMaster(string compname, string statetax, string IRSno, DateTime yearEnddate, DateTime FirstYearStartDate, DateTime yearstatrdate, string paypalId, string logo, string contPerName,
                                string address1,string address2, string phone, string country, string city, string state, string pin, string email, string fax, string contPerDesi, string website )
    {


        // haiyal ..... removed logo update from this fn         string logo,
        string s1="SELECT     CompanyId FROM         CompanyMaster";
        SqlCommand c1=new SqlCommand(s1,con);
        SqlDataAdapter a1=new SqlDataAdapter(c1);
        DataTable d1=new DataTable();
        a1.Fill(d1);

        if (d1.Rows.Count == 0)
        {
            //removed logo ..................   '"+ logo +"',
            string s2 = " INSERT INTO CompanyMaster " +
                           " (CompanyName, StateTaxNumber, IRSNumber, YearEndingDate, FirstYearStartDate, StartDateOfAccountYear, PaypalEmailId, CompanyLogo,  AdminName)" +
                         " VALUES     ('" + compname + "' ,'" + statetax + "','" + IRSno + "','" + yearEnddate + "','" + FirstYearStartDate + "','" + yearstatrdate + "','" + paypalId + "','" + logo + "','" + contPerName + "')";
            SqlCommand c2 = new SqlCommand(s2,con);
            con.Open();
            c2.ExecuteNonQuery();
            con.Close();

            string s3 = "SELECT     max(CompanyId) as CompanyId FROM         CompanyMaster";
            SqlCommand c3 = new SqlCommand(s3, con);
            SqlDataAdapter a3 = new SqlDataAdapter(c3);
            DataTable d3= new DataTable();
            a3.Fill(d3);

            string s4 = "INSERT INTO CompanyAddressMaster " +
                      " (LegalName, CompanyName, Address1, Address2, Phone, Country, City, State, PinCode, Email, Fax, ContactPersonNAme, ContactPersonDesignation, WebSite, CompanyMasterId) " +
                    " VALUES     ('" + compname + "','" + compname + "','" + address1 + "','" + address2 + "','" + phone + "','" + country + "','" + city + "','" + state + "','" + pin + "','" + email + "','" + fax + "','" + contPerName + "','" + contPerDesi + "','" + website + "','" + Convert.ToInt32(d3.Rows[0]["CompanyId"]) + "')";
            SqlCommand c4 = new SqlCommand(s4,con);
            con.Open();
            c4.ExecuteNonQuery();
            con.Close();


        }
        else
        {
            // removed  ..............  CompanyLogo='" + logo + "', 

            string s5 = " Update CompanyMaster " +
                          " set CompanyName='" + compname + "', StateTaxNumber='" + statetax + "', IRSNumber='" + IRSno + "', YearEndingDate='" + yearEnddate + "', FirstYearStartDate='" + FirstYearStartDate + "', StartDateOfAccountYear='" + yearstatrdate + "', PaypalEmailId= '" + paypalId + "', AdminName= '" + contPerName + "'" +
                        " where CompanyId='"+ Convert.ToInt32(d1.Rows[0]["CompanyId"]) +"' ";
            SqlCommand c5 = new SqlCommand(s5,con);
            con.Open();
            c5.ExecuteNonQuery();
            con.Close();

            string s6 = "update CompanyAddressMaster " +
                     " set LegalName='" + compname + "', CompanyName= '" + compname + "', Address1='" + address1 + "', Address2='" + address2 + "', Phone='" + phone + "', Country='" + country + "', City='" + city + "', State ='" + state + "', PinCode= '" + pin + "', Email= '" + email + "', Fax= '" + fax + "', ContactPersonNAme='" + contPerName + "', ContactPersonDesignation='" + contPerDesi + "', WebSite='" + website + "' " +
                   "  where CompanyMasterId='" + Convert.ToInt32(d1.Rows[0]["CompanyId"]) + "' ";
            SqlCommand c6 = new SqlCommand(s6,con);
            con.Open();
            c6.ExecuteNonQuery();
            con.Close();
        }
        return 1;
    }

    // new fn 19-Feb-10 for update logo only..........................................


    public int LOGOUPDATE_CompanyMaster(string logo)
    {


        // haiyal ..... removed logo update from this fn         string logo,
        string s1 = "SELECT     CompanyId FROM         CompanyMaster";
        SqlCommand c1 = new SqlCommand(s1, con);
        SqlDataAdapter a1 = new SqlDataAdapter(c1);
        DataTable d1 = new DataTable();
        a1.Fill(d1);

        if (d1.Rows.Count == 0)
        {
            //removed logo ..................   '"+ logo +"',
            //string s2 = " INSERT INTO CompanyMaster " +
            //               " (CompanyName, StateTaxNumber, IRSNumber, YearEndingDate, FirstYearStartDate, StartDateOfAccountYear, PaypalEmailId, CompanyLogo,  AdminName)" +
            //             " VALUES     ('" + compname + "' ,'" + statetax + "','" + IRSno + "','" + yearEnddate + "','" + FirstYearStartDate + "','" + yearstatrdate + "','" + paypalId + "','" + contPerName + "')";
            //SqlCommand c2 = new SqlCommand(s2, con);
            //con.Open();
            //c2.ExecuteNonQuery();
            //con.Close();

            //string s3 = "SELECT     max(CompanyId) as CompanyId FROM         CompanyMaster";
            //SqlCommand c3 = new SqlCommand(s3, con);
            //SqlDataAdapter a3 = new SqlDataAdapter(c3);
            //DataTable d3 = new DataTable();
            //a3.Fill(d3);

            //string s4 = "INSERT INTO CompanyAddressMaster " +
            //          " (LegalName, CompanyName, Address1, Address2, Phone, Country, City, State, PinCode, Email, Fax, ContactPersonNAme, ContactPersonDesignation, WebSite, CompanyMasterId) " +
            //        " VALUES     ('" + compname + "','" + compname + "','" + address1 + "','" + address2 + "','" + phone + "','" + country + "','" + city + "','" + state + "','" + pin + "','" + email + "','" + fax + "','" + contPerName + "','" + contPerDesi + "','" + website + "','" + Convert.ToInt32(d3.Rows[0]["CompanyId"]) + "')";
            //SqlCommand c4 = new SqlCommand(s4, con);
            //con.Open();
            //c4.ExecuteNonQuery();
            //con.Close();


        }
        else
        {
            // removed  ..............  

            string s5 = " Update CompanyMaster " +
                          " set CompanyLogo='" + logo + "' "+
                          " where CompanyId='" + Convert.ToInt32(d1.Rows[0]["CompanyId"]) + "' ";
            SqlCommand c5 = new SqlCommand(s5, con);
            con.Open();
            c5.ExecuteNonQuery();
            con.Close();

        }
        return 1;
    }



    //  ..........logo insert update finish
    public int updatecompanymaster(string compname, string statetax, string IRSno, DateTime yearEnddate, DateTime FirstYearStartDate, DateTime yearstatrdate, string paypalId, string contPerName,
        string address1, string address2, string phone, string country, string city, string state, string pin, string email, string fax, string contPerDesi, string website,string compid)
    {
        string s5 = " Update CompanyMaster " +
                          " set CompanyName='" + compname + "', StateTaxNumber='" + statetax + "', IRSNumber='" + IRSno + "', YearEndingDate='" + yearEnddate + "', FirstYearStartDate='" + FirstYearStartDate + "', StartDateOfAccountYear='" + yearstatrdate + "', PaypalEmailId= '" + paypalId + "', AdminName= '" + contPerName + "'" +
                        " where CompanyId='" + compid + "' ";
        SqlCommand c5 = new SqlCommand(s5, con);
        con.Open();
        c5.ExecuteNonQuery();
        con.Close();

        string s6 = "update CompanyAddressMaster " +
                 " set LegalName='" + compname + "', CompanyName= '" + compname + "', Address1='" + address1 + "', Address2='" + address2 + "', Phone='" + phone + "', Country='" + country + "', City='" + city + "', State ='" + state + "', PinCode= '" + pin + "', Email= '" + email + "', Fax= '" + fax + "', ContactPersonNAme='" + contPerName + "', ContactPersonDesignation='" + contPerDesi + "', WebSite='" + website + "' " +
               "  where CompanyMasterId='" + compid + "' ";
        SqlCommand c6 = new SqlCommand(s6, con);
        con.Open();
        c6.ExecuteNonQuery();
        con.Close();

        return 1;
    }

    
}
