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
/// Summary description for MessageCls
/// </summary>
public class MessageCls
{
    SqlCommand cmd;
    DataTable dt;
	public MessageCls()
	{
		//
		// TODO: Add constructor logic here
		//
	}
   //=== added by alkesh 19-02-2009
    public DataTable SelectDocumentMasterByID(int DocumentId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectDoucmentMasterByID";
        cmd.Parameters.Add(new SqlParameter("@DocumentId", SqlDbType.Int));
        cmd.Parameters["@DocumentId"].Value = DocumentId;
        dt = DatabaseCls.FillAdapter(cmd);
        return dt;
    }
    public bool InsertSpamEmail(String SpamEmailId, Boolean AllowEmail, Int32 MsgId, Int32 Partyid)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "InsertSpamEmail";
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.Add(new SqlParameter("@SpamEmailId", SqlDbType.NVarChar));
        cmd.Parameters["@SpamEmailId"].Value = SpamEmailId;

        cmd.Parameters.Add(new SqlParameter("@MsgId", SqlDbType.Int));
        cmd.Parameters["@MsgId"].Value = MsgId;

        cmd.Parameters.Add(new SqlParameter("@AllowEmail", SqlDbType.Bit));
        cmd.Parameters["@AllowEmail"].Value = AllowEmail;

        cmd.Parameters.Add(new SqlParameter("@PartyId", SqlDbType.Int));
        cmd.Parameters["@PartyId"].Value = Partyid;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;

        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;

