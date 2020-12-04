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
using System.Text;

/// <summary>
/// Summary description for afterLogin
/// </summary>
public class afterLogin
{
    //SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["OnlineAccountConnectionString"].ToString());
     SqlCommand  cmd;
     SqlConnection conn;
    DataTable dt;
	public afterLogin()
	{
       
	}
    public DataTable selectSalesOrder(string compid)
    {
        PageConn pgcon = new PageConn();
        conn = pgcon.dynconn;
        string str =" SELECT  top(4)    SalesOrderMaster.PartyId, SalesOrderMaster.SalesOrderDate, SalesOrderMaster.ShippingCharges, SalesOrderMaster.HandlingCharges,  "+
                      " SalesOrderMasterDetail.PartyDiscountAmount, SalesOrderMasterDetail.OrderValueDiscountAmount, SalesOrderMaster.GrossAmount,  "+
                      " SalesOrderMaster.SalesOrderId,case when (Party_master.Compname is null) then '--' else   Party_master.Compname end as Compname, left(Compname,12)+ '..' as comp" + 
                    " FROM SalesOrderMaster INNER JOIN "+
                      " SalesOrderMasterDetail ON SalesOrderMaster.SalesOrderId = SalesOrderMasterDetail.SalesOrderId LEFT OUTER JOIN "+
                      " Party_master ON SalesOrderMaster.PartyId = Party_master.PartyID "+
            " where  Party_master.id='" + compid + "' and SalesOrderMaster.SalesOrderId not in (SELECT   RefSalesOrderId " +
                            " FROM    SalesChallanMaster)  order by SalesOrderMaster.SalesOrderDate desc";
        cmd = new SqlCommand(str,conn);
        dt = new DataTable();
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        adp.Fill(dt);
        return dt;
    }
    public DataTable selectSalesOrder1()
    {
        PageConn pgcon = new PageConn();
        conn = pgcon.dynconn;
        string str = " SELECT  top(4)    SalesOrderMaster.PartyId, SalesOrderMaster.SalesOrderDate, SalesOrderMaster.ShippingCharges, SalesOrderMaster.HandlingCharges,  " +
                      " SalesOrderMasterDetail.PartyDiscountAmount, SalesOrderMasterDetail.OrderValueDiscountAmount, SalesOrderMaster.GrossAmount,  " +
                      " SalesOrderMaster.SalesOrderId,case when (Party_master.Compname is null) then '--' else   Party_master.Compname end as Compname, left(Compname,12)+ '..' as comp" +
                    " FROM SalesOrderMaster INNER JOIN " +
                      " SalesOrderMasterDetail ON SalesOrderMaster.SalesOrderId = SalesOrderMasterDetail.SalesOrderId LEFT OUTER JOIN " +
                      " Party_master ON SalesOrderMaster.PartyId = Party_master.PartyID " +
            " where  SalesOrderMaster.SalesOrderId not in (SELECT   RefSalesOrderId " +
                            " FROM    SalesChallanMaster)  order by SalesOrderMaster.SalesOrderDate desc";
        cmd = new SqlCommand(str, conn);
        dt = new DataTable();
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        adp.Fill(dt);
        return dt;
    }
    public DataTable selectservicecall()
    {
        PageConn pgcon = new PageConn();
        conn = pgcon.dynconn;
        string s = " SELECT     TOP (5) ServiceStatusMaster.StatusName AS Servicestatus, CustomerServiceCallMaster.CustomerServiceCallMasterId,  "+
                      " CustomerServiceCallMaster.Entrydate, CustomerServiceCallMaster.ProblemTitle, CustomerServiceCallMaster.ProblemDescription,  "+
                      " CustomerServiceCallMaster.ProblemType, CustomerServiceCallMaster.CustomerId, CustomerServiceCallMaster.ServiceStatusId,  "+
                      " Party_master.Compname, Party_master.Contactperson, left(Party_master.Compname,12)+ '..' as comp "+
                    " FROM         CustomerServiceCallMaster INNER JOIN "+
                     " ServiceStatusMaster ON CustomerServiceCallMaster.ServiceStatusId = ServiceStatusMaster.StatusId INNER JOIN " +
                     " User_master ON CustomerServiceCallMaster.CustomerId = User_master.UserID INNER JOIN "+
                     " Party_master ON User_master.PartyID = Party_master.PartyID "+
                    " WHERE     (ServiceStatusMaster.StatusId = 1) "+
                        "  ORDER BY CustomerServiceCallMaster.Entrydate DESC";

        SqlCommand cmd = new SqlCommand(s, conn);
        cmd.CommandType = CommandType.Text;
      SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        return dt;
    }

