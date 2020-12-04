using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Data.Common;



/// <summary>
/// Summary description for DataByCompany
/// </summary>
public class DataByCompany
{
    SqlConnection con;
    SqlCommand cmd;
    DataTable dt;
    SqlDataAdapter adp;

	public DataByCompany()
	{
        //con = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
	}
    public DataTable selectBusinessByCompany(int company_id)
    {
        DataTable dt = new DataTable();
        PageConn pgcon = new PageConn();
        con = pgcon.dynconn; 
        con.Close();
        con.Open();
        string k1;
        k1 = "SELECT BusinessMaster.BusinessID as businessid,BusinessMaster.BusinessName + ' : ' +DepartmentmasterMNC.Departmentname as businessname, BusinessMaster.DepartmentId FROM BusinessMaster inner join DepartmentmasterMNC on  DepartmentmasterMNC.id=BusinessMaster.DepartmentId WHERE BusinessMaster.company_id = '" + company_id + "' ORDER BY BusinessMaster.BusinessName";
        
        SqlCommand cmd1 = new SqlCommand(k1, con);
        SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);

        adp1.Fill(dt);
        con.Close();

        return dt;


    }

    public DataTable selectDesignation(int company_id)
    {
        DataTable dt = new DataTable();
        PageConn pgcon = new PageConn();
        con = pgcon.dynconn; 
        con.Close();
        con.Open();
        string k1;
       // k1 = "SELECT BusinessMaster.BusinessName , BusinessMaster.businessid FROM Company_Business INNER JOIN  BusinessMaster ON Company_Business.BusinessID = BusinessMaster.BusinessID WHERE Company_Business.company_id = " + company_id + " ORDER BY BusinessMaster.BusinessName";
        k1 = "SELECT DesignationMaster.DesignationID, DesignationMaster.DesignationName, Department.Dept_Name from DesignationMaster inner join Department on Department.Dept_ID=DesignationMaster.Dept_ID  where Department.company_id = " + company_id + " ORDER BY Department.Dept_Name";
        SqlCommand cmd1 = new SqlCommand(k1, con);
        SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);

        adp1.Fill(dt);
        con.Close();

        return dt;


    }

    public DataTable selectDepartment(int company_id)
    {
        DataTable dt = new DataTable();
        PageConn pgcon = new PageConn();
        con = pgcon.dynconn; 
        con.Close();
        con.Open();
        string k1;
       k1 = "SELECT Department.Dept_Name, Department.Dept_ID FROM Department WHERE Department.company_id = " + company_id + " ORDER BY Department.Dept_Name";
       
        SqlCommand cmd1 = new SqlCommand(k1, con);
        SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);

        adp1.Fill(dt);
        con.Close();

        return dt;


    }


    public DataTable selectEmployeeByCompany(int company_id)
    {
        DataTable dt = new DataTable();
        PageConn pgcon = new PageConn();
        con = pgcon.dynconn; 
        con.Close();
        con.Open();
        string k1;
        k1 = "SELECT EmployeeMaster.EmployeeMasterID, EmployeeMaster.EmployeeName FROM EmployeeMaster  inner join Party_Master on Party_Master.PartyID=EmployeeMaster.PartyID WHERE   (Party_Master.id='" + company_id + "') ORDER BY EmployeeMaster.EmployeeName";
        SqlCommand cmd1 = new SqlCommand(k1, con);
        SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);

        adp1.Fill(dt);
        con.Close();

        return dt;



    }

    public DataTable selectAllEmployeeByCompany(int company_id)
    {
        DataTable dt = new DataTable();
        PageConn pgcon = new PageConn();
        con = pgcon.dynconn; 
        con.Close();
        con.Open();
        string k1;
        k1 = "SELECT EmployeeMaster.EmployeeMasterID, EmployeeMaster.EmployeeName FROM  Company_Employee INNER JOIN EmployeeMaster ON Company_Employee.EmployeeID = EmployeeMaster.EmployeeMasterID WHERE (Company_Employee.company_id = " + company_id + ")  ORDER BY EmployeeMaster.EmployeeName";
        SqlCommand cmd1 = new SqlCommand(k1, con);
        SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);

        adp1.Fill(dt);
        con.Close();

        return dt;


    }
    public DataTable selectObjectiveByCompany(int company_id)
    {
        DataTable dt = new DataTable();
        PageConn pgcon = new PageConn();
        con = pgcon.dynconn; 
        con.Close();
        con.Open();
        string k1;
      
        k1 = "SELECT  ObjectiveMaster.MasterId, ObjectiveMaster.BusinessId, ObjectiveMaster.EmployeeId, ObjectiveMaster.ObjectiveName, ObjectiveMaster.Description, "
                       + " ObjectiveMaster.EStartDate, ObjectiveMaster.EEndDate, ObjectiveMaster.AEndDate, ObjectiveMaster.BudgetedCost, ObjectiveMaster.ActualCost,  "
                      +" ObjectiveMaster.ShortageExcess, BusinessMaster.BusinessID , BusinessMaster.BusinessName"
+" FROM ObjectiveMaster INNER JOIN"
                     +" BusinessMaster ON ObjectiveMaster.BusinessId = BusinessMaster.BusinessID INNER JOIN"
                      +" Company_Business ON BusinessMaster.BusinessID = Company_Business.BusinessID "
+ " WHERE (Company_Business.company_id = " + company_id + ")"
+ " ORDER BY BusinessMaster.BusinessName, ObjectiveMaster.ObjectiveName";


        SqlCommand cmd1 = new SqlCommand(k1, con);
        SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);

        adp1.Fill(dt);
        con.Close();

        return dt;

    }
    public DataTable ObjectiveMasterGetDataByYear(int company_id,int year)
    {
        DataTable dt = new DataTable();
        PageConn pgcon = new PageConn();
        con = pgcon.dynconn; 
        con.Close();
        con.Open();
        string k1;

        k1 = " SELECT businessmaster.*, objectivemaster.*, (year(objectivemaster.eenddate))as year"
                 + " FROM ObjectiveMaster INNER JOIN"
                 + " BusinessMaster ON ObjectiveMaster.BusinessId = BusinessMaster.BusinessID INNER JOIN"
                 + " Company_Business ON BusinessMaster.BusinessID = Company_Business.BusinessID"
                 + " WHERE     (YEAR(ObjectiveMaster.EEndDate) = " + year + ") AND (Company_Business.company_id = " + company_id + ")"
                 + " ORDER BY BusinessMaster.BusinessName, ObjectiveMaster.ObjectiveName";


        SqlCommand cmd1 = new SqlCommand(k1, con);
        SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);

        adp1.Fill(dt);
        con.Close();

        return dt;


    }


    public DataTable ObjectiveMasterGetDataByStatusId(int status, int company_id)
    {
        DataTable dt = new DataTable();
        PageConn pgcon = new PageConn();
        con = pgcon.dynconn; 
        con.Close();
        con.Open();
        string k1;

        k1 = "SELECT objectivemaster.*,businessmaster.*"
                + " FROM ObjectiveMaster INNER JOIN"
                + " BusinessMaster ON ObjectiveMaster.BusinessId = BusinessMaster.BusinessID AND ObjectiveMaster.MasterId IN"
                + " (SELECT DISTINCT MasterId"
                + " FROM ObjectiveEvaluation"
                + " WHERE (StatusId = " + status + ")) INNER JOIN"
                + " Company_Business ON BusinessMaster.BusinessID = Company_Business.BusinessID"
                + " WHERE (Company_Business.company_id = " + company_id + ")";
        
        SqlCommand cmd1 = new SqlCommand(k1, con);
        SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);

        adp1.Fill(dt);
        con.Close();

        return dt;


    }
    public void CreateDocumentAttach(string TableMasterId, string RecordId, string Url, string AttachTitle, string FileName, string Date, string DocumentType)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "InsertDocumetAttachmentTbl";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@TableMasterId";
        param.Value = TableMasterId;
        param.DbType = DbType.String;
        param.Size = 10;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@RecordId";
        param.Value = RecordId;
        param.DbType = DbType.String;
        //param.Size = 500;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@Url";
        param.Value = Url;
        param.DbType = DbType.String;
        param.Size = 600;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@AttachTitle";
        param.Value = AttachTitle;
        param.DbType = DbType.String;
        param.Size = 200;
        comm.Parameters.Add(param);
        param = comm.CreateParameter();
        param.ParameterName = "@FileName";
        param.Value = FileName;
        param.DbType = DbType.String;
        param.Size = 500;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@Date";
        param.Value = Date;
        param.DbType = DbType.String;
        param.Size = 20;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@DocumentType";
        param.Value = DocumentType;
        param.DbType = DbType.String;
        param.Size = 100;
        comm.Parameters.Add(param);
        DataAccess.ExecuteNonQuery(comm);
    }

    public void createNewEmployeebyMaster(string employeename, string Dept_ID, string address, string city, string state, string country, string phone, string mobile, string email, string rtypeid, string rutypeid, string ctc, string otctc, string doj, string supervisorid, int cid, string WEmail, string PCName, string WindowsId, string PCIPAddress, string EmpDomainName, string EmpDomainPwd, int Active, string RegularStartTime, string RegularEndTime, Boolean Resumeupload, Boolean Imageupload)
    {
        // get a configured DbCommand object
        DbCommand comm = DataAccess.CreateCommand();

        // set the stored procedure name
        comm.CommandText = "InsertByEmployeeMaster";

        // create a new parameter
        DbParameter param = comm.CreateParameter();
        param.ParameterName = "@employeename";
        param.Value = employeename;
        param.DbType = DbType.String;
        param.Size = 500;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@Dept_ID";
        param.Value = Dept_ID;
        param.DbType = DbType.String;
        //param.Size = 500;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@address";
        param.Value = address;
        param.DbType = DbType.String;
        param.Size = 500;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@city";
        param.Value = city;
        param.DbType = DbType.String;
        param.Size = 100;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@state";
        param.Value = state;
        param.DbType = DbType.String;
        param.Size = 100;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@country";
        param.Value = country;
        param.DbType = DbType.String;
        param.Size = 100;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@phone";
        param.Value = phone;
        param.DbType = DbType.String;
        param.Size = 100;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@mobile";
        param.Value = mobile;
        param.DbType = DbType.String;
        param.Size = 100;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@email";
        param.Value = email;
        param.DbType = DbType.String;
        param.Size = 100;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@rtypeid";
        param.Value = rtypeid;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@rutypeid";
        param.Value = rutypeid;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@ctc";
        param.Value = ctc;
        param.DbType = DbType.Decimal;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@otctc";
        param.Value = otctc;
        param.DbType = DbType.Decimal;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@doj";
        param.Value = doj;
        param.DbType = DbType.DateTime;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@supervisorid";
        param.Value = supervisorid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);


        param = comm.CreateParameter();
        param.ParameterName = "@cid";
        param.Value = cid;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@wemail";
        param.Value = WEmail;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@pcname";
        param.Value = PCName;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@windowsid";
        param.Value = WindowsId;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@pcipaddress";
        param.Value = PCIPAddress;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@empdomainname";
        param.Value = EmpDomainName;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@empdomainpwd";
        param.Value = EmpDomainPwd;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@active";
        param.Value = Active;
        param.DbType = DbType.Int32;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@RegularStartTime";
        param.Value = RegularStartTime;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@RegularEndTime";
        param.Value = RegularEndTime;
        param.DbType = DbType.String;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@Resumeupload";
        param.Value = Resumeupload;
        param.DbType = DbType.Boolean;
        comm.Parameters.Add(param);

        param = comm.CreateParameter();
        param.ParameterName = "@Imageupload";
        param.Value = Imageupload;
        param.DbType = DbType.Boolean;
        comm.Parameters.Add(param);

       DataAccess.ExecuteNonQuery(comm);
      
    }



    public DataTable LTGMasterByComapanyid(int company_id)
    {
        DataTable dt = new DataTable();
        PageConn pgcon = new PageConn();
        con = pgcon.dynconn; 
        con.Close();
        con.Open();
        string k1;

        k1 = "SELECT * FROM LTGMaster INNER JOIN"
              +" ObjectiveMaster ON LTGMaster.ObjectiveMasterId = ObjectiveMaster.MasterId INNER JOIN"
              +" BusinessMaster ON LTGMaster.BusinessId = BusinessMaster.BusinessID INNER JOIN"
              +" Company_Business ON BusinessMaster.BusinessID = Company_Business.BusinessID"
              + " WHERE (Company_Business.company_id = " + company_id + ")  order by BusinessMaster.BusinessName";
             

        SqlCommand cmd1 = new SqlCommand(k1, con);
        SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);

        adp1.Fill(dt);
        con.Close();

        return dt;


    }


    public DataTable LTGMasterGetDataByYearbyCompany(int company_id, int year)
    {
        DataTable dt = new DataTable();
        PageConn pgcon = new PageConn();
        con = pgcon.dynconn; 
        con.Close();
        con.Open();
        string k1;


        k1 = "select businessmaster.*, ltgmaster.*, (year(ltgmaster.eenddate))as year"
                + " FROM LTGMaster INNER JOIN"
                + " BusinessMaster ON LTGMaster.BusinessId = BusinessMaster.BusinessID INNER JOIN"
                + " Company_Business ON BusinessMaster.BusinessID = Company_Business.BusinessID"
                + " WHERE     (YEAR(LTGMaster.EEndDate) = " + year + ") AND (Company_Business.company_id = " + company_id + ")"
             + " order by BusinessMaster.BusinessName";


        SqlCommand cmd1 = new SqlCommand(k1, con);
        SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);

        adp1.Fill(dt);
        con.Close();

        return dt;


    }
    public DataTable LTGMasterGetDataByStatusbyCompany(int company_id, int status)
    {
        DataTable dt = new DataTable();
        PageConn pgcon = new PageConn();
        con = pgcon.dynconn; 
        con.Close();
        con.Open();
        string k1;
          k1 = "SELECT * FROM LTGMaster INNER JOIN BusinessMaster ON LTGMaster.BusinessId = BusinessMaster.BusinessID AND LTGMaster.MasterId IN"
            + " (SELECT DISTINCT MasterId FROM LTGEvaluation WHERE (StatusId = " + status + ")) INNER JOIN"
            + " Company_Business ON BusinessMaster.BusinessID = Company_Business.BusinessID"
		    + " WHERE     (Company_Business.company_id = " + company_id + ") ORDER BY BusinessMaster.BusinessName";

        SqlCommand cmd1 = new SqlCommand(k1, con);
        SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);

        adp1.Fill(dt);
        con.Close();

        return dt;


    }



    public DataTable STGMasterGetDatabyCompany(int company_id)
    {
        DataTable dt = new DataTable();
        PageConn pgcon = new PageConn();
        con = pgcon.dynconn; 
        con.Close();
        con.Open();
        string k1;
        k1 = "select * FROM STGMaster INNER JOIN"
             + " LTGMaster ON STGMaster.LTGMasterID = LTGMaster.MasterId INNER JOIN"
             + " ObjectiveMaster ON LTGMaster.ObjectiveMasterId = ObjectiveMaster.MasterId INNER JOIN"
             + " BusinessMaster ON STGMaster.BusinessId = BusinessMaster.BusinessID INNER JOIN"
             + " Company_Business ON BusinessMaster.BusinessID = Company_Business.BusinessID"
             + " WHERE (Company_Business.company_id = " + company_id + ") ORDER BY BusinessMaster.BusinessName";

        SqlCommand cmd1 = new SqlCommand(k1, con);
        SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);

        adp1.Fill(dt);
        con.Close();

        return dt;


    }


    public DataTable STGMasterByYearbyCompany(int company_id,int year)
    {
        PageConn pgcon = new PageConn();
        con = pgcon.dynconn; 
        DataTable dt = new DataTable();
        con.Close();
        con.Open();
        string k1;
        k1 = "select * FROM"
                + " STGMaster INNER JOIN LTGMaster ON STGMaster.LTGMasterID = LTGMaster.MasterId"
                + " INNER JOIN ObjectiveMaster ON LTGMaster.ObjectiveMasterId = ObjectiveMaster.MasterId"
                + " INNER JOIN BusinessMaster ON STGMaster.BusinessId = BusinessMaster.BusinessID "
                + " INNER JOIN Company_Business ON BusinessMaster.BusinessID = Company_Business.BusinessID"
                + " WHERE (Company_Business.company_id = " + company_id + ") and  (YEAR(STGMaster.EEndDate) = " + year + ")"
                + " ORDER BY BusinessMaster.BusinessName";

        SqlCommand cmd1 = new SqlCommand(k1, con);
        SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);

        adp1.Fill(dt);
        con.Close();

        return dt;


    }



    public DataTable STGMasterByStatusIdbyCompany(int status, int comapny_id)
    {
        PageConn pgcon = new PageConn();
        con = pgcon.dynconn; 
        DataTable dt = new DataTable();
        con.Close();
        con.Open();
        string k1;
        k1 = "SELECT  * FROM STGMaster INNER JOIN"
                +" BusinessMaster ON STGMaster.BusinessId = BusinessMaster.BusinessID AND STGMaster.MasterId IN"
                +" (SELECT DISTINCT MasterId FROM STGEvaluation WHERE (StatusId = " + status +" )) INNER JOIN"
                +" Company_Business ON BusinessMaster.BusinessID = Company_Business.BusinessID"
		        +" WHERE (Company_Business.company_id = " + comapny_id + ")";

        SqlCommand cmd1 = new SqlCommand(k1, con);
        SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);

        adp1.Fill(dt);
        con.Close();

        return dt;


    }



    public DataTable YMasterGetDatabyCompany(int company_id,int business_id,int year)
    {
        DataTable dt = new DataTable();
        PageConn pgcon = new PageConn();
        con = pgcon.dynconn; 
        con.Close();
        con.Open();
        string k1,k2;
        
        //k1 = "SELECT * FROM YMaster INNER JOIN"
        //     +" STGMaster ON YMaster.StgMasterId = STGMaster.MasterId INNER JOIN"
        //     +" LTGMaster ON STGMaster.LTGMasterID = LTGMaster.MasterId INNER JOIN"
        //     +" ObjectiveMaster ON LTGMaster.ObjectiveMasterId = ObjectiveMaster.MasterId INNER JOIN"
        //     +" BusinessMaster ON YMaster.BusinessId = BusinessMaster.BusinessID INNER JOIN"
        //     +" Company_Business ON BusinessMaster.BusinessID = Company_Business.BusinessID"
        //     +" WHERE (Company_Business.company_id = 1) order by BusinessMaster.BusinessName";


        k1="SELECT SUBSTRING(ObjectiveMaster.ObjectiveName,1,20)+'-->LTG: '+SUBSTRING(LTGMaster.Title,1,20)+'-->STG: '+SUBSTRING(STGMaster.Title,1,20)+'-->YG: '+SUBSTRING(YMaster.Title,1,80) as title,* "
			    +" FROM YMaster INNER JOIN"
                +" STGMaster ON YMaster.StgMasterId = STGMaster.MasterId INNER JOIN"
                +" LTGMaster ON STGMaster.LTGMasterID = LTGMaster.MasterId INNER JOIN"
                +" ObjectiveMaster ON LTGMaster.ObjectiveMasterId = ObjectiveMaster.MasterId INNER JOIN"
			    +" BusinessMaster ON YMaster.BusinessId = BusinessMaster.BusinessID INNER JOIN"
			    +" Company_Business ON BusinessMaster.BusinessID = Company_Business.BusinessID"
                +"  WHERE (Company_Business.company_id = " + company_id + ")";
        if (business_id != 0)
        {
            k1 = k1 + " " + "and BusinessMaster.BusinessID = " + business_id ;
        }
        if (year != 0)
        {
            k1 = k1 + " " + "and YMaster.year  = " + year;

        }
        k2 = " order by BusinessMaster.BusinessName";

        k1 = k1 + k2;

        SqlCommand cmd1 = new SqlCommand(k1, con);
        SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);

        adp1.Fill(dt);
        con.Close();

        return dt;


    }




    public DataTable YMasterGetDatabyStatus(int company_id, int BusinessID, int StatusId)
    {
        DataTable dt = new DataTable();
        PageConn pgcon = new PageConn();
        con = pgcon.dynconn; 
        con.Close();
        con.Open();
        string k1, k2;

        //new

        //k1 = "SELECT SUBSTRING(ObjectiveMaster.ObjectiveName,1,20)+'-->'+SUBSTRING(LTGMaster.Title,1,20)+'-->'+SUBSTRING(STGMaster.Title,1,20)+'-->'+SUBSTRING(YMaster.Title,1,60) as title,* "
        //        + " FROM YMaster INNER JOIN"
        //        + " STGMaster ON YMaster.StgMasterId = STGMaster.MasterId INNER JOIN"
        //        + " LTGMaster ON STGMaster.LTGMasterID = LTGMaster.MasterId INNER JOIN"
        //        + " ObjectiveMaster ON LTGMaster.ObjectiveMasterId = ObjectiveMaster.MasterId INNER JOIN"
        //        + " BusinessMaster ON YMaster.BusinessId = BusinessMaster.BusinessID INNER JOIN"
        //        + " Company_Business ON BusinessMaster.BusinessID = Company_Business.BusinessID"
        //        + "  WHERE (Company_Business.company_id = " + company_id + ")";

      k1 = "SELECT SUBSTRING(ObjectiveMaster.ObjectiveName,1,20)+'-->LTG: '+SUBSTRING(LTGMaster.Title,1,20)+'-->STG: '+SUBSTRING(STGMaster.Title,1,20)+'-->YG: '+SUBSTRING(YMaster.Title,1,80) as title,* "
               + " FROM YMaster INNER JOIN"
               + " STGMaster ON YMaster.StgMasterId = STGMaster.MasterId INNER JOIN"
               + " LTGMaster ON STGMaster.LTGMasterID = LTGMaster.MasterId INNER JOIN"
               + " ObjectiveMaster ON LTGMaster.ObjectiveMasterId = ObjectiveMaster.MasterId INNER JOIN"
               + " BusinessMaster ON YMaster.BusinessId = BusinessMaster.BusinessID INNER JOIN"
               + " Company_Business ON BusinessMaster.BusinessID = Company_Business.BusinessID INNER JOIN" 
               + " StatusMaster on StatusMaster.Company_id=Company_Business.company_id"
               + " WHERE (Company_Business.company_id = " + company_id + ")";




      if (BusinessID != 0)
        {
            k1 = k1 + " " + "and BusinessMaster.BusinessID = " + BusinessID;
        }
        if (StatusId != 0)
        {
            k1 = k1 + " " + "and StatusMaster.StatusId  = " + StatusId;

        }
        k2 = " order by BusinessMaster.BusinessName";

        k1 = k1 + k2;

        SqlCommand cmd1 = new SqlCommand(k1, con);
        SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);

        adp1.Fill(dt);
        con.Close();

        return dt;


    }



    public DataTable MMasterGetDatabyStatus(int company_id, int statusId, int year, int month,int empid)
    {
        DataTable dt = new DataTable();
        PageConn pgcon = new PageConn();
        con = pgcon.dynconn; 
        con.Close();
        con.Open();
        string k1, k2;


        k1 = "Select Distinct MMaster.MasterId, BusinessMaster.BusinessName,MMaster.BudgetedCost,MMaster.ActualCost,MMaster.ShortageExcess, EmployeeMaster.EmployeeName,Year.Name+'->'+Month.Name as yearmonth, 'LTG: '+SUBSTRING(LTGMaster.Title,1,20)+'-->STG: '+SUBSTRING(STGMaster.Title,1,20)+'-->YG: '+SUBSTRING(YMaster.Title,1,20)+'-->MG: '+ MMaster.Title as title from Year inner join Month on Month.Yid=Year.Id inner join MMaster on MMaster.Month=Month.Id Right join EmployeeMaster on EmployeeMaster.EmployeeMasterID=MMaster.EmployeeId join ymaster"
                + " on mmaster.ymasterid=ymaster.masterid join stgmaster on mMaster.stgmasterid=stgmaster.masterid"
                + " join ltgmaster on mMaster.ltgmasterid=ltgmaster.masterid join objectivemaster"
                + " on mMaster.objectivemasterid=objectivemaster.masterid join businessmaster"
                + " on MMaster.businessid=businessmaster.businessid"
                + " join Company_Business on Company_Business.BusinessID = businessmaster.businessid"
                + "  WHERE (Company_Business.company_id = " + company_id + ")";
        if (statusId != 0)
        {
            k1 = k1 + " " + "and MMaster.masterid in"
                + "(select distinct masterid from MEvaluation where statusid='"+statusId+"')";
        }
        if (year > 0)
        {
            k1 = k1 + " " + "and YMaster.year = " + year;
        }
        if (month > 0)
        {
            k1 = k1 + " " + "and MMaster.month = " + month;
        }
        if (empid > 0)
        {
            k1 = k1 + " " + "and MMaster.EmployeeId = " + empid;
        }
        k2 = " order by BusinessMaster.BusinessName";

        k1 = k1 + k2;

        SqlCommand cmd1 = new SqlCommand(k1, con);
        SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);

        adp1.Fill(dt);
        con.Close();

        return dt;


    }

    public DataTable MMasterGetDatabyCompany(int company_id, int business_id, int year, int month, int empid)
    {
        DataTable dt = new DataTable();
        PageConn pgcon = new PageConn();
        con = pgcon.dynconn; 
        con.Close();
        con.Open();
        string k1, k2;


        k1 = "Select  MMaster.MasterId,MMaster.Description, BusinessMaster.BusinessName,MMaster.BudgetedCost,MMaster.ActualCost,MMaster.ShortageExcess, EmployeeMaster.EmployeeName,Year.Name+'->'+Month.Name as yearmonth,'LTG: '+SUBSTRING(LTGMaster.Title,1,20)+'-->STG: '+SUBSTRING(STGMaster.Title,1,20)+'-->YG: '+SUBSTRING(YMaster.Title,1,20)+'-->MG: '+ MMaster.Title as title from Year inner join Month on Month.Yid=Year.Id inner join MMaster on MMaster.Month=Month.Id Right join EmployeeMaster on EmployeeMaster.EmployeeMasterID=MMaster.EmployeeId join ymaster"
                + " on mmaster.ymasterid=ymaster.masterid join stgmaster on mMaster.stgmasterid=stgmaster.masterid"
                + " join ltgmaster on mMaster.ltgmasterid=ltgmaster.masterid join objectivemaster"
                + " on mMaster.objectivemasterid=objectivemaster.masterid join businessmaster"
                + " on MMaster.businessid=businessmaster.businessid"
                + " join Company_Business on Company_Business.BusinessID = businessmaster.businessid"
                + "  WHERE (Company_Business.company_id = " + company_id + ")";
        if (business_id != 0)
        {
            k1 = k1 + " " + "and BusinessMaster.BusinessID = " + business_id;
        }
        if (year > 0)
        {
            k1 = k1 + " " + "and YMaster.year = " + year;
        }
        if (month > 0)
        {
            k1 = k1 + " " + "and MMaster.month = " + month;
        }
        if (empid > 0)
        {
            k1 = k1 + " " + "and MMaster.EmployeeId = " + empid;
        }
        k2 = " order by BusinessMaster.BusinessName";

        k1 = k1 + k2;

        SqlCommand cmd1 = new SqlCommand(k1, con);
        SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);

        adp1.Fill(dt);
        con.Close();

        return dt;


    }

    public DataTable WMasterGetDatabyStatus(int company_id, int statusId, int year, int month, int week, int empid)
    {
        DataTable dt = new DataTable();
        PageConn pgcon = new PageConn();
        con = pgcon.dynconn; 
        con.Close();
        con.Open();
        string k1, k2;


        k1 = "Select  WMaster.MasterId,StatusMaster.StatusName,WMaster.Description,WEvaluation.EvaluationNote, BusinessMaster.BusinessName,WMaster.BudgetedCost,WMaster.ActualCost,WMaster.ShortageExcess, EmployeeMaster.EmployeeName,Year.Name+'->'+Month.Name+'->'+Week.Name as yearmonth,'YG: '+SUBSTRING(YMaster.Title,1,20)+'-->MG: '+ SUBSTRING(MMaster.Title,1,20)+'-->WG: '+WMaster.Title as title from Year inner join Month on Month.Yid=Year.Id inner join Week on Week.Mid=Month.Id inner join WMaster on  WMaster.Week=Week.Id Right join EmployeeMaster on EmployeeMaster.EmployeeMasterID=WMaster.EmployeeId join mmaster on wmaster.mmasterid=mmaster.masterid  join ymaster"
                + " on mmaster.ymasterid=ymaster.masterid join stgmaster on mMaster.stgmasterid=stgmaster.masterid"
                + " join ltgmaster on mMaster.ltgmasterid=ltgmaster.masterid join objectivemaster"
                + " on mMaster.objectivemasterid=objectivemaster.masterid join businessmaster"
                + " on MMaster.businessid=businessmaster.businessid"
                + " join Company_Business on Company_Business.BusinessID = businessmaster.businessid  inner join  WEvaluation on WEvaluation.MasterId=WMaster.MasterId  inner join StatusMaster on StatusMaster.StatusId=WEvaluation.StatusId"
                + "  WHERE Company_Business.company_id = '" + company_id + "'";
        if (statusId != 0)
        {
            k1 = k1 + " and StatusMaster.statusid='" + statusId + "'";
        }
        if (year > 0)
        {
            k1 = k1 + " " + "and YMaster.year = " + year;
        }
        if (month > 0)
        {
            k1 = k1 + " " + "and MMaster.month = " + month;
        }
        if (week > 0)
        {
            k1 = k1 + " " + "and WMaster.Week = " + week;
        }
        if (empid > 0)
        {
            k1 = k1 + " " + "and WMaster.EmployeeId = " + empid;
        }
        k2 = " order by BusinessMaster.BusinessName";

        k1 = k1 + k2;

        SqlCommand cmd1 = new SqlCommand(k1, con);
        SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);

        adp1.Fill(dt);
        con.Close();

        return dt;


    }
    public DataTable wMasterGetDatabyCompany(int company_id, int business_id, int year, int month, int week, int empid)
    {
        PageConn pgcon = new PageConn();
        con = pgcon.dynconn; 
        DataTable dt = new DataTable();

        con.Close();
        con.Open();
        string k1, k2;


        //k1 = "SELECT SUBSTRING(ObjectiveMaster.ObjectiveName,1,20) + '-' + SUBSTRING(LTGMaster.Title,1,20) + '-' + SUBSTRING(STGMaster.Title,1,20) + '-' + SUBSTRING(YMaster.Title,1,20) + '-' + SUBSTRING(MMaster.Title,1,20) + '-' + WMaster.Title AS title,BusinessMaster.BusinessName,*"
        //        +" FROM WMaster INNER JOIN"
        //        +" MMaster ON WMaster.MMasterId = MMaster.MasterId INNER JOIN"
        //        +" YMaster ON WMaster.YMasterId = YMaster.MasterId INNER JOIN"
        //        +" STGMaster ON WMaster.STGMasterId = STGMaster.MasterId INNER JOIN"
        //        +" LTGMaster ON WMaster.LTGMasterId = LTGMaster.MasterId INNER JOIN"
        //        +" ObjectiveMaster ON WMaster.ObjectiveMasterId = ObjectiveMaster.MasterId INNER JOIN"
        //        +" Company_Business INNER JOIN"
        //        +" BusinessMaster ON Company_Business.BusinessID = BusinessMaster.BusinessID ON WMaster.BusinessId = BusinessMaster.BusinessID"
        //        +" WHERE (Company_Business.company_id = " + company_id + ")";
        k1 = " Select  WMaster.MasterId,StatusMaster.StatusName,WMaster.Description,WEvaluation.EvaluationNote, BusinessMaster.BusinessName,WMaster.BudgetedCost,WMaster.ActualCost,WMaster.ShortageExcess, EmployeeMaster.EmployeeName,Year.Name+'->'+Month.Name+'->'+Week.Name as yearmonth,'YG: '+SUBSTRING(YMaster.Title,1,20)+'-->MG: '+ SUBSTRING(MMaster.Title,1,20)+'-->WG: '+WMaster.Title as title from Year inner join Month on Month.Yid=Year.Id inner join Week on Week.Mid=Month.Id inner join WMaster on  WMaster.Week=Week.Id Right join EmployeeMaster on EmployeeMaster.EmployeeMasterID=WMaster.EmployeeId join mmaster on wmaster.mmasterid=mmaster.masterid  join ymaster"
              + " on mmaster.ymasterid=ymaster.masterid join stgmaster on mMaster.stgmasterid=stgmaster.masterid"
              + " join ltgmaster on mMaster.ltgmasterid=ltgmaster.masterid join objectivemaster"
              + " on mMaster.objectivemasterid=objectivemaster.masterid join businessmaster"
              + " on MMaster.businessid=businessmaster.businessid inner join  WEvaluation on WEvaluation.MasterId=WMaster.MasterId  inner join StatusMaster on StatusMaster.StatusId=WEvaluation.StatusId"
              + " join Company_Business on Company_Business.BusinessID = businessmaster.businessid"
              + "  WHERE (Company_Business.company_id = " + company_id + ")";
        if (business_id != 0)
        {
            k1 = k1 + " " + "and BusinessMaster.BusinessID = " + business_id;
        }
        if (year > 0)
        {
            k1 = k1 + " " + "and YMaster.year = " + year;
        }
        if (month > 0)
        {
            k1 = k1 + " " + "and MMaster.month = " + month;
        }
        if (week > 0)
        {
            k1 = k1 + " " + "and WMaster.Week = " + week;
        }
        if (empid > 0)
        {
            k1 = k1 + " " + "and WMaster.EmployeeId = " + empid;
        }
        k2 = " order by BusinessMaster.BusinessName";

        k1 = k1 + k2;

      

        SqlCommand cmd1 = new SqlCommand(k1, con);
        SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);

        adp1.Fill(dt);
        con.Close();

        return dt;
    }






    public DataTable SelectRolebyCompany(int company_id)
    {
        PageConn pgcon = new PageConn();
        con = pgcon.dynconn; 
        DataTable dt = new DataTable();
        con.Close();
        con.Open();
        string k1;
        k1 = "Select * From RoleMaster Where compid = " + company_id;      
        SqlCommand cmd1 = new SqlCommand(k1, con);
        SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
        adp1.Fill(dt);
        con.Close();
        return dt;
    }

    public DataTable SelectUserbyCompany(int company_id)
    {
        DataTable dt = new DataTable();
        con.Close();
        con.Open();
        string k1;
        k1 = "SELECT UserMaster.UserId, UserMaster.UserName, UserMaster.Password, UserMaster.RoleId, UserMaster.EmployeeId"
	            + " FROM UserMaster INNER JOIN"
                + " RoleMaster ON UserMaster.RoleId = RoleMaster.Role_id"
                + " and RoleMaster.compid = " + company_id;
        SqlCommand cmd1 = new SqlCommand(k1, con);
        SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);
        adp1.Fill(dt);
        con.Close();
        return dt;
    }
    public DataTable selectProjectByCompany(int company_id)
    {
        DataTable dt = new DataTable();
        PageConn pgcon = new PageConn();
        con = pgcon.dynconn; 
        con.Close();
        con.Open();
        string k1;
        k1 = "SELECT  * FROM BusinessMaster INNER JOIN ProjectMaster ON BusinessMaster.BusinessID = ProjectMaster.BusinessId INNER JOIN Company_Business ON BusinessMaster.BusinessID = Company_Business.BusinessID and Company_Business.Company_id=" + company_id;
        k1 = k1 + " ORDER BY BusinessMaster.BusinessName, ProjectMaster.ProjectName";
        SqlCommand cmd1 = new SqlCommand(k1, con);
        SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);

        adp1.Fill(dt);
        con.Close();
        return dt;
    }

    public DataTable selectResourceTypeByCompany(int company_id)
    {
        DataTable dt = new DataTable();
        PageConn pgcon = new PageConn();
        con = pgcon.dynconn; 
        con.Close();
        con.Open();
        string k1;
        k1 = "SELECT * FROM BatchMaster where Status='1' and companyId = " + company_id;
        SqlCommand cmd1 = new SqlCommand(k1, con);
        SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);

        adp1.Fill(dt);
        con.Close();

        return dt;


    }
    public DataTable selectBatch(string Batchid)
    {
        DataTable dt = new DataTable();
        PageConn pgcon = new PageConn();
        con = pgcon.dynconn; 
        con.Close();
        con.Open();
        string k1;
        k1 = "SELECT * FROM BatchTiming where BatchMasterId  = " + Batchid;
        SqlCommand cmd1 = new SqlCommand(k1, con);
        SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);

        adp1.Fill(dt);
        con.Close();

        return dt;


    }
    public DataTable Tablemaster(String tbl)
    {
        DataTable dt = new DataTable();
        PageConn pgcon = new PageConn();
        con = pgcon.dynconn; 
        con.Close();
        con.Open();
      
      
        SqlCommand cmd1 = new SqlCommand(tbl, con);
        SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);

        adp1.Fill(dt);
        con.Close();

        return dt;


    }
    public DataTable SelectResourceUnitTypeByCompany(int company_id)
    {
        DataTable dt = new DataTable();
        PageConn pgcon = new PageConn();
        con = pgcon.dynconn; 
        con.Close();
        con.Open();
        string k1;
        k1 = "SELECT * FROM resourceunittype where company_id = " + company_id;
        SqlCommand cmd1 = new SqlCommand(k1, con);
        SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);

        adp1.Fill(dt);
        con.Close();

        return dt;


    }

    public DataTable SelectSupervisorByCompany(int company_id)
    {
        DataTable dt = new DataTable();
        PageConn pgcon = new PageConn();
        con = pgcon.dynconn; 
        con.Close();
        con.Open();
        string k1;
        k1 = "SELECT EmployeeMaster.EmployeeMasterID, EmployeeMaster.EmployeeName, UserMaster.RoleId"
                + " FROM EmployeeMaster INNER JOIN UserMaster ON EmployeeMaster.EmployeeMasterID = UserMaster.EmployeeId INNER JOIN"
                + " Company_Employee ON EmployeeMaster.EmployeeMasterID = Company_Employee.EmployeeID WHERE ((UserMaster.RoleId = 2) OR (UserMaster.RoleId = 1)) and Company_Employee.company_id =' " + company_id + "' and  EmployeeMaster.Active = '1' ";
        SqlCommand cmd1 = new SqlCommand(k1, con);
        SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);

        adp1.Fill(dt);
        con.Close();

        return dt;
    }

    public DataTable SelectStatusByCompany(String company_id)
    {
        DataTable dt = new DataTable();
        PageConn pgcon = new PageConn();
        con = pgcon.dynconn; 
        con.Close();
        con.Open();
        string k1;
        k1 = "SELECT [StatusId],[StatusName]  FROM [StatusMaster] inner join StatusCategory on  StatusCategory.StatusCategoryMasterId=StatusMaster.StatusCategoryMasterId where StatusCategory.StatusCategoryMasterId ='170'";
        SqlCommand cmd1 = new SqlCommand(k1, con);
        SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);

        adp1.Fill(dt);
        con.Close();

        return dt;
    }

    public DataTable SelectTaskMasterByCompany(int company_id)
    {
        DataTable dt = new DataTable();
        PageConn pgcon = new PageConn();
        con = pgcon.dynconn; 
        con.Close();
        con.Open();
        string k1;
        k1 = "SELECT TaskMaster.* FROM TaskMaster INNER JOIN"
                + " ProjectMaster ON TaskMaster.ProjectId = ProjectMaster.ProjectId INNER JOIN"
                + " Company_Business ON ProjectMaster.BusinessId = Company_Business.BusinessID where Company_Business.Company_id = " + company_id + " order by EstartDate";
        SqlCommand cmd1 = new SqlCommand(k1, con);
        SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);

        adp1.Fill(dt);
        con.Close();

        return dt;
    }

    public DataTable SelectEmployeeActiveByCompany(int company_id,int emp_id)
    {
        DataTable dt = new DataTable();
        PageConn pgcon = new PageConn();
        con = pgcon.dynconn; 
        con.Close();
        con.Open();
        string k1;
        k1 = "Select Active_Deactive From Company_Employee where company_id = " + company_id + " and EmployeeID = " + emp_id;
        SqlCommand cmd1 = new SqlCommand(k1, con);
        SqlDataAdapter adp1 = new SqlDataAdapter(cmd1);

        adp1.Fill(dt);
        con.Close();

        return dt;
    }
    public void UpdateActiveDeactiveEmployee(int active_deactive, int EmployeeID,int Company_id)
    {
        PageConn pgcon = new PageConn();
        con = pgcon.dynconn; 
        try
        {
            con.Close();
            cmd = new SqlCommand("UpdateActiveDeactiveEmployee", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@active_deactive", active_deactive);
            cmd.Parameters.AddWithValue("@EmployeeID", EmployeeID);
            cmd.Parameters.AddWithValue("@Company_id", Company_id);
            con.Open();
            cmd.ExecuteNonQuery();
            con.Close();



            return;
        }
        catch (Exception ex)
        {
            return;
        }
    }




}
