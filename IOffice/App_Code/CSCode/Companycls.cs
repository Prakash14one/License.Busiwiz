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
/// Summary description for Companycls
/// </summary>
public class Companycls
{
    SqlCommand cmd;
    DataTable dt;
	public Companycls()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public DataTable selectOrganiseTypeMaster()
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "selectOrganiseTypeMaster";
        //cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        //cmd.Parameters["@CID"].Value = HttpContext.Current.Session["CompanyLoginId"].ToString(); // CompanyLoginId;
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }
    public DataTable selectIndustryTypeMaster()
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "selectIndustryTypeMaster";
        //cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        //cmd.Parameters["@CID"].Value = HttpContext.Current.Session["CompanyLoginId"].ToString(); // CompanyLoginId;
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }
    public bool DeleteIndustryTypeByID(Int32 IndustryTypeId)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "DeleteIndustryTypeByID";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@IndustryTypeId", SqlDbType.Int));
        cmd.Parameters["@IndustryTypeId"].Value = IndustryTypeId;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls1.ExecuteNonQuery(cmd);
        return (result != -1);
    }
    public bool DeleteOrganiseTypeByID(Int32 OrganiseTypeId)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "DeleteOrganiseTypeByID";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@OrganiseTypeId", SqlDbType.Int));
        cmd.Parameters["@OrganiseTypeId"].Value = OrganiseTypeId;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls1.ExecuteNonQuery(cmd);
        return (result != -1);
    }
    public bool UpdateCompanyMasterByIndustryType(Int32 CompanyId, Int32 IndustryTypeId)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "UpdateCompanyMasterByIndustryType";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int));
        cmd.Parameters["@CompanyId"].Value = CompanyId;
        cmd.Parameters.Add(new SqlParameter("@IndustryTypeId", SqlDbType.Int));
        cmd.Parameters["@IndustryTypeId"].Value = IndustryTypeId;       
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;

        Int32 result = DatabaseCls1.ExecuteNonQuery(cmd);
        result = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value.ToString());

        return (result != -1);
    }
    public bool UpdateCompanyMasterByOrganiseType(Int32 CompanyId, Int32 OrganiseTypeId)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "UpdateCompanyMasterByOrganiseType";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int));
        cmd.Parameters["@CompanyId"].Value = CompanyId;
        cmd.Parameters.Add(new SqlParameter("@OrganiseTypeId", SqlDbType.Int));
        cmd.Parameters["@OrganiseTypeId"].Value = OrganiseTypeId;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;

        Int32 result = DatabaseCls1.ExecuteNonQuery(cmd);
        result = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value.ToString());

        return (result != -1);
    }
    public DataTable selectCompanyMaster()
    {
        cmd = new SqlCommand();
       DataTable  dtr = new DataTable();
        cmd.CommandText = "selectCompanyMaster";
        cmd.Parameters.Add(new SqlParameter("@CompanyName", SqlDbType.NVarChar));
        cmd.Parameters["@CompanyName"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        dtr = DatabaseCls1.FilleppAdapter(cmd);
        return dtr;
    }
    public DataTable selectIPMaster()
    {
        cmd = new SqlCommand();
        DataTable dtIp = new DataTable();
         
        cmd.CommandText = "selectIPMaster";
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["CompanyLoginId"].ToString(); // CompanyLoginId;
        dtIp = DatabaseCls1.FillAdapter(cmd);
        return dtIp;
    }
    public DataTable selectCompanyEmailMaster()
    {
        cmd = new SqlCommand();
        DataTable dtIp = new DataTable();

        cmd.CommandText = "selectCompanyEmailMaster";
        dtIp = DatabaseCls1.FillAdapter(cmd);
        return dtIp;
    }
    public DataTable selectCompanyAddressTypeMaster()
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "selectCompanyAddressTypeMaster";
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }
    public Int32  UpdateCompanyMasterforIP(bool IPStatus)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "UpdateCompanyMasterforIP";
        cmd.CommandType = CommandType.StoredProcedure; 
        cmd.Parameters.Add(new SqlParameter("@IP", SqlDbType.Bit ));
        cmd.Parameters["@IP"].Value = IPStatus;

        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;       
        Int32 result  =DatabaseCls1.ExecuteNonQuery(cmd);
         result = 0;
         result = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value.ToString());
        return result;
    }
    public Int32 UpdateCompanymansterAllowIp(bool IPStatus)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "UpdateCompanymansterAllowIp";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@IP", SqlDbType.Bit));
        cmd.Parameters["@IP"].Value = IPStatus;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls1.ExecuteNonQuery(cmd);
        result = 0;
        result = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value.ToString());
        return result;
    }
    public Int32 InsertIPAddress(String IPAddress)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "InsertIPMaster";
        cmd.CommandType = CommandType.StoredProcedure; 
        cmd.Parameters.Add(new SqlParameter("@IPAddress", SqlDbType.NVarChar ));
        cmd.Parameters["@IPAddress"].Value = IPAddress;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["CompanyLoginId"].ToString(); // CompanyLoginId;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = Convert.ToInt32(DatabaseCls1.ExecuteNonQuery(cmd));
        result = 0;
        result = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value.ToString());
        return result;
    }
   
    public Int32 DeleteIPAddress(Int32  IPId)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "DeleteIPMaster";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@IPId", SqlDbType.Int ));
        cmd.Parameters["@IPId"].Value = IPId;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls1.ExecuteNonQuery(cmd);
        result = 0;
        result = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value.ToString());
        return result;
    }

    public Int32 InsertCompanyMaster(Int32 IndustryTypeId, Int32 OrganiseTypeId, String CompanyName, String LegalName , String AdminName,String StateTaxNo, String @IRSNo,String  @Companywebsite, String ContactPersonName, String ContactPersonDesignation, String logourl )
    {
        cmd = new SqlCommand();
        cmd.CommandText = "InsertCompanyMaster";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@IndustryTypeId", SqlDbType.Int));
        cmd.Parameters["@IndustryTypeId"].Value = IndustryTypeId;
        cmd.Parameters.Add(new SqlParameter("@OrganiseTypeId", SqlDbType.Int));
        cmd.Parameters["@OrganiseTypeId"].Value = OrganiseTypeId;

        cmd.Parameters.Add(new SqlParameter("@CompanyName", SqlDbType.NVarChar));
        cmd.Parameters["@CompanyName"].Value = CompanyName;
        cmd.Parameters.Add(new SqlParameter("@AdminName", SqlDbType.NVarChar));
        cmd.Parameters["@AdminName"].Value = AdminName;
        cmd.Parameters.Add(new SqlParameter("@LegalName", SqlDbType.NVarChar));
        cmd.Parameters["@LegalName"].Value = LegalName;
        cmd.Parameters.Add(new SqlParameter("@StateTaxNo", SqlDbType.NVarChar));
        cmd.Parameters["@StateTaxNo"].Value = StateTaxNo;
        cmd.Parameters.Add(new SqlParameter("@IRSNo", SqlDbType.NVarChar));
        cmd.Parameters["@IRSNo"].Value = @IRSNo;
        cmd.Parameters.Add(new SqlParameter("@Companywebsite", SqlDbType.NVarChar));
        cmd.Parameters["@Companywebsite"].Value = @Companywebsite;
        cmd.Parameters.Add(new SqlParameter("@ContactPersonName", SqlDbType.NVarChar));
        cmd.Parameters["@ContactPersonName"].Value = ContactPersonName;
        cmd.Parameters.Add(new SqlParameter("@ContactPersonDesignation", SqlDbType.NVarChar));
        cmd.Parameters["@ContactPersonDesignation"].Value = ContactPersonDesignation;
        cmd.Parameters.Add(new SqlParameter("@CompanyLogo", SqlDbType.NVarChar));
        cmd.Parameters["@CompanyLogo"].Value = logourl;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int));
        cmd.Parameters["@CompanyId"].Direction = ParameterDirection.Output;
        Int32 result = DatabaseCls1.ExecuteNonQuery(cmd);
        result = Convert.ToInt32(cmd.Parameters["@CompanyId"].Value.ToString());
        return result;
    }
    public Int32 InsertCompanyAddressDetail(Int32 CompanyId, Int32 CompanyAddressTypeId, String Address, String City, String StateName, String PinCode, String Email, String Fax, String ContactNo, String WebsiteAddress, String CountyName)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "InsertCompanyAddressDetail";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@CompanyId", SqlDbType.Int));
        cmd.Parameters["@CompanyId"].Value = CompanyId;
        cmd.Parameters.Add(new SqlParameter("@CompanyAddressTypeId", SqlDbType.Int));
        cmd.Parameters["@CompanyAddressTypeId"].Value = CompanyAddressTypeId;
        cmd.Parameters.Add(new SqlParameter("@Address", SqlDbType.NVarChar));
        cmd.Parameters["@Address"].Value = Address;
        cmd.Parameters.Add(new SqlParameter("@City", SqlDbType.NVarChar));
        cmd.Parameters["@City"].Value = City;
        cmd.Parameters.Add(new SqlParameter("@StateName", SqlDbType.NVarChar));
        cmd.Parameters["@StateName"].Value = StateName;
        cmd.Parameters.Add(new SqlParameter("@PinCode", SqlDbType.NVarChar));
        cmd.Parameters["@PinCode"].Value = PinCode;
        cmd.Parameters.Add(new SqlParameter("@Email", SqlDbType.NVarChar));
        cmd.Parameters["@Email"].Value = Email;
        cmd.Parameters.Add(new SqlParameter("@Fax", SqlDbType.NVarChar));
        cmd.Parameters["@Fax"].Value = Fax;
        cmd.Parameters.Add(new SqlParameter("@ContactNo", SqlDbType.NVarChar));
        cmd.Parameters["@ContactNo"].Value = ContactNo;
        cmd.Parameters.Add(new SqlParameter("@WebsiteAddress", SqlDbType.NVarChar));
        cmd.Parameters["@WebsiteAddress"].Value = WebsiteAddress;
        cmd.Parameters.Add(new SqlParameter("@CountryName", SqlDbType.NVarChar));
        cmd.Parameters["@CountryName"].Value = CountyName;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls1.ExecuteNonQuery(cmd);
        return result;
    }

    //=============== alkesh 24-03-2008

    public Int32 InsertCompanyMasterFTP(bool ftp)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "InsertCompanyMasterFTP";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@ftp", SqlDbType.Bit));
        cmd.Parameters["@ftp"].Value = ftp;
        cmd.Parameters.Add(new SqlParameter("@CompId", SqlDbType.NVarChar));
        cmd.Parameters["@CompId"].Value = HttpContext.Current.Session["Comid"].ToString();
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls1.ExecuteNonQueryep(cmd);
        result = 0;
        result = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value.ToString());
        return result;
    }
    public Int32 InsertCompanyMasterEmail(bool email)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "InsertCompanyMasterEmail";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@email", SqlDbType.Bit));
        cmd.Parameters["@email"].Value = email;
        cmd.Parameters.Add(new SqlParameter("@CompId", SqlDbType.NVarChar));
        cmd.Parameters["@CompId"].Value = HttpContext.Current.Session["Comid"].ToString();
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls1.ExecuteNonQueryep(cmd);
        result = 0;
        result = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value.ToString());
        return result;
    }
 public Int32 InsertAutoAllocation(Int32  EmployeeId, String Whid)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "InsertAutoAllocation";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@EmployeeId", SqlDbType.Int));
        cmd.Parameters["@EmployeeId"].Value = EmployeeId;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        cmd.Parameters.Add(new SqlParameter("@Whid", SqlDbType.NVarChar));
        cmd.Parameters["@Whid"].Value = Whid; // CompanyLoginId;
      
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls1.ExecuteNonQueryep(cmd);
        result = 0;
        result = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value.ToString());
        return result;
    }
    public DataTable selectAutoAllocationMaster(String Whid)
    {
        cmd = new SqlCommand();
        DataTable dtIp = new DataTable();

        cmd.CommandText = "selectAutoAllocationMaster";
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        cmd.Parameters.Add(new SqlParameter("@Whid", SqlDbType.NVarChar));
        cmd.Parameters["@Whid"].Value = Whid; // CompanyLoginId;
       
        dtIp = DatabaseCls1.FilleppAdapter(cmd);
        return dtIp;
    }
    public Int32 UpdateCompanyMasterforAutoAllocation(bool IPStatus)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "UpdateCompanyMasterforAutoAllocation";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@Auto", SqlDbType.Bit));
        cmd.Parameters["@Auto"].Value = IPStatus;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls1.ExecuteNonQuery(cmd);
        result = 0;
        result = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value.ToString());
        return result;
    }
     public Int32 DeleteAutoAllocationMaster(Int32 EmployeeId)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "DeleteAutoAllocationMaster";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@EmployeeId", SqlDbType.Int));
        cmd.Parameters["@EmployeeId"].Value = EmployeeId;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls1.ExecuteNonQueryep(cmd);
        result = 0;
        result = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value.ToString());
        return result;
    }
     public Int32 UpdateCompanyMasterforDocFolder(bool DocFolder)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "UpdateCompanyMasterforDocFolder";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@DocFolder", SqlDbType.Bit));
        cmd.Parameters["@DocFolder"].Value = DocFolder;

        cmd.Parameters.Add(new SqlParameter("@Compid", SqlDbType.NVarChar));
        cmd.Parameters["@Compid"].Value = HttpContext.Current.Session["Comid"].ToString(); 
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls1.ExecuteNonQueryep(cmd);
        result = 0;
        result = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value.ToString());
        return result;
    }
     public Int32 InsertDownloadFolder(String FolderName, bool FolderRule, bool DocumentAutoApprove, String RuleType,String Whid)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "InsertDownloadFolder";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@FolderName", SqlDbType.NVarChar));
        cmd.Parameters["@FolderName"].Value = FolderName;
        cmd.Parameters.Add(new SqlParameter("@FolderRule", SqlDbType.Bit));
        cmd.Parameters["@FolderRule"].Value = FolderRule;
        cmd.Parameters.Add(new SqlParameter("@DocumentAutoApprove", SqlDbType.Bit));
        cmd.Parameters["@DocumentAutoApprove"].Value = DocumentAutoApprove;
        cmd.Parameters.Add(new SqlParameter("@RuleType", SqlDbType.VarChar));
        cmd.Parameters["@RuleType"].Value = RuleType;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString();

        cmd.Parameters.Add(new SqlParameter("@Whid", SqlDbType.NVarChar));
        cmd.Parameters["@Whid"].Value = Whid;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        cmd.Parameters.Add(new SqlParameter("@FolderId", SqlDbType.Int));
        cmd.Parameters["@FolderId"].Direction = ParameterDirection.Output;
        Int32 result = DatabaseCls1.ExecuteNonQueryep(cmd);
        result = Convert.ToInt32(cmd.Parameters["@FolderId"].Value.ToString());
        return result;
        //Int32 result = DatabaseCls1.ExecuteNonQuery(cmd);
        //result = 0;
        //result = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value.ToString());
        //return result;
    }
    public DataTable SelectDownloadFolder(String Whid)
    {
        cmd = new SqlCommand();
        DataTable dtIp = new DataTable();

        cmd.CommandText = "SelectDownloadFolder";
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString();
        cmd.Parameters.Add(new SqlParameter("@Whid", SqlDbType.NVarChar));
        cmd.Parameters["@Whid"].Value = Whid;// CompanyLoginId;
        dtIp = DatabaseCls1.FilleppAdapter(cmd);
        return dtIp;
    }
    public Int32 DeleteDownloadFolder(Int32 FolderId)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "DeleteDownloadFolder";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@FolderId", SqlDbType.Int));
        cmd.Parameters["@FolderId"].Value = FolderId;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls1.ExecuteNonQueryep(cmd);
        result = 0;
        result = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value.ToString());
        return result;
    }
    public Int32 DeleteDownloadFolderDefaultProp(Int32 FolderId)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "DeleteDownloadFolderDefaultProp";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@FolderId", SqlDbType.Int));
        cmd.Parameters["@FolderId"].Value = FolderId;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls1.ExecuteNonQueryep(cmd);
        result = 0;
        result = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value.ToString());
        return result;
    }
    public Int32 DeleteFTPMaster(Int32 FtpId)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "DeleteFTPMaster";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@FtpId", SqlDbType.Int));
        cmd.Parameters["@FtpId"].Value = FtpId;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls1.ExecuteNonQuery(cmd);
        result = 0;
        result = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value.ToString());
        return result;
    }
    public Int32 DeleteFTPMasterDefaultProp(Int32 FTPMasterId)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "DeleteFTPMasterDefaultProp";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@FTPMasterId", SqlDbType.Int));
        cmd.Parameters["@FTPMasterId"].Value = FTPMasterId;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls1.ExecuteNonQueryep(cmd);
        result = 0;
        result = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value.ToString());
        return result;
    }
    public Int32 DeleteDocumentEmailDownloadMaster(Int32 DocumentEmailDownloadID)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "DeleteDocumentEmailDownloadMaster";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@DocumentEmailDownloadID", SqlDbType.Int));
        cmd.Parameters["@DocumentEmailDownloadID"].Value = DocumentEmailDownloadID;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls1.ExecuteNonQueryep(cmd);
        result = 0;
        result = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value.ToString());
        return result;
    }
    public Int32 DeleteInOutCompanyEmailMaster(Int32 ID)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "DeleteInOutCompanyEmailMaster";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int));
        cmd.Parameters["@ID"].Value = ID;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls1.ExecuteNonQuery(cmd);
        result = 0;
        result = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value.ToString());
        return result;
    }
    public Int32 DeleteDocumentEmailDownloadDefaultProp(Int32 DocumentEmailDownloadID)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "DeleteDocumentEmailDownloadDefaultProp";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@DocumentEmailDownloadID", SqlDbType.Int));
        cmd.Parameters["@DocumentEmailDownloadID"].Value = DocumentEmailDownloadID;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls1.ExecuteNonQueryep(cmd);
        result = 0;
        result = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value.ToString());
        return result;
    }
     public Int32 UpdateCompanyMasterforDocFolderRule(bool DocFolderRule)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "UpdateCompanyMasterforDocFolderRule";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@DocFolderRule", SqlDbType.Bit));
        cmd.Parameters["@DocFolderRule"].Value = DocFolderRule;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls1.ExecuteNonQuery(cmd);
        result = 0;
        result = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value.ToString());
        return result;
    }
     public Int32 UpdateCompanyMaster(Int32 IndustryTypeId, Int32 OrganiseTypeId, String LegalName, String AdminName, String CompanyLogo, String StateTaxNumber, String IRSNumber, String CompanyWebsite, String ContactPersonName, String ContactPersonDesignation)
     {
         cmd = new SqlCommand();
         cmd.CommandText = "UpdateCompanyMaster";
         cmd.CommandType = CommandType.StoredProcedure;
         cmd.Parameters.Add(new SqlParameter("@IndustryTypeId", SqlDbType.Int));
         cmd.Parameters["@IndustryTypeId"].Value = IndustryTypeId;
         cmd.Parameters.Add(new SqlParameter("@OrganiseTypeId", SqlDbType.Int));
         cmd.Parameters["@OrganiseTypeId"].Value = OrganiseTypeId;
         cmd.Parameters.Add(new SqlParameter("@LegalName", SqlDbType.NVarChar));
         cmd.Parameters["@LegalName"].Value = LegalName;
         cmd.Parameters.Add(new SqlParameter("@AdminName", SqlDbType.NVarChar));
         cmd.Parameters["@AdminName"].Value = AdminName;
         cmd.Parameters.Add(new SqlParameter("@CompanyLogo", SqlDbType.NVarChar));
         cmd.Parameters["@CompanyLogo"].Value = CompanyLogo;
         cmd.Parameters.Add(new SqlParameter("@StateTaxNumber", SqlDbType.NVarChar));
         cmd.Parameters["@StateTaxNumber"].Value = StateTaxNumber;
         cmd.Parameters.Add(new SqlParameter("@IRSNumber", SqlDbType.NVarChar));
         cmd.Parameters["@IRSNumber"].Value = IRSNumber;
         cmd.Parameters.Add(new SqlParameter("@CompanyWebsite", SqlDbType.NVarChar));
         cmd.Parameters["@CompanyWebsite"].Value = CompanyWebsite;
         cmd.Parameters.Add(new SqlParameter("@ContactPersonName", SqlDbType.NVarChar));
         cmd.Parameters["@ContactPersonName"].Value = ContactPersonName;
         cmd.Parameters.Add(new SqlParameter("@ContactPersonDesignation", SqlDbType.NVarChar));
         cmd.Parameters["@ContactPersonDesignation"].Value = ContactPersonDesignation;
         cmd.Parameters.Add(new SqlParameter("@CompanyName", SqlDbType.NVarChar));
         cmd.Parameters["@CompanyName"].Value = HttpContext.Current.Session["CompanyLoginId"].ToString(); // CompanyLoginId;
         cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
         cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
         Int32 result = DatabaseCls1.ExecuteNonQuery(cmd);
         result = 0;
         result = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value.ToString());
         return result;
     }
     public Int32 UpdateCompanyLogo(String CompanyLogo)
     {
         cmd = new SqlCommand();
         cmd.CommandText = "UpdateCompanyLogo";
         cmd.CommandType = CommandType.StoredProcedure;
         cmd.Parameters.Add(new SqlParameter("@CompanyLogo", SqlDbType.NVarChar));
         cmd.Parameters["@CompanyLogo"].Value = CompanyLogo;
         cmd.Parameters.Add(new SqlParameter("@CompanyName", SqlDbType.NVarChar));
         cmd.Parameters["@CompanyName"].Value = HttpContext.Current.Session["CompanyLoginId"].ToString(); // CompanyLoginId;
         cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
         cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
         Int32 result = DatabaseCls1.ExecuteNonQuery(cmd);
         result = 0;
         result = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value.ToString());
         return result;
     }
     public Int32 UpdateCompanyAddressMaster(String Address, String City, Int32 StateId, String PinCode, String ContactNo, String Email, String Fax, String websiteAddress)
     {
         cmd = new SqlCommand();
         cmd.CommandText = "UpdateCompanyAddressMaster";
         cmd.CommandType = CommandType.StoredProcedure;
         cmd.Parameters.Add(new SqlParameter("@Address", SqlDbType.NVarChar));
         cmd.Parameters["@Address"].Value = Address;
         cmd.Parameters.Add(new SqlParameter("@City", SqlDbType.NVarChar));
         cmd.Parameters["@City"].Value = City;
         cmd.Parameters.Add(new SqlParameter("@StateId", SqlDbType.Int));
         cmd.Parameters["@StateId"].Value = StateId;
         cmd.Parameters.Add(new SqlParameter("@PinCode", SqlDbType.NVarChar));
         cmd.Parameters["@PinCode"].Value = PinCode;
         cmd.Parameters.Add(new SqlParameter("@ContactNo", SqlDbType.NVarChar));
         cmd.Parameters["@ContactNo"].Value = ContactNo;
         cmd.Parameters.Add(new SqlParameter("@Email", SqlDbType.NVarChar));
         cmd.Parameters["@Email"].Value = Email;
         cmd.Parameters.Add(new SqlParameter("@Fax", SqlDbType.NVarChar));
         cmd.Parameters["@Fax"].Value = Fax;
         cmd.Parameters.Add(new SqlParameter("@websiteAddress", SqlDbType.NVarChar));
         cmd.Parameters["@websiteAddress"].Value = websiteAddress;
         cmd.Parameters.Add(new SqlParameter("@CompanyName", SqlDbType.NVarChar));
         cmd.Parameters["@CompanyName"].Value = HttpContext.Current.Session["CompanyLoginId"].ToString(); // CompanyLoginId;
         cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
         cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
         Int32 result = DatabaseCls1.ExecuteNonQuery(cmd);
         result = 0;
         result = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value.ToString());
         return result;
     }

}
