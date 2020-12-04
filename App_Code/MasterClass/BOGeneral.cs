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
using DS.BO;
using System.Data.SqlClient;
using DS.DO;
using DS.Common;

/// <summary>
/// Summary description for BOGeneral
/// </summary>
namespace DS.Win.BO
{
    public class BOGeneral : EditableBusinessBase
    {

        public static bool ExecuteQuery(string SQL)
        {
            MasterDal oDal = new MasterDal();
            int intRet = oDal.ExecuteNonQuery(SQL);
            oDal.Dispose();
            oDal = null;
            if (intRet >= 0)
            {
                //DSExceptions.Messages.Add("", "Data saved", ExceptionType.Information);
                return true;
            }
            else
            {
                //DSExceptions.Messages.Add("", "Some error occured to ExecuteQuery", ExceptionType.Information);
                return false;
            }
        }



        public static long ExecuteQuery(string SQL, bool IsPKID)
        {
            MasterDal oDal = new MasterDal();
            long intRet = oDal.ExecuteNonQuery(SQL);
            if (IsPKID == true && (int)intRet > 0 && SQL.ToUpper().IndexOf("INSERT ") >= 0)
            {
                intRet = Convert.ToInt32(oDal.ExecuteScalar("Select SCOPE_IDENTITY()"));
            }
            oDal.Dispose();
            oDal = null;
            if (intRet >= 0)
            {
                //DSExceptions.Messages.Add("", "Data saved", ExceptionType.Information);
                return intRet;
            }
            else
            {
                //DSExceptions.Messages.Add("", "Some error occured to ExecuteQuery", ExceptionType.Information);
                return intRet;
            }
        }

        public static void ExceteQueryWithStoreprocedure(String strStoreProceureName, DBParams dbpParameterName)
        {
            MasterDal oDal = new MasterDal();
            oDal.ExecuteNonQuery(strStoreProceureName, dbpParameterName);
            oDal.Dispose();
            oDal = null;
            //return oDStore;
        }
        //public static DataSet GetDatasetUsingProcedure(string OstoredProcName, DBParams OparameterValues)
        //{
        //    MasterDal oDal = new MasterDal();

        //    DataSet oDS = oDal.ExecuteDataSet(OstoredProcName,OparameterValues);
        //    oDal.Dispose();
        //    oDal = null;
        //    return oDS;
        //}
        public static DataSet GetDataset(String SQL)
        {
            MasterDal oDal = new MasterDal();
            DataSet oDS = oDal.ExecuteDataSet(SQL);
            oDal.Dispose();
            oDal = null;
            return oDS;
        }

        public static Resultset<DataSet> GetDataSetWithStoreprocedure(String strStoreProceureName, DBParams dbpParameterName)
        {
            MasterDal oDal = new MasterDal();
            Resultset<DataSet> oDStore = oDal.ExecuteDataSet(strStoreProceureName, dbpParameterName);
            oDal.Dispose();
            oDal = null;
            return oDStore;
        }

        public static SqlDataReader GetDataReader(String SQL)
        {
            MasterDal oDal = new MasterDal();
            SqlDataReader oDR = oDal.ExecuteDataReader(SQL);
            oDal.Dispose();
            oDal = null;
            return oDR;
        }
        public static object GetScalar(string SQL)
        {
            MasterDal oDal = new MasterDal();
            object obj = oDal.ExecuteScalar(SQL);
            oDal.Dispose();
            oDal = null;
            return obj;
        }


        public static string MakeInsertable(object strValue, bool NeedSingleQuote)
        {
            string strReturn;
            if (strValue == null)
                strReturn = "Null";
            else if (strValue.ToString() == "")
            {
                strReturn = "Null";
            }
            else
            {
                if (NeedSingleQuote == true)
                {
                    strReturn = "'" + strValue.ToString().Replace("'", "''").Replace(((char)0).ToString(), "") + "'";
                }
                else
                {
                    strReturn = strValue.ToString().Replace("'", "''").Replace(((char)0).ToString(), "");
                }
            }
            return (strReturn.Trim());
        }

        public static bool isNumeric(object Value, System.TypeCode TC)
        {
            bool b = false;
            if (TC == System.TypeCode.Double)
            {
                double a;
                b = Double.TryParse((string)Value, out a);
            }
            return b;
        }

        public static class CommanConstant
        {
            public class QueryString
            {
                public const string Event_ID = "Event_ID";
            }
        }

        public static string MakeQueryForDataExistChecking(string TableName, params string[] WhereConditions)
        {
            if (WhereConditions.Length == 0)
            {
                //DSExceptions.Messages.Add("", "Atleast one WhereCondition should be in method calling", ExceptionType.Information);
                return "";
            }
            string WhereClause = "";
            for (int i = 0; i < WhereConditions.Length; i++)
            {
                if (WhereClause == "")
                {
                    WhereClause = WhereConditions[i].ToString();
                }
                else
                {
                    WhereClause = WhereClause + " and " + WhereConditions[i].ToString();
                }
            }
            string SQL = "select 1 where exists(SELECT 1 from " + TableName + " where " + WhereClause + ")";
            return SQL;
        }

        public static string MakeQueryForDataDeleteChecking(params string[] TABLE_COLUMN_VALUE)
        {
            try
            {
                string strReturn = "";
                int intRemainder = 0;
                int intQuotient = Math.DivRem(TABLE_COLUMN_VALUE.Length, 3, out intRemainder);
                //if (intRemainder > 0)
                //{
                //    DSExceptions.Messages.Add("", "Wrong parameters for MakeQueryForDataDeleteChecking Method", ExceptionType.Information);
                //    return strReturn;
                //}
                for (int i = 0; i < TABLE_COLUMN_VALUE.Length; i = i + 3)
                {
                    string SQL = "select 1 where exists(SELECT 1 from " + TABLE_COLUMN_VALUE[i].ToString() + " where " + TABLE_COLUMN_VALUE[i + 1].ToString() + "=" + TABLE_COLUMN_VALUE[i + 2].ToString() + ")";
                    if (strReturn == "")
                    {
                        strReturn = SQL;
                    }
                    else
                    {
                        strReturn = strReturn + ";" + SQL;
                    }
                }
                return strReturn;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }


}