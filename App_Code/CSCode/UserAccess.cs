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
/// Summary description for UserAccess
/// </summary>
public class UserAccess
{
   
	public UserAccess()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public static Boolean Usercon(string tablename, string tblforeignkeyid,String countId,string std,string etd,String CID,String Inque)
    {
        PageConn pgcon = new PageConn();

         SqlConnection con = pgcon.dynconn;
        Boolean ins = true;
     SqlCommand   cmd = new SqlCommand();
       DataTable dt = new DataTable();
    
       SqlCommand cmdt = new SqlCommand();
       DataTable dtt = new DataTable();
        // "Select DocumentImageMaster.*,DoucmentMaster.* from DocumentImageMaster inner join DocumentImageMaster.DocumentMasterId=DoucmentMaster.DocumentId where DoucmentMaster.DocumentId='@DocumentId and CID=@CID"
        // "Select DocumentImageMaster.* from DocumentImageMaster where DocumentMasterId='@DocumentId"
       string forkey = "";
        if (tblforeignkeyid.Length > 0)
       {
           forkey = " and  ForeignkeyrecordId='" + ClsEncDesc.Encrypted(tblforeignkeyid) + "'";
       }
        string adf = "Select ClientProductTableMaster.*,ClientProductRecordsAllowed.* from  ClientProductTableMaster inner join ClientProductRecordsAllowed on ClientProductRecordsAllowed.ClientProductTblId=ClientProductTableMaster.Id where TableName='" + ClsEncDesc.Encrypted(tablename) + "' and PricePlanId='" + ClsEncDesc.Encrypted(HttpContext.Current.Session["PriceId"].ToString()) + "'" + forkey;
       cmd = new SqlCommand(adf, PageConn.busclient());
        SqlDataAdapter ad = new SqlDataAdapter(cmd);
        ad.Fill(dt);
        if (dt.Rows.Count > 0)
        {
            string pera = "";

            if(Convert.ToString(dt.Rows[0]["ForeignkeyrecordName"])!="")
            {
                pera = "and " + ClsEncDesc.Decrypted(Convert.ToString(dt.Rows[0]["ForeignkeyrecordName"])) + "=" + tblforeignkeyid;
            }
            try
            {
                string adrt = "Select Count(" + tablename + "." + countId + ") from  " + Inque + " where " + CID + "='" + HttpContext.Current.Session["Comid"].ToString() + "' " + pera + std + etd;

                cmdt = new SqlCommand(adrt, con);
                SqlDataAdapter adt = new SqlDataAdapter(cmdt);
                adt.Fill(dtt);
                if (Convert.ToString(dtt.Rows[0][0]) != "")
                {
                    string pagerecod = ClsEncDesc.Decrypted(dt.Rows[0]["RecordsAllowed"].ToString());
                    if (Convert.ToInt32(pagerecod) <= Convert.ToInt32(dtt.Rows[0][0]))
                    {
                        ins = false;
                    }
                }
            }
            catch
            {
                ins = true;
            }
        }
        return ins;
    }
}