    public DataTable SelectPayment()
    {
        PageConn pgcon = new PageConn();
        conn = pgcon.dynconn;
        string s = " SELECT TOP (4) User_master.PartyID, TranctionMaster.Date, TranctionMaster.EntryNumber, TranctionMaster.EntryTypeId, EntryTypeMaster.Entry_Type_Name,  "+
                      " User_master.UserID, User_master.Name, Tranction_Details.AmountDebit, Tranction_Details.AmountCredit, Tranction_Details.Tranction_Details_Id,  "+
                      " TranctionMaster.Tranction_Master_Id, Tranction_Details.AccountCredit,  left(Party_master.Compname,12) + '..' as comp " +
                    " FROM         Tranction_Details INNER JOIN "+
                     " TranctionMaster ON Tranction_Details.Tranction_Master_Id = TranctionMaster.Tranction_Master_Id INNER JOIN "+
                     " User_master ON TranctionMaster.UserId = User_master.UserID INNER JOIN "+
                     " EntryTypeMaster ON TranctionMaster.EntryTypeId = EntryTypeMaster.Entry_Type_Id INNER JOIN "+
                     " Party_master ON User_master.PartyID = Party_master.PartyID "+
                    " ORDER BY TranctionMaster.Date DESC ";


        SqlCommand cmd = new SqlCommand(s, conn);
        cmd.CommandType = CommandType.Text;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        return dt;
    }

    public DataTable SelectCrDrNotesByCompany(string compid)
    {
        PageConn pgcon = new PageConn();
        conn = pgcon.dynconn;
        string s = "SELECT     TOP (4) TranctionMaster.Date AS EntryDate, TranctionMaster.EntryNumber, EntryTypeMaster.Entry_Type_Name AS EntryTypeName,  "+
                      " CASE WHEN TranctionMaster.EntryTypeId = 6 THEN TranctionMaster.Tranction_Amount ELSE - TranctionMaster.Tranction_Amount END AS Amount, "+
                      " TranctionMaster.Tranction_Master_Id, TranctionMaster.UserId,  left(Party_master.Compname,12) + '..' as comp " +
                        " FROM         TranctionMaster INNER JOIN "+
                      " EntryTypeMaster ON TranctionMaster.EntryTypeId = EntryTypeMaster.Entry_Type_Id INNER JOIN "+
                      " User_master ON TranctionMaster.UserId = User_master.UserID INNER JOIN "+
                      " Party_master ON User_master.PartyID = Party_master.PartyID "+
                " WHERE   TranctionMaster.compid='" + compid + "' and   TranctionMaster.compid>'0' and   (TranctionMaster.EntryTypeId = 6) OR " +
                    "  (TranctionMaster.EntryTypeId = 7) "+
                    " ORDER BY EntryDate DESC ";


        SqlCommand cmd = new SqlCommand(s, conn);
        cmd.CommandType = CommandType.Text;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        return dt;
    }
    public DataTable SelectCrDrNotesByCompany1()
    {
        PageConn pgcon = new PageConn();
        conn = pgcon.dynconn;
        string s = "SELECT     TOP (4) TranctionMaster.Date AS EntryDate, TranctionMaster.EntryNumber, EntryTypeMaster.Entry_Type_Name AS EntryTypeName,  " +
                      " CASE WHEN TranctionMaster.EntryTypeId = 6 THEN TranctionMaster.Tranction_Amount ELSE - TranctionMaster.Tranction_Amount END AS Amount, " +
                      " TranctionMaster.Tranction_Master_Id, TranctionMaster.UserId,  left(Party_master.Compname,12) + '..' as comp " +
                        " FROM         TranctionMaster INNER JOIN " +
                      " EntryTypeMaster ON TranctionMaster.EntryTypeId = EntryTypeMaster.Entry_Type_Id INNER JOIN " +
                      " User_master ON TranctionMaster.UserId = User_master.UserID INNER JOIN " +
                      " Party_master ON User_master.PartyID = Party_master.PartyID " +
                " WHERE     (TranctionMaster.EntryTypeId = 6) OR " +
                    "  (TranctionMaster.EntryTypeId = 7) " +
                    " ORDER BY EntryDate DESC ";


        SqlCommand cmd = new SqlCommand(s, conn);
        cmd.CommandType = CommandType.Text;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        return dt;
    }

 

