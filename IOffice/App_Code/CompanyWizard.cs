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
public class CompanyWizard
{
   // SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["OnlineAccountConnectionString"].ConnectionString);
    SqlConnection con = new SqlConnection(PageConn.connnn);
	public CompanyWizard()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public DataTable SelectCompanyInfo(string compid )
    {
        //********Radhika Changes
        //string str = "SELECT     CompanyMaster.CompanyId, CompanyMaster.CompanyName, CompanyMaster.AdminName, CompanyMaster.StateTaxNumber,  "+
        //              " CompanyMaster.IRSNumber, CompanyMaster.YearEndingDate, CompanyMaster.FirstYearStartDate, CompanyMaster.StartDateOfAccountYear,  "+
        //              "CompanyMaster.PaypalEmailId, CompanyMaster.CompanyLogo, CompanyAddressMaster.Address1,  "+
        //              "CompanyAddressMaster.CompanyName AS Expr1, CompanyAddressMaster.Address2, CompanyAddressMaster.Phone,  "+
        //              "CompanyAddressMaster.Country, CompanyAddressMaster.City, CompanyAddressMaster.State, CompanyAddressMaster.PinCode,  "+
        //              " CompanyAddressMaster.Email, CompanyAddressMaster.Fax, CompanyAddressMaster.ContactPersonDesignation, "+
        //              " CompanyAddressMaster.ContactPersonNAme, CompanyAddressMaster.WebSite "+
        //                " FROM         CompanyMaster INNER JOIN "+
        //              " CompanyAddressMaster ON CompanyMaster.CompanyId = CompanyAddressMaster.CompanyMasterId  ";
        //******************************
        
        //Session["Comid"].to
      

        string str = "SELECT     CompanyMaster.CompanyId, CompanyMaster.CompanyName, CompanyMaster.AdminName, CompanyMaster.StateTaxNumber,  " +
                     " CompanyMaster.IRSNumber, CompanyMaster.YearEndingDate, CompanyMaster.FirstYearStartDate, CompanyMaster.StartDateOfAccountYear,  " +
                     "CompanyMaster.PaypalEmailId, CompanyMaster.CompanyLogo, CompanyAddressMaster.Address1,  " +
                     "CompanyAddressMaster.CompanyName AS Expr1, CompanyAddressMaster.Address2, CompanyAddressMaster.Phone,  " +
                     "CompanyAddressMaster.Country, CompanyAddressMaster.City, CompanyAddressMaster.State, CompanyAddressMaster.PinCode,  " +
                     " CompanyAddressMaster.Email, CompanyAddressMaster.Fax, CompanyAddressMaster.ContactPersonDesignation, " +
                     " CompanyAddressMaster.ContactPersonNAme, CompanyAddressMaster.WebSite " +
                       " FROM         CompanyMaster INNER JOIN " +
                     " CompanyAddressMaster ON CompanyMaster.CompanyId = CompanyAddressMaster.CompanyMasterId where CompanyMaster.Compid='" +compid  + "'  ";

        SqlCommand cmd = new SqlCommand(str,con);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        return dt;

    }
    public int CompanyMaster(string compname,string statetax, string IRSno, DateTime yearEnddate,DateTime FirstYearStartDate, DateTime yearstatrdate, string paypalId, string logo, string contPerName,
                                string address1,string address2, string phone, string country, string city, string state, string pin, string email, string fax, string contPerDesi, string website,string compid )
    {
        string s1="SELECT     CompanyId FROM         CompanyMaster";
        SqlCommand c1=new SqlCommand(s1,con);
        SqlDataAdapter a1=new SqlDataAdapter(c1);
        DataTable d1=new DataTable();
        a1.Fill(d1);

        if (d1.Rows.Count == 0)
        {

            //string s2 = " INSERT INTO CompanyMaster " +
            //               " (CompanyName, StateTaxNumber, IRSNumber, YearEndingDate, FirstYearStartDate, StartDateOfAccountYear, PaypalEmailId, CompanyLogo,  AdminName)" +
            //             " VALUES     ('"+compname +"' ,'"+ statetax +"','"+ IRSno +"','"+ yearEnddate +"','"+ FirstYearStartDate +"','"+ yearstatrdate +"','"+ paypalId +"','"+ logo +"','"+ contPerName +"')";

            string s2 = " INSERT INTO CompanyMaster " +
                           " (CompanyName, StateTaxNumber, IRSNumber, YearEndingDate, FirstYearStartDate, StartDateOfAccountYear, PaypalEmailId, CompanyLogo,  AdminName,Compid)" +
                         " VALUES     ('" + compname + "' ,'" + statetax + "','" + IRSno + "','" + yearEnddate + "','" + FirstYearStartDate + "','" + yearstatrdate + "','" + paypalId + "','" + logo + "','" + contPerName + "','"+compid+"')";
           
            
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
            //******Radhika Chnages
            //string s5 = " Update CompanyMaster " +
            //              " set CompanyName='" + compname + "', StateTaxNumber='" + statetax + "', IRSNumber='" + IRSno + "', YearEndingDate='" + yearEnddate + "', FirstYearStartDate='" + FirstYearStartDate + "', StartDateOfAccountYear='" + yearstatrdate + "', PaypalEmailId= '" + paypalId + "', CompanyLogo='" + logo + "',  AdminName= '" + contPerName + "'" +
            //            " where CompanyId='"+ Convert.ToInt32(d1.Rows[0]["CompanyId"]) +"' ";
            //SqlCommand c5 = new SqlCommand(s5,con);
            //****************

            string s5 = " Update CompanyMaster " +
                        " set CompanyName='" + compname + "', StateTaxNumber='" + statetax + "', IRSNumber='" + IRSno + "', YearEndingDate='" + yearEnddate + "', FirstYearStartDate='" + FirstYearStartDate + "', StartDateOfAccountYear='" + yearstatrdate + "', PaypalEmailId= '" + paypalId + "', CompanyLogo='" + logo + "',  AdminName= '" + contPerName + "'" +
                      " where CompanyId='" + Convert.ToInt32(d1.Rows[0]["CompanyId"]) + "' and Compid='"+compid+"' ";
            SqlCommand c5 = new SqlCommand(s5, con);

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
    public int updatecompanymaster(string compname, string statetax, string IRSno, DateTime yearEnddate, DateTime FirstYearStartDate, DateTime yearstatrdate, string paypalId, string contPerName,
        string address1, string address2, string phone, string country, string city, string state, string pin, string email, string fax, string contPerDesi, string website,string compid,string finalcompid)
    {
        string s5 = " Update CompanyMaster " +
                          " set CompanyName='" + compname + "', StateTaxNumber='" + statetax + "', IRSNumber='" + IRSno + "', YearEndingDate='" + yearEnddate + "', FirstYearStartDate='" + FirstYearStartDate + "', StartDateOfAccountYear='" + yearstatrdate + "', PaypalEmailId= '" + paypalId + "', AdminName= '" + contPerName + "'" +
                        " where CompanyId='" + compid + "' and Compid='"+finalcompid+"' ";
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
