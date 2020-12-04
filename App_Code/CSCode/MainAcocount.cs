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
/// Summary description for MainAcocount
/// </summary>
public class MainAcocount
{
    
	public MainAcocount()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public static DataTable SelectDocumentMasterwithId(String DocumentId)
    {
       SqlCommand cmd = new SqlCommand();
       DataTable dt = new DataTable();
       cmd.CommandText = "SelectDocumentMasterwithId";
       cmd.Parameters.Add(new SqlParameter("@DocumentId", SqlDbType.NVarChar));
       cmd.Parameters["@DocumentId"].Value = DocumentId;
      
       dt = DatabaseCls1.FilleppAdapter(cmd); //.FillAdapter(cmd);
        return dt;

    }
    public static DataTable SelectEntrynumber(String EntryTypeId, String Whid)
    {
        SqlCommand cmd = new SqlCommand();
        DataTable dt = new DataTable();
        cmd.CommandText = "SelectEntrynumber";
        cmd.Parameters.Add(new SqlParameter("@EntryTypeId", SqlDbType.NVarChar));
        cmd.Parameters["@EntryTypeId"].Value = EntryTypeId;

        cmd.Parameters.Add(new SqlParameter("@Whid", SqlDbType.NVarChar));
        cmd.Parameters["@Whid"].Value = Whid; // CompanyLoginId;
        dt = DatabaseCls1.FilleppAdapter(cmd); //.FillAdapter(cmd);
        return dt;

    }
    public static DataTable SelectAccountwithwhid( String Whid)
    {
        SqlCommand cmd = new SqlCommand();
        DataTable dt = new DataTable();
        cmd.CommandText = "SelectAccountwithwhid";
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString() ;

        cmd.Parameters.Add(new SqlParameter("@Whid", SqlDbType.NVarChar));
        cmd.Parameters["@Whid"].Value = Whid; // CompanyLoginId;
        dt = DatabaseCls1.FilleppAdapter(cmd); //.FillAdapter(cmd);
        return dt;

    }
    public static DataTable SelectFillTrndata(String Tid)
    {
        SqlCommand cmd = new SqlCommand();
        DataTable dt = new DataTable();
        cmd.CommandText = "SelectFillTrndata";
      
        cmd.Parameters.Add(new SqlParameter("@Tid", SqlDbType.NVarChar));
        cmd.Parameters["@Tid"].Value = Tid; // CompanyLoginId;

        dt = DatabaseCls1.FilleppAdapter(cmd); //.FillAdapter(cmd);
        return dt;
    }

