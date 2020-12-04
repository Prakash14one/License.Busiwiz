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
/// Summary description for MasterCls
/// </summary>
public class MasterCls
{
    SqlCommand cmd;
    //SqlDataReader rdr;
    //SqlParameter param;
    //DatabaseCls clsDatabase   ; 
    //DataSet ds;
    DataTable dt;
	public MasterCls()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public DataTable SelectPageTypeMaster()
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectPageTypeMaster";

        dt = DatabaseCls.FillAdapter(cmd);
        return dt;
    }
    public DataTable selectEmployeewithDepDesAll(String Whid)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "selectEmployeewithDepDesAll";
      
        cmd.Parameters.Add(new SqlParameter("@Whid", SqlDbType.NVarChar));
        cmd.Parameters["@Whid"].Value =Whid; // CompanyLoginId;
     
        dt = DatabaseCls.FillAdapter(cmd);
        return dt;
    } 
    public DataTable SelectPageMasterwithPageType(Int32 PageTypeId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectPageMasterwithPageType";
        cmd.Parameters.Add(new SqlParameter("@PageTypeId", SqlDbType.Int));
        cmd.Parameters["@PageTypeId"].Value = PageTypeId;
        dt = DatabaseCls.FillAdapter(cmd);
        return dt;
    }
    public DataTable SelectEmployeewithDesgDeptName(String Whid)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectEmployeewithDesgDeptName";
       
        cmd.Parameters.Add(new SqlParameter("@Whid", SqlDbType.NVarChar));
        cmd.Parameters["@Whid"].Value = Whid; // CompanyLoginId;
        
        dt = DatabaseCls.FillAdapter(cmd);
        return dt;
    }
    public DataTable SelectPartyMasterWithoutEmployeeType(String Whid)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectPartyMasterWithoutEmployeeType";
        cmd.Parameters.Add(new SqlParameter("@Whid", SqlDbType.NVarChar));
        cmd.Parameters["@Whid"].Value = Whid;
        dt = DatabaseCls.FillAdapter(cmd);
        return dt;
    }
    public Int32 InsertPartyMasterMess(Int32 PartyTypeId, Int32 AccountId, String PartyName, String ContactPerson, String Address,String City,String State,String Country, String Website, String Email, String Whid)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "InsertPartyMasterMess";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@PartyTypeId", SqlDbType.Int));
        cmd.Parameters["@PartyTypeId"].Value = PartyTypeId;
        cmd.Parameters.Add(new SqlParameter("@AccountId", SqlDbType.Int));
        cmd.Parameters["@AccountId"].Value = AccountId;
       
        cmd.Parameters.Add(new SqlParameter("@PartyName", SqlDbType.NVarChar));
        cmd.Parameters["@PartyName"].Value = PartyName;
        cmd.Parameters.Add(new SqlParameter("@ContactPerson", SqlDbType.NVarChar));
        cmd.Parameters["@ContactPerson"].Value = ContactPerson;

        cmd.Parameters.Add(new SqlParameter("@Address", SqlDbType.NVarChar));
        cmd.Parameters["@Address"].Value = Address;

        cmd.Parameters.Add(new SqlParameter("@City", SqlDbType.NVarChar));
        cmd.Parameters["@City"].Value =City;

        cmd.Parameters.Add(new SqlParameter("@State", SqlDbType.NVarChar));
        cmd.Parameters["@State"].Value = State;

        cmd.Parameters.Add(new SqlParameter("@Country", SqlDbType.NVarChar));
        cmd.Parameters["@Country"].Value = Country;


        cmd.Parameters.Add(new SqlParameter("@Website", SqlDbType.NVarChar));
        cmd.Parameters["@Website"].Value = Website;
       
      
        cmd.Parameters.Add(new SqlParameter("@Email", SqlDbType.NVarChar));
        cmd.Parameters["@Email"].Value = Email;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        cmd.Parameters.Add(new SqlParameter("@Whid", SqlDbType.NVarChar));
        cmd.Parameters["@Whid"].Value = Whid; // CompanyLoginId;

        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        cmd.Parameters.Add(new SqlParameter("@PartyId", SqlDbType.Int));
        cmd.Parameters["@PartyId"].Direction = ParameterDirection.Output;

        Int32 result = DatabaseCls.ExecuteNonQuery(cmd);
        result = Convert.ToInt32(cmd.Parameters["@PartyId"].Value.ToString());
        return result;
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
        int result = DatabaseCls.ExecuteNonQuery(cmd);
        return (result != -1);
    }
    public DataTable SelectPartyNamebyPartyTypeId(Int32 PartyTypeId,String Whid)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectPartyNamebyPartyTypeId";
        cmd.Parameters.Add(new SqlParameter("@PartyTypeId", SqlDbType.Int));
        cmd.Parameters["@PartyTypeId"].Value = PartyTypeId;
        cmd.Parameters.Add(new SqlParameter("@Whid", SqlDbType.NVarChar));
        cmd.Parameters["@Whid"].Value = Whid;
        
        dt = DatabaseCls.FillAdapter(cmd);
        return dt;
    }
    public DataTable SelectPartyTypeMasterwithoutEmployee()
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectPartyTypeMasterwithoutEmployee";
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        dt = DatabaseCls.FillAdapter(cmd);
        return dt;
    }
    public DataTable SelectDesignationMasterbyEmpId(Int32 EmployeeId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectDesignationMasterbyEmpId";
        cmd.Parameters.Add(new SqlParameter("@EmployeeID", SqlDbType.Int));
        cmd.Parameters["@EmployeeID"].Value = EmployeeId;
           dt = DatabaseCls.FillAdapter(cmd);
        return dt;
    }
    public DataTable SelectEmployeeMasterwithDataDept(String Whid)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectEmployeeMasterwithDataDept";
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString();
        cmd.Parameters.Add(new SqlParameter("@Whid", SqlDbType.NVarChar));
        cmd.Parameters["@Whid"].Value = Whid; 
        dt = DatabaseCls.FillAdapter(cmd);
        return dt;
    }
    public DataTable SelectDesignationMasterwithDeptName(String Whid)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectDesignationMasterwithDeptName";
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString();
        cmd.Parameters.Add(new SqlParameter("@Whid", SqlDbType.NVarChar));
        cmd.Parameters["@Whid"].Value = Whid;
        //cmd.Parameters.Add(new SqlParameter("@DepartmentId", SqlDbType.Int));
        //cmd.Parameters["@DepartmentId"].Value = DepartmentId;

        dt = DatabaseCls.FillAdapter(cmd);
        return dt;
    }
    public DataTable selectEmployeewithDesignation(Int32 DesignationId,String Whid)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "selectEmployeewithDesignation";
        cmd.Parameters.Add(new SqlParameter("@DesignationId", SqlDbType.Int));
        cmd.Parameters["@DesignationId"].Value = DesignationId;
        cmd.Parameters.Add(new SqlParameter("@Whid", SqlDbType.NVarChar));
        cmd.Parameters["@Whid"].Value = Whid; // CompanyLoginId;
        dt = DatabaseCls.FillAdapter(cmd);
        return dt;
    }

    public DataTable SelectParty_MasterbyPartyTypeId(Int32 DesignationId, String Whid)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectParty_MasterbyPartyTypeId";
        cmd.Parameters.Add(new SqlParameter("@PartyTypeId", SqlDbType.Int));
        cmd.Parameters["@PartyTypeId"].Value = DesignationId;
        cmd.Parameters.Add(new SqlParameter("@Whid", SqlDbType.NVarChar));
        cmd.Parameters["@Whid"].Value = Whid; // CompanyLoginId;
        dt = DatabaseCls.FillAdapter(cmd);
        return dt;
    }
    public DataTable SelectDesignationMasterwithDataDept(String Whid)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectDesignationMasterwithDataDept";
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        cmd.Parameters.Add(new SqlParameter("@Whid", SqlDbType.NVarChar));
        cmd.Parameters["@Whid"].Value = Whid; // CompanyLoginId;
        dt = DatabaseCls.FillAdapter(cmd);
        return dt;
    }
    public Int32 InsertAccountMasterParty(String Accountid, Int32 ClassId, Int32 GroupId, String AccountName, String AccountDate, String Whid)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "InsertAccountMasterParty";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@AccountId", SqlDbType.NVarChar));
        cmd.Parameters["@AccountId"].Value = Accountid;
        cmd.Parameters.Add(new SqlParameter("@ClassId", SqlDbType.Int));
        cmd.Parameters["@ClassId"].Value = ClassId;
        cmd.Parameters.Add(new SqlParameter("@GroupId", SqlDbType.Int));
        cmd.Parameters["@GroupId"].Value = GroupId;

        cmd.Parameters.Add(new SqlParameter("@AccountName", SqlDbType.NVarChar));
        cmd.Parameters["@AccountName"].Value = AccountName;

        cmd.Parameters.Add(new SqlParameter("@AccountDate", SqlDbType.DateTime));
        cmd.Parameters["@AccountDate"].Value = AccountDate;
        cmd.Parameters.Add(new SqlParameter("@Status", SqlDbType.Bit));
        cmd.Parameters["@Status"].Value = 1;

        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;

        cmd.Parameters.Add(new SqlParameter("@Whid", SqlDbType.NVarChar));
        cmd.Parameters["@Whid"].Value = Whid; // CompanyLoginId;
        cmd.Parameters.Add(new SqlParameter("@Id", SqlDbType.Int));
        cmd.Parameters["@Id"].Direction = ParameterDirection.Output;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls.ExecuteNonQuery(cmd);
        result = Convert.ToInt32(cmd.Parameters["@Id"].Value.ToString());
        return result;
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
        dt = DatabaseCls.FillAdapter(cmd);
        return dt;
    }
    public DataTable SelectPageMaster()
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectPageMaster";
        dt = DatabaseCls.FillAdapter(cmd);
        return dt;
    }
    
    public DataTable SelectCompanyEmailForEmp(Int32 EmployeeId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectCompanyEmailForEmp";
        cmd.Parameters.Add(new SqlParameter("@EmployeeId", SqlDbType.Int));
        cmd.Parameters["@EmployeeId"].Value = EmployeeId;
        dt = DatabaseCls.FillAdapter(cmd);
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
        dt = DatabaseCls.FillAdapter(cmd);
        return dt;
    }
    public DataTable SelectEmpEmaildetail(Int32 EmployeeId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectEmpEmaildetail";
        cmd.Parameters.Add(new SqlParameter("@EmployeeId", SqlDbType.Int));
        cmd.Parameters["@EmployeeId"].Value = EmployeeId;
        dt = DatabaseCls.FillAdapter(cmd);
        return dt;
    }
    public DataTable SelectEmpEmail(Int32 EmployeeId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectEmpEmail";
        cmd.Parameters.Add(new SqlParameter("@EmployeeId", SqlDbType.Int));
        cmd.Parameters["@EmployeeId"].Value = EmployeeId;
        dt = DatabaseCls.FillAdapter(cmd);
        return dt;
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
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls.ExecuteNonQuery(cmd);
        return result;
    }

    public bool UpdatePageMaster(Int32 PageId, Int32 PageTypeId, String PageName, String PageTitle, String PageDescription)
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
        //cmd.Parameters.Add(new SqlParameter("@DepartmentId", SqlDbType.Int));
        //cmd.Parameters["@DepartmentId"].Value = DepartmentId;
        //cmd.Parameters.Add(new SqlParameter("@DepartmentName", SqlDbType.NVarChar));
        //cmd.Parameters["@DepartmentName"].Value = DepartmentName;
        ////cmd.Parameters.Add(new SqlParameter("@DepartmentName", SqlDbType.NVarChar));
        ////cmd.Parameters["@DepartmentName"].Value = DepartmentName;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        int result = DatabaseCls.ExecuteNonQuery(cmd);
        return (result != -1);
    }
    public DataTable SelectPartyMasterAll(string id)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectPartyMasterAll";
        cmd.Parameters.Add(new SqlParameter("@id", SqlDbType.NVarChar));
        cmd.Parameters["@id"].Value = id;

        cmd.CommandType = CommandType.StoredProcedure;
        SqlCommand cmdd = new SqlCommand();
        dt = DatabaseCls.FillAdapter(cmd);
        return dt;
        
       
    }

    //public DataTable SelectPartyMaster(int id)
    //{
    //    cmd = new SqlCommand();
    //    dt = new DataTable();
    //    cmd.CommandText = "SelectPartyMasterAll";
    //    cmd.Parameters.Add("@id", id);
    //    dt = DatabaseCls.FillAdapter(cmd);
       
    //    return dt;
    //}
    public DataTable SelectPageMasterbyPageName(String PageName) //,ref Int32  EmployeeId, ref  Int32  DesignationId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectPageMasterbyPageName";
        
        cmd.Parameters.Add(new SqlParameter("@PageName", SqlDbType.NVarChar));
        cmd.Parameters["@PageName"].Value = PageName;

        cmd.Parameters.Add(new SqlParameter("@VersionInfoMasterId", SqlDbType.Int));
        cmd.Parameters["@VersionInfoMasterId"].Value = HttpContext.Current.Session["verId"].ToString();

        cmd.CommandType = CommandType.StoredProcedure;
        SqlCommand cmdd = new SqlCommand();
        dt = DatabaseCls.FillAdapter(cmd);
        return dt;
    }
    public DataTable SelectPageMasterbyPageNameForShoppingcart(String PageName) //,ref Int32  EmployeeId, ref  Int32  DesignationId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectPageMasterbyPageNameForShoppingcart";

        cmd.Parameters.Add(new SqlParameter("@PageName", SqlDbType.NVarChar));
        cmd.Parameters["@PageName"].Value = PageName;

       

        cmd.CommandType = CommandType.StoredProcedure;
        SqlCommand cmdd = new SqlCommand();
        dt = DatabaseCls.FillAdapter(cmd);
        return dt;
    }

}
