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
/// Summary description for InstructionCls
/// </summary>
public class InstructionCls
{
    SqlCommand cmd;

    DataTable dt;
    public InstructionCls()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    // Neetu 16-3-09 Instruction 
    public DataTable SelectInstructionTypeMaster()
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectInstructionTypeMaster";
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }

    public DataTable SelectRuleTypeMaster(String Whid)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectRuleTypeMaster";
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        cmd.Parameters.Add(new SqlParameter("@Whid", SqlDbType.NVarChar));
        cmd.Parameters["@Whid"].Value = Whid;
     
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;
    }

    public bool InsertInstructionMaster(Int32 InsTypeId, Int32 InsTypeDId, String InsTitle, Int32 EnteredBy, String InsDetail, String Note)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "InsertInstructionMaster";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@InsTypeId", SqlDbType.Int));
        cmd.Parameters["@InsTypeId"].Value = InsTypeId;
        cmd.Parameters.Add(new SqlParameter("@InsTypeDId", SqlDbType.Int));
        cmd.Parameters["@InsTypeDId"].Value = InsTypeDId;
        cmd.Parameters.Add(new SqlParameter("@InsTitle", SqlDbType.NVarChar));
        cmd.Parameters["@InsTitle"].Value = InsTitle;
        // cmd.Parameters.Add(new SqlParameter("@InsMasterId", SqlDbType.Int));
        //cmd.Parameters["@InsMasterId"].Direction = ParameterDirection.Output  ;
        cmd.Parameters.Add(new SqlParameter("@EnteredBy", SqlDbType.Int));
        cmd.Parameters["@EnteredBy"].Value = EnteredBy;

        cmd.Parameters.Add(new SqlParameter("@InsDetail", SqlDbType.NVarChar));
        cmd.Parameters["@InsDetail"].Value = @InsDetail;

        cmd.Parameters.Add(new SqlParameter("@Note", SqlDbType.NVarChar));
        cmd.Parameters["@Note"].Value = @Note;

        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls1.ExecuteNonQuery(cmd);
        result = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value.ToString());

        return (result != -1);
    }
    public DataTable SelectInstructionMasterTypeWise(Int32 InsTypeId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectInstructionMasterTypeWise";
        cmd.Parameters.Add(new SqlParameter("@InsTypeId", SqlDbType.Int));
        cmd.Parameters["@InsTypeId"].Value = InsTypeId;

        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }
    public DataTable SelectInstructionMasterIdWise(Int32 InsMasterId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectInstructionMasterIdWise";
        cmd.Parameters.Add(new SqlParameter("@InsMasterId", SqlDbType.Int));
        cmd.Parameters["@InsMasterId"].Value = @InsMasterId;

        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }





    public bool InsertInstructionTypeMaster(String InstructionType)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "InsertInstructionTypeMaster";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@InstructionType", SqlDbType.NVarChar));
        cmd.Parameters["@InstructionType"].Value = @InstructionType;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls1.ExecuteNonQuery(cmd);
        result = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value.ToString());
        return (result != -1);
    }
    public bool UpdateInstructionTypeMaster(Int32 InsTypeId, String InstructionType)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "UpdateInstructionTypeMaster";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@InsTypeId", SqlDbType.Int));
        cmd.Parameters["@InsTypeId"].Value = InsTypeId;
        cmd.Parameters.Add(new SqlParameter("@InstructionType", SqlDbType.NVarChar));
        cmd.Parameters["@InstructionType"].Value = InstructionType;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls1.ExecuteNonQuery(cmd);
        result = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value.ToString());
        return (result != -1);
    }
    public bool UpdateRuleMasterByRuleType(Int32 RuleId, Int32 RuleTypeId)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "UpdateRuleMasterByRuleType";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@RuleId", SqlDbType.Int));
        cmd.Parameters["@RuleId"].Value = RuleId;
        cmd.Parameters.Add(new SqlParameter("@RuleTypeId", SqlDbType.Int));
        cmd.Parameters["@RuleTypeId"].Value = RuleTypeId;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls1.ExecuteNonQueryep(cmd);
        result = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value.ToString());
        return (result != -1);
    }
    public bool UpdateRuleMaster(Int32 RuleId, Int32 RuleTypeId, Int32 DocumentTypeId, DateTime RuleDate, String RuleTitle, Int32 ConditionTypeId, String MainId, String Subid, String Whid, Boolean Approvemail)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "UpdateRuleMaster";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@RuleId", SqlDbType.Int));
        cmd.Parameters["@RuleId"].Value = RuleId;
        cmd.Parameters.Add(new SqlParameter("@RuleTypeId", SqlDbType.Int));
        cmd.Parameters["@RuleTypeId"].Value = RuleTypeId;
        cmd.Parameters.Add(new SqlParameter("@DocumentTypeId", SqlDbType.Int));
        cmd.Parameters["@DocumentTypeId"].Value = DocumentTypeId;
        cmd.Parameters.Add(new SqlParameter("@RuleDate", SqlDbType.DateTime));
        cmd.Parameters["@RuleDate"].Value = RuleDate;
        cmd.Parameters.Add(new SqlParameter("@RuleTitle", SqlDbType.NVarChar));
        cmd.Parameters["@RuleTitle"].Value = RuleTitle;
        cmd.Parameters.Add(new SqlParameter("@ConditionTypeId", SqlDbType.Int));
        cmd.Parameters["@ConditionTypeId"].Value = ConditionTypeId;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;

        cmd.Parameters.Add(new SqlParameter("@Whid", SqlDbType.NVarChar));
        cmd.Parameters["@Whid"].Value = Whid;
        cmd.Parameters.Add(new SqlParameter("@DocumentMainId", SqlDbType.NVarChar));
        cmd.Parameters["@DocumentMainId"].Value = MainId;
        cmd.Parameters.Add(new SqlParameter("@DocumentSubId", SqlDbType.NVarChar));
        cmd.Parameters["@DocumentSubId"].Value = Subid;

        cmd.Parameters.Add(new SqlParameter("@Approvemail", SqlDbType.NVarChar));
        cmd.Parameters["@Approvemail"].Value = Approvemail;

        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls1.ExecuteNonQueryep(cmd);
        result = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value.ToString());
        return (result != -1);
    }
    public bool UpdateRuleMasterforParty(Int32 RuleId, Int32 RuleTypeId, Int32 DocumentTypeId, DateTime RuleDate, String RuleTitle, Int32 ConditionTypeId, String MainId, String Subid, String Whid)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "UpdateRuleMasterforParty";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@RuleId", SqlDbType.Int));
        cmd.Parameters["@RuleId"].Value = RuleId;
        cmd.Parameters.Add(new SqlParameter("@RuleTypeId", SqlDbType.Int));
        cmd.Parameters["@RuleTypeId"].Value = RuleTypeId;
        cmd.Parameters.Add(new SqlParameter("@DocumentTypeId", SqlDbType.Int));
        cmd.Parameters["@DocumentTypeId"].Value = DocumentTypeId;
        cmd.Parameters.Add(new SqlParameter("@RuleDate", SqlDbType.DateTime));
        cmd.Parameters["@RuleDate"].Value = RuleDate;
        cmd.Parameters.Add(new SqlParameter("@RuleTitle", SqlDbType.NVarChar));
        cmd.Parameters["@RuleTitle"].Value = RuleTitle;
        cmd.Parameters.Add(new SqlParameter("@ConditionTypeId", SqlDbType.Int));
        cmd.Parameters["@ConditionTypeId"].Value = ConditionTypeId;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;

        cmd.Parameters.Add(new SqlParameter("@Whid", SqlDbType.NVarChar));
        cmd.Parameters["@Whid"].Value = Whid;
        cmd.Parameters.Add(new SqlParameter("@DocumentMainId", SqlDbType.NVarChar));
        cmd.Parameters["@DocumentMainId"].Value = MainId;
        cmd.Parameters.Add(new SqlParameter("@DocumentSubId", SqlDbType.NVarChar));
        cmd.Parameters["@DocumentSubId"].Value = Subid;


        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls1.ExecuteNonQueryep(cmd);
        result = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value.ToString());
        return (result != -1);
    }
    public bool InsertInstuctionMasterwithText(String CmdText)
    {
        cmd = new SqlCommand();
        cmd.CommandText = CmdText;
        cmd.CommandType = CommandType.Text;
        Int32 result = DatabaseCls1.ExecuteNonQuery(cmd);
        return (result != -1);
    }
    public DataTable SelectInstuctionMasterwithText(String CmdText)
    {
        cmd = new SqlCommand();
        cmd.CommandText = CmdText;
        cmd.CommandType = CommandType.Text;
        dt = new DataTable();
        dt = DatabaseCls1.FillAdapterwithText(cmd);
        return dt;
    }

    public DataTable SelectRuleApproveTypeMaster(String Whid)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "SelectRuleApproveTypeMaster";

        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        cmd.Parameters.Add(new SqlParameter("@Whid", SqlDbType.NVarChar));
        cmd.Parameters["@Whid"].Value = Whid;
        dt = new DataTable();
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;
    }

    public Int32 InsertRuleMaster(Int32 RuleTypeId, Int32 DocumentTypeId, DateTime RuleDate, String RuleTitle, Int32 ConditionTypeId, String cabinet, String Drower, String Whid, Boolean Approvemail, Int32 Active)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "InsertRuleMaster";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@RuleTypeId", SqlDbType.Int));
        cmd.Parameters["@RuleTypeId"].Value = RuleTypeId;
        cmd.Parameters.Add(new SqlParameter("@DocumentTypeId", SqlDbType.Int));
        cmd.Parameters["@DocumentTypeId"].Value = DocumentTypeId;
        cmd.Parameters.Add(new SqlParameter("@RuleTitle", SqlDbType.NVarChar));
        cmd.Parameters["@RuleTitle"].Value = RuleTitle;
        cmd.Parameters.Add(new SqlParameter("@RuleDate", SqlDbType.DateTime));
        cmd.Parameters["@RuleDate"].Value = RuleDate;
        cmd.Parameters.Add(new SqlParameter("@ConditionTypeId", SqlDbType.Int));
        cmd.Parameters["@ConditionTypeId"].Value = ConditionTypeId;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;


        cmd.Parameters.Add(new SqlParameter("@DocumentMainId", SqlDbType.NVarChar));
        cmd.Parameters["@DocumentMainId"].Value = cabinet;
        cmd.Parameters.Add(new SqlParameter("@DocumentSubId", SqlDbType.NVarChar));
        cmd.Parameters["@DocumentSubId"].Value = Drower;
        cmd.Parameters.Add(new SqlParameter("@Whid", SqlDbType.NVarChar));
        cmd.Parameters["@Whid"].Value = Whid;

        cmd.Parameters.Add(new SqlParameter("@Approvemail", SqlDbType.NVarChar));
        cmd.Parameters["@Approvemail"].Value = Approvemail;

        cmd.Parameters.Add(new SqlParameter("@Active", SqlDbType.NVarChar));
        cmd.Parameters["@Active"].Value = Active;

        cmd.Parameters.Add(new SqlParameter("@RuleId", SqlDbType.Int));
        cmd.Parameters["@RuleId"].Direction = ParameterDirection.Output;

        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls1.ExecuteNonQueryep(cmd);
        result = Convert.ToInt32(cmd.Parameters["@RuleId"].Value.ToString());
        return result;
    }

    public Int32 InsertRuleMasterforParty(Int32 RuleTypeId, Int32 DocumentTypeId, DateTime RuleDate, String RuleTitle, Int32 ConditionTypeId, String cabinet, String Drower, String Whid)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "InsertRuleMasterforParty";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@RuleTypeId", SqlDbType.Int));
        cmd.Parameters["@RuleTypeId"].Value = RuleTypeId;
        cmd.Parameters.Add(new SqlParameter("@DocumentTypeId", SqlDbType.Int));
        cmd.Parameters["@DocumentTypeId"].Value = DocumentTypeId;
        cmd.Parameters.Add(new SqlParameter("@RuleTitle", SqlDbType.NVarChar));
        cmd.Parameters["@RuleTitle"].Value = RuleTitle;
        cmd.Parameters.Add(new SqlParameter("@RuleDate", SqlDbType.DateTime));
        cmd.Parameters["@RuleDate"].Value = RuleDate;
        cmd.Parameters.Add(new SqlParameter("@ConditionTypeId", SqlDbType.Int));
        cmd.Parameters["@ConditionTypeId"].Value = ConditionTypeId;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;



        cmd.Parameters.Add(new SqlParameter("@DocumentMainId", SqlDbType.NVarChar));
        cmd.Parameters["@DocumentMainId"].Value = cabinet;
        cmd.Parameters.Add(new SqlParameter("@DocumentSubId", SqlDbType.NVarChar));
        cmd.Parameters["@DocumentSubId"].Value = Drower;
        cmd.Parameters.Add(new SqlParameter("@Whid", SqlDbType.NVarChar));
        cmd.Parameters["@Whid"].Value = Whid;


        cmd.Parameters.Add(new SqlParameter("@RuleId", SqlDbType.Int));
        cmd.Parameters["@RuleId"].Direction = ParameterDirection.Output;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls1.ExecuteNonQueryep(cmd);
        result = Convert.ToInt32(cmd.Parameters["@RuleId"].Value.ToString());
        return result;
    }

    public bool InsertRuleDetail(Int32 RuleId, Int32 EmployeeId, Int32 RuleApproveTypeId, Int32 StepId, String Days)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "InsertRuleDetail";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@RuleId", SqlDbType.Int));
        cmd.Parameters["@RuleId"].Value = @RuleId;
        cmd.Parameters.Add(new SqlParameter("@EmployeeId", SqlDbType.Int));
        cmd.Parameters["@EmployeeId"].Value = @EmployeeId;
        cmd.Parameters.Add(new SqlParameter("@RuleApproveTypeId", SqlDbType.Int));
        cmd.Parameters["@RuleApproveTypeId"].Value = @RuleApproveTypeId;
        cmd.Parameters.Add(new SqlParameter("@StepId", SqlDbType.Int));
        cmd.Parameters["@StepId"].Value = StepId;

        cmd.Parameters.Add(new SqlParameter("@Days", SqlDbType.NVarChar));
        cmd.Parameters["@Days"].Value = Days;

        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls1.ExecuteNonQueryep(cmd);
        result = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value.ToString());
        return (result != -1);
    }
    public bool InsertRuleDetailforParty(Int32 RuleId, Int32 PartyId, Int32 RuleApproveTypeId, Int32 StepId, String Days)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "InsertRuleDetailforParty";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@RuleId", SqlDbType.Int));
        cmd.Parameters["@RuleId"].Value = @RuleId;
        cmd.Parameters.Add(new SqlParameter("@PartyId", SqlDbType.Int));
        cmd.Parameters["@PartyId"].Value = PartyId;
        cmd.Parameters.Add(new SqlParameter("@RuleApproveTypeId", SqlDbType.Int));
        cmd.Parameters["@RuleApproveTypeId"].Value = @RuleApproveTypeId;
        cmd.Parameters.Add(new SqlParameter("@StepId", SqlDbType.Int));
        cmd.Parameters["@StepId"].Value = StepId;

        cmd.Parameters.Add(new SqlParameter("@Days", SqlDbType.NVarChar));
        cmd.Parameters["@Days"].Value = Days;

        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls1.ExecuteNonQueryep(cmd);
        result = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value.ToString());
        return (result != -1);
    }
    public bool DeleteRuleMaster(Int32 RuleId)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "DeleteRuleMaster";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@RuleId", SqlDbType.Int));
        cmd.Parameters["@RuleId"].Value = RuleId;

        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls1.ExecuteNonQueryep(cmd);
        result = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value.ToString());
        return (result != -1);
    }
    public bool DeleteRuleMasterforParty(Int32 RuleId)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "DeleteRuleMasterforParty";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@RuleId", SqlDbType.Int));
        cmd.Parameters["@RuleId"].Value = RuleId;

        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls1.ExecuteNonQueryep(cmd);
        result = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value.ToString());
        return (result != -1);
    }
    public bool DeleteRuleTypeMaster(Int32 RuleTypeId)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "DeleteRuleTypeMaster";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@RuleTypeId", SqlDbType.Int));
        cmd.Parameters["@RuleTypeId"].Value = RuleTypeId;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls1.ExecuteNonQueryep(cmd);
        result = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value.ToString());
        return (result != -1);
    }
    public bool DeleteRuleDetail(Int32 RuleId)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "DeleteRuleDetail";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@RuleId", SqlDbType.Int));
        cmd.Parameters["@RuleId"].Value = RuleId;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls1.ExecuteNonQuery(cmd);
        result = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value.ToString());
        return (result != -1);
    }

    public DataTable SelectRuleMaster(String Whid)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "SelectRuleMaster";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        cmd.Parameters.Add(new SqlParameter("@Whid", SqlDbType.NVarChar));
        cmd.Parameters["@Whid"].Value = Whid; // CompanyLoginId;
       
        
        dt = new DataTable();
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;
    }
    public DataTable SelectRuleMasterforParty(String Whid)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "SelectRuleMasterforParty";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        cmd.Parameters.Add(new SqlParameter("@Whid", SqlDbType.NVarChar));
        cmd.Parameters["@Whid"].Value = Whid;
      
        dt = new DataTable();
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;
    }
    public DataTable SelectRuleMasterDocTypeWiseRuleTypeWise(Int32 DocTypeId, Int32 RuleTypeId)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "SelectRuleMasterDocTypeWiseRuleTypeWise";
        cmd.Parameters.Add(new SqlParameter("@DocTypeId", SqlDbType.Int));
        cmd.Parameters["@DocTypeId"].Value = DocTypeId;
        cmd.Parameters.Add(new SqlParameter("@RuleTypeId", SqlDbType.Int));
        cmd.Parameters["@RuleTypeId"].Value = RuleTypeId;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        cmd.CommandType = CommandType.StoredProcedure;
        dt = new DataTable();
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;
    }
    public DataTable SelectRuleMasterDocTypeWise(int DocTypeId)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "SelectRuleMasterDocTypeWise";
        cmd.Parameters.Add(new SqlParameter("@DocTypeId", SqlDbType.Int));
        cmd.Parameters["@DocTypeId"].Value = DocTypeId;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        cmd.CommandType = CommandType.StoredProcedure;
        dt = new DataTable();
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;
    }
    public DataTable SelectRuleMasterRuleTypeWise(Int32 RuleTypeId,String Whid)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "SelectRuleMasterRuleTypeWise";
        cmd.Parameters.Add(new SqlParameter("@RuleTypeId", SqlDbType.Int));
        cmd.Parameters["@RuleTypeId"].Value = RuleTypeId;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        cmd.Parameters.Add(new SqlParameter("@Whid", SqlDbType.NVarChar));
        cmd.Parameters["@Whid"].Value = Whid; // CompanyLoginId;
      
        cmd.CommandType = CommandType.StoredProcedure;
        dt = new DataTable();
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;
    }
    public DataTable SelectRuleTypeAll(String Whid)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "SelectRuleTypeAll";        
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        cmd.Parameters.Add(new SqlParameter("@Whid", SqlDbType.NVarChar));
        cmd.Parameters["@Whid"].Value = Whid; // CompanyLoginId;
     
        
        cmd.CommandType = CommandType.StoredProcedure;
        dt = new DataTable();
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;
    }
    public DataTable SelectRuleTypeAllforParty( String Whid)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "SelectRuleTypeAllforParty";
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        cmd.Parameters.Add(new SqlParameter("@Whid", SqlDbType.NVarChar));
        cmd.Parameters["@Whid"].Value = Whid;
        cmd.CommandType = CommandType.StoredProcedure;
        dt = new DataTable();
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;
    }
    public DataTable SelectRuleDetail(Int32 RuleId)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "SelectRuleDetail";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@RuleId", SqlDbType.Int));
        cmd.Parameters["@RuleId"].Value = @RuleId;
        dt = new DataTable();
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;
    }
    public DataTable SelectRuleDetailParty(Int32 RuleId)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "SelectRuleDetailParty";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@RuleId", SqlDbType.Int));
        cmd.Parameters["@RuleId"].Value = @RuleId;
        dt = new DataTable();
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;
    }
    public DataTable SelectRuleDetailforPartySub(int PartyId, String Apptypeid, String Whid)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "SelectRuleDetailforPartySub";
        cmd.Parameters.Add(new SqlParameter("@EmployeeId", SqlDbType.Int));
        cmd.Parameters["@EmployeeId"].Value = PartyId;
        cmd.Parameters.Add(new SqlParameter("@Whid", SqlDbType.Int));
        cmd.Parameters["@Whid"].Value = Whid;

        cmd.Parameters.Add(new SqlParameter("@RuleApproveTypeId", SqlDbType.Int));
        cmd.Parameters["@RuleApproveTypeId"].Value = Apptypeid;

        cmd.CommandType = CommandType.StoredProcedure;
        dt = new DataTable();
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;
    }
    public DataTable SelectRuleDetailforEmployeeSub(int EmployeeId, String Apptypeid, String Whid)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "SelectRuleDetailforEmployeeSub";
        cmd.Parameters.Add(new SqlParameter("@EmployeeId", SqlDbType.Int));
        cmd.Parameters["@EmployeeId"].Value = EmployeeId;
        cmd.Parameters.Add(new SqlParameter("@Whid", SqlDbType.Int));
        cmd.Parameters["@Whid"].Value = Whid;

        cmd.Parameters.Add(new SqlParameter("@RuleApproveTypeId", SqlDbType.Int));
        cmd.Parameters["@RuleApproveTypeId"].Value = Apptypeid;

        cmd.CommandType = CommandType.StoredProcedure;
        dt = new DataTable();
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;
    }
    public DataTable SelectRuleDetailforPartyMain(int PartyId, String Apptypeid, String Whid)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "SelectRuleDetailforPartyMain";
        cmd.Parameters.Add(new SqlParameter("@EmployeeId", SqlDbType.Int));
        cmd.Parameters["@EmployeeId"].Value = PartyId;
        cmd.Parameters.Add(new SqlParameter("@Whid", SqlDbType.Int));
        cmd.Parameters["@Whid"].Value = Whid;

        cmd.Parameters.Add(new SqlParameter("@RuleApproveTypeId", SqlDbType.Int));
        cmd.Parameters["@RuleApproveTypeId"].Value = Apptypeid;

        cmd.CommandType = CommandType.StoredProcedure;
        dt = new DataTable();
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;
    }
    public DataTable SelectRuleDetailforEmployeeMain(int EmployeeId, String Apptypeid, String Whid)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "SelectRuleDetailforEmployeeMain";
        cmd.Parameters.Add(new SqlParameter("@EmployeeId", SqlDbType.Int));
        cmd.Parameters["@EmployeeId"].Value = EmployeeId;
        cmd.Parameters.Add(new SqlParameter("@Whid", SqlDbType.Int));
        cmd.Parameters["@Whid"].Value = Whid;

        cmd.Parameters.Add(new SqlParameter("@RuleApproveTypeId", SqlDbType.Int));
        cmd.Parameters["@RuleApproveTypeId"].Value = Apptypeid;

        cmd.CommandType = CommandType.StoredProcedure;
        dt = new DataTable();
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;
    }
    public DataTable SelectRuleDetailforParty(int PartyId, String Apptypeid, String Whid)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "SelectRuleDetailforParty";
        cmd.Parameters.Add(new SqlParameter("@EmployeeId", SqlDbType.Int));
        cmd.Parameters["@EmployeeId"].Value = PartyId;
        cmd.Parameters.Add(new SqlParameter("@Whid", SqlDbType.Int));
        cmd.Parameters["@Whid"].Value = Whid;

        cmd.Parameters.Add(new SqlParameter("@RuleApproveTypeId", SqlDbType.Int));
        cmd.Parameters["@RuleApproveTypeId"].Value = Apptypeid;

        cmd.CommandType = CommandType.StoredProcedure;
        dt = new DataTable();
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;
    }
    public DataTable SelectRuleDetailforEmployee(int EmployeeId,String Apptypeid,String Whid)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "SelectRuleDetailforEmployee";
        cmd.Parameters.Add(new SqlParameter("@EmployeeId", SqlDbType.Int));
        cmd.Parameters["@EmployeeId"].Value = EmployeeId;
        cmd.Parameters.Add(new SqlParameter("@Whid", SqlDbType.Int));
        cmd.Parameters["@Whid"].Value = Whid;

        cmd.Parameters.Add(new SqlParameter("@RuleApproveTypeId", SqlDbType.Int));
        cmd.Parameters["@RuleApproveTypeId"].Value = Apptypeid;
      
        cmd.CommandType = CommandType.StoredProcedure;
        dt = new DataTable();
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;
    }
    public DataTable SelectRuleDetailforParty(int PartyId)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "SelectRuleDetailforParty";
        cmd.Parameters.Add(new SqlParameter("@PartyId", SqlDbType.Int));
        cmd.Parameters["@PartyId"].Value = PartyId;
        cmd.CommandType = CommandType.StoredProcedure;
        dt = new DataTable();
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }

    public bool InsertRuleEmpSelectionMaster(Int32 RuleId, Int32 StepId, Int32 @EmpSelectionId)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "InsertRuleEmpSelectionMaster";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@RuleId", SqlDbType.Int));
        cmd.Parameters["@RuleId"].Value = @RuleId;
        cmd.Parameters.Add(new SqlParameter("@StepId", SqlDbType.Int));
        cmd.Parameters["@StepId"].Value = StepId;
        cmd.Parameters.Add(new SqlParameter("@EmpSelectionId", SqlDbType.Int));
        cmd.Parameters["@EmpSelectionId"].Value = @EmpSelectionId;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls1.ExecuteNonQueryep(cmd);
        result = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value.ToString());
        return (result != -1);
    }
    public bool InsertRulePartySelectionMaster(Int32 RuleId, Int32 StepId, Int32 PartySelectionId)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "InsertRulePartySelectionMaster";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@RuleId", SqlDbType.Int));
        cmd.Parameters["@RuleId"].Value = @RuleId;
        cmd.Parameters.Add(new SqlParameter("@StepId", SqlDbType.Int));
        cmd.Parameters["@StepId"].Value = StepId;
        cmd.Parameters.Add(new SqlParameter("@PartySelectionId", SqlDbType.Int));
        cmd.Parameters["@PartySelectionId"].Value = PartySelectionId;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls1.ExecuteNonQueryep(cmd);
        result = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value.ToString());
        return (result != -1);
    }


    public DataTable SelectRuleEmpSelectionMaster(Int32 RuleId, Int32 StepId)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "SelectRuleEmpSelectionMaster";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@RuleId", SqlDbType.Int));
        cmd.Parameters["@RuleId"].Value = RuleId;
        cmd.Parameters.Add(new SqlParameter("@StepId", SqlDbType.Int));
        cmd.Parameters["@StepId"].Value = StepId;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        //cmd.Parameters.Add(new SqlParameter("@EmpSelectionId", SqlDbType.Int));
        //cmd.Parameters["@EmpSelectionId"].Value = @EmpSelectionId;             
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;

        dt = new DataTable();
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;
    }
    public DataTable SelectRulePartySelectionMaster(Int32 RuleId, Int32 StepId)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "SelectRulePartySelectionMaster";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@RuleId", SqlDbType.Int));
        cmd.Parameters["@RuleId"].Value = RuleId;
        cmd.Parameters.Add(new SqlParameter("@StepId", SqlDbType.Int));
        cmd.Parameters["@StepId"].Value = StepId;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        //cmd.Parameters.Add(new SqlParameter("@EmpSelectionId", SqlDbType.Int));
        //cmd.Parameters["@EmpSelectionId"].Value = @EmpSelectionId;             
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;

        dt = new DataTable();
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;
    }
    public DataTable SelectRuleProcessMasterDocIdWiseRuleIdWise(Int32 DocumentId, Int32 RuleId)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "SelectRuleProcessMasterDocIdWiseRuleIdWise";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@DocumentId", SqlDbType.Int));
        cmd.Parameters["@DocumentId"].Value = DocumentId;
        cmd.Parameters.Add(new SqlParameter("@RuleId", SqlDbType.Int));
        cmd.Parameters["@RuleId"].Value = RuleId;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;

        dt = new DataTable();
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;
    }
    public DataTable SelectRuleProcessMasterDocIdWiseRuleIdWiseforParty(Int32 DocumentId, Int32 RuleId)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "SelectRuleProcessMasterDocIdWiseRuleIdWiseforParty";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@DocumentId", SqlDbType.Int));
        cmd.Parameters["@DocumentId"].Value = DocumentId;
        cmd.Parameters.Add(new SqlParameter("@RuleId", SqlDbType.Int));
        cmd.Parameters["@RuleId"].Value = RuleId;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;

        dt = new DataTable();
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }

    public DataTable SelectRuleProcesstocheckAllCond(Int32 DocumentId, Int32 RuleId, Int32 StepId)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "SelectRuleProcesstocheckAllCond";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@DocumentId", SqlDbType.Int));
        cmd.Parameters["@DocumentId"].Value = @DocumentId;
        cmd.Parameters.Add(new SqlParameter("@RuleId", SqlDbType.Int));
        cmd.Parameters["@RuleId"].Value = RuleId;
        cmd.Parameters.Add(new SqlParameter("@StepId", SqlDbType.Int));
        cmd.Parameters["@StepId"].Value = StepId;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        dt = new DataTable();
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;
    }

    public DataTable SelectRuleProcesstocheckAllCondforParty(Int32 DocumentId, Int32 RuleId, Int32 StepId)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "SelectRuleProcesstocheckAllCondforParty";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@DocumentId", SqlDbType.Int));
        cmd.Parameters["@DocumentId"].Value = @DocumentId;
        cmd.Parameters.Add(new SqlParameter("@RuleId", SqlDbType.Int));
        cmd.Parameters["@RuleId"].Value = RuleId;
        cmd.Parameters.Add(new SqlParameter("@StepId", SqlDbType.Int));
        cmd.Parameters["@StepId"].Value = StepId;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        dt = new DataTable();
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }
    //13-7-09

    public DataTable SelectRuleDetailEmployeeName(Int32 RuleId)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "SelectRuleDetail";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@RuleId", SqlDbType.Int));
        cmd.Parameters["@RuleId"].Value = @RuleId;
        dt = new DataTable();
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }

    public DataTable SelectRuleDetailDocTypeIdWise(int DocTypeId)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "SelectRuleDetailDocTypeIdWise";
        cmd.Parameters.Add(new SqlParameter("@DocTypeId", SqlDbType.Int));
        cmd.Parameters["@DocTypeId"].Value = DocTypeId;
        cmd.CommandType = CommandType.StoredProcedure;
        dt = new DataTable();
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }
    public DataTable SelectRuleDetailRuleTypeIdWise(int RuleTypeId)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "SelectRuleDetailRuleTypeIdWise";
        cmd.Parameters.Add(new SqlParameter("@RuleTypeId", SqlDbType.Int));
        cmd.Parameters["@RuleTypeId"].Value = RuleTypeId;
        cmd.CommandType = CommandType.StoredProcedure;
        dt = new DataTable();
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }
    public DataTable SelectRuleDetailAll(String Whid )
    {
        cmd = new SqlCommand();
        cmd.CommandText = "SelectRuleDetailAll";
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        cmd.Parameters.Add(new SqlParameter("@Whid", SqlDbType.NVarChar));
        cmd.Parameters["@Whid"].Value = Whid; // CompanyLoginId;
        
        
        cmd.CommandType = CommandType.StoredProcedure;
        dt = new DataTable();
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;
    }
    public DataTable SelectRuleDetailAllforParty(String Whid)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "SelectRuleDetailAllforParty";
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        cmd.Parameters.Add(new SqlParameter("@Whid", SqlDbType.NVarChar));
        cmd.Parameters["@Whid"].Value = Whid;
       
        cmd.CommandType = CommandType.StoredProcedure;
        dt = new DataTable();
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;
    }


    public DataTable SelectRuleDetailStep(int RuleId)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "SelectRuleDetailStep";
        cmd.Parameters.Add(new SqlParameter("@RuleId", SqlDbType.Int));
        cmd.Parameters["@RuleId"].Value = RuleId;
        //cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        //cmd.Parameters["@CID"].Value = HttpContext.Current.Session["CompanyLoginId"].ToString(); // CompanyLoginId;
        cmd.CommandType = CommandType.StoredProcedure;
        dt = new DataTable();
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;
    }
    public DataTable SelectRuleDetailStepforParty(int RuleId)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "SelectRuleDetailStepforParty";
        cmd.Parameters.Add(new SqlParameter("@RuleId", SqlDbType.Int));
        cmd.Parameters["@RuleId"].Value = RuleId;
        //cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        //cmd.Parameters["@CID"].Value = HttpContext.Current.Session["CompanyLoginId"].ToString(); // CompanyLoginId;
        cmd.CommandType = CommandType.StoredProcedure;
        dt = new DataTable();
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;
    }

    public DataTable SelectRuleDetailRuleIdwiseStepWise(int RuleId , int StepId,String Whid)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "SelectRuleDetailRuleIdwiseStepWise";
        cmd.Parameters.Add(new SqlParameter("@RuleId", SqlDbType.Int));
        cmd.Parameters["@RuleId"].Value = RuleId;
        cmd.Parameters.Add(new SqlParameter("@StepId", SqlDbType.Int));
        cmd.Parameters["@StepId"].Value = StepId;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        cmd.Parameters.Add(new SqlParameter("@Whid", SqlDbType.NVarChar));
        cmd.Parameters["@Whid"].Value = Whid;
       
        cmd.CommandType = CommandType.StoredProcedure;
        dt = new DataTable();
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;
    }
    public DataTable SelectRuleDetailRuleIdwiseStepWiseforParty(int RuleId, int StepId)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "SelectRuleDetailRuleIdwiseStepWiseforParty";
        cmd.Parameters.Add(new SqlParameter("@RuleId", SqlDbType.Int));
        cmd.Parameters["@RuleId"].Value = RuleId;
        cmd.Parameters.Add(new SqlParameter("@StepId", SqlDbType.Int));
        cmd.Parameters["@StepId"].Value = StepId;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        cmd.CommandType = CommandType.StoredProcedure;
        dt = new DataTable();
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;
    }
    public DataTable SelectRuleProcessMasterDocIdWiseRuleIdWiseRuleDetailIdwise(int DocumentId, int RuleDetailId )
    {
        cmd = new SqlCommand();
        cmd.CommandText = "SelectRuleProcessMasterDocIdWiseRuleIdWiseRuleDetailIdwise";
        cmd.Parameters.Add(new SqlParameter("@DocumentId", SqlDbType.Int));
        cmd.Parameters["@DocumentId"].Value = DocumentId;
        cmd.Parameters.Add(new SqlParameter("@RuleDetailId", SqlDbType.Int));
        cmd.Parameters["@RuleDetailId"].Value = RuleDetailId;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        cmd.CommandType = CommandType.StoredProcedure;
        dt = new DataTable();
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;
    }
    public DataTable SelectRuleProcessMasterDocIdWiseRuleIdWiseRuleDetailIdwiseforParty(int DocumentId, int RuleDetailId)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "SelectRuleProcessMasterDocIdWiseRuleIdWiseRuleDetailIdwiseforParty";
        cmd.Parameters.Add(new SqlParameter("@DocumentId", SqlDbType.Int));
        cmd.Parameters["@DocumentId"].Value = DocumentId;
        cmd.Parameters.Add(new SqlParameter("@RuleDetailId", SqlDbType.Int));
        cmd.Parameters["@RuleDetailId"].Value = RuleDetailId;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        cmd.CommandType = CommandType.StoredProcedure;
        dt = new DataTable();
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;
    }
    public DataTable SelectApproveNotefromRuleProcessMaster(int DocumentId, int EmployeeId)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "SelectApproveNotefromRuleProcessMaster";
        cmd.Parameters.Add(new SqlParameter("@DocumentId", SqlDbType.Int));
        cmd.Parameters["@DocumentId"].Value = DocumentId;
        cmd.Parameters.Add(new SqlParameter("@EmployeeId", SqlDbType.Int));
        cmd.Parameters["@EmployeeId"].Value = EmployeeId;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        cmd.CommandType = CommandType.StoredProcedure;
        dt = new DataTable();
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;
    }
    public DataTable SelectApproveNotefromRuleProcessMasterforParty(int DocumentId, int PartyId)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "SelectApproveNotefromRuleProcessMasterforParty";
        cmd.Parameters.Add(new SqlParameter("@DocumentId", SqlDbType.Int));
        cmd.Parameters["@DocumentId"].Value = DocumentId;
        cmd.Parameters.Add(new SqlParameter("@PartyId", SqlDbType.Int));
        cmd.Parameters["@PartyId"].Value = PartyId;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        cmd.CommandType = CommandType.StoredProcedure;
        dt = new DataTable();
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;
    }
    public DataTable SelectRuleDetailAllDocIdWise(int DocumentId,String Whid)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "SelectRuleDetailAllDocIdWise";
        cmd.Parameters.Add(new SqlParameter("@DocumentId", SqlDbType.Int));
        cmd.Parameters["@DocumentId"].Value = DocumentId;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        cmd.Parameters.Add(new SqlParameter("@Whid", SqlDbType.NVarChar));
        cmd.Parameters["@Whid"].Value = Whid; // CompanyLoginId;
  
        
        cmd.CommandType = CommandType.StoredProcedure;
        dt = new DataTable();
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;
    }
    public DataTable SelectRuleDetailAllDocIdWisevv(int DocumentId,String Whid)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "SelectRuleDetailAllDocIdWisevv";
        cmd.Parameters.Add(new SqlParameter("@DocumentId", SqlDbType.Int));
        cmd.Parameters["@DocumentId"].Value = DocumentId;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        cmd.Parameters.Add(new SqlParameter("@Whid", SqlDbType.NVarChar));
        cmd.Parameters["@Whid"].Value = Whid; // CompanyLoginId;
        cmd.CommandType = CommandType.StoredProcedure;
        dt = new DataTable();
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;
    }
    public DataTable SelectRuleDetailAllDocIdWiseforParty(int DocumentId,String Whid)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "SelectRuleDetailAllDocIdWiseforParty";
        cmd.Parameters.Add(new SqlParameter("@DocumentId", SqlDbType.Int));
        cmd.Parameters["@DocumentId"].Value = DocumentId;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        cmd.Parameters.Add(new SqlParameter("@Whid", SqlDbType.NVarChar));
        cmd.Parameters["@Whid"].Value = Whid; // CompanyLoginId;
        cmd.CommandType = CommandType.StoredProcedure;
        dt = new DataTable();
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;
    }



    public DataTable SelectRuleDetailEmployeewise(int EmployeeId,String Whid)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "SelectRuleDetailEmployeewise";
        cmd.Parameters.Add(new SqlParameter("@EmployeeId", SqlDbType.Int));
        cmd.Parameters["@EmployeeId"].Value = EmployeeId;
        cmd.Parameters.Add(new SqlParameter("@Whid", SqlDbType.Int));
        cmd.Parameters["@Whid"].Value = Whid;
        cmd.CommandType = CommandType.StoredProcedure;
        dt = new DataTable();
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;
    }
    public DataTable SelectRuleDetailAllDocTypeIdWise(int DocumentTypeId)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "SelectRuleDetailAllDocTypeIdWise";

        cmd.Parameters.Add(new SqlParameter("@DocumentTypeId", SqlDbType.Int));
        cmd.Parameters["@DocumentTypeId"].Value = DocumentTypeId;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        cmd.CommandType = CommandType.StoredProcedure;
        dt = new DataTable();
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;
    }

    public DataTable SelectRuleDetailAllDocTypeIdWiseforParty(int DocumentTypeId)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "SelectRuleDetailAllDocTypeIdWiseforParty";

        cmd.Parameters.Add(new SqlParameter("@DocumentTypeId", SqlDbType.Int));
        cmd.Parameters["@DocumentTypeId"].Value = DocumentTypeId;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        cmd.CommandType = CommandType.StoredProcedure;
        dt = new DataTable();
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;
    }
    public DataTable SelectRuleDetailAllDocTypeIdWisePartywiseforParty(int DocumentTypeId, Int32 PartyId)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "SelectRuleDetailAllDocTypeIdWisePartywiseforParty";

        cmd.Parameters.Add(new SqlParameter("@DocumentTypeId", SqlDbType.Int));
        cmd.Parameters["@DocumentTypeId"].Value = DocumentTypeId;
        cmd.Parameters.Add(new SqlParameter("@PartyId", SqlDbType.Int));
        cmd.Parameters["@PartyId"].Value = PartyId;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        cmd.CommandType = CommandType.StoredProcedure;
        dt = new DataTable();
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;
    }
    public DataTable SelectRuleDetailAllRuleTypeIdwise(int RuleTypeId)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "SelectRuleDetailAllRuleTypeIdwise";

        cmd.Parameters.Add(new SqlParameter("@RuleTypeId", SqlDbType.Int));
        cmd.Parameters["@RuleTypeId"].Value =RuleTypeId;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["CompanyLoginId"].ToString(); // CompanyLoginId;
        cmd.CommandType = CommandType.StoredProcedure;
        dt = new DataTable();
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }
    public DataTable SelectRuleDetailByRuleApproveTypeId(Int32 RuleApproveTypeId)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "SelectRuleDetailByRuleApproveTypeId";

        cmd.Parameters.Add(new SqlParameter("@RuleApproveTypeId", SqlDbType.Int));
        cmd.Parameters["@RuleApproveTypeId"].Value = RuleApproveTypeId;
        
        cmd.CommandType = CommandType.StoredProcedure;
        dt = new DataTable();
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;
    }

    public DataTable SelectRuleDetailAllRuleTypeIdwiseforParty(int RuleTypeId)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "SelectRuleDetailAllRuleTypeIdwiseforParty";

        cmd.Parameters.Add(new SqlParameter("@RuleTypeId", SqlDbType.Int));
        cmd.Parameters["@RuleTypeId"].Value = RuleTypeId;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        cmd.CommandType = CommandType.StoredProcedure;
        dt = new DataTable();
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;
    }

    public DataTable SelectRuleDetailAllRuleIdwise(int RuleId)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "SelectRuleDetailAllRuleIdwise";

        cmd.Parameters.Add(new SqlParameter("@RuleId", SqlDbType.Int));
        cmd.Parameters["@RuleId"].Value = RuleId;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        cmd.CommandType = CommandType.StoredProcedure;
        dt = new DataTable();
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;
    }

    public DataTable SelectRuleDetailAllRuleIdwiseforParty(int RuleId)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "SelectRuleDetailAllRuleIdwiseforParty";

        cmd.Parameters.Add(new SqlParameter("@RuleId", SqlDbType.Int));
        cmd.Parameters["@RuleId"].Value = RuleId;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        cmd.CommandType = CommandType.StoredProcedure;
        dt = new DataTable();
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;
    }
    public DataTable SelectRuleDetailAllRuleIdwiseDocTypeIdWise(int RuleId, int DocumentTypeId)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "SelectRuleDetailAllRuleIdwiseDocTypeIdWise";

        cmd.Parameters.Add(new SqlParameter("@RuleId", SqlDbType.Int));
        cmd.Parameters["@RuleId"].Value = RuleId;
        cmd.Parameters.Add(new SqlParameter("@DocumentTypeId", SqlDbType.Int));
        cmd.Parameters["@DocumentTypeId"].Value = @DocumentTypeId;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        cmd.CommandType = CommandType.StoredProcedure;
        dt = new DataTable();
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;
    }
    public DataTable SelectRuleDetailAllRuleIdwiseDocTypeIdWiseforParty(int RuleId, int DocumentTypeId)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "SelectRuleDetailAllRuleIdwiseDocTypeIdWiseforParty";

        cmd.Parameters.Add(new SqlParameter("@RuleId", SqlDbType.Int));
        cmd.Parameters["@RuleId"].Value = RuleId;
        cmd.Parameters.Add(new SqlParameter("@DocumentTypeId", SqlDbType.Int));
        cmd.Parameters["@DocumentTypeId"].Value = @DocumentTypeId;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        cmd.CommandType = CommandType.StoredProcedure;
        dt = new DataTable();
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;
    }
    public DataTable SelectRuleDetailAllRuleIdwiseDocTypeIdWiseStatusWiseforParty(int RuleId, int DocumentTypeId,int PartyId,Boolean Approve)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "SelectRuleDetailAllRuleIdwiseDocTypeIdWiseStatusWiseforParty";

        cmd.Parameters.Add(new SqlParameter("@RuleId", SqlDbType.Int));
        cmd.Parameters["@RuleId"].Value = RuleId;
        cmd.Parameters.Add(new SqlParameter("@DocumentTypeId", SqlDbType.Int));
        cmd.Parameters["@DocumentTypeId"].Value = DocumentTypeId;
        cmd.Parameters.Add(new SqlParameter("@PartyId", SqlDbType.Int));
        cmd.Parameters["@PartyId"].Value = PartyId;
        cmd.Parameters.Add(new SqlParameter("@Approve", SqlDbType.Bit));
        cmd.Parameters["@Approve"].Value = Approve;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        cmd.CommandType = CommandType.StoredProcedure;
        dt = new DataTable();
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;
    }
    public DataTable SelectRuleDetailAllRuleIdwiseDocTypeIdWisePartyWiseforParty(int RuleId, int DocumentTypeId, int PartyId)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "SelectRuleDetailAllRuleIdwiseDocTypeIdWisePartyWiseforParty";

        cmd.Parameters.Add(new SqlParameter("@RuleId", SqlDbType.Int));
        cmd.Parameters["@RuleId"].Value = RuleId;
        cmd.Parameters.Add(new SqlParameter("@DocumentTypeId", SqlDbType.Int));
        cmd.Parameters["@DocumentTypeId"].Value = DocumentTypeId;
        cmd.Parameters.Add(new SqlParameter("@PartyId", SqlDbType.Int));
        cmd.Parameters["@PartyId"].Value = PartyId;
        
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        cmd.CommandType = CommandType.StoredProcedure;
        dt = new DataTable();
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;
    }
    public DataTable SelectRuleDetailAllRuleTypeIdwiseDocTypeIdWise(int RuleTypeId, int DocumentTypeId)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "SelectRuleDetailAllRuleTypeIdwiseDocTypeIdWise";

        cmd.Parameters.Add(new SqlParameter("@RuleTypeId", SqlDbType.Int));
        cmd.Parameters["@RuleTypeId"].Value = RuleTypeId;
        cmd.Parameters.Add(new SqlParameter("@DocumentTypeId", SqlDbType.Int));
        cmd.Parameters["@DocumentTypeId"].Value = @DocumentTypeId;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["CompanyLoginId"].ToString(); // CompanyLoginId;
        cmd.CommandType = CommandType.StoredProcedure;
        dt = new DataTable();
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }
    public DataTable SelectRuleDetailAllRuleTypeIdwiseDocTypeIdWiseforParty(int RuleTypeId, int DocumentTypeId)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "SelectRuleDetailAllRuleTypeIdwiseDocTypeIdWiseforParty";

        cmd.Parameters.Add(new SqlParameter("@RuleTypeId", SqlDbType.Int));
        cmd.Parameters["@RuleTypeId"].Value = RuleTypeId;
        cmd.Parameters.Add(new SqlParameter("@DocumentTypeId", SqlDbType.Int));
        cmd.Parameters["@DocumentTypeId"].Value = @DocumentTypeId;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["CompanyLoginId"].ToString(); // CompanyLoginId;
        cmd.CommandType = CommandType.StoredProcedure;
        dt = new DataTable();
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }

    public DataTable SelectRuleDetailAllDocNameWise(string DocName,String Whid)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "SelectRuleDetailAllDocNameWise";

        cmd.Parameters.Add(new SqlParameter("@DocumentTitle", SqlDbType.NVarChar));
        cmd.Parameters["@DocumentTitle"].Value = DocName;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        cmd.Parameters.Add(new SqlParameter("@Whid", SqlDbType.NVarChar));
        cmd.Parameters["@Whid"].Value =Whid; // CompanyLoginId;
    
        cmd.CommandType = CommandType.StoredProcedure;
        dt = new DataTable();
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;
    }

    public DataTable SelectRuleDetailAllDocNameWiseforParty(string DocName,String Whid)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "SelectRuleDetailAllDocNameWiseforParty";

        cmd.Parameters.Add(new SqlParameter("@DocumentTitle", SqlDbType.NVarChar));
        cmd.Parameters["@DocumentTitle"].Value = DocName;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;

        cmd.Parameters.Add(new SqlParameter("@Whid", SqlDbType.NVarChar));
        cmd.Parameters["@Whid"].Value = Whid;
        cmd.CommandType = CommandType.StoredProcedure;
        dt = new DataTable();
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;
    }

    public DataTable SelectRuleDetailEmployeewiseRuleTypeIdwise(int EmployeeId , int RuleTypeId)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "SelectRuleDetailEmployeewiseRuleTypeIdwise";
        cmd.Parameters.Add(new SqlParameter("@EmployeeId", SqlDbType.Int));
        cmd.Parameters["@EmployeeId"].Value = EmployeeId;
        cmd.Parameters.Add(new SqlParameter("@RuleTypeId", SqlDbType.Int));
        cmd.Parameters["@RuleTypeId"].Value = RuleTypeId;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        cmd.CommandType = CommandType.StoredProcedure;
        dt = new DataTable();
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;
    }
    public DataTable SelectRuleDetailEmployeewiseDocumentTypeIdwise(int EmployeeId, int DocumentTypeId)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "SelectRuleDetailEmployeewiseDocumentTypeIdwise";
        cmd.Parameters.Add(new SqlParameter("@EmployeeId", SqlDbType.Int));
        cmd.Parameters["@EmployeeId"].Value = EmployeeId;
        cmd.Parameters.Add(new SqlParameter("@DocumentTypeId", SqlDbType.Int));
        cmd.Parameters["@DocumentTypeId"].Value = DocumentTypeId;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        cmd.CommandType = CommandType.StoredProcedure;
        dt = new DataTable();
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;
    }
    public DataTable SelectRuleDetailEmployeewiseDocumentTypeIdwiseRuleIdwise(int EmployeeId, int DocumentTypeId, int RuleId,String Whid)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "SelectRuleDetailEmployeewiseDocumentTypeIdwiseRuleIdwise";
        cmd.Parameters.Add(new SqlParameter("@EmployeeId", SqlDbType.Int));
        cmd.Parameters["@EmployeeId"].Value = EmployeeId;
        cmd.Parameters.Add(new SqlParameter("@DocumentTypeId", SqlDbType.Int));
        cmd.Parameters["@DocumentTypeId"].Value = DocumentTypeId;
        cmd.Parameters.Add(new SqlParameter("@RuleId", SqlDbType.Int));
        cmd.Parameters["@RuleId"].Value = RuleId;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        cmd.Parameters.Add(new SqlParameter("@Whid", SqlDbType.NVarChar));
        cmd.Parameters["@Whid"].Value = Whid;
       
        cmd.CommandType = CommandType.StoredProcedure;
        dt = new DataTable();
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;
    }
    public DataTable SelectRuleDetailEmployeewiseRuleIdwise(int EmployeeId, int RuleId)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "SelectRuleDetailEmployeewiseRuleIdwise";
        cmd.Parameters.Add(new SqlParameter("@EmployeeId", SqlDbType.Int));
        cmd.Parameters["@EmployeeId"].Value = EmployeeId;
        
        cmd.Parameters.Add(new SqlParameter("@RuleId", SqlDbType.Int));
        cmd.Parameters["@RuleId"].Value = RuleId;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        cmd.CommandType = CommandType.StoredProcedure;
        dt = new DataTable();
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;
    }
    public DataTable SelectRuleDetailEmployeewiseDocTYpeIdwiseRuleTypeIdwise(int EmployeeId, int DocumentTypeId, int RuleTypeId,String Whid)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "SelectRuleDetailEmployeewiseDocTYpeIdwiseRuleTypeIdwise";
        cmd.Parameters.Add(new SqlParameter("@EmployeeId", SqlDbType.Int));
        cmd.Parameters["@EmployeeId"].Value = EmployeeId;
        cmd.Parameters.Add(new SqlParameter("@DocumentTypeId", SqlDbType.Int));
        cmd.Parameters["@DocumentTypeId"].Value = DocumentTypeId;
        cmd.Parameters.Add(new SqlParameter("@RuleTypeId", SqlDbType.Int));
        cmd.Parameters["@RuleTypeId"].Value = RuleTypeId;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        cmd.Parameters.Add(new SqlParameter("@Whid", SqlDbType.NVarChar));
        cmd.Parameters["@Whid"].Value = Whid; // CompanyLoginId;
   
        cmd.CommandType = CommandType.StoredProcedure;
        dt = new DataTable();
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;
    }
    public DataTable SelectRuleDetailEmployeewiseDocTYpeIdwiseRuleTypeIdwiseDocIdwise(int EmployeeId, int DocumentTypeId, int RuleTypeId, int DocumentId,String Whid)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "SelectRuleDetailEmployeewiseDocTYpeIdwiseRuleTypeIdwiseDocIdwise";
        cmd.Parameters.Add(new SqlParameter("@EmployeeId", SqlDbType.Int));
        cmd.Parameters["@EmployeeId"].Value = EmployeeId;
        cmd.Parameters.Add(new SqlParameter("@DocumentTypeId", SqlDbType.Int));
        cmd.Parameters["@DocumentTypeId"].Value = DocumentTypeId;
        cmd.Parameters.Add(new SqlParameter("@RuleTypeId", SqlDbType.Int));
        cmd.Parameters["@RuleTypeId"].Value = RuleTypeId;
        cmd.Parameters.Add(new SqlParameter("@DocumentId", SqlDbType.Int));
        cmd.Parameters["@DocumentId"].Value = DocumentId;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        cmd.Parameters.Add(new SqlParameter("@Whid", SqlDbType.NVarChar));
        cmd.Parameters["@Whid"].Value = Whid;

        cmd.CommandType = CommandType.StoredProcedure;
        dt = new DataTable();
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;
    }
    public DataTable SelectRuleDetailEmployeewiseDocTYpeIdwiseRuleTypeIdwiseDocTitlewise(int EmployeeId, int DocumentTypeId, int RuleTypeId, string DocumentTitle,String Whid)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "SelectRuleDetailEmployeewiseDocTYpeIdwiseRuleTypeIdwiseDocTitlewise";
        cmd.Parameters.Add(new SqlParameter("@EmployeeId", SqlDbType.Int));
        cmd.Parameters["@EmployeeId"].Value = EmployeeId;
        cmd.Parameters.Add(new SqlParameter("@DocumentTypeId", SqlDbType.Int));
        cmd.Parameters["@DocumentTypeId"].Value = DocumentTypeId;
        cmd.Parameters.Add(new SqlParameter("@RuleTypeId", SqlDbType.Int));
        cmd.Parameters["@RuleTypeId"].Value = RuleTypeId;
        cmd.Parameters.Add(new SqlParameter("@DocumentTitle", SqlDbType.NVarChar));
        cmd.Parameters["@DocumentTitle"].Value = DocumentTitle;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        cmd.Parameters.Add(new SqlParameter("@Whid", SqlDbType.NVarChar));
        cmd.Parameters["@Whid"].Value = Whid;

        cmd.CommandType = CommandType.StoredProcedure;
        dt = new DataTable();
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;
    }

}
