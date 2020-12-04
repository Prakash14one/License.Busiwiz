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
/// Summary description for DocumentCls
/// </summary>
public class DocumentCls1
{

   
    SqlCommand cmd;
    DataTable dt;
    
    public DocumentCls1()
    {
        //
        // TODO: Add constructor logic here
        //
       
    }

    //   haiyal 16-2-2009........
  
    public DataTable SelectDocumentMainType()
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectDocumentMainType";
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;
    }
    public DataTable selectentrytype(string tid)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "select Entry_Type_Name,EntryNumber,Entry_Type_Id FROM EntryTypeMaster INNER JOIN TranctionMaster ON dbo.EntryTypeMaster.Entry_Type_Id = dbo.TranctionMaster.EntryTypeId WHERE  TranctionMaster.Tranction_Master_Id='" + tid + "'";
      
        dt = DatabaseCls1.FillepAdapter(cmd);
        return dt;
    }
    public DataTable selectconame(string eid)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "select CompanyName FROM CompanyMaster  WHERE  compid='" + eid + "'";

        dt = DatabaseCls1.FillepAdapter(cmd);
        return dt;
    }
    public DataTable SelectDocumentMainTypeforpartyId()
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectDocumentMainTypeforpartyId";
        cmd.Parameters.Add(new SqlParameter("@PartyId", SqlDbType.Int));
        cmd.Parameters["@PartyId"].Value = Int32.Parse(HttpContext.Current.Session["PartyId"].ToString());
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["CompanyLoginId"].ToString(); // CompanyLoginId;
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }
    public Int32 InsertDocumentMainType(String DocumentMainType,String Whid)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "InsertDocumentMainType";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@DocumentMainType", SqlDbType.NVarChar));
        cmd.Parameters["@DocumentMainType"].Value = DocumentMainType;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        cmd.Parameters.Add(new SqlParameter("@Whid", SqlDbType.NVarChar));
        cmd.Parameters["@Whid"].Value = Whid;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls1.ExecuteNonQueryep(cmd);
        return result;
    }
    public bool UpdateDocumentMainType(Int32 DocumentMainTypeId, String DocumentMainType,string Whid)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "UpdateDocumentMainType";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@DocumentMainTypeId", SqlDbType.Int));
        cmd.Parameters["@DocumentMainTypeId"].Value = DocumentMainTypeId;
        cmd.Parameters.Add(new SqlParameter("@DocumentMainType", SqlDbType.NVarChar));
        cmd.Parameters["@DocumentMainType"].Value = DocumentMainType;

        cmd.Parameters.Add(new SqlParameter("@Whid", SqlDbType.NVarChar));
        cmd.Parameters["@Whid"].Value = Whid;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        int result = DatabaseCls1.ExecuteNonQueryep(cmd);
        return (result != -1);
    }

    public DataTable SelectDoucmentImageMaster(int DocumentId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        // "Select DocumentImageMaster.*,DoucmentMaster.* from DocumentImageMaster inner join DocumentImageMaster.DocumentMasterId=DoucmentMaster.DocumentId where DoucmentMaster.DocumentId='@DocumentId and CID=@CID"
        // "Select DocumentImageMaster.* from DocumentImageMaster where DocumentMasterId='@DocumentId"

        cmd.CommandText = "SelectDoucmentImageMaster";
        cmd.Parameters.Add(new SqlParameter("@DocumentMasterId", SqlDbType.Int));
        cmd.Parameters["@DocumentMasterId"].Value = DocumentId;
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;
    }
    public bool InsertDocumentApprove(Int32 DocumentProcessingId, bool Approve, String Note)
    {
        cmd = new SqlCommand();

        cmd.CommandText = "InsertDocumentApprove";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@DocumentProcessingId", SqlDbType.Int));
        cmd.Parameters["@DocumentProcessingId"].Value = DocumentProcessingId;
        cmd.Parameters.Add(new SqlParameter("@Approve", SqlDbType.Bit));
        cmd.Parameters["@Approve"].Value = Approve;
        cmd.Parameters.Add(new SqlParameter("@Note", SqlDbType.VarChar));
        cmd.Parameters["@Note"].Value = Note;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        int result = DatabaseCls1.ExecuteNonQueryep(cmd);
        return (result != -1);
    }
    public DataTable SelectDocumentSubType()
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectDocumentSubType";
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;
    }
    public Int32 InsertDocumentSubType(Int32 DocumentMainTypeId, String DocumentSubType)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "InsertDocumentSubType";
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.Add(new SqlParameter("@DocumentMainTypeId", SqlDbType.Int));
        cmd.Parameters["@DocumentMainTypeId"].Value = DocumentMainTypeId;

        cmd.Parameters.Add(new SqlParameter("@DocumentSubType", SqlDbType.NVarChar));
        cmd.Parameters["@DocumentSubType"].Value = DocumentSubType;

        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;

        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        cmd.Parameters.Add(new SqlParameter("@DocumentSubTypeId", SqlDbType.Int));
        cmd.Parameters["@DocumentSubTypeId"].Direction = ParameterDirection.Output;
        Int32 result = DatabaseCls1.ExecuteNonQueryep(cmd);
        result = Convert.ToInt32(cmd.Parameters["@DocumentSubTypeId"].Value.ToString());
        return result;
    }
    public DataTable SelectDocumentSubTypeWithMainType(Int32 DocumentMainTypeId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectDocumentSubTypeWithMainType";
        cmd.Parameters.Add(new SqlParameter("@DocumentMainTypeId", SqlDbType.Int));
        cmd.Parameters["@DocumentMainTypeId"].Value = DocumentMainTypeId;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;
    }
    public DataTable SelectDocumentSubTypeWithMainTypeforPartyID(Int32 DocumentMainTypeId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectDocumentSubTypeWithMainTypeforPartyID";
        cmd.Parameters.Add(new SqlParameter("@DocumentMainTypeId", SqlDbType.Int));
        cmd.Parameters["@DocumentMainTypeId"].Value = DocumentMainTypeId;
        cmd.Parameters.Add(new SqlParameter("@PartyId", SqlDbType.Int));
        cmd.Parameters["@PartyId"].Value = Int32.Parse(HttpContext.Current.Session["PartyId"].ToString());
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["CompanyLoginId"].ToString(); // CompanyLoginId;
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }
    public bool UpdateDocumentSubType(Int32 DocumentSubTypeId, Int32 DocumentMainTypeId, String DocumentSubType)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "UpdateDocumentSubType";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@DocumentSubTypeId", SqlDbType.Int));
        cmd.Parameters["@DocumentSubTypeId"].Value = DocumentSubTypeId;
        cmd.Parameters.Add(new SqlParameter("@DocumentMainTypeId", SqlDbType.Int));
        cmd.Parameters["@DocumentMainTypeId"].Value = DocumentMainTypeId;
        cmd.Parameters.Add(new SqlParameter("@DocumentSubType", SqlDbType.NVarChar));
        cmd.Parameters["@DocumentSubType"].Value = DocumentSubType;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        int result = DatabaseCls1.ExecuteNonQueryep(cmd);
        return (result != -1);
    }



    public DataTable SelectDocumentType(string Whid)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectDocumentType";
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        cmd.Parameters.Add(new SqlParameter("@Whid", SqlDbType.NVarChar));
        cmd.Parameters["@Whid"].Value = Whid; // CompanyLoginId;
    
        
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;
    }
    public DataTable Selectsetuploadinterval()
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "Selectsetuploadinterval";
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;
    }
    public DataTable Selectsetuploadintervalall()
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "Selectsetuploadintervalall";
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;
    }
    public Int32 InsertDocumentType(Int32 DocumentSubTypeId, String DocumentType)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "InsertDocumentType";
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.Add(new SqlParameter("@DocumentSubTypeId", SqlDbType.Int));
        cmd.Parameters["@DocumentSubTypeId"].Value = DocumentSubTypeId;

        cmd.Parameters.Add(new SqlParameter("@DocumentType", SqlDbType.NVarChar));
        cmd.Parameters["@DocumentType"].Value = DocumentType;

        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;

        Int32 result = DatabaseCls1.ExecuteNonQuery(cmd);
        return result;
    }
    public Int32 InsertDocumentType1(Int32 DocumentSubTypeId, String DocumentType)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "InsertDocumentType1";
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.Add(new SqlParameter("@DocumentSubTypeId", SqlDbType.Int));
        cmd.Parameters["@DocumentSubTypeId"].Value = DocumentSubTypeId;

        cmd.Parameters.Add(new SqlParameter("@DocumentType", SqlDbType.NVarChar));
        cmd.Parameters["@DocumentType"].Value = DocumentType;

        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["comid"].ToString(); // CompanyLoginId;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        cmd.Parameters.Add(new SqlParameter("@DocumentTypeId", SqlDbType.Int));
        cmd.Parameters["@DocumentTypeId"].Direction = ParameterDirection.Output;
        Int32 result = DatabaseCls1.ExecuteNonQueryep(cmd);
        result = Convert.ToInt32(cmd.Parameters["@DocumentTypeId"].Value.ToString());
        return result;
    }
    public DataTable SelectDocumentTypeWithSubType(Int32 DocumentSubTypeId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectDocumentTypeWithSubType";
        cmd.Parameters.Add(new SqlParameter("@DocumentSubTypeId", SqlDbType.Int));
        cmd.Parameters["@DocumentSubTypeId"].Value = DocumentSubTypeId;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;
    }
    public DataTable SelectDocumentTypeWithSubTypeforPartyID(Int32 DocumentSubTypeId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectDocumentTypeWithSubTypeforPartyID";
        cmd.Parameters.Add(new SqlParameter("@DocumentSubTypeId", SqlDbType.Int));
        cmd.Parameters["@DocumentSubTypeId"].Value = DocumentSubTypeId;
        cmd.Parameters.Add(new SqlParameter("@PartyId", SqlDbType.Int));
        cmd.Parameters["@PartyId"].Value = Int32.Parse(HttpContext.Current.Session["PartyId"].ToString());
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["CompanyLoginId"].ToString(); // CompanyLoginId;
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }
    public bool UpdateDocumentType(Int32 DocumentTypeId, Int32 DocumentSubTypeId, String DocumentType)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "UpdateDocumentType";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@DocumentTypeId", SqlDbType.Int));
        cmd.Parameters["@DocumentTypeId"].Value = DocumentTypeId;
        cmd.Parameters.Add(new SqlParameter("@DocumentSubTypeId", SqlDbType.Int));
        cmd.Parameters["@DocumentSubTypeId"].Value = DocumentSubTypeId;
        cmd.Parameters.Add(new SqlParameter("@DocumentType", SqlDbType.NVarChar));
        cmd.Parameters["@DocumentType"].Value = DocumentType;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        int result = DatabaseCls1.ExecuteNonQueryep(cmd);
        return (result != -1);
    }

    // Neetu 18-2-09
    public DataTable SelectDocumentAccessRightwithDesignation(Int32 DesignationId, Int32 DocumentTypeId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectDocumentAccessRightwithDesignation";
        cmd.Parameters.Add(new SqlParameter("@DocumentTypeId", SqlDbType.Int));
        cmd.Parameters["@DocumentTypeId"].Value = DocumentTypeId;
        cmd.Parameters.Add(new SqlParameter("@DesignationId", SqlDbType.Int));
        cmd.Parameters["@DesignationId"].Value = DesignationId;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;
    }
    public DataTable SelectEmailAccessRightwithCompanyID(Int32 CompanyEmailId, Int32 EmployeeId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectEmailAccessRightwithCompanyID";
        cmd.Parameters.Add(new SqlParameter("@EmployeeId", SqlDbType.Int));
        cmd.Parameters["@EmployeeId"].Value = EmployeeId;
        cmd.Parameters.Add(new SqlParameter("@CompanyEmailId", SqlDbType.Int));
        cmd.Parameters["@CompanyEmailId"].Value = CompanyEmailId;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;
    }
    public DataTable SelectCabinetAccessRightwithDesignation(Int32 DesignationId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectCabinetAccessRightwithDesignation";        
        cmd.Parameters.Add(new SqlParameter("@DesignationId", SqlDbType.Int));
        cmd.Parameters["@DesignationId"].Value = DesignationId;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["CompanyLoginId"].ToString(); // CompanyLoginId;
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }
    public DataTable SelectDrawerAccessRightwithDesignation(Int32 DesignationId, Int32 DocumentMainTypeId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectDrawerAccessRightwithDesignation";
        cmd.Parameters.Add(new SqlParameter("@DocumentMainTypeId", SqlDbType.Int));
        cmd.Parameters["@DocumentMainTypeId"].Value = DocumentMainTypeId;
        cmd.Parameters.Add(new SqlParameter("@DesignationId", SqlDbType.Int));
        cmd.Parameters["@DesignationId"].Value = DesignationId;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["CompanyLoginId"].ToString(); // CompanyLoginId;
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }
    public DataTable SelectFolderAccessRightwithDesignation(Int32 DesignationId, Int32 DocumentSubTypeId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectFolderAccessRightwithDesignation";
        cmd.Parameters.Add(new SqlParameter("@DocumentSubTypeId", SqlDbType.Int));
        cmd.Parameters["@DocumentSubTypeId"].Value = DocumentSubTypeId;
        cmd.Parameters.Add(new SqlParameter("@DesignationId", SqlDbType.Int));
        cmd.Parameters["@DesignationId"].Value = DesignationId;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["CompanyLoginId"].ToString(); // CompanyLoginId;
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }
    public DataTable SelectDocumentAccessRightwithDesignationdup(Int32 DesignationId, Int32 DocumentTypeId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectDocumentAccessRightwithDesignationdup";
        cmd.Parameters.Add(new SqlParameter("@DocumentTypeId", SqlDbType.Int));
        cmd.Parameters["@DocumentTypeId"].Value = DocumentTypeId;
        cmd.Parameters.Add(new SqlParameter("@DesignationId", SqlDbType.Int));
        cmd.Parameters["@DesignationId"].Value = DesignationId;
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }

    public Int32 InsertDocumentAccessRightMaster(Int32 DocumentTypeId, Int32 DesignationId, bool PrintAccess, bool ViewAccess,
        bool DeleteAccess, bool SaveAccess, bool EditAccess, bool EmailAccess, bool FaxAccess, bool MessageAccess)
    {

        cmd = new SqlCommand();
        cmd.CommandText = "[InsertDocumentAccessRightMaster]";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@DesignationId", SqlDbType.Int));
        cmd.Parameters["@DesignationId"].Value = DesignationId;
        cmd.Parameters.Add(new SqlParameter("@DocumentTypeId", SqlDbType.Int));
        cmd.Parameters["@DocumentTypeId"].Value = DocumentTypeId;
        cmd.Parameters.Add(new SqlParameter("@PrintAccess", SqlDbType.Bit));
        cmd.Parameters["@PrintAccess"].Value = PrintAccess;
        cmd.Parameters.Add(new SqlParameter("@ViewAccess", SqlDbType.Bit));
        cmd.Parameters["@ViewAccess"].Value = ViewAccess;
        cmd.Parameters.Add(new SqlParameter("@DeleteAccess", SqlDbType.Bit));
        cmd.Parameters["@DeleteAccess"].Value = DeleteAccess;
        cmd.Parameters.Add(new SqlParameter("@SaveAccess", SqlDbType.Bit));
        cmd.Parameters["@SaveAccess"].Value = SaveAccess;
        cmd.Parameters.Add(new SqlParameter("@EditAccess", SqlDbType.Bit));
        cmd.Parameters["@EditAccess"].Value = EditAccess;
        cmd.Parameters.Add(new SqlParameter("@EmailAccess", SqlDbType.Bit));
        cmd.Parameters["@EmailAccess"].Value = EmailAccess;
        cmd.Parameters.Add(new SqlParameter("@FaxAccess", SqlDbType.Bit));
        cmd.Parameters["@FaxAccess"].Value = FaxAccess;
        cmd.Parameters.Add(new SqlParameter("@MessageAccess", SqlDbType.Bit));
        cmd.Parameters["@MessageAccess"].Value = MessageAccess;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls1.ExecuteNonQueryep(cmd);
        result = Int32.Parse(cmd.Parameters["@ReturnValue"].Value.ToString());
        return result;
    }
    public Int32 InsertIntervalTime(String uploadtype, Int32 intervaltime)
    {

        cmd = new SqlCommand();
        cmd.CommandText = "[InsertIntervalTime]";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@uploadtype", SqlDbType.NVarChar));
        cmd.Parameters["@uploadtype"].Value = uploadtype;
        cmd.Parameters.Add(new SqlParameter("@intervaltime", SqlDbType.Int));
        cmd.Parameters["@intervaltime"].Value = intervaltime;        
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls1.ExecuteNonQueryep(cmd);
        result = Int32.Parse(cmd.Parameters["@ReturnValue"].Value.ToString());
        return result;
    }
    public Int32 InsertCompanyEmailAccessRightMaster(Int32 CompanyEmailId, Int32 EmployeeId, bool viewRights, bool DeleteRights, bool SendRights)
    {

        cmd = new SqlCommand();
        cmd.CommandText = "[InsertCompanyEmailAccessRightMaster]";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@CompanyEmailId", SqlDbType.Int));
        cmd.Parameters["@CompanyEmailId"].Value = CompanyEmailId;
        cmd.Parameters.Add(new SqlParameter("@EmployeeId", SqlDbType.Int));
        cmd.Parameters["@EmployeeId"].Value = EmployeeId;
        cmd.Parameters.Add(new SqlParameter("@viewRights", SqlDbType.Bit));
        cmd.Parameters["@viewRights"].Value = viewRights;
        cmd.Parameters.Add(new SqlParameter("@DeleteRights", SqlDbType.Bit));
        cmd.Parameters["@DeleteRights"].Value = DeleteRights;
        cmd.Parameters.Add(new SqlParameter("@SendRights", SqlDbType.Bit));
        cmd.Parameters["@SendRights"].Value = SendRights;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls1.ExecuteNonQueryep(cmd);
        result = Int32.Parse(cmd.Parameters["@ReturnValue"].Value.ToString());
        return result;
    }
    public Int32 InsertDocumentAccessRightMasterdup(Int32 DocumentTypeId, Int32 DesignationId, bool PrintAccess, bool ViewAccess,
        bool DeleteAccess, bool SaveAccess, bool EditAccess, bool EmailAccess, bool FaxAccess, bool MessageAccess)
    {

        cmd = new SqlCommand();
        cmd.CommandText = "[InsertDocumentAccessRightMasterdup]";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@DesignationId", SqlDbType.Int));
        cmd.Parameters["@DesignationId"].Value = DesignationId;
        cmd.Parameters.Add(new SqlParameter("@DocumentTypeId", SqlDbType.Int));
        cmd.Parameters["@DocumentTypeId"].Value = DocumentTypeId;
        cmd.Parameters.Add(new SqlParameter("@PrintAccess", SqlDbType.Bit));
        cmd.Parameters["@PrintAccess"].Value = PrintAccess;
        cmd.Parameters.Add(new SqlParameter("@ViewAccess", SqlDbType.Bit));
        cmd.Parameters["@ViewAccess"].Value = ViewAccess;
        cmd.Parameters.Add(new SqlParameter("@DeleteAccess", SqlDbType.Bit));
        cmd.Parameters["@DeleteAccess"].Value = DeleteAccess;
        cmd.Parameters.Add(new SqlParameter("@SaveAccess", SqlDbType.Bit));
        cmd.Parameters["@SaveAccess"].Value = SaveAccess;
        cmd.Parameters.Add(new SqlParameter("@EditAccess", SqlDbType.Bit));
        cmd.Parameters["@EditAccess"].Value = EditAccess;
        cmd.Parameters.Add(new SqlParameter("@EmailAccess", SqlDbType.Bit));
        cmd.Parameters["@EmailAccess"].Value = EmailAccess;
        cmd.Parameters.Add(new SqlParameter("@FaxAccess", SqlDbType.Bit));
        cmd.Parameters["@FaxAccess"].Value = FaxAccess;
        cmd.Parameters.Add(new SqlParameter("@MessageAccess", SqlDbType.Bit));
        cmd.Parameters["@MessageAccess"].Value = MessageAccess;

        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls1.ExecuteNonQuery(cmd);
        result = Int32.Parse(cmd.Parameters["@ReturnValue"].Value.ToString());
        return result;
    }

    //== added by alkesh 18-02-2009
    public Int32 InsertCabinetAccessRightMaster(Int32 DesignationId, bool PrintAccess, bool ViewAccess,
        bool DeleteAccess, bool SaveAccess, bool EditAccess, bool EmailAccess, bool FaxAccess, bool MessageAccess)
    {

        cmd = new SqlCommand();
        cmd.CommandText = "[InsertCabinetAccessRightMaster]";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@DesignationId", SqlDbType.Int));
        cmd.Parameters["@DesignationId"].Value = DesignationId;        
        cmd.Parameters.Add(new SqlParameter("@PrintAccess", SqlDbType.Bit));
        cmd.Parameters["@PrintAccess"].Value = PrintAccess;
        cmd.Parameters.Add(new SqlParameter("@ViewAccess", SqlDbType.Bit));
        cmd.Parameters["@ViewAccess"].Value = ViewAccess;
        cmd.Parameters.Add(new SqlParameter("@DeleteAccess", SqlDbType.Bit));
        cmd.Parameters["@DeleteAccess"].Value = DeleteAccess;
        cmd.Parameters.Add(new SqlParameter("@SaveAccess", SqlDbType.Bit));
        cmd.Parameters["@SaveAccess"].Value = SaveAccess;
        cmd.Parameters.Add(new SqlParameter("@EditAccess", SqlDbType.Bit));
        cmd.Parameters["@EditAccess"].Value = EditAccess;
        cmd.Parameters.Add(new SqlParameter("@EmailAccess", SqlDbType.Bit));
        cmd.Parameters["@EmailAccess"].Value = EmailAccess;
        cmd.Parameters.Add(new SqlParameter("@FaxAccess", SqlDbType.Bit));
        cmd.Parameters["@FaxAccess"].Value = FaxAccess;
        cmd.Parameters.Add(new SqlParameter("@MessageAccess", SqlDbType.Bit));
        cmd.Parameters["@MessageAccess"].Value = MessageAccess;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls1.ExecuteNonQueryep(cmd);
        result = Int32.Parse(cmd.Parameters["@ReturnValue"].Value.ToString());
        return result;
    }
    public Int32 InsertDrawerAccessRightMaster(Int32 DocumentMainTypeId,Int32 DesignationId, bool PrintAccess, bool ViewAccess,
        bool DeleteAccess, bool SaveAccess, bool EditAccess, bool EmailAccess, bool FaxAccess, bool MessageAccess)
    {

        cmd = new SqlCommand();
        cmd.CommandText = "[InsertDrawerAccessRightMaster]";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@DocumentMainTypeId", SqlDbType.Int));
        cmd.Parameters["@DocumentMainTypeId"].Value = DocumentMainTypeId;
        cmd.Parameters.Add(new SqlParameter("@DesignationId", SqlDbType.Int));
        cmd.Parameters["@DesignationId"].Value = DesignationId;
        cmd.Parameters.Add(new SqlParameter("@PrintAccess", SqlDbType.Bit));
        cmd.Parameters["@PrintAccess"].Value = PrintAccess;
        cmd.Parameters.Add(new SqlParameter("@ViewAccess", SqlDbType.Bit));
        cmd.Parameters["@ViewAccess"].Value = ViewAccess;
        cmd.Parameters.Add(new SqlParameter("@DeleteAccess", SqlDbType.Bit));
        cmd.Parameters["@DeleteAccess"].Value = DeleteAccess;
        cmd.Parameters.Add(new SqlParameter("@SaveAccess", SqlDbType.Bit));
        cmd.Parameters["@SaveAccess"].Value = SaveAccess;
        cmd.Parameters.Add(new SqlParameter("@EditAccess", SqlDbType.Bit));
        cmd.Parameters["@EditAccess"].Value = EditAccess;
        cmd.Parameters.Add(new SqlParameter("@EmailAccess", SqlDbType.Bit));
        cmd.Parameters["@EmailAccess"].Value = EmailAccess;
        cmd.Parameters.Add(new SqlParameter("@FaxAccess", SqlDbType.Bit));
        cmd.Parameters["@FaxAccess"].Value = FaxAccess;
        cmd.Parameters.Add(new SqlParameter("@MessageAccess", SqlDbType.Bit));
        cmd.Parameters["@MessageAccess"].Value = MessageAccess;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls1.ExecuteNonQueryep(cmd);
        result = Int32.Parse(cmd.Parameters["@ReturnValue"].Value.ToString());
        return result;
    }
    public Int32 InsertFolderAccessRightMaster(Int32 DocumentSubTypeId,Int32 DesignationId, bool PrintAccess, bool ViewAccess,
        bool DeleteAccess, bool SaveAccess, bool EditAccess, bool EmailAccess, bool FaxAccess, bool MessageAccess)
    {

        cmd = new SqlCommand();
        cmd.CommandText = "[InsertFolderAccessRightMaster]";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@DocumentSubTypeId", SqlDbType.Int));
        cmd.Parameters["@DocumentSubTypeId"].Value = DocumentSubTypeId;
        cmd.Parameters.Add(new SqlParameter("@DesignationId", SqlDbType.Int));
        cmd.Parameters["@DesignationId"].Value = DesignationId;
        cmd.Parameters.Add(new SqlParameter("@PrintAccess", SqlDbType.Bit));
        cmd.Parameters["@PrintAccess"].Value = PrintAccess;
        cmd.Parameters.Add(new SqlParameter("@ViewAccess", SqlDbType.Bit));
        cmd.Parameters["@ViewAccess"].Value = ViewAccess;
        cmd.Parameters.Add(new SqlParameter("@DeleteAccess", SqlDbType.Bit));
        cmd.Parameters["@DeleteAccess"].Value = DeleteAccess;
        cmd.Parameters.Add(new SqlParameter("@SaveAccess", SqlDbType.Bit));
        cmd.Parameters["@SaveAccess"].Value = SaveAccess;
        cmd.Parameters.Add(new SqlParameter("@EditAccess", SqlDbType.Bit));
        cmd.Parameters["@EditAccess"].Value = EditAccess;
        cmd.Parameters.Add(new SqlParameter("@EmailAccess", SqlDbType.Bit));
        cmd.Parameters["@EmailAccess"].Value = EmailAccess;
        cmd.Parameters.Add(new SqlParameter("@FaxAccess", SqlDbType.Bit));
        cmd.Parameters["@FaxAccess"].Value = FaxAccess;
        cmd.Parameters.Add(new SqlParameter("@MessageAccess", SqlDbType.Bit));
        cmd.Parameters["@MessageAccess"].Value = MessageAccess;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls1.ExecuteNonQueryep(cmd);
        result = Int32.Parse(cmd.Parameters["@ReturnValue"].Value.ToString());
        return result;
    }
    public DataTable selectparty(string Whid)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectPartyMaster";
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString();
        cmd.Parameters.Add(new SqlParameter("@Whid", SqlDbType.NVarChar));
        cmd.Parameters["@Whid"].Value = Whid;// CompanyLoginId;
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;

    }
    public DataTable SelectPartyidAll()
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectPartyidAll";
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["CompanyLoginId"].ToString(); // CompanyLoginId;
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }


    public DataTable CreateDatatable()
    {
        DataTable dt = new DataTable();
        DataColumn Dcom = new DataColumn();
        Dcom.DataType = System.Type.GetType("System.String");
        Dcom.ColumnName = "documentname";
        Dcom.AllowDBNull = true;
        Dcom.Unique = false;
        Dcom.ReadOnly = false;

        DataColumn Dcom1 = new DataColumn();
        Dcom1.DataType = System.Type.GetType("System.String");
        Dcom1.ColumnName = "documenttype";
        Dcom1.AllowDBNull = true;
        Dcom1.Unique = false;
        Dcom1.ReadOnly = false;

        DataColumn Dcom2 = new DataColumn();
        Dcom2.DataType = System.Type.GetType("System.String");
        Dcom2.ColumnName = "DocumentTitle";
        Dcom2.AllowDBNull = true;
        Dcom2.Unique = false;
        Dcom2.ReadOnly = false;
        DataColumn Dcom3 = new DataColumn();
        Dcom3.DataType = System.Type.GetType("System.String");
        Dcom3.ColumnName = "status";
        Dcom3.AllowDBNull = true;
        Dcom3.Unique = false;
        Dcom3.ReadOnly = false;


        DataColumn Dcom4 = new DataColumn();
        Dcom4.DataType = System.Type.GetType("System.String");
        Dcom4.ColumnName = "Docty";
        Dcom4.AllowDBNull = true;
        Dcom4.Unique = false;
        Dcom4.ReadOnly = false;

        DataColumn Dcom5 = new DataColumn();
        Dcom5.DataType = System.Type.GetType("System.String");
        Dcom5.ColumnName = "DoctyId";
        Dcom5.AllowDBNull = true;
        Dcom5.Unique = false;
        Dcom5.ReadOnly = false;
        DataColumn Dcom6 = new DataColumn();
        Dcom6.DataType = System.Type.GetType("System.String");
        Dcom6.ColumnName = "PRN";
        Dcom6.AllowDBNull = true;
        Dcom6.Unique = false;
        Dcom6.ReadOnly = false;

        dt.Columns.Add(Dcom);
        dt.Columns.Add(Dcom1);
        dt.Columns.Add(Dcom2);
        dt.Columns.Add(Dcom3);
        dt.Columns.Add(Dcom4);
        dt.Columns.Add(Dcom5);
        dt.Columns.Add(Dcom6);
        return dt;
    }

    // Neetu 19_2_09  start
    public DataTable SelectDocumentforProcessing(String Whid)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectDocumentforProcessing";
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        cmd.Parameters.Add(new SqlParameter("@Whid", SqlDbType.NVarChar));
        cmd.Parameters["@Whid"].Value = Whid; // CompanyLoginId;
        dt = DatabaseCls1.FilleppAdapter(cmd); //.FillAdapter(cmd);
        return dt;

    }
    public DataTable SelectMaxDocumentIdfromDocumentMaster()
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectMaxDocumentIdfromDocumentMaster";
        dt = DatabaseCls1.FillAdapter(cmd); //.FillAdapter(cmd);
        return dt;

    }
    public DataTable SelectGeneralFolderId(String Whid)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectGeneralFolderId";
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        cmd.Parameters.Add(new SqlParameter("@Whid", SqlDbType.NVarChar));
        cmd.Parameters["@Whid"].Value = Whid; // CompanyLoginId;
        dt = DatabaseCls1.FilleppAdapter(cmd); //.FillAdapter(cmd);
        return dt;

    }
    public DataTable SelectGeneralCabinetId()
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectGeneralCabinetId";
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["CompanyLoginId"].ToString(); // CompanyLoginId;
        dt = DatabaseCls1.FillAdapter(cmd); //.FillAdapter(cmd);
        return dt;

    }
    public DataTable SelectMinPartyIdfromPartyMasterName(String Whid)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectMinPartyIdfromPartyMasterName";
     
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString();
        cmd.Parameters.Add(new SqlParameter("@Whid", SqlDbType.NVarChar));
        cmd.Parameters["@Whid"].Value = Whid;// CompanyLoginId;
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;

    }
    public DataTable SelectMinPartyIdfromPartyMaster(string partyname,String Whid)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectMinPartyIdfromPartyMaster";        
        cmd.Parameters.Add(new SqlParameter("@PartyName", SqlDbType.NVarChar));
        cmd.Parameters["@PartyName"].Value = partyname;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString();
        cmd.Parameters.Add(new SqlParameter("@Whid", SqlDbType.NVarChar));
        cmd.Parameters["@Whid"].Value = Whid;// CompanyLoginId;
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;

    }
    public DataTable SelectPartyIdFromPartyEmail(string Email)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectPartyIdFromPartyEmail";
        cmd.Parameters.Add(new SqlParameter("@Email", SqlDbType.NVarChar));
        cmd.Parameters["@Email"].Value = Email;
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;

    }
    public DataTable SelectOtherPartyTypeId(String PartyTypeName)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectOtherPartyTypeId";
        cmd.Parameters.Add(new SqlParameter("@PartyTypeName", SqlDbType.NVarChar));
        cmd.Parameters["@PartyTypeName"].Value = PartyTypeName;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;
    }
    public DataTable SelectSpamEmail(string Email,Int32 Partyid)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectSpamEmail";
        cmd.Parameters.Add(new SqlParameter("@SpamEmailId", SqlDbType.NVarChar));
        cmd.Parameters["@SpamEmailId"].Value = Email;
        cmd.Parameters.Add(new SqlParameter("@PartyId", SqlDbType.Int));
        cmd.Parameters["@PartyId"].Value = Partyid;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;

    }
    public DataTable SelectAllSpamEmailByParty(Int32 PartyId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectAllSpamEmailByParty";
        cmd.Parameters.Add(new SqlParameter("@PartyId", SqlDbType.Int));
        cmd.Parameters["@PartyId"].Value = PartyId;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        dt = DatabaseCls.FillAdapter(cmd); //.FillAdapter(cmd);
        return dt;

    }
    public DataTable SelectMaxDocumentSubIdfromDocumentSubType(string DocumentSubType,String Whid)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectMaxDocumentSubIdfromDocumentSubType";
        cmd.Parameters.Add(new SqlParameter("@DocumentSubType", SqlDbType.NVarChar));
        cmd.Parameters["@DocumentSubType"].Value = DocumentSubType;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        cmd.Parameters.Add(new SqlParameter("@Whid", SqlDbType.NVarChar));
        cmd.Parameters["@Whid"].Value =Whid; // CompanyLoginId;
  
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;

    }
    public DataTable SelectFolderId1(string DocumentType)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectFolderId1";
        cmd.Parameters.Add(new SqlParameter("@DocumentType", SqlDbType.NVarChar));
        cmd.Parameters["@DocumentType"].Value = DocumentType;
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;

    }
    public DataTable SelectCabinetName(string CabinetName)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectCabinetName";
        cmd.Parameters.Add(new SqlParameter("@DocumentMainType", SqlDbType.NVarChar));
        cmd.Parameters["@DocumentMainType"].Value = CabinetName;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["CompanyLoginId"].ToString(); // CompanyLoginId;
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;

    }
    public DataTable SelectDrawerName(string DrawerName)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectDrawerName";
        cmd.Parameters.Add(new SqlParameter("@DocumentSubType", SqlDbType.NVarChar));
        cmd.Parameters["@DocumentSubType"].Value = DrawerName;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["CompanyLoginId"].ToString(); // CompanyLoginId;
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;

    }
    public DataTable SelectFolderName(string FolderName,string Whid)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectFolderName";
        cmd.Parameters.Add(new SqlParameter("@DocumentType", SqlDbType.NVarChar));
        cmd.Parameters["@DocumentType"].Value = FolderName;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString();
        cmd.Parameters.Add(new SqlParameter("@Whid", SqlDbType.NVarChar));
        cmd.Parameters["@Whid"].Value = HttpContext.Current.Session["Whid"].ToString();
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;

    }
    public DataTable SelectCountryMaster(string CountryName, string CountryCode)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectCountryMasterexit";
        cmd.Parameters.Add(new SqlParameter("@CountryName", SqlDbType.NVarChar));
        cmd.Parameters["@CountryName"].Value = CountryName;
        cmd.Parameters.Add(new SqlParameter("@CountryCode", SqlDbType.NVarChar));
        cmd.Parameters["@CountryCode"].Value = CountryCode;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["CompanyLoginId"].ToString(); // CompanyLoginId;
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;

    }
    public DataTable SelectDepartmentMasterexit(string DepartmentName)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectDepartmentMasterexit";
        cmd.Parameters.Add(new SqlParameter("@DepartmentName", SqlDbType.NVarChar));
        cmd.Parameters["@DepartmentName"].Value = DepartmentName;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["CompanyLoginId"].ToString(); // CompanyLoginId;
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;

    }
    public DataTable SelectCompanyEmailexitoriginal(string OutEmailID, string Whid,string empid)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectCompanyEmailexitoriginal";
        cmd.Parameters.Add(new SqlParameter("@EmailId", SqlDbType.NVarChar));
        cmd.Parameters["@EmailId"].Value = OutEmailID;
        cmd.Parameters.Add(new SqlParameter("@Whid", SqlDbType.NVarChar));
        cmd.Parameters["@Whid"].Value = Whid; // CompanyLoginId;
        cmd.Parameters.Add(new SqlParameter("@EmployeeID", SqlDbType.NVarChar));
        cmd.Parameters["@EmployeeID"].Value = Whid; // CompanyLoginId;
        //cmd.Parameters.Add(new SqlParameter("@rest", SqlDbType.VarChar));
        //cmd.Parameters["@rest"].Direction = ParameterDirection.Output;
        //cmd.Parameters.Add(new SqlParameter("@rest", SqlDbType.NVarChar));
        //cmd.Parameters["@rest"].Size = 500;
        //cmd.CommandType = CommandType.StoredProcedure;
        //cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        //cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        //SqlCommand cmdd = new SqlCommand();
        //cmdd = (SqlCommand)DatabaseCls1.FillAdapterwithreturn(cmd);        
        //string result = Convert.ToString(cmdd.Parameters["@rest"].Value.ToString());
        //return result;
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;

    }
    public DataTable SelectCompanyEmailexit(string OutEmailID, string Whid)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectCompanyEmailexit";
        cmd.Parameters.Add(new SqlParameter("@OutEmailID", SqlDbType.NVarChar));
        cmd.Parameters["@OutEmailID"].Value = OutEmailID;
        cmd.Parameters.Add(new SqlParameter("@Whid", SqlDbType.NVarChar));
        cmd.Parameters["@Whid"].Value = Whid; // CompanyLoginId;
        //cmd.Parameters.Add(new SqlParameter("@rest", SqlDbType.VarChar));
        //cmd.Parameters["@rest"].Direction = ParameterDirection.Output;
        //cmd.Parameters.Add(new SqlParameter("@rest", SqlDbType.NVarChar));
        //cmd.Parameters["@rest"].Size = 500;
        //cmd.CommandType = CommandType.StoredProcedure;
        //cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        //cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        //SqlCommand cmdd = new SqlCommand();
        //cmdd = (SqlCommand)DatabaseCls1.FillAdapterwithreturn(cmd);        
        //string result = Convert.ToString(cmdd.Parameters["@rest"].Value.ToString());
        //return result;
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;

    }
    public DataTable SelectCompanyInEmailexit(string InEmailID, string Whid)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectCompanyInEmailexit";
        cmd.Parameters.Add(new SqlParameter("@InEmailID", SqlDbType.NVarChar));
        cmd.Parameters["@InEmailID"].Value = InEmailID;
        cmd.Parameters.Add(new SqlParameter("@Whid", SqlDbType.NVarChar));
        cmd.Parameters["@Whid"].Value = Whid; // CompanyLoginId;
        //cmd.Parameters.Add(new SqlParameter("@rest", SqlDbType.VarChar));
        //cmd.Parameters["@rest"].Direction = ParameterDirection.Output;
        //cmd.Parameters.Add(new SqlParameter("@rest", SqlDbType.NVarChar));
        //cmd.Parameters["@rest"].Size = 500;
        //cmd.CommandType = CommandType.StoredProcedure;
        //cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        //cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        //SqlCommand cmdd = new SqlCommand();
        //cmdd = (SqlCommand)DatabaseCls1.FillAdapterwithreturn(cmd);        
        //string result = Convert.ToString(cmdd.Parameters["@rest"].Value.ToString());
        //return result;
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;

    }
    public DataTable SelectStateMasterexit(string StateName, string StateCode,Int32 CountryId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectStateMasterexit";
        cmd.Parameters.Add(new SqlParameter("@StateName", SqlDbType.NVarChar));
        cmd.Parameters["@StateName"].Value = StateName;
        cmd.Parameters.Add(new SqlParameter("@StateCode", SqlDbType.NVarChar));
        cmd.Parameters["@StateCode"].Value = StateCode;
        cmd.Parameters.Add(new SqlParameter("@CountryId", SqlDbType.NVarChar));
        cmd.Parameters["@CountryId"].Value = CountryId;
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;

    }
    public DataTable SelectFolderIdFromPartyID(Int32 PartyId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectFolderIdFromPartyID";
        cmd.Parameters.Add(new SqlParameter("@PartyId", SqlDbType.NVarChar));
        cmd.Parameters["@PartyId"].Value = PartyId;
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;

    }
    public DataTable SelectFolderPartyRelation()
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectFolderPartyRelation";        
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;

    }
    public DataTable SelectFolderPartyRelationBYDocId(Int32 DocumentTypeId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectFolderPartyRelationBYDocId";
        cmd.Parameters.Add(new SqlParameter("@DocumentTypeId", SqlDbType.Int));
        cmd.Parameters["@DocumentTypeId"].Value = DocumentTypeId;
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;

    }
    public DataTable SelectPartyIdFromFolderID(Int32 FolderId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectPartyIdFromFolderID";
        cmd.Parameters.Add(new SqlParameter("@FolderId", SqlDbType.NVarChar));
        cmd.Parameters["@FolderId"].Value = FolderId;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;

    }
    public DataTable SelectFolderIdPartyID(Int32 PartyId, Int32 FolderId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectFolderIdPartyID";
        cmd.Parameters.Add(new SqlParameter("@PartyId", SqlDbType.Int));
        cmd.Parameters["@PartyId"].Value = PartyId;
        cmd.Parameters.Add(new SqlParameter("@FolderId", SqlDbType.Int));
        cmd.Parameters["@FolderId"].Value = FolderId;
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;

    }
    public DataTable SelectDocumentforMyApproval(Int32 EmployeeID,String Whid)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectDocumentforMyApproval";
        cmd.Parameters.Add(new SqlParameter("@EmployeeId", SqlDbType.Int));
        cmd.Parameters["@EmployeeId"].Value = EmployeeID; // EmployeeId;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        cmd.Parameters.Add(new SqlParameter("@Whid", SqlDbType.NVarChar));
        cmd.Parameters["@Whid"].Value = Whid; // CompanyLoginId;
       
        dt = DatabaseCls1.FilleppAdapter(cmd); //.FillAdapter(cmd);
        return dt;

    }

    public DataTable SelectDocumentforApproval(String Emp, String pera)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectDocumentforApproval";
        cmd.Parameters.Add(new SqlParameter("@EmployeeId", SqlDbType.Int));
        cmd.Parameters["@EmployeeId"].Value = Convert.ToInt32(Emp); // EmployeeId;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        cmd.Parameters.Add(new SqlParameter("@accepval", SqlDbType.NVarChar));
        cmd.Parameters["@accepval"].Value =pera; // EmployeeId;
        dt = DatabaseCls1.FilleppAdapter(cmd); //.FillAdapter(cmd);
        return dt;

    }
    public bool insertDocumentProcessing(Int32 EmployeeId, Int32 DocumentId)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "insertDocumentProcessing";
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.Add(new SqlParameter("@EmployeeId", SqlDbType.Int));
        cmd.Parameters["@EmployeeId"].Value = EmployeeId;

        cmd.Parameters.Add(new SqlParameter("@DocumentId", SqlDbType.Int));
        cmd.Parameters["@DocumentId"].Value = DocumentId;

        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;

        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;

        Int32 result = DatabaseCls1.ExecuteNonQueryep(cmd);
        return (result != -1);

    }
    public bool insertDocumentProcessingnew(Int32 EmployeeId, Int32 DocumentId,Boolean Approve)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "insertDocumentProcessingnew";
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.Add(new SqlParameter("@EmployeeId", SqlDbType.Int));
        cmd.Parameters["@EmployeeId"].Value = EmployeeId;

        cmd.Parameters.Add(new SqlParameter("@DocumentId", SqlDbType.Int));
        cmd.Parameters["@DocumentId"].Value = DocumentId;

        cmd.Parameters.Add(new SqlParameter("@Approve", SqlDbType.Bit));
        cmd.Parameters["@Approve"].Value = Approve;

        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;

        Int32 result = DatabaseCls1.ExecuteNonQueryep(cmd);
        return (result != -1);

    }
    // Neetu 19_2_09 end
    public Int32 UpdateDocumentMaster(Int32 DocumentId, Int32 DocumentTypeId, Int32

DocumentUploadTypeId, DateTime DocumentUploadDate, String DocumentName, String DocumentTitle, String

Description, Int32 PartyId, String DocumentRefNo, Decimal DocumentAmount, DateTime DocumentDate, String Doct, String PartDocrefno)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "UpdateDocumentMaster";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@DocumentId", SqlDbType.Int));
        cmd.Parameters["@DocumentId"].Value = DocumentId;

        cmd.Parameters.Add(new SqlParameter("@DocumentTypeId", SqlDbType.Int));
        cmd.Parameters["@DocumentTypeId"].Value = DocumentTypeId;
        cmd.Parameters.Add(new SqlParameter("@DocumentUploadTypeId", SqlDbType.Int));
        cmd.Parameters["@DocumentUploadTypeId"].Value = DocumentUploadTypeId;
        cmd.Parameters.Add(new SqlParameter("@DocumentUploadDate", SqlDbType.DateTime));
        cmd.Parameters["@DocumentUploadDate"].Value = DocumentUploadDate;
        cmd.Parameters.Add(new SqlParameter("@DocumentName", SqlDbType.NVarChar));
        cmd.Parameters["@DocumentName"].Value = DocumentName;
        cmd.Parameters.Add(new SqlParameter("@DocumentTitle", SqlDbType.NVarChar));
        cmd.Parameters["@DocumentTitle"].Value = DocumentTitle;
        cmd.Parameters.Add(new SqlParameter("@Description", SqlDbType.NVarChar));
        cmd.Parameters["@Description"].Value = Description;
        cmd.Parameters.Add(new SqlParameter("@PartyId", SqlDbType.Int));
        cmd.Parameters["@PartyId"].Value = PartyId;
        cmd.Parameters.Add(new SqlParameter("@DocumentRefNo", SqlDbType.NVarChar));
        cmd.Parameters["@DocumentRefNo"].Value = DocumentRefNo;
        cmd.Parameters.Add(new SqlParameter("@DocumentAmount", SqlDbType.Decimal));
        cmd.Parameters["@DocumentAmount"].Value = DocumentAmount;
        cmd.Parameters.Add(new SqlParameter("@DocumentDate", SqlDbType.DateTime));
        cmd.Parameters["@DocumentDate"].Value = DocumentDate;

        cmd.Parameters.Add(new SqlParameter("@DocumentTypenmId", SqlDbType.NVarChar));
        cmd.Parameters["@DocumentTypenmId"].Value = Doct;

        cmd.Parameters.Add(new SqlParameter("@PartyDocrefno", SqlDbType.NVarChar));
        cmd.Parameters["@PartyDocrefno"].Value = PartDocrefno;


        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls1.ExecuteNonQueryep(cmd);
        return result;
    }

    public DataTable SelectDocumentMainTypeByType(int DocumentTypeId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectDocumentMainTypeByType";
        cmd.Parameters.Add(new SqlParameter("@DocumentTypeId", SqlDbType.Int));
        cmd.Parameters["@DocumentTypeId"].Value = DocumentTypeId;
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;
    }

    public DataTable SelectDoucmentMasterByIDwithobj(Int32 DocumentId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectDoucmentMasterByIDwithobj";
        cmd.Parameters.Add(new SqlParameter("@DocumentId", SqlDbType.Int));
        cmd.Parameters["@DocumentId"].Value = DocumentId;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;
    }
    public DataTable SelectDocumentAccessRigthsByDesignationID()
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectDoucmentAccessRigthsByDesignationID";
        cmd.Parameters.Add(new SqlParameter("@DesignationId", SqlDbType.Int));
        cmd.Parameters["@DesignationId"].Value = Convert.ToInt32(HttpContext.Current.Session["DesignationId"]);
      
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;
    }
    public DataTable SelectDocumentAccessRigthsByDesignationIDGene(String Whid)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectDocumentAccessRigthsByDesignationIDGene";
        cmd.Parameters.Add(new SqlParameter("@DesignationId", SqlDbType.NVarChar));
        cmd.Parameters["@DesignationId"].Value = Convert.ToInt32(HttpContext.Current.Session["DesignationId"]);
        cmd.Parameters.Add(new SqlParameter("@Whid", SqlDbType.NVarChar));
        cmd.Parameters["@Whid"].Value = Whid;
      
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;
    }
    public DataTable SelectDocumentAccessRigthsByDesignationIDdup()
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectDoucmentAccessRigthsByDesignationIDdup";
        cmd.Parameters.Add(new SqlParameter("@DesignationId", SqlDbType.Int));
        cmd.Parameters["@DesignationId"].Value = Convert.ToInt32(HttpContext.Current.Session["DesignationId"]);
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }
    // ----- updated  by alkesh on 24-02-2009

    public DataTable SelectDoucmentMasterByDocumentTypeID(int DocumentTypeId, String Whid,String Cabinet,String Drower)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectDoucmentMasterByDocumentTypeID";
        cmd.Parameters.Add(new SqlParameter("@DocumentTypeId", SqlDbType.Int));
        cmd.Parameters["@DocumentTypeId"].Value = DocumentTypeId;
       
        cmd.Parameters.Add(new SqlParameter("@Whid", SqlDbType.NVarChar));
        cmd.Parameters["@Whid"].Value = Whid;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["comid"].ToString(); // CompanyLoginId;
        cmd.Parameters.Add(new SqlParameter("@DocumentMainTypeId", SqlDbType.NVarChar));
        cmd.Parameters["@DocumentMainTypeId"].Value =Cabinet; // CompanyLoginId;
        cmd.Parameters.Add(new SqlParameter("@DocumentSubTypeId", SqlDbType.NVarChar));
        cmd.Parameters["@DocumentSubTypeId"].Value = Drower; // CompanyLoginId;
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;
    }
    public DataTable SelectDoucmentMasterByDocumentTypeIDforPartyId(int DocumentTypeId, DateTime startdate, DateTime enddate)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectDoucmentMasterByDocumentTypeIDforPartyId";
        cmd.Parameters.Add(new SqlParameter("@DocumentTypeId", SqlDbType.Int));
        cmd.Parameters["@DocumentTypeId"].Value = DocumentTypeId;
        //cmd.Parameters["@DocumentTypeId"].Value = 117;
        cmd.Parameters.Add(new SqlParameter("@PartyId", SqlDbType.Int));
        cmd.Parameters["@PartyId"].Value = Int32.Parse(HttpContext.Current.Session["PartyId"].ToString());
        cmd.Parameters.Add(new SqlParameter("@startdate", SqlDbType.DateTime));
        cmd.Parameters["@startdate"].Value = startdate;
        cmd.Parameters.Add(new SqlParameter("@enddate", SqlDbType.DateTime));
        cmd.Parameters["@enddate"].Value = enddate;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["CompanyLoginId"].ToString(); // CompanyLoginId;
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }
    public DataTable SelectDoucmentMasterByDocumentDate(int DocumentTypeId, DateTime startdate, DateTime enddate)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectDoucmentMasterByDocumentDate";
        cmd.Parameters.Add(new SqlParameter("@DocumentTypeId", SqlDbType.Int));
        cmd.Parameters["@DocumentTypeId"].Value = DocumentTypeId;
        //cmd.Parameters["@DocumentTypeId"].Value = 117;
        cmd.Parameters.Add(new SqlParameter("@startdate", SqlDbType.DateTime));
        cmd.Parameters["@startdate"].Value = startdate;
        cmd.Parameters.Add(new SqlParameter("@enddate", SqlDbType.DateTime));
        cmd.Parameters["@enddate"].Value = enddate;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["CompanyLoginId"].ToString(); // CompanyLoginId;
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }
    public DataTable SelectDoucmentMasterByIDForSearchByDate(int DocumentTypeId,int DocumentId, DateTime startdate, DateTime enddate,String Whid,String DateType)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectDoucmentMasterByIDForSearchByDate";
        cmd.Parameters.Add(new SqlParameter("@DocumentTypeId", SqlDbType.Int));
        cmd.Parameters["@DocumentTypeId"].Value = DocumentTypeId;
        cmd.Parameters.Add(new SqlParameter("@DocumentId", SqlDbType.Int));
        cmd.Parameters["@DocumentId"].Value = DocumentId;
        //cmd.Parameters["@DocumentTypeId"].Value = 117;
        cmd.Parameters.Add(new SqlParameter("@startdate", SqlDbType.DateTime));
        cmd.Parameters["@startdate"].Value = startdate;
        cmd.Parameters.Add(new SqlParameter("@enddate", SqlDbType.DateTime));
        cmd.Parameters["@enddate"].Value = enddate;
        cmd.Parameters.Add(new SqlParameter("@DateType", SqlDbType.VarChar));
        cmd.Parameters["@DateType"].Value = DateType;

        cmd.Parameters.Add(new SqlParameter("@Whid", SqlDbType.VarChar));
        cmd.Parameters["@Whid"].Value = Whid;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;
    }
    // ====== alkesh on 24-02-2009

    public DataTable SelectDoucmentMyUploadedByLoginId(int EmployeeId, DateTime startdate, DateTime enddate)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectDoucmentMyUploadedByLoginId";
        cmd.Parameters.Add(new SqlParameter("@EmployeeId", SqlDbType.Int));
        cmd.Parameters["@EmployeeId"].Value = EmployeeId;
        cmd.Parameters.Add(new SqlParameter("@startdate", SqlDbType.DateTime));
        cmd.Parameters["@startdate"].Value = startdate;
        cmd.Parameters.Add(new SqlParameter("@enddate", SqlDbType.DateTime));
        cmd.Parameters["@enddate"].Value = enddate;
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }
    public Int32 DeleteDocumentMasterByID(int DocumentId)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "DeleteDocumentMasterByID";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@DocumentId", SqlDbType.Int));
        cmd.Parameters["@DocumentId"].Value = DocumentId;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls1.ExecuteNonQueryep(cmd);
        return result;
    }
    public Int32 DeleteRuleProcess(int DocumentId)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "DeleteRuleProcess";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@DocumentId", SqlDbType.Int));
        cmd.Parameters["@DocumentId"].Value = DocumentId;
        cmd.Parameters.Add(new SqlParameter("@EmployeeId", SqlDbType.Int));
        cmd.Parameters["@EmployeeId"].Value = Convert.ToInt32(HttpContext.Current.Session["EmployeeId"].ToString()); // EmployeeId;        
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls1.ExecuteNonQueryep(cmd);
        return result;
    }
    public Int32 DeleteRuleProcessforParty(int DocumentId)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "DeleteRuleProcessforParty";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@DocumentId", SqlDbType.Int));
        cmd.Parameters["@DocumentId"].Value = DocumentId;
        cmd.Parameters.Add(new SqlParameter("@PartyId", SqlDbType.Int));
        cmd.Parameters["@PartyId"].Value = Convert.ToInt32(HttpContext.Current.Session["PartyId"].ToString()); // EmployeeId;        
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls1.ExecuteNonQueryep(cmd);
        return result;
    }
    public Int32 InsertDocumentLog(int DocumentId, int EmployeeId, DateTime date, bool ViewLog, bool DeleteLog, bool SaveLog, bool EditLog, bool EmailLog, bool FaxLog, bool PrintLog, bool MessageLog)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "InsertDocumentLog";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@DocumentId", SqlDbType.Int));
        cmd.Parameters["@DocumentId"].Value = DocumentId;
        cmd.Parameters.Add(new SqlParameter("@EmployeeId", SqlDbType.Int));
        cmd.Parameters["@EmployeeId"].Value = EmployeeId;
        cmd.Parameters.Add(new SqlParameter("@date", SqlDbType.DateTime));
        cmd.Parameters["@date"].Value = date;
        cmd.Parameters.Add(new SqlParameter("@ViewLog", SqlDbType.Bit));
        cmd.Parameters["@ViewLog"].Value = ViewLog;
        cmd.Parameters.Add(new SqlParameter("@DeleteLog", SqlDbType.Bit));
        cmd.Parameters["@DeleteLog"].Value = DeleteLog;
        cmd.Parameters.Add(new SqlParameter("@SaveLog", SqlDbType.Bit));
        cmd.Parameters["@SaveLog"].Value = SaveLog;
        cmd.Parameters.Add(new SqlParameter("@EditLog", SqlDbType.Bit));
        cmd.Parameters["@EditLog"].Value = EditLog;
        cmd.Parameters.Add(new SqlParameter("@EmailLog", SqlDbType.Bit));
        cmd.Parameters["@EmailLog"].Value = EmailLog;
        cmd.Parameters.Add(new SqlParameter("@FaxLog", SqlDbType.Bit));
        cmd.Parameters["@FaxLog"].Value = FaxLog;
        cmd.Parameters.Add(new SqlParameter("@PrintLog", SqlDbType.Bit));
        cmd.Parameters["@PrintLog"].Value = PrintLog;
        cmd.Parameters.Add(new SqlParameter("@MessageLog", SqlDbType.Bit));
        cmd.Parameters["@MessageLog"].Value = MessageLog;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls1.ExecuteNonQueryep(cmd);
        return result;

    }
    public Int32 InsertDocumentLogByParty(int DocumentId, int PartyId, DateTime date, bool ViewLog, bool DeleteLog, bool SaveLog, bool EditLog, bool EmailLog, bool FaxLog, bool PrintLog, bool MessageLog)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "InsertDocumentLogByParty";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@DocumentId", SqlDbType.Int));
        cmd.Parameters["@DocumentId"].Value = DocumentId;
        cmd.Parameters.Add(new SqlParameter("@PartyId", SqlDbType.Int));
        cmd.Parameters["@PartyId"].Value = PartyId;
        cmd.Parameters.Add(new SqlParameter("@date", SqlDbType.DateTime));
        cmd.Parameters["@date"].Value = date;
        cmd.Parameters.Add(new SqlParameter("@ViewLog", SqlDbType.Bit));
        cmd.Parameters["@ViewLog"].Value = ViewLog;
        cmd.Parameters.Add(new SqlParameter("@DeleteLog", SqlDbType.Bit));
        cmd.Parameters["@DeleteLog"].Value = DeleteLog;
        cmd.Parameters.Add(new SqlParameter("@SaveLog", SqlDbType.Bit));
        cmd.Parameters["@SaveLog"].Value = SaveLog;
        cmd.Parameters.Add(new SqlParameter("@EditLog", SqlDbType.Bit));
        cmd.Parameters["@EditLog"].Value = EditLog;
        cmd.Parameters.Add(new SqlParameter("@EmailLog", SqlDbType.Bit));
        cmd.Parameters["@EmailLog"].Value = EmailLog;
        cmd.Parameters.Add(new SqlParameter("@FaxLog", SqlDbType.Bit));
        cmd.Parameters["@FaxLog"].Value = FaxLog;
        cmd.Parameters.Add(new SqlParameter("@PrintLog", SqlDbType.Bit));
        cmd.Parameters["@PrintLog"].Value = PrintLog;
        cmd.Parameters.Add(new SqlParameter("@MessageLog", SqlDbType.Bit));
        cmd.Parameters["@MessageLog"].Value = MessageLog;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["CompanyLoginId"].ToString(); // CompanyLoginId;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls1.ExecuteNonQuery(cmd);
        return result;

    }
    public DataTable SelectDocumentLog(DateTime startdate, DateTime enddate)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectDocumentLog";

        cmd.Parameters.Add(new SqlParameter("@startdate", SqlDbType.DateTime));
        cmd.Parameters["@startdate"].Value = startdate;
        cmd.Parameters.Add(new SqlParameter("@enddate", SqlDbType.DateTime));
        cmd.Parameters["@enddate"].Value = enddate;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["CompanyLoginId"].ToString(); // CompanyLoginId;
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }


    //___________ Harshal Desai 27 - 03 - 2009 

    public DataTable SelectDocumentLogByType(DateTime startdate, DateTime enddate, int doctype)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectDocumentLogByType";

        cmd.Parameters.Add(new SqlParameter("@startdate", SqlDbType.DateTime));
        cmd.Parameters["@startdate"].Value = startdate;
        cmd.Parameters.Add(new SqlParameter("@enddate", SqlDbType.DateTime));
        cmd.Parameters["@enddate"].Value = enddate;
        cmd.Parameters.Add(new SqlParameter("@doctype", SqlDbType.Int));
        cmd.Parameters["@doctype"].Value = doctype;
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }
    public DataTable SelectDocumentLogByTypeByEmp(DateTime startdate, DateTime enddate, int doctype, int employee)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectDocumentLogByTypeByEmp";

        cmd.Parameters.Add(new SqlParameter("@startdate", SqlDbType.DateTime));
        cmd.Parameters["@startdate"].Value = startdate;
        cmd.Parameters.Add(new SqlParameter("@enddate", SqlDbType.DateTime));
        cmd.Parameters["@enddate"].Value = enddate;
        cmd.Parameters.Add(new SqlParameter("@doctype", SqlDbType.Int));
        cmd.Parameters["@doctype"].Value = doctype;
        cmd.Parameters.Add(new SqlParameter("@employee", SqlDbType.Int));
        cmd.Parameters["@employee"].Value = employee;
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }
    public DataTable SelectDocumentLogByActionByType(DateTime startdate, DateTime enddate, int doctype, String Action)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectDocumentLogByActionByType";

        cmd.Parameters.Add(new SqlParameter("@startdate", SqlDbType.DateTime));
        cmd.Parameters["@startdate"].Value = startdate;
        cmd.Parameters.Add(new SqlParameter("@enddate", SqlDbType.DateTime));
        cmd.Parameters["@enddate"].Value = enddate;
        cmd.Parameters.Add(new SqlParameter("@doctype", SqlDbType.Int));
        cmd.Parameters["@doctype"].Value = doctype;
        cmd.Parameters.Add(new SqlParameter("@action", SqlDbType.NVarChar));
        cmd.Parameters["@action"].Value = Action;
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }
    public DataTable SelectDocumentLogByActionByEmp(DateTime startdate, DateTime enddate, int employee, String Action)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectDocumentLogByActionByEmp";

        cmd.Parameters.Add(new SqlParameter("@startdate", SqlDbType.DateTime));
        cmd.Parameters["@startdate"].Value = startdate;
        cmd.Parameters.Add(new SqlParameter("@enddate", SqlDbType.DateTime));
        cmd.Parameters["@enddate"].Value = enddate;
        cmd.Parameters.Add(new SqlParameter("@employee", SqlDbType.Int));
        cmd.Parameters["@employee"].Value = employee;
        cmd.Parameters.Add(new SqlParameter("@action", SqlDbType.NVarChar));
        cmd.Parameters["@action"].Value = Action;
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }
    public DataTable SelectDocumentLogByActionByTypeByEmp(DateTime startdate, DateTime enddate, int doctype, int employee, String Action)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectDocumentLogByActionByTypeByEmp";

        cmd.Parameters.Add(new SqlParameter("@startdate", SqlDbType.DateTime));
        cmd.Parameters["@startdate"].Value = startdate;
        cmd.Parameters.Add(new SqlParameter("@enddate", SqlDbType.DateTime));
        cmd.Parameters["@enddate"].Value = enddate;
        cmd.Parameters.Add(new SqlParameter("@doctype", SqlDbType.Int));
        cmd.Parameters["@doctype"].Value = doctype;
        cmd.Parameters.Add(new SqlParameter("@employee", SqlDbType.Int));
        cmd.Parameters["@employee"].Value = employee;
        cmd.Parameters.Add(new SqlParameter("@action", SqlDbType.NVarChar));
        cmd.Parameters["@action"].Value = Action;
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }
    public DataTable SelectDocumentLogByAction(DateTime startdate, DateTime enddate, String Action)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectDocumentLogByAction";

        cmd.Parameters.Add(new SqlParameter("@startdate", SqlDbType.DateTime));
        cmd.Parameters["@startdate"].Value = startdate;
        cmd.Parameters.Add(new SqlParameter("@enddate", SqlDbType.DateTime));
        cmd.Parameters["@enddate"].Value = enddate;
        cmd.Parameters.Add(new SqlParameter("@action", SqlDbType.NVarChar));
        cmd.Parameters["@action"].Value = Action;
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }
    public DataTable SelectDocumentLogByDesignation1(DateTime startdate, DateTime enddate, int Department)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectDocumentLogbyDesignation1";

        cmd.Parameters.Add(new SqlParameter("@startdate", SqlDbType.DateTime));
        cmd.Parameters["@startdate"].Value = startdate;
        cmd.Parameters.Add(new SqlParameter("@enddate", SqlDbType.DateTime));
        cmd.Parameters["@enddate"].Value = enddate;
        cmd.Parameters.Add(new SqlParameter("@department", SqlDbType.Int));
        cmd.Parameters["@department"].Value = Department;
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }
    public DataTable SelectDocumentLogByDesignation2(DateTime startdate, DateTime enddate, int Designation)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectDocumentLogbyDesignation2";

        cmd.Parameters.Add(new SqlParameter("@startdate", SqlDbType.DateTime));
        cmd.Parameters["@startdate"].Value = startdate;
        cmd.Parameters.Add(new SqlParameter("@enddate", SqlDbType.DateTime));
        cmd.Parameters["@enddate"].Value = enddate;
        cmd.Parameters.Add(new SqlParameter("@designation", SqlDbType.Int));
        cmd.Parameters["@designation"].Value = Designation;
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }
    public DataTable SelectDocumentLogByDesignation3(DateTime startdate, DateTime enddate, int employee)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectDocumentLogbyDesignation3";

        cmd.Parameters.Add(new SqlParameter("@startdate", SqlDbType.DateTime));
        cmd.Parameters["@startdate"].Value = startdate;
        cmd.Parameters.Add(new SqlParameter("@enddate", SqlDbType.DateTime));
        cmd.Parameters["@enddate"].Value = enddate;
        //cmd.Parameters.Add(new SqlParameter("@employee", SqlDbType.Int));
        //cmd.Parameters["@employee"].Value = employee;
        cmd.Parameters.Add(new SqlParameter("@PartyId", SqlDbType.Int));
        cmd.Parameters["@PartyId"].Value = employee;
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }

    // ---------------------------------------------



    public DataTable CreateDatatableForLog()
    {
        DataTable dt = new DataTable();
        DataColumn Dcom = new DataColumn();
        Dcom.DataType = System.Type.GetType("System.String");
        Dcom.ColumnName = "documentid";
        Dcom.AllowDBNull = true;
        Dcom.Unique = false;
        Dcom.ReadOnly = false;

        DataColumn Dcom1 = new DataColumn();
        Dcom1.DataType = System.Type.GetType("System.String");
        Dcom1.ColumnName = "documenttitle";
        Dcom1.AllowDBNull = true;
        Dcom1.Unique = false;
        Dcom1.ReadOnly = false;

        DataColumn Dcom2 = new DataColumn();
        Dcom2.DataType = System.Type.GetType("System.String");
        Dcom2.ColumnName = "documenttype";
        Dcom2.AllowDBNull = true;
        Dcom2.Unique = false;
        Dcom2.ReadOnly = false;

        DataColumn Dcom3 = new DataColumn();
        Dcom3.DataType = System.Type.GetType("System.String");
        Dcom3.ColumnName = "date";
        Dcom3.AllowDBNull = true;
        Dcom3.Unique = false;
        Dcom3.ReadOnly = false;

        DataColumn Dcom4 = new DataColumn();
        Dcom4.DataType = System.Type.GetType("System.String");
        Dcom4.ColumnName = "action";
        Dcom4.AllowDBNull = true;
        Dcom4.Unique = false;
        Dcom4.ReadOnly = false;

        DataColumn Dcom5 = new DataColumn();
        Dcom5.DataType = System.Type.GetType("System.String");
        Dcom5.ColumnName = "employee";
        Dcom5.AllowDBNull = true;
        Dcom5.Unique = false;
        Dcom5.ReadOnly = false;

        dt.Columns.Add(Dcom);
        dt.Columns.Add(Dcom1);
        dt.Columns.Add(Dcom2);
        dt.Columns.Add(Dcom3);
        dt.Columns.Add(Dcom4);
        dt.Columns.Add(Dcom5);
        return dt;
    }

    //_______________________Harshal Desai 30 - 03 - 2009

    public DataTable CreateDatatableForLogReport()
    {
        DataTable dt = new DataTable();
        DataColumn Dcom = new DataColumn();
        Dcom.DataType = System.Type.GetType("System.String");
        Dcom.ColumnName = "documentid";
        Dcom.AllowDBNull = true;
        Dcom.Unique = false;
        Dcom.ReadOnly = false;

        DataColumn Dcom1 = new DataColumn();
        Dcom1.DataType = System.Type.GetType("System.String");
        Dcom1.ColumnName = "documenttitle";
        Dcom1.AllowDBNull = true;
        Dcom1.Unique = false;
        Dcom1.ReadOnly = false;

        DataColumn Dcom2 = new DataColumn();
        Dcom2.DataType = System.Type.GetType("System.String");
        Dcom2.ColumnName = "documenttype";
        Dcom2.AllowDBNull = true;
        Dcom2.Unique = false;
        Dcom2.ReadOnly = false;

        DataColumn Dcom3 = new DataColumn();
        Dcom3.DataType = System.Type.GetType("System.String");
        Dcom3.ColumnName = "date";
        Dcom3.AllowDBNull = true;
        Dcom3.Unique = false;
        Dcom3.ReadOnly = false;

        DataColumn Dcom4 = new DataColumn();
        Dcom4.DataType = System.Type.GetType("System.String");
        Dcom4.ColumnName = "action";
        Dcom4.AllowDBNull = true;
        Dcom4.Unique = false;
        Dcom4.ReadOnly = false;

        DataColumn Dcom5 = new DataColumn();
        Dcom5.DataType = System.Type.GetType("System.String");
        Dcom5.ColumnName = "employee";
        Dcom5.AllowDBNull = true;
        Dcom5.Unique = false;
        Dcom5.ReadOnly = false;

        DataColumn Dcom6 = new DataColumn();
        Dcom6.DataType = System.Type.GetType("System.String");
        Dcom6.ColumnName = "FromDate";
        Dcom6.AllowDBNull = true;
        Dcom6.Unique = false;
        Dcom6.ReadOnly = false;


        DataColumn Dcom7 = new DataColumn();
        Dcom7.DataType = System.Type.GetType("System.String");
        Dcom7.ColumnName = "ToDate";
        Dcom7.AllowDBNull = true;
        Dcom7.Unique = false;
        Dcom7.ReadOnly = false;

        dt.Columns.Add(Dcom);
        dt.Columns.Add(Dcom1);
        dt.Columns.Add(Dcom2);
        dt.Columns.Add(Dcom3);
        dt.Columns.Add(Dcom4);
        dt.Columns.Add(Dcom5);
        dt.Columns.Add(Dcom6);
        dt.Columns.Add(Dcom7);
        return dt;
    }

    //____________________________________________________


    public DataTable SelectDoucmentMasterByIDForSearch(int DocumentTypeId, String DocumentId,String Whid)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectDoucmentMasterByIDForSearch";

        cmd.Parameters.Add(new SqlParameter("@DocumentTypeId", SqlDbType.Int));
        cmd.Parameters["@DocumentTypeId"].Value = DocumentTypeId;
        cmd.Parameters.Add(new SqlParameter("@Whid", SqlDbType.NVarChar));
        cmd.Parameters["@Whid"].Value = Whid;
        cmd.Parameters.Add(new SqlParameter("@DocumentId", SqlDbType.NVarChar));
        cmd.Parameters["@DocumentId"].Value = DocumentId;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;
    }
   
    public DataTable SelectDoucmentMasterByPartyIDForSearch(int PartyID, int DocumentId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectDoucmentMasterByPartyIDForSearch";

        cmd.Parameters.Add(new SqlParameter("@PartyId", SqlDbType.Int));
        cmd.Parameters["@PartyId"].Value = PartyID;
        cmd.Parameters.Add(new SqlParameter("@DocumentId", SqlDbType.Int));
        cmd.Parameters["@DocumentId"].Value = DocumentId;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["CompanyLoginId"].ToString(); // CompanyLoginId;
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }
    public DataTable SelectDoucmentTypeforPartyIDforTree(int PartyID)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectDoucmentTypeforPartyIDforTree";
        cmd.Parameters.Add(new SqlParameter("@PartyId", SqlDbType.Int));
        cmd.Parameters["@PartyId"].Value = PartyID;        
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["CompanyLoginId"].ToString(); // CompanyLoginId;
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }

    public DataTable SelectDoucmentMasterByTitle(int DocumentTypeId, string DocumentTitle)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectDoucmentMasterByTitle";

        cmd.Parameters.Add(new SqlParameter("@DocumentTypeId", SqlDbType.Int));
        cmd.Parameters["@DocumentTypeId"].Value = DocumentTypeId;
        cmd.Parameters.Add(new SqlParameter("@DocumentTitle", SqlDbType.NVarChar));
        cmd.Parameters["@DocumentTitle"].Value = DocumentTitle;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;
    }
    public DataTable SelectDoucmentMasterByTitleByDate(int DocumentTypeId, string DocumentTitle, DateTime startdate, DateTime enddate, String DateType)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectDoucmentMasterByTitleByDate";

        cmd.Parameters.Add(new SqlParameter("@DocumentTypeId", SqlDbType.Int));
        cmd.Parameters["@DocumentTypeId"].Value = DocumentTypeId;
        cmd.Parameters.Add(new SqlParameter("@DocumentTitle", SqlDbType.NVarChar));
        cmd.Parameters["@DocumentTitle"].Value = DocumentTitle;
        cmd.Parameters.Add(new SqlParameter("@startdate", SqlDbType.DateTime));
        cmd.Parameters["@startdate"].Value = startdate;
        cmd.Parameters.Add(new SqlParameter("@enddate", SqlDbType.DateTime));
        cmd.Parameters["@enddate"].Value = enddate;
        cmd.Parameters.Add(new SqlParameter("@DateType", SqlDbType.VarChar));
        cmd.Parameters["@DateType"].Value = DateType;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;
    }
    public DataTable SelectDoucmentMasterByPartyIDforTitleSearch(int PartyId, string DocumentTitle)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectDoucmentMasterByPartyIDforTitleSearch";

        cmd.Parameters.Add(new SqlParameter("@PartyId", SqlDbType.Int));
        cmd.Parameters["@PartyId"].Value = PartyId;
        cmd.Parameters.Add(new SqlParameter("@DocumentTitle", SqlDbType.NVarChar));
        cmd.Parameters["@DocumentTitle"].Value = DocumentTitle;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["CompanyLoginId"].ToString(); // CompanyLoginId;
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }

    //=========== alkesh 25-02-2009==================================

    public DataTable SelectDocumentApprovalType()
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectDocumentApprovalType";

        dt = DatabaseCls1.FillepAdapter(cmd);
        return dt;
    }

    public bool InsertDocumentEmpApproveLog(int DocumentApproveTypeId, int DocumentId, int EmployeeId, bool Approve, string note)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "InsertDocumentEmpApproveLog";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@DocumentId", SqlDbType.Int));
        cmd.Parameters["@DocumentId"].Value = DocumentId;
        cmd.Parameters.Add(new SqlParameter("@EmployeeId", SqlDbType.Int));
        cmd.Parameters["@EmployeeId"].Value = EmployeeId;
        cmd.Parameters.Add(new SqlParameter("@DocumentApproveTypeId", SqlDbType.Int));
        cmd.Parameters["@DocumentApproveTypeId"].Value = DocumentApproveTypeId;
        cmd.Parameters.Add(new SqlParameter("@Approve", SqlDbType.Bit));
        cmd.Parameters["@Approve"].Value = Approve;
        cmd.Parameters.Add(new SqlParameter("@note", SqlDbType.VarChar));
        cmd.Parameters["@note"].Value = note;



        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        int result = DatabaseCls1.ExecuteNonQueryep(cmd);
        return (result != -1);

    }

    public DataTable SelectDocumentEmpApproveLogByDocId(int DocumentId, int EmployeeID)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectDocumentEmpApproveLogByDocId";

        cmd.Parameters.Add(new SqlParameter("@DocumentId", SqlDbType.Int));
        cmd.Parameters["@DocumentId"].Value = DocumentId;
        cmd.Parameters.Add(new SqlParameter("@EmployeeID", SqlDbType.Int));
        cmd.Parameters["@EmployeeID"].Value = EmployeeID;
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;
    }

    public DataTable SelectDoucmentMasterByDocTypeSearch(int DocumentTypeSearch , int DocumentTypeId , int mainid, int Subid)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectDoucmentMasterByDocTypeSearch";


        cmd.Parameters.Add(new SqlParameter("@DocumentMainTypeId", SqlDbType.Int));
        cmd.Parameters["@DocumentMainTypeId"].Value = mainid;
        cmd.Parameters.Add(new SqlParameter("@DocumentSubTypeId", SqlDbType.Int));
        cmd.Parameters["@DocumentSubTypeId"].Value = Subid;


        cmd.Parameters.Add(new SqlParameter("@DocumentTypeId", SqlDbType.Int));
        cmd.Parameters["@DocumentTypeId"].Value = DocumentTypeId;
        cmd.Parameters.Add(new SqlParameter("@DocumentTypeSearch", SqlDbType.Int));
        cmd.Parameters["@DocumentTypeSearch"].Value = DocumentTypeSearch;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;
    }
    public DataTable SelectDoucmentMasterByDocTypeSearchByDate(int DocumentTypeSearch, int DocumentTypeId, DateTime startdate, DateTime enddate, int mainid, int Subid, String DateType)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectDoucmentMasterByDocTypeSearchByDate";

        cmd.Parameters.Add(new SqlParameter("@DocumentTypeId", SqlDbType.Int));
        cmd.Parameters["@DocumentTypeId"].Value = DocumentTypeId;
        cmd.Parameters.Add(new SqlParameter("@DocumentTypeSearch", SqlDbType.Int));
        cmd.Parameters["@DocumentTypeSearch"].Value = DocumentTypeSearch;
        cmd.Parameters.Add(new SqlParameter("@startdate", SqlDbType.DateTime));
        cmd.Parameters["@startdate"].Value = startdate;

        cmd.Parameters.Add(new SqlParameter("@DocumentMainTypeId", SqlDbType.Int));
        cmd.Parameters["@DocumentMainTypeId"].Value = mainid;
        cmd.Parameters.Add(new SqlParameter("@DocumentSubTypeId", SqlDbType.Int));
        cmd.Parameters["@DocumentSubTypeId"].Value = Subid;

        cmd.Parameters.Add(new SqlParameter("@enddate", SqlDbType.DateTime));
        cmd.Parameters["@enddate"].Value = enddate;
        cmd.Parameters.Add(new SqlParameter("@DateType", SqlDbType.VarChar));
        cmd.Parameters["@DateType"].Value = DateType;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;
    }
    public DataTable SelectDoucmentMasterByPartyIdforDocTypeSearch(int PartyId, int DocumentTypeSearch)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectDoucmentMasterByPartyIdforDocTypeSearch";

        cmd.Parameters.Add(new SqlParameter("@PartyId", SqlDbType.Int));
        cmd.Parameters["@PartyId"].Value = PartyId;
        cmd.Parameters.Add(new SqlParameter("@DocumentTypeSearch", SqlDbType.Int));
        cmd.Parameters["@DocumentTypeSearch"].Value = DocumentTypeSearch;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["CompanyLoginId"].ToString(); // CompanyLoginId;
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }

    public DataTable SelectDoucmentMasterByParty(int DocumentTypeId, int PartyId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectDoucmentMasterByParty";

        cmd.Parameters.Add(new SqlParameter("@DocumentTypeId", SqlDbType.Int));
        cmd.Parameters["@DocumentTypeId"].Value = DocumentTypeId;
        cmd.Parameters.Add(new SqlParameter("@PartyId", SqlDbType.Int));
        cmd.Parameters["@PartyId"].Value = PartyId;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;
    }
    public DataTable SelectDoucmentMasterByPartyByDate(int DocumentTypeId, int PartyId, DateTime startdate, DateTime enddate, String DateType)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectDoucmentMasterByPartyByDate";

        cmd.Parameters.Add(new SqlParameter("@DocumentTypeId", SqlDbType.Int));
        cmd.Parameters["@DocumentTypeId"].Value = DocumentTypeId;
        cmd.Parameters.Add(new SqlParameter("@PartyId", SqlDbType.Int));
        cmd.Parameters["@PartyId"].Value = PartyId;
        cmd.Parameters.Add(new SqlParameter("@startdate", SqlDbType.DateTime));
        cmd.Parameters["@startdate"].Value = startdate;
        cmd.Parameters.Add(new SqlParameter("@enddate", SqlDbType.DateTime));
        cmd.Parameters["@enddate"].Value = enddate;
        cmd.Parameters.Add(new SqlParameter("@DateType", SqlDbType.VarChar));
        cmd.Parameters["@DateType"].Value = DateType;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;
    }
    // Neetu 25-02-2009

    public DataTable SelectDocumentforMyApprovalbyStatus(bool Approve,Int32 EmployeeID,String Whid)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectDocumentforMyApprovalbyStatus";
        cmd.Parameters.Add(new SqlParameter("@EmployeeId", SqlDbType.Int));
        cmd.Parameters["@EmployeeId"].Value = EmployeeID; // EmployeeId;
        cmd.Parameters.Add(new SqlParameter("@Approve", SqlDbType.Bit));
        cmd.Parameters["@Approve"].Value = Approve; // Convert.ToInt32(HttpContext.Current.Session["Approve"].ToString()); // EmployeeId;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        cmd.Parameters.Add(new SqlParameter("@Whid", SqlDbType.NVarChar));
        cmd.Parameters["@Whid"].Value = Whid; // CompanyLoginId;
    
        dt = DatabaseCls1.FilleppAdapter(cmd); //.FillAdapter(cmd);
        return dt;

    }
    //---------------- haiyal 2-3-2009-----------------------//
    public Int32 InsertDocumentApproveTypeMaster(String DocumentApproveType,String Desc,String Whid)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "InsertDocumentApproveTypeMaster";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@RuleApproveTypeName", SqlDbType.NVarChar));
        cmd.Parameters["@RuleApproveTypeName"].Value = DocumentApproveType;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        cmd.Parameters.Add(new SqlParameter("@Description", SqlDbType.NVarChar));
        cmd.Parameters["@Description"].Value = Desc;
        cmd.Parameters.Add(new SqlParameter("@Whid", SqlDbType.NVarChar));
        cmd.Parameters["@Whid"].Value = Whid;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls1.ExecuteNonQueryep(cmd);
        return result;

    }

    //public DataTable SelectDocumentApproveType()
    //{
    //    cmd = new SqlCommand();
    //    dt = new DataTable();
    //    cmd.CommandText = "SelectDocumentApproveType";
    //    dt = DatabaseCls1.FillAdapter(cmd);
    //    return dt;
    //}

    public bool UpdateDocumentApproveTypeMaster(Int32 RuleApproveTypeId, String RuleApproveTypeName,String Desc,String Whid)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "UpdateDocumentApproveTypeMaster";
        cmd.CommandType = CommandType.StoredProcedure;
        //cmd.Parameters.Add(new SqlParameter("@DocumentApproveTypeId", SqlDbType.Int));
        //cmd.Parameters["@DocumentApproveTypeId"].Value = DocumentApproveTypeId;
        //cmd.Parameters.Add(new SqlParameter("@DocumentApproveType", SqlDbType.NVarChar));
        //cmd.Parameters["@DocumentApproveType"].Value = DocumentApproveType;
        cmd.Parameters.Add(new SqlParameter("@RuleApproveTypeId", SqlDbType.Int));
        cmd.Parameters["@RuleApproveTypeId"].Value = RuleApproveTypeId;
        cmd.Parameters.Add(new SqlParameter("@RuleApproveTypeName", SqlDbType.NVarChar));
        cmd.Parameters["@RuleApproveTypeName"].Value = RuleApproveTypeName;

        cmd.Parameters.Add(new SqlParameter("@Description", SqlDbType.NVarChar));
        cmd.Parameters["@Description"].Value = Desc;

        cmd.Parameters.Add(new SqlParameter("@Whid", SqlDbType.NVarChar));
        cmd.Parameters["@Whid"].Value = Whid;


        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        int result = DatabaseCls1.ExecuteNonQueryep(cmd);
        return (result != -1);
    }
    public bool UpdateEmployeeTypeMaster(Int32 EmployeeTypeId, String EmployeeTypeName)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "UpdateEmployeeTypeMaster";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@EmployeeTypeId", SqlDbType.Int));
        cmd.Parameters["@EmployeeTypeId"].Value = EmployeeTypeId;
        cmd.Parameters.Add(new SqlParameter("@EmployeeTypeName", SqlDbType.NVarChar));
        cmd.Parameters["@EmployeeTypeName"].Value = EmployeeTypeName;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        int result = DatabaseCls1.ExecuteNonQuery(cmd);
        return (result != -1);
    }
    public bool UpdateEmployeeAddressTypeMaster(Int32 EmployeeAddressTypeId, String EmployeeAddressTypeName)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "UpdateEmployeeAddressTypeMaster";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@EmployeeAddressTypeId", SqlDbType.Int));
        cmd.Parameters["@EmployeeAddressTypeId"].Value = EmployeeAddressTypeId;
        cmd.Parameters.Add(new SqlParameter("@EmployeeAddressTypeName", SqlDbType.NVarChar));
        cmd.Parameters["@EmployeeAddressTypeName"].Value = EmployeeAddressTypeName;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        int result = DatabaseCls1.ExecuteNonQuery(cmd);
        return (result != -1);
    }
    public bool UpdatePartyTypeMaster(Int32 PartyTypeId, String PartyTypeName)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "UpdatePartyTypeMaster";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@PartyTypeId", SqlDbType.Int));
        cmd.Parameters["@PartyTypeId"].Value = PartyTypeId;
        cmd.Parameters.Add(new SqlParameter("@PartyTypeName", SqlDbType.NVarChar));
        cmd.Parameters["@PartyTypeName"].Value = PartyTypeName;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        int result = DatabaseCls1.ExecuteNonQuery(cmd);
        return (result != -1);
    }
    public bool UpdatePartyAddressTypeMaster(Int32 PartyAddressTypeId, String PartyAddressTypeName)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "UpdatePartyAddressTypeMaster";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@PartyAddressTypeId", SqlDbType.Int));
        cmd.Parameters["@PartyAddressTypeId"].Value = PartyAddressTypeId;
        cmd.Parameters.Add(new SqlParameter("@PartyAddressTypeName", SqlDbType.NVarChar));
        cmd.Parameters["@PartyAddressTypeName"].Value = PartyAddressTypeName;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        int result = DatabaseCls1.ExecuteNonQuery(cmd);
        return (result != -1);
    }
    //---------------- haiyal 3-3-2009-----------------------//

    public DataTable SelectDocumentViewToUser(Int32 DocumentId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectDocumentViewToUser";
        cmd.Parameters.Add(new SqlParameter("@DocumentId", SqlDbType.Int));
        cmd.Parameters["@DocumentId"].Value = DocumentId;
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;
    }


    //---------------- haiyal 4-3-2009-----------------------//
    public DataTable SelectDocumentViewApproveByEmp(Int32 DesignationId, Int32 EmployeeID)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectDocumentViewApproveByEmp";
        cmd.Parameters.Add(new SqlParameter("@DesignationId", SqlDbType.Int));
        cmd.Parameters["@DesignationId"].Value = DesignationId;
        cmd.Parameters.Add(new SqlParameter("@EmployeeID", SqlDbType.Int));
        cmd.Parameters["@EmployeeID"].Value = EmployeeID;
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }
    public DataTable SelectDoucmentMasterByDocumentTypeID_se(Int32 EmployeeId, DateTime startdate, DateTime enddate,Boolean Approve,String Whid,int flag)
    {   
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectDoucmentMasterByDocumentTypeID_se";
        cmd.Parameters.Add(new SqlParameter("@EmployeeId", SqlDbType.Int));
        cmd.Parameters["@EmployeeId"].Value = EmployeeId;
        //cmd.Parameters.Add(new SqlParameter("@DocumentTypeId", SqlDbType.Int));
        //cmd.Parameters["@DocumentTypeId"].Value = DocumentTypeId;
        cmd.Parameters.Add(new SqlParameter("@startdate", SqlDbType.DateTime));
        cmd.Parameters["@startdate"].Value = startdate;
        cmd.Parameters.Add(new SqlParameter("@enddate", SqlDbType.DateTime));
        cmd.Parameters["@enddate"].Value = enddate;
        cmd.Parameters.Add(new SqlParameter("@approve", SqlDbType.Bit));
        cmd.Parameters["@approve"].Value = Approve;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        cmd.Parameters.Add(new SqlParameter("@Whid", SqlDbType.NVarChar));
        cmd.Parameters["@Whid"].Value = Whid; // CompanyLoginId;
        cmd.Parameters.Add(new SqlParameter("@flag", SqlDbType.NVarChar));
        cmd.Parameters["@flag"].Value = flag; // CompanyLoginId;
        
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;
    }
    public DataTable selectEmployeeWebsiteTrackDetail(Int32 EmployeeID, DateTime startdate, DateTime enddate)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "selectEmployeeWebsiteTrackDetail";
        //cmd.Parameters.Add(new SqlParameter("@PartyId", SqlDbType.Int));
        //cmd.Parameters["@PartyId"].Value = PartyId;
        cmd.Parameters.Add(new SqlParameter("@EmployeeID", SqlDbType.Int));
        cmd.Parameters["@EmployeeID"].Value = EmployeeID;
        //cmd.Parameters.Add(new SqlParameter("@DocumentTypeId", SqlDbType.Int));
        //cmd.Parameters["@DocumentTypeId"].Value = DocumentTypeId;
        cmd.Parameters.Add(new SqlParameter("@startdate", SqlDbType.DateTime));
        cmd.Parameters["@startdate"].Value = startdate;
        cmd.Parameters.Add(new SqlParameter("@enddate", SqlDbType.DateTime));
        cmd.Parameters["@enddate"].Value = enddate;        
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["CompanyLoginId"].ToString(); // CompanyLoginId;
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }
    public DataTable SelectDoucmentMasterByText_se(int Empid, String  Searchtext, DateTime startdate, DateTime enddate, Boolean Approve,String Whid, int flag)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectDoucmentMasterByText_se";

        cmd.Parameters.Add(new SqlParameter("@EmployeeId", SqlDbType.Int));
        cmd.Parameters["@EmployeeId"].Value = Empid;
        cmd.Parameters.Add(new SqlParameter("@TextDetail", SqlDbType.NVarChar));
        cmd.Parameters["@TextDetail"].Value = Searchtext;
        cmd.Parameters.Add(new SqlParameter("@startdate", SqlDbType.DateTime));
        cmd.Parameters["@startdate"].Value = startdate;
        cmd.Parameters.Add(new SqlParameter("@enddate", SqlDbType.DateTime));
        cmd.Parameters["@enddate"].Value = enddate;
        cmd.Parameters.Add(new SqlParameter("@approve", SqlDbType.Bit));
        cmd.Parameters["@approve"].Value = Approve;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        cmd.Parameters.Add(new SqlParameter("@Whid", SqlDbType.NVarChar));
        cmd.Parameters["@Whid"].Value = Whid; // CompanyLoginId;
        cmd.Parameters.Add(new SqlParameter("@flag", SqlDbType.Int));
        cmd.Parameters["@flag"].Value = flag; // CompanyLoginId;
        
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;
    }
    ////////   haiyal ----------------------------------------------
    public DataTable SelectDEO(String Whid)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectDEO";
        cmd.Parameters.Add(new SqlParameter("@Whid", SqlDbType.NVarChar));
        cmd.Parameters["@Whid"].Value = Whid; // CompanyLoginId;
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;
    }
    public DataTable SelectSupervisor(String Whid)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectSupervisor";
        cmd.Parameters.Add(new SqlParameter("@Whid", SqlDbType.NVarChar));
        cmd.Parameters["@Whid"].Value = Whid; // CompanyLoginId;
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;
    }
    public DataTable SelectMANAGER()
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectMANAGER";
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }
    //SelectDocumentAvailableApprovebyDEO
    //SelectDocumentAvailableNOTapproveBySupervisor
    //SelectDocumentWithDataEntOprANDSupervisor
    //SelectDocumentWithoutDataEntOprANDSupervisor
    public DataTable SelectDocumentWithDataEntOprANDSupervisor(Int32 SupervisorId, Int32 DataEntryOperatorId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectDocumentWithDataEntOprANDSupervisor";

        cmd.Parameters.Add(new SqlParameter("@SupervisorId", SqlDbType.NVarChar));
        cmd.Parameters["@SupervisorId"].Value = SupervisorId;

        cmd.Parameters.Add(new SqlParameter("@DataEntryOperatorId", SqlDbType.NVarChar));
        cmd.Parameters["@DataEntryOperatorId"].Value = DataEntryOperatorId;

        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }
    public DataTable SelectDocumentWithoutDataEntOprANDSupervisor()
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectDocumentWithoutDataEntOprANDSupervisor";
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }

    public DataTable SelectDocumentAvailableApprovebyDEO(Int32 DataEntryOperatorId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectDocumentAvailableApprovebyDEO";

        cmd.Parameters.Add(new SqlParameter("@DataEntryOperatorId", SqlDbType.Int));
        cmd.Parameters["@DataEntryOperatorId"].Value = DataEntryOperatorId;

        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }
    //[SelectDocumentAvailableApproveBySupervisor]
    //[SelectDocumentAvailableNOTapproveByMng]
    public DataTable SelectDocumentAvailableNOTapproveBySupervisor(Int32 SupervisorId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectDocumentAvailableNOTapproveBySupervisor";

        cmd.Parameters.Add(new SqlParameter("@SupervisorId", SqlDbType.Int));
        cmd.Parameters["@SupervisorId"].Value = SupervisorId;

        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }
    public DataTable SelectDocumentAvailableApproveBySupervisor(Int32 SupervisorId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectDocumentAvailableApproveBySupervisor";

        cmd.Parameters.Add(new SqlParameter("@SupervisorId", SqlDbType.Int));
        cmd.Parameters["@SupervisorId"].Value = SupervisorId;

        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }

    public DataTable SelectDocumentAvailableNOTapproveByMng(Int32 MngId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectDocumentAvailableNOTapproveByMng";

        cmd.Parameters.Add(new SqlParameter("@MngId", SqlDbType.Int));
        cmd.Parameters["@MngId"].Value = MngId;

        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }
    public bool UpdateDocumentProcessing(Int32 DocumentProcessingId, Boolean Approve, DateTime ApproveDate, String Note)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "UpdateDocumentProcessing";
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.Add(new SqlParameter("@DocumentProcessingId", SqlDbType.Int));
        cmd.Parameters["@DocumentProcessingId"].Value = DocumentProcessingId;

        cmd.Parameters.Add(new SqlParameter("@Approve", SqlDbType.Bit));
        cmd.Parameters["@Approve"].Value = Approve;

        cmd.Parameters.Add(new SqlParameter("@ApproveDate", SqlDbType.DateTime));
        cmd.Parameters["@ApproveDate"].Value = ApproveDate;

        cmd.Parameters.Add(new SqlParameter("@Note", SqlDbType.NVarChar));
        cmd.Parameters["@Note"].Value = Note;

        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        int result = DatabaseCls1.ExecuteNonQuery(cmd);
        return (result != -1);
    }

    // Neetu 17-3-09
    public Int32 InsertDocumentViewRuleMaster(Int32 DesId, Int32 RuleSelectId, DateTime FromDate, DateTime ToDate,
      Int32 FromId, Int32 ToId,String Whid)
    {


        cmd = new SqlCommand();
        cmd.CommandText = "InsertDocumentViewRuleMaster";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@DesId", SqlDbType.Int));
        cmd.Parameters["@DesId"].Value = DesId;
        cmd.Parameters.Add(new SqlParameter("@RuleSelectId", SqlDbType.Int));
        cmd.Parameters["@RuleSelectId"].Value = RuleSelectId;
        cmd.Parameters.Add(new SqlParameter("@FromDate", SqlDbType.DateTime));
        cmd.Parameters["@FromDate"].Value = FromDate;
        cmd.Parameters.Add(new SqlParameter("@ToDate", SqlDbType.DateTime));
        cmd.Parameters["@ToDate"].Value = ToDate;
        cmd.Parameters.Add(new SqlParameter("@FromId", SqlDbType.Int));
        cmd.Parameters["@FromId"].Value = FromId;
        cmd.Parameters.Add(new SqlParameter("@ToId", SqlDbType.Int));
        cmd.Parameters["@ToId"].Value = ToId;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        cmd.Parameters.Add(new SqlParameter("@Whid", SqlDbType.NVarChar));
        cmd.Parameters["@Whid"].Value = Whid;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls1.ExecuteNonQueryep(cmd);
        result = Int32.Parse(cmd.Parameters["@ReturnValue"].Value.ToString());
        return result;
    }

    public DataTable SelectDocumentViewRuleMasterDesIdWise(Int32 DesId,String Whid)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectDocumentViewRuleMasterDesIdWise";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@DesId", SqlDbType.Int));
        cmd.Parameters["@DesId"].Value = DesId;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
       cmd.Parameters.Add(new SqlParameter("@Whid", SqlDbType.NVarChar));
        cmd.Parameters["@Whid"].Value = Whid; // CompanyLoginId;
      
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;
    }
    public DataTable SelectDocumentViewRuleMaster(String Whid)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectDocumentViewRuleMaster";
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString();
        cmd.Parameters.Add(new SqlParameter("@Whid", SqlDbType.NVarChar));
        cmd.Parameters["@Whid"].Value =Whid;// CompanyLoginId;
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;
    }



    public DataTable SelectDocumentforMyApprovalIdWiseStatus(bool Approve, Int32 FromId, Int32 ToId,Int32 EmployeeId,String Whid)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectDocumentforMyApprovalIdWiseStatus";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@EmployeeId", SqlDbType.Int));
        cmd.Parameters["@EmployeeId"].Value = EmployeeId; // EmployeeId;
        cmd.Parameters.Add(new SqlParameter("@Approve", SqlDbType.Bit));
        cmd.Parameters["@Approve"].Value = Approve; ;
        cmd.Parameters.Add(new SqlParameter("@FromId", SqlDbType.Int));
        cmd.Parameters["@FromId"].Value = FromId;
        cmd.Parameters.Add(new SqlParameter("@ToId", SqlDbType.Int));
        cmd.Parameters["@ToId"].Value = ToId;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        cmd.Parameters.Add(new SqlParameter("@Whid", SqlDbType.NVarChar));
        cmd.Parameters["@Whid"].Value = Whid; // CompanyLoginId;


        dt = DatabaseCls1.FilleppAdapter(cmd); //.FillAdapter(cmd);
        return dt;

    }
    public DataTable SelectDocumentforMyApprovalDateWisebyStatus(bool Approve, DateTime FromDate, DateTime ToDate,Int32 EmployeeId,String Whid)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectDocumentforMyApprovalDateWisebyStatus";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@EmployeeId", SqlDbType.Int));
        cmd.Parameters["@EmployeeId"].Value = EmployeeId; // EmployeeId;
        cmd.Parameters.Add(new SqlParameter("@Approve", SqlDbType.Bit));
        cmd.Parameters["@Approve"].Value = Approve; ;
        cmd.Parameters.Add(new SqlParameter("@FromDate", SqlDbType.DateTime));
        cmd.Parameters["@FromDate"].Value = FromDate;
        cmd.Parameters.Add(new SqlParameter("@ToDate", SqlDbType.DateTime));
        cmd.Parameters["@ToDate"].Value = ToDate;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        cmd.Parameters.Add(new SqlParameter("@Whid", SqlDbType.NVarChar));
        cmd.Parameters["@Whid"].Value = Whid; // CompanyLoginId;
     
        
        dt = DatabaseCls1.FilleppAdapter(cmd); //.FillAdapter(cmd);
        return dt;

    }

    public DataTable SelectDocumentforMyApprovalDateWise(DateTime FromDate, DateTime ToDate,Int32 EmployeeId,String Whid)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectDocumentforMyApprovalDateWise";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@EmployeeId", SqlDbType.Int));
        cmd.Parameters["@EmployeeId"].Value = EmployeeId; // EmployeeId;
        cmd.Parameters.Add(new SqlParameter("@FromDate", SqlDbType.DateTime));
        cmd.Parameters["@FromDate"].Value = FromDate;
        cmd.Parameters.Add(new SqlParameter("@ToDate", SqlDbType.DateTime));
        cmd.Parameters["@ToDate"].Value = ToDate;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        cmd.Parameters.Add(new SqlParameter("@Whid", SqlDbType.NVarChar));
        cmd.Parameters["@Whid"].Value = Whid; // CompanyLoginId;


        dt = DatabaseCls1.FilleppAdapter(cmd); //.FillAdapter(cmd);
        return dt;

    }
    public DataTable SelectDocumentforMyApprovalIdWise(Int32 FromId, Int32 ToId,Int32 EmployeeId,String Whid)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectDocumentforMyApprovalIdWise";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@EmployeeId", SqlDbType.Int));
        cmd.Parameters["@EmployeeId"].Value = EmployeeId; // EmployeeId;
        cmd.Parameters.Add(new SqlParameter("@FromId", SqlDbType.Int));
        cmd.Parameters["@FromId"].Value = FromId;
        cmd.Parameters.Add(new SqlParameter("@ToId", SqlDbType.Int));
        cmd.Parameters["@ToId"].Value = ToId;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        cmd.Parameters.Add(new SqlParameter("@Whid", SqlDbType.NVarChar));
        cmd.Parameters["@Whid"].Value = Whid; // CompanyLoginId;


        dt = DatabaseCls1.FilleppAdapter(cmd); //.FillAdapter(cmd);
        return dt;

    }


    public bool DeleteDocumentViewRuleMasterDesIdWise(Int32 DesId,String Whid)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "DeleteDocumentViewRuleMasterDesIdWise";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@DesId", SqlDbType.Int));
        cmd.Parameters["@DesId"].Value = DesId;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        cmd.Parameters.Add(new SqlParameter("@Whid", SqlDbType.NVarChar));
        cmd.Parameters["@Whid"].Value = Whid;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        int result = DatabaseCls1.ExecuteNonQueryep(cmd);
        return (result != -1);
    }


    //================= alkesh on 11-03-2009 for document relationship=============

    public Int32 InsertDocumentFolder(String DocumentFolder, Int32 EmployeeId)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "InsertDocumentFolder";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@DocumentFolder", SqlDbType.NVarChar));
        cmd.Parameters["@DocumentFolder"].Value = DocumentFolder;
        cmd.Parameters.Add(new SqlParameter("@EmployeeId", SqlDbType.Int));
        cmd.Parameters["@EmployeeId"].Value = EmployeeId;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls1.ExecuteNonQueryep(cmd);
        return result;
    }

    public DataTable SelectDocumentFolderByEmpId(Int32 EmployeeId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectDocumentFolderByEmpId";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@EmployeeId", SqlDbType.Int));
        cmd.Parameters["@EmployeeId"].Value = EmployeeId;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;
    }

    public bool UpdateDocumentFolder(Int32 FolderID, String FolderName)
    {
        cmd = new SqlCommand();

        cmd.CommandText = "UpdateDocumentFolder";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@FolderID", SqlDbType.Int));
        cmd.Parameters["@FolderID"].Value = FolderID;
        cmd.Parameters.Add(new SqlParameter("@FolderName", SqlDbType.NVarChar));
        cmd.Parameters["@FolderName"].Value = FolderName;

        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        int result = DatabaseCls1.ExecuteNonQueryep(cmd);
        return (result != -1);
    }

    public DataTable SelectDoucmentMasterByIdAll(int DocumentId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectDoucmentMasterByIdAll";
        cmd.Parameters.Add(new SqlParameter("@DocumentId", SqlDbType.Int));
        cmd.Parameters["@DocumentId"].Value = DocumentId;
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;
    }

    public DataTable SelectDoucmentTotalInFolder(int FolderID)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectDoucmentTotalInFolder";
        cmd.Parameters.Add(new SqlParameter("@FolderID", SqlDbType.Int));
        cmd.Parameters["@FolderID"].Value = FolderID;
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;
    }

    public DataTable SelectDoucmentRelationByFolderId(int FolderID)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectDoucmentRelationByFolderId";
        cmd.Parameters.Add(new SqlParameter("@FolderID", SqlDbType.Int));
        cmd.Parameters["@FolderID"].Value = FolderID;
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;
    }

    public Int32 InsertDoucmentRelationMaster(Int32 DocumentId, Int32 FolderID)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "InsertDoucmentRelationMaster";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@FolderID", SqlDbType.NVarChar));
        cmd.Parameters["@FolderID"].Value = FolderID;
        cmd.Parameters.Add(new SqlParameter("@DocumentId", SqlDbType.Int));
        cmd.Parameters["@DocumentId"].Value = DocumentId;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls1.ExecuteNonQueryep(cmd);
        return result;
    }
    //  alkesh 13-03-2009
    public DataTable SelectDoucmentFolderRelation(int FolderID, Int32 DocumentId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectDoucmentFolderRelation";
        cmd.Parameters.Add(new SqlParameter("@FolderID", SqlDbType.Int));
        cmd.Parameters["@FolderID"].Value = FolderID;
        cmd.Parameters.Add(new SqlParameter("@DocumentId", SqlDbType.Int));
        cmd.Parameters["@DocumentId"].Value = DocumentId;
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;
    }
    // alkesh 16-03-2009

    public bool DeleteDocumentFolderRelation(Int32 RelationID)
    {
        cmd = new SqlCommand();

        cmd.CommandText = "DeleteDocumentFolderRelation";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@RelationID", SqlDbType.Int));
        cmd.Parameters["@RelationID"].Value = RelationID;

        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        int result = DatabaseCls1.ExecuteNonQueryep(cmd);
        return (result != -1);
    }
    public bool DeleteDocumentFolderMaster(Int32 FolderID)
    {
        cmd = new SqlCommand();

        cmd.CommandText = "DeleteDocumentFolderMaster";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@FolderID", SqlDbType.Int));
        cmd.Parameters["@FolderID"].Value = FolderID;

        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        int result = DatabaseCls1.ExecuteNonQueryep(cmd);
        return (result != -1);
    }
    public bool DeleteSpamListByID(Int32 ID)
    {
        cmd = new SqlCommand();

        cmd.CommandText = "DeleteSpamListByID";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int));
        cmd.Parameters["@ID"].Value = ID;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["CompanyLoginId"].ToString(); // CompanyLoginId;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        int result = DatabaseCls1.ExecuteNonQueryep(cmd);
        return (result != -1);
    }
    // alkesh 17-03-2009

    public DataTable SelectDocumentFolderTotalByDocument(int DocumentId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectDocumentFolderTotalByDocument";
        cmd.Parameters.Add(new SqlParameter("@DocumentId", SqlDbType.Int));
        cmd.Parameters["@DocumentId"].Value = DocumentId;
        cmd.Parameters.Add(new SqlParameter("@EmployeeId", SqlDbType.Int));
        cmd.Parameters["@EmployeeId"].Value = HttpContext.Current.Session["EmployeeId"].ToString(); 
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;
    }

    // alkesh 18-03-2009

    public DataTable SelectDocumentFolderByDocumentId(int DocumentId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectDocumentFolderByDocumentId";
        cmd.Parameters.Add(new SqlParameter("@DocumentId", SqlDbType.Int));
        cmd.Parameters["@DocumentId"].Value = DocumentId;
        cmd.Parameters.Add(new SqlParameter("@EmployeeId", SqlDbType.Int));
        cmd.Parameters["@EmployeeId"].Value = HttpContext.Current.Session["EmployeeId"].ToString(); 
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;
    }
    

    public DataTable SelectDocumentMasterByDocumentTypeIDFolder(int DocumentTypeId, int FolderID)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectDocumentMasterByDocumentTypeIDFolder";
        cmd.Parameters.Add(new SqlParameter("@DocumentTypeId", SqlDbType.Int));
        cmd.Parameters["@DocumentTypeId"].Value = DocumentTypeId;
        cmd.Parameters.Add(new SqlParameter("@FolderID", SqlDbType.Int));
        cmd.Parameters["@FolderID"].Value = FolderID;
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;
    }

    public DataTable SelectDocumentFolderByFolderID(int FolderID)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectDocumentFolderByFolderID";
        cmd.Parameters.Add(new SqlParameter("@FolderID", SqlDbType.Int));
        cmd.Parameters["@FolderID"].Value = FolderID;
        cmd.Parameters.Add(new SqlParameter("@EmployeeId", SqlDbType.Int));
        cmd.Parameters["@EmployeeId"].Value = HttpContext.Current.Session["EmployeeId"].ToString(); // CompanyLoginId;
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;
    }

    //================== alkesh 19-03-2009

    public DataTable SelectDocTypeAll(string Whid)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectDocTypeAll";
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        cmd.Parameters.Add(new SqlParameter("@Whid", SqlDbType.NVarChar));
        cmd.Parameters["@Whid"].Value = Whid; // CompanyLoginId;
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;
    }
    public DataTable SelectPublicDocTypeAll(String Whid)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectPublicDocTypeAll";
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        cmd.Parameters.Add(new SqlParameter("@Whid", SqlDbType.NVarChar));
        cmd.Parameters["@Whid"].Value = Whid; // CompanyLoginId;
       
        
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;
    }

    public DataTable SelectPartyDetail()
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectPartyDetail";
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;
    }
    //============alkesh 23-03-2008
    //public Int32 InsertFTPMaster(string FTP, string Username, string Password)
    //{
    //    cmd = new SqlCommand();
    //    cmd.CommandText = "InsertFTPMaster";
    //    cmd.CommandType = CommandType.StoredProcedure;
    //    cmd.Parameters.Add(new SqlParameter("@FTP", SqlDbType.NVarChar));
    //    cmd.Parameters["@FTP"].Value = FTP;
    //    cmd.Parameters.Add(new SqlParameter("@Username", SqlDbType.NVarChar));
    //    cmd.Parameters["@Username"].Value = Username;
    //    cmd.Parameters.Add(new SqlParameter("@Password", SqlDbType.NVarChar));
    //    cmd.Parameters["@Password"].Value = Password;
    //    cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
    //    cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
    //    Int32 result = DatabaseCls1.ExecuteNonQuery(cmd);
    //    return result;
    //}
    public DataTable SelectFtpMaster(String Whid)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectFtpMaster";
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString();
        cmd.Parameters.Add(new SqlParameter("@Whid", SqlDbType.NVarChar));
        cmd.Parameters["@Whid"].Value = Whid;
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;
    }

    //  ======================= alkesh 24-03-2009

    ////public Int32 InsertDocumentEmailDownloadMaster(string ServerName, string EmailId, string Password)
    ////{
    ////    cmd = new SqlCommand();
    ////    cmd.CommandText = "InsertDocumentEmailDownloadMaster";
    ////    cmd.CommandType = CommandType.StoredProcedure;
    ////    cmd.Parameters.Add(new SqlParameter("@ServerName", SqlDbType.NVarChar));
    ////    cmd.Parameters["@ServerName"].Value = ServerName;
    ////    cmd.Parameters.Add(new SqlParameter("@EmailId", SqlDbType.NVarChar));
    ////    cmd.Parameters["@EmailId"].Value = EmailId;
    ////    cmd.Parameters.Add(new SqlParameter("@Password", SqlDbType.NVarChar));
    ////    cmd.Parameters["@Password"].Value = Password;
    ////    cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
    ////    cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
    ////    Int32 result = DatabaseCls1.ExecuteNonQuery(cmd);
    ////    return result;
    ////}

    public DataTable SelectDocumentEmailDownloadMaster(String Whid)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectDocumentEmailDownloadMaster";
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        cmd.Parameters.Add(new SqlParameter("@Whid", SqlDbType.NVarChar));
        cmd.Parameters["@Whid"].Value = Whid; // CompanyLoginId;
       
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;
    }
    public DataTable SelectInOutCompanyEmailMaster()
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectInOutCompanyEmailMaster";
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["CompanyLoginId"].ToString(); // CompanyLoginId;
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }
    public DataTable SelectDefaultOutGoingEmailServer()
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectDefaultOutGoingEmailServer";
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["CompanyLoginId"].ToString(); // CompanyLoginId;
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }
    public DataTable SelectInOutCompanyEmailMasterbyID(Int32 ID)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectInOutCompanyEmailMasterbyID";
        cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int));
        cmd.Parameters["@ID"].Value = ID;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["CompanyLoginId"].ToString(); // CompanyLoginId;
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }

    public bool UpdateDocumentEmailDownloadMaster(Int32 DocumentEmailDownloadID, String ServerName, String EmailId, String Password)
    {
        cmd = new SqlCommand();

        cmd.CommandText = "UpdateDocumentEmailDownloadMaster";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@DocumentEmailDownloadID", SqlDbType.Int));
        cmd.Parameters["@DocumentEmailDownloadID"].Value = DocumentEmailDownloadID;
        cmd.Parameters.Add(new SqlParameter("@ServerName", SqlDbType.NVarChar));
        cmd.Parameters["@ServerName"].Value = ServerName;
        cmd.Parameters.Add(new SqlParameter("@EmailId", SqlDbType.NVarChar));
        cmd.Parameters["@EmailId"].Value = EmailId;
        cmd.Parameters.Add(new SqlParameter("@Password", SqlDbType.NVarChar));
        cmd.Parameters["@Password"].Value = Password;

        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        int result = DatabaseCls1.ExecuteNonQuery(cmd);
        return (result != -1);
    }
    //===================\     alkesh 25-03-2008

    public bool UpdateDocumentEmailLastDownloadIndex(Int32 DocumentEmailDownloadID, int LastDownloadIndex)
    {
        cmd = new SqlCommand();

        cmd.CommandText = "UpdateDocumentEmailLastDownloadIndex";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@DocumentEmailDownloadID", SqlDbType.Int));
        cmd.Parameters["@DocumentEmailDownloadID"].Value = DocumentEmailDownloadID;
        cmd.Parameters.Add(new SqlParameter("@LastDownloadIndex", SqlDbType.Int));
        cmd.Parameters["@LastDownloadIndex"].Value = LastDownloadIndex;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        int result = DatabaseCls1.ExecuteNonQuery(cmd);
        return (result != -1);
    }
    public bool UpdateDocumentEmailLastDownloadTime(Int32 DocumentEmailDownloadID, DateTime LastDownloadTime)
    {
        cmd = new SqlCommand();

        cmd.CommandText = "UpdateDocumentEmailLastDownloadTime";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@DocumentEmailDownloadID", SqlDbType.Int));
        cmd.Parameters["@DocumentEmailDownloadID"].Value = DocumentEmailDownloadID;
        cmd.Parameters.Add(new SqlParameter("@LastDownloadTime", SqlDbType.Int));
        cmd.Parameters["@LastDownloadTime"].Value = LastDownloadTime;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        int result = DatabaseCls1.ExecuteNonQuery(cmd);
        return (result != -1);
    }
    public bool UpdateEmpEmailLastDownloadIndex(Int32 EmployeeId, int LastDownloadIndex, DateTime LastDownloadTime)
    {
        cmd = new SqlCommand();

        cmd.CommandText = "UpdateEmpEmailLastDownloadIndex";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@EmployeeId", SqlDbType.Int));
        cmd.Parameters["@EmployeeId"].Value = EmployeeId;
        cmd.Parameters.Add(new SqlParameter("@LastDownloadIndex", SqlDbType.Int));
        cmd.Parameters["@LastDownloadIndex"].Value = LastDownloadIndex;
        cmd.Parameters.Add(new SqlParameter("@LastDownloadTime", SqlDbType.DateTime));
        cmd.Parameters["@LastDownloadTime"].Value = LastDownloadTime;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        int result = DatabaseCls1.ExecuteNonQueryep(cmd);
        return (result != -1);
    }
    public bool UpdateCompanyEmailLastDownloadIndex(Int32 ID, int LastDownloadIndex, DateTime LastDownloadedTime)
    {
        cmd = new SqlCommand();

        cmd.CommandText = "UpdateCompanyEmailLastDownloadIndex";
        cmd.CommandType = CommandType.StoredProcedure;
        //cmd.Parameters.Add(new SqlParameter("@PartyId", SqlDbType.Int));
        //cmd.Parameters["@PartyId"].Value = PartyId;
        cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int));
        cmd.Parameters["@ID"].Value = ID;
        cmd.Parameters.Add(new SqlParameter("@LastDownloadIndex", SqlDbType.Int));
        cmd.Parameters["@LastDownloadIndex"].Value = LastDownloadIndex;
        cmd.Parameters.Add(new SqlParameter("@LastDownloadedTime", SqlDbType.DateTime));
        cmd.Parameters["@LastDownloadedTime"].Value = LastDownloadedTime;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        int result = DatabaseCls1.ExecuteNonQuery(cmd);
        return (result != -1);
    }
    public DataTable SelectDocumentAvailableApprovebyAllDEO_NotbyAllSup(String Whid, Boolean DeoApprove, Boolean SupApprove)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectDocumentAvailableApprovebyAllDEO_NotbyAllSup";
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString();
        cmd.Parameters.Add(new SqlParameter("@Whid", SqlDbType.NVarChar));
        cmd.Parameters["@Whid"].Value = Whid;// CompanyLoginId;
        cmd.Parameters.Add(new SqlParameter("@DeoApprove", SqlDbType.Int));
        cmd.Parameters["@DeoApprove"].Value = DeoApprove;
        cmd.Parameters.Add(new SqlParameter("@SupApprove", SqlDbType.Int));
        cmd.Parameters["@SupApprove"].Value = SupApprove;
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;
    }
    public DataTable SelectDocumentAvailableApprovebyDEO_NotbyAllSup(Int32 DataEntryOperatorId,Boolean Approve)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectDocumentAvailableApprovebyDEO_NotbyAllSup";
        cmd.Parameters.Add(new SqlParameter("@DataEntryOperatorId", SqlDbType.Int));
        cmd.Parameters["@DataEntryOperatorId"].Value = DataEntryOperatorId;
        cmd.Parameters.Add(new SqlParameter("@approve", SqlDbType.Int));
        cmd.Parameters["@approve"].Value = Approve;
       
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;
    }
    public DataTable SelectDocumentAvailableApprovebyAllDEO_NotbySup(Int32 SupervisorId,String Whid,bool app)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectDocumentAvailableApprovebyAllDEO_NotbySup";
        cmd.Parameters.Add(new SqlParameter("@SupervisorId", SqlDbType.Int));
        cmd.Parameters["@SupervisorId"].Value = SupervisorId;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        cmd.Parameters.Add(new SqlParameter("@Whid", SqlDbType.NVarChar));
        cmd.Parameters["@Whid"].Value = Whid; // CompanyLoginId;

        cmd.Parameters.Add(new SqlParameter("@Approve", SqlDbType.Bit));
        cmd.Parameters["@Approve"].Value = app; // CompanyLoginId;
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;
    }
    public DataTable SelectDocumentAvailableApprovebyDEO_NotbySup(Int32 DataEntryOperatorId, Int32 SupervisorId, Boolean DeoApprove, Boolean SupApprove,String Whid)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectDocumentAvailableApprovebyDEO_NotbySup";
        cmd.Parameters.Add(new SqlParameter("@SupervisorId", SqlDbType.Int));
        cmd.Parameters["@SupervisorId"].Value = SupervisorId;
        cmd.Parameters.Add(new SqlParameter("@DataEntryOperatorId", SqlDbType.Int));
        cmd.Parameters["@DataEntryOperatorId"].Value = DataEntryOperatorId;
        cmd.Parameters.Add(new SqlParameter("@DeoApprove", SqlDbType.Int));
        cmd.Parameters["@DeoApprove"].Value = DeoApprove;
        cmd.Parameters.Add(new SqlParameter("@SupApprove", SqlDbType.Int));
        cmd.Parameters["@SupApprove"].Value = SupApprove;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        cmd.Parameters.Add(new SqlParameter("@Whid", SqlDbType.NVarChar));
        cmd.Parameters["@Whid"].Value = Whid; // CompanyLoginId;
       
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;
    }

    public DataTable SelectDocumentAvailableApprovebyAllDEO_AllSup_NotByMe(String Empid)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectDocumentAvailableApprovebyAllDEO_AllSup_NotByMe";
       
        cmd.Parameters.Add(new SqlParameter("@EmployeeId", SqlDbType.Int));
        cmd.Parameters["@EmployeeId"].Value = Convert.ToInt32(Empid); // EmployeeId;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;

        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;
    }
    public DataTable SelectDocumentAvailableApprovebyDEO_AllSup_NotByMe(Int32 DataEntryOperatorId, String Empid)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectDocumentAvailableApprovebyDEO_AllSup_NotByMe";
        
        cmd.Parameters.Add(new SqlParameter("@EmployeeId", SqlDbType.Int));
        cmd.Parameters["@EmployeeId"].Value = Convert.ToInt32(Empid); 
        cmd.Parameters.Add(new SqlParameter("@DataEntryOperatorId", SqlDbType.Int));
        cmd.Parameters["@DataEntryOperatorId"].Value = DataEntryOperatorId;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
     
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;
    }
    public DataTable SelectDocumentAvailableApprovebyAllDEO_Sup_NotByMe(Int32 SupervisorId, String Empid)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
                           
        cmd.CommandText = "SelectDocumentAvailableApprovebyAllDEO_Sup_NotByMe";
       
        cmd.Parameters.Add(new SqlParameter("@EmployeeId", SqlDbType.Int));
        cmd.Parameters["@EmployeeId"].Value = Convert.ToInt32(Empid); // EmployeeId;
        cmd.Parameters.Add(new SqlParameter("@SupervisorId", SqlDbType.Int));
        cmd.Parameters["@SupervisorId"].Value = SupervisorId;
         cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
     
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;
    }
    public DataTable SelectDocumentAvailableApprovebyDEO_Sup_NotByMe(Int32 DataEntryOperatorId, Int32 SupervisorId, String Empid)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectDocumentAvailableApprovebyDEO_Sup_NotByMe";
       
        cmd.Parameters.Add(new SqlParameter("@EmployeeId", SqlDbType.Int));
        cmd.Parameters["@EmployeeId"].Value = Convert.ToInt32(Empid); // EmployeeId;
        cmd.Parameters.Add(new SqlParameter("@SupervisorId", SqlDbType.Int));
        cmd.Parameters["@SupervisorId"].Value = SupervisorId;
        cmd.Parameters.Add(new SqlParameter("@DataEntryOperatorId", SqlDbType.Int));
        cmd.Parameters["@DataEntryOperatorId"].Value = DataEntryOperatorId;
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;
    }
    // Neetu 3_4_09

    public DataTable SelectProcessingDocumentbyDocIdwise(Int32 DocumentId)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "SelectProcessingDocumentbyDocIdwise";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@DocumentId", SqlDbType.Int));
        cmd.Parameters["@DocumentId"].Value = DocumentId;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        dt = new DataTable();
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;
    }


    public DataTable SelectProcessingDocumentbyDocTypeIdwise(Int32 DocumentTypeId, Int32 RuleId)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "SelectProcessingDocumentbyDocTypeIdwise";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@DocumentTypeId", SqlDbType.Int));
        cmd.Parameters["@DocumentTypeId"].Value = DocumentTypeId;
        cmd.Parameters.Add(new SqlParameter("@EmployeeId", SqlDbType.Int));
        cmd.Parameters["@EmployeeId"].Value = Convert.ToInt32(HttpContext.Current.Session["EmployeeId"].ToString()); // EmployeeId;
        cmd.Parameters.Add(new SqlParameter("@RuleId", SqlDbType.Int));
        cmd.Parameters["@RuleId"].Value = RuleId;
        // cmd.Parameters.Add(new SqlParameter("@StepId", SqlDbType.Int));
        // cmd.Parameters["@StepId"].Value = StepId;

        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        dt = new DataTable();
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }
    // Neetu 7-4-09
    public DataTable SelectDocumentforApprovalFirst()
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectDocumentforApprovalFirst";
        cmd.Parameters.Add(new SqlParameter("@EmployeeId", SqlDbType.Int));
        cmd.Parameters["@EmployeeId"].Value = Convert.ToInt32(HttpContext.Current.Session["EmployeeId"].ToString()); // EmployeeId;
        //  cmd.Parameters.Add(new SqlParameter("@EmployeeId", SqlDbType.Int));
        //cmd.Parameters["@EmployeeId"].Value = Convert.ToInt32(HttpContext.Current.Session["EmployeeId"].ToString()); // EmployeeId;

        dt = DatabaseCls1.FilleppAdapter(cmd); //.FillAdapter(cmd);
        return dt;
    }

    public DataTable SelectDocumentforApprovalLast()
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectDocumentforApprovalLast";
        cmd.Parameters.Add(new SqlParameter("@EmployeeId", SqlDbType.Int));
        cmd.Parameters["@EmployeeId"].Value = Convert.ToInt32(HttpContext.Current.Session["EmployeeId"].ToString()); // EmployeeId;
        //  cmd.Parameters.Add(new SqlParameter("@EmployeeId", SqlDbType.Int));
        //cmd.Parameters["@EmployeeId"].Value = Convert.ToInt32(HttpContext.Current.Session["EmployeeId"].ToString()); // EmployeeId;

        dt = DatabaseCls1.FilleppAdapter(cmd); //.FillAdapter(cmd);
        return dt;
    }
    public DataTable SelectDocumentforApprovalPrev(Int32 DocumentId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectDocumentforApprovalPrev";
        cmd.Parameters.Add(new SqlParameter("@EmployeeId", SqlDbType.Int));
        cmd.Parameters["@EmployeeId"].Value = Convert.ToInt32(HttpContext.Current.Session["EmployeeId"].ToString()); // EmployeeId;
        cmd.Parameters.Add(new SqlParameter("@DocumentId", SqlDbType.Int));
        cmd.Parameters["@DocumentId"].Value = DocumentId;
        dt = DatabaseCls1.FilleppAdapter(cmd); //.FillAdapter(cmd);
        return dt;
    }
    public DataTable SelectDocumentforApprovalNext(Int32 DocumentId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectDocumentforApprovalNext";
        cmd.Parameters.Add(new SqlParameter("@EmployeeId", SqlDbType.Int));
        cmd.Parameters["@EmployeeId"].Value = Convert.ToInt32(HttpContext.Current.Session["EmployeeId"].ToString()); // EmployeeId;
        cmd.Parameters.Add(new SqlParameter("@DocumentId", SqlDbType.Int));
        cmd.Parameters["@DocumentId"].Value = DocumentId;
        dt = DatabaseCls1.FilleppAdapter(cmd); //.FillAdapter(cmd);
        return dt;
    }
    // func again here for DcID as output
    public Int32 InsertDocumentMaster(Int32 DocumentTypeId, Int32 DocumentUploadTypeId, DateTime DocumentUploadDate, String DocumentName, String DocumentTitle, String Description, Int32 PartyId, String DocumentRefNo, Decimal DocumentAmount, Int32 EmployeeId, DateTime DocumentDate, String FileExtensionType, String Doct, String PartDocrefno) //  remove from this line on 27/5 ninad
    {
        cmd = new SqlCommand();
        cmd.CommandText = "InsertDocumentMaster";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@DocumentTypeId", SqlDbType.Int));
        cmd.Parameters["@DocumentTypeId"].Value = DocumentTypeId;
        cmd.Parameters.Add(new SqlParameter("@DocumentUploadTypeId", SqlDbType.Int));
        cmd.Parameters["@DocumentUploadTypeId"].Value = DocumentUploadTypeId;
        cmd.Parameters.Add(new SqlParameter("@DocumentUploadDate", SqlDbType.DateTime));
        cmd.Parameters["@DocumentUploadDate"].Value = DocumentUploadDate;
        cmd.Parameters.Add(new SqlParameter("@DocumentName", SqlDbType.NVarChar));
        cmd.Parameters["@DocumentName"].Value = DocumentName;
        cmd.Parameters.Add(new SqlParameter("@DocumentTitle", SqlDbType.NVarChar));
        cmd.Parameters["@DocumentTitle"].Value = DocumentTitle;
        cmd.Parameters.Add(new SqlParameter("@Description", SqlDbType.NVarChar));
        cmd.Parameters["@Description"].Value = Description;
        cmd.Parameters.Add(new SqlParameter("@PartyId", SqlDbType.Int));
        cmd.Parameters["@PartyId"].Value = PartyId;
        cmd.Parameters.Add(new SqlParameter("@DocumentRefNo", SqlDbType.NVarChar));
        cmd.Parameters["@DocumentRefNo"].Value = DocumentRefNo;
        cmd.Parameters.Add(new SqlParameter("@DocumentAmount", SqlDbType.Decimal));
        cmd.Parameters["@DocumentAmount"].Value = DocumentAmount;
        cmd.Parameters.Add(new SqlParameter("@EmployeeId", SqlDbType.Int));
        cmd.Parameters["@EmployeeId"].Value = EmployeeId;
        cmd.Parameters.Add(new SqlParameter("@DocumentDate", SqlDbType.DateTime));
        cmd.Parameters["@DocumentDate"].Value = DocumentDate;
        cmd.Parameters.Add(new SqlParameter("@FileExtensionType", SqlDbType.NVarChar));
        cmd.Parameters["@FileExtensionType"].Value = FileExtensionType;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;

        cmd.Parameters.Add(new SqlParameter("@DocumentTypenmId", SqlDbType.NVarChar));
        cmd.Parameters["@DocumentTypenmId"].Value = Doct;

        cmd.Parameters.Add(new SqlParameter("@PartyDocrefno", SqlDbType.NVarChar));
        cmd.Parameters["@PartyDocrefno"].Value = PartDocrefno; 



        cmd.Parameters.Add(new SqlParameter("@DocumentId", SqlDbType.Int));
        cmd.Parameters["@DocumentId"].Direction = ParameterDirection.Output;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls1.ExecuteNonQueryep(cmd);
        result = Convert.ToInt32(cmd.Parameters["@DocumentId"].Value);
        return (result);
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
            //HttpContext.Current.Session["EmployeeId"] = Int32.Parse(cmdd.Parameters["@EmployeeId"].Value.ToString()); //EmployeeId;
           // HttpContext.Current.Session["DesignationId"] = Int32.Parse(cmdd.Parameters["@DesignationId"].Value.ToString()); //DesignationId;
           // HttpContext.Current.Session["PartyId"] = Int32.Parse(cmdd.Parameters["@PartyId"].Value.ToString()); //
          //  HttpContext.Current.Session["PartyId1"] = Int32.Parse(cmdd.Parameters["@PartyId"].Value.ToString());
          //  HttpContext.Current.Session["EmployeeName"] = cmdd.Parameters["@EmployeeName"].Value.ToString(); //EmployeeId;


        }
        else
        {
           // HttpContext.Current.Session["EmployeeId"] = 0;
         //   HttpContext.Current.Session["DesignationId"] = 0;
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
           // HttpContext.Current.Session["DesignationId"] = Int32.Parse(cmdd.Parameters["@DesignationId"].Value.ToString()); //DesignationId;
           // HttpContext.Current.Session["PartyId"] = Int32.Parse(cmdd.Parameters["@PartyId"].Value.ToString()); //
            //HttpContext.Current.Session["PartyId1"] = Int32.Parse(cmdd.Parameters["@PartyId"].Value.ToString());
           // HttpContext.Current.Session["EmployeeName"] = cmdd.Parameters["@EmployeeName"].Value.ToString(); //EmployeeId;


        }
        else
        {
          //  HttpContext.Current.Session["EmployeeId"] = 0;
           // HttpContext.Current.Session["DesignationId"] = 0;
        }
        return rtn;
    }

    public Int32 InsertAssociateAtachmentMaster(string Datetime, string AssociatesiteId, string RelatedtablemasterId, String RelatedtableId, String IfilecabinetdocId, String OAentrytypeId, String OAentryno, String Entrydate)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "InsertAssociateAtachmentMaster";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@Datetime", SqlDbType.NVarChar));
        cmd.Parameters["@Datetime"].Value = Datetime;
        cmd.Parameters.Add(new SqlParameter("@AssociatesiteId", SqlDbType.NVarChar));
        cmd.Parameters["@AssociatesiteId"].Value = AssociatesiteId;
        cmd.Parameters.Add(new SqlParameter("@RelatedtablemasterId", SqlDbType.NVarChar));
        cmd.Parameters["@RelatedtablemasterId"].Value = RelatedtablemasterId;
        cmd.Parameters.Add(new SqlParameter("@RelatedtableId", SqlDbType.NVarChar));
        cmd.Parameters["@RelatedtableId"].Value = RelatedtableId;
        cmd.Parameters.Add(new SqlParameter("@IfilecabinetdocId", SqlDbType.NVarChar));
        cmd.Parameters["@IfilecabinetdocId"].Value = IfilecabinetdocId;
        cmd.Parameters.Add(new SqlParameter("@OAentrytypeId", SqlDbType.NVarChar));
        cmd.Parameters["@OAentrytypeId"].Value = OAentrytypeId;
        cmd.Parameters.Add(new SqlParameter("@OAentryno", SqlDbType.Int));
        cmd.Parameters["@OAentryno"].Value = OAentryno;
        cmd.Parameters.Add(new SqlParameter("@Entrydate", SqlDbType.NVarChar));
        cmd.Parameters["@Entrydate"].Value = Entrydate;

        cmd.Parameters.Add(new SqlParameter("@assoid", SqlDbType.Int));
        cmd.Parameters["@assoid"].Direction = ParameterDirection.Output;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls1.ExecuteNonQuery(cmd);
        result = Convert.ToInt32(cmd.Parameters["@assoid"].Value);
        return (result);
    }
    public Int32 InsertAttachmentmaster(string titalname, string filename, DateTime DocumentUploadDate, String Relatedtablmasterid, String relatedtableId, int docid)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "InsertAttachmentMaster";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@Titlename", SqlDbType.NVarChar));
        cmd.Parameters["@Titlename"].Value = titalname;
        cmd.Parameters.Add(new SqlParameter("@Filename", SqlDbType.NVarChar));
        cmd.Parameters["@Filename"].Value = filename;

        cmd.Parameters.Add(new SqlParameter("@Datetime", SqlDbType.DateTime));
        cmd.Parameters["@Datetime"].Value = DocumentUploadDate;
        cmd.Parameters.Add(new SqlParameter("@RelatedtablemasterId", SqlDbType.NVarChar));
        cmd.Parameters["@RelatedtablemasterId"].Value = Relatedtablmasterid;
        cmd.Parameters.Add(new SqlParameter("@RelatedTableId", SqlDbType.NVarChar));
        cmd.Parameters["@RelatedTableId"].Value = relatedtableId;
               cmd.Parameters.Add(new SqlParameter("@IfilecabinetDocId", SqlDbType.NVarChar));
               cmd.Parameters["@IfilecabinetDocId"].Value = docid;
               cmd.Parameters.Add(new SqlParameter("@Attachment", SqlDbType.Int));
               cmd.Parameters["@Attachment"].Direction = ParameterDirection.Output;

        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls1.ExecuteNonQueryep(cmd);
        result = Convert.ToInt32(cmd.Parameters["@Attachment"].Value);
        return (result);
    }
    
    public bool UpdateDocumentDateDetail(Int32 DocumentId, DateTime DocumentDate)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "UpdateDocumentDateDetail";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@DocumentId", SqlDbType.Int));
        cmd.Parameters["@DocumentId"].Value = DocumentId;
        cmd.Parameters.Add(new SqlParameter("@DocumentDate", SqlDbType.DateTime));
        cmd.Parameters["@DocumentDate"].Value = DocumentDate;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        int result = DatabaseCls1.ExecuteNonQueryep(cmd);
        return (result != -1);
    }

    public DataTable SelectDocumentDateDetail(Int32 DocumentId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectDocumentDateDetail";
        cmd.Parameters.Add(new SqlParameter("@DocumentId", SqlDbType.Int));
        cmd.Parameters["@DocumentId"].Value = DocumentId;

        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }

    public DataTable SelectDoucmentMasterByDocumentTypeIDwithDocDate(int DocumentTypeId, DateTime startdate, DateTime enddate)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectDoucmentMasterByDocumentTypeIDwithDocDate";
        cmd.Parameters.Add(new SqlParameter("@DocumentTypeId", SqlDbType.Int));
        cmd.Parameters["@DocumentTypeId"].Value = DocumentTypeId;
        cmd.Parameters.Add(new SqlParameter("@startdate", SqlDbType.DateTime));
        cmd.Parameters["@startdate"].Value = startdate;
        cmd.Parameters.Add(new SqlParameter("@enddate", SqlDbType.DateTime));
        cmd.Parameters["@enddate"].Value = enddate;
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }

    public DataTable SelectDocumentMainTypebyId(int DocumentMainTypeId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectDocumentMainTypebyId";
        cmd.Parameters.Add(new SqlParameter("@DocumentMainTypeId", SqlDbType.Int));
        cmd.Parameters["@DocumentMainTypeId"].Value = DocumentMainTypeId;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;

        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;
    }

    public DataTable SelectDocumentSubTypebyId(int DocumentSubTypeid)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectDocumentSubTypebyId";
        cmd.Parameters.Add(new SqlParameter("@DocumentSubTypeid", SqlDbType.Int));
        cmd.Parameters["@DocumentSubTypeid"].Value = DocumentSubTypeid;

        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }

    //,

    public bool UpdateDocumentSubType_SubTypeName(Int32 DocumentSubTypeId, String DocumentSubType)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "UpdateDocumentSubType_SubTypeName";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@DocumentSubTypeId", SqlDbType.Int));
        cmd.Parameters["@DocumentSubTypeId"].Value = DocumentSubTypeId;
        //cmd.Parameters.Add(new SqlParameter("@DocumentMainTypeId", SqlDbType.Int));
        //cmd.Parameters["@DocumentMainTypeId"].Value = DocumentMainTypeId;
        cmd.Parameters.Add(new SqlParameter("@DocumentSubType", SqlDbType.NVarChar));
        cmd.Parameters["@DocumentSubType"].Value = DocumentSubType;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        int result = DatabaseCls1.ExecuteNonQueryep(cmd);
        return (result != -1);
    }
    public bool UpdateDocumentSubType_MainTypeId(Int32 DocumentSubTypeId, Int32 DocumentMainTypeId)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "UpdateDocumentSubType_MainTypeId";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@DocumentSubTypeId", SqlDbType.Int));
        cmd.Parameters["@DocumentSubTypeId"].Value = DocumentSubTypeId;
        cmd.Parameters.Add(new SqlParameter("@DocumentMainTypeId", SqlDbType.Int));
        cmd.Parameters["@DocumentMainTypeId"].Value = DocumentMainTypeId;
        // cmd.Parameters.Add(new SqlParameter("@DocumentSubType", SqlDbType.NVarChar));
        // cmd.Parameters["@DocumentSubType"].Value = DocumentSubType;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        int result = DatabaseCls1.ExecuteNonQueryep(cmd);
        return (result != -1);
    }
    public bool UpdateDocumentType_SubId(Int32 DocumentTypeId, Int32 DocumentSubTypeId)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "UpdateDocumentType_SubId";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@DocumentTypeId", SqlDbType.Int));
        cmd.Parameters["@DocumentTypeId"].Value = DocumentTypeId;
        cmd.Parameters.Add(new SqlParameter("@DocumentSubTypeId", SqlDbType.Int));
        cmd.Parameters["@DocumentSubTypeId"].Value = DocumentSubTypeId;

        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        int result = DatabaseCls1.ExecuteNonQuery(cmd);
        return (result != -1);
    }
    public bool UpdateDocumentType_DocTypeName(Int32 DocumentTypeId, String DocumentType)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "UpdateDocumentType_DocTypeName";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@DocumentTypeId", SqlDbType.Int));
        cmd.Parameters["@DocumentTypeId"].Value = DocumentTypeId;

        cmd.Parameters.Add(new SqlParameter("@DocumentType", SqlDbType.NVarChar));
        cmd.Parameters["@DocumentType"].Value = DocumentType;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        int result = DatabaseCls1.ExecuteNonQuery(cmd);
        return (result != -1);
    }
    public DataTable SelectDocumentTypeMainSubName()
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectDocumentTypeMainSubName";
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;
    }

    public DataTable SelectDocumentTypebyId(Int32 DocumentTypeId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectDocumentTypebyId";

        cmd.Parameters.Add(new SqlParameter("@DocumentTypeId", SqlDbType.Int));
        cmd.Parameters["@DocumentTypeId"].Value = DocumentTypeId;
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }

    public DataTable SelectDocumentSubTypebyMainType(Int32 DocumentMainTypeId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectDocumentSubTypebyMainType";

        cmd.Parameters.Add(new SqlParameter("@DocumentMainTypeId", SqlDbType.Int));
        cmd.Parameters["@DocumentMainTypeId"].Value = DocumentMainTypeId;
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;
    }

    public bool DeleteDocumentMainTypebyId(Int32 DocumentMainTypeId)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "DeleteDocumentMainTypebyId";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@DocumentMainTypeId", SqlDbType.Int));
        cmd.Parameters["@DocumentMainTypeId"].Value = DocumentMainTypeId;


        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        int result = DatabaseCls1.ExecuteNonQueryep(cmd);
        return (result != -1);
    }

    public bool DeleteDocumentSubTypebyId(Int32 DocumentSubTypeId)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "DeleteDocumentSubTypebyId";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@DocumentSubTypeId", SqlDbType.Int));
        cmd.Parameters["@DocumentSubTypeId"].Value = DocumentSubTypeId;


        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        int result = DatabaseCls1.ExecuteNonQuery(cmd);
        return (result != -1);
    }
    public bool DeleteDepartmentByID(Int32 DepartmentId)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "DeleteDepartmentByID";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@DepartmentId", SqlDbType.Int));
        cmd.Parameters["@DepartmentId"].Value = DepartmentId;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["CompanyLoginId"].ToString(); // CompanyLoginId;

        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        int result = DatabaseCls1.ExecuteNonQuery(cmd);
        return (result != -1);
    }
    public bool DeleteDesignationByID(Int32 DesignationId)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "DeleteDesignationByID";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@DesignationId", SqlDbType.Int));
        cmd.Parameters["@DesignationId"].Value = DesignationId;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["CompanyLoginId"].ToString(); // CompanyLoginId;

        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        int result = DatabaseCls1.ExecuteNonQuery(cmd);
        return (result != -1);
    }
    public bool DeleteRuleApproveTypeMasterByID(Int32 RuleApproveTypeId)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "DeleteRuleApproveTypeMasterByID";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@RuleApproveTypeId", SqlDbType.Int));
        cmd.Parameters["@RuleApproveTypeId"].Value = RuleApproveTypeId;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;

        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        int result = DatabaseCls1.ExecuteNonQueryep(cmd);
        return (result != -1);
    }

    public DataTable SelectDocumentMasterByDocumentTypeIDforDelete(Int32 DocumentTypeId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectDocumentMasterByDocumentTypeIDforDelete";
        cmd.Parameters.Add(new SqlParameter("@DocumentTypeId", SqlDbType.Int));
        cmd.Parameters["@DocumentTypeId"].Value = DocumentTypeId;
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;
    }


    public bool UpdateDocumentMaster_DocTypeId(Int32 DocumentId, Int32 DocumentTypeId)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "UpdateDocumentMaster_DocTypeId";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@DocumentId", SqlDbType.Int));
        cmd.Parameters["@DocumentId"].Value = DocumentId;
        cmd.Parameters.Add(new SqlParameter("@DocumentTypeId", SqlDbType.Int));
        cmd.Parameters["@DocumentTypeId"].Value = DocumentTypeId;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        int result = DatabaseCls1.ExecuteNonQuery(cmd);
        return (result != -1);
    }


    public bool DeleteDocumentTypebyId(Int32 DocumentTypeId)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "DeleteDocumentTypebyId";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@DocumentTypeId", SqlDbType.Int));
        cmd.Parameters["@DocumentTypeId"].Value = DocumentTypeId;


        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        int result = DatabaseCls1.ExecuteNonQueryep(cmd);
        return (result != -1);
    }
    // 13-5-09
    public DataTable SelectDocumentMasterByTypeId(int DocumentTypeId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectDocumentMasterByTypeId";

        cmd.Parameters.Add(new SqlParameter("@DocumentTypeId", SqlDbType.Int));
        cmd.Parameters["@DocumentTypeId"].Value = DocumentTypeId;

        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;
    }

    public DataTable SelectDocumentforApprovalWithDocumentProcessingId(Int32 DocumentProcessingId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectDocumentforApprovalWithDocumentProcessingId";

        cmd.Parameters.Add(new SqlParameter("@DocumentProcessingId", SqlDbType.Int));
        cmd.Parameters["@DocumentProcessingId"].Value = DocumentProcessingId;

        cmd.Parameters.Add(new SqlParameter("@EmployeeId", SqlDbType.Int));
        cmd.Parameters["@EmployeeId"].Value = Convert.ToInt32(HttpContext.Current.Session["EmployeeId"].ToString()); // EmployeeId;
        dt = DatabaseCls1.FillAdapter(cmd); //.FillAdapter(cmd);
        return dt;

    }

    ////////////////------------------haiyal 12-5-2009----------------------

    public Int32 InsertFTPMaster(string FTP, string Username, string Password, string Ftppath, bool DocumentAutoApprove,String RuleType,String Whid,String Autoinv)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "InsertFTPMaster";
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.Add(new SqlParameter("@FTP", SqlDbType.NVarChar));
        cmd.Parameters["@FTP"].Value = FTP;
        cmd.Parameters.Add(new SqlParameter("@Username", SqlDbType.NVarChar));
        cmd.Parameters["@Username"].Value = Username;
        cmd.Parameters.Add(new SqlParameter("@Password", SqlDbType.NVarChar));
        cmd.Parameters["@Password"].Value = Password;
        cmd.Parameters.Add(new SqlParameter("@Ftppath", SqlDbType.NVarChar));
        cmd.Parameters["@Ftppath"].Value = Ftppath;
        cmd.Parameters.Add(new SqlParameter("@DocumentAutoApprove", SqlDbType.Bit));
        cmd.Parameters["@DocumentAutoApprove"].Value = DocumentAutoApprove;
        cmd.Parameters.Add(new SqlParameter("@Ruletype", SqlDbType.VarChar));
        cmd.Parameters["@Ruletype"].Value = RuleType;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        cmd.Parameters.Add(new SqlParameter("@Whid", SqlDbType.NVarChar));
        cmd.Parameters["@Whid"].Value = Whid;
        cmd.Parameters.Add(new SqlParameter("@AutoRetrival", SqlDbType.NVarChar));
        cmd.Parameters["@AutoRetrival"].Value = Autoinv;
        cmd.Parameters.Add(new SqlParameter("@FTPId", SqlDbType.Int));
        cmd.Parameters["@FTPId"].Direction = ParameterDirection.Output;

        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;

        Int32 result = DatabaseCls1.ExecuteNonQueryep(cmd);
        result = Convert.ToInt32(cmd.Parameters["@FTPId"].Value);
        return (result);
    }

    public DataTable SelectFTPMasterWithID(Int32 FTPId)
    {

        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectFTPMasterWithID";
        cmd.Parameters.Add(new SqlParameter("@FTPId", SqlDbType.Int));
        cmd.Parameters["@FTPId"].Value = FTPId;
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;

    }
    public DataTable SelectDownloadFolderIdwise(Int32 FolderId)
    {

        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectDownloadFolderIdwise";
        cmd.Parameters.Add(new SqlParameter("@FolderId", SqlDbType.Int));
        cmd.Parameters["@FolderId"].Value = FolderId;
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;

    }

    public bool UpdateFTPMaster(Int32 FtpId, String FTP, String Ftppath, String Username, String Password, Boolean DocumentAutoApprove, String RuleType, String Whid,String AutoRetrival)
    {
        cmd = new SqlCommand();

        cmd.CommandText = "UpdateFTPMaster";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@FtpId", SqlDbType.Int));
        cmd.Parameters["@FtpId"].Value = FtpId;
        cmd.Parameters.Add(new SqlParameter("@FTP", SqlDbType.NVarChar));
        cmd.Parameters["@FTP"].Value = FTP;
        cmd.Parameters.Add(new SqlParameter("@Ftppath", SqlDbType.NVarChar));
        cmd.Parameters["@Ftppath"].Value = Ftppath;

        cmd.Parameters.Add(new SqlParameter("@Username", SqlDbType.NVarChar));
        cmd.Parameters["@Username"].Value = Username;
        cmd.Parameters.Add(new SqlParameter("@Password", SqlDbType.NVarChar));
        cmd.Parameters["@Password"].Value = Password;


        cmd.Parameters.Add(new SqlParameter("@DocumentAutoApprove", SqlDbType.Bit));
        cmd.Parameters["@DocumentAutoApprove"].Value = DocumentAutoApprove;
        cmd.Parameters.Add(new SqlParameter("@RuleType", SqlDbType.VarChar));
        cmd.Parameters["@RuleType"].Value = RuleType;
        cmd.Parameters.Add(new SqlParameter("@Whid", SqlDbType.VarChar));
        cmd.Parameters["@Whid"].Value = Whid;
        cmd.Parameters.Add(new SqlParameter("@AutoRetrival", SqlDbType.VarChar));
        cmd.Parameters["@AutoRetrival"].Value = AutoRetrival;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        int result = DatabaseCls1.ExecuteNonQueryep(cmd);
        return (result != -1);
    }
    public bool Updatesetuploadinterval(string uploadtype,Int32 intervaltime)
    {
        cmd = new SqlCommand();

        cmd.CommandText = "Updatesetuploadinterval";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@uploadtype", SqlDbType.VarChar));
        cmd.Parameters["@uploadtype"].Value = uploadtype;
        cmd.Parameters.Add(new SqlParameter("@intervaltime", SqlDbType.Int));
        cmd.Parameters["@intervaltime"].Value = intervaltime;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["CompanyLoginId"].ToString(); // CompanyLoginId;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        int result = DatabaseCls1.ExecuteNonQuery(cmd);
        return (result != -1);
    }
    public bool Updatesetuploadintervallasttime(string uploadtype, DateTime lastupdatetime)
    {
        cmd = new SqlCommand();

        cmd.CommandText = "Updatesetuploadintervallasttime";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@uploadtype", SqlDbType.VarChar));
        cmd.Parameters["@uploadtype"].Value = uploadtype;
        cmd.Parameters.Add(new SqlParameter("@lastupdatetime", SqlDbType.DateTime));
        cmd.Parameters["@lastupdatetime"].Value = lastupdatetime;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        int result = DatabaseCls1.ExecuteNonQueryep(cmd);
        return (result != -1);
    }
    public bool UpdateFTPMasterDefaultProp(Int32 FTPMasterDefaultPropId, String DocumentTitle,
                                Int32 DocumentTypeId, Int32 PartyId, String DocumentDescription, String Docyt)
    {
        cmd = new SqlCommand();

        cmd.CommandText = "UpdateFTPMasterDefaultProp";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@FTPMasterDefaultPropId", SqlDbType.Int));
        cmd.Parameters["@FTPMasterDefaultPropId"].Value = FTPMasterDefaultPropId;
        cmd.Parameters.Add(new SqlParameter("@DocumentTitle", SqlDbType.NVarChar));
        cmd.Parameters["@DocumentTitle"].Value = DocumentTitle;
        cmd.Parameters.Add(new SqlParameter("@DocumentTypeId", SqlDbType.Int));
        cmd.Parameters["@DocumentTypeId"].Value = DocumentTypeId;
        cmd.Parameters.Add(new SqlParameter("@PartyId", SqlDbType.Int));
        cmd.Parameters["@PartyId"].Value = PartyId;
        cmd.Parameters.Add(new SqlParameter("@DocumentDescription", SqlDbType.NVarChar));
        cmd.Parameters["@DocumentDescription"].Value = DocumentDescription;
        cmd.Parameters.Add(new SqlParameter("@DocTypenm", SqlDbType.NVarChar));
        cmd.Parameters["@DocTypenm"].Value = Docyt;

        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        int result = DatabaseCls1.ExecuteNonQueryep(cmd);
        return (result != -1);
    }
    //UpdateDocumentEmailDownloadDefaultProp
    public bool UpdateDocumentEmailDownloadDefaultProp(Int32 DocumentEmailDownloadDefaultPropId, String DocumentTittle,
                            Int32 DocumentTypeId, Int32 PartyId, String DocumentDescription, bool DocTittleOrEmailSub, String Docyt)
    {
        cmd = new SqlCommand();

        cmd.CommandText = "UpdateDocumentEmailDownloadDefaultProp";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@DocumentEmailDownloadDefaultPropId", SqlDbType.Int));
        cmd.Parameters["@DocumentEmailDownloadDefaultPropId"].Value = DocumentEmailDownloadDefaultPropId;
        cmd.Parameters.Add(new SqlParameter("@DocumentTittle", SqlDbType.NVarChar));
        cmd.Parameters["@DocumentTittle"].Value = DocumentTittle;
        cmd.Parameters.Add(new SqlParameter("@DocumentTypeId", SqlDbType.Int));
        cmd.Parameters["@DocumentTypeId"].Value = DocumentTypeId;
        cmd.Parameters.Add(new SqlParameter("@PartyId", SqlDbType.Int));
        cmd.Parameters["@PartyId"].Value = PartyId;
        cmd.Parameters.Add(new SqlParameter("@DocumentDescription", SqlDbType.NVarChar));
        cmd.Parameters["@DocumentDescription"].Value = DocumentDescription;

        cmd.Parameters.Add(new SqlParameter("@DocTittleOrEmailSub", SqlDbType.Bit));
        cmd.Parameters["@DocTittleOrEmailSub"].Value = DocTittleOrEmailSub;
        cmd.Parameters.Add(new SqlParameter("@DocTypenm", SqlDbType.NVarChar));
        cmd.Parameters["@DocTypenm"].Value = Docyt;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        int result = DatabaseCls1.ExecuteNonQueryep(cmd);
        return (result != -1);
    }
    public bool UpdateDocumentFolderDownloadDefaultProp(Int32 DocumentFolderDownloadDefaultPropId, String DocumentTittle,
                            Int32 DocumentTypeId, Int32 PartyId, String DocumentDescription)
    {
        cmd = new SqlCommand();

        cmd.CommandText = "UpdateDocumentFolderDownloadDefaultProp";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@DocumentFolderDownloadDefaultPropId", SqlDbType.Int));
        cmd.Parameters["@DocumentFolderDownloadDefaultPropId"].Value = DocumentFolderDownloadDefaultPropId;
        cmd.Parameters.Add(new SqlParameter("@DocumentTittle", SqlDbType.NVarChar));
        cmd.Parameters["@DocumentTittle"].Value = DocumentTittle;
        cmd.Parameters.Add(new SqlParameter("@DocumentTypeId", SqlDbType.Int));
        cmd.Parameters["@DocumentTypeId"].Value = DocumentTypeId;
        cmd.Parameters.Add(new SqlParameter("@PartyId", SqlDbType.Int));
        cmd.Parameters["@PartyId"].Value = PartyId;
        cmd.Parameters.Add(new SqlParameter("@DocumentDescription", SqlDbType.NVarChar));
        cmd.Parameters["@DocumentDescription"].Value = DocumentDescription;        

        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        int result = DatabaseCls1.ExecuteNonQueryep(cmd);
        return (result != -1);
    }
    //SelectDocumentEmailDownloadMasterWithID
    public DataTable SelectDocumentEmailDownloadMasterWithID(Int32 DocumentEmailDownloadID)
    {

        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectDocumentEmailDownloadMasterWithID";
        cmd.Parameters.Add(new SqlParameter("@DocumentEmailDownloadID", SqlDbType.Int));
        cmd.Parameters["@DocumentEmailDownloadID"].Value = DocumentEmailDownloadID;
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;

    }
    public DataTable SelectDocumentEmailDownloadMasterWithID2(Int32 DocumentEmailDownloadID)
    {

        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectDocumentEmailDownloadMasterWithID2";
        cmd.Parameters.Add(new SqlParameter("@DocumentEmailDownloadID", SqlDbType.Int));
        cmd.Parameters["@DocumentEmailDownloadID"].Value = DocumentEmailDownloadID;
        dt = DatabaseCls1.FillAdapter(cmd);
        return dt;

    }




    //---------temp emailid , password unavailable...................
    public bool UpdateDocumentEmailDownloadMaster(Int32 DocumentEmailDownloadID, String ServerName, Boolean DocumentAutoApprove, String RuleType, String Whid, String Pass, String Eid, String AutoRetrival)
        //, String EmailId, String Password
        
    {
        cmd = new SqlCommand();

        cmd.CommandText = "UpdateDocumentEmailDownloadMaster";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@DocumentEmailDownloadID", SqlDbType.Int));
        cmd.Parameters["@DocumentEmailDownloadID"].Value = DocumentEmailDownloadID;
        cmd.Parameters.Add(new SqlParameter("@ServerName", SqlDbType.NVarChar));
        cmd.Parameters["@ServerName"].Value = ServerName;

        cmd.Parameters.Add(new SqlParameter("@DocumentAutoApprove", SqlDbType.NVarChar));
        cmd.Parameters["@DocumentAutoApprove"].Value = DocumentAutoApprove;
        cmd.Parameters.Add(new SqlParameter("@RuleType", SqlDbType.VarChar));
        cmd.Parameters["@RuleType"].Value = RuleType;
        cmd.Parameters.Add(new SqlParameter("@Whid", SqlDbType.VarChar));
        cmd.Parameters["@Whid"].Value = Whid;
        cmd.Parameters.Add(new SqlParameter("@EmailId", SqlDbType.NVarChar));
        cmd.Parameters["@EmailId"].Value = Eid;
        cmd.Parameters.Add(new SqlParameter("@Password", SqlDbType.NVarChar));
        cmd.Parameters["@Password"].Value = Pass;
        cmd.Parameters.Add(new SqlParameter("@AutoRetrival", SqlDbType.NVarChar));
        cmd.Parameters["@AutoRetrival"].Value = AutoRetrival;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        int result = DatabaseCls1.ExecuteNonQueryep(cmd);
        return (result != -1);
    }
    public bool UpdateInOutCompanyEmailMaster(Int32 ID, String OutgoingEmailServer, String OutEmailID, String OutPassword, String IncomingEmailServer, String InEmailID, String InPassword, Boolean chk, string Whid, string empid, string emailid, string port1, string port2, string EmailName)
    //, String EmailId, String Password
    {
        cmd = new SqlCommand();

        cmd.CommandText = "UpdateInOutCompanyEmailMaster";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int));
        cmd.Parameters["@ID"].Value = ID;
        cmd.Parameters.Add(new SqlParameter("@OutgoingEmailServer", SqlDbType.NVarChar));
        cmd.Parameters["@OutgoingEmailServer"].Value = OutgoingEmailServer;
        cmd.Parameters.Add(new SqlParameter("@OutEmailID", SqlDbType.NVarChar));
        cmd.Parameters["@OutEmailID"].Value = OutEmailID;
        cmd.Parameters.Add(new SqlParameter("@OutPassword", SqlDbType.NVarChar));
        cmd.Parameters["@OutPassword"].Value = OutPassword;
        cmd.Parameters.Add(new SqlParameter("@IncomingEmailServer", SqlDbType.NVarChar));
        cmd.Parameters["@IncomingEmailServer"].Value = IncomingEmailServer;
        cmd.Parameters.Add(new SqlParameter("@InEmailID", SqlDbType.NVarChar));
        cmd.Parameters["@InEmailID"].Value = InEmailID;
        cmd.Parameters.Add(new SqlParameter("@InPassword", SqlDbType.NVarChar));
        cmd.Parameters["@InPassword"].Value = InPassword;

        cmd.Parameters.Add(new SqlParameter("@EmployeeID", SqlDbType.NVarChar));
        cmd.Parameters["@EmployeeID"].Value = empid;
        cmd.Parameters.Add(new SqlParameter("@EmailId", SqlDbType.NVarChar));
        cmd.Parameters["@EmailId"].Value = emailid;
        cmd.Parameters.Add(new SqlParameter("@port1", SqlDbType.NVarChar));
        cmd.Parameters["@port1"].Value = port1;
        cmd.Parameters.Add(new SqlParameter("@port2", SqlDbType.NVarChar));
        cmd.Parameters["@port2"].Value = port2;
        cmd.Parameters.Add(new SqlParameter("@SetforOutgoingemail", SqlDbType.Bit));
        cmd.Parameters["@SetforOutgoingemail"].Value = chk;
        cmd.Parameters.Add(new SqlParameter("@Whid", SqlDbType.NVarChar));
        cmd.Parameters["@Whid"].Value = Whid; // CompanyLoginId;

        cmd.Parameters.Add(new SqlParameter("@Emailname", SqlDbType.NVarChar));
        cmd.Parameters["@Emailname"].Value = EmailName;

        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        int result = DatabaseCls1.ExecuteNonQueryep(cmd);
        return (result != -1);
    }
    public bool UpdateDownloadFolder(Int32 FolderId, String FolderName, Boolean DocumentAutoApprove, String RuleType, String Whid, String Autoret)
    {
        cmd = new SqlCommand();

        cmd.CommandText = "UpdateDownloadFolder";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@FolderId", SqlDbType.Int));
        cmd.Parameters["@FolderId"].Value = FolderId;
        cmd.Parameters.Add(new SqlParameter("@FolderName", SqlDbType.NVarChar));
        cmd.Parameters["@FolderName"].Value = FolderName;
        cmd.Parameters.Add(new SqlParameter("@DocumentAutoApprove", SqlDbType.NVarChar));
        cmd.Parameters["@DocumentAutoApprove"].Value = DocumentAutoApprove;
        cmd.Parameters.Add(new SqlParameter("@RuleType", SqlDbType.NVarChar));
        cmd.Parameters["@RuleType"].Value = RuleType;
        cmd.Parameters.Add(new SqlParameter("@AutoInterval", SqlDbType.NVarChar));
        cmd.Parameters["@AutoInterval"].Value = Autoret;
        cmd.Parameters.Add(new SqlParameter("@Whid", SqlDbType.NVarChar));
        cmd.Parameters["@Whid"].Value = Whid;
       
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        int result = DatabaseCls1.ExecuteNonQueryep(cmd);
        return (result != -1);
    }
    public Int32 InsertDocumentEmailDownloadDefaultProp(Int32 DocumentEmailDownloadID, string DocumentTittle, Int32 DocumentTypeId, Int32 PartyId, string DocumentDescription, bool DocTittleOrEmailSub)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "InsertDocumentEmailDownloadDefaultProp";
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.Add(new SqlParameter("@DocumentEmailDownloadID", SqlDbType.Int));
        cmd.Parameters["@DocumentEmailDownloadID"].Value = DocumentEmailDownloadID;

        cmd.Parameters.Add(new SqlParameter("@DocumentTittle", SqlDbType.NVarChar));
        cmd.Parameters["@DocumentTittle"].Value = DocumentTittle;

        cmd.Parameters.Add(new SqlParameter("@DocumentTypeId", SqlDbType.Int));
        cmd.Parameters["@DocumentTypeId"].Value = DocumentTypeId;
        cmd.Parameters.Add(new SqlParameter("@PartyId", SqlDbType.Int));
        cmd.Parameters["@PartyId"].Value = PartyId;

        cmd.Parameters.Add(new SqlParameter("@DocumentDescription", SqlDbType.NVarChar));
        cmd.Parameters["@DocumentDescription"].Value = DocumentDescription;

        cmd.Parameters.Add(new SqlParameter("@DocTittleOrEmailSub", SqlDbType.Bit));
        cmd.Parameters["@DocTittleOrEmailSub"].Value = DocTittleOrEmailSub;

        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls1.ExecuteNonQueryep(cmd);
        return result;
    }

    public Int32 InsertFTPMasterDefaultProp(Int32 FTPMasterId, string DocumentTitle, Int32 DocumentTypeId, Int32 PartyId, string DocumentDescription,string Docyt)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "InsertFTPMasterDefaultProp";
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.Add(new SqlParameter("@FTPMasterId", SqlDbType.Int));
        cmd.Parameters["@FTPMasterId"].Value = FTPMasterId;

        cmd.Parameters.Add(new SqlParameter("@DocumentTitle", SqlDbType.NVarChar));
        cmd.Parameters["@DocumentTitle"].Value = DocumentTitle;

        cmd.Parameters.Add(new SqlParameter("@DocumentTypeId", SqlDbType.Int));
        cmd.Parameters["@DocumentTypeId"].Value = DocumentTypeId;
        cmd.Parameters.Add(new SqlParameter("@PartyId", SqlDbType.Int));
        cmd.Parameters["@PartyId"].Value = PartyId;

        cmd.Parameters.Add(new SqlParameter("@DocumentDescription", SqlDbType.NVarChar));
        cmd.Parameters["@DocumentDescription"].Value = DocumentDescription;

        cmd.Parameters.Add(new SqlParameter("@DocTypenm", SqlDbType.NVarChar));
        cmd.Parameters["@DocTypenm"].Value = Docyt;

        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls1.ExecuteNonQueryep(cmd);
        return result;
    }
    public Int32 InsertDocumentEmailDownloadMaster(string ServerName, string EmailId, string Password, bool DocumentAutoApprove,String RuleType,String Whid)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "InsertDocumentEmailDownloadMaster";
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.Add(new SqlParameter("@ServerName", SqlDbType.NVarChar));
        cmd.Parameters["@ServerName"].Value = ServerName;

        cmd.Parameters.Add(new SqlParameter("@EmailId", SqlDbType.NVarChar));
        cmd.Parameters["@EmailId"].Value = EmailId;

        cmd.Parameters.Add(new SqlParameter("@Password", SqlDbType.NVarChar));
        cmd.Parameters["@Password"].Value = Password;

        cmd.Parameters.Add(new SqlParameter("@DocumentEmailDownloadID", SqlDbType.Int));
        cmd.Parameters["@DocumentEmailDownloadID"].Direction = ParameterDirection.Output;

        cmd.Parameters.Add(new SqlParameter("@DocumentAutoApprove", SqlDbType.Bit));
        cmd.Parameters["@DocumentAutoApprove"].Value = DocumentAutoApprove;

        cmd.Parameters.Add(new SqlParameter("@RuleType", SqlDbType.VarChar));
        cmd.Parameters["@RuleType"].Value = RuleType;

        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        cmd.Parameters.Add(new SqlParameter("@Whid", SqlDbType.NVarChar));
        cmd.Parameters["@Whid"].Value = Whid; // CompanyLoginId;

    
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;

        Int32 result = DatabaseCls1.ExecuteNonQueryep(cmd);
        result = Convert.ToInt32(cmd.Parameters["@DocumentEmailDownloadID"].Value);
        return (result);
    }
    // 10-6-09
    public Int32 InsertCompanyEmailMaster(string ServerName, string EmailId, string Password, bool DocumentAutoApprove,Int32 partyid)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "InsertCompanyEmailMaster";
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.Add(new SqlParameter("@ServerName", SqlDbType.NVarChar));
        cmd.Parameters["@ServerName"].Value = ServerName;

        cmd.Parameters.Add(new SqlParameter("@EmailId", SqlDbType.NVarChar));
        cmd.Parameters["@EmailId"].Value = EmailId;

        cmd.Parameters.Add(new SqlParameter("@Password", SqlDbType.NVarChar));
        cmd.Parameters["@Password"].Value = Password;

        cmd.Parameters.Add(new SqlParameter("@CompanyEmailId", SqlDbType.Int));
        cmd.Parameters["@CompanyEmailId"].Direction = ParameterDirection.Output;

        cmd.Parameters.Add(new SqlParameter("@DocumentAutoApprove", SqlDbType.Bit));
        cmd.Parameters["@DocumentAutoApprove"].Value = DocumentAutoApprove;

        cmd.Parameters.Add(new SqlParameter("@PartyId", SqlDbType.Int));
        cmd.Parameters["@PartyId"].Value = partyid;

        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["CompanyLoginId"].ToString(); // CompanyLoginId;

        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;

        Int32 result = DatabaseCls1.ExecuteNonQuery(cmd);
        result = Convert.ToInt32(cmd.Parameters["@CompanyEmailId"].Value);
        return (result);
    }
    public bool UpdateEmailSignatureMaster(string inoutgoingmail, string signature)
    {
        cmd = new SqlCommand();

        cmd.CommandText = "UpdateEmailSignatureMaster";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@Signature", SqlDbType.NVarChar));
        cmd.Parameters["@Signature"].Value = signature;
        cmd.Parameters.Add(new SqlParameter("@InoutgoingMasterId", SqlDbType.NVarChar));
        cmd.Parameters["@InoutgoingMasterId"].Value = inoutgoingmail;

        int result = DatabaseCls1.ExecuteNonQueryep(cmd);
        return (result != -1);
    }

    public Int32 InsertSignatureMaster(string inoutgoingmail, string signature)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "InsertSignatureMaster";
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.Add(new SqlParameter("@Signature", SqlDbType.NVarChar));
        cmd.Parameters["@Signature"].Value = signature;
        cmd.Parameters.Add(new SqlParameter("@InoutgoingMasterId", SqlDbType.NVarChar));
        cmd.Parameters["@InoutgoingMasterId"].Value = inoutgoingmail;
       
       
        cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int));
        cmd.Parameters["@ID"].Direction = ParameterDirection.Output;
       

        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;

        Int32 result = DatabaseCls1.ExecuteNonQueryep(cmd);
        result = Convert.ToInt32(cmd.Parameters["@ID"].Value);
        return (result);
    }

    public Int32 InsertOutgoingCompanyEmail(String OutgoingEmailServer, String OutEmailID, String OutPassword, String IncomingEmailServer, String InEmailID, String InPassword, DateTime LastDownloadedTime, Int32 LastDownloadIndex, Boolean chk, string Whid, string empid, string email, string port1, string port2, string Emailname)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "InsertOutgoingCompanyEmail";
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.Add(new SqlParameter("@OutgoingEmailServer", SqlDbType.NVarChar));
        cmd.Parameters["@OutgoingEmailServer"].Value = OutgoingEmailServer;
        cmd.Parameters.Add(new SqlParameter("@OutEmailID", SqlDbType.NVarChar));
        cmd.Parameters["@OutEmailID"].Value = OutEmailID;
        cmd.Parameters.Add(new SqlParameter("@OutPassword", SqlDbType.NVarChar));
        cmd.Parameters["@OutPassword"].Value = OutPassword;
        cmd.Parameters.Add(new SqlParameter("@IncomingEmailServer", SqlDbType.NVarChar));
        cmd.Parameters["@IncomingEmailServer"].Value = IncomingEmailServer;
        cmd.Parameters.Add(new SqlParameter("@InEmailID", SqlDbType.NVarChar));
        cmd.Parameters["@InEmailID"].Value = InEmailID;
        cmd.Parameters.Add(new SqlParameter("@InPassword", SqlDbType.NVarChar));
        cmd.Parameters["@InPassword"].Value = InPassword;
        cmd.Parameters.Add(new SqlParameter("@LastDownloadedTime", SqlDbType.DateTime));
        cmd.Parameters["@LastDownloadedTime"].Value = LastDownloadedTime;
        cmd.Parameters.Add(new SqlParameter("@LastDownloadIndex", SqlDbType.Int));
        cmd.Parameters["@LastDownloadIndex"].Value = LastDownloadIndex;
        cmd.Parameters.Add(new SqlParameter("@EmployeeID", SqlDbType.NVarChar));
        cmd.Parameters["@EmployeeID"].Value = empid;
        cmd.Parameters.Add(new SqlParameter("@EmailId", SqlDbType.NVarChar));
        cmd.Parameters["@EmailId"].Value = email;
        cmd.Parameters.Add(new SqlParameter("@port1", SqlDbType.NVarChar));
        cmd.Parameters["@port1"].Value = port1;
        cmd.Parameters.Add(new SqlParameter("@port2", SqlDbType.NVarChar));
        cmd.Parameters["@port2"].Value = port2;        
        cmd.Parameters.Add(new SqlParameter("@SetforOutgoingemail", SqlDbType.Bit));
        cmd.Parameters["@SetforOutgoingemail"].Value = chk;
        cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int));
        cmd.Parameters["@ID"].Direction = ParameterDirection.Output;
        cmd.Parameters.Add(new SqlParameter("@Whid", SqlDbType.NVarChar));

        cmd.Parameters.Add(new SqlParameter("@Emailname", SqlDbType.NVarChar));
        cmd.Parameters["@Emailname"].Value = Emailname;

        cmd.Parameters["@Whid"].Value = Whid; // CompanyLoginId;

        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;

        Int32 result = DatabaseCls1.ExecuteNonQueryep(cmd);
        result = Convert.ToInt32(cmd.Parameters["@ID"].Value);
        return (result);
    }

    public bool InsertRuleProcess(Int32 DocumentId, Int32 RuleDetailId, String Note, Boolean Approve)
    {
        cmd = new SqlCommand();

        cmd.CommandText = "InsertRuleProcess";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@DocumentId", SqlDbType.Int));
        cmd.Parameters["@DocumentId"].Value = DocumentId;
        cmd.Parameters.Add(new SqlParameter("@EmployeeId", SqlDbType.Int));
        cmd.Parameters["@EmployeeId"].Value = Convert.ToInt32(HttpContext.Current.Session["EmployeeId"].ToString()); // EmployeeId;
        //cmd.Parameters.Add(new SqlParameter("@RuleId", SqlDbType.Int));
        //cmd.Parameters["@RuleId"].Value = @RuleId;
        cmd.Parameters.Add(new SqlParameter("@RuleDetailId", SqlDbType.Int));
        cmd.Parameters["@RuleDetailId"].Value = @RuleDetailId;
        cmd.Parameters.Add(new SqlParameter("@Note", SqlDbType.VarChar));
        cmd.Parameters["@Note"].Value = Note;
        cmd.Parameters.Add(new SqlParameter("@Approve", SqlDbType.Bit));
        cmd.Parameters["@Approve"].Value = Approve;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        int result = DatabaseCls1.ExecuteNonQueryep(cmd);
        return (result != -1);
    }
    public bool UpdateRuleProcessforParty(Int32 DocumentId, Int32 RuleDetailId, String Note, Boolean Approve)
    {
        cmd = new SqlCommand();

        cmd.CommandText = "UpdateRuleProcessforParty";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@DocumentId", SqlDbType.Int));
        cmd.Parameters["@DocumentId"].Value = DocumentId;
        cmd.Parameters.Add(new SqlParameter("@PartyId", SqlDbType.Int));
        cmd.Parameters["@PartyId"].Value = Convert.ToInt32(HttpContext.Current.Session["PartyId"].ToString()); // EmployeeId;        
        cmd.Parameters.Add(new SqlParameter("@RuleDetailId", SqlDbType.Int));
        cmd.Parameters["@RuleDetailId"].Value = @RuleDetailId;
        cmd.Parameters.Add(new SqlParameter("@Note", SqlDbType.VarChar));
        cmd.Parameters["@Note"].Value = Note;
        cmd.Parameters.Add(new SqlParameter("@Approve", SqlDbType.Bit));
        cmd.Parameters["@Approve"].Value = Approve;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Conid"].ToString(); // CompanyLoginId;

        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        int result = DatabaseCls1.ExecuteNonQueryep(cmd);
        return (result != -1);
    }
    public bool UpdateRuleProcess(Int32 DocumentId, Int32 RuleDetailId, String Note, Boolean Approve)
    {
        cmd = new SqlCommand();

        cmd.CommandText = "UpdateRuleProcess";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@DocumentId", SqlDbType.Int));
        cmd.Parameters["@DocumentId"].Value = DocumentId;
        cmd.Parameters.Add(new SqlParameter("@EmployeeId", SqlDbType.Int));
        cmd.Parameters["@EmployeeId"].Value = Convert.ToInt32(HttpContext.Current.Session["EmployeeId"].ToString()); // EmployeeId;        
        cmd.Parameters.Add(new SqlParameter("@RuleDetailId", SqlDbType.Int));
        cmd.Parameters["@RuleDetailId"].Value = @RuleDetailId;
        cmd.Parameters.Add(new SqlParameter("@Note", SqlDbType.VarChar));
        cmd.Parameters["@Note"].Value = Note;
        cmd.Parameters.Add(new SqlParameter("@Approve", SqlDbType.Bit));
        cmd.Parameters["@Approve"].Value = Approve;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;

        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        int result = DatabaseCls1.ExecuteNonQueryep(cmd);
        return (result != -1);
    }
    public bool InsertRuleProcessforParty(Int32 DocumentId, Int32 RuleDetailId, String Note, Boolean Approve)
    {
        cmd = new SqlCommand();

        cmd.CommandText = "InsertRuleProcessforParty";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@DocumentId", SqlDbType.Int));
        cmd.Parameters["@DocumentId"].Value = DocumentId;
        cmd.Parameters.Add(new SqlParameter("@PartyId", SqlDbType.Int));
        cmd.Parameters["@PartyId"].Value = Convert.ToInt32(HttpContext.Current.Session["PartyId"].ToString()); // EmployeeId;
        //cmd.Parameters.Add(new SqlParameter("@RuleId", SqlDbType.Int));
        //cmd.Parameters["@RuleId"].Value = @RuleId;
        cmd.Parameters.Add(new SqlParameter("@RuleDetailId", SqlDbType.Int));
        cmd.Parameters["@RuleDetailId"].Value = @RuleDetailId;
        cmd.Parameters.Add(new SqlParameter("@Note", SqlDbType.VarChar));
        cmd.Parameters["@Note"].Value = Note;
        cmd.Parameters.Add(new SqlParameter("@Approve", SqlDbType.Bit));
        cmd.Parameters["@Approve"].Value = Approve;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        int result = DatabaseCls1.ExecuteNonQueryep(cmd);
        return (result != -1);
    }
    public DataTable SelectRuleMasterRuleTitleWise(string RuleTitle)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectRuleMasterRuleTitleWise";

        cmd.Parameters.Add(new SqlParameter("@RuleTitle", SqlDbType.VarChar));
        cmd.Parameters["@RuleTitle"].Value = RuleTitle;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["CompanyLoginId"].ToString(); // CompanyLoginId;
        dt = DatabaseCls1.FillAdapter(cmd); //.FillAdapter(cmd);
        return dt;

    }


    public DataTable SelectDocumentMasterByDocId(int DocId,String Whid)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectDocumentMasterByDocId";

        cmd.Parameters.Add(new SqlParameter("@DocId", SqlDbType.Int));
        cmd.Parameters["@DocId"].Value = DocId;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        cmd.Parameters.Add(new SqlParameter("@Whid", SqlDbType.NVarChar));
        cmd.Parameters["@Whid"].Value = Whid; // CompanyLoginId;
      
        
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;
    }


    public DataTable SelectDocumentProcesswithAllEmployee(int DocId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectDocumentProcesswithAllEmployee";

        cmd.Parameters.Add(new SqlParameter("@DocId", SqlDbType.Int));
        cmd.Parameters["@DocId"].Value = DocId;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;
    }


    public DataTable SelectDocumentMasterByDocumentName(string DocumentTitle,String Whid)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectDocumentMasterByDocumentName";

        cmd.Parameters.Add(new SqlParameter("@DocumentTitle", SqlDbType.NVarChar));
        cmd.Parameters["@DocumentTitle"].Value = DocumentTitle;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        cmd.Parameters.Add(new SqlParameter("@Whid", SqlDbType.NVarChar));
        cmd.Parameters["@Whid"].Value =Whid; // CompanyLoginId;
      
        
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;
    }
    public DataTable SelectProcessingDocumentbyDocTypeIdwiseDiscforparty(Int32 DocumentTypeId, Int32 RuleId)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "SelectProcessingDocumentbyDocTypeIdwiseDiscforparty";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@DocumentTypeId", SqlDbType.Int));
        cmd.Parameters["@DocumentTypeId"].Value = DocumentTypeId;
        cmd.Parameters.Add(new SqlParameter("@EmployeeId", SqlDbType.Int));
        cmd.Parameters["@EmployeeId"].Value = Convert.ToInt32(HttpContext.Current.Session["PartyId"].ToString()); // EmployeeId;
        cmd.Parameters.Add(new SqlParameter("@RuleId", SqlDbType.Int));
        cmd.Parameters["@RuleId"].Value = RuleId;
        // cmd.Parameters.Add(new SqlParameter("@StepId", SqlDbType.Int));
        // cmd.Parameters["@StepId"].Value = StepId;

        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        dt = new DataTable();
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;
    }
    public DataTable SelectProcessingDocumentbyDocTypeIdwiseDisc(Int32 DocumentTypeId, Int32 RuleId)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "SelectProcessingDocumentbyDocTypeIdwiseDisc";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@DocumentTypeId", SqlDbType.Int));
        cmd.Parameters["@DocumentTypeId"].Value = DocumentTypeId;
        cmd.Parameters.Add(new SqlParameter("@EmployeeId", SqlDbType.Int));
        cmd.Parameters["@EmployeeId"].Value = Convert.ToInt32(HttpContext.Current.Session["EmployeeId"].ToString()); // EmployeeId;
        cmd.Parameters.Add(new SqlParameter("@RuleId", SqlDbType.Int));
        cmd.Parameters["@RuleId"].Value = RuleId;
        // cmd.Parameters.Add(new SqlParameter("@StepId", SqlDbType.Int));
        // cmd.Parameters["@StepId"].Value = StepId;

        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        dt = new DataTable();
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;
    }
 
    public DataTable SelectProcessingDocumentbyDocTypeIdwiseTopforParty(Int32 DocumentTypeId, Int32 RuleId, Int32 DocumentId)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "SelectProcessingDocumentbyDocTypeIdwiseTopforParty";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@DocumentTypeId", SqlDbType.Int));
        cmd.Parameters["@DocumentTypeId"].Value = DocumentTypeId;
        cmd.Parameters.Add(new SqlParameter("@EmployeeId", SqlDbType.Int));
        cmd.Parameters["@EmployeeId"].Value = Convert.ToInt32(HttpContext.Current.Session["PartyId"].ToString()); // EmployeeId;
        cmd.Parameters.Add(new SqlParameter("@RuleId", SqlDbType.Int));
        cmd.Parameters["@RuleId"].Value = RuleId;
        cmd.Parameters.Add(new SqlParameter("@DocumentId", SqlDbType.Int));
        cmd.Parameters["@DocumentId"].Value = DocumentId;
        // cmd.Parameters.Add(new SqlParameter("@StepId", SqlDbType.Int));
        // cmd.Parameters["@StepId"].Value = StepId;

        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        dt = new DataTable();
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;
    }
    public DataTable SelectProcessingDocumentbyDocTypeIdwiseTop(Int32 DocumentTypeId, Int32 RuleId, Int32 DocumentId)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "SelectProcessingDocumentbyDocTypeIdwiseTop";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@DocumentTypeId", SqlDbType.Int));
        cmd.Parameters["@DocumentTypeId"].Value = DocumentTypeId;
        cmd.Parameters.Add(new SqlParameter("@EmployeeId", SqlDbType.Int));
        cmd.Parameters["@EmployeeId"].Value = Convert.ToInt32(HttpContext.Current.Session["EmployeeId"].ToString()); // EmployeeId;
        cmd.Parameters.Add(new SqlParameter("@RuleId", SqlDbType.Int));
        cmd.Parameters["@RuleId"].Value = RuleId;
        cmd.Parameters.Add(new SqlParameter("@DocumentId", SqlDbType.Int));
        cmd.Parameters["@DocumentId"].Value = DocumentId;
        // cmd.Parameters.Add(new SqlParameter("@StepId", SqlDbType.Int));
        // cmd.Parameters["@StepId"].Value = StepId;

        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        dt = new DataTable();
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;
    }
    public DataTable SelectDocumentMasterByStatusinDocumentFlowByParty(Boolean Approve, String Whid, String Apptypeid)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "SelectDocumentMasterByStatusinDocumentFlowByParty";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@PartyId", SqlDbType.Int));
        cmd.Parameters["@PartyId"].Value = Convert.ToInt32(HttpContext.Current.Session["PartyId"].ToString()); // EmployeeId;

        cmd.Parameters.Add(new SqlParameter("@Whid", SqlDbType.Int));
        cmd.Parameters["@Whid"].Value = Whid; // EmployeeId;
        cmd.Parameters.Add(new SqlParameter("@RuleApproveTypeId", SqlDbType.Int));
        cmd.Parameters["@RuleApproveTypeId"].Value = Apptypeid;
        cmd.Parameters.Add(new SqlParameter("@Approve", SqlDbType.Bit));
        cmd.Parameters["@Approve"].Value = Approve;

        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        dt = new DataTable();
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;
    }
    public DataTable SelectDocumentMasterByStatusinDocumentFlowByEmployee(Boolean Approve,String Whid,String Apptypeid)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "SelectDocumentMasterByStatusinDocumentFlowByEmployee";
        cmd.CommandType = CommandType.StoredProcedure;        
        cmd.Parameters.Add(new SqlParameter("@EmployeeId", SqlDbType.Int));
        cmd.Parameters["@EmployeeId"].Value = Convert.ToInt32(HttpContext.Current.Session["EmployeeId"].ToString()); // EmployeeId;

        cmd.Parameters.Add(new SqlParameter("@Whid", SqlDbType.Int));
        cmd.Parameters["@Whid"].Value = Whid; // EmployeeId;
        cmd.Parameters.Add(new SqlParameter("@RuleApproveTypeId", SqlDbType.Int));
        cmd.Parameters["@RuleApproveTypeId"].Value = Apptypeid;
        cmd.Parameters.Add(new SqlParameter("@Approve", SqlDbType.Bit));
        cmd.Parameters["@Approve"].Value = Approve;     
      
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        dt = new DataTable();
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;
    }
   
    public DataTable SelectProcessingDocumentbyDocTypeIdwiseDiscIdWise(Int32 DocumentTypeId, Int32 RuleId, Int32 DocumentId)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "SelectProcessingDocumentbyDocTypeIdwiseDiscIdWise";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@DocumentTypeId", SqlDbType.Int));
        cmd.Parameters["@DocumentTypeId"].Value = DocumentTypeId;
        cmd.Parameters.Add(new SqlParameter("@EmployeeId", SqlDbType.Int));
        cmd.Parameters["@EmployeeId"].Value = Convert.ToInt32(HttpContext.Current.Session["EmployeeId"].ToString()); // EmployeeId;
        cmd.Parameters.Add(new SqlParameter("@RuleId", SqlDbType.Int));
        cmd.Parameters["@RuleId"].Value = RuleId;
        cmd.Parameters.Add(new SqlParameter("@DocumentId", SqlDbType.Int));
        cmd.Parameters["@DocumentId"].Value = DocumentId;
        // cmd.Parameters.Add(new SqlParameter("@StepId", SqlDbType.Int));
        // cmd.Parameters["@StepId"].Value = StepId;

        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        dt = new DataTable();
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;
    }
    public DataTable SelectProcessingDocumentbyDocTypeIdwiseDiscTitleWise(Int32 DocumentTypeId, Int32 RuleId, string DocumentName)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "SelectProcessingDocumentbyDocTypeIdwiseDiscTitleWise";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@DocumentTypeId", SqlDbType.Int));
        cmd.Parameters["@DocumentTypeId"].Value = DocumentTypeId;
        cmd.Parameters.Add(new SqlParameter("@EmployeeId", SqlDbType.Int));
        cmd.Parameters["@EmployeeId"].Value = Convert.ToInt32(HttpContext.Current.Session["EmployeeId"].ToString()); // EmployeeId;
        cmd.Parameters.Add(new SqlParameter("@RuleId", SqlDbType.Int));
        cmd.Parameters["@RuleId"].Value = RuleId;
        cmd.Parameters.Add(new SqlParameter("@DocumentName", SqlDbType.NVarChar));
        cmd.Parameters["@DocumentName"].Value = DocumentName;
        // cmd.Parameters.Add(new SqlParameter("@StepId", SqlDbType.Int));
        // cmd.Parameters["@StepId"].Value = StepId;

        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        dt = new DataTable();
        dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;
    }
     public DataTable SelectDownloadFolder(String Whid)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectDownloadFolder";
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        cmd.Parameters.Add(new SqlParameter("@Whid", SqlDbType.NVarChar));
        cmd.Parameters["@Whid"].Value = Whid;
         dt = DatabaseCls1.FilleppAdapter(cmd);
        return dt;
    }
     public Int32 InsertDocumentFolderDownloadDefaultProp(Int32 FolderId, string DocumentTittle, Int32 DocumentTypeId, Int32 PartyId, string DocumentDescription )
    {
        cmd = new SqlCommand();
        cmd.CommandText = "InsertDocumentFolderDownloadDefaultProp";
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.Add(new SqlParameter("@FolderId", SqlDbType.Int));
        cmd.Parameters["@FolderId"].Value = FolderId;

        cmd.Parameters.Add(new SqlParameter("@DocumentTittle", SqlDbType.NVarChar));
        cmd.Parameters["@DocumentTittle"].Value = DocumentTittle;

        cmd.Parameters.Add(new SqlParameter("@DocumentTypeId", SqlDbType.Int));
        cmd.Parameters["@DocumentTypeId"].Value = DocumentTypeId;
        cmd.Parameters.Add(new SqlParameter("@PartyId", SqlDbType.Int));
        cmd.Parameters["@PartyId"].Value = PartyId;

        cmd.Parameters.Add(new SqlParameter("@DocumentDescription", SqlDbType.NVarChar));
        cmd.Parameters["@DocumentDescription"].Value = DocumentDescription;

        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls1.ExecuteNonQueryep(cmd);
        return result;
    }
    // 26_10_09

    public DataTable SelectDocumentforApprovalLessProcessId(Int32 DocumentProcessingId, Int32 DocumentId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectDocumentforApprovalLessProcessId";
        cmd.Parameters.Add(new SqlParameter("@DocumentProcessingId", SqlDbType.Int));
        cmd.Parameters["@DocumentProcessingId"].Value = DocumentProcessingId; // Convert.ToInt32(HttpContext.Current.Session["EmployeeId"].ToString()); // EmployeeId;
        cmd.Parameters.Add(new SqlParameter("@DocumentId", SqlDbType.Int));
        cmd.Parameters["@DocumentId"].Value = DocumentId; // Convert.ToInt32(HttpContext.Current.Session["EmployeeId"].ToString()); // EmployeeId;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;


        dt = DatabaseCls1.FilleppAdapter(cmd); //.FillAdapter(cmd);
        return dt;

    }
    public DataTable SelectDocumentforApprovalLessProcessIdSup(Int32 DocumentProcessingId, Int32 DocumentId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectDocumentforApprovalLessProcessIdSup";
        cmd.Parameters.Add(new SqlParameter("@DocumentProcessingId", SqlDbType.Int));
        cmd.Parameters["@DocumentProcessingId"].Value = DocumentProcessingId; // Convert.ToInt32(HttpContext.Current.Session["EmployeeId"].ToString()); // EmployeeId;
        cmd.Parameters.Add(new SqlParameter("@DocumentId", SqlDbType.Int));
        cmd.Parameters["@DocumentId"].Value = DocumentId; // Convert.ToInt32(HttpContext.Current.Session["EmployeeId"].ToString()); // EmployeeId;


        dt = DatabaseCls1.FillAdapter(cmd); //.FillAdapter(cmd);
        return dt;

    }
    public DataTable SelectDepartmentmasterMNCWithStoreId(String Whid)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectDepartmentmasterMNCWithStoreId";
        cmd.Parameters.Add(new SqlParameter("@Whid", SqlDbType.NVarChar));
        cmd.Parameters["@Whid"].Value = Whid; // Convert.ToInt32(HttpContext.Current.Session["EmployeeId"].ToString()); // EmployeeId;

        dt = DatabaseCls1.FilleppAdapter(cmd); //.FillAdapter(cmd);
        return dt;

    }

    public DataTable SelectDivisionwithStoreIdanddeptId(String Whid,String Depart,Int32 flag)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectDivisionwithStoreIdanddeptId";
        cmd.Parameters.Add(new SqlParameter("@Whid", SqlDbType.NVarChar));
        cmd.Parameters["@Whid"].Value = Whid; // Convert.ToInt32(HttpContext.Current.Session["EmployeeId"].ToString()); // EmployeeId;
        cmd.Parameters.Add(new SqlParameter("@DeptId", SqlDbType.NVarChar));
        cmd.Parameters["@DeptId"].Value = Depart;
        cmd.Parameters.Add(new SqlParameter("@flag", SqlDbType.NVarChar));
        cmd.Parameters["@flag"].Value = flag;
        dt = DatabaseCls1.FilleppAdapter(cmd); //.FillAdapter(cmd);
        return dt;

    }
    public DataTable SelectEmployeeMasterwithDivId(String BusinessId,Int32 flag,String Whid)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectEmployeeMasterwithDivId";
        cmd.Parameters.Add(new SqlParameter("@BusinessId", SqlDbType.NVarChar));
        cmd.Parameters["@BusinessId"].Value = BusinessId; // Convert.ToInt32(HttpContext.Current.Session["EmployeeId"].ToString()); // EmployeeId;
        cmd.Parameters.Add(new SqlParameter("@flag", SqlDbType.NVarChar));
        cmd.Parameters["@flag"].Value = flag;
        cmd.Parameters.Add(new SqlParameter("@Whid", SqlDbType.NVarChar));
        cmd.Parameters["@Whid"].Value = Whid;
        dt = DatabaseCls1.FilleppAdapter(cmd); //.FillAdapter(cmd);
        return dt;

    }
    public DataTable SelectTablemaster(String TableName)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectTablemaster";

        cmd.Parameters.Add(new SqlParameter("@TableName", SqlDbType.NVarChar));
        cmd.Parameters["@TableName"].Value = TableName;
        dt = DatabaseCls1.FilleppAdapter(cmd); //.FillAdapter(cmd);
        return dt;

    }
    public  DataTable SpObjectiveMasterGetDataStructById(string MasterId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SpObjectiveMasterGetDataStructById";
        cmd.Parameters.Add(new SqlParameter("@MasterId", SqlDbType.NVarChar));
        cmd.Parameters["@MasterId"].Value = MasterId; // Convert.ToInt32(HttpContext.Current.Session["EmployeeId"].ToString()); // EmployeeId;
        
        dt = DatabaseCls1.FilleppAdapter(cmd); //.FillAdapter(cmd);
        return dt;
    }

    public DataTable SpObjectiveMasterGridfill(String para,string filc)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SpObjectiveMasterGridfill";
        cmd.Parameters.Add(new SqlParameter("@para", SqlDbType.NVarChar));
        cmd.Parameters["@para"].Value = para; // Convert.ToInt32(HttpContext.Current.Session["EmployeeId"].ToString()); // EmployeeId;

        cmd.Parameters.Add(new SqlParameter("@filc", SqlDbType.NVarChar));
        cmd.Parameters["@filc"].Value = filc; //
        //cmd.Parameters.Add(new SqlParameter("@BusinessID", SqlDbType.NVarChar));
        //cmd.Parameters["@BusinessID"].Value = BusinessID;
        //cmd.Parameters.Add(new SqlParameter("@Whid", SqlDbType.NVarChar));
        //cmd.Parameters["@Whid"].Value = Whid;
        //cmd.Parameters.Add(new SqlParameter("@EmployeemasterId", SqlDbType.NVarChar));
        //cmd.Parameters["@EmployeemasterId"].Value = EmployeemasterId;

        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = "'"+HttpContext.Current.Session["Comid"].ToString()+"'"; // CompanyLoginId;
        dt = DatabaseCls1.FilleppAdapter(cmd); //.FillAdapter(cmd);
        return dt;

    }
    public DataTable SelectOfficeManagerDocuments(string MissionId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectOfficeManagerDocuments";
        cmd.Parameters.Add(new SqlParameter("@MissionId", SqlDbType.NVarChar));
        cmd.Parameters["@MissionId"].Value = MissionId; // Convert.ToInt32(HttpContext.Current.Session["EmployeeId"].ToString()); // EmployeeId;
        
        dt = DatabaseCls1.FilleppAdapter(cmd); //.FillAdapter(cmd);
        return dt;
    }

    public DataTable SelectOfficeManagerDocuments1(string MissionId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectOfficeManagerDocuments1";
        cmd.Parameters.Add(new SqlParameter("@MissionId", SqlDbType.NVarChar));
        cmd.Parameters["@MissionId"].Value = MissionId; // Convert.ToInt32(HttpContext.Current.Session["EmployeeId"].ToString()); // EmployeeId;

        dt = DatabaseCls1.FilleppAdapter(cmd); //.FillAdapter(cmd);
        return dt;
    }

    public int DeleteOfficeManagerDocuments(string MissionId)
    {
        cmd = new SqlCommand();
        
        cmd.CommandText = "DeleteOfficeManagerDocuments";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@MissionId", SqlDbType.NVarChar));
        cmd.Parameters["@MissionId"].Value = MissionId; // Convert.ToInt32(HttpContext.Current.Session["EmployeeId"].ToString()); // EmployeeId;

        int I = DatabaseCls1.ExecuteNonQueryep(cmd); //.FillAdapter(cmd);
        return I;
    }
    public DataTable SelectOfficedocwithmissionId(string MissionId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectOfficedocwithmissionId";
        cmd.Parameters.Add(new SqlParameter("@MissionId", SqlDbType.NVarChar));
        cmd.Parameters["@MissionId"].Value = MissionId; // Convert.ToInt32(HttpContext.Current.Session["EmployeeId"].ToString()); // EmployeeId;

        dt = DatabaseCls1.FilleppAdapter(cmd); //.FillAdapter(cmd);
        return dt;
    }
    public DataTable SpObjectivedetailGridfill(String para, string filc)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SpObjectivedetailGridfill";
        cmd.Parameters.Add(new SqlParameter("@para", SqlDbType.NVarChar));
        cmd.Parameters["@para"].Value = para; // Convert.ToInt32(HttpContext.Current.Session["EmployeeId"].ToString()); // EmployeeId;
        //cmd.Parameters.Add(new SqlParameter("@BusinessID", SqlDbType.NVarChar));
        //cmd.Parameters["@BusinessID"].Value = BusinessID;
        //cmd.Parameters.Add(new SqlParameter("@Whid", SqlDbType.NVarChar));
        //cmd.Parameters["@Whid"].Value = Whid;
        //cmd.Parameters.Add(new SqlParameter("@EmployeemasterId", SqlDbType.NVarChar));
        //cmd.Parameters["@EmployeemasterId"].Value = EmployeemasterId;
        cmd.Parameters.Add(new SqlParameter("@filc", SqlDbType.NVarChar));
        cmd.Parameters["@filc"].Value = filc; //
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = "'"+HttpContext.Current.Session["Comid"].ToString()+"'"; // CompanyLoginId;
        dt = DatabaseCls1.FilleppAdapter(cmd); //.FillAdapter(cmd);
        return dt;

    }
    public DataTable SpObjectivemasterdd(String Whid, String Deptid, String BusinessID, String EmployeemasterId, String dateid)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SpObjectivemasterdd";
        cmd.Parameters.Add(new SqlParameter("@Deptid", SqlDbType.NVarChar));
        cmd.Parameters["@Deptid"].Value = Deptid; // Convert.ToInt32(HttpContext.Current.Session["EmployeeId"].ToString()); // EmployeeId;
        cmd.Parameters.Add(new SqlParameter("@BusinessID", SqlDbType.NVarChar));
        cmd.Parameters["@BusinessID"].Value = BusinessID;
        cmd.Parameters.Add(new SqlParameter("@Whid", SqlDbType.NVarChar));
        cmd.Parameters["@Whid"].Value = Whid;
        
        cmd.Parameters.Add(new SqlParameter("@EmployeemasterId", SqlDbType.NVarChar));
        cmd.Parameters["@EmployeemasterId"].Value = EmployeemasterId;

        cmd.Parameters.Add(new SqlParameter("@dateid", SqlDbType.NVarChar));
        cmd.Parameters["@dateid"].Value = dateid;

        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        dt = DatabaseCls1.FilleppAdapter(cmd); //.FillAdapter(cmd);
        return dt;

    }
    public DataTable SpObjectivemasterdd1(String Whid, String Deptid, String BusinessID, String EmployeemasterId, String dateid)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SpObjectivemasterdd1";
        cmd.Parameters.Add(new SqlParameter("@Deptid", SqlDbType.NVarChar));
        cmd.Parameters["@Deptid"].Value = Deptid; // Convert.ToInt32(HttpContext.Current.Session["EmployeeId"].ToString()); // EmployeeId;
        cmd.Parameters.Add(new SqlParameter("@BusinessID", SqlDbType.NVarChar));
        cmd.Parameters["@BusinessID"].Value = BusinessID;
        cmd.Parameters.Add(new SqlParameter("@Whid", SqlDbType.NVarChar));
        cmd.Parameters["@Whid"].Value = Whid;

        cmd.Parameters.Add(new SqlParameter("@EmployeemasterId", SqlDbType.NVarChar));
        cmd.Parameters["@EmployeemasterId"].Value = EmployeemasterId;

        cmd.Parameters.Add(new SqlParameter("@dateid", SqlDbType.NVarChar));
        cmd.Parameters["@dateid"].Value = dateid;

        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        dt = DatabaseCls1.FilleppAdapter(cmd); //.FillAdapter(cmd);
        return dt;

    }
    public DataTable SpObjectivemasterddfilter(String Whid, String Deptid, String BusinessID, String EmployeemasterId, int flag, String dateid)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SpObjectivemasterddfilter";
        cmd.Parameters.Add(new SqlParameter("@Deptid", SqlDbType.NVarChar));
        cmd.Parameters["@Deptid"].Value = Deptid; // Convert.ToInt32(HttpContext.Current.Session["EmployeeId"].ToString()); // EmployeeId;
        cmd.Parameters.Add(new SqlParameter("@BusinessID", SqlDbType.NVarChar));
        cmd.Parameters["@BusinessID"].Value = BusinessID;
        cmd.Parameters.Add(new SqlParameter("@Whid", SqlDbType.NVarChar));
        cmd.Parameters["@Whid"].Value = Whid;

        cmd.Parameters.Add(new SqlParameter("@EmployeemasterId", SqlDbType.NVarChar));
        cmd.Parameters["@EmployeemasterId"].Value = EmployeemasterId;

        cmd.Parameters.Add(new SqlParameter("@dateid", SqlDbType.NVarChar));
        cmd.Parameters["@dateid"].Value = dateid;

        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        cmd.Parameters.Add(new SqlParameter("@flag", SqlDbType.NVarChar));
        cmd.Parameters["@flag"].Value = flag; // CompanyLoginId;
        dt = DatabaseCls1.FilleppAdapter(cmd); //.FillAdapter(cmd);
        return dt;

    }
    public DataTable SpObjectivemasterddfilter1(String Whid, String Deptid, String BusinessID, String EmployeemasterId, int flag, String dateid)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SpObjectivemasterddfilter1";
        cmd.Parameters.Add(new SqlParameter("@Deptid", SqlDbType.NVarChar));
        cmd.Parameters["@Deptid"].Value = Deptid; // Convert.ToInt32(HttpContext.Current.Session["EmployeeId"].ToString()); // EmployeeId;
        cmd.Parameters.Add(new SqlParameter("@BusinessID", SqlDbType.NVarChar));
        cmd.Parameters["@BusinessID"].Value = BusinessID;
        cmd.Parameters.Add(new SqlParameter("@Whid", SqlDbType.NVarChar));
        cmd.Parameters["@Whid"].Value = Whid;

        cmd.Parameters.Add(new SqlParameter("@EmployeemasterId", SqlDbType.NVarChar));
        cmd.Parameters["@EmployeemasterId"].Value = EmployeemasterId;

        cmd.Parameters.Add(new SqlParameter("@dateid", SqlDbType.NVarChar));
        cmd.Parameters["@dateid"].Value = dateid;

        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        cmd.Parameters.Add(new SqlParameter("@flag", SqlDbType.NVarChar));
        cmd.Parameters["@flag"].Value = flag; // CompanyLoginId;
        dt = DatabaseCls1.FilleppAdapter(cmd); //.FillAdapter(cmd);
        return dt;

    }
    public DataTable SelectobjActualcost(string MissionId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectobjActualcost";
        cmd.Parameters.Add(new SqlParameter("@MasterId", SqlDbType.NVarChar));
        cmd.Parameters["@MasterId"].Value = MissionId; // Convert.ToInt32(HttpContext.Current.Session["EmployeeId"].ToString()); // EmployeeId;

        dt = DatabaseCls1.FilleppAdapter(cmd); //.FillAdapter(cmd);
        return dt;
    }
    public int Updateobjactualcost(string MissionId, decimal ActualCost, decimal ShortageExcess, int StatusId)
    {
        cmd = new SqlCommand();

        cmd.CommandText = "Updateobjactualcost";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@masterid", SqlDbType.NVarChar));
        cmd.Parameters["@masterid"].Value = MissionId; // Convert.ToInt32(HttpContext.Current.Session["EmployeeId"].ToString()); // EmployeeId;

        cmd.Parameters.Add(new SqlParameter("@ActualCost", SqlDbType.Decimal));
        cmd.Parameters["@ActualCost"].Value = ActualCost; // Convert.ToInt32(HttpContext.Current.Session["EmployeeId"].ToString()); // EmployeeId;

        cmd.Parameters.Add(new SqlParameter("@StatusId", SqlDbType.BigInt));
        cmd.Parameters["@StatusId"].Value = StatusId; 

        cmd.Parameters.Add(new SqlParameter("@ShortageExcess", SqlDbType.Decimal));
        cmd.Parameters["@ShortageExcess"].Value = ShortageExcess; // Convert.ToInt32(HttpContext.Current.Session["EmployeeId"].ToString()); // EmployeeId;

        int I = DatabaseCls1.ExecuteNonQueryep(cmd); //.FillAdapter(cmd);
        return I;
    }
    public DataTable SpObjectiveEvalutionGridfill(String para, string filc)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SpObjectiveEvalutionGridfill";
        cmd.Parameters.Add(new SqlParameter("@para", SqlDbType.NVarChar));
        cmd.Parameters["@para"].Value = para; // Convert.ToInt32(HttpContext.Current.Session["EmployeeId"].ToString()); // EmployeeId;

        cmd.Parameters.Add(new SqlParameter("@filc", SqlDbType.NVarChar));
        cmd.Parameters["@filc"].Value = filc; // Convert.ToInt32(HttpContext.Current.Session["EmployeeId"].ToString()); // EmployeeId;
     
        //cmd.Parameters.Add(new SqlParameter("@BusinessID", SqlDbType.NVarChar));
        //cmd.Parameters["@BusinessID"].Value = BusinessID;
        //cmd.Parameters.Add(new SqlParameter("@Whid", SqlDbType.NVarChar));
        //cmd.Parameters["@Whid"].Value = Whid;
        //cmd.Parameters.Add(new SqlParameter("@EmployeemasterId", SqlDbType.NVarChar));
        //cmd.Parameters["@EmployeemasterId"].Value = EmployeemasterId;

        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = "'"+HttpContext.Current.Session["Comid"].ToString()+"'"; // CompanyLoginId;
        dt = DatabaseCls1.FilleppAdapter(cmd); //.FillAdapter(cmd);
        return dt;

    }
    public DataTable SelectDoucmentMasterByID(int DocumentId)
    {

        SqlCommand cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.CommandText = "SelectDoucmentMasterByID";
        cmd.Parameters.Add(new SqlParameter("@DocumentId", SqlDbType.Int));
        cmd.Parameters["@DocumentId"].Value = DocumentId;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;

        dt = DatabaseCls1.FilleppAdapter(cmd); //.FillAdapter(cmd);
        return dt;


    }
    public DataTable SpObjectivebybusinessGridfill(String para)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SpObjectivebybusinessGridfill";
        cmd.Parameters.Add(new SqlParameter("@para", SqlDbType.NVarChar));
        cmd.Parameters["@para"].Value = para; // Convert.ToInt32(HttpContext.Current.Session["EmployeeId"].ToString()); // EmployeeId;
        //cmd.Parameters.Add(new SqlParameter("@BusinessID", SqlDbType.NVarChar));
        //cmd.Parameters["@BusinessID"].Value = BusinessID;
        //cmd.Parameters.Add(new SqlParameter("@Whid", SqlDbType.NVarChar));
        //cmd.Parameters["@Whid"].Value = Whid;
        //cmd.Parameters.Add(new SqlParameter("@EmployeemasterId", SqlDbType.NVarChar));
        //cmd.Parameters["@EmployeemasterId"].Value = EmployeemasterId;

        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        dt = DatabaseCls1.FilleppAdapter(cmd); //.FillAdapter(cmd);
        return dt;

    }
    public DataTable selectstatus()
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "selectstatus";
        
        dt = DatabaseCls1.FilleppAdapter(cmd); //.FillAdapter(cmd);
        return dt;

    }
    public DataTable SpObjectiveMasterwithId(string MissionId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SpObjectiveMasterwithId";
        cmd.Parameters.Add(new SqlParameter("@masterid", SqlDbType.NVarChar));
        cmd.Parameters["@masterid"].Value = MissionId; // Convert.ToInt32(HttpContext.Current.Session["EmployeeId"].ToString()); // EmployeeId;

        dt = DatabaseCls1.FilleppAdapter(cmd); //.FillAdapter(cmd);
        return dt;
    }
    public DataTable SpSTGMasterGridfill(String para, string filc)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SpSTGMasterGridfill";
        cmd.Parameters.Add(new SqlParameter("@para", SqlDbType.NVarChar));
        cmd.Parameters["@para"].Value = para;
        cmd.Parameters.Add(new SqlParameter("@filc", SqlDbType.NVarChar));
        cmd.Parameters["@filc"].Value = filc;
        // Convert.ToInt32(HttpContext.Current.Session["EmployeeId"].ToString()); // EmployeeId;
        //cmd.Parameters.Add(new SqlParameter("@BusinessID", SqlDbType.NVarChar));
        //cmd.Parameters["@BusinessID"].Value = BusinessID;
        //cmd.Parameters.Add(new SqlParameter("@Whid", SqlDbType.NVarChar));
        //cmd.Parameters["@Whid"].Value = Whid;
        //cmd.Parameters.Add(new SqlParameter("@EmployeemasterId", SqlDbType.NVarChar));
        //cmd.Parameters["@EmployeemasterId"].Value = EmployeemasterId;

        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = "'"+HttpContext.Current.Session["Comid"].ToString()+"'"; // CompanyLoginId;
        dt = DatabaseCls1.FilleppAdapter(cmd); //.FillAdapter(cmd);
        return dt;

    }
    public DataTable SelectOfficeManagerDocumentswithobjevid(string MissionevId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectOfficeManagerDocumentswithobjevid";
        cmd.Parameters.Add(new SqlParameter("@MissionevId", SqlDbType.NVarChar));
        cmd.Parameters["@MissionevId"].Value = MissionevId; // Convert.ToInt32(HttpContext.Current.Session["EmployeeId"].ToString()); // EmployeeId;

        dt = DatabaseCls1.FilleppAdapter(cmd); //.FillAdapter(cmd);
        return dt;
    }
    public DataTable SelectOfficeManagerDocumentswithobjinsid(string MissioninstructionId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectOfficeManagerDocumentswithobjinsid";
        cmd.Parameters.Add(new SqlParameter("@MissioninstructionId", SqlDbType.NVarChar));
        cmd.Parameters["@MissioninstructionId"].Value = MissioninstructionId; // Convert.ToInt32(HttpContext.Current.Session["EmployeeId"].ToString()); // EmployeeId;

        dt = DatabaseCls1.FilleppAdapter(cmd); //.FillAdapter(cmd);
        return dt;
    }
    public DataTable SelectOfficedocwithincid(string MissioninstructionId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectOfficedocwithincid";
        cmd.Parameters.Add(new SqlParameter("@MissioninstructionId", SqlDbType.NVarChar));
        cmd.Parameters["@MissioninstructionId"].Value = MissioninstructionId; // Convert.ToInt32(HttpContext.Current.Session["EmployeeId"].ToString()); // EmployeeId;

        dt = DatabaseCls1.FilleppAdapter(cmd); //.FillAdapter(cmd);
        return dt;
    }
    public DataTable SelectOfficedocwithevid(string MissionevId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectOfficedocwithevid";
        cmd.Parameters.Add(new SqlParameter("@MissionevId", SqlDbType.NVarChar));
        cmd.Parameters["@MissionevId"].Value = MissionevId; // Convert.ToInt32(HttpContext.Current.Session["EmployeeId"].ToString()); // EmployeeId;

        dt = DatabaseCls1.FilleppAdapter(cmd); //.FillAdapter(cmd);
        return dt;
    }
    public DataTable SpLTGMasterGridfill(String para, string filc)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SpLTGMasterGridfill";

        cmd.Parameters.Add(new SqlParameter("@para", SqlDbType.NVarChar));

        cmd.Parameters["@para"].Value = para;

        cmd.Parameters.Add(new SqlParameter("@filc", SqlDbType.NVarChar));
        cmd.Parameters["@filc"].Value = filc; //

        // Convert.ToInt32(HttpContext.Current.Session["EmployeeId"].ToString()); // EmployeeId;
        //cmd.Parameters.Add(new SqlParameter("@BusinessID", SqlDbType.NVarChar));
        //cmd.Parameters["@BusinessID"].Value = BusinessID;
        //cmd.Parameters.Add(new SqlParameter("@Whid", SqlDbType.NVarChar));
        //cmd.Parameters["@Whid"].Value = Whid;
        //cmd.Parameters.Add(new SqlParameter("@EmployeemasterId", SqlDbType.NVarChar));
        //cmd.Parameters["@EmployeemasterId"].Value = EmployeemasterId;

        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = "'"+HttpContext.Current.Session["Comid"].ToString()+"'"; // CompanyLoginId;
        dt = DatabaseCls1.FilleppAdapter(cmd); //.FillAdapter(cmd);
        return dt;

    }
    public DataTable SpLTGDetailGridfill(String para, string filc)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SpLTGDetailGridfill";
        cmd.Parameters.Add(new SqlParameter("@para", SqlDbType.NVarChar));
        cmd.Parameters["@para"].Value = para;

        cmd.Parameters.Add(new SqlParameter("@filc", SqlDbType.NVarChar));
        cmd.Parameters["@filc"].Value = filc; //


        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = "'"+HttpContext.Current.Session["Comid"].ToString()+"'"; // CompanyLoginId;
        dt = DatabaseCls1.FilleppAdapter(cmd); //.FillAdapter(cmd);
        return dt;

    }
    public DataTable SpLTGDetailGetDataById(string MasterId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SpLTGDetailGetDataById";
        cmd.Parameters.Add(new SqlParameter("@detailid", SqlDbType.NVarChar));
        cmd.Parameters["@detailid"].Value = MasterId; // Convert.ToInt32(HttpContext.Current.Session["EmployeeId"].ToString()); // EmployeeId;

        dt = DatabaseCls1.FilleppAdapter(cmd); //.FillAdapter(cmd);
        return dt;
    }
    public DataTable SpLTGMasterGetDataStructById(string MasterId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SpLTGMasterGetDataStructById";
        cmd.Parameters.Add(new SqlParameter("@MasterId", SqlDbType.NVarChar));
        cmd.Parameters["@MasterId"].Value = MasterId; // Convert.ToInt32(HttpContext.Current.Session["EmployeeId"].ToString()); // EmployeeId;

        dt = DatabaseCls1.FilleppAdapter(cmd); //.FillAdapter(cmd);
        return dt;
    }
    public DataTable SpLTGEvaluationGetDataById(string MasterId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SpLTGEvaluationGetDataById";
        cmd.Parameters.Add(new SqlParameter("@evaluationid", SqlDbType.NVarChar));
        cmd.Parameters["@evaluationid"].Value = MasterId; // Convert.ToInt32(HttpContext.Current.Session["EmployeeId"].ToString()); // EmployeeId;

        dt = DatabaseCls1.FilleppAdapter(cmd); //.FillAdapter(cmd);
        return dt;
    }
    public DataTable SpLTGEvaluationGridfill(String para, string filc)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SpLTGEvaluationGridfill";
        cmd.Parameters.Add(new SqlParameter("@para", SqlDbType.NVarChar));
        cmd.Parameters["@para"].Value = para;

        cmd.Parameters.Add(new SqlParameter("@filc", SqlDbType.NVarChar));
        cmd.Parameters["@filc"].Value = filc; //


        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = "'"+HttpContext.Current.Session["Comid"].ToString()+"'"; // CompanyLoginId;
        dt = DatabaseCls1.FilleppAdapter(cmd); //.FillAdapter(cmd);
        return dt;

    }
    public DataTable SelectOfficeManagerDocumentsbyltgmasterid(string LtgId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectOfficeManagerDocumentsbyltgmasterid";
        cmd.Parameters.Add(new SqlParameter("@LtgId", SqlDbType.NVarChar));
        cmd.Parameters["@LtgId"].Value = LtgId; // Convert.ToInt32(HttpContext.Current.Session["EmployeeId"].ToString()); // EmployeeId;

        dt = DatabaseCls1.FilleppAdapter(cmd); //.FillAdapter(cmd);
        return dt;
    }
    public DataTable SelectOfficeManagerDocumentswithltgdetailid(string Ltgdetail)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectOfficeManagerDocumentswithltgdetailid";
        cmd.Parameters.Add(new SqlParameter("@Ltgdetail", SqlDbType.NVarChar));
        cmd.Parameters["@Ltgdetail"].Value = Ltgdetail; // Convert.ToInt32(HttpContext.Current.Session["EmployeeId"].ToString()); // EmployeeId;

        dt = DatabaseCls1.FilleppAdapter(cmd); //.FillAdapter(cmd);
        return dt;
    }
    public DataTable SelectOfficeManagerDocumentswithltgevalutionid(string Ltgevalution)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectOfficeManagerDocumentswithltgevalutionid";
        cmd.Parameters.Add(new SqlParameter("@Ltgevalution", SqlDbType.NVarChar));
        cmd.Parameters["@Ltgevalution"].Value = Ltgevalution; // Convert.ToInt32(HttpContext.Current.Session["EmployeeId"].ToString()); // EmployeeId;

        dt = DatabaseCls1.FilleppAdapter(cmd); //.FillAdapter(cmd);
        return dt;
    }
    //mehul file on 6-8-2012
    public DataTable SpYMasterGridfill(String para, string filc)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SpYMasterGridfill";

        cmd.Parameters.Add(new SqlParameter("@para", SqlDbType.NVarChar));

        cmd.Parameters["@para"].Value = para;

        cmd.Parameters.Add(new SqlParameter("@filc", SqlDbType.NVarChar));
        cmd.Parameters["@filc"].Value = filc; //



        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = "'"+HttpContext.Current.Session["Comid"].ToString()+"'"; // CompanyLoginId;
        dt = DatabaseCls1.FilleppAdapter(cmd); //.FillAdapter(cmd);
        return dt;

    }
    public DataTable SelectOfficeManagerDocumentswithyearmasterid(string YgId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectOfficeManagerDocumentswithyearmasterid";
        cmd.Parameters.Add(new SqlParameter("@YgId", SqlDbType.NVarChar));
        cmd.Parameters["@YgId"].Value = YgId; // Convert.ToInt32(HttpContext.Current.Session["EmployeeId"].ToString()); // EmployeeId;

        dt = DatabaseCls1.FilleppAdapter(cmd); //.FillAdapter(cmd);
        return dt;
    }

    public DataTable SelectOfficeManagerDocumentswithannouncementid(string annid)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectOfficeManagerDocumentswithannouncementid";
        cmd.Parameters.Add(new SqlParameter("@annid", SqlDbType.NVarChar));
        cmd.Parameters["@annid"].Value = annid; // Convert.ToInt32(HttpContext.Current.Session["EmployeeId"].ToString()); // EmployeeId;

        dt = DatabaseCls1.FilleppAdapter(cmd); //.FillAdapter(cmd);
        return dt;
    }

    public DataTable SpYMasterGetDataStructById(string MasterId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SpYMasterGetDataStructById";
        cmd.Parameters.Add(new SqlParameter("@MasterId", SqlDbType.NVarChar));
        cmd.Parameters["@MasterId"].Value = MasterId; // Convert.ToInt32(HttpContext.Current.Session["EmployeeId"].ToString()); // EmployeeId;

        dt = DatabaseCls1.FilleppAdapter(cmd); //.FillAdapter(cmd);
        return dt;
    }
    //mehul 8-1-2012
    public DataTable SpYDetailGridfill(String para, string filc)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SpYDetailGridfill";

        cmd.Parameters.Add(new SqlParameter("@para", SqlDbType.NVarChar));

        cmd.Parameters["@para"].Value = para;

        cmd.Parameters.Add(new SqlParameter("@filc", SqlDbType.NVarChar));
        cmd.Parameters["@filc"].Value = filc; //



        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = "'"+HttpContext.Current.Session["Comid"].ToString()+"'"; // CompanyLoginId;
        dt = DatabaseCls1.FilleppAdapter(cmd); //.FillAdapter(cmd);
        return dt;

    }
    public DataTable SelectOfficeManagerDocumentswithyeardetailid(string Ydetail)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectOfficeManagerDocumentswithyeardetailid";
        cmd.Parameters.Add(new SqlParameter("@Ydetail", SqlDbType.NVarChar));
        cmd.Parameters["@Ydetail"].Value = Ydetail; // Convert.ToInt32(HttpContext.Current.Session["EmployeeId"].ToString()); // EmployeeId;

        dt = DatabaseCls1.FilleppAdapter(cmd); //.FillAdapter(cmd);
        return dt;
    }
    //mehul 8-4-2012
    public DataTable SpYDetailGetDataStructById(string DetailId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SpYDetailGetDataStructById";
        cmd.Parameters.Add(new SqlParameter("@DetailId", SqlDbType.NVarChar));
        cmd.Parameters["@DetailId"].Value = DetailId; // Convert.ToInt32(HttpContext.Current.Session["EmployeeId"].ToString()); // EmployeeId;

        dt = DatabaseCls1.FilleppAdapter(cmd); //.FillAdapter(cmd);
        return dt;
    }
    public DataTable SpYEvaluationGetDataStructById(string EvaluationId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SpYEvaluationGetDataStructById";
        cmd.Parameters.Add(new SqlParameter("@EvaluationId", SqlDbType.NVarChar));
        cmd.Parameters["@EvaluationId"].Value = EvaluationId; // Convert.ToInt32(HttpContext.Current.Session["EmployeeId"].ToString()); // EmployeeId;

        dt = DatabaseCls1.FilleppAdapter(cmd); //.FillAdapter(cmd);
        return dt;
    }
    public DataTable SpYEvaluationGridfill(String para, string filc)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SpYEvaluationGridfill";

        cmd.Parameters.Add(new SqlParameter("@para", SqlDbType.NVarChar));

        cmd.Parameters["@para"].Value = para;

        cmd.Parameters.Add(new SqlParameter("@filc", SqlDbType.NVarChar));
        cmd.Parameters["@filc"].Value = filc; //



        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = "'"+HttpContext.Current.Session["Comid"].ToString()+"'"; // CompanyLoginId;
        dt = DatabaseCls1.FilleppAdapter(cmd); //.FillAdapter(cmd);
        return dt;

    }
    public DataTable SelectOfficeManagerDocumentswithyearevaluationid(string Yeevalution)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectOfficeManagerDocumentswithyearevaluationid";
        cmd.Parameters.Add(new SqlParameter("@Yeevalution", SqlDbType.NVarChar));
        cmd.Parameters["@Yeevalution"].Value = Yeevalution; // Convert.ToInt32(HttpContext.Current.Session["EmployeeId"].ToString()); // EmployeeId;

        dt = DatabaseCls1.FilleppAdapter(cmd); //.FillAdapter(cmd);
        return dt;
    }

    //mehul 8-9-2012
    public DataTable SpObjectiveMasterGridfillforstrategy(String para, string filc)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SpObjectiveMasterGridfillforstrategy";

        cmd.Parameters.Add(new SqlParameter("@para", SqlDbType.NVarChar));

        cmd.Parameters["@para"].Value = para;

        cmd.Parameters.Add(new SqlParameter("@filc", SqlDbType.NVarChar));
        cmd.Parameters["@filc"].Value = filc; //



        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        dt = DatabaseCls1.FilleppAdapter(cmd); //.FillAdapter(cmd);
        return dt;

    }
    public DataTable SpLTGMasterGridfillforstrategy(String para, string filc)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SpLTGMasterGridfillforstrategy";

        cmd.Parameters.Add(new SqlParameter("@para", SqlDbType.NVarChar));

        cmd.Parameters["@para"].Value = para;

        cmd.Parameters.Add(new SqlParameter("@filc", SqlDbType.NVarChar));
        cmd.Parameters["@filc"].Value = filc; //



        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        dt = DatabaseCls1.FilleppAdapter(cmd); //.FillAdapter(cmd);
        return dt;

    }

    public DataTable SpSTGMasterGridfillforstrategy(String para, string filc)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SpSTGMasterGridfillforstrategy";

        cmd.Parameters.Add(new SqlParameter("@para", SqlDbType.NVarChar));

        cmd.Parameters["@para"].Value = para;

        cmd.Parameters.Add(new SqlParameter("@filc", SqlDbType.NVarChar));
        cmd.Parameters["@filc"].Value = filc; //



        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        dt = DatabaseCls1.FilleppAdapter(cmd); //.FillAdapter(cmd);
        return dt;

    }
    public DataTable SpYMasterGridfillforstrategy(String para, string filc)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SpYMasterGridfillforstrategy";

        cmd.Parameters.Add(new SqlParameter("@para", SqlDbType.NVarChar));

        cmd.Parameters["@para"].Value = para;

        cmd.Parameters.Add(new SqlParameter("@filc", SqlDbType.NVarChar));
        cmd.Parameters["@filc"].Value = filc; //



        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        dt = DatabaseCls1.FilleppAdapter(cmd); //.FillAdapter(cmd);
        return dt;

    }
    public DataTable SpMMasterGridfillforstrategy(String para, string filc)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SpMMasterGridfillforstrategy";

        cmd.Parameters.Add(new SqlParameter("@para", SqlDbType.NVarChar));

        cmd.Parameters["@para"].Value = para;

        cmd.Parameters.Add(new SqlParameter("@filc", SqlDbType.NVarChar));
        cmd.Parameters["@filc"].Value = filc; //



        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        dt = DatabaseCls1.FilleppAdapter(cmd); //.FillAdapter(cmd);
        return dt;

    }
    public DataTable SpStrategyMasterGetDataStructById(string MasterId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SpStrategyMasterGetDataStructById";
        cmd.Parameters.Add(new SqlParameter("@MasterId", SqlDbType.NVarChar));
        cmd.Parameters["@MasterId"].Value = MasterId; // Convert.ToInt32(HttpContext.Current.Session["EmployeeId"].ToString()); // EmployeeId;

        dt = DatabaseCls1.FilleppAdapter(cmd); //.FillAdapter(cmd);
        return dt;
    }
    //mehul 13-8-2012
    public DataTable SelectOfficeManagerDocumentswithstrategymasterid(string StrategyId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectOfficeManagerDocumentswithstrategymasterid";
        cmd.Parameters.Add(new SqlParameter("@StrategyId", SqlDbType.NVarChar));
        cmd.Parameters["@StrategyId"].Value = StrategyId; // Convert.ToInt32(HttpContext.Current.Session["EmployeeId"].ToString()); // EmployeeId;

        dt = DatabaseCls1.FilleppAdapter(cmd); //.FillAdapter(cmd);
        return dt;
    }

    public DataTable SelectOfficeManagerDocumentswithcandidateid(string CandidateId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectOfficeManagerDocumentswithcandidateid";
        cmd.Parameters.Add(new SqlParameter("@CandidateId", SqlDbType.NVarChar));
        cmd.Parameters["@CandidateId"].Value = CandidateId; // Convert.ToInt32(HttpContext.Current.Session["EmployeeId"].ToString()); // EmployeeId;

        dt = DatabaseCls1.FilleppAdapter(cmd); //.FillAdapter(cmd);
        return dt;
    }

    public DataTable SpStrategyDetailGetDataStructById(string MasterId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SpStrategyDetailGetDataStructById";
        cmd.Parameters.Add(new SqlParameter("@DetailId", SqlDbType.NVarChar));
        cmd.Parameters["@DetailId"].Value = MasterId; // Convert.ToInt32(HttpContext.Current.Session["EmployeeId"].ToString()); // EmployeeId;

        dt = DatabaseCls1.FilleppAdapter(cmd); //.FillAdapter(cmd);
        return dt;
    }

    public DataTable SpObjectiveMasterGridfillforstrategydetail(String para, string filc)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SpObjectiveMasterGridfillforstrategydetail";

        cmd.Parameters.Add(new SqlParameter("@para", SqlDbType.NVarChar));

        cmd.Parameters["@para"].Value = para;

        cmd.Parameters.Add(new SqlParameter("@filc", SqlDbType.NVarChar));
        cmd.Parameters["@filc"].Value = filc; //



        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        dt = DatabaseCls1.FilleppAdapter(cmd); //.FillAdapter(cmd);
        return dt;

    }
    public DataTable SpLTGMasterGridfillforstrategydetail(String para, string filc)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SpLTGMasterGridfillforstrategydetail";

        cmd.Parameters.Add(new SqlParameter("@para", SqlDbType.NVarChar));

        cmd.Parameters["@para"].Value = para;

        cmd.Parameters.Add(new SqlParameter("@filc", SqlDbType.NVarChar));
        cmd.Parameters["@filc"].Value = filc; //



        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        dt = DatabaseCls1.FilleppAdapter(cmd); //.FillAdapter(cmd);
        return dt;

    }
    public DataTable SpSTGMasterGridfillforstrategydetail(String para, string filc)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SpSTGMasterGridfillforstrategydetail";

        cmd.Parameters.Add(new SqlParameter("@para", SqlDbType.NVarChar));

        cmd.Parameters["@para"].Value = para;

        cmd.Parameters.Add(new SqlParameter("@filc", SqlDbType.NVarChar));
        cmd.Parameters["@filc"].Value = filc; //



        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        dt = DatabaseCls1.FilleppAdapter(cmd); //.FillAdapter(cmd);
        return dt;

    }
    public DataTable SpYMasterGridfillforstrategydetail(String para, string filc)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SpYMasterGridfillforstrategydetail";

        cmd.Parameters.Add(new SqlParameter("@para", SqlDbType.NVarChar));

        cmd.Parameters["@para"].Value = para;

        cmd.Parameters.Add(new SqlParameter("@filc", SqlDbType.NVarChar));
        cmd.Parameters["@filc"].Value = filc; //



        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        dt = DatabaseCls1.FilleppAdapter(cmd); //.FillAdapter(cmd);
        return dt;

    }
    public DataTable SpMMasterGridfillforstrategydetail(String para, string filc)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SpMMasterGridfillforstrategydetail";

        cmd.Parameters.Add(new SqlParameter("@para", SqlDbType.NVarChar));

        cmd.Parameters["@para"].Value = para;

        cmd.Parameters.Add(new SqlParameter("@filc", SqlDbType.NVarChar));
        cmd.Parameters["@filc"].Value = filc; //



        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        dt = DatabaseCls1.FilleppAdapter(cmd); //.FillAdapter(cmd);
        return dt;

    }

    public DataTable SelectOfficeManagerDocumentswithstrategydetailid(string Strategydetail)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectOfficeManagerDocumentswithstrategydetailid";
        cmd.Parameters.Add(new SqlParameter("@Strategydetail", SqlDbType.NVarChar));
        cmd.Parameters["@Strategydetail"].Value = Strategydetail; // Convert.ToInt32(HttpContext.Current.Session["EmployeeId"].ToString()); // EmployeeId;

        dt = DatabaseCls1.FilleppAdapter(cmd); //.FillAdapter(cmd);
        return dt;
    }
    public DataTable SpStrategyEvaluationGetDataStructById(string EvaluationId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SpStrategyEvaluationGetDataStructById";
        cmd.Parameters.Add(new SqlParameter("@EvaluationId", SqlDbType.NVarChar));
        cmd.Parameters["@EvaluationId"].Value = EvaluationId; // Convert.ToInt32(HttpContext.Current.Session["EmployeeId"].ToString()); // EmployeeId;

        dt = DatabaseCls1.FilleppAdapter(cmd); //.FillAdapter(cmd);
        return dt;
    }

    public DataTable SpObjectiveMasterGridfillforstrategyevaluation(String para, string filc)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SpObjectiveMasterGridfillforstrategyevaluation";

        cmd.Parameters.Add(new SqlParameter("@para", SqlDbType.NVarChar));

        cmd.Parameters["@para"].Value = para;

        cmd.Parameters.Add(new SqlParameter("@filc", SqlDbType.NVarChar));
        cmd.Parameters["@filc"].Value = filc; //



        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        dt = DatabaseCls1.FilleppAdapter(cmd); //.FillAdapter(cmd);
        return dt;

    }
    public DataTable SpLTGMasterGridfillforstrategyevaluation(String para, string filc)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SpLTGMasterGridfillforstrategyevaluation";

        cmd.Parameters.Add(new SqlParameter("@para", SqlDbType.NVarChar));

        cmd.Parameters["@para"].Value = para;

        cmd.Parameters.Add(new SqlParameter("@filc", SqlDbType.NVarChar));
        cmd.Parameters["@filc"].Value = filc; //



        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        dt = DatabaseCls1.FilleppAdapter(cmd); //.FillAdapter(cmd);
        return dt;

    }
    public DataTable SpSTGMasterGridfillforstrategyevaluation(String para, string filc)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SpSTGMasterGridfillforstrategyevaluation";

        cmd.Parameters.Add(new SqlParameter("@para", SqlDbType.NVarChar));

        cmd.Parameters["@para"].Value = para;

        cmd.Parameters.Add(new SqlParameter("@filc", SqlDbType.NVarChar));
        cmd.Parameters["@filc"].Value = filc; //



        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        dt = DatabaseCls1.FilleppAdapter(cmd); //.FillAdapter(cmd);
        return dt;

    }
    public DataTable SpYMasterGridfillforstrategyevaluation(String para, string filc)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SpYMasterGridfillforstrategyevaluation";

        cmd.Parameters.Add(new SqlParameter("@para", SqlDbType.NVarChar));

        cmd.Parameters["@para"].Value = para;

        cmd.Parameters.Add(new SqlParameter("@filc", SqlDbType.NVarChar));
        cmd.Parameters["@filc"].Value = filc; //



        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        dt = DatabaseCls1.FilleppAdapter(cmd); //.FillAdapter(cmd);
        return dt;

    }
    public DataTable SpMMasterGridfillforstrategyevaluation(String para, string filc)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SpMMasterGridfillforstrategyevaluation";

        cmd.Parameters.Add(new SqlParameter("@para", SqlDbType.NVarChar));

        cmd.Parameters["@para"].Value = para;

        cmd.Parameters.Add(new SqlParameter("@filc", SqlDbType.NVarChar));
        cmd.Parameters["@filc"].Value = filc; //



        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        dt = DatabaseCls1.FilleppAdapter(cmd); //.FillAdapter(cmd);
        return dt;

    }
    public DataTable SelectOfficeManagerDocumentswithstrategyevaluationId(string Strategevaution)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectOfficeManagerDocumentswithstrategyevaluationId";
        cmd.Parameters.Add(new SqlParameter("@Strategevaution", SqlDbType.NVarChar));
        cmd.Parameters["@Strategevaution"].Value = Strategevaution; // Convert.ToInt32(HttpContext.Current.Session["EmployeeId"].ToString()); // EmployeeId;

        dt = DatabaseCls1.FilleppAdapter(cmd); //.FillAdapter(cmd);
        return dt;
    }
    public Int32 InsertDocumentMasterForTempTask(Int32 DocumentTypeId, Int32 DocumentUploadTypeId, DateTime DocumentUploadDate, String DocumentName, String DocumentTitle, String Description, Int32 PartyId, String DocumentRefNo, Decimal DocumentAmount, Int32 EmployeeId, DateTime DocumentDate, String FileExtensionType, Int32 TableRowTaskId)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "InsertDocumentMasterForTempTask";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@DocumentTypeId", SqlDbType.Int));
        cmd.Parameters["@DocumentTypeId"].Value = DocumentTypeId;
        cmd.Parameters.Add(new SqlParameter("@DocumentUploadTypeId", SqlDbType.Int));
        cmd.Parameters["@DocumentUploadTypeId"].Value = DocumentUploadTypeId;
        cmd.Parameters.Add(new SqlParameter("@DocumentUploadDate", SqlDbType.DateTime));
        cmd.Parameters["@DocumentUploadDate"].Value = DocumentUploadDate;
        cmd.Parameters.Add(new SqlParameter("@DocumentName", SqlDbType.NVarChar));
        cmd.Parameters["@DocumentName"].Value = DocumentName;
        cmd.Parameters.Add(new SqlParameter("@DocumentTitle", SqlDbType.NVarChar));
        cmd.Parameters["@DocumentTitle"].Value = DocumentTitle;
        cmd.Parameters.Add(new SqlParameter("@Description", SqlDbType.NVarChar));
        cmd.Parameters["@Description"].Value = Description;
        cmd.Parameters.Add(new SqlParameter("@PartyId", SqlDbType.Int));
        cmd.Parameters["@PartyId"].Value = PartyId;
        cmd.Parameters.Add(new SqlParameter("@DocumentRefNo", SqlDbType.NVarChar));
        cmd.Parameters["@DocumentRefNo"].Value = DocumentRefNo;
        cmd.Parameters.Add(new SqlParameter("@DocumentAmount", SqlDbType.Decimal));
        cmd.Parameters["@DocumentAmount"].Value = DocumentAmount;
        cmd.Parameters.Add(new SqlParameter("@EmployeeId", SqlDbType.Int));
        cmd.Parameters["@EmployeeId"].Value = EmployeeId;
        cmd.Parameters.Add(new SqlParameter("@DocumentDate", SqlDbType.DateTime));
        cmd.Parameters["@DocumentDate"].Value = DocumentDate;

        cmd.Parameters.Add(new SqlParameter("@FileExtensionType", SqlDbType.NVarChar));
        cmd.Parameters["@FileExtensionType"].Value = FileExtensionType;

        cmd.Parameters.Add(new SqlParameter("@TableRowTaskId", SqlDbType.Int));
        cmd.Parameters["@TableRowTaskId"].Value = TableRowTaskId;

        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;

        //Convert.ToString(Session["UserId"])
        cmd.Parameters.Add(new SqlParameter("@UserId", SqlDbType.NVarChar));
        cmd.Parameters["@UserId"].Value = HttpContext.Current.Session["UserId"].ToString();

        cmd.Parameters.Add(new SqlParameter("@DocumentId", SqlDbType.Int));
        cmd.Parameters["@DocumentId"].Direction = ParameterDirection.Output;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls1.ExecuteNonQueryep(cmd);
        result = Convert.ToInt32(cmd.Parameters["@DocumentId"].Value);
        return (result);
    }
    //mehul 22-8-2012
    public DataTable SelectOfficeManagerDocumentswithtaskmasterid(string taskid)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectOfficeManagerDocumentswithtaskmasterid";
        cmd.Parameters.Add(new SqlParameter("@taskid", SqlDbType.NVarChar));
        cmd.Parameters["@taskid"].Value = taskid; // Convert.ToInt32(HttpContext.Current.Session["EmployeeId"].ToString()); // EmployeeId;

        dt = DatabaseCls1.FilleppAdapter(cmd); //.FillAdapter(cmd);
        return dt;
    }
}
