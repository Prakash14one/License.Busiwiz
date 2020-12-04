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
using System.Data.Common;
/// <summary>
/// Summary description for EmployeeCls
/// </summary>
public class EmployeeCls
{
    SqlCommand cmd;
    DataTable dt;
	public EmployeeCls()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public DataTable SelectEmployeeTypeMaster()
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectEmployeeTypeMaster";
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["CompanyLoginId"].ToString(); // CompanyLoginId;
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }
    public DataTable SelectEmployeeAddressTypeMaster()
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectEmployeeAddressTypeMaster";
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["CompanyLoginId"].ToString(); // CompanyLoginId;
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }

     //(@EmployeeTypeId,
     //      @AccountId,
     //      @StatusId, 
     //      @EmployeeName, 
     //      @SupervisorId)
    public Int32 InsertPartyAddressTypeMaster(string PartyAddressTypeName)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "InsertPartyAddressTypeMaster";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@PartyAddressTypeName", SqlDbType.NVarChar));
        cmd.Parameters["@PartyAddressTypeName"].Value = PartyAddressTypeName;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["CompanyLoginId"].ToString(); // CompanyLoginId;
        Int32 result = DatabaseCls1.ExecuteNonQuery(cmd);
        return result;
    }
    public Int32 InsertPartyTypeMaster(string PartyTypeName)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "InsertPartyTypeMaster";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@PartyTypeName", SqlDbType.NVarChar));
        cmd.Parameters["@PartyTypeName"].Value = PartyTypeName;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["CompanyLoginId"].ToString(); // CompanyLoginId;
        Int32 result = DatabaseCls1.ExecuteNonQuery(cmd);
        return result;
    }
    public void InserDocumentImageMaster(int Docid, string imgname)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "InserDocumentImageMaster";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@DocumentMasterId", SqlDbType.Int));
        cmd.Parameters["@DocumentMasterId"].Value = Docid;
        cmd.Parameters.Add(new SqlParameter("@DocumentImgName", SqlDbType.NVarChar));
        cmd.Parameters["@DocumentImgName"].Value = imgname;

        Int32 result = DatabaseCls1.ExecuteNonQueryep(cmd);


    }
    public Int32 InsertEmployeeTypeMaster(string EmployeeTypeName)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "InsertEmployeeTypeMaster";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@EmployeeTypeName", SqlDbType.NVarChar));
        cmd.Parameters["@EmployeeTypeName"].Value = EmployeeTypeName;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["CompanyLoginId"].ToString(); // CompanyLoginId;
        Int32 result = DatabaseCls1.ExecuteNonQuery(cmd);        
        return result;
    }
    public Int32 InsertRuleTypeMaster(string RuleType,String Whid)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "InsertRuleTypeMaster";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@RuleType", SqlDbType.NVarChar));
        cmd.Parameters["@RuleType"].Value = RuleType;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        cmd.Parameters.Add(new SqlParameter("@Whid", SqlDbType.NVarChar));
        cmd.Parameters["@Whid"].Value = Whid; // CompanyLoginId;
        Int32 result = DatabaseCls1.ExecuteNonQueryep(cmd);
        return result;
    }
    public Int32 InsertEmpAddTypeMaster(string EmployeeAddressTypeName)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "InsertEmpAddTypeMaster";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@EmployeeAddressTypeName", SqlDbType.NVarChar));
        cmd.Parameters["@EmployeeAddressTypeName"].Value = EmployeeAddressTypeName;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["CompanyLoginId"].ToString(); // CompanyLoginId;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        //cmd.Parameters.Add(new SqlParameter("@EmployeeAddressTypeId", SqlDbType.Int));
        //cmd.Parameters["@EmployeeAddressTypeId"].Direction = ParameterDirection.Output;

        Int32 result = DatabaseCls1.ExecuteNonQuery(cmd);
        //result = Convert.ToInt32(cmd.Parameters["@EmployeeAddressTypeId"].Value.ToString());
        return result;
    }
    public Int32 InsertSecurityQMaster(string SecurityQName)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "InsertSecurityQMaster";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@SecurityQName", SqlDbType.NVarChar));
        cmd.Parameters["@SecurityQName"].Value = SecurityQName;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["CompanyLoginId"].ToString(); // CompanyLoginId;
        Int32 result = DatabaseCls1.ExecuteNonQuery(cmd);
        return result;
    }
    public Int32 InsertEmployeeMaster(Int32 EmployeeTypeId, Int32 AccountId, Int32 StatusId, String EmployeeName, Int32 SupervisorId, String Email, Int32 DesignationId, Int32 PartyId, String ServerName, String Password,String TxtMsg,String HomeEmail)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "InsertEmployeeMaster";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@EmployeeTypeId", SqlDbType.Int));
        cmd.Parameters["@EmployeeTypeId"].Value = EmployeeTypeId;
        cmd.Parameters.Add(new SqlParameter("@AccountId", SqlDbType.Int));
        cmd.Parameters["@AccountId"].Value = AccountId;
        cmd.Parameters.Add(new SqlParameter("@StatusId", SqlDbType.Int));
        cmd.Parameters["@StatusId"].Value = StatusId;
        cmd.Parameters.Add(new SqlParameter("@EmployeeName", SqlDbType.NVarChar));
        cmd.Parameters["@EmployeeName"].Value = EmployeeName;

        cmd.Parameters.Add(new SqlParameter("@SupervisorId", SqlDbType.Int));
        cmd.Parameters["@SupervisorId"].Value = SupervisorId;
        cmd.Parameters.Add(new SqlParameter("@Email", SqlDbType.VarChar ));
        cmd.Parameters["@Email"].Value = Email;
        cmd.Parameters.Add(new SqlParameter("@DesignationId", SqlDbType.Int));
        cmd.Parameters["@DesignationId"].Value = DesignationId;
        cmd.Parameters.Add(new SqlParameter("@PartyId", SqlDbType.Int));
        cmd.Parameters["@PartyId"].Value = PartyId;
        cmd.Parameters.Add(new SqlParameter("@ServerName", SqlDbType.VarChar));
        cmd.Parameters["@ServerName"].Value = ServerName;
        cmd.Parameters.Add(new SqlParameter("@Password", SqlDbType.VarChar));
        cmd.Parameters["@Password"].Value = Password;
        cmd.Parameters.Add(new SqlParameter("@EmployeeSignature", SqlDbType.VarChar));
        cmd.Parameters["@EmployeeSignature"].Value = TxtMsg;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["CompanyLoginId"].ToString(); // CompanyLoginId;
        cmd.Parameters.Add(new SqlParameter("@HomeEmail", SqlDbType.VarChar));
        cmd.Parameters["@HomeEmail"].Value = HomeEmail;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        cmd.Parameters.Add(new SqlParameter("@EmployeeId", SqlDbType.Int));
        cmd.Parameters["@EmployeeId"].Direction = ParameterDirection.Output;

        Int32 result = DatabaseCls1.ExecuteNonQuery(cmd);
        result = Convert.ToInt32(cmd.Parameters["@EmployeeId"].Value.ToString());
        return result;
    }

    public Int32 InsertEmployeeAddressDetail(Int32 EmployeeId, Int32 EmployeeAddressTypeId, String Address, String City, String StateName, String PinCode, String ContactNo, String Fax, String CountryName, String ContactExt)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "InsertEmployeeAddressDetail";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@EmployeeId", SqlDbType.Int));
        cmd.Parameters["@EmployeeId"].Value = EmployeeId;
        cmd.Parameters.Add(new SqlParameter("@EmployeeAddressTypeId", SqlDbType.Int));
        cmd.Parameters["@EmployeeAddressTypeId"].Value = EmployeeAddressTypeId;
        cmd.Parameters.Add(new SqlParameter("@Address", SqlDbType.NVarChar));
        cmd.Parameters["@Address"].Value = Address;
        cmd.Parameters.Add(new SqlParameter("@City", SqlDbType.NVarChar));
        cmd.Parameters["@City"].Value = City;
        cmd.Parameters.Add(new SqlParameter("@StateName", SqlDbType.NVarChar ));
        cmd.Parameters["@StateName"].Value = StateName;
        cmd.Parameters.Add(new SqlParameter("@PinCode", SqlDbType.NVarChar));
        cmd.Parameters["@PinCode"].Value = PinCode;
        
        cmd.Parameters.Add(new SqlParameter("@Fax", SqlDbType.NVarChar));
        cmd.Parameters["@Fax"].Value = Fax;
        cmd.Parameters.Add(new SqlParameter("@ContactNo", SqlDbType.NVarChar));
        cmd.Parameters["@ContactNo"].Value = ContactNo;

        cmd.Parameters.Add(new SqlParameter("@ContactExt", SqlDbType.NVarChar));
        cmd.Parameters["@ContactExt"].Value = ContactExt;
        cmd.Parameters.Add(new SqlParameter("@CountryName", SqlDbType.NVarChar));
        cmd.Parameters["@CountryName"].Value = CountryName;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls1.ExecuteNonQuery(cmd);
        return result;
    }
    public Int32 InsertEmployeeDownloadtime(Int32 EmployeeId, DateTime LastDownloadTime, Boolean DocuAutoApprove)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "InsertEmployeeDownloadtime";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@EmployeeId", SqlDbType.Int));
        cmd.Parameters["@EmployeeId"].Value = EmployeeId;
        cmd.Parameters.Add(new SqlParameter("@LastDownloadTime", SqlDbType.DateTime));
        cmd.Parameters["@LastDownloadTime"].Value = LastDownloadTime;
        cmd.Parameters.Add(new SqlParameter("@DocuAutoApprove", SqlDbType.Bit));
        cmd.Parameters["@DocuAutoApprove"].Value = DocuAutoApprove;        
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls1.ExecuteNonQuery(cmd);
        return result;
    }
    public Int32 UpdateEmployeeMaster(Int32 EmployeeId,   String Address, String City, String StateName, String PinCode, String ContactNo,  String CountryName)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "InsertEmployeeAddressDetail";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@EmployeeId", SqlDbType.Int));
        cmd.Parameters["@EmployeeId"].Value = EmployeeId;
        
        cmd.Parameters.Add(new SqlParameter("@Address", SqlDbType.NVarChar));
        cmd.Parameters["@Address"].Value = Address;
        cmd.Parameters.Add(new SqlParameter("@City", SqlDbType.NVarChar));
        cmd.Parameters["@City"].Value = City;
        cmd.Parameters.Add(new SqlParameter("@StateName", SqlDbType.NVarChar));
        cmd.Parameters["@StateName"].Value = StateName;
        cmd.Parameters.Add(new SqlParameter("@PinCode", SqlDbType.NVarChar));
        cmd.Parameters["@PinCode"].Value = PinCode;

        
        cmd.Parameters.Add(new SqlParameter("@Phone", SqlDbType.NVarChar));
        cmd.Parameters["@Phone"].Value = ContactNo;
        cmd.Parameters.Add(new SqlParameter("@CountryName", SqlDbType.NVarChar));
        cmd.Parameters["@CountryName"].Value = CountryName;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls1.ExecuteNonQuery(cmd);
        return result;
    }
    public Int32 InsertEmployeeLoginMaster(Int32 EmployeeId, String EmployeeLoginName, String EmployeeLoginPassword)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "InsertEmployeeLoginMaster";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@EmployeeId", SqlDbType.Int));
        cmd.Parameters["@EmployeeId"].Value = EmployeeId;
        cmd.Parameters.Add(new SqlParameter("@UserName", SqlDbType.NVarChar));
        cmd.Parameters["@UserName"].Value = EmployeeLoginName;
        cmd.Parameters.Add(new SqlParameter("@UserPassword", SqlDbType.NVarChar));
        cmd.Parameters["@UserPassword"].Value = EmployeeLoginPassword;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["CompanyLoginId"].ToString(); // CompanyLoginId;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls1.ExecuteNonQuery(cmd);
        return result;
    }
    public Int32 InsertLoginMasterByPartyID(Int32 PartyID, String EmployeeLoginName, String EmployeeLoginPassword, String Company)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "InsertLoginMasterByPartyID";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@PartyID", SqlDbType.Int));
        cmd.Parameters["@PartyID"].Value = PartyID;
        cmd.Parameters.Add(new SqlParameter("@UserName", SqlDbType.NVarChar));
        cmd.Parameters["@UserName"].Value = EmployeeLoginName;
        cmd.Parameters.Add(new SqlParameter("@UserPassword", SqlDbType.NVarChar));
        cmd.Parameters["@UserPassword"].Value = EmployeeLoginPassword;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = Company; // CompanyLoginId;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls1.ExecuteNonQuery(cmd);
        return result;
    }
    public Int32 InsertSessionMaster(Int32 PartyID, String IP, DateTime StartTime, DateTime EndTime)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "InsertSessionMaster";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@PartyID", SqlDbType.Int));
        cmd.Parameters["@PartyID"].Value = PartyID;
        cmd.Parameters.Add(new SqlParameter("@IP", SqlDbType.NVarChar));
        cmd.Parameters["@IP"].Value = IP;
        cmd.Parameters.Add(new SqlParameter("@StartTime", SqlDbType.DateTime));
        cmd.Parameters["@StartTime"].Value = StartTime;
        cmd.Parameters.Add(new SqlParameter("@EndTime", SqlDbType.DateTime));
        cmd.Parameters["@EndTime"].Value = EndTime;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["CompanyLoginId"].ToString(); // CompanyLoginId;
        cmd.Parameters.Add(new SqlParameter("@SessionID", SqlDbType.Int));
        cmd.Parameters["@SessionID"].Direction = ParameterDirection.Output;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls1.ExecuteNonQuery(cmd);
        result = Convert.ToInt32(cmd.Parameters["@SessionID"].Value);
        return (result);
    }
    public String InsertSessionDetailMaster(String SessionID, String PageName)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "InsertSessionDetailMaster";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@SessionID", SqlDbType.Int));
        cmd.Parameters["@SessionID"].Value = SessionID;        
        cmd.Parameters.Add(new SqlParameter("@PageName", SqlDbType.NVarChar));
        cmd.Parameters["@PageName"].Value = PageName;
        cmd.Parameters.Add(new SqlParameter("@SessionDetailID", SqlDbType.Int));
        cmd.Parameters["@SessionDetailID"].Direction = ParameterDirection.Output;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
       String result = DatabaseCls1.ExecuteNonQuery(cmd).ToString();
       result = cmd.Parameters["@SessionID"].Value.ToString() ;
        return (result);
    }
    public bool UpdateSessionMaster(Int32 SessionID, DateTime EndTime)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "UpdateSessionMaster";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@SessionID", SqlDbType.Int));
        cmd.Parameters["@SessionID"].Value = SessionID;
        cmd.Parameters.Add(new SqlParameter("@EndTime", SqlDbType.DateTime));
        cmd.Parameters["@EndTime"].Value = EndTime;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["CompanyLoginId"].ToString(); // CompanyLoginId;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;


        Int32 result = DatabaseCls1.ExecuteNonQuery(cmd);
        result = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value.ToString());

        return (result != -1);
    }
    public DataTable SelectEmployeeMasterAll()
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectEmployeeMasterAll";
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["CompanyLoginId"].ToString(); // CompanyLoginId;
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }



    public DataTable SelectEmployeeMasterbyId(Int32 EmployeeId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectEmployeeMasterbyId";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@EmployeeId", SqlDbType.Int));
        cmd.Parameters["@EmployeeId"].Value = EmployeeId;
         
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;

        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;
    }
    public DataTable SelectEmployeeMasterbyDeptId(Int32 DepartmentId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectEmployeeMasterbyDeptId";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@DepartmentId", SqlDbType.Int));
        cmd.Parameters["@DepartmentId"].Value = DepartmentId;

        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;

        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }
    public DataTable SelectEmployeeMasterbyStatusId(Int32 StatusId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectEmployeeMasterbyStatusId";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@StatusId", SqlDbType.Int));
        cmd.Parameters["@StatusId"].Value = StatusId;

        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;

        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }
    public DataTable SelectPartyEmailbypartyid(Int32 PartyId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectPartyEmailbypartyid";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@PartyId", SqlDbType.Int));
        cmd.Parameters["@PartyId"].Value = PartyId;

        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;

        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;
    }
    public DataTable SelectEmployeeAddressDetail(Int32 EmployeeId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectEmployeeAddressDetail";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@EmployeeId", SqlDbType.Int));
        cmd.Parameters["@EmployeeId"].Value = EmployeeId;

        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;

        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }

    public bool UpdateEmployeeMaster(Int32 EmployeeId, Int32 EmployeeTypeId, Int32 StatusId, String EmployeeName, Int32 SupervisorId, String Email, Int32 DesignationId, String ServerName, String Password, String EmployeeSignature,String HomeEmail)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "UpdateEmployeeMaster";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@EmployeeId", SqlDbType.Int));
        cmd.Parameters["@EmployeeId"].Value = EmployeeId;
        cmd.Parameters.Add(new SqlParameter("@EmployeeTypeId", SqlDbType.Int));
        cmd.Parameters["@EmployeeTypeId"].Value = EmployeeTypeId;
        
        cmd.Parameters.Add(new SqlParameter("@StatusId", SqlDbType.Int));
        cmd.Parameters["@StatusId"].Value = StatusId;
        cmd.Parameters.Add(new SqlParameter("@EmployeeName", SqlDbType.NVarChar));
        cmd.Parameters["@EmployeeName"].Value = EmployeeName;

        cmd.Parameters.Add(new SqlParameter("@SupervisorId", SqlDbType.Int));
        cmd.Parameters["@SupervisorId"].Value = SupervisorId;
        cmd.Parameters.Add(new SqlParameter("@Email", SqlDbType.VarChar));
        cmd.Parameters["@Email"].Value = Email;
        cmd.Parameters.Add(new SqlParameter("@ServerName", SqlDbType.VarChar));
        cmd.Parameters["@ServerName"].Value = ServerName;
        cmd.Parameters.Add(new SqlParameter("@Password", SqlDbType.VarChar));
        cmd.Parameters["@Password"].Value = Password;
        cmd.Parameters.Add(new SqlParameter("@EmployeeSignature", SqlDbType.VarChar));
        cmd.Parameters["@EmployeeSignature"].Value = EmployeeSignature;
        cmd.Parameters.Add(new SqlParameter("@DesignationId", SqlDbType.Int));
        cmd.Parameters["@DesignationId"].Value = DesignationId;
        cmd.Parameters.Add(new SqlParameter("@HomeEmail", SqlDbType.VarChar));
        cmd.Parameters["@HomeEmail"].Value = HomeEmail;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
         

        Int32 result = DatabaseCls1.ExecuteNonQuery(cmd);
        result = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value.ToString());
          
        return (result != -1);
    }
    public bool DeleteEmployeeAddressDetail(Int32 EmployeeId)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "DeleteEmployeeAddressDetail";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@EmployeeId", SqlDbType.Int));
        cmd.Parameters["@EmployeeId"].Value = EmployeeId;    
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls1.ExecuteNonQuery(cmd);
        result = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value.ToString());
        return (result != -1);
    }
    public bool DeleteEmployeeMaster(Int32 EmployeeId)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "DeleteEmployeeMaster";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@EmployeeId", SqlDbType.Int));
        cmd.Parameters["@EmployeeId"].Value = EmployeeId;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["CompanyLoginId"].ToString(); // CompanyLoginId;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls1.ExecuteNonQuery(cmd);
        result = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value.ToString());
        return (result != -1);
    }
    public bool DeleteEmployeeTypeByID(Int32 EmployeeTypeId)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "DeleteEmployeeTypeByID";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@EmployeeTypeId", SqlDbType.Int));
        cmd.Parameters["@EmployeeTypeId"].Value = EmployeeTypeId;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["CompanyLoginId"].ToString(); // CompanyLoginId;

        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        int result = DatabaseCls1.ExecuteNonQuery(cmd);
        return (result != -1);
    }
    public bool DeleteEmployeeAddressTypeByID(Int32 EmployeeAddressTypeId)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "DeleteEmployeeAddressTypeByID";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@EmployeeAddressTypeId", SqlDbType.Int));
        cmd.Parameters["@EmployeeAddressTypeId"].Value = EmployeeAddressTypeId;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["CompanyLoginId"].ToString(); // CompanyLoginId;

        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        int result = DatabaseCls1.ExecuteNonQuery(cmd);
        return (result != -1);
    }
    /////// 9-3-09
    public DataTable SelectEmployeeForSalesMan()
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectEmployeeForSalesMan";
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }
    public bool DeletePartyTypeMaster(Int32 PartyTypeId)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "DeletePartyTypeMaster";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@PartyTypeId", SqlDbType.Int));
        cmd.Parameters["@PartyTypeId"].Value = PartyTypeId;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["CompanyLoginId"].ToString(); // CompanyLoginId;

        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        int result = DatabaseCls1.ExecuteNonQuery(cmd);
        return (result != -1);
    }
    public bool DeletePartyAddressMaster(Int32 PartyAddressTypeId)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "DeletePartyAddressMaster";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@PartyAddressTypeId", SqlDbType.Int));
        cmd.Parameters["@PartyAddressTypeId"].Value = PartyAddressTypeId;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["CompanyLoginId"].ToString(); // CompanyLoginId;

        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        int result = DatabaseCls1.ExecuteNonQuery(cmd);
        return (result != -1);
    }
    //------------------haiyal 1-4-2009---------------------
    //InsertNewUserMasterWith_emp


    public Int32 InsertNewUserMasterWith_cust_vend(String NewUserName, Int32 NewUserTypeId,
        Int32 SecurityQId, String SecurityAns,
        Int32 StatusId,
        String PartyName,
        String ContactPerson, Int32 CountryId, Int32 StateId,
         String Address, String City,
         String Pintcode, String Email,
          String ContactNo, String ContactNoExtn,
         String Fax, String Website, DateTime Date)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "InsertNewUserMasterWith_cust_vend";
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.Add(new SqlParameter("@NewUserName", SqlDbType.NVarChar));
        cmd.Parameters["@NewUserName"].Value = NewUserName;

        cmd.Parameters.Add(new SqlParameter("@NewUserTypeId", SqlDbType.Int));
        cmd.Parameters["@NewUserTypeId"].Value = NewUserTypeId;
        cmd.Parameters.Add(new SqlParameter("@SecurityQId", SqlDbType.Int));
        cmd.Parameters["@SecurityQId"].Value = SecurityQId;

        cmd.Parameters.Add(new SqlParameter("@SecurityAns", SqlDbType.NVarChar));
        cmd.Parameters["@SecurityAns"].Value = SecurityAns;

        cmd.Parameters.Add(new SqlParameter("@StatusId", SqlDbType.Int));
        cmd.Parameters["@StatusId"].Value = StatusId;

        cmd.Parameters.Add(new SqlParameter("@PartyName", SqlDbType.NVarChar));
        cmd.Parameters["@PartyName"].Value = PartyName;
        cmd.Parameters.Add(new SqlParameter("@ContactPerson", SqlDbType.NVarChar));
        cmd.Parameters["@ContactPerson"].Value = ContactPerson;

        cmd.Parameters.Add(new SqlParameter("@CountryId", SqlDbType.Int));
        cmd.Parameters["@CountryId"].Value = CountryId;
        cmd.Parameters.Add(new SqlParameter("@StateId", SqlDbType.Int));
        cmd.Parameters["@StateId"].Value = StateId;

        cmd.Parameters.Add(new SqlParameter("@Address", SqlDbType.NVarChar));
        cmd.Parameters["@Address"].Value = Address;
        cmd.Parameters.Add(new SqlParameter("@City", SqlDbType.NVarChar));
        cmd.Parameters["@City"].Value = City;
        cmd.Parameters.Add(new SqlParameter("@Pintcode", SqlDbType.NVarChar));
        cmd.Parameters["@Pintcode"].Value = Pintcode;
        cmd.Parameters.Add(new SqlParameter("@Email", SqlDbType.NVarChar));
        cmd.Parameters["@Email"].Value = Email;
        cmd.Parameters.Add(new SqlParameter("@ContactNo", SqlDbType.NVarChar));
        cmd.Parameters["@ContactNo"].Value = ContactNo;
        cmd.Parameters.Add(new SqlParameter("@ContactNoExtn", SqlDbType.NVarChar));
        cmd.Parameters["@ContactNoExtn"].Value = ContactNoExtn;
        cmd.Parameters.Add(new SqlParameter("@Fax", SqlDbType.NVarChar));
        cmd.Parameters["@Fax"].Value = Fax;
        cmd.Parameters.Add(new SqlParameter("@Website", SqlDbType.NVarChar));
        cmd.Parameters["@Website"].Value = Website;
        cmd.Parameters.Add(new SqlParameter("@Date", SqlDbType.DateTime));
        cmd.Parameters["@Date"].Value = Date;

        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls1.ExecuteNonQuery(cmd);
        return result;

    }



    public Int32 InsertNewUserMasterWith_emp(String NewUserName, Int32 NewUserTypeId,
        Int32 SecurityQId, String SecurityAns, Int32 StatusId,

        Int32 DepartmentId,
          Int32 DesignationId,
         Int32 EmployeeTypeId,
         String LoginId,
         String Password,


        Int32 CountryId, Int32 StateId,
         String Address, String City,
         String Pintcode, String Email,
          String ContactNo, String ContactNoExtn,
         String Fax, String Website,
        DateTime Date)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "InsertNewUserMasterWith_emp";
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.Add(new SqlParameter("@NewUserName", SqlDbType.NVarChar));
        cmd.Parameters["@NewUserName"].Value = NewUserName;

        cmd.Parameters.Add(new SqlParameter("@NewUserTypeId", SqlDbType.Int));
        cmd.Parameters["@NewUserTypeId"].Value = NewUserTypeId;
        cmd.Parameters.Add(new SqlParameter("@SecurityQId", SqlDbType.Int));
        cmd.Parameters["@SecurityQId"].Value = SecurityQId;

        cmd.Parameters.Add(new SqlParameter("@SecurityAns", SqlDbType.NVarChar));
        cmd.Parameters["@SecurityAns"].Value = SecurityAns;
        cmd.Parameters.Add(new SqlParameter("@StatusId", SqlDbType.Int));
        cmd.Parameters["@StatusId"].Value = StatusId;


        cmd.Parameters.Add(new SqlParameter("@DepartmentId", SqlDbType.Int));
        cmd.Parameters["@DepartmentId"].Value = DepartmentId;
        cmd.Parameters.Add(new SqlParameter("@DesignationId", SqlDbType.Int));
        cmd.Parameters["@DesignationId"].Value = DesignationId;
        cmd.Parameters.Add(new SqlParameter("@EmployeeTypeId", SqlDbType.Int));
        cmd.Parameters["@EmployeeTypeId"].Value = EmployeeTypeId;
        cmd.Parameters.Add(new SqlParameter("@LoginId", SqlDbType.NVarChar));
        cmd.Parameters["@LoginId"].Value = LoginId;
        cmd.Parameters.Add(new SqlParameter("@Password", SqlDbType.NVarChar));
        cmd.Parameters["@Password"].Value = Password;

        cmd.Parameters.Add(new SqlParameter("@CountryId", SqlDbType.Int));
        cmd.Parameters["@CountryId"].Value = CountryId;
        cmd.Parameters.Add(new SqlParameter("@StateId", SqlDbType.Int));
        cmd.Parameters["@StateId"].Value = StateId;

        cmd.Parameters.Add(new SqlParameter("@Address", SqlDbType.NVarChar));
        cmd.Parameters["@Address"].Value = Address;
        cmd.Parameters.Add(new SqlParameter("@City", SqlDbType.NVarChar));
        cmd.Parameters["@City"].Value = City;
        cmd.Parameters.Add(new SqlParameter("@Pintcode", SqlDbType.NVarChar));
        cmd.Parameters["@Pintcode"].Value = Pintcode;
        cmd.Parameters.Add(new SqlParameter("@Email", SqlDbType.NVarChar));
        cmd.Parameters["@Email"].Value = Email;
        cmd.Parameters.Add(new SqlParameter("@ContactNo", SqlDbType.NVarChar));
        cmd.Parameters["@ContactNo"].Value = ContactNo;
        cmd.Parameters.Add(new SqlParameter("@ContactNoExtn", SqlDbType.NVarChar));
        cmd.Parameters["@ContactNoExtn"].Value = ContactNoExtn;
        cmd.Parameters.Add(new SqlParameter("@Fax", SqlDbType.NVarChar));
        cmd.Parameters["@Fax"].Value = Fax;
        cmd.Parameters.Add(new SqlParameter("@Website", SqlDbType.NVarChar));
        cmd.Parameters["@Website"].Value = Website;

        cmd.Parameters.Add(new SqlParameter("@Date", SqlDbType.DateTime));
        cmd.Parameters["@Date"].Value = Date;

        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls1.ExecuteNonQuery(cmd);
        return result;

    }

    public DataTable SelectSecurityQMaster()
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectSecurityQMaster";
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["CompanyLoginId"].ToString(); // CompanyLoginId;
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }
    public bool DeleteSecurityQAnsbyId(Int32 SecurityQId)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "DeleteSecurityQAnsbyId";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@SecurityQId", SqlDbType.Int));
        cmd.Parameters["@SecurityQId"].Value = SecurityQId;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["CompanyLoginId"].ToString(); // CompanyLoginId;

        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        int result = DatabaseCls1.ExecuteNonQuery(cmd);
        return (result != -1);
    }
    public DataTable SelectSecurityQMasterfornewuser(String company)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectSecurityQMasterfornewuser";
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = company; // CompanyLoginId;
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }
    public bool UpdateSecurityQMaster(Int32 SecurityQId, String SecurityQName)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "UpdateSecurityQMaster";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@SecurityQId", SqlDbType.Int));
        cmd.Parameters["@SecurityQId"].Value = SecurityQId;
        cmd.Parameters.Add(new SqlParameter("@SecurityQName", SqlDbType.NVarChar));
        cmd.Parameters["@SecurityQName"].Value = SecurityQName;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        int result = DatabaseCls1.ExecuteNonQuery(cmd);
        return (result != -1);
    }
    public bool UpdateRuleTypeMaster(Int32 RuleTypeId, String RuleType,String Whid)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "UpdateRuleTypeMaster";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@RuleTypeId", SqlDbType.Int));
        cmd.Parameters["@RuleTypeId"].Value = RuleTypeId;
        cmd.Parameters.Add(new SqlParameter("@RuleType", SqlDbType.NVarChar));
        cmd.Parameters["@RuleType"].Value = RuleType;
        cmd.Parameters.Add(new SqlParameter("@Whid", SqlDbType.NVarChar));
        cmd.Parameters["@Whid"].Value = Whid;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        int result = DatabaseCls1.ExecuteNonQueryep(cmd);
        return (result != -1);
    }
    public DataTable SelectNewUserMaster()
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectNewUserMaster";
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }
    //SelectNewUserMasterWithId
    public DataTable SelectNewUserMasterWithId(Int32 NewUserId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectNewUserMasterWithId";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@NewUserId", SqlDbType.Int));
        cmd.Parameters["@NewUserId"].Value = NewUserId;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["CompanyLoginId"].ToString(); // CompanyLoginId;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;

        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }
    //UpdateNewUserMaster
    //    @NewUserId int,
    //@StatusId int
    public bool UpdateNewUserMaster(Int32 NewUserId, Int32 StatusId)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "UpdateNewUserMaster";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@NewUserId", SqlDbType.Int));
        cmd.Parameters["@NewUserId"].Value = NewUserId;
        cmd.Parameters.Add(new SqlParameter("@StatusId", SqlDbType.Int));
        cmd.Parameters["@StatusId"].Value = StatusId;

        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;


        Int32 result = DatabaseCls1.ExecuteNonQuery(cmd);
        result = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value.ToString());

        return (result != -1);
    }

    public DataTable SelectUserWithForgetPassword_ByLoginId(String UserName,String companyId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectUserWithForgetPassword_ByLoginId";
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.Add(new SqlParameter("@UserName", SqlDbType.NVarChar));
        cmd.Parameters["@UserName"].Value = UserName;

        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = companyId;

        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;

        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }


    ////--------haiyal 3-4-2009

    //@SecurityQId int,
    //      @PartyId int,
    //      @SecurityAnsName nvarchar(50)

    //InsertSecurityAnsEntry
    public Int32 InsertSecurityAnsEntry(Int32 SecurityQId, Int32 PartyId, String SecurityAnsName)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "InsertSecurityAnsEntry";
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.Add(new SqlParameter("@SecurityQId", SqlDbType.Int));
        cmd.Parameters["@SecurityQId"].Value = SecurityQId;

        cmd.Parameters.Add(new SqlParameter("@PartyId", SqlDbType.Int));
        cmd.Parameters["@PartyId"].Value = PartyId;


        cmd.Parameters.Add(new SqlParameter("@SecurityAnsName", SqlDbType.NVarChar));
        cmd.Parameters["@SecurityAnsName"].Value = SecurityAnsName;

        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["CompanyLoginId"].ToString(); // CompanyLoginId;

        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;

        Int32 result = DatabaseCls1.ExecuteNonQuery(cmd);
        return result;
    }
    public Int32 InsertSecurityAnsEntryfromNewUser(Int32 SecurityQId, Int32 PartyId, String SecurityAnsName,String Company)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "InsertSecurityAnsEntry";
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.Add(new SqlParameter("@SecurityQId", SqlDbType.Int));
        cmd.Parameters["@SecurityQId"].Value = SecurityQId;

        cmd.Parameters.Add(new SqlParameter("@PartyId", SqlDbType.Int));
        cmd.Parameters["@PartyId"].Value = PartyId;


        cmd.Parameters.Add(new SqlParameter("@SecurityAnsName", SqlDbType.NVarChar));
        cmd.Parameters["@SecurityAnsName"].Value = SecurityAnsName;

        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = Company; // CompanyLoginId;

        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;

        Int32 result = DatabaseCls1.ExecuteNonQuery(cmd);
        return result;
    }


    public DataTable SelectUserWithForgetPassword_ByEmail(String Email, String companyId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectUserWithForgetPassword_ByEmail";
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.Add(new SqlParameter("@Email", SqlDbType.NVarChar));
        cmd.Parameters["@Email"].Value = Email;

        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = companyId;

        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;

        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }

    //[SelectUserWithForgetPassword_Email_&&_Login]
    public DataTable SelectUserWithForgetPassword_Email_Login(String Email, String UserName, String companyId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectUserWithForgetPassword_Email_Login";
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.Add(new SqlParameter("@Email", SqlDbType.NVarChar));
        cmd.Parameters["@Email"].Value = Email;

        cmd.Parameters.Add(new SqlParameter("@UserName", SqlDbType.NVarChar));
        cmd.Parameters["@UserName"].Value = UserName;

        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = companyId;

        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;

        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }
    // Neetu 8-4-09
    public bool UpdateEmployeeMasterStatus(Int32 EmployeeId, Int32 StatusId)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "UpdateEmployeeMasterStatus";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@EmployeeId", SqlDbType.Int));
        cmd.Parameters["@EmployeeId"].Value = EmployeeId;
        cmd.Parameters.Add(new SqlParameter("@StatusId", SqlDbType.Int));
        cmd.Parameters["@StatusId"].Value = StatusId;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["CompanyLoginId"].ToString(); // CompanyLoginId;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;


        Int32 result = DatabaseCls1.ExecuteNonQuery(cmd);
        result = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value.ToString());

        return (result != -1);
    }
    public bool UpdatePartyMasterStatus(Int32 PartyId, Int32 StatusId)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "UpdatePartyMasterStatus";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@PartyId", SqlDbType.Int));
        cmd.Parameters["@PartyId"].Value = PartyId;
        cmd.Parameters.Add(new SqlParameter("@StatusId", SqlDbType.Int));
        cmd.Parameters["@StatusId"].Value = StatusId;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["CompanyLoginId"].ToString(); // CompanyLoginId;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;


        Int32 result = DatabaseCls1.ExecuteNonQuery(cmd);
        result = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value.ToString());

        return (result != -1);
    }
    public bool UpdateEmployeeMasterDesignation(Int32 EmployeeId, Int32 DesignationId, String EmployeeName)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "UpdateEmployeeMasterDesignation";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@EmployeeId", SqlDbType.Int));
        cmd.Parameters["@EmployeeId"].Value = EmployeeId;
        cmd.Parameters.Add(new SqlParameter("@DesignationId", SqlDbType.Int));
        cmd.Parameters["@DesignationId"].Value = DesignationId;
        cmd.Parameters.Add(new SqlParameter("@EmployeeName", SqlDbType.NVarChar));
        cmd.Parameters["@EmployeeName"].Value = EmployeeName;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["CompanyLoginId"].ToString(); // CompanyLoginId;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;


        Int32 result = DatabaseCls1.ExecuteNonQuery(cmd);
        result = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value.ToString());

        return (result != -1);
    }
    public bool UpdateEmployeeMasterByEmployeeType(Int32 EmployeeId, Int32 EmployeeTypeId, String EmployeeName)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "UpdateEmployeeMasterByEmployeeType";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@EmployeeId", SqlDbType.Int));
        cmd.Parameters["@EmployeeId"].Value = EmployeeId;
        cmd.Parameters.Add(new SqlParameter("@EmployeeTypeId", SqlDbType.Int));
        cmd.Parameters["@EmployeeTypeId"].Value = EmployeeTypeId;
        cmd.Parameters.Add(new SqlParameter("@EmployeeName", SqlDbType.NVarChar));
        cmd.Parameters["@EmployeeName"].Value = EmployeeName;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["CompanyLoginId"].ToString(); // CompanyLoginId;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;


        Int32 result = DatabaseCls1.ExecuteNonQuery(cmd);
        result = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value.ToString());

        return (result != -1);
    }
    public bool UpdateEmployeeMasterByEmployeeAddressType(Int32 EmployeeId, Int32 EmployeeAddressTypeId)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "UpdateEmployeeMasterByEmployeeAddressType";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@EmployeeId", SqlDbType.Int));
        cmd.Parameters["@EmployeeId"].Value = EmployeeId;
        cmd.Parameters.Add(new SqlParameter("@EmployeeAddressTypeId", SqlDbType.Int));
        cmd.Parameters["@EmployeeAddressTypeId"].Value = EmployeeAddressTypeId;       
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;


        Int32 result = DatabaseCls1.ExecuteNonQuery(cmd);
        result = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value.ToString());

        return (result != -1);
    }
    public bool UpdatePartyMasterByPartyType(Int32 PartyId, Int32 PartyTypeId)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "UpdatePartyMasterByPartyType";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@PartyId", SqlDbType.Int));
        cmd.Parameters["@PartyId"].Value = PartyId;
        cmd.Parameters.Add(new SqlParameter("@PartyTypeId", SqlDbType.Int));
        cmd.Parameters["@PartyTypeId"].Value = PartyTypeId;        
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["CompanyLoginId"].ToString(); // CompanyLoginId;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;

        Int32 result = DatabaseCls1.ExecuteNonQuery(cmd);
        result = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value.ToString());

        return (result != -1);
    }
    public bool UpdatePartyMasterByPartyAddressType(Int32 PartyId, Int32 PartyAddressTypeId)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "UpdatePartyMasterByPartyAddressType";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@PartyId", SqlDbType.Int));
        cmd.Parameters["@PartyId"].Value = PartyId;
        cmd.Parameters.Add(new SqlParameter("@PartyAddressTypeId", SqlDbType.Int));
        cmd.Parameters["@PartyAddressTypeId"].Value = PartyAddressTypeId;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;


        Int32 result = DatabaseCls1.ExecuteNonQuery(cmd);
        result = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value.ToString());

        return (result != -1);
    }
}
