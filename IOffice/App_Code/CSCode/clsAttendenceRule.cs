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

public class clsAttendenceRule
{
    SqlConnection con;
    SqlCommand cmd;
    DataSet ds;
    SqlDataAdapter da;
    DataTable dt;


    public string StoreId
    {
        get;
        set;
    }
    public string AcceptableInTimeDeviationMinutes
    {
        get;
        set;
    }
    public string AcceptableOutTimeDeviationMinutes
    {
        get;
        set;
    }
    public int Considerwithinrangedeviationasstandardtime
    {
        get;
        set;
    }
    public int ShowtheFieldtorecordthereasonfordeviation
    {
        get;
        set;
    }
    public string Showreasonafterinstance
    {
        get;
        set;
    }
    public int TakeapprovaloftheseniorEmployee
    {
        get;
        set;
    }
    public int SeniorEmployeeID
    {
        get;
        set;
    }
    public string Takeapprovalafterinstance
    {
        get;
        set;
    }
    public int Donotallowemployeetomakeentryinregister
    {
        get;
        set;
    }
    public string Donotallowemployeeinstance
    {
        get;
        set;
    }
    public string CompId
    {
        get;
        set;
    }
    public string Whid
    {
        get;
        set;
    }
    public int overtimeruleno
    {
        get;
        set;
    }
    public string OvertimepaymentRate
    {
        get;
        set;
    }
    public string Overtimehours
    {
        get;
        set;
    }
    public string Overtimeremunaration
    {
        get;
        set;
    }
 
    public int Generalapprovalrule
    {
        get;
        set;
    }
    public int Considerinoutrangeintance
    {
        get;
        set;
    }
    public int rulegreatertime
    {
        get;
        set;
    }
    public int rulegreatertimeinstance
    {
        get;
        set;
    }
    public int overtimerulerange
    {
        get;
        set;
    }
    public int overtomeapproval
    {
        get;
        set;
    }
    public int EmployeeMasterId
    {
        get;
        set;
    }
    public int User_id
    {
        get;
        set;
    }
    public int User_id1
    {
        get;
        set;
    }
    public int Role_id
    {
        get;
        set;
    }
    public int ActiveDeactive
    {
        get;
        set;
    }
    public int RoleId
    {
        get;
        set;
    }
    public int PageId
    {
        get;
        set;
    }
    public int Attendence_Rule_Id
    {
        get;
        set;
    }

	public clsAttendenceRule()
	{
        PageConn pgcon = new PageConn();
        con = pgcon.dynconn;
	}

