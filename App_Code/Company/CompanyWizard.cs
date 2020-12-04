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
    SqlConnection con = new SqlConnection();       
	public CompanyWizard()
	{
        con = MyCommonfile.licenseconn();		
	}
    public static Boolean Company_Active_Status(string CompanyLoginId)
    {
        Boolean status = true;
        SqlConnection liceco = new SqlConnection();
        liceco = MyCommonfile.licenseconn();
        if (liceco.State.ToString() != "Open")
        {
            liceco.Open();
        }
        string str = " SELECT dbo.CompanyMaster.CompanyId, dbo.CompanyMaster.CompanyName, dbo.CompanyMaster.PricePlanId, dbo.PricePlanMaster.PricePlanName, dbo.CompanyMaster.ServerId, dbo.ServerMasterTbl.ServerName, dbo.CompanyMaster.active, dbo.PricePlanMaster.active AS PPActive, dbo.ServerMasterTbl.Status as SerStatus FROM dbo.CompanyMaster INNER JOIN dbo.PricePlanMaster ON dbo.CompanyMaster.PricePlanId = dbo.PricePlanMaster.PricePlanId INNER JOIN dbo.ServerMasterTbl ON dbo.CompanyMaster.ServerId = dbo.ServerMasterTbl.Id  where CompanyMaster.CompanyLoginId='" + CompanyLoginId + "' and CompanyMaster.Active='1' and dbo.ServerMasterTbl.Status='1' and dbo.PricePlanMaster.active='1' ";
        SqlCommand cmd = new SqlCommand(str, liceco);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        if (dt.Rows.Count == 0)
        {
            status = false;
        }
        liceco.Close();
        return status;
    }
    public static Boolean Company_LicenseExpire_Status(string CompanyLoginId)
    {
        Boolean status = true;
        SqlConnection liceco = new SqlConnection();
        liceco = MyCommonfile.licenseconn();
        if (liceco.State.ToString() != "Open")
        {
            liceco.Open();
        }
        string period = " SELECT dbo.CompanyMaster.PricePlanId, dbo.CompanyMaster.ServerId, dbo.CompanyMaster.active, dbo.ServerMasterTbl.Status, dbo.LicenseMaster.LicenseMasterId, dbo.LicenseMaster.SiteMasterId, dbo.LicenseMaster.CompanyId, dbo.LicenseMaster.LicenseDate, dbo.LicenseMaster.LicensePeriod FROM            dbo.CompanyMaster INNER JOIN dbo.PricePlanMaster ON dbo.CompanyMaster.PricePlanId = dbo.PricePlanMaster.PricePlanId INNER JOIN dbo.ServerMasterTbl ON dbo.CompanyMaster.ServerId = dbo.ServerMasterTbl.Id INNER JOIN dbo.LicenseMaster ON dbo.CompanyMaster.CompanyId = dbo.LicenseMaster.CompanyId Where CompanyMaster.CompanyLoginId='" + CompanyLoginId + "'  ";
        SqlDataAdapter adperiod = new SqlDataAdapter(period, liceco);
        DataTable dtperiodfor = new DataTable();
        adperiod.Fill(dtperiodfor);
        if (dtperiodfor.Rows.Count > 0)
        {
            foreach (DataRow uyt in dtperiodfor.Rows)
            {
                string licensePeriod = uyt["LicensePeriod"].ToString();
                string  PricePlanId = uyt["PricePlanId"].ToString();
                string month = "SELECT  DATEDIFF(day,'" + uyt["LicenseDate"] + "','" + DateTime.Now.ToShortDateString() + "')";
                SqlCommand cmdmonth = new SqlCommand(month, liceco);               
                object periodmont = cmdmonth.ExecuteScalar(); 
                int licenseday = 0;
                if (licensePeriod != "")
                {
                    licenseday = Convert.ToInt32(licensePeriod);
                }
             
                if (licenseday - Convert.ToInt32(periodmont) < 0)
                {
                    status = false;
                }               
            }
        }
        return status;
    }

    public static DataTable SelectCompanyInfo(string compid)
    {
        SqlConnection liceco = new SqlConnection();
        liceco = MyCommonfile.licenseconn();
        if (liceco.State.ToString() != "Open")
        {
            liceco.Open();
        }
        string str = " Select * From CompanyMaster where CompanyMaster.CompanyLoginId='" + compid + "'  ";
        SqlCommand cmd = new SqlCommand(str, liceco);
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
