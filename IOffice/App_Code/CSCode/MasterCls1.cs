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


public class MasterCls1
{
    SqlCommand cmd;
    //SqlDataReader rdr;
    //SqlParameter param;
    //DatabaseCls1 clsDatabase   ; 
    //DataSet ds;
    DataTable dt;
    public MasterCls1()
    {

    }

    /*-----------insert---------*/
    public Int32 InsertDepartmentMaster(String DepartmentName)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "InsertDepartmentMaster";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@DepartmentName", SqlDbType.NVarChar));
        cmd.Parameters["@DepartmentName"].Value = DepartmentName;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["CompanyLoginId"].ToString(); // CompanyLoginId;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls1.ExecuteNonQuery(cmd);
        return result;
    }
    public Int32 InsertIndustryTypeMaster(String IndustryType)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "InsertIndustryTypeMaster";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@IndustryType", SqlDbType.NVarChar));
        cmd.Parameters["@IndustryType"].Value = IndustryType;
        //cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        //cmd.Parameters["@CID"].Value = HttpContext.Current.Session["CompanyLoginId"].ToString(); // CompanyLoginId;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls1.ExecuteNonQuery(cmd);
        return result;
    }
    public Int32 InsertOrganiseTypeMaster(String OrganiseTypeName)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "InsertOrganiseTypeMaster";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@OrganiseTypeName", SqlDbType.NVarChar));
        cmd.Parameters["@OrganiseTypeName"].Value = OrganiseTypeName;
        //cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        //cmd.Parameters["@CID"].Value = HttpContext.Current.Session["CompanyLoginId"].ToString(); // CompanyLoginId;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls1.ExecuteNonQuery(cmd);
        return result;
    }

    public Int32 InsertDesignationMaster(Int32 DepartmentId, String DesignationName)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "InsertDesignationMaster";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@DepartmentID", SqlDbType.Int));
        cmd.Parameters["@DepartmentID"].Value = DepartmentId;
        cmd.Parameters.Add(new SqlParameter("@DesignationName", SqlDbType.NVarChar));
        cmd.Parameters["@DesignationName"].Value = DesignationName;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["CompanyLoginId"].ToString(); // CompanyLoginId;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls1.ExecuteNonQuery(cmd);
        return result;
    }


    public Int32 InsertAccessRightMaster(Int32 DesignationId, Int32 PageId, bool ReadAccess, bool WriteAccess, bool DeleteAccess, bool EditAccess)
    {

        cmd = new SqlCommand();
        cmd.CommandText = "InsertAccessRightMaster";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@DesignationId", SqlDbType.Int));
        cmd.Parameters["@DesignationId"].Value = DesignationId;
        cmd.Parameters.Add(new SqlParameter("@PageId", SqlDbType.Int));
        cmd.Parameters["@PageId"].Value = PageId;
        cmd.Parameters.Add(new SqlParameter("@ReadAccess", SqlDbType.Bit));
        cmd.Parameters["@ReadAccess"].Value = ReadAccess;
        cmd.Parameters.Add(new SqlParameter("@WriteAccess", SqlDbType.Bit));
        cmd.Parameters["@WriteAccess"].Value = WriteAccess;
        cmd.Parameters.Add(new SqlParameter("@DeleteAccess", SqlDbType.Bit));
        cmd.Parameters["@DeleteAccess"].Value = DeleteAccess;
        cmd.Parameters.Add(new SqlParameter("@EditAccess", SqlDbType.Bit));
        cmd.Parameters["@EditAccess"].Value = EditAccess;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["CompanyLoginId"].ToString(); // CompanyLoginId;

        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls1.ExecuteNonQuery(cmd);
        result = Int32.Parse(cmd.Parameters["@ReturnValue"].Value.ToString());
        return result;
    }

    public Int32 InsertCountryMaster(String CountryName, String CountryCode)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "InsertCountryMaster";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@CountryName", SqlDbType.NVarChar));
        cmd.Parameters["@CountryName"].Value = CountryName;
        cmd.Parameters.Add(new SqlParameter("@CountryCode", SqlDbType.NChar));
        cmd.Parameters["@CountryCode"].Value = CountryCode;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["CompanyLoginId"].ToString(); // CompanyLoginId;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls1.ExecuteNonQuery(cmd);
        return result;
    }
    public Int32 InsertStateMaster(Int32 CountryId, String StateName, String StateCode)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "InsertStateMaster";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@CountryId", SqlDbType.Int));
        cmd.Parameters["@CountryId"].Value = CountryId;
        cmd.Parameters.Add(new SqlParameter("@StateName", SqlDbType.NVarChar));
        cmd.Parameters["@StateName"].Value = StateName;
        cmd.Parameters.Add(new SqlParameter("@StateCode", SqlDbType.NVarChar));
        cmd.Parameters["@StateCode"].Value = StateCode;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls1.ExecuteNonQuery(cmd);
        return result;
    }
    public Int32 InsertCityMaster(Int32 StateId, String CityName)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "InsertCityMaster";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@StateId", SqlDbType.Int));
        cmd.Parameters["@StateId"].Value = StateId;
        cmd.Parameters.Add(new SqlParameter("@CityName", SqlDbType.NVarChar));
        cmd.Parameters["@CityName"].Value = CityName;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls1.ExecuteNonQuery(cmd);
        return result;
    }
    public Int32 InsertPageMaster(Int32 PageTypeId, String PageName, String PageTitle, String PageDescription)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "InsertPageMaster";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@PageTypeId", SqlDbType.Int));
        cmd.Parameters["@PageTypeId"].Value = PageTypeId;
        cmd.Parameters.Add(new SqlParameter("@PageName", SqlDbType.NVarChar));
        cmd.Parameters["@PageName"].Value = PageName;
        cmd.Parameters.Add(new SqlParameter("@PageTitle", SqlDbType.NVarChar));
        cmd.Parameters["@PageTitle"].Value = PageTitle;
        cmd.Parameters.Add(new SqlParameter("@PageDescription", SqlDbType.NVarChar));
        cmd.Parameters["@PageDescription"].Value = PageDescription;
        //cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        //cmd.Parameters["@CID"].Value = HttpContext.Current.Session["CompanyLoginId"].ToString(); // CompanyLoginId;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls1.ExecuteNonQuery(cmd);
        return result;
    }


    /*-----------select---------*/
    public DataTable SelectDepartmentMaster(string Whid)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectDepartmentMaster";
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString();
        cmd.Parameters.Add(new SqlParameter("@Whid", SqlDbType.NVarChar));
        cmd.Parameters["@Whid"].Value = Whid;// CompanyLoginId;
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;
    }
    public DataTable SelectDesignationMasterwithDept(Int32 DepartmentId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectDesignationMasterwithDept";
        cmd.Parameters.Add(new SqlParameter("@DepartmentId", SqlDbType.Int));
        cmd.Parameters["@DepartmentId"].Value = DepartmentId;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;
    }
    public DataTable SelectDesignationMasterwithDataDept()
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectDesignationMasterwithDataDept";
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["CompanyLoginId"].ToString(); // CompanyLoginId;
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }
    public DataTable SelectEmployeeMasterwithDataDept()
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectEmployeeMasterwithDataDept";
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["CompanyLoginId"].ToString(); // CompanyLoginId;
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }
    public DataTable selectEmployeewithDesignation(Int32 DesignationId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "selectEmployeewithDesignation";
        cmd.Parameters.Add(new SqlParameter("@DesignationId", SqlDbType.Int));
        cmd.Parameters["@DesignationId"].Value = DesignationId;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["CompanyLoginId"].ToString(); // CompanyLoginId;
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }
    public DataTable selectEmployeewithDesignationforadminpg(Int32 DesignationId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "selectEmployeewithDesignationforadminpg";
        cmd.Parameters.Add(new SqlParameter("@DesignationId", SqlDbType.Int));
        cmd.Parameters["@DesignationId"].Value = DesignationId;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["CompanyLoginId"].ToString(); // CompanyLoginId;
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }
    public DataTable SelectEmployeewithDesgDeptName()
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectEmployeewithDesgDeptName";
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["CompanyLoginId"].ToString(); // CompanyLoginId;
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }
    public DataTable SelectEmployeeAll()
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectEmployeeAll";
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["CompanyLoginId"].ToString(); // CompanyLoginId;
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }
    public DataTable SelectEmployeeMasterwithEmpType(Int32 EmployeeTypeId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectEmployeeMasterwithEmpType";
        cmd.Parameters.Add(new SqlParameter("@EmployeeTypeId", SqlDbType.Int));
        cmd.Parameters["@EmployeeTypeId"].Value = EmployeeTypeId;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["CompanyLoginId"].ToString(); // CompanyLoginId;
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }
    public DataTable SelectEmployeeMasterwithEmpAddressType(Int32 EmployeeAddressTypeId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectEmployeeMasterwithEmpAddressType";
        cmd.Parameters.Add(new SqlParameter("@EmployeeAddressTypeId", SqlDbType.Int));
        cmd.Parameters["@EmployeeAddressTypeId"].Value = EmployeeAddressTypeId;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["CompanyLoginId"].ToString(); // CompanyLoginId;
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }
    public DataTable SelectPartyAll()
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectPartyAll";
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["CompanyLoginId"].ToString(); // CompanyLoginId;
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }
    public DataTable SelectPartyAllWithoutEmp()
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectPartyAllWithoutEmp";
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["CompanyLoginId"].ToString(); // CompanyLoginId;
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }
    public DataTable SelectPartyAllByPartyTypeWithoutEmp(Int32 PartyTypeId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectPartyAllByPartyTypeWithoutEmp";
        cmd.Parameters.Add(new SqlParameter("@PartyTypeId", SqlDbType.Int));
        cmd.Parameters["@PartyTypeId"].Value = PartyTypeId;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["CompanyLoginId"].ToString(); // CompanyLoginId;
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }
    public DataTable SelectPartyMasterWithoutEmployeeType()
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectPartyMasterWithoutEmployeeType";
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["CompanyLoginId"].ToString(); // CompanyLoginId;
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }
    public DataTable SelectCompanyEmail()
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectCompanyEmail";
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["CompanyLoginId"].ToString(); // CompanyLoginId;
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }
    public DataTable SelectCompanyEmailRightsbyEmp(Int32 EmployeeId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectCompanyEmailRightsbyEmp";
        cmd.Parameters.Add(new SqlParameter("@EmployeeId", SqlDbType.Int));
        cmd.Parameters["@EmployeeId"].Value = EmployeeId;
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }
    public DataTable SelectCompanyEmailForEmp(Int32 EmployeeId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectCompanyEmailForEmp";
        cmd.Parameters.Add(new SqlParameter("@EmployeeId", SqlDbType.Int));
        cmd.Parameters["@EmployeeId"].Value = EmployeeId;
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }
    public DataTable SelectEmpEmail(Int32 EmployeeId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectEmpEmail";
        cmd.Parameters.Add(new SqlParameter("@EmployeeId", SqlDbType.Int));
        cmd.Parameters["@EmployeeId"].Value = EmployeeId;
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }
    public DataTable SelectEmpEmaildetail(Int32 EmployeeId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectEmpEmaildetail";
        cmd.Parameters.Add(new SqlParameter("@EmployeeId", SqlDbType.Int));
        cmd.Parameters["@EmployeeId"].Value = EmployeeId;
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;
    }
    public DataTable SelectCompanyEmaildetail(Int32 ID)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectCompanyEmaildetail";
        cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int));
        cmd.Parameters["@ID"].Value = ID;
        //cmd.Parameters.Add(new SqlParameter("@PartyId", SqlDbType.Int));
        //cmd.Parameters["@PartyId"].Value = PartyId;
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;
    }
    public DataTable SelectPartyIdfromCompanyEmail(Int32 ID)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectPartyIdfromCompanyEmail";
        cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int));
        cmd.Parameters["@ID"].Value = ID;
        //cmd.Parameters.Add(new SqlParameter("@PartyId", SqlDbType.Int));
        //cmd.Parameters["@PartyId"].Value = PartyId;
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;
    }
    public DataTable SelectPartyIdfromCompanyEmailId(Int32 ID)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectPartyIdfromCompanyEmailId";
        cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int));
        cmd.Parameters["@ID"].Value = ID;
        //cmd.Parameters.Add(new SqlParameter("@PartyId", SqlDbType.Int));
        //cmd.Parameters["@PartyId"].Value = PartyId;
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }
    public DataTable SelectDesignationMaster()
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectDesignationMaster";
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["CompanyLoginId"].ToString(); // CompanyLoginId;
        //cmd.Parameters.Add(new SqlParameter("@DepartmentId", SqlDbType.Int));
        //cmd.Parameters["@DepartmentId"].Value = DepartmentId;

        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }
    public DataTable SelectCountryMaster()
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectCountryMaster";
        //cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        //cmd.Parameters["@CID"].Value = HttpContext.Current.Session["CompanyLoginId"].ToString(); // CompanyLoginId;
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }
    public DataTable SelectStateMaster()
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectStateMaster";
        //cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        //cmd.Parameters["@CID"].Value = HttpContext.Current.Session["CompanyLoginId"].ToString(); // CompanyLoginId;
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }
    public DataTable SelectStateMasterStateIDwise(Int32 StateId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectStateMasterStateIDwise";
        cmd.Parameters.Add(new SqlParameter("@StateId", SqlDbType.Int));
        cmd.Parameters["@StateId"].Value = StateId;

        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }
    public DataTable SelectStateMasterWithCountry(Int32 CountryId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectStateMasterWithCountry";
        cmd.Parameters.Add(new SqlParameter("@CountryId", SqlDbType.Int));
        cmd.Parameters["@CountryId"].Value = CountryId;
        //cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        //cmd.Parameters["@CID"].Value = HttpContext.Current.Session["CompanyLoginId"].ToString(); // CompanyLoginId;
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }

    public DataTable SelectPageMaster()
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectPageMaster";
        //cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        //cmd.Parameters["@CID"].Value = HttpContext.Current.Session["CompanyLoginId"].ToString(); // CompanyLoginId;
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }
    //public DataTable SelectPageMasterwithPageType(Int32 PageTypeId)
    //{
    //    cmd = new SqlCommand();
    //    dt = new DataTable();
    //    cmd.CommandText = "SelectPageMasterwithPageType";
    //    cmd.Parameters.Add(new SqlParameter("@PageTypeId", SqlDbType.Int));
    //    cmd.Parameters["@PageTypeId"].Value = PageTypeId;
    //    dt = DatabaseCls1.FillAdapter(cmd);
    //    return dt;
    //}
    public DataTable SelectPageTypeMaster()
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectPageTypeMaster";
        //cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        //cmd.Parameters["@CID"].Value = HttpContext.Current.Session["CompanyLoginId"].ToString(); // CompanyLoginId;
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }
    public DataTable SelectCityMaster()
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectCityMaster";

        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }
    public DataTable SelectCityMasterByCountry(Int32 CountryId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectCityMasterByCountry";
        cmd.Parameters.Add(new SqlParameter("@CountryId", SqlDbType.Int));
        cmd.Parameters["@CountryId"].Value = CountryId;
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }
    public DataTable SelectCityMasterByCountryState(Int32 CountryId, Int32 StateId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectCityMasterByCountryState";
        cmd.Parameters.Add(new SqlParameter("@CountryId", SqlDbType.Int));
        cmd.Parameters["@CountryId"].Value = CountryId;
        cmd.Parameters.Add(new SqlParameter("@StateId", SqlDbType.Int));
        cmd.Parameters["@StateId"].Value = StateId;
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }
    public Int32 SelectLoginMaster(String UserName, String UserPassword) //,ref Int32  EmployeeId, ref  Int32  DesignationId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        Int32 rtn;
        cmd.CommandText = "SelectLoginMaster";
        cmd.Parameters.Add(new SqlParameter("@UserName", SqlDbType.NVarChar));
        cmd.Parameters["@UserName"].Value = UserName;
        cmd.Parameters.Add(new SqlParameter("@UserPassword", SqlDbType.NVarChar));
        cmd.Parameters["@UserPassword"].Value = UserPassword;
        cmd.Parameters.Add(new SqlParameter("@EmployeeId", SqlDbType.Int));
        cmd.Parameters["@EmployeeId"].Direction = ParameterDirection.Output;
        cmd.Parameters.Add(new SqlParameter("@DesignationId", SqlDbType.Int));
        cmd.Parameters["@DesignationId"].Direction = ParameterDirection.Output;
        cmd.Parameters.Add(new SqlParameter("@PartyId", SqlDbType.Int));
        cmd.Parameters["@PartyId"].Direction = ParameterDirection.Output;

        cmd.Parameters.Add(new SqlParameter("@EmployeeName", SqlDbType.NVarChar));
        cmd.Parameters["@EmployeeName"].Size = 500;
        cmd.Parameters["@EmployeeName"].Direction = ParameterDirection.Output;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        cmd.CommandType = CommandType.StoredProcedure;
        SqlCommand cmdd = new SqlCommand();
        cmdd = (SqlCommand)DatabaseCls1.ExecuteNonQuerywithReturn(cmd);
        //EmployeeId = Int32.Parse(cmdd.Parameters["@EmployeeId"].Value.ToString());
        //DesignationId = Int32.Parse(cmdd.Parameters["@DesignationId"].Value.ToString());
        rtn = Int32.Parse(cmd.Parameters["@ReturnValue"].Value.ToString());
        if (rtn > 0)
        {
          //  HttpContext.Current.Session["EmployeeId"] = Int32.Parse(cmdd.Parameters["@EmployeeId"].Value.ToString()); //EmployeeId;
            HttpContext.Current.Session["DesignationId"] = Int32.Parse(cmdd.Parameters["@DesignationId"].Value.ToString()); //DesignationId;
            HttpContext.Current.Session["PartyId"] = Int32.Parse(cmdd.Parameters["@PartyId"].Value.ToString()); //
            HttpContext.Current.Session["PartyId1"] = Int32.Parse(cmdd.Parameters["@PartyId"].Value.ToString());
            HttpContext.Current.Session["EmployeeName"] = cmdd.Parameters["@EmployeeName"].Value.ToString(); //EmployeeId;


        }
        else
        {
           // HttpContext.Current.Session["EmployeeId"] = 0;
            HttpContext.Current.Session["DesignationId"] = 0;
        }
        return rtn;
    }
    public Int32 SelectLoginMasterbyPartyID(String UserName, String UserPassword, String Company) //,ref Int32  EmployeeId, ref  Int32  DesignationId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        Int32 rtn;
        cmd.CommandText = "SelectLoginMasterbyPartyID";
        cmd.Parameters.Add(new SqlParameter("@UserName", SqlDbType.NVarChar));
        cmd.Parameters["@UserName"].Value = UserName;
        cmd.Parameters.Add(new SqlParameter("@UserPassword", SqlDbType.NVarChar));
        cmd.Parameters["@UserPassword"].Value = UserPassword;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = Company;
        cmd.Parameters.Add(new SqlParameter("@EmployeeId", SqlDbType.Int));
        cmd.Parameters["@EmployeeId"].Direction = ParameterDirection.Output;
        cmd.Parameters.Add(new SqlParameter("@DesignationId", SqlDbType.Int));
        cmd.Parameters["@DesignationId"].Direction = ParameterDirection.Output;
        cmd.Parameters.Add(new SqlParameter("@PartyId", SqlDbType.Int));
        cmd.Parameters["@PartyId"].Direction = ParameterDirection.Output;

        cmd.Parameters.Add(new SqlParameter("@EmployeeName", SqlDbType.NVarChar));
        cmd.Parameters["@EmployeeName"].Size = 500;
        cmd.Parameters["@EmployeeName"].Direction = ParameterDirection.Output;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        cmd.CommandType = CommandType.StoredProcedure;
        SqlCommand cmdd = new SqlCommand();
        cmdd = (SqlCommand)DatabaseCls1.ExecuteNonQuerywithReturn(cmd);
        //EmployeeId = Int32.Parse(cmdd.Parameters["@EmployeeId"].Value.ToString());
        //DesignationId = Int32.Parse(cmdd.Parameters["@DesignationId"].Value.ToString());
        rtn = Int32.Parse(cmd.Parameters["@ReturnValue"].Value.ToString());
        if (rtn > 0)
        {
           // HttpContext.Current.Session["EmployeeId"] = Int32.Parse(cmdd.Parameters["@EmployeeId"].Value.ToString()); //EmployeeId;
            HttpContext.Current.Session["DesignationId"] = Int32.Parse(cmdd.Parameters["@DesignationId"].Value.ToString()); //DesignationId;
          //  HttpContext.Current.Session["PartyId"] = Int32.Parse(cmdd.Parameters["@PartyId"].Value.ToString()); //
           // HttpContext.Current.Session["PartyId1"] = Int32.Parse(cmdd.Parameters["@PartyId"].Value.ToString());
            HttpContext.Current.Session["EmployeeName"] = cmdd.Parameters["@EmployeeName"].Value.ToString(); //EmployeeId;


        }
        else
        {
          //  HttpContext.Current.Session["EmployeeId"] = 0;
            HttpContext.Current.Session["DesignationId"] = 0;
        }
        return rtn;
    }

    public Int32 SelectLoginMaster_Party(String UserName, String UserPassword) //,ref Int32  EmployeeId, ref  Int32  DesignationId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        Int32 rtn;
        cmd.CommandText = "SelectLoginMaster_Party";
        cmd.Parameters.Add(new SqlParameter("@UserName", SqlDbType.NVarChar));
        cmd.Parameters["@UserName"].Value = UserName;
        cmd.Parameters.Add(new SqlParameter("@UserPassword", SqlDbType.NVarChar));
        cmd.Parameters["@UserPassword"].Value = UserPassword;

        cmd.Parameters.Add(new SqlParameter("@PartyId", SqlDbType.Int));
        cmd.Parameters["@PartyId"].Direction = ParameterDirection.Output;

        cmd.Parameters.Add(new SqlParameter("@PartyName", SqlDbType.NVarChar));
        cmd.Parameters["@PartyName"].Size = 500;
        cmd.Parameters["@PartyName"].Direction = ParameterDirection.Output;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        cmd.CommandType = CommandType.StoredProcedure;
        SqlCommand cmdd = new SqlCommand();
        cmdd = (SqlCommand)DatabaseCls1.ExecuteNonQuerywithReturn(cmd);
        //EmployeeId = Int32.Parse(cmdd.Parameters["@EmployeeId"].Value.ToString());
        //DesignationId = Int32.Parse(cmdd.Parameters["@DesignationId"].Value.ToString());
        rtn = Int32.Parse(cmd.Parameters["@ReturnValue"].Value.ToString());
        if (rtn > 0)
        {
            // HttpContext.Current.Session["EmployeeId"] = Int32.Parse(cmdd.Parameters["@EmployeeId"].Value.ToString()); //EmployeeId;
            //HttpContext.Current.Session["DesignationId"] = Int32.Parse(cmdd.Parameters["@DesignationId"].Value.ToString()); //DesignationId;
            HttpContext.Current.Session["PartyId"] = Int32.Parse(cmdd.Parameters["@PartyId"].Value.ToString()); //
            HttpContext.Current.Session["PartyName"] = cmdd.Parameters["@PartyName"].Value.ToString(); //EmployeeId;


        }
        else
        {
           // HttpContext.Current.Session["EmployeeId"] = 0;
            HttpContext.Current.Session["DesignationId"] = 0;
        }
        return rtn;
    }


    public Int32 SelectLoginMasterusingName(String UserName, String companyId) //,ref Int32  EmployeeId, ref  Int32  DesignationId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        Int32 rtn;
        cmd.CommandText = "SelectLoginMasterusingName";
        cmd.Parameters.Add(new SqlParameter("@UserName", SqlDbType.NVarChar));
        cmd.Parameters["@UserName"].Value = UserName;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = companyId;
        cmd.Parameters.Add(new SqlParameter("@LoginId", SqlDbType.Int));
        cmd.Parameters["@LoginId"].Direction = ParameterDirection.Output;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        cmd.CommandType = CommandType.StoredProcedure;
        SqlCommand cmdd = new SqlCommand();
        cmdd = (SqlCommand)DatabaseCls1.ExecuteNonQuerywithReturn(cmd);
        //EmployeeId = Int32.Parse(cmdd.Parameters["@EmployeeId"].Value.ToString());
        //DesignationId = Int32.Parse(cmdd.Parameters["@DesignationId"].Value.ToString());
        rtn = Int32.Parse(cmd.Parameters["@LoginId"].Value.ToString());
        return rtn;
    }
    public DataTable selectEmployeeIdAsAdmin(String companyId) //,ref Int32  EmployeeId, ref  Int32  DesignationId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "selectEmployeeIdAsAdmin";
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = companyId;
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }
    public Int32 SelectPartyMasterforNewParty(Int32 PartyID, String Email, String companyId) //,ref Int32  EmployeeId, ref  Int32  DesignationId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        Int32 rtn;
        cmd.CommandText = "SelectPartyMasterforNewParty";
        cmd.Parameters.Add(new SqlParameter("@PartyId", SqlDbType.Int));
        cmd.Parameters["@PartyId"].Value = PartyID;
        cmd.Parameters.Add(new SqlParameter("@Email", SqlDbType.NVarChar));
        cmd.Parameters["@Email"].Value = Email;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = companyId;
        cmd.Parameters.Add(new SqlParameter("@PrtyId", SqlDbType.Int));
        cmd.Parameters["@PrtyId"].Direction = ParameterDirection.Output;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        cmd.CommandType = CommandType.StoredProcedure;
        SqlCommand cmdd = new SqlCommand();
        cmdd = (SqlCommand)DatabaseCls1.ExecuteNonQuerywithReturn(cmd);
        //EmployeeId = Int32.Parse(cmdd.Parameters["@EmployeeId"].Value.ToString());
        //DesignationId = Int32.Parse(cmdd.Parameters["@DesignationId"].Value.ToString());
        rtn = Int32.Parse(cmd.Parameters["@PrtyId"].Value.ToString());
        return rtn;
    }
    public Int32 SelectEmployeeMasterforNewEmployee(Int32 PartyID, String Email, String companyId) //,ref Int32  EmployeeId, ref  Int32  DesignationId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        Int32 rtn;
        cmd.CommandText = "SelectEmployeeMasterforNewEmployee";
        cmd.Parameters.Add(new SqlParameter("@PartyId", SqlDbType.Int));
        cmd.Parameters["@PartyId"].Value = PartyID;
        cmd.Parameters.Add(new SqlParameter("@Email", SqlDbType.NVarChar));
        cmd.Parameters["@Email"].Value = Email;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = companyId;
        cmd.Parameters.Add(new SqlParameter("@EmployeeID", SqlDbType.Int));
        cmd.Parameters["@EmployeeID"].Direction = ParameterDirection.Output;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        cmd.CommandType = CommandType.StoredProcedure;
        SqlCommand cmdd = new SqlCommand();
        cmdd = (SqlCommand)DatabaseCls1.ExecuteNonQuerywithReturn(cmd);
        //EmployeeId = Int32.Parse(cmdd.Parameters["@EmployeeId"].Value.ToString());
        //DesignationId = Int32.Parse(cmdd.Parameters["@DesignationId"].Value.ToString());
        rtn = Int32.Parse(cmd.Parameters["@EmployeeID"].Value.ToString());
        return rtn;
    }

    public Int32 SelectPartyLoginMasterusingName(String UserName) //,ref Int32  EmployeeId, ref  Int32  DesignationId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        Int32 rtn;
        cmd.CommandText = "SelectPartyLoginMasterusingName";
        cmd.Parameters.Add(new SqlParameter("@UserName", SqlDbType.NVarChar));
        cmd.Parameters["@UserName"].Value = UserName;
        cmd.Parameters.Add(new SqlParameter("@LoginId", SqlDbType.Int));
        cmd.Parameters["@LoginId"].Direction = ParameterDirection.Output;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        cmd.CommandType = CommandType.StoredProcedure;
        SqlCommand cmdd = new SqlCommand();
        cmdd = (SqlCommand)DatabaseCls1.ExecuteNonQuerywithReturn(cmd);
        //EmployeeId = Int32.Parse(cmdd.Parameters["@EmployeeId"].Value.ToString());
        //DesignationId = Int32.Parse(cmdd.Parameters["@DesignationId"].Value.ToString());
        rtn = Int32.Parse(cmd.Parameters["@LoginId"].Value.ToString());
        return rtn;
    }

    public DataTable SelectCityMasterWithState(Int32 StateId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectCityMasterWithState";
        cmd.Parameters.Add(new SqlParameter("@StateId", SqlDbType.Int));
        cmd.Parameters["@StateId"].Value = StateId;
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }
    public DataTable SelectPagelistDesignationwisePageTypewise(Int32 PageTypeId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectPagelistDesignationwisePageTypewise";
        cmd.Parameters.Add(new SqlParameter("@DesignationId", SqlDbType.Int));
        cmd.Parameters["@DesignationId"].Value = HttpContext.Current.Session["DesignationId"].ToString();
        cmd.Parameters.Add(new SqlParameter("@PageTypeId", SqlDbType.Int));
        cmd.Parameters["@PageTypeId"].Value = PageTypeId; //neetu
        //cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        //cmd.Parameters["@CID"].Value = HttpContext.Current.Session["CompanyLoginId"].ToString(); // CompanyLoginId;
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }

    /*-----------update---------*/
    public bool UpdateDepartmentMaster(Int32 DepartmentId, String DepartmentName)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "UpdateDepartmentMaster";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@DepartmentId", SqlDbType.Int));
        cmd.Parameters["@DepartmentId"].Value = DepartmentId;
        cmd.Parameters.Add(new SqlParameter("@DepartmentName", SqlDbType.NVarChar));
        cmd.Parameters["@DepartmentName"].Value = DepartmentName;
        //cmd.Parameters.Add(new SqlParameter("@DepartmentName", SqlDbType.NVarChar));
        //cmd.Parameters["@DepartmentName"].Value = DepartmentName;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        int result = DatabaseCls1.ExecuteNonQuery(cmd);
        return (result != -1);
    }
    public bool UpdateIndustryTypeMaster(Int32 IndustryTypeId, String IndustryType)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "UpdateIndustryTypeMaster";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@IndustryTypeId", SqlDbType.Int));
        cmd.Parameters["@IndustryTypeId"].Value = IndustryTypeId;
        cmd.Parameters.Add(new SqlParameter("@IndustryType", SqlDbType.NVarChar));
        cmd.Parameters["@IndustryType"].Value = IndustryType;
        //cmd.Parameters.Add(new SqlParameter("@DepartmentName", SqlDbType.NVarChar));
        //cmd.Parameters["@DepartmentName"].Value = DepartmentName;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        int result = DatabaseCls1.ExecuteNonQuery(cmd);
        return (result != -1);
    }
    public bool UpdateOrganiseTypeMaster(Int32 OrganiseTypeId, String OrganiseTypeName)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "UpdateOrganiseTypeMaster";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@OrganiseTypeId", SqlDbType.Int));
        cmd.Parameters["@OrganiseTypeId"].Value = OrganiseTypeId;
        cmd.Parameters.Add(new SqlParameter("@OrganiseTypeName", SqlDbType.NVarChar));
        cmd.Parameters["@OrganiseTypeName"].Value = OrganiseTypeName;
        //cmd.Parameters.Add(new SqlParameter("@DepartmentName", SqlDbType.NVarChar));
        //cmd.Parameters["@DepartmentName"].Value = DepartmentName;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        int result = DatabaseCls1.ExecuteNonQuery(cmd);
        return (result != -1);
    }
    public bool UpdateDesignationMaster(Int32 DesignationId, Int32 DepartmentId, String DepartmentName)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "UpdateDesignationMaster";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@DesignationId", SqlDbType.Int));
        cmd.Parameters["@DesignationId"].Value = DesignationId;
        cmd.Parameters.Add(new SqlParameter("@DepartmentId", SqlDbType.Int));
        cmd.Parameters["@DepartmentId"].Value = DepartmentId;
        cmd.Parameters.Add(new SqlParameter("@DesignationName", SqlDbType.NVarChar));
        cmd.Parameters["@DesignationName"].Value = DepartmentName;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["CompanyLoginId"].ToString(); // CompanyLoginId;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        int result = DatabaseCls1.ExecuteNonQuery(cmd);
        return (result != -1);
    }
    public bool UpdateRuleDetailByRuleApproveTypeId(Int32 RuleApproveTypeId1, Int32 RuleApproveTypeId2)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "UpdateRuleDetailByRuleApproveTypeId";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@RuleApproveTypeId1", SqlDbType.Int));
        cmd.Parameters["@RuleApproveTypeId1"].Value = RuleApproveTypeId1;
        cmd.Parameters.Add(new SqlParameter("@RuleApproveTypeId2", SqlDbType.Int));
        cmd.Parameters["@RuleApproveTypeId2"].Value = RuleApproveTypeId2;

        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        int result = DatabaseCls1.ExecuteNonQuery(cmd);
        return (result != -1);
    }
    public bool UpdatePageMaster(Int32 PageId, Int32 PageTypeId, String PageName, String PageTitle, String PageDescription, Int32 PageIndex)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "UpdatePageMaster";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@PageId", SqlDbType.Int));
        cmd.Parameters["@PageId"].Value = PageId;
        cmd.Parameters.Add(new SqlParameter("@PageTypeId", SqlDbType.Int));
        cmd.Parameters["@PageTypeId"].Value = PageTypeId;
        cmd.Parameters.Add(new SqlParameter("@PageName", SqlDbType.NVarChar));
        cmd.Parameters["@PageName"].Value = PageName;
        cmd.Parameters.Add(new SqlParameter("@PageTitle", SqlDbType.NVarChar));
        cmd.Parameters["@PageTitle"].Value = PageTitle;
        cmd.Parameters.Add(new SqlParameter("@PageDescription", SqlDbType.NVarChar));
        cmd.Parameters["@PageDescription"].Value = PageDescription;
        cmd.Parameters.Add(new SqlParameter("@PageIndex", SqlDbType.Int));
        cmd.Parameters["@PageIndex"].Value = PageIndex;
        //cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        //cmd.Parameters["@CID"].Value = HttpContext.Current.Session["CompanyLoginId"].ToString(); // CompanyLoginId;
        //cmd.Parameters.Add(new SqlParameter("@DepartmentId", SqlDbType.Int));
        //cmd.Parameters["@DepartmentId"].Value = DepartmentId;
        //cmd.Parameters.Add(new SqlParameter("@DepartmentName", SqlDbType.NVarChar));
        //cmd.Parameters["@DepartmentName"].Value = DepartmentName;
        ////cmd.Parameters.Add(new SqlParameter("@DepartmentName", SqlDbType.NVarChar));
        ////cmd.Parameters["@DepartmentName"].Value = DepartmentName;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        int result = DatabaseCls1.ExecuteNonQuery(cmd);
        return (result != -1);
    }
    public DataTable SelectAccessRightMasterwithPageDesignation(Int32 PageId, Int32 DesignationId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectAccessRightMasterwithPageDesignation";
        cmd.Parameters.Add(new SqlParameter("@PageId", SqlDbType.Int));
        cmd.Parameters["@PageId"].Value = PageId;
        cmd.Parameters.Add(new SqlParameter("@DesignationId", SqlDbType.Int));
        cmd.Parameters["@DesignationId"].Value = DesignationId;
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }
    public bool UpdateCountryMaster(Int32 CountryId, String CountryName, String CountryCode)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "UpdateCountryMaster";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@CountryId", SqlDbType.Int));
        cmd.Parameters["@CountryId"].Value = CountryId;
        cmd.Parameters.Add(new SqlParameter("@CountryName", SqlDbType.NVarChar));
        cmd.Parameters["@CountryName"].Value = CountryName;
        cmd.Parameters.Add(new SqlParameter("@CountryCode", SqlDbType.NChar));
        cmd.Parameters["@CountryCode"].Value = CountryCode;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["CompanyLoginId"].ToString(); // CompanyLoginId;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        int result = DatabaseCls1.ExecuteNonQuery(cmd);
        return (result != -1);
    }
    public bool UpdateStateMaster(Int32 StateId, Int32 CountryId, String StateName, String StateCode)
    {

        cmd = new SqlCommand();
        cmd.CommandText = "UpdateStateMaster";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@StateId", SqlDbType.Int));
        cmd.Parameters["@StateId"].Value = StateId;
        cmd.Parameters.Add(new SqlParameter("@CountryId", SqlDbType.Int));
        cmd.Parameters["@CountryId"].Value = CountryId;
        cmd.Parameters.Add(new SqlParameter("@StateName", SqlDbType.NVarChar));
        cmd.Parameters["@StateName"].Value = StateName;
        cmd.Parameters.Add(new SqlParameter("@StateCode", SqlDbType.NVarChar));
        cmd.Parameters["@StateCode"].Value = StateCode;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        int result = DatabaseCls1.ExecuteNonQuery(cmd);
        return (result != -1);
    }
    public bool UpdateCityMaster(Int32 CityId, Int32 StateId, String CityName)
    {

        cmd = new SqlCommand();
        cmd.CommandText = "UpdateCityMaster";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@CityId", SqlDbType.Int));
        cmd.Parameters["@CityId"].Value = CityId;
        cmd.Parameters.Add(new SqlParameter("@StateId", SqlDbType.Int));
        cmd.Parameters["@StateId"].Value = StateId;
        cmd.Parameters.Add(new SqlParameter("@CityName", SqlDbType.NVarChar));
        cmd.Parameters["@CityName"].Value = CityName;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        int result = DatabaseCls1.ExecuteNonQuery(cmd);
        return (result != -1);
    }

    public DataTable SelectPageMasterwithPageType(Int32 PageTypeId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectPageMasterwithPageType";
        cmd.Parameters.Add(new SqlParameter("@PageTypeId", SqlDbType.Int));
        cmd.Parameters["@PageTypeId"].Value = PageTypeId;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["CompanyLoginId"].ToString(); // CompanyLoginId;
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }


    // Haiyal Start

    /*-------haiyal 12 - 2 - 2009 ------------*/

    public DataTable SelectClassTypeMaster()
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectClassTypeMaster";
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }
    public Int32 InsertClassTypeMaster(String ClassType, String Description)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "InsertClassTypeMaster";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@ClassType", SqlDbType.NVarChar));
        cmd.Parameters["@ClassType"].Value = ClassType;
        cmd.Parameters.Add(new SqlParameter("@Description", SqlDbType.NVarChar));
        cmd.Parameters["@Description"].Value = Description;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls1.ExecuteNonQuery(cmd);
        return result;
    }
    public bool UpdateClassTypeMaster(Int32 ClassTypeId, String ClassType, String Description)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "UpdateClassTypeMaster";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@ClassTypeId", SqlDbType.Int));
        cmd.Parameters["@ClassTypeId"].Value = ClassTypeId;
        cmd.Parameters.Add(new SqlParameter("@ClassType", SqlDbType.NVarChar));
        cmd.Parameters["@ClassType"].Value = ClassType;
        cmd.Parameters.Add(new SqlParameter("@Description", SqlDbType.NVarChar));
        cmd.Parameters["@Description"].Value = Description;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        int result = DatabaseCls1.ExecuteNonQuery(cmd);
        return (result != -1);
    }
    public DataTable SelectClassMaster()
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectClassMaster";
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }
    public DataTable SelectClassMasterWithClassType(Int32 ClassTypeId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectClassMasterWithClassType";
        cmd.Parameters.Add(new SqlParameter("@ClassTypeId", SqlDbType.Int));
        cmd.Parameters["@ClassTypeId"].Value = ClassTypeId;
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }
    public Int32 InsertClassMaster(Int32 ClassTypeId, String ClassName, String Description)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "InsertClassMaster";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@ClassTypeId", SqlDbType.Int));
        cmd.Parameters["@ClassTypeId"].Value = ClassTypeId;
        cmd.Parameters.Add(new SqlParameter("@ClassName", SqlDbType.NVarChar));
        cmd.Parameters["@ClassName"].Value = ClassName;
        cmd.Parameters.Add(new SqlParameter("@Description", SqlDbType.NVarChar));
        cmd.Parameters["@Description"].Value = Description;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls1.ExecuteNonQuery(cmd);
        return result;
    }
    public bool UpdateClassMaster(Int32 ClassId, Int32 ClassTypeId, String ClassName, String Description)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "UpdateClassMaster";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@ClassId", SqlDbType.Int));
        cmd.Parameters["@ClassId"].Value = ClassId;
        cmd.Parameters.Add(new SqlParameter("@ClassTypeId", SqlDbType.Int));
        cmd.Parameters["@ClassTypeId"].Value = ClassTypeId;
        cmd.Parameters.Add(new SqlParameter("@ClassName", SqlDbType.NVarChar));
        cmd.Parameters["@ClassName"].Value = ClassName;
        cmd.Parameters.Add(new SqlParameter("@Description", SqlDbType.NVarChar));
        cmd.Parameters["@Description"].Value = Description;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        int result = DatabaseCls1.ExecuteNonQuery(cmd);
        return (result != -1);
    }
    public DataTable SelectClassTypeMasterINGRID(Int32 ClassTypeId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectClassTypeMasterINGRID";
        cmd.Parameters.Add(new SqlParameter("@ClassTypeId", SqlDbType.Int));
        cmd.Parameters["@ClassTypeId"].Value = ClassTypeId;
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }
    public DataTable SelectCountryMasterINGRID(Int32 CountryId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectCountryMasterINGRID";
        cmd.Parameters.Add(new SqlParameter("@CountryId", SqlDbType.Int));
        cmd.Parameters["@CountryId"].Value = CountryId;
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }
    public DataTable SelectStateMasterINGRID(Int32 StateId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectStateMasterINGRID";
        cmd.Parameters.Add(new SqlParameter("@StateId", SqlDbType.Int));
        cmd.Parameters["@StateId"].Value = StateId;
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }
    public DataTable SelectGroupMaster()
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectGroupMaster";
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }
    public DataTable SelectGroupMasterWithClass(Int32 ClassId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectGroupMasterWithClass";
        cmd.Parameters.Add(new SqlParameter("@ClassId", SqlDbType.Int));
        cmd.Parameters["@ClassId"].Value = ClassId;
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }
    public Int32 InsertGroupMaster(Int32 ClassId, String GroupName, String Description)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "InsertGroupMaster";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@ClassId", SqlDbType.Int));
        cmd.Parameters["@ClassId"].Value = ClassId;
        cmd.Parameters.Add(new SqlParameter("@GroupName", SqlDbType.NVarChar));
        cmd.Parameters["@GroupName"].Value = GroupName;
        cmd.Parameters.Add(new SqlParameter("@Description", SqlDbType.NVarChar));
        cmd.Parameters["@Description"].Value = Description;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls1.ExecuteNonQuery(cmd);
        return result;
    }
    public bool UpdateGroupMaster(Int32 GroupId, Int32 ClassId, String GroupName, String Description)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "UpdateGroupMaster";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@GroupId", SqlDbType.Int));
        cmd.Parameters["@GroupId"].Value = GroupId;
        cmd.Parameters.Add(new SqlParameter("@ClassId", SqlDbType.Int));
        cmd.Parameters["@ClassId"].Value = ClassId;
        cmd.Parameters.Add(new SqlParameter("@GroupName", SqlDbType.NVarChar));
        cmd.Parameters["@GroupName"].Value = GroupName;
        cmd.Parameters.Add(new SqlParameter("@Description", SqlDbType.NVarChar));
        cmd.Parameters["@Description"].Value = Description;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        int result = DatabaseCls1.ExecuteNonQuery(cmd);
        return (result != -1);
    }


    /*-------haiyal 12 - 2 - 2009 ------------*/

    public DataTable SelectStatusCategoryMaster()
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectStatusCategoryMaster";
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }
    public Int32 InsertStatusCategoryMaster(String StatusCategory, String StatusCategoryDesc)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "InsertStatusCategoryMaster";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@StatusCategory", SqlDbType.NVarChar));
        cmd.Parameters["@StatusCategory"].Value = StatusCategory;
        cmd.Parameters.Add(new SqlParameter("@StatusCategoryDesc", SqlDbType.NVarChar));
        cmd.Parameters["@StatusCategoryDesc"].Value = StatusCategoryDesc;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls1.ExecuteNonQuery(cmd);
        return result;
    }
    public bool UpdateStatusCategoryMaster(Int32 StatusCategoryId, String StatusCategory, String StatusCategoryDesc)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "UpdateStatusCategoryMaster";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@StatusCategoryId", SqlDbType.Int));
        cmd.Parameters["@StatusCategoryId"].Value = StatusCategoryId;
        cmd.Parameters.Add(new SqlParameter("@StatusCategory ", SqlDbType.NVarChar));
        cmd.Parameters["@StatusCategory "].Value = StatusCategory;
        cmd.Parameters.Add(new SqlParameter("@StatusCategoryDesc ", SqlDbType.NVarChar));
        cmd.Parameters["@StatusCategoryDesc "].Value = StatusCategoryDesc;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        int result = DatabaseCls1.ExecuteNonQuery(cmd);
        return (result != -1);
    }
    public Int32 InsertStatusMaster(Int32 StatusCategoryId, String StatusName, String Description)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "InsertStatusMaster";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@StatusCategoryId", SqlDbType.Int));
        cmd.Parameters["@StatusCategoryId"].Value = StatusCategoryId;
        cmd.Parameters.Add(new SqlParameter("@StatusName", SqlDbType.NVarChar));
        cmd.Parameters["@StatusName"].Value = StatusName;
        cmd.Parameters.Add(new SqlParameter("@Description", SqlDbType.NVarChar));
        cmd.Parameters["@Description"].Value = Description;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls1.ExecuteNonQuery(cmd);
        return result;
    }
    public DataTable SelectStatusMaster()
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectStatusMaster";
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }
    public DataTable SelectStatusMasterWithStatusCatg(Int32 StatusCategoryId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectStatusMasterWithStatusCatg";
        cmd.Parameters.Add(new SqlParameter("@StatusCategoryId", SqlDbType.Int));
        cmd.Parameters["@StatusCategoryId"].Value = StatusCategoryId;
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }
    public DataTable SelectStatusCategoryMasterINGRID(Int32 StatusCategoryId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectStatusCategoryMasterINGRID";
        cmd.Parameters.Add(new SqlParameter("@StatusCategoryId", SqlDbType.Int));
        cmd.Parameters["@StatusCategoryId"].Value = StatusCategoryId;
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }
    public bool UpdateStatusMaster(Int32 StatusId, Int32 StatusCategoryId, String StatusName, String Description)
    {

        cmd = new SqlCommand();

        cmd.CommandText = "UpdateStatusMaster";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@StatusId", SqlDbType.Int));
        cmd.Parameters["@StatusId"].Value = StatusId;
        cmd.Parameters.Add(new SqlParameter("@StatusCategoryId", SqlDbType.Int));
        cmd.Parameters["@StatusCategoryId"].Value = StatusCategoryId;
        cmd.Parameters.Add(new SqlParameter("@StatusName", SqlDbType.NVarChar));
        cmd.Parameters["@StatusName"].Value = StatusName;
        cmd.Parameters.Add(new SqlParameter("@Description", SqlDbType.NVarChar));
        cmd.Parameters["@Description"].Value = Description;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        int result = DatabaseCls1.ExecuteNonQuery(cmd);
        return (result != -1);
    }



    public Int32 InsertAccountMaster(Int32 GroupId, Int32 StatusId, String AccountName, String Description, String AccountDate, String OpeningBalance)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "InsertAccountMaster";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@GroupId", SqlDbType.Int));
        cmd.Parameters["@GroupId"].Value = GroupId;
        cmd.Parameters.Add(new SqlParameter("@StatusId", SqlDbType.Int));
        cmd.Parameters["@StatusId"].Value = StatusId;
        cmd.Parameters.Add(new SqlParameter("@AccountName", SqlDbType.NVarChar));
        cmd.Parameters["@AccountName"].Value = AccountName;
        cmd.Parameters.Add(new SqlParameter("@Description", SqlDbType.NVarChar));
        cmd.Parameters["@Description"].Value = Description;
        cmd.Parameters.Add(new SqlParameter("@AccountDate", SqlDbType.DateTime));
        cmd.Parameters["@AccountDate"].Value = AccountDate;
        cmd.Parameters.Add(new SqlParameter("@OpeningBalance", SqlDbType.Decimal));
        cmd.Parameters["@OpeningBalance"].Value = OpeningBalance;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls1.ExecuteNonQuery(cmd);
        return result;
    }
    public DataTable SelectAccountMaster()
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectAccountMaster";
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }
    public DataTable SelectAccountMasterWithGroup(Int32 GroupId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectAccountMasterWithGroup";
        cmd.Parameters.Add(new SqlParameter("@GroupId", SqlDbType.Int));
        cmd.Parameters["@GroupId"].Value = GroupId;
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }
    public DataTable SelectAccountMasterWithStatus(Int32 StatusId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectAccountMasterWithStatus";
        cmd.Parameters.Add(new SqlParameter("@StatusId", SqlDbType.Int));
        cmd.Parameters["@StatusId"].Value = StatusId;
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }
    public bool UpdateAccountMaster(Int32 AccountId, Int32 GroupId, Int32 StatusId, String AccountName, String Description, String AccountDate, String OpeningBalance)
    {

        cmd = new SqlCommand();

        cmd.CommandText = "UpdateAccountMaster";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@AccountId", SqlDbType.Int));
        cmd.Parameters["@AccountId"].Value = AccountId;
        cmd.Parameters.Add(new SqlParameter("@GroupId", SqlDbType.Int));
        cmd.Parameters["@GroupId"].Value = GroupId;
        cmd.Parameters.Add(new SqlParameter("@StatusId", SqlDbType.Int));
        cmd.Parameters["@StatusId"].Value = StatusId;
        cmd.Parameters.Add(new SqlParameter("@AccountName", SqlDbType.NVarChar));
        cmd.Parameters["@AccountName"].Value = AccountName;
        cmd.Parameters.Add(new SqlParameter("@Description", SqlDbType.NVarChar));
        cmd.Parameters["@Description"].Value = Description;
        cmd.Parameters.Add(new SqlParameter("@AccountDate", SqlDbType.DateTime));
        cmd.Parameters["@AccountDate"].Value = AccountDate;
        cmd.Parameters.Add(new SqlParameter("@OpeningBalance", SqlDbType.Decimal));
        cmd.Parameters["@OpeningBalance"].Value = OpeningBalance;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        int result = DatabaseCls1.ExecuteNonQuery(cmd);
        return (result != -1);
    }

    // Haiyal End

    // alkesh 25-02-2008

    public DataTable SelectLoginMasterByEmployeeID(int PartyID)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectLoginMasterByEmployeeID";
        //cmd.Parameters.Add(new SqlParameter("@EmployeeId", SqlDbType.Int));
        //cmd.Parameters["@EmployeeId"].Value = EmployeeId;
        cmd.Parameters.Add(new SqlParameter("@PartyID", SqlDbType.Int));
        cmd.Parameters["@PartyID"].Value = PartyID;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["CompanyLoginId"].ToString(); // CompanyLoginId;
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }

    public DataTable SelectLoginMasterByEmployeeIDPassword(int PartyID, string UserPassword, int LoginId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectLoginMasterByEmployeeIDPassword";
        //cmd.Parameters.Add(new SqlParameter("@EmployeeId", SqlDbType.Int));
        //cmd.Parameters["@EmployeeId"].Value = EmployeeId;
        cmd.Parameters.Add(new SqlParameter("@PartyID", SqlDbType.Int));
        cmd.Parameters["@PartyID"].Value = PartyID;
        cmd.Parameters.Add(new SqlParameter("@UserPassword", SqlDbType.VarChar));
        cmd.Parameters["@UserPassword"].Value = UserPassword;
        cmd.Parameters.Add(new SqlParameter("@LoginId", SqlDbType.Int));
        cmd.Parameters["@LoginId"].Value = LoginId;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["CompanyLoginId"].ToString(); // CompanyLoginId;
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }

    public bool UpdateLoginMasterByLoginID(int LoginId, string NewPassword)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "UpdateLoginMasterByLoginID";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@LoginId", SqlDbType.Int));
        cmd.Parameters["@LoginId"].Value = LoginId;
        cmd.Parameters.Add(new SqlParameter("@NewPassword", SqlDbType.VarChar));
        cmd.Parameters["@NewPassword"].Value = NewPassword;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["CompanyLoginId"].ToString(); // CompanyLoginId;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        int result = DatabaseCls1.ExecuteNonQuery(cmd);
        return (result != -1);
    }

    // Haiyal ==== 25-02-2009==================================
    public DataTable SelectPartyTypeMaster()
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectPartyTypeMaster";
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["CompanyLoginId"].ToString(); // CompanyLoginId;
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }

    public DataTable SelectPartyAddressTypeMaster()
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectPartyAddressTypeMaster";
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["CompanyLoginId"].ToString(); // CompanyLoginId;
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }



    public DataTable SelectACCOUNTINGDesg()
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectACCOUNTINGDesg";
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }
    public DataTable SelectSALESDesg()
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectSALESDesg";
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }
    public DataTable SelectSHIPPINGDesg()
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectSHIPPINGDesg";
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }
    public DataTable SelectPURCHASEDesg()
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectPURCHASEDesg";
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }
    public DataTable SelectAssignACCOUNTInchargeWithDesg_PartyMaster(Int32 DesignationID)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectAssignACCOUNTInchargeWithDesg_PartyMaster";
        cmd.Parameters.Add(new SqlParameter("@DesignationID", SqlDbType.Int));
        cmd.Parameters["@DesignationID"].Value = DesignationID;
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }
    public DataTable SelectAssignSALESInchargeWithDesg_PartyMaster(Int32 DesignationID)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectAssignSALESInchargeWithDesg_PartyMaster";
        cmd.Parameters.Add(new SqlParameter("@DesignationID", SqlDbType.Int));
        cmd.Parameters["@DesignationID"].Value = DesignationID;
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }
    public DataTable SelectAssignSHIPPINGInchargeWithDesg_PartyMaster(Int32 DesignationID)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectAssignSHIPPINGInchargeWithDesg_PartyMaster";
        cmd.Parameters.Add(new SqlParameter("@DesignationID", SqlDbType.Int));
        cmd.Parameters["@DesignationID"].Value = DesignationID;
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }
    public DataTable SelectAssignPURCHASEInchargeWithDesg_PartyMaster(Int32 DesignationID)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectAssignPURCHASEInchargeWithDesg_PartyMaster";
        cmd.Parameters.Add(new SqlParameter("@DesignationID", SqlDbType.Int));
        cmd.Parameters["@DesignationID"].Value = DesignationID;
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }

    public DataTable SelectAssignACCOUNTIncharge_PartyMaster()
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectAssignACCOUNTIncharge_PartyMaster";
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["CompanyLoginId"].ToString(); // CompanyLoginId;
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }
    public DataTable SelectAssignSALESIncharge_PartyMaster()
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectAssignSALESIncharge_PartyMaster";
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["CompanyLoginId"].ToString(); // CompanyLoginId;
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }
    public DataTable SelectAssignSHIPPINGIncharge_PartyMaster()
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectAssignSHIPPINGIncharge_PartyMaster";
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["CompanyLoginId"].ToString(); // CompanyLoginId;
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }
    public DataTable SelectAssignPURCHASEIncharge_PartyMaster()
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectAssignPURCHASEIncharge_PartyMaster";
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["CompanyLoginId"].ToString(); // CompanyLoginId;
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }


    public Int32 InsertPartyMaster(Int32 PartyTypeId, Int32 AccountId, Int32 StatusId, String PartyName, String ContactPerson, String IncomeTaxNo, Int32 AssignedAccountManagerInchargeId, Int32 AssignedPurchaseDepartmentInchargeId, Int32 AssignedShippingDepartmentInchargeId, Int32 AssignedSalesDepartmentInchargeId, String GSTNo, String Email)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "InsertPartyMaster";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@PartyTypeId", SqlDbType.Int));
        cmd.Parameters["@PartyTypeId"].Value = PartyTypeId;
        cmd.Parameters.Add(new SqlParameter("@AccountId", SqlDbType.Int));
        cmd.Parameters["@AccountId"].Value = AccountId;
        cmd.Parameters.Add(new SqlParameter("@StatusId", SqlDbType.Int));
        cmd.Parameters["@StatusId"].Value = StatusId;
        cmd.Parameters.Add(new SqlParameter("@PartyName", SqlDbType.NVarChar));
        cmd.Parameters["@PartyName"].Value = PartyName;
        cmd.Parameters.Add(new SqlParameter("@ContactPerson", SqlDbType.NVarChar));
        cmd.Parameters["@ContactPerson"].Value = ContactPerson;
        cmd.Parameters.Add(new SqlParameter("@IncomeTaxNo", SqlDbType.NVarChar));
        cmd.Parameters["@IncomeTaxNo"].Value = IncomeTaxNo;

        cmd.Parameters.Add(new SqlParameter("@GSTNo", SqlDbType.NVarChar));
        cmd.Parameters["@GSTNo"].Value = GSTNo;
        cmd.Parameters.Add(new SqlParameter("@AssignedAccountManagerInchargeId", SqlDbType.Int));
        cmd.Parameters["@AssignedAccountManagerInchargeId"].Value = AssignedAccountManagerInchargeId;
        cmd.Parameters.Add(new SqlParameter("@AssignedPurchaseDepartmentInchargeId", SqlDbType.Int));
        cmd.Parameters["@AssignedPurchaseDepartmentInchargeId"].Value = AssignedPurchaseDepartmentInchargeId;
        cmd.Parameters.Add(new SqlParameter("@AssignedShippingDepartmentInchargeId", SqlDbType.Int));
        cmd.Parameters["@AssignedShippingDepartmentInchargeId"].Value = AssignedShippingDepartmentInchargeId;
        cmd.Parameters.Add(new SqlParameter("@AssignedSalesDepartmentInchargeId", SqlDbType.Int));
        cmd.Parameters["@AssignedSalesDepartmentInchargeId"].Value = AssignedSalesDepartmentInchargeId;
        cmd.Parameters.Add(new SqlParameter("@Email", SqlDbType.NVarChar));
        cmd.Parameters["@Email"].Value = Email;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["CompanyLoginId"].ToString(); // CompanyLoginId;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        cmd.Parameters.Add(new SqlParameter("@PartyId", SqlDbType.Int));
        cmd.Parameters["@PartyId"].Direction = ParameterDirection.Output;

        Int32 result = DatabaseCls1.ExecuteNonQuery(cmd);
        result = Convert.ToInt32(cmd.Parameters["@PartyId"].Value.ToString());
        return result;
    }
    public Int32 InsertPartyAddressDetail(Int32 PartyId, Int32 PartyAddressTypeId, String Address, String City, String StateName, String PinCode, String Email, String Fax, String ContactNo, String WebsiteAddress, String CountyName, string ContactExt)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "InsertPartyAddressDetail";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@PartyId", SqlDbType.Int));
        cmd.Parameters["@PartyId"].Value = PartyId;
        cmd.Parameters.Add(new SqlParameter("@PartyAddressTypeId", SqlDbType.Int));
        cmd.Parameters["@PartyAddressTypeId"].Value = PartyAddressTypeId;
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
        cmd.Parameters.Add(new SqlParameter("@ContactExt", SqlDbType.NVarChar));
        cmd.Parameters["@ContactExt"].Value = ContactExt;

        cmd.Parameters.Add(new SqlParameter("@CountryName", SqlDbType.NVarChar));
        cmd.Parameters["@CountryName"].Value = CountyName;

        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;

        Int32 result = DatabaseCls1.ExecuteNonQuery(cmd);
        return result;
    }
    public Int32 InsertPartyLoginMaster(Int32 PartyId, String PartyLoginName, String ParyLoginPassword)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "InsertPartyLoginMaster";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@PartyId", SqlDbType.Int));
        cmd.Parameters["@PartyId"].Value = PartyId;
        cmd.Parameters.Add(new SqlParameter("@PartyLoginName", SqlDbType.NVarChar));
        cmd.Parameters["@PartyLoginName"].Value = PartyLoginName;
        cmd.Parameters.Add(new SqlParameter("@PartyLoginPassword", SqlDbType.NVarChar));
        cmd.Parameters["@PartyLoginPassword"].Value = ParyLoginPassword;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls1.ExecuteNonQuery(cmd);
        result = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value);
        return result;
    }
    public Int32 InsertAccountMasterParty(Int32 GroupId, Int32 StatusId, String AccountName, String Description, String AccountDate, String OpeningBalance)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "InsertAccountMasterParty";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@GroupId", SqlDbType.Int));
        cmd.Parameters["@GroupId"].Value = GroupId;
        cmd.Parameters.Add(new SqlParameter("@StatusId", SqlDbType.Int));
        cmd.Parameters["@StatusId"].Value = StatusId;
        cmd.Parameters.Add(new SqlParameter("@AccountName", SqlDbType.NVarChar));
        cmd.Parameters["@AccountName"].Value = AccountName;
        cmd.Parameters.Add(new SqlParameter("@Description", SqlDbType.NVarChar));
        cmd.Parameters["@Description"].Value = Description;
        cmd.Parameters.Add(new SqlParameter("@AccountDate", SqlDbType.DateTime));
        cmd.Parameters["@AccountDate"].Value = AccountDate;
        cmd.Parameters.Add(new SqlParameter("@OpeningBalance", SqlDbType.Decimal));
        cmd.Parameters["@OpeningBalance"].Value = OpeningBalance;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["CompanyLoginId"].ToString(); // CompanyLoginId;
        cmd.Parameters.Add(new SqlParameter("@AccountID", SqlDbType.Int));
        cmd.Parameters["@AccountID"].Direction = ParameterDirection.Output;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls1.ExecuteNonQuery(cmd);
        result = Convert.ToInt32(cmd.Parameters["@AccountID"].Value.ToString());
        return result;
    }

    public DataTable SelectPartyAddressDetail(Int32 PartyId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectPartyAddressDetail";
        cmd.Parameters.Add(new SqlParameter("@PartyId", SqlDbType.Int));
        cmd.Parameters["@PartyId"].Value = PartyId;
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }
    public DataTable selectEmployeeforReporting(Int32 DepartmentId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "selectEmployeeforReporting";
        cmd.Parameters.Add(new SqlParameter("@DepartmentId", SqlDbType.Int));
        cmd.Parameters["@DepartmentId"].Value = DepartmentId;
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }

    public DataTable SelectCompanyMaster()
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectCompanyMaster";
        cmd.Parameters.Add(new SqlParameter("@CompanyName", SqlDbType.NVarChar));
        cmd.Parameters["@CompanyName"].Value = HttpContext.Current.Session["CompanyLoginId"].ToString(); // CompanyLoginId;
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }
    public DataTable SelectCompanyMaster1()
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectCompanyMaster1";
        //cmd.Parameters.Add(new SqlParameter("@CompanyName", SqlDbType.NVarChar));
        //cmd.Parameters["@CompanyName"].Value = HttpContext.Current.Session["CompanyLoginId"].ToString(); // CompanyLoginId;
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }

    // 2-3-09 start Neetu
    public DataTable SelectPartyMasterAll()
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectPartyMasterAll";
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["CompanyLoginId"].ToString(); // CompanyLoginId;
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }
    public DataTable SelectPartyMasterAsNotEmployee(string storeid)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectPartyMasterAsNotEmployee";
        cmd.Parameters.Add(new SqlParameter("@Whid", SqlDbType.NVarChar));
        cmd.Parameters["@Whid"].Value = storeid; // CompanyLoginId;
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;
    }
    public DataTable SelectPartyMasterAsEmployee()
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectPartyMasterAsEmployee";
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["CompanyLoginId"].ToString(); // CompanyLoginId;
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }
    public DataTable SelectPartyMasterbyId(Int32 PartyId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectPartyMasterbyId";
        cmd.Parameters.Add(new SqlParameter("@PartyId", SqlDbType.Int));
        cmd.Parameters["@PartyId"].Value = PartyId;
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }
    public bool UpdatePartyMaster(Int32 PartyId, Int32 PartyTypeId, Int32 StatusId, String PartyName, String ContactPerson, String IncomeTaxNo, Int32 AssignedAccountManagerInchargeId, Int32 AssignedPurchaseDepartmentInchargeId, Int32 AssignedShippingDepartmentInchargeId, Int32 AssignedSalesDepartmentInchargeId, String GSTNo)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "UpdatePartyMaster";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@PartyId", SqlDbType.Int));
        cmd.Parameters["@PartyId"].Value = PartyId;
        cmd.Parameters.Add(new SqlParameter("@PartyTypeId", SqlDbType.Int));
        cmd.Parameters["@PartyTypeId"].Value = PartyTypeId;
        cmd.Parameters.Add(new SqlParameter("@StatusId", SqlDbType.Int));
        cmd.Parameters["@StatusId"].Value = StatusId;
        cmd.Parameters.Add(new SqlParameter("@PartyName", SqlDbType.NVarChar));
        cmd.Parameters["@PartyName"].Value = PartyName;
        cmd.Parameters.Add(new SqlParameter("@ContactPerson", SqlDbType.NVarChar));
        cmd.Parameters["@ContactPerson"].Value = ContactPerson;
        cmd.Parameters.Add(new SqlParameter("@IncomeTaxNo", SqlDbType.NVarChar));
        cmd.Parameters["@IncomeTaxNo"].Value = IncomeTaxNo;
        cmd.Parameters.Add(new SqlParameter("@GSTNo", SqlDbType.NVarChar));
        cmd.Parameters["@GSTNo"].Value = GSTNo;
        cmd.Parameters.Add(new SqlParameter("@AssignedAccountManagerInchargeId", SqlDbType.Int));
        cmd.Parameters["@AssignedAccountManagerInchargeId"].Value = AssignedAccountManagerInchargeId;
        cmd.Parameters.Add(new SqlParameter("@AssignedPurchaseDepartmentInchargeId", SqlDbType.Int));
        cmd.Parameters["@AssignedPurchaseDepartmentInchargeId"].Value = AssignedPurchaseDepartmentInchargeId;
        cmd.Parameters.Add(new SqlParameter("@AssignedShippingDepartmentInchargeId", SqlDbType.Int));
        cmd.Parameters["@AssignedShippingDepartmentInchargeId"].Value = AssignedShippingDepartmentInchargeId;
        cmd.Parameters.Add(new SqlParameter("@AssignedSalesDepartmentInchargeId", SqlDbType.Int));
        cmd.Parameters["@AssignedSalesDepartmentInchargeId"].Value = AssignedSalesDepartmentInchargeId;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;


        Int32 result = DatabaseCls1.ExecuteNonQuery(cmd);
        result = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value.ToString());
        return (result != -1);
    }
    public bool UpdatePartyFolderRelation(Int32 PartyId, string FolderId, Boolean Partydefaultfolder, Boolean Folderdefaultparty)
    {
        Int32 fldid = Convert.ToInt32(FolderId);
        cmd = new SqlCommand();
        cmd.CommandText = "UpdatePartyFolderRelation";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@PartyId", SqlDbType.Int));
        cmd.Parameters["@PartyId"].Value = PartyId;
        cmd.Parameters.Add(new SqlParameter("@FolderId", SqlDbType.Int));
        cmd.Parameters["@FolderId"].Value = fldid;
        cmd.Parameters.Add(new SqlParameter("@Partydefaultfolder", SqlDbType.Bit));
        cmd.Parameters["@Partydefaultfolder"].Value = Partydefaultfolder;
        cmd.Parameters.Add(new SqlParameter("@Folderdefaultparty", SqlDbType.Bit));
        cmd.Parameters["@Folderdefaultparty"].Value = Folderdefaultparty;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;

        //cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        //cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        //Int32 result = DatabaseCls1.ExecuteNonQuery(cmd);
        //result = Int32.Parse(cmd.Parameters["@ReturnValue"].Value.ToString());
        //return result;
        Int32 result = DatabaseCls1.ExecuteNonQuery(cmd);
        result = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value.ToString());
        return (result != -1);
    }
    public bool UpdatePartyFolderRelationFromcabinet(string PartyId, Int32 FolderId, Boolean Partydefaultfolder, Boolean Folderdefaultparty)
    {
        Int32 prtid = Convert.ToInt32(PartyId);
        cmd = new SqlCommand();
        cmd.CommandText = "UpdatePartyFolderRelationFromcabinet";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@PartyId", SqlDbType.Int));
        cmd.Parameters["@PartyId"].Value = prtid;
        cmd.Parameters.Add(new SqlParameter("@FolderId", SqlDbType.Int));
        cmd.Parameters["@FolderId"].Value = FolderId;
        cmd.Parameters.Add(new SqlParameter("@Partydefaultfolder", SqlDbType.Bit));
        cmd.Parameters["@Partydefaultfolder"].Value = Partydefaultfolder;
        cmd.Parameters.Add(new SqlParameter("@Folderdefaultparty", SqlDbType.Bit));
        cmd.Parameters["@Folderdefaultparty"].Value = Folderdefaultparty;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;

        //cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        //cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        //Int32 result = DatabaseCls1.ExecuteNonQuery(cmd);
        //result = Int32.Parse(cmd.Parameters["@ReturnValue"].Value.ToString());
        //return result;
        Int32 result = DatabaseCls1.ExecuteNonQuery(cmd);
        result = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value.ToString());
        return (result != -1);
    }
    public bool DeletePartyAddressDetail(Int32 PartyId)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "DeletePartyAddressDetail";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@PartyId", SqlDbType.Int));
        cmd.Parameters["@PartyId"].Value = PartyId;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls1.ExecuteNonQuery(cmd);
        result = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value.ToString());
        return (result != -1);
    }
    public bool DeletePartyMaster(Int32 PartyId)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "DeletePartyMaster";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@PartyID", SqlDbType.Int));
        cmd.Parameters["@PartyID"].Value = PartyId;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["CompanyLoginId"].ToString(); // CompanyLoginId;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls1.ExecuteNonQuery(cmd);
        result = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value.ToString());
        return (result != -1);
    }
    public bool DeleteCountryByID(Int32 CountryId)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "DeleteCountryByID";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@CountryId", SqlDbType.Int));
        cmd.Parameters["@CountryId"].Value = CountryId;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls1.ExecuteNonQuery(cmd);
        result = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value.ToString());
        return (result != -1);
    }
    public bool DeleteStateByID(Int32 StateId)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "DeleteStateByID";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@StateId", SqlDbType.Int));
        cmd.Parameters["@StateId"].Value = StateId;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls1.ExecuteNonQuery(cmd);
        result = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value.ToString());
        return (result != -1);
    }
    public bool DeleteCityMaster(Int32 CityId)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "DeleteCityByID";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@CityId", SqlDbType.Int));
        cmd.Parameters["@CityId"].Value = CityId;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls1.ExecuteNonQuery(cmd);
        result = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value.ToString());
        return (result != -1);
    }
    public DataTable SelectPartyMasterbyPartyTypeId(Int32 PartyTypeId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectPartyMasterbyPartyTypeId";
        cmd.Parameters.Add(new SqlParameter("@PartyTypeId", SqlDbType.Int));
        cmd.Parameters["@PartyTypeId"].Value = PartyTypeId;
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }
    public DataTable SelectCompanyMasterbyIndustryTypeId(Int32 IndustryTypeId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectCompanyMasterbyIndustryTypeId";
        cmd.Parameters.Add(new SqlParameter("@IndustryTypeId", SqlDbType.Int));
        cmd.Parameters["@IndustryTypeId"].Value = IndustryTypeId;
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }
    public DataTable SelectCompanyMasterbyOrganiseTypeId(Int32 OrganiseTypeId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectCompanyMasterbyOrganiseTypeId";
        cmd.Parameters.Add(new SqlParameter("@OrganiseTypeId", SqlDbType.Int));
        cmd.Parameters["@OrganiseTypeId"].Value = OrganiseTypeId;
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }
    public DataTable SelectPartyMasterwithPartyAddressType(Int32 PartyAddressTypeId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectPartyMasterwithPartyAddressType";
        cmd.Parameters.Add(new SqlParameter("@PartyAddressTypeId", SqlDbType.Int));
        cmd.Parameters["@PartyAddressTypeId"].Value = PartyAddressTypeId;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["CompanyLoginId"].ToString(); // CompanyLoginId;
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }
    public DataTable SelectPartyNamebyPartyTypeId(Int32 PartyTypeId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectPartyNamebyPartyTypeId";
        cmd.Parameters.Add(new SqlParameter("@PartyTypeId", SqlDbType.Int));
        cmd.Parameters["@PartyTypeId"].Value = PartyTypeId;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["CompanyLoginId"].ToString(); // CompanyLoginId;
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }
    public DataTable SelectPartyMasterbyStatus(Int32 StatusID)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectPartyMasterbyStatus";
        cmd.Parameters.Add(new SqlParameter("@StatusId", SqlDbType.Int));
        cmd.Parameters["@StatusId"].Value = StatusID;
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }
    public DataTable SelectPartyMasterAddressTypeWise(Int32 PartyId, Int32 PartyAddressTypeId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectPartyMasterAddressTypeWise";
        cmd.Parameters.Add(new SqlParameter("@PartyId", SqlDbType.Int));
        cmd.Parameters["@PartyId"].Value = PartyId;
        cmd.Parameters.Add(new SqlParameter("@PartyAddressTypeId", SqlDbType.Int));
        cmd.Parameters["@PartyAddressTypeId"].Value = PartyAddressTypeId;
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }
    public DataTable SelectPartyAddressDetailIDwise(Int32 PartyAddressDetailId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectPartyAddressDetailIDwise";
        cmd.Parameters.Add(new SqlParameter("@PartyAddressDetailId", SqlDbType.Int));
        cmd.Parameters["@PartyAddressDetailId"].Value = PartyAddressDetailId;

        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }
    public DataTable SelectTaxMaster()
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectTaxMaster";
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }

    //--------------------------9-3-2009------------haiyal-----------
    public Int32 InsertTaxMaster(String TaxName, String Percentage, String Amount, Boolean ChargetoAllSaleInvoice)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "InsertTaxMaster";
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.Add(new SqlParameter("@TaxName", SqlDbType.NVarChar));
        cmd.Parameters["@TaxName"].Value = TaxName;
        cmd.Parameters.Add(new SqlParameter("@Percentage", SqlDbType.NVarChar));
        cmd.Parameters["@Percentage"].Value = Percentage;
        cmd.Parameters.Add(new SqlParameter("@Amount", SqlDbType.NVarChar));
        cmd.Parameters["@Amount"].Value = Amount;
        cmd.Parameters.Add(new SqlParameter("@ChargetoAllSaleInvoice", SqlDbType.Bit));
        cmd.Parameters["@ChargetoAllSaleInvoice"].Value = ChargetoAllSaleInvoice;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls1.ExecuteNonQuery(cmd);
        // result = Convert.ToInt32(cmd.Parameters["@AccountID"].Value.ToString());
        return result;
    }

    public bool UpdateTaxMaster(Int32 TaxId, String TaxName, String Percentage, String Amount, Boolean ChargetoAllSaleInvoice)
    {

        cmd = new SqlCommand();

        cmd.CommandText = "UpdateTaxMaster";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@TaxId", SqlDbType.Int));
        cmd.Parameters["@TaxId"].Value = TaxId;
        cmd.Parameters.Add(new SqlParameter("@TaxName", SqlDbType.NVarChar));
        cmd.Parameters["@TaxName"].Value = TaxName;
        cmd.Parameters.Add(new SqlParameter("@Percentage", SqlDbType.NVarChar));
        cmd.Parameters["@Percentage"].Value = Percentage;
        cmd.Parameters.Add(new SqlParameter("@Amount", SqlDbType.NVarChar));
        cmd.Parameters["@Amount"].Value = Amount;
        cmd.Parameters.Add(new SqlParameter("@ChargetoAllSaleInvoice", SqlDbType.Bit));
        cmd.Parameters["@ChargetoAllSaleInvoice"].Value = ChargetoAllSaleInvoice;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        int result = DatabaseCls1.ExecuteNonQuery(cmd);
        return (result != -1);
    }
    public DataTable SelectTaxMasterWithTaxId(Int32 TaxId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectTaxMasterWithTaxId";
        cmd.Parameters.Add(new SqlParameter("@TaxId", SqlDbType.Int));
        cmd.Parameters["@TaxId"].Value = TaxId;

        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }

    public DataTable SelectPartyMasterAllforList()
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectPartyMasterAllforList";
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["CompanyLoginId"].ToString(); // CompanyLoginId;
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }

    //neetu 17-3-09

    public DataTable SelectDesignationMasterbyDesId(Int32 DesId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectDesignationMasterbyDesId";
        cmd.Parameters.Add(new SqlParameter("@DesignationId", SqlDbType.Int));
        cmd.Parameters["@DesignationId"].Value = DesId;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["CompanyLoginId"].ToString(); // CompanyLoginId;
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }
    public DataTable SelectDesignationMasterbyEmpId(Int32 EmployeeId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectDesignationMasterbyEmpId";
        cmd.Parameters.Add(new SqlParameter("@EmployeeID", SqlDbType.Int));
        cmd.Parameters["@EmployeeID"].Value = EmployeeId;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["CompanyLoginId"].ToString(); // CompanyLoginId;
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }
    // Neetu 23-3-09 

    public DataTable selectLoginAttemptMaster(String UserName)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "selectLoginAttemptMaster";
        cmd.Parameters.Add(new SqlParameter("@UserName", SqlDbType.NVarChar));
        cmd.Parameters["@UserName"].Value = UserName;
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }

    public bool InsertLoginAttemptMaster(String UserName)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "InsertLoginAttemptMaster";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@UserName", SqlDbType.NVarChar));
        cmd.Parameters["@UserName"].Value = UserName;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls1.ExecuteNonQuery(cmd);
        result = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value.ToString());
        return (result != -1);
    }

    ///-------------------haiyal 24-3-2009
    ///

    public DataTable SelectFormatMaster()
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectFormatMaster";
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }

    public Int32 InsertFormatMaster(String FormatName)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "InsertFormatMaster";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@FormatName", SqlDbType.NVarChar));
        cmd.Parameters["@FormatName"].Value = FormatName;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls1.ExecuteNonQuery(cmd);
        return result;
    }
    public bool UpdateFormatMaster(Int32 FormatId, String FormatName)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "UpdateFormatMaster";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@FormatId", SqlDbType.Int));
        cmd.Parameters["@FormatId"].Value = FormatId;
        cmd.Parameters.Add(new SqlParameter("@FormatName", SqlDbType.NVarChar));
        cmd.Parameters["@FormatName"].Value = FormatName;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        int result = DatabaseCls1.ExecuteNonQuery(cmd);
        return (result != -1);
    }
    public bool UpdateEmpSignature(Int32 EmployeeId, String EmployeeSignature)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "UpdateEmpSignature";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@EmployeeId", SqlDbType.Int));
        cmd.Parameters["@EmployeeId"].Value = EmployeeId;
        cmd.Parameters.Add(new SqlParameter("@EmployeeSignature", SqlDbType.NVarChar));
        cmd.Parameters["@EmployeeSignature"].Value = EmployeeSignature;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        int result = DatabaseCls1.ExecuteNonQueryep(cmd);
        return (result != -1);
    }
    public bool DeleteFormatMaster(Int32 FormatId)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "DeleteFormatMaster";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@FormatId", SqlDbType.Int));
        cmd.Parameters["@FormatId"].Value = FormatId;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;

        Int32 result = DatabaseCls1.ExecuteNonQuery(cmd);
        result = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value.ToString());
        return (result != -1);
    }
    public bool DeletePageMaster(Int32 PageId)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "DeletePageMaster";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@PageId", SqlDbType.Int));
        cmd.Parameters["@PageId"].Value = PageId;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;

        Int32 result = DatabaseCls1.ExecuteNonQuery(cmd);
        result = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value.ToString());
        return (result != -1);
    }
    // New on Server

    public DataTable SelectPageMasterbyPageName(String PageName) //,ref Int32  EmployeeId, ref  Int32  DesignationId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectPageMasterbyPageName";
        cmd.Parameters.Add(new SqlParameter("@PageName", SqlDbType.NVarChar));
        cmd.Parameters["@PageName"].Value = PageName;
        //cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        //cmd.Parameters["@CID"].Value = HttpContext.Current.Session["CompanyLoginId"].ToString(); // CompanyLoginId;
        cmd.CommandType = CommandType.StoredProcedure;
        SqlCommand cmdd = new SqlCommand();
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }
    public DataTable SelectDesignationMasterwithDeptName()
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectDesignationMasterwithDeptName";
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["CompanyLoginId"].ToString(); // CompanyLoginId;
        //cmd.Parameters.Add(new SqlParameter("@DepartmentId", SqlDbType.Int));
        //cmd.Parameters["@DepartmentId"].Value = DepartmentId;

        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }

    public DataTable SelectLoginMasterforList()
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectLoginMasterforList";
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }
    // Insert Template 12/5/2009
    //public Int32 InsertFormatMaster(String FormatName)
    //{
    //    cmd = new SqlCommand();
    //    cmd.CommandText = "InsertFormatMaster";
    //    cmd.CommandType = CommandType.StoredProcedure;
    //    cmd.Parameters.Add(new SqlParameter("@FormatName", SqlDbType.NVarChar));
    //    cmd.Parameters["@FormatName"].Value = FormatName;
    //    cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
    //    cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
    //    Int32 result = DatabaseCls1.ExecuteNonQuery(cmd);
    //    return result;
    //}

    public DataTable SelectCustomerPanelMaster(Int32 PartyId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectCustomerPanelMaster";
        cmd.Parameters.Add(new SqlParameter("@PartyId", SqlDbType.Int));
        cmd.Parameters["@PartyId"].Value = PartyId;

        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }


    public Int32 InsertCustomerPanelMaster(Int32 PartyId, Int32 PanelId, Int32 DocumentTypeId)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "InsertCustomerPanelMaster";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@PartyId", SqlDbType.Int));
        cmd.Parameters["@PartyId"].Value = PartyId;
        cmd.Parameters.Add(new SqlParameter("@PanelId", SqlDbType.Int));
        cmd.Parameters["@PanelId"].Value = PanelId;
        cmd.Parameters.Add(new SqlParameter("@DocumentTypeId", SqlDbType.Int));
        cmd.Parameters["@DocumentTypeId"].Value = DocumentTypeId;

        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls1.ExecuteNonQuery(cmd);
        return result;
    }

    public DataTable SelectCustomerPanelMasterwithDocType(Int32 PartyId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectCustomerPanelMasterwithDocType";
        cmd.Parameters.Add(new SqlParameter("@PartyId", SqlDbType.Int));
        cmd.Parameters["@PartyId"].Value = PartyId;

        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }
    public Int32 UpdateCustomerPanelMaster(Int32 PartyId, Int32 PanelId, Int32 DocumentTypeId)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "UpdateCustomerPanelMaster";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@PartyId", SqlDbType.Int));
        cmd.Parameters["@PartyId"].Value = PartyId;
        cmd.Parameters.Add(new SqlParameter("@PanelId", SqlDbType.Int));
        cmd.Parameters["@PanelId"].Value = PanelId;
        cmd.Parameters.Add(new SqlParameter("@DocumentTypeId", SqlDbType.Int));
        cmd.Parameters["@DocumentTypeId"].Value = DocumentTypeId;

        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls1.ExecuteNonQuery(cmd);
        return result;
    }


    //--------------------------haiyal 15-4-2009


    //public Int32 SelectLoginMaster_PartyusingName(String UserName) //,ref Int32  EmployeeId, ref  Int32  DesignationId)
    //{
    //    cmd = new SqlCommand();
    //    dt = new DataTable();
    //    Int32 rtn;
    //    cmd.CommandText = "SelectLoginMaster_PartyusingName";
    //    cmd.Parameters.Add(new SqlParameter("@UserName", SqlDbType.NVarChar));
    //    cmd.Parameters["@UserName"].Value = UserName;
    //    cmd.Parameters.Add(new SqlParameter("@LoginId", SqlDbType.Int));
    //    cmd.Parameters["@LoginId"].Direction = ParameterDirection.Output;
    //    cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
    //    cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
    //    cmd.CommandType = CommandType.StoredProcedure;
    //    SqlCommand cmdd = new SqlCommand();
    //    cmdd = (SqlCommand)DatabaseCls1.ExecuteNonQuerywithReturn(cmd);
    //    //EmployeeId = Int32.Parse(cmdd.Parameters["@EmployeeId"].Value.ToString());
    //    //DesignationId = Int32.Parse(cmdd.Parameters["@DesignationId"].Value.ToString());
    //    rtn = Int32.Parse(cmd.Parameters["@LoginId"].Value.ToString());
    //    return rtn;
    //}
    // 18-5-09



    //public DataTable SelectPartyMasterAllforAddressList()
    //{
    //    cmd = new SqlCommand();
    //    dt = new DataTable();
    //    cmd.CommandText = "SelectPartyMasterAllforAddressList";
    //    dt = DatabaseCls1.FillAdapter(cmd);
    //    return dt;
    //}
    // 19-5-09 Function for Price Plan selection
    //public bool CheckPageStatus(  String PageName)
    //{
    //    bool status = false;
    //    string priceplanid =    HttpContext.Current.Session["PricePlanId"].ToString() ;
    //    cmd = new SqlCommand();
    //    dt = new DataTable();
    //    string str = "SELECT * from PricePlanView where PricePlanId = "+priceplanid ;
    //    cmd.CommandText = str ;
    //    dt = DatabaseCls1.FillAdapterIfile(cmd);
    //    //
    //    if (dt.Rows.Count > 0)
    //    {
    //        switch (PageName)
    //        {
    //            //case "WizardDocumentFTPDownload.aspx":
    //            //    if (dt.Rows[0]["autoretrivalemailftp"].ToString() == "0")
    //            //    { status = false; }
    //            //    else
    //            //    { status = true; }
    //            //case "WizardDocumentFTPDownload13.aspx":
    //            //    if (dt.Rows[0]["autoretrivalemailftp"].ToString() == "0")
    //            //    { status = false; }
    //            //    else
    //            //    { status = true; }      
    //        }
    //    }
    //    return status ;
    //}
    //public DataTable  GetPageStatus()
    //{
    //    bool status = false;
    //    string priceplanid = HttpContext.Current.Session["PricePlanId"].ToString();
    //    cmd = new SqlCommand();
    //    dt = new DataTable();
    //    string str = "SELECT * from PricePlanView where PricePlanId = " + priceplanid;
    //    cmd.CommandText = str;
    //    dt = DatabaseCls1.FillAdapterIfile(cmd);
    //    //

    //      return dt;
    //}






    //----------update 22-5-2009


    //public DataTable SelectPartyMasterAllforAddressList()
    //{
    //    cmd = new SqlCommand();
    //    dt = new DataTable();
    //    cmd.CommandText = "SelectPartyMasterAllforAddressList";
    //    dt = DatabaseCls1.FillAdapter(cmd);
    //    return dt;
    //}





    public Int32 SelectLoginMaster_PartyusingName(String UserName) //,ref Int32  EmployeeId, ref  Int32  DesignationId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        Int32 rtn;
        cmd.CommandText = "SelectLoginMaster_PartyusingName";
        cmd.Parameters.Add(new SqlParameter("@UserName", SqlDbType.NVarChar));
        cmd.Parameters["@UserName"].Value = UserName;
        cmd.Parameters.Add(new SqlParameter("@LoginId", SqlDbType.Int));
        cmd.Parameters["@LoginId"].Direction = ParameterDirection.Output;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        cmd.CommandType = CommandType.StoredProcedure;
        SqlCommand cmdd = new SqlCommand();
        cmdd = (SqlCommand)DatabaseCls1.ExecuteNonQuerywithReturn(cmd);
        //EmployeeId = Int32.Parse(cmdd.Parameters["@EmployeeId"].Value.ToString());
        //DesignationId = Int32.Parse(cmdd.Parameters["@DesignationId"].Value.ToString());
        rtn = Int32.Parse(cmd.Parameters["@LoginId"].Value.ToString());
        return rtn;
    }


    // 18-5-09

    public DataTable SelectPartyMasterAllforAddressList()
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectPartyMasterAllforAddressList";
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }

    // 19-5-09 Function for Price Plan selection
    public bool CheckPageStatus(String PageName)
    {
        bool status = false;
        string priceplanid = HttpContext.Current.Session["PricePlanId"].ToString();
        cmd = new SqlCommand();
        dt = new DataTable();
        string str = "SELECT * from PricePlanDetail where PricePlanDetailId = " + priceplanid;
        cmd.CommandText = str;
        dt = DatabaseCls1.FillAdapterIfile(cmd);
        //
        if (dt.Rows.Count > 0)
        {
            switch (PageName)
            {

                case "DocumentHideApprovedDoc.aspx":
                    if (dt.Rows[0]["secureddocument"].ToString() == "0")
                    { status = false; }
                    else
                    { status = true; }
                    break;
                case "DocumentFTPDownload.aspx":
                    if (dt.Rows[0]["autoretrivalemailftp"].ToString() == "0")
                    { status = false; }
                    else
                    { status = true; }
                    break;
                case "WizardDocumentFTPDownload.aspx":
                    if (dt.Rows[0]["autoretrivalemailftp"].ToString() == "0")
                    { status = false; }
                    else
                    { status = true; }
                    break;
                case "wizardDocumentEmailDownload.aspx":
                    if (dt.Rows[0]["autoretrivalemailftp"].ToString() == "0")
                    { status = false; }
                    else
                    { status = true; }
                    break;
                case "DocumentEmailDownload.aspx":
                    if (dt.Rows[0]["autoretrivalemailftp"].ToString() == "0")
                    { status = false; }
                    else
                    { status = true; }
                    break;
                case "DocumentMyApproved.aspx":
                    if (dt.Rows[0]["authorisation"].ToString() == "0")
                    { status = false; }
                    else
                    { status = true; }
                    break;


            }
        }
        return status;
        //return dt;
    }
    public DataTable GetPageStatus()
    {
       
        string priceplanid = HttpContext.Current.Session["PricePlanId"].ToString();
        cmd = new SqlCommand();
        dt = new DataTable();
        string str = "SELECT * from PricePlanDetail where PricePlanDetailId = " + priceplanid;
        cmd.CommandText = str;
        dt = DatabaseCls1.FillAdapterIfile(cmd);
        return dt;
    }
    // 26_5_09

    public DataTable selectEmployeewithDesignationAll()
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "selectEmployeewithDesignationAll";
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["CompanyLoginId"].ToString(); // CompanyLoginId;
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }
    public DataTable selectEmployeewithDesignationAllforadminpg()
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "selectEmployeewithDesignationAllforadminpg";
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["CompanyLoginId"].ToString(); // CompanyLoginId;
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }
    // 28-5-09
    public DataTable GetIfileCompInfo(string RedirectURL)
    {
       
        // = HttpContext.Current.Session["PricePlanId"].ToString();
        cmd = new SqlCommand();
        dt = new DataTable();
        string str = "SELECT * from CompanyMaster  where redirect = '" + RedirectURL + "'";
        cmd.CommandText = str;
        dt = DatabaseCls1.FillAdapterIfile(cmd);
        return dt;
    }
    public DataTable SelectPartyTypeMasterwithoutEmployee()
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectPartyTypeMasterwithoutEmployee";
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["CompanyLoginId"].ToString(); // CompanyLoginId;
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }
    // 17-7-09
    public DataTable selectEmployeewithDepDesAll()
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "selectEmployeewithDepDesAll";
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["CompanyLoginId"].ToString(); // CompanyLoginId;
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }
    public DataTable SelectPartyMasterEmpIdwise(Int32 EmployeeId,String Whid)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectPartyMasterEmpIdwise";
        cmd.Parameters.Add(new SqlParameter("@EmployeeId", SqlDbType.Int));
        cmd.Parameters["@EmployeeId"].Value = EmployeeId;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        cmd.Parameters.Add(new SqlParameter("@Whid", SqlDbType.NVarChar));
        cmd.Parameters["@Whid"].Value = Whid; // CompanyLoginId;
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;
    }
    // 29-4-09
    public DataTable selectEmployeewithDesignationIDEmpId(Int32 DesignationId, Int32 EmployeeId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "selectEmployeewithDesignationIDEmpId";
        cmd.Parameters.Add(new SqlParameter("@DesignationId", SqlDbType.Int));
        cmd.Parameters["@DesignationId"].Value = DesignationId;
        cmd.Parameters.Add(new SqlParameter("@EmployeeId", SqlDbType.Int));
        cmd.Parameters["@EmployeeId"].Value = EmployeeId;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["CompanyLoginId"].ToString(); // CompanyLoginId;
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }
    public DataTable SelectDownloadFolderIdwise(Int32 FolderId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectDownloadFolderIdwise";
        cmd.Parameters.Add(new SqlParameter("@FolderId", SqlDbType.Int));
        cmd.Parameters["@FolderId"].Value = FolderId;

        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }
    public Int32 insertPartyFolderRelation(Int32 PartyId, string FolderId, bool Partydefaultfolder, bool Folderdefaultparty)
    {
        Int32 foldr = Convert.ToInt32(FolderId);
        cmd = new SqlCommand();
        cmd.CommandText = "insertPartyFolderRelation";
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.Add(new SqlParameter("@PartyId", SqlDbType.Int));
        cmd.Parameters["@PartyId"].Value = PartyId;

        cmd.Parameters.Add(new SqlParameter("@FolderId", SqlDbType.Int));
        cmd.Parameters["@FolderId"].Value = foldr;

        cmd.Parameters.Add(new SqlParameter("@Partydefaultfolder", SqlDbType.Bit));
        cmd.Parameters["@Partydefaultfolder"].Value = Partydefaultfolder;

        cmd.Parameters.Add(new SqlParameter("@Folderdefaultparty", SqlDbType.Bit));
        cmd.Parameters["@Folderdefaultparty"].Value = Folderdefaultparty;

        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["CompanyLoginId"].ToString(); // CompanyLoginId;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls1.ExecuteNonQuery(cmd);
        result = Int32.Parse(cmd.Parameters["@ReturnValue"].Value.ToString());
        return result;
        //cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        //cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;

        //Int32 result = DatabaseCls1.ExecuteNonQuery(cmd);
        //return (result != -1);

    }
    public Int32 insertPartyFolderRelation1(String PartyId, Int32 FolderId, bool Partydefaultfolder, bool Folderdefaultparty)
    {
        Int32 prtid = Convert.ToInt32(PartyId);
        cmd = new SqlCommand();
        cmd.CommandText = "insertPartyFolderRelation";
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.Add(new SqlParameter("@PartyId", SqlDbType.Int));
        cmd.Parameters["@PartyId"].Value = prtid;

        cmd.Parameters.Add(new SqlParameter("@FolderId", SqlDbType.Int));
        cmd.Parameters["@FolderId"].Value = FolderId;

        cmd.Parameters.Add(new SqlParameter("@Partydefaultfolder", SqlDbType.Bit));
        cmd.Parameters["@Partydefaultfolder"].Value = Partydefaultfolder;

        cmd.Parameters.Add(new SqlParameter("@Folderdefaultparty", SqlDbType.Bit));
        cmd.Parameters["@Folderdefaultparty"].Value = Folderdefaultparty;

        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["CompanyLoginId"].ToString(); // CompanyLoginId;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls1.ExecuteNonQuery(cmd);
        result = Int32.Parse(cmd.Parameters["@ReturnValue"].Value.ToString());
        return result;
        //cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        //cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;

        //Int32 result = DatabaseCls1.ExecuteNonQuery(cmd);
        //return (result != -1);

    }
    //public bool insertPartyFolderRelation(int partyid, string p, bool Partydefaultfolder, bool Folderdefaultparty)
    //{
    //    throw new NotImplementedException();
    //}
}