    public DataTable SelectAttandanceRuleWaremaster()
    {
        cmd = new SqlCommand("SelectAttandanceRuleWarehouseMaster", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@StoreId", SqlDbType.NVarChar).Value = StoreId;
        da = new SqlDataAdapter(cmd);
        dt = new DataTable();
        da.Fill(dt);
        return dt;
    }
    public DataTable SelectAttandanceRuleStoreId()
    {
        cmd = new SqlCommand("SelectAttandanceRuleByStoreId", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@StoreId", SqlDbType.NVarChar).Value = StoreId;
        da = new SqlDataAdapter(cmd);
        dt = new DataTable();
        da.Fill(dt);
        return dt;
    }
    public int InsertAttandanceRule11(char s)
    {
        cmd = new SqlCommand("InsertAttandanceRule1", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@StoreId", SqlDbType.NVarChar).Value = StoreId;
        cmd.Parameters.Add("@AcceptableInTimeDeviationMinutes", SqlDbType.NVarChar).Value = AcceptableInTimeDeviationMinutes;
        cmd.Parameters.Add("@AcceptableOutTimeDeviationMinutes", SqlDbType.NVarChar).Value = AcceptableOutTimeDeviationMinutes;
        cmd.Parameters.Add("@Considerwithinrangedeviationasstandardtime", SqlDbType.Bit).Value = Considerwithinrangedeviationasstandardtime;
        cmd.Parameters.Add("@ShowtheFieldtorecordthereasonfordeviation", SqlDbType.Bit).Value = ShowtheFieldtorecordthereasonfordeviation;
        cmd.Parameters.Add("@Showreasonafterinstance", SqlDbType.NVarChar).Value = Showreasonafterinstance;
        cmd.Parameters.Add("@TakeapprovaloftheseniorEmployee", SqlDbType.Bit).Value = TakeapprovaloftheseniorEmployee;
        cmd.Parameters.Add("@SeniorEmployeeID", SqlDbType.Int).Value = SeniorEmployeeID;
        cmd.Parameters.Add("@Takeapprovalafterinstance", SqlDbType.NVarChar).Value = Takeapprovalafterinstance;
        cmd.Parameters.Add("@Donotallowemployeetomakeentryinregister", SqlDbType.Bit).Value = Donotallowemployeetomakeentryinregister;
        cmd.Parameters.Add("@Donotallowemployeeinstance", SqlDbType.NVarChar).Value = Donotallowemployeeinstance;
        cmd.Parameters.Add("@CompId", SqlDbType.NVarChar).Value = CompId;
        cmd.Parameters.Add("@overtimeruleno", SqlDbType.Int).Value = overtimeruleno;
        cmd.Parameters.Add("@OvertimepaymentRate", SqlDbType.NVarChar).Value = OvertimepaymentRate;
        cmd.Parameters.Add("@Overtimehours", SqlDbType.NVarChar).Value = Overtimehours;
        cmd.Parameters.Add("@Overtimeremunaration", SqlDbType.NVarChar).Value = Overtimeremunaration;
        //cmd.Parameters.Add("@Overtimepara", SqlDbType.NVarChar).Value = Overtimepara;
        cmd.Parameters.Add("@Generalapprovalrule", SqlDbType.Bit).Value = Generalapprovalrule;
        cmd.Parameters.Add("@Considerinoutrangeintance", SqlDbType.Int).Value = Considerinoutrangeintance;
        cmd.Parameters.Add("@rulegreatertime", SqlDbType.Bit).Value = rulegreatertime;
        cmd.Parameters.Add("@rulegreatertimeinstance", SqlDbType.Int).Value = rulegreatertimeinstance;
        cmd.Parameters.Add("@overtimerulerange", SqlDbType.Bit).Value = overtimerulerange;
        cmd.Parameters.Add("@overtomeapproval", SqlDbType.Bit).Value = overtomeapproval;
        cmd.Parameters.Add("@flag", SqlDbType.Char).Value = s;

        if (cmd.Connection.State.ToString() != "Open")
        {
            con.Open();
        }
        return cmd.ExecuteNonQuery();
        con.Close();
    }

    public DataTable SelectRoleMasterByRole_name11()
    {
        cmd = new SqlCommand("SelectRoleMasterByRole_name", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@compid", SqlDbType.NVarChar).Value = CompId;
        da = new SqlDataAdapter(cmd);
        dt = new DataTable();
        da.Fill(dt);
        return dt;
    }

    public DataTable SelectEmployeeMasterParty()
    {
        cmd = new SqlCommand("SelectEmployeeMasterParty_Master", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@EmployeeMasterId", SqlDbType.Int).Value = EmployeeMasterId;
        da = new SqlDataAdapter(cmd);
        dt = new DataTable();
        da.Fill(dt);
        return dt;
    }

    public int InsertUser_Role11()
    {
        cmd = new SqlCommand("InsertUser_Role", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@User_id", SqlDbType.Int).Value = User_id;
        cmd.Parameters.Add("@Role_id", SqlDbType.Int).Value = Role_id;

        if (cmd.Connection.State.ToString() != "Open")
        {
            con.Open();
        }
        return cmd.ExecuteNonQuery();
        con.Close();
    }

    public DataTable SelectPageMaster11as()
    {
        cmd = new SqlCommand("SelectPageMaster11", con);
        cmd.CommandType = CommandType.StoredProcedure;
 
        da = new SqlDataAdapter(cmd);
        dt = new DataTable();
        da.Fill(dt);
        return dt;
    }

    public int InsertRolePageAccessRightTable()
    {
        cmd = new SqlCommand("InsertRolePageAccessRightTbl", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@PageId", SqlDbType.Int).Value = PageId;
        cmd.Parameters.Add("@RoleId", SqlDbType.Int).Value = RoleId;

        if (cmd.Connection.State.ToString() != "Open")
        {
            con.Open();
        }
        return cmd.ExecuteNonQuery();
        con.Close();
    }
    public DataTable SelectAttandanceRuleWarehouse()
    {
        cmd = new SqlCommand("SelectAttandanceRuleWarehouseMaster21", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@Attendence_Rule_Id", SqlDbType.Int).Value = Attendence_Rule_Id;
        da = new SqlDataAdapter(cmd);
        dt = new DataTable();
        da.Fill(dt);
        return dt;
    }

    public DataTable SelectAttandanceRuleByAtte()
    {
        cmd = new SqlCommand("SelectAttandanceRuleByAttendence_Rule_Id", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@StoreId", SqlDbType.NVarChar).Value = StoreId;
        cmd.Parameters.Add("@Attendence_Rule_Id", SqlDbType.Int).Value = Attendence_Rule_Id;
        da = new SqlDataAdapter(cmd);
        dt = new DataTable();
        da.Fill(dt);
        return dt;
    }

    public DataTable SelectRoleMasterRole()
    {
        cmd = new SqlCommand("SelectRoleMasterRole_name", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@compid", SqlDbType.NVarChar).Value = CompId;

        da = new SqlDataAdapter(cmd);
        dt = new DataTable();
        da.Fill(dt);
        return dt;
    }

    public DataTable SelectEmployeeMasterinnerjoin()
    {
        cmd = new SqlCommand("SelectEmployeeMasterinnerjoinParty_Master", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@EmployeeMasterId", SqlDbType.Int).Value = EmployeeMasterId;
        da = new SqlDataAdapter(cmd);
        dt = new DataTable();
        da.Fill(dt);
        return dt;
    }

    public int UpdateUser_Role112()
    {
        cmd = new SqlCommand("UpdateUser_Role", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@User_id", SqlDbType.Int).Value = User_id;
        cmd.Parameters.Add("@Role_id", SqlDbType.Int).Value = Role_id;
        cmd.Parameters.Add("@User_id1", SqlDbType.Int).Value = User_id1;
        if (cmd.Connection.State.ToString() != "Open")
        {
            con.Open();
        }
        return cmd.ExecuteNonQuery();
        con.Close();
    }

    public DataSet SelectEmployeeMasterWhid12()
    {
        cmd = new SqlCommand("SelectEmployeeMasterByWhid14", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@Whid", SqlDbType.NVarChar).Value = Whid;
        da = new SqlDataAdapter(cmd);
        ds = new DataSet();
        da.Fill(ds);
        return ds;
    }

    public DataTable SelectRemunerationMasterWhid2()
    {
        cmd = new SqlCommand("SelectRemunerationMasterByWhid23", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@Whid", SqlDbType.Int).Value = Whid;
        da = new SqlDataAdapter(cmd);
        dt = new DataTable();
        da.Fill(dt);
        return dt;
    }

    public int UpdateAttendancerule(char s)
    {
        cmd = new SqlCommand("UpdateAttendancerule11", con);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.Add("@Attendence_Rule_Id", SqlDbType.Int).Value = Attendence_Rule_Id;
        cmd.Parameters.Add("@StoreId", SqlDbType.NVarChar).Value = StoreId;
        cmd.Parameters.Add("@AcceptableInTimeDeviationMinutes", SqlDbType.NVarChar).Value = AcceptableInTimeDeviationMinutes;
        cmd.Parameters.Add("@AcceptableOutTimeDeviationMinutes", SqlDbType.NVarChar).Value = AcceptableOutTimeDeviationMinutes;
        cmd.Parameters.Add("@Considerwithinrangedeviationasstandardtime", SqlDbType.Bit).Value = Considerwithinrangedeviationasstandardtime;
        cmd.Parameters.Add("@ShowtheFieldtorecordthereasonfordeviation", SqlDbType.Bit).Value = ShowtheFieldtorecordthereasonfordeviation;
        cmd.Parameters.Add("@Showreasonafterinstance", SqlDbType.NVarChar).Value = Showreasonafterinstance;
        cmd.Parameters.Add("@TakeapprovaloftheseniorEmployee", SqlDbType.Bit).Value = TakeapprovaloftheseniorEmployee;
        cmd.Parameters.Add("@SeniorEmployeeID", SqlDbType.Int).Value = SeniorEmployeeID;
        cmd.Parameters.Add("@Takeapprovalafterinstance", SqlDbType.NVarChar).Value = Takeapprovalafterinstance;
        cmd.Parameters.Add("@Donotallowemployeetomakeentryinregister", SqlDbType.Bit).Value = Donotallowemployeetomakeentryinregister;
        cmd.Parameters.Add("@Donotallowemployeeinstance", SqlDbType.NVarChar).Value = Donotallowemployeeinstance;
        cmd.Parameters.Add("@CompId", SqlDbType.NVarChar).Value = CompId;
        cmd.Parameters.Add("@overtimeruleno", SqlDbType.Int).Value = overtimeruleno;
        cmd.Parameters.Add("@OvertimepaymentRate", SqlDbType.NVarChar).Value = OvertimepaymentRate;
        cmd.Parameters.Add("@Overtimehours", SqlDbType.NVarChar).Value = Overtimehours;
        cmd.Parameters.Add("@Overtimeremunaration", SqlDbType.NVarChar).Value = Overtimeremunaration;
        //cmd.Parameters.Add("@Overtimepara", SqlDbType.NVarChar).Value = Overtimepara;
        cmd.Parameters.Add("@Generalapprovalrule", SqlDbType.Bit).Value = Generalapprovalrule;
        cmd.Parameters.Add("@Considerinoutrangeintance", SqlDbType.Int).Value = Considerinoutrangeintance;
        cmd.Parameters.Add("@rulegreatertime", SqlDbType.Bit).Value = rulegreatertime;
        cmd.Parameters.Add("@rulegreatertimeinstance", SqlDbType.Int).Value = rulegreatertimeinstance;
        cmd.Parameters.Add("@overtimerulerange", SqlDbType.Bit).Value = overtimerulerange;
        cmd.Parameters.Add("@overtomeapproval", SqlDbType.Bit).Value = overtomeapproval;
        cmd.Parameters.Add("@flag", SqlDbType.Char).Value = s;
        if (cmd.Connection.State.ToString() != "Open")
        {
            con.Open();
        }
        return cmd.ExecuteNonQuery();
        con.Close();
    }
}