    public static DataTable SelectAccountId(String AccountId,String Whid)
    {
        SqlCommand cmd = new SqlCommand();
        DataTable dt = new DataTable();
        cmd.CommandText = "SelectAccountId";
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString() ;

        cmd.Parameters.Add(new SqlParameter("@Whid", SqlDbType.NVarChar));
        cmd.Parameters["@Whid"].Value = Whid; // CompanyLoginId;
        cmd.Parameters.Add(new SqlParameter("@AccountId", SqlDbType.NVarChar));
        cmd.Parameters["@AccountId"].Value = AccountId; // CompanyLoginId;

        dt = DatabaseCls1.FilleppAdapter(cmd); //.FillAdapter(cmd);
        return dt;

    }
    public static DataTable SelectReportPeriodwithWhid(String Whid)
    {
        SqlCommand cmd = new SqlCommand();
        DataTable dt = new DataTable();
        cmd.CommandText = "SelectReportPeriodwithWhid";
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString();

        cmd.Parameters.Add(new SqlParameter("@Whid", SqlDbType.NVarChar));
        cmd.Parameters["@Whid"].Value = Whid; // CompanyLoginId;
        
        dt = DatabaseCls1.FilleppAdapter(cmd); //.FillAdapter(cmd);
        return dt;

    }
    public static int InsertTransactionMaster( DateTime Date,String EntryNumber,String EntryTypeId,int UserId, Decimal Tranction_Amount,String Whid)
    {
      
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "InsertTransactionMaster";
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@Date", Convert.ToDateTime(Date).ToShortDateString());
        cmd.Parameters.AddWithValue("@EntryNumber", Convert.ToInt64(EntryNumber));
        cmd.Parameters.AddWithValue("@EntryTypeId", EntryTypeId);


        cmd.Parameters.AddWithValue("@UserId", UserId);
        cmd.Parameters.AddWithValue("@comid", HttpContext.Current.Session["Comid"].ToString() );
        cmd.Parameters.AddWithValue("@whid", Whid);

        cmd.Parameters.AddWithValue("@Tranction_Amount", Convert.ToDecimal(Math.Round(Tranction_Amount, 2, MidpointRounding.AwayFromZero).ToString()));
        
        cmd.Parameters.Add(new SqlParameter("@Tranction_Master_Id", SqlDbType.Int));
        cmd.Parameters["@Tranction_Master_Id"].Direction = ParameterDirection.Output;
        cmd.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmd.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;
        Int32 result = DatabaseCls1.ExecuteNonQueryep(cmd);
        if (result > 0)
        {
        result = Convert.ToInt32(cmd.Parameters["@Tranction_Master_Id"].Value);
        }
        return result;
    }
    public static int Sp_Insert_TranctionMasterSuppliment(int TransactionMasterId, String Memo, Decimal AmountDue, int PartyMasterId, int GrnMasterId)
    {

        SqlCommand cmd78 = new SqlCommand();
        cmd78.CommandText = "Sp_Insert_TranctionMasterSuppliment";
        cmd78.CommandType = CommandType.StoredProcedure;
        cmd78.Parameters.AddWithValue("@TransactionMasterId", TransactionMasterId);
        cmd78.Parameters.AddWithValue("@Memo", Memo);
        if (AmountDue >Convert.ToDecimal(0))
        {
            cmd78.Parameters.AddWithValue("@AmountDue", Convert.ToDecimal(Math.Round(AmountDue, 2)));
        }
        else
        {
            cmd78.Parameters.AddWithValue("@AmountDue", DBNull.Value);

        }
        cmd78.Parameters.AddWithValue("@PartyMasterId", PartyMasterId);

        if (GrnMasterId >0)
        {
            cmd78.Parameters.AddWithValue("@GrnMasterId", GrnMasterId);
        }
        else
        {
            cmd78.Parameters.AddWithValue("@GrnMasterId", DBNull.Value);
   
        }
        Int32 result = DatabaseCls1.ExecuteNonQueryep(cmd78);
       
        return result;
    }
    public static int Sp_Insert_Tranction_Details1(int AccountDebit, int AccountCredit, Decimal AmountDebit, Decimal AmountCredit, int Tranction_Master_Id, String Memo,DateTime tddate,String Whid)
    {

        SqlCommand cmd1 = new SqlCommand();
        cmd1.CommandText = "Sp_Insert_Tranction_Details1";
        cmd1.CommandType = CommandType.StoredProcedure;

        if (Convert.ToDouble(Math.Round(Convert.ToDecimal(AmountCredit), 2, MidpointRounding.AwayFromZero).ToString()) > 0)
        {
            cmd1.Parameters.AddWithValue("@AccountDebit", "0");
            cmd1.Parameters.AddWithValue("@AccountCredit", Convert.ToInt32(AccountCredit));
            cmd1.Parameters.AddWithValue("@AmountDebit", "0");
            cmd1.Parameters.AddWithValue("@AmountCredit", Math.Round(Convert.ToDecimal(AmountCredit), 2));
        }
        else
        {
            cmd1.Parameters.AddWithValue("@AccountDebit", Convert.ToInt32(AccountDebit));
            cmd1.Parameters.AddWithValue("@AccountCredit", "0");
            cmd1.Parameters.AddWithValue("@AmountDebit", (Convert.ToDouble(Math.Round(Convert.ToDecimal(AmountDebit), 2))));
            cmd1.Parameters.AddWithValue("@AmountCredit", "0");
        }

        cmd1.Parameters.AddWithValue("@Tranction_Master_Id", Tranction_Master_Id);
        cmd1.Parameters.AddWithValue("@Memo", Memo);
        cmd1.Parameters.AddWithValue("@DateTimeOfTransaction", tddate.ToShortDateString());
        cmd1.Parameters.AddWithValue("@DiscEarn", DBNull.Value);
        cmd1.Parameters.AddWithValue("@DiscPaid", DBNull.Value);
        cmd1.Parameters.AddWithValue("@comid", HttpContext.Current.Session["Comid"].ToString() );
        cmd1.Parameters.AddWithValue("@whid", Whid);

        Int32 result = DatabaseCls1.ExecuteNonQueryep(cmd1);

        return result;
    }