    public DataTable SelectMessageInbox(int partyid)
    {
        PageConn pgcon = new PageConn();
        conn = pgcon.dynconn;
        //string partyid12 = "select PartyID from User_master where UserID="+partyid+"";
        //SqlDataAdapter adp123 = new SqlDataAdapter(partyid12, conn);
        //DataSet ser = new DataSet();
        //adp123.Fill(ser);
        //string par = ser.Tables[0].Rows[0][0].ToString();


        //string s = " SELECT top(5)     MsgMaster.MsgDate,left(MsgMaster.MsgSubject,25) + '...' as MsgSubject ,left(Party_master.Compname,20) + '..' as Compname , MsgStatusMaster.MsgStatusName, MsgDetail.MsgDetailId, MsgDetail.ToPartyId, " +
        //                  " MsgDetail.MsgStatusId " +
        //                " FROM         MsgDetail INNER JOIN " +
        //                 " MsgMaster ON MsgDetail.MsgId = MsgMaster.MsgId INNER JOIN " +
        //                 " MsgStatusMaster ON MsgDetail.MsgStatusId = MsgStatusMaster.MsgStatusId INNER JOIN " +
        //                 " Party_master ON MsgMaster.FromPartyId = Party_master.PartyID " +
        //                " WHERE     (MsgDetail.ToPartyId ='" + par + "') AND (MsgDetail.MsgStatusId IN (1, 2))  " +
        //                " order by MsgMaster.MsgDate desc ";


        string s = " SELECT top(5)     MsgMaster.MsgDate,left(MsgMaster.MsgSubject,25) + '...' as MsgSubject ,left(Party_master.Compname,20) + '..' as Compname ,  MsgDetail.MsgDetailId, MsgDetail.ToPartyId, " +
                          " MsgDetail.MsgStatusId " +
                        " FROM         MsgDetail INNER JOIN " +
                         " MsgMaster ON MsgDetail.MsgId = MsgMaster.MsgId INNER JOIN " +
                        " Party_master ON MsgMaster.FromPartyId = Party_master.PartyID " +
                        " WHERE     (MsgDetail.ToPartyId ='" + partyid + "') AND (MsgDetail.MsgStatusId IN (1, 2))  " +
                        " order by MsgMaster.MsgDate desc ";

        SqlCommand cmd = new SqlCommand(s, conn);
        cmd.CommandType = CommandType.Text;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        return dt;
    }