        Int32 result = DatabaseCls.ExecuteNonQuery(cmd);
        return (result != -1);

    }
    public bool UpdateSpamEmail(Int32 ID, String EmailId, Boolean Allow)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "UpdateSpamEmail";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@ID", SqlDbType.Int));
        cmd.Parameters["@ID"].Value = ID;
        cmd.Parameters.Add(new SqlParameter("@EmailID", SqlDbType.NVarChar));
        cmd.Parameters["@EmailID"].Value = EmailId;
        cmd.Parameters.Add(new SqlParameter("@AllowEmail", SqlDbType.Bit));
        cmd.Parameters["@AllowEmail"].Value = Allow;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls.ExecuteNonQuery(cmd);
        result = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value.ToString());
        return (result != -1);
        //  return (result != -1);
    }
    public DataTable SelectPartyEmailFromMsgId(Int32 MsgId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectPartyEmailFromMsgId";
        cmd.Parameters.Add(new SqlParameter("@MsgId", SqlDbType.Int));
        cmd.Parameters["@MsgId"].Value = MsgId;
        dt = DatabaseCls.FillAdapter(cmd);
        return dt;
    }
    public DataTable SelectMsgIdUsingMsgDetailIdExt(Int32 MsgDetailId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectMsgIdUsingMsgDetailIdExt";
        cmd.Parameters.Add(new SqlParameter("@MsgDetailId", SqlDbType.Int));
        cmd.Parameters["@MsgDetailId"].Value = MsgDetailId;
        dt = DatabaseCls.FillAdapter(cmd);
        return dt;
    }
    public DataTable SelectMsgforDeleteBoxExt(Int32 ToPartyId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectMsgforDeleteBoxExt";
        cmd.Parameters.Add(new SqlParameter("@ToPartyId", SqlDbType.Int));
        cmd.Parameters["@ToPartyId"].Value = ToPartyId;
        dt = DatabaseCls.FillAdapter(cmd);
        return dt;
    }
    public bool UpdateMsgMasterforSendDeleteExt(Int32 MsgId, String MsgStatus)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "UpdateMsgMasterforSendDeleteExt";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@MsgId", SqlDbType.Int));
        cmd.Parameters["@MsgId"].Value = MsgId;
        cmd.Parameters.Add(new SqlParameter("@MsgStatus", SqlDbType.NVarChar));
        cmd.Parameters["@MsgStatus"].Value = MsgStatus;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls.ExecuteNonQuery(cmd);
        result = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value.ToString());
        return (result != -1);
    }

    public DataTable SelectMsgDetailforSentPartyListExt(Int32 MsgId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectMsgDetailforSentPartyListExt";
        cmd.Parameters.Add(new SqlParameter("@MsgId", SqlDbType.Int));
        cmd.Parameters["@MsgId"].Value = MsgId;
        dt = DatabaseCls.FillAdapter(cmd);
        return dt;
    }
    public DataTable SelectMsgforSendBoxExt(Int32 FromPartyId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectMsgforSendBoxExt";
        cmd.Parameters.Add(new SqlParameter("@FromPartyId", SqlDbType.Int));
        cmd.Parameters["@FromPartyId"].Value = FromPartyId;
        dt = DatabaseCls.FillAdapter(cmd);
        return dt;
    }
    public bool UpdateMsgMasterforDraftSendExt(Int32 MsgId)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "UpdateMsgMasterforDraftSendExt";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@MsgId", SqlDbType.Int));
        cmd.Parameters["@MsgId"].Value = MsgId;

        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls.ExecuteNonQuery(cmd);
        result = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value.ToString());
        return (result != -1);
        //  return (result != -1);
    }
    public DataTable Empid(String Whid)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = " Select distinct EmployeeMaster.EmployeeMasterID from EmployeeMaster inner join Party_master on Party_master.PartyID=EmployeeMaster.PartyID inner join User_master on User_master.PartyID=Party_master.PartyID  inner join PartytTypeMaster on PartytTypeMaster.PartyTypeId=Party_master.PartyTypeId where PartytTypeMaster.PartType='Admin' and EmployeeMaster.Whid='" + Whid + "'";
           
        //cmd.CommandText = " Select distinct EmployeeMaster.EmployeeMasterID from EmployeeMaster inner join Party_master on Party_master.PartyID=EmployeeMaster.PartyID inner join User_master on User_master.PartyID=Party_master.PartyID where User_master.UserID='" + HttpContext.Current.Session["UserId"].ToString() + "'";
        dt = DatabaseCls.FillAdapterque(cmd);
        return dt;
    }
    public Int32 InsertMsgMaster(Int32 FromPartyId, String MsgSubject, String MsgDetail, String Signature, Boolean Picture, String str)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "InsertMsgMaster";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@FromPartyId", SqlDbType.Int));
        cmd.Parameters["@FromPartyId"].Direction = ParameterDirection.Input;
        cmd.Parameters["@FromPartyId"].Value = FromPartyId;
        cmd.Parameters.Add(new SqlParameter("@MsgSubject", SqlDbType.NVarChar));
        cmd.Parameters["@MsgSubject"].Value = MsgSubject;
        cmd.Parameters.Add(new SqlParameter("@MsgDetail", SqlDbType.NVarChar));
        cmd.Parameters["@MsgDetail"].Value = MsgDetail;

        cmd.Parameters.Add(new SqlParameter("@Signature", SqlDbType.NVarChar));
        cmd.Parameters["@Signature"].Value = Signature;

        cmd.Parameters.Add(new SqlParameter("@str", SqlDbType.NVarChar));
        cmd.Parameters["@str"].Value = str;

        cmd.Parameters.Add(new SqlParameter("@Picture", SqlDbType.Bit));
        cmd.Parameters["@Picture"].Value = Picture;

        cmd.Parameters.Add(new SqlParameter("@MsgId", SqlDbType.Int));
        cmd.Parameters["@MsgId"].Direction = ParameterDirection.Output;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls.ExecuteNonQuery(cmd);
        result = Convert.ToInt32(cmd.Parameters["@MsgId"].Value.ToString());
        return result;
    }

    public bool InsertMsgFileAttachDetail(Int32 MsgId, String FileName)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "InsertMsgFileAttachDetail";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@MsgId", SqlDbType.Int));
        cmd.Parameters["@MsgId"].Direction = ParameterDirection.Input;
        cmd.Parameters["@MsgId"].Value = MsgId;
        cmd.Parameters.Add(new SqlParameter("@FileName", SqlDbType.NVarChar));
        cmd.Parameters["@FileName"].Value = FileName;
        
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls.ExecuteNonQuery(cmd);
        result = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value.ToString());
        return (result != -1);
          
    }
    public Int32   InsertMsgDetail(Int32 MsgId, Int32 ToPartyId)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "InsertMsgDetail";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@MsgId", SqlDbType.Int));
        cmd.Parameters["@MsgId"].Direction = ParameterDirection.Input;
        cmd.Parameters["@MsgId"].Value = MsgId;
        cmd.Parameters.Add(new SqlParameter("@ToPartyId", SqlDbType.Int));
        cmd.Parameters["@ToPartyId"].Direction = ParameterDirection.Input;
        cmd.Parameters["@ToPartyId"].Value = ToPartyId;
        cmd.Parameters.Add(new SqlParameter("@MsgDetailId", SqlDbType.Int));
        cmd.Parameters["@MsgDetailId"].Direction = ParameterDirection.Output;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32   result = DatabaseCls.ExecuteNonQuery(cmd);
        result = Convert.ToInt32(cmd.Parameters["@MsgDetailId"].Value.ToString());
        return result;
      //  return (result != -1);
    }
    public bool UpdateMsgDetail(Int32 MsgDetailId, Int32 MsgStatusId)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "UpdateMsgDetail";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@MsgDetailId", SqlDbType.Int));
        cmd.Parameters["@MsgDetailId"].Value = @MsgDetailId;
        cmd.Parameters.Add(new SqlParameter("@MsgStatusId", SqlDbType.Int));
        cmd.Parameters["@MsgStatusId"].Value = MsgStatusId;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls.ExecuteNonQuery(cmd);
        result = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value.ToString());
        return (result != -1);
        //  return (result != -1);
    }



    public bool UpdateMsgDetailusingMsgId(Int32 MsgId, Int32 MsgStatusId)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "UpdateMsgDetailusingMsgId";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@MsgId", SqlDbType.Int));
        cmd.Parameters["@MsgId"].Value = MsgId;
        cmd.Parameters.Add(new SqlParameter("@MsgStatusId", SqlDbType.Int));
        cmd.Parameters["@MsgStatusId"].Value = MsgStatusId;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls.ExecuteNonQuery(cmd);
        result = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value.ToString());
        return (result != -1);
        //  return (result != -1);
    }
    public bool UpdateMsgDetailusingMsgIdExt(Int32 MsgId, Int32 MsgStatusId)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "UpdateMsgDetailusingMsgIdExt";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@MsgId", SqlDbType.Int));
        cmd.Parameters["@MsgId"].Value = MsgId;
        cmd.Parameters.Add(new SqlParameter("@MsgStatusId", SqlDbType.Int));
        cmd.Parameters["@MsgStatusId"].Value = MsgStatusId;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls.ExecuteNonQuery(cmd);
        result = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value.ToString());
        return (result != -1);
        //  return (result != -1);
    }
    public bool UpdateMsgMasterforDraftSend(Int32 MsgId )
    {
        cmd = new SqlCommand();
        cmd.CommandText = "UpdateMsgMasterforDraftSend";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@MsgId", SqlDbType.Int));
        cmd.Parameters["@MsgId"].Value = MsgId;
        
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls.ExecuteNonQuery(cmd);
        result = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value.ToString());
        return (result != -1);
        //  return (result != -1);
    }

    public DataTable SelectMsgforInbox(Int32 ToPartyId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectMsgforInbox";
        cmd.Parameters.Add(new SqlParameter("@ToPartyId", SqlDbType.Int));
        cmd.Parameters["@ToPartyId"].Value = ToPartyId;
        dt = DatabaseCls.FillAdapter(cmd);
        return dt;
    }


    public DataTable SelectMsgforDeleteBox(Int32 ToPartyId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectMsgforDeleteBox";
        cmd.Parameters.Add(new SqlParameter("@ToPartyId", SqlDbType.Int));
        cmd.Parameters["@ToPartyId"].Value = ToPartyId;
        dt = DatabaseCls.FillAdapter(cmd);
        return dt;
    }
    public DataTable SelectMsgforDetail(Int32 MsgDetailId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectMsgforDetail";
        cmd.Parameters.Add(new SqlParameter("@MsgDetailId", SqlDbType.Int));
        cmd.Parameters["@MsgDetailId"].Value = MsgDetailId;
        dt = DatabaseCls.FillAdapter(cmd);
        return dt;
    }
    public DataTable SelectMsgforDetailExt(Int32 MsgDetailId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectMsgforDetailExt";
        cmd.Parameters.Add(new SqlParameter("@MsgDetailId", SqlDbType.Int));
        cmd.Parameters["@MsgDetailId"].Value = MsgDetailId;
        dt = DatabaseCls.FillAdapter(cmd);
        return dt;
    }
    public DataTable SelectPartynamebypartyid(Int32 Partyid)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectPartynamebypartyid";
        cmd.Parameters.Add(new SqlParameter("@PartyId", SqlDbType.Int));
        cmd.Parameters["@PartyId"].Value = Partyid;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        dt = DatabaseCls.FillAdapter(cmd);
        return dt;
    }
    public DataTable SelectMsgforDraft(Int32 FromPartyId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectMsgforDraft";
        cmd.Parameters.Add(new SqlParameter("@FromPartyId", SqlDbType.Int));
        cmd.Parameters["@FromPartyId"].Value = FromPartyId;
        dt = DatabaseCls.FillAdapter(cmd);
        return dt;
    }
    public DataTable SelectMsgforDraftExt(Int32 FromPartyId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectMsgforDraftExt";
        cmd.Parameters.Add(new SqlParameter("@FromPartyId", SqlDbType.Int));
        cmd.Parameters["@FromPartyId"].Value = FromPartyId;
        dt = DatabaseCls.FillAdapter(cmd);
        return dt;
    }
    public DataTable SelectMsgforDraftDetail(Int32 MsgId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectMsgforDraftDetail";
        cmd.Parameters.Add(new SqlParameter("@MsgId", SqlDbType.Int));
        cmd.Parameters["@MsgId"].Value = MsgId;
        dt = DatabaseCls.FillAdapter(cmd);
        return dt;
    }


    public DataTable SelectMsgforFileAttach(Int32 MsgId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectMsgforFileAttach";
        cmd.Parameters.Add(new SqlParameter("@MsgId", SqlDbType.Int));
        cmd.Parameters["@MsgId"].Value = MsgId;
        dt = DatabaseCls.FillAdapter(cmd);
        return dt;
    }


    public bool DeleteMsgFileAttachDetail(Int32 MsgId )
    {
        cmd = new SqlCommand();
        cmd.CommandText = "DeleteMsgFileAttachDetail";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@MsgId", SqlDbType.Int));
        cmd.Parameters["@MsgId"].Value = MsgId;         
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls.ExecuteNonQuery(cmd);
        result = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value.ToString());
        return (result != -1);
        //  return (result != -1);
    }
    public bool DeleteMsgDetail(Int32 MsgId)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "DeleteMsgDetail";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@MsgId", SqlDbType.Int));
        cmd.Parameters["@MsgId"].Value = MsgId;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls.ExecuteNonQuery(cmd);
        result = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value.ToString());
        return (result != -1);
        //  return (result != -1);
    }

    public bool DeleteMsgMaster(Int32 MsgId)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "DeleteMsgMaster";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@MsgId", SqlDbType.Int));
        cmd.Parameters["@MsgId"].Value = MsgId;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls.ExecuteNonQuery(cmd);
        result = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value.ToString());
        return (result != -1);
        //  return (result != -1);
    }
    public DataTable SelectMsgforInboxExt(Int32 ToPartyId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectMsgforInboxExt";
        cmd.Parameters.Add(new SqlParameter("@ToPartyId", SqlDbType.Int));
        cmd.Parameters["@ToPartyId"].Value = ToPartyId;
        dt = DatabaseCls.FillAdapter(cmd);
        return dt;
    }

    public Int32 InsertMsgMasterExt(Int32 FromPartyId, String MsgSubject, String MsgDetail,String FromEmail)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "InsertMsgMasterExt";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@FromPartyId", SqlDbType.Int));
        cmd.Parameters["@FromPartyId"].Direction = ParameterDirection.Input;
        cmd.Parameters["@FromPartyId"].Value = FromPartyId;
        cmd.Parameters.Add(new SqlParameter("@MsgSubject", SqlDbType.NVarChar));
        cmd.Parameters["@MsgSubject"].Value = MsgSubject;
        cmd.Parameters.Add(new SqlParameter("@MsgDetail", SqlDbType.NVarChar));
        cmd.Parameters["@MsgDetail"].Value = MsgDetail;
        cmd.Parameters.Add(new SqlParameter("@FromEmail", SqlDbType.NVarChar));
        cmd.Parameters["@FromEmail"].Value = FromEmail;
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString(); // CompanyLoginId;
        cmd.Parameters.Add(new SqlParameter("@MsgId", SqlDbType.Int));
        cmd.Parameters["@MsgId"].Direction = ParameterDirection.Output;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls.ExecuteNonQuery(cmd);
        result = Convert.ToInt32(cmd.Parameters["@MsgId"].Value.ToString());
        return result;
    }
    public bool InsertMsgFileAttachDetailExt(Int32 MsgId, String FileName)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "InsertMsgFileAttachDetailExt";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@MsgId", SqlDbType.Int));
        cmd.Parameters["@MsgId"].Direction = ParameterDirection.Input;
        cmd.Parameters["@MsgId"].Value = MsgId;
        cmd.Parameters.Add(new SqlParameter("@FileName", SqlDbType.NVarChar));
        cmd.Parameters["@FileName"].Value = FileName;

        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls.ExecuteNonQuery(cmd);
        result = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value.ToString());
        return (result != -1);

    }
    public bool DeleteMsgFileAttachDetailExt(Int32 MsgId)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "DeleteMsgFileAttachDetailExt";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@MsgId", SqlDbType.Int));
        cmd.Parameters["@MsgId"].Value = MsgId;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls.ExecuteNonQuery(cmd);
        result = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value.ToString());
        return (result != -1);
        //  return (result != -1);
    }
    public DataTable SelectMsgforFileAttachExt(Int32 MsgId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectMsgforFileAttachExt";
        cmd.Parameters.Add(new SqlParameter("@MsgId", SqlDbType.Int));
        cmd.Parameters["@MsgId"].Value = MsgId;
        dt = DatabaseCls.FillAdapter(cmd);
        return dt;
    }
    public DataTable SelectMsgDetailforDraftPartyListExt(Int32 MsgId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectMsgDetailforDraftPartyListExt";
        cmd.Parameters.Add(new SqlParameter("@MsgId", SqlDbType.Int));
        cmd.Parameters["@MsgId"].Value = MsgId;
        dt = DatabaseCls.FillAdapter(cmd);
        return dt;
    }
    public bool DeleteMsgMasterExt(Int32 MsgId)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "DeleteMsgMasterExt";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@MsgId", SqlDbType.Int));
        cmd.Parameters["@MsgId"].Value = MsgId;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls.ExecuteNonQuery(cmd);
        result = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value.ToString());
        return (result != -1);
        //  return (result != -1);
    }
    public bool UpdateMsgDetailExt(Int32 MsgDetailId, Int32 MsgStatusId)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "UpdateMsgDetailExt";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@MsgDetailId", SqlDbType.Int));
        cmd.Parameters["@MsgDetailId"].Value = @MsgDetailId;
        cmd.Parameters.Add(new SqlParameter("@MsgStatusId", SqlDbType.Int));
        cmd.Parameters["@MsgStatusId"].Value = MsgStatusId;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls.ExecuteNonQuery(cmd);
        result = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value.ToString());
        return (result != -1);
        //  return (result != -1);
    }
    public Int32 
        InsertMsgDetailExt(Int32 MsgId, Int32 ToPartyId, Int32 Msgstatusid)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "InsertMsgDetailExt";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@MsgId", SqlDbType.Int));
        cmd.Parameters["@MsgId"].Direction = ParameterDirection.Input;
        cmd.Parameters["@MsgId"].Value = MsgId;
        cmd.Parameters.Add(new SqlParameter("@ToPartyId", SqlDbType.Int));
        cmd.Parameters["@ToPartyId"].Direction = ParameterDirection.Input;
        cmd.Parameters["@ToPartyId"].Value = ToPartyId;

        cmd.Parameters.Add(new SqlParameter("@Msgstatusid", SqlDbType.Int));
        cmd.Parameters["@Msgstatusid"].Direction = ParameterDirection.Input;
        cmd.Parameters["@Msgstatusid"].Value = Msgstatusid;

        cmd.Parameters.Add(new SqlParameter("@MsgDetailId", SqlDbType.Int));
        cmd.Parameters["@MsgDetailId"].Direction = ParameterDirection.Output;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls.ExecuteNonQuery(cmd);
        result = Convert.ToInt32(cmd.Parameters["@MsgDetailId"].Value.ToString());
        return result;
        //  return (result != -1);
    }
    public bool  UpdateMsgMaster(Int32 MsgId, String MsgSubject, String MsgDetail)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "UpdateMsgMaster";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@MsgId", SqlDbType.Int));
        cmd.Parameters["@MsgId"].Value = MsgId;
        cmd.Parameters.Add(new SqlParameter("@MsgSubject", SqlDbType.NVarChar));
        cmd.Parameters["@MsgSubject"].Value = MsgSubject;
        cmd.Parameters.Add(new SqlParameter("@MsgDetail", SqlDbType.NVarChar));
        cmd.Parameters["@MsgDetail"].Value = MsgDetail;
       
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls.ExecuteNonQuery(cmd);
        result = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value.ToString());
        return (result != -1);
    }
    public bool UpdateMsgMasterExt(Int32 MsgId, String MsgSubject)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "UpdateMsgMasterExt";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@MsgId", SqlDbType.Int));
        cmd.Parameters["@MsgId"].Value = MsgId;

        cmd.Parameters.Add(new SqlParameter("@MsgSubject", SqlDbType.NVarChar));
        cmd.Parameters["@MsgSubject"].Value = MsgSubject;

        //cmd.Parameters.Add(new SqlParameter("@MsgDetail", SqlDbType.NVarChar));
        //cmd.Parameters["@MsgDetail"].Value = MsgDetail;

        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls.ExecuteNonQuery(cmd);
        result = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value.ToString());
        return (result != -1);
    }

    public bool DeleteMsgDetailExt(Int32 MsgId)
    {
        cmd = new SqlCommand();
        cmd.CommandText = "DeleteMsgDetailExt";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@MsgId", SqlDbType.Int));
        cmd.Parameters["@MsgId"].Value = MsgId;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls.ExecuteNonQuery(cmd);
        result = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value.ToString());
        return (result != -1);
        //  return (result != -1);
    }
    public bool UpdateMsgMasterforSendDelete(Int32 MsgId, String MsgStatus )
    {
        cmd = new SqlCommand();
        cmd.CommandText = "UpdateMsgMasterforSendDelete";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add(new SqlParameter("@MsgId", SqlDbType.Int));
        cmd.Parameters["@MsgId"].Value = MsgId;
        cmd.Parameters.Add(new SqlParameter("@MsgStatus", SqlDbType.NVarChar));
        cmd.Parameters["@MsgStatus"].Value = MsgStatus;       
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls.ExecuteNonQuery(cmd);
        result = Convert.ToInt32(cmd.Parameters["@ReturnValue"].Value.ToString());
        return (result != -1);
    }

    public DataTable SelectMsgforSendBox(Int32 FromPartyId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectMsgforSendBox";
        cmd.Parameters.Add(new SqlParameter("@FromPartyId", SqlDbType.Int));
        cmd.Parameters["@FromPartyId"].Value = FromPartyId;
        dt = DatabaseCls.FillAdapter(cmd);
        return dt;
    }


    //public DataTable SelectMsgforReplyForward(Int32 MsgDetailId)
    //{
    //    cmd = new SqlCommand();
    //    dt = new DataTable();
    //    cmd.CommandText = "SelectMsgforReplyForward";
    //    cmd.Parameters.Add(new SqlParameter("@MsgDetailId", SqlDbType.Int));
    //    cmd.Parameters["@MsgDetailId"].Value = MsgDetailId;
    //    dt = DatabaseCls.FillAdapter(cmd);
    //    return dt;
    //}


    public DataTable SelectMsgIdUsingMsgDetailId(Int32 MsgDetailId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectMsgIdUsingMsgDetailId";
        cmd.Parameters.Add(new SqlParameter("@MsgDetailId", SqlDbType.Int));
        cmd.Parameters["@MsgDetailId"].Value = MsgDetailId;
        dt = DatabaseCls.FillAdapter(cmd);
        return dt;
    }

    public DataTable SelectMsgDetailforSentPartyList(Int32 MsgId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectMsgDetailforSentPartyList";
        cmd.Parameters.Add(new SqlParameter("@MsgId", SqlDbType.Int));
        cmd.Parameters["@MsgId"].Value = MsgId;
        dt = DatabaseCls.FillAdapter(cmd);
        return dt;
    }

    public DataTable SelectMsgDetailforDraftPartyList(Int32 MsgId)
    {
        cmd = new SqlCommand();
        dt = new DataTable();
        cmd.CommandText = "SelectMsgDetailforDraftPartyList";
        cmd.Parameters.Add(new SqlParameter("@MsgId", SqlDbType.Int));
        cmd.Parameters["@MsgId"].Value = MsgId;
        dt = DatabaseCls.FillAdapter(cmd);
        return dt;
    }

    //public DataTable SelectMsgDetailforDeletedPartyList(Int32 MsgId)
    //{
    //    cmd = new SqlCommand();
    //    dt = new DataTable();
    //    cmd.CommandText = "SelectMsgDetailforDeletedPartyList";
    //    cmd.Parameters.Add(new SqlParameter("@MsgId", SqlDbType.Int));
    //    cmd.Parameters["@MsgId"].Value = MsgId;
    //    dt = DatabaseCls.FillAdapter(cmd);
    //    return dt;
    //}
}