    public static int InsertAttachmentMaster(string Titlename, string Filename, string RelatedtablemasterId, string TID, string DocumentId)
    {

        SqlCommand cmdi = new SqlCommand();
        cmdi.CommandText = "InsertAttachmentMaster";

        cmdi.CommandType = CommandType.StoredProcedure;
        cmdi.Parameters.Add(new SqlParameter("@Titlename", SqlDbType.NVarChar));
        cmdi.Parameters["@Titlename"].Value = Titlename;
        cmdi.Parameters.Add(new SqlParameter("@Filename", SqlDbType.NVarChar));
        cmdi.Parameters["@Filename"].Value = Filename;

        cmdi.Parameters.Add(new SqlParameter("@Datetime", SqlDbType.DateTime));
        cmdi.Parameters["@Datetime"].Value = System.DateTime.Now.ToString() ;
        cmdi.Parameters.Add(new SqlParameter("@RelatedtablemasterId", SqlDbType.NVarChar));
        cmdi.Parameters["@RelatedtablemasterId"].Value = RelatedtablemasterId;
        cmdi.Parameters.Add(new SqlParameter("@RelatedTableId", SqlDbType.NVarChar));
        cmdi.Parameters["@RelatedTableId"].Value = TID;
        cmdi.Parameters.Add(new SqlParameter("@IfilecabinetDocId", SqlDbType.NVarChar));
        cmdi.Parameters["@IfilecabinetDocId"].Value = DocumentId;
        cmdi.Parameters.Add(new SqlParameter("@Attachment", SqlDbType.Int));
        cmdi.Parameters["@Attachment"].Direction = ParameterDirection.Output;

        cmdi.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
        cmdi.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;

        Int32 result = DatabaseCls1.ExecuteNonQueryep(cmdi);
        if (result > 0)
        {
            result = Convert.ToInt32(cmdi.Parameters["@Attachment"].Value);
        }
      

        return result;
    }
    public static DataTable SelectJournalGridfill(String Whid, String accdd, String strperiod)
    {
        SqlCommand cmd = new SqlCommand();
        DataTable dt = new DataTable();
        cmd.CommandText = "SelectJournalGridfill";
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString() ;

        cmd.Parameters.Add(new SqlParameter("@Whid", SqlDbType.NVarChar));
        cmd.Parameters["@Whid"].Value = Whid; // CompanyLoginId;

        cmd.Parameters.Add(new SqlParameter("@accdd", SqlDbType.NVarChar));
        cmd.Parameters["@accdd"].Value = accdd; // CompanyLoginId;

        cmd.Parameters.Add(new SqlParameter("@strperiod", SqlDbType.NVarChar));
        cmd.Parameters["@strperiod"].Value = strperiod; // CompanyLoginId;
        dt = DatabaseCls1.FilleppAdapter(cmd); //.FillAdapter(cmd);
        return dt;

    }
    public static DataTable SelectAccountGroupClasswithWhid(String Whid)
    {
        SqlCommand cmd = new SqlCommand();
        DataTable dt = new DataTable();
        cmd.CommandText = "SelectAccountGroupClasswithWhid";
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString();

        cmd.Parameters.Add(new SqlParameter("@Whid", SqlDbType.NVarChar));
        cmd.Parameters["@Whid"].Value = Whid; // CompanyLoginId;

        dt = DatabaseCls1.FilleppAdapter(cmd); //.FillAdapter(cmd);
        return dt;

    }
    public static DataTable Selectattachdocbytid(String Docid, String tid)
    {
        SqlCommand cmd = new SqlCommand();
        DataTable dt = new DataTable();
        cmd.CommandText = "Selectattachdocbytid";
        cmd.Parameters.Add(new SqlParameter("@Docid", SqlDbType.NVarChar));
        cmd.Parameters["@Docid"].Value = Docid;

        cmd.Parameters.Add(new SqlParameter("@tid", SqlDbType.NVarChar));
        cmd.Parameters["@tid"].Value = tid; // CompanyLoginId;

        dt = DatabaseCls1.FilleppAdapter(cmd); //.FillAdapter(cmd);
        return dt;

    }
    public static DataTable Selectattachdocwithtid(String tid)
    {
        SqlCommand cmd = new SqlCommand();
        DataTable dt = new DataTable();
        cmd.CommandText = "Selectattachdocwithtid";
       
        cmd.Parameters.Add(new SqlParameter("@tid", SqlDbType.NVarChar));
        cmd.Parameters["@tid"].Value = tid; // CompanyLoginId;

        dt = DatabaseCls1.FilleppAdapter(cmd); //.FillAdapter(cmd);
        return dt;

    }
    public static DataTable Selecttrnentryno(String tid)
    {
        SqlCommand cmd = new SqlCommand();
        DataTable dt = new DataTable();
        cmd.CommandText = "Selecttrnentryno";

        cmd.Parameters.Add(new SqlParameter("@tid", SqlDbType.NVarChar));
        cmd.Parameters["@tid"].Value = tid; // CompanyLoginId;

        dt = DatabaseCls1.FilleppAdapter(cmd); //.FillAdapter(cmd);
        return dt;

    }
    public static DataTable SelectpartyGridfill(String para, String para1)
    {
        SqlCommand cmd = new SqlCommand();
        DataTable dt = new DataTable();
        cmd.CommandText = "SelectpartyGridfill";
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString() ;

        cmd.Parameters.Add(new SqlParameter("@para", SqlDbType.NVarChar));
        cmd.Parameters["@para"].Value = para; // CompanyLoginId;
        cmd.Parameters.Add(new SqlParameter("@para1", SqlDbType.NVarChar));
        cmd.Parameters["@para1"].Value = para1; // CompanyLoginId;
        dt = DatabaseCls1.FilleppAdapter(cmd); //.FillAdapter(cmd);
        return dt;

    }
    public static DataTable SelectDepartmentmasterMNCwithCID()
    {
        SqlCommand cmd = new SqlCommand();
        DataTable dt = new DataTable();
        cmd.CommandText = "SelectDepartmentmasterMNCwithCID";
        cmd.Parameters.Add(new SqlParameter("@CID", SqlDbType.NVarChar));
        cmd.Parameters["@CID"].Value = HttpContext.Current.Session["Comid"].ToString() ;
        dt = DatabaseCls1.FilleppAdapter(cmd); //.FillAdapter(cmd);
        return dt;

    }
    public static DataTable SelectWhidwithdeptid(String deptid)
    {
        SqlCommand cmd = new SqlCommand();
        DataTable dt = new DataTable();
        cmd.CommandText = "SelectWhidwithdeptid";
        cmd.Parameters.Add(new SqlParameter("@deptid", SqlDbType.NVarChar));
        cmd.Parameters["@deptid"].Value = deptid;
        dt = DatabaseCls1.FilleppAdapter(cmd); //.FillAdapter(cmd);
        return dt;

    }