    public DataSet GetMasterDetail(int OrderNo)
    {
        PageConn pgcon = new PageConn();
        conn = pgcon.dynconn;
        string s1 = " SELECT     SalesOrderId, SalesOrderNo, SalesManId, PartyId, SalesOrderDate, BuyersPOno, ShippersId, ExpextedDeliveryDate, PaymentsTerms, OtherTerms, " +
                           " ShippingCharges, HandlingCharges, OtherCharges, Discounts, GrossAmount  " +
                           " FROM         SalesOrderMaster " +
                           " Where SalesOrderId = '" + OrderNo + "'";

        SqlCommand cmd = new SqlCommand(s1, conn);
        cmd.CommandType = CommandType.Text;
        SqlDataAdapter sda = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        sda.Fill(ds);
        return ds;
    }
    public DataSet getBillingAddress(int OrderNo)
    {
        PageConn pgcon = new PageConn();
        conn = pgcon.dynconn;
        SqlCommand cmd = new SqlCommand("SELECT  Name, Address, City, State, Country, Phone, Zipcode FROM BillingAddress WHERE (SalesOrderId = '" + OrderNo + "')", conn);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);
        return ds;
    }

    public DataSet getShippingAddress(int OrderNo)
    {
        PageConn pgcon = new PageConn();
        conn = pgcon.dynconn;
        SqlCommand cmd = new SqlCommand("SELECT  Name, Address, City, State, Country, Phone, Zipcode FROM ShippingAddress WHERE (SalesOrderId = '" + OrderNo + "')", conn);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);
        return ds;
    }

    public DataSet getPayment(int OrderNo)
    {
        PageConn pgcon = new PageConn();
        conn = pgcon.dynconn;
        string s = " SELECT     SalesOrderPaymentOption.Id, SalesOrderPaymentOption.SalesOrderId, PaymentMethodMaster.PaymentMethodName, " +
                    "    SalesOrderPaymentStatus.OrderPaymentStatus " +
                    " FROM         SalesOrderPaymentOption INNER JOIN " +
                    "  PaymentMethodMaster ON SalesOrderPaymentOption.PaymentMethodID = PaymentMethodMaster.PaymentMethodID INNER JOIN " +
                    " SalesOrderPaymentStatus ON SalesOrderPaymentOption.SalesOrderId = SalesOrderPaymentStatus.SalesOrderId " +
                    " WHERE     (SalesOrderPaymentOption.SalesOrderId = '" + OrderNo + "')  ";
        SqlCommand cmd = new SqlCommand(s, conn);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);
        return ds;
    }

    public DataSet getProdcutDetail(int OrderNo)
    {
        PageConn pgcon = new PageConn();
        conn = pgcon.dynconn;
        string s = " SELECT     SalesOrderDetail.SalesOrderId, SalesOrderDetail.SalesOrderMasterId, SalesOrderDetail.categorySubSubId, SalesOrderDetail.InventoryWHM_Id, " +
                   "    SalesOrderDetail.Qty, SalesOrderDetail.Rate, SalesOrderDetail.Amount, SalesOrderDetail.Quality, SalesOrderDetail.Notes, InventoryMaster.Name, " +
                   "   SalesOrderDetailDetail.InventoryVolumeDiscountAmount, SalesOrderDetailDetail.PromotionDiscountAmount,  " +
                   "   SalesOrderDetailDetail.PackHandlingAmount " +
                   " FROM party_master  inner join SalesOrderMaster on SalesOrderMaster.PartyId=party_master.PartyId inner join SalesOrderDetail on SalesOrderDetail.SalesOrderMasterId=SalesOrderMaster.Salesordeid  INNER JOIN " +
                   "   InventoryMaster ON SalesOrderDetail.InventoryWHM_Id = InventoryMaster.InventoryMasterId INNER JOIN " +
                   "   SalesOrderDetailDetail ON SalesOrderDetail.SalesOrderId = SalesOrderDetailDetail.SalesOrderDetailId" +
                   " Where SalesOrderDetail.SalesOrderMasterId = '" + OrderNo + "'";

        SqlCommand cmd = new SqlCommand(s, conn);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);
        return ds;
    }
    public StringBuilder getSiteAddress()
    {
        PageConn pgcon = new PageConn();
        conn = pgcon.dynconn;
        SqlCommand cmd = new SqlCommand("Sp_select_Siteaddress", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable ds = new DataTable();
        adp.Fill(ds);
        //return ds;
        // string path = Server.MapPath(@"../ShoppingCart/images/logo.gif"); 
        StringBuilder strAddress = new StringBuilder();
        strAddress.Append("<table width=\"100%\"> ");

        strAddress.Append("<tr><td> <img src=\"http://shopusa.indiaauthentic.com/ShoppingCart/images/logo.gif\" \"border=\"0\"  /> </td><td align=\"center\"><b><span style=\"color: #996600\">" + ds.Rows[0]["Sitename"].ToString() + "</span></b><Br><b>" + ds.Rows[0]["CompanyName"].ToString() + "</b><Br>" + ds.Rows[0]["Address1"].ToString() + "<Br><b>TollFree:</b>" + ds.Rows[0]["TollFree1"].ToString() + "," + ds.Rows[0]["TollFree2"].ToString() + "<Br><b>Phone:</b>" + ds.Rows[0]["Phone1"].ToString() + "," + ds.Rows[0]["Phone2"].ToString() + "<Br><b>Fax:</b>" + ds.Rows[0]["Fax"].ToString() + "<Br><b>Email:</b>" + ds.Rows[0]["Email"].ToString() + "<Br><b>Website:</b>" + ds.Rows[0]["SiteUrl"].ToString() + " </td></tr>  ");


        strAddress.Append("</table> ");
      //  ViewState["sitename"] = ds.Rows[0]["Sitename"].ToString();
        return strAddress;

    }

    public DataSet getCustInfo(int OrderNo)
    {
        PageConn pgcon = new PageConn();
        conn = pgcon.dynconn;
        string str = "SELECT     SalesOrderMaster.PartyId, SalesOrderMaster.SalesOrderId, User_master.UserID, User_master.Name, User_master.EmailID " +
                    " FROM         SalesOrderMaster INNER JOIN " +
        " Party_master ON SalesOrderMaster.PartyId = Party_master.PartyID INNER JOIN " +
         " User_master ON Party_master.PartyID = User_master.PartyID " +
                        " WHERE (SalesOrderMaster.SalesOrderId = '" + OrderNo + "')";
        SqlCommand cmd = new SqlCommand(str, conn);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataSet ds = new DataSet();
        adp.Fill(ds);
        return ds;
    }

    public DataTable SelectServiceStatus()
    {
        PageConn pgcon = new PageConn();
        conn = pgcon.dynconn;
        string str = "SELECT     StatusId, StatusName " +
                    " FROM         ServiceStatusMaster";
        SqlCommand cmd = new SqlCommand(str, conn);
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        adp.Fill(dt);
        return dt;
    }
    public DataTable SelectProblemType()
    {
        PageConn pgcon = new PageConn();
        conn = pgcon.dynconn;
        SqlCommand cmd = new SqlCommand("Sp_Select_Problemtype", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable ds = new DataTable();
        adp.Fill(ds);
        return ds;
    }

    public DataTable SelectServiceDetail( int id)
    {
        PageConn pgcon = new PageConn();
        conn = pgcon.dynconn;
        string s = " SELECT     ServiceStatusMaster.StatusName AS Servicestatus, CustomerServiceCallMaster.CustomerServiceCallMasterId, CustomerServiceCallMaster.Entrydate,  " +
                     " CustomerServiceCallMaster.ProblemTitle, CustomerServiceCallMaster.ProblemDescription,   " +
                     " CustomerServiceCallMaster.CustomerId, CustomerServiceCallMaster.ServiceStatusId, Party_master.Compname, Party_master.Contactperson,  " +
                     " ProblemTypeMaster.ProblemName, ProblemTypeMaster.ProblemTypeId " +
                       " FROM         CustomerServiceCallMaster INNER JOIN " +
                     " ServiceStatusMaster ON CustomerServiceCallMaster.ServiceStatusId = ServiceStatusMaster.StatusId INNER JOIN " +
                     " User_master ON CustomerServiceCallMaster.CustomerId = User_master.UserID INNER JOIN " +
                     " Party_master ON User_master.PartyID = Party_master.PartyID INNER JOIN " +
                     " ProblemTypeMaster ON CustomerServiceCallMaster.ProblemTypeId = ProblemTypeMaster.ProblemTypeId " +
                    " WHERE     (CustomerServiceCallMaster.CustomerServiceCallMasterId = '" + id + "')" +
                       "  ORDER BY CustomerServiceCallMaster.Entrydate DESC";

        SqlCommand cmd = new SqlCommand(s, conn);
        cmd.CommandType = CommandType.Text;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        return dt;
    }
    public Int32 updateServiceStatus(int statusid, int problemtypeid, int id, string ServiceNotes)
    {
        PageConn pgcon = new PageConn();
        conn = pgcon.dynconn;
        try
        {
            string str = "UPDATE    CustomerServiceCallMaster " +
                        " SET      ProblemTypeId ='" + problemtypeid + "', ServiceStatusId ='" + statusid + "', ServiceNotes='"+ ServiceNotes.ToString() +"' " +
            " where  (CustomerServiceCallMaster.CustomerServiceCallMasterId = '" + id + "') ";

            SqlCommand cmd = new SqlCommand(str, conn);
            conn.Open();
            cmd.ExecuteNonQuery();
            conn.Close();
            return 1;
        }
        catch
        {
            return 0;
        }
    }
    public Int32 getMaxSerivceId()
    {
        PageConn pgcon = new PageConn();
        conn = pgcon.dynconn;
        SqlCommand cm = new SqlCommand("SELECT     MAX(CustomerServiceCallMasterId) AS Id FROM         CustomerServiceCallMaster", conn);
        cm.CommandType = CommandType.Text;
        SqlDataAdapter sadp = new SqlDataAdapter(cm);
        DataSet ds2 = new DataSet();
        sadp.Fill(ds2);
        int s = 0;
        if (ds2.Tables[0].Rows.Count > 0)
        {
            if (ds2.Tables[0].Rows[0]["Id"].ToString() != "")
            {
                s = Convert.ToInt32(ds2.Tables[0].Rows[0]["Id"]) + 1;
            }
            else
            {
                s = 1;
            }
        } 
        return s;
    }

    public DataTable GetProblemType()
    {
        PageConn pgcon = new PageConn();
        conn = pgcon.dynconn;
        SqlCommand cmd = new SqlCommand("Sp_Select_Problemtype", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable ds = new DataTable();
        adp.Fill(ds);
        return ds;
    }

    public DataTable getUsermaster()
    {
        PageConn pgcon = new PageConn();
        conn = pgcon.dynconn;
        SqlCommand cmd = new SqlCommand("SELECT UserID,Name FROM dbo.User_master  order by Name", conn);
        cmd.CommandType = CommandType.Text;
        SqlDataAdapter adp = new SqlDataAdapter(cmd);
        DataTable ds = new DataTable();
        adp.Fill(ds);
        return ds;

    }
    public DataTable getCustomerCrDrNotes()
    {
        PageConn pgcon = new PageConn();
        conn = pgcon.dynconn;
        string str = "SELECT  top 4   DocumentReferenceMasterTbl.RferenceId, DocumentReferenceMasterTbl.DocumentReferenceTypeId, Documentdetail_master.reffno,  "+
                     " Document_master.Docudate, DocumentReferenceMasterTbl.DocumentMasterId, Document_Amount.Amount, Documenttype_master.Name,  "+
                     " Document_master.UserId, Document_master.DocumentID, "+
                     " CASE WHEN dbo.Documenttype_master.Name = 'CustDr Notes' THEN 'Debit Notes' ELSE 'Credit Notes' END AS Name1,  "+
                     " Party_master.Compname, left(Party_master.Compname,12) + '..' as comp " +
                    " FROM         Document_Amount INNER JOIN "+
                     " DocumentReferenceMasterTbl ON Document_Amount.Document_Master_Id = DocumentReferenceMasterTbl.DocumentMasterId LEFT OUTER JOIN "+
                     " Documenttype_master INNER JOIN "+
                     " Documentdetail_master INNER JOIN "+
                     " Document_master ON Documentdetail_master.Master = Document_master.DocumentID ON  "+
                     " Documenttype_master.DocutypeID = Document_master.Docutype INNER JOIN "+
                     " Party_master INNER JOIN "+
                     " User_master ON Party_master.PartyID = User_master.PartyID ON Document_master.UserId = User_master.UserID ON  "+
                    "  DocumentReferenceMasterTbl.DocumentMasterId = Document_master.DocumentID "+
                    " order by Docudate desc   ";
        SqlCommand cmd = new SqlCommand(str, conn);
        cmd.CommandType = CommandType.Text;
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        da.Fill(dt);
        return dt;

    }


}
