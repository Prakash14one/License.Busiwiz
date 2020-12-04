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
/// Summary description for CostGroup
/// </summary>
public class CostGroup
{
    protected static SqlConnection condef = new SqlConnection(PageConn.connnn);
	public CostGroup()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    protected static DataTable select(string str)
    {
        SqlCommand cmd = new SqlCommand(str, condef);
        SqlDataAdapter dtp = new SqlDataAdapter(cmd);
        DataTable dt = new DataTable();
        dtp.Fill(dt);

        return dt;

    }
    protected static double opstock(string op, string rptid, string whid)
    {
        double opsto = 0;
        DataTable dspv = new DataTable();
        dspv = (DataTable)select("  select InventoryWarehouseMasterAvgCostTbl.Qty,InventoryWarehouseMasterAvgCostTbl.Rate from InventoryWarehouseMasterAvgCostTbl inner join InventoryWarehouseMasterTbl on InventoryWarehouseMasterTbl.InventoryWarehouseMasterId=InventoryWarehouseMasterAvgCostTbl.InvWMasterId where InventoryWarehouseMasterTbl.WarehouseId='" +whid + "' and  DateUpdated='" + op + "' and Tranction_Master_Id In('00000','--')");


        foreach (DataRow dt in dspv.Rows)
        {
            opsto += Convert.ToDouble(dt["Qty"]) * Convert.ToDouble(dt["Rate"]);
        }
        //DataTable dtbal = (DataTable)select("Select AccountBalance.Balance,Account_Balance_Id from AccountBalance  inner join AccountMaster on AccountMaster.Id=AccountBalance.AccountMasterId where AccountId='8000' and Report_Period_Id='" + rptid+ "'  and Whid='" + whid + "'");
        //if (dtbal.Rows.Count > 0)
        //{
        //    opsto = opsto + Convert.ToDouble(dtbal.Rows[0]["Balance"]);
        //}

        return opsto;
    }
    protected static double opstockoth(string op, string rptid, string whid)
    {
        double totalcost = 0;


        DataTable dtodop = new DataTable();
        DataTable dtodop1 = new DataTable();
        DataTable DRS = select(" Select distinct InventoryWarehouseMasterTbl.InventoryWarehouseMasterId from  InventoryWarehouseMasterAvgCostTbl  inner join InventoryWarehouseMasterTbl on InventoryWarehouseMasterTbl.InventoryWarehouseMasterId=InventoryWarehouseMasterAvgCostTbl.InvWMasterId where   InventoryWarehouseMasterTbl.WareHouseId='" +whid + "' " + op + "");
        foreach (DataRow dtra in DRS.Rows)
        {
            double TotalAvgBal = 0;
            double AvgQtyAvail = 0;
            double AvgCostFinal = 0;
            double finaltotalqty = 0;

            DataTable ds1451 = select("select * from InventoryWarehouseMasterAvgCostTbl   where   InventoryWarehouseMasterAvgCostTbl.InvWMasterId='" + dtra["InventoryWarehouseMasterId"] + "' " + op + " order by DateUpdated,IWMAvgCostId  ");
            if (ds1451.Rows.Count > 0)
            {
                foreach (DataRow dtr in ds1451.Rows)
                {
                    double avgqty = 0;
                    double avgrate = 0;
                    double TotalAvgBalsub = 0;
                    double totalqtycount = 0;


                    if (Convert.ToString(dtr["Qty"]) != "" && Convert.ToString(dtr["Rate"]) != "")
                    {

                        avgqty = Convert.ToDouble(dtr["Qty"].ToString());
                        avgrate = Convert.ToDouble(dtr["Rate"].ToString());


                    }
                    if (Convert.ToString(dtr["Qty"]) != "")
                    {
                        totalqtycount = Convert.ToDouble(dtr["Qty"].ToString());
                    }



                    AvgQtyAvail += avgqty;
                    finaltotalqty += totalqtycount;

                    if (finaltotalqty == 0)
                    {
                        TotalAvgBal = 0;
                        AvgQtyAvail = 0;
                        AvgCostFinal = 0;
                        finaltotalqty = 0;
                        avgqty = 0;
                        avgrate = 0;
                        TotalAvgBalsub = 0;
                        totalqtycount = 0;

                    }

                    TotalAvgBalsub = avgqty * avgrate;
                    TotalAvgBal += TotalAvgBalsub;
                }
                if (TotalAvgBal == 0 && AvgQtyAvail == 0)
                {
                    AvgCostFinal = 0;

                }
                else
                {
                    AvgCostFinal = TotalAvgBal / AvgQtyAvail;
                    AvgCostFinal = Math.Round(AvgCostFinal, 2);
                }
            }

            totalcost += finaltotalqty * AvgCostFinal;
        }
        //DataTable dtbal = (DataTable)select("Select AccountBalance.Balance,Account_Balance_Id from AccountBalance  inner join AccountMaster on AccountMaster.Id=AccountBalance.AccountMasterId where AccountId='8000' and Report_Period_Id='" + ViewState["period"] + "'  and Whid='" + ddwarehouse.SelectedValue + "'");
        // if (dtbal.Rows.Count > 0)
        // {
        //     totalcost = totalcost + Convert.ToDouble(dtbal.Rows[0]["Balance"]);
        // }

        totalcost = Math.Round(totalcost, 2);
        
        return totalcost;

    }
    public static void fillcost(string Whid,string sdate,string edate,string rptid,bool abv,string acc)
    {
     
        string invopstock = "0";
        string invClosstock = "0";
        
        SqlCommand cds = new SqlCommand("Delete from  TranctionMaster where Whid='" + Whid + "' and UserId='100011'",condef);
        if (condef.State.ToString() != "Open")
        {
            condef.Open();
        }
        cds.ExecuteNonQuery();
        condef.Close();

        SqlCommand cds1 = new SqlCommand("Delete from  Tranction_Details where Whid='" + Whid + "' and DiscEarn='100011'", condef);
        if (condef.State.ToString() != "Open")
        {
            condef.Open();
        }
        cds1.ExecuteNonQuery();
        condef.Close();
        if (abv == true)
        {
            DataTable dtod = select("select StartDate,Enddate from [ReportPeriod] where Whid='" + Whid + "' and Active='1'");
            string fb = "";
            string serchdate = "";
            if (dtod.Rows.Count > 0)
            {


                fb = Convert.ToDateTime(sdate).AddDays(-1).ToShortDateString();
                serchdate = Convert.ToDateTime(sdate).ToString();


             
            }

            if (Convert.ToDateTime(serchdate) == Convert.ToDateTime(dtod.Rows[0]["StartDate"]))
            {

                double opto = opstock(serchdate, rptid, Whid);
                invopstock = Convert.ToString(opto);



            }
            else
            {
                fb = Convert.ToDateTime(sdate).AddDays(-1).ToShortDateString();
                string sk1 = " and InventoryWarehouseMasterAvgCostTbl.DateUpdated Between '" + dtod.Rows[0]["StartDate"].ToString() + "' and'" + fb + "'";
                double opto = opstockoth(sk1, rptid, Whid);
                invopstock = Convert.ToString(opto);

            }



            fb = edate;

            string sk11 = " and InventoryWarehouseMasterAvgCostTbl.DateUpdated Between  '" + sdate + "' and '" + fb + "'";


            double optoc = opstockoth(sk11, rptid, Whid);
            double totcrdit = 0;
            double totdebit = 0;
            double balance = 0;
            DataTable dtcredit = new DataTable();
            if (acc != "ABCDR")
            {
                dtcredit = (DataTable)select("Select sum(AmountCredit) as AmountCredit from Tranction_Details inner join TranctionMaster on TranctionMaster.Tranction_Master_Id=Tranction_Details.Tranction_Master_Id where Tranction_Details.Whid='" + Whid + "' and TranctionMaster.Whid='" + Whid + "' and AccountCredit='8000' and TranctionMaster.Date between '" + dtod.Rows[0]["StartDate"].ToString() + "' and '" + fb + "'");
                if (dtcredit.Rows.Count > 0)
                {
                    if (dtcredit.Rows[0]["AmountCredit"].ToString() != "")
                    {

                        totcrdit = Convert.ToDouble(dtcredit.Rows[0]["AmountCredit"].ToString());

                    }
                }
                DataTable dtamtd1 = new DataTable();
                dtamtd1 = (DataTable)select("Select sum(AmountDebit) as AmountDebit from Tranction_Details inner join TranctionMaster on TranctionMaster.Tranction_Master_Id=Tranction_Details.Tranction_Master_Id where Tranction_Details.Whid='" + Whid + "' and TranctionMaster.Whid='" + Whid + "' and AccountDebit='8000' and TranctionMaster.Date between '" + dtod.Rows[0]["StartDate"].ToString() + "' and '" + fb + "'");
                if (dtamtd1.Rows.Count > 0)
                {
                    if (dtamtd1.Rows[0]["AmountDebit"].ToString() != "")
                    {

                        totdebit = Convert.ToDouble(dtamtd1.Rows[0]["AmountDebit"].ToString());
                    }
                }

                balance = totdebit - totcrdit;
            }
            invClosstock = Convert.ToString(optoc + balance);



            SqlCommand cd3 = new SqlCommand("Sp_Insert_TranctionMasterRetIdentity", condef);


            cd3.CommandType = CommandType.StoredProcedure;
            cd3.Parameters.AddWithValue("@Date", sdate);
            cd3.Parameters.AddWithValue("@EntryNumber", Convert.ToInt32(0));
            cd3.Parameters.AddWithValue("@EntryTypeId", "3");
            cd3.Parameters.AddWithValue("@UserId", "100011");
            cd3.Parameters.AddWithValue("@Tranction_Amount", Convert.ToDecimal(invopstock));
            cd3.Parameters.AddWithValue("@whid", Whid);

            cd3.Parameters.AddWithValue("@compid", HttpContext.Current.Session["Comid"]);


            cd3.Parameters.Add(new SqlParameter("@Tranction_Master_Id", SqlDbType.Int));
            cd3.Parameters["@Tranction_Master_Id"].Direction = ParameterDirection.Output;
            cd3.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
            cd3.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;

            if (condef.State.ToString() != "Open")
            {
                condef.Open();
            }
            int Id1 = (int)cd3.ExecuteNonQuery();
            Id1 = Convert.ToInt32(cd3.Parameters["@Tranction_Master_Id"].Value);
            condef.Close();

             string a6 = "INSERT INTO dbo.Tranction_Details(AccountDebit,AmountDebit,Tranction_Master_Id" +
                        " ,DateTimeOfTransaction,compid,whid,DiscEarn)" +
                        " VALUES('8003','" + Convert.ToDecimal(invopstock) + "'" +
                        " ,'" + Id1 + "','" + sdate + "','" + HttpContext.Current.Session["Comid"] + "','" +Whid + "','100011')";

             if (condef.State.ToString() != "Open")
             {
                 condef.Open();
             }

                SqlCommand cd4 = new SqlCommand(a6, condef);
               
                cd4.ExecuteNonQuery();
                condef.Close();

                string a61 = "INSERT INTO dbo.Tranction_Details(AccountCredit,AmountCredit,Tranction_Master_Id" +
                            " ,DateTimeOfTransaction,compid,whid,DiscEarn)" +
                            " VALUES('8000','" + Convert.ToDecimal(invopstock) + "'" +
                            " ,'" + Id1 + "','" + sdate + "','" + HttpContext.Current.Session["Comid"] + "','" + Whid + "','100011')";

                if (condef.State.ToString() != "Open")
                {
                    condef.Open();
                }

                SqlCommand cd41 = new SqlCommand(a61, condef);

                cd41.ExecuteNonQuery();
                condef.Close();

            //Closing Entry

                SqlCommand cd31 = new SqlCommand("Sp_Insert_TranctionMasterRetIdentity", condef);


                cd31.CommandType = CommandType.StoredProcedure;
                cd31.Parameters.AddWithValue("@Date", sdate);
                cd31.Parameters.AddWithValue("@EntryNumber", Convert.ToInt32(0));
                cd31.Parameters.AddWithValue("@EntryTypeId", "3");
                cd31.Parameters.AddWithValue("@UserId", "100011");
                cd31.Parameters.AddWithValue("@Tranction_Amount", Convert.ToDecimal(invClosstock));
                cd31.Parameters.AddWithValue("@whid", Whid);

                cd31.Parameters.AddWithValue("@compid", HttpContext.Current.Session["Comid"]);


                cd31.Parameters.Add(new SqlParameter("@Tranction_Master_Id", SqlDbType.Int));
                cd31.Parameters["@Tranction_Master_Id"].Direction = ParameterDirection.Output;
                cd31.Parameters.Add(new SqlParameter("@ReturnValue", SqlDbType.Int));
                cd31.Parameters["@ReturnValue"].Direction = ParameterDirection.ReturnValue;

                if (condef.State.ToString() != "Open")
                {
                    condef.Open();
                }
                int Id11 = (int)cd31.ExecuteNonQuery();
                Id11 = Convert.ToInt32(cd31.Parameters["@Tranction_Master_Id"].Value);
                condef.Close();

                string a6z = "INSERT INTO dbo.Tranction_Details(AccountDebit,AmountDebit,Tranction_Master_Id" +
                           " ,DateTimeOfTransaction,compid,whid,DiscEarn)" +
                           " VALUES('8000','" + Convert.ToDecimal(invClosstock) + "'" +
                           " ,'" + Id11 + "','" + sdate + "','" + HttpContext.Current.Session["Comid"] + "','" + Whid + "','100011')";

                if (condef.State.ToString() != "Open")
                {
                    condef.Open();
                }

                SqlCommand cd4z = new SqlCommand(a6z, condef);

                cd4z.ExecuteNonQuery();
                condef.Close();

                string a61z = "INSERT INTO dbo.Tranction_Details(AccountCredit,AmountCredit,Tranction_Master_Id" +
                            " ,DateTimeOfTransaction,compid,whid,DiscEarn)" +
                            " VALUES('8003','" + Convert.ToDecimal(invClosstock) + "'" +
                            " ,'" + Id11 + "','" + sdate + "','" + HttpContext.Current.Session["Comid"] + "','" + Whid + "','100011')";

                if (condef.State.ToString() != "Open")
                {
                    condef.Open();
                }

                SqlCommand cd41z = new SqlCommand(a61z, condef);

                cd41z.ExecuteNonQuery();
                condef.Close();
        }
    }
}