    public static DataTable SelctDocHeadType(String mid,int flag)
    {
        SqlCommand cmd = new SqlCommand();
        DataTable dt = new DataTable();
        cmd.CommandText = "SelctDocHeadType";
        cmd.Parameters.Add(new SqlParameter("@mid", SqlDbType.NVarChar));
        cmd.Parameters["@mid"].Value = mid;
        cmd.Parameters.Add(new SqlParameter("@flag", SqlDbType.NVarChar));
        cmd.Parameters["@flag"].Value = flag;
        dt = DatabaseCls1.FilleppAdapter(cmd); //.FillAdapter(cmd);
        return dt;

    }

    public static DataTable SelectOfficedocforGeneral(String Mid, int flag)
    {
        SqlCommand cmd = new SqlCommand();
        DataTable dt = new DataTable();
        cmd.CommandText = "SelectOfficedocforGeneral";
        cmd.Parameters.Add(new SqlParameter("@Mid", SqlDbType.NVarChar));
        cmd.Parameters["@Mid"].Value = Mid;
        cmd.Parameters.Add(new SqlParameter("@flag", SqlDbType.NVarChar));
        cmd.Parameters["@flag"].Value = flag;
        dt = DatabaseCls1.FilleppAdapter(cmd); //.FillAdapter(cmd);
        return dt;

    }
    public static DataTable SelectOfficedocidwithdocid(String Mid, int flag, int docid)
    {
        SqlCommand cmd = new SqlCommand();
        DataTable dt = new DataTable();
        cmd.CommandText = "SelectOfficedocidwithdocid";
        cmd.Parameters.Add(new SqlParameter("@Mid", SqlDbType.NVarChar));
        cmd.Parameters["@Mid"].Value = Mid;
        cmd.Parameters.Add(new SqlParameter("@flag", SqlDbType.NVarChar));
        cmd.Parameters["@flag"].Value = flag;
        cmd.Parameters.Add(new SqlParameter("@docid", SqlDbType.NVarChar));
        cmd.Parameters["@docid"].Value = docid;
        dt = DatabaseCls1.FilleppAdapter(cmd); //.FillAdapter(cmd);
        return dt;

    }
}
