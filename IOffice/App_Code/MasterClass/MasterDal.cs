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
using DS.Common;

/// <summary>
/// Summary description for MasterDal
/// </summary>
namespace DS.DO
{
    public class MasterDal : IDisposable
    {
        #region PrivateMembers
        private string _ConnectionString;
        private SqlConnection objConn;
        private SqlTransaction objTrans;
        #endregion

        public MasterDal()
        {
            _ConnectionString = GetConnectionString();
        }
        public MasterDal(string ConnectionString)
        {
            _ConnectionString = Convert.ToString(ConnectionString);
        }
        ~MasterDal()
        {
            // RollbackTransaction(); 
            CloseConnection();
            objConn = null;
            objTrans = null;
        }
        public void Dispose()
        {
            //RollbackTransaction(); commented by Dhaval Shah
            CloseConnection();
            objConn = null;
            objTrans = null;
            GC.SuppressFinalize(this);
        }

        public string GetConnectionString()
        {
            return (ConfigurationManager.ConnectionStrings["ConStr"].ToString());
        }
        public void CloseConnection()
        {
            try
            {
                if (objConn != null)
                {
                    if (objConn.State != ConnectionState.Closed)
                    {
                        objConn.Close();
                    }
                }
            }
            catch
            {
            }
        }
        public void OpenConnection()
        {
            if (objConn == null)
            {
                objConn = new SqlConnection(_ConnectionString);
            }
            if (objConn.State == ConnectionState.Closed)
            {
                objConn.Open();
            }
        }
        public void BeginTransaction()
        {
            OpenConnection();
            if (objTrans == null)
            {
                objTrans = objConn.BeginTransaction();
            }
        }
        public void CommitTransaction()
        {
            if (objTrans != null)
            {
                objTrans.Commit();
                objTrans.Dispose();
                objTrans = null;
            }
            CloseConnection();
        }
        public void RollbackTransaction()
        {
            if (objTrans != null)
            {
                objTrans.Rollback();
                objTrans.Dispose();
                objTrans = null;
            }
            CloseConnection();
        }

        public DBParams ExecuteNonQuery(string storedProcName, DBParams parameterValues)
        {
            if (objTrans != null)
                return ExecuteNonQuery(storedProcName, parameterValues, true);
            else
                return ExecuteNonQuery(storedProcName, parameterValues, false);
        }
        public DBParams ExecuteNonQuery(string storedProcName, DBParams parameterValues, bool IsTransaction)
        {
            bool IsTrans = IsTransaction;
            DBParams retVal = new DBParams();
            int rowsAffected;

            SqlCommand cmd = GetCommand(CommandType.StoredProcedure, storedProcName);

            //ravi patel
            //AddErrorParameter(parameterValues);
            retVal = AddParameters(parameterValues, cmd);
            PrepareCommand(cmd, IsTrans);
            //cmd.CommandTimeout = objTrans.Connection.ConnectionTimeout;
            rowsAffected = DoExecuteNonQuery(cmd);
            foreach (SqlParameter pRetVal in retVal)
            {
                pRetVal.Value = this.GetParameterValue(cmd, pRetVal.ParameterName);
            }
            return retVal;
        }
        public int ExecuteNonQuery(string SQLQuery)
        {
            if (objTrans != null)
                return ExecuteNonQuery(SQLQuery, true);
            else
                return ExecuteNonQuery(SQLQuery, false);
        }
        public int ExecuteNonQuery(string SQLQuery, bool IsTransaction)
        {
            bool IsTrans = IsTransaction;
            int rowsAffected;
            SqlCommand cmd = GetCommand(CommandType.Text, SQLQuery);
            PrepareCommand(cmd, IsTrans);
            //cmd.CommandTimeout = objTrans.Connection.ConnectionTimeout;
            rowsAffected = DoExecuteNonQuery(cmd);
            return rowsAffected;
        }

        public Resultset<Object> ExecuteScalar(string storedProcName, DBParams parameterValues)
        {
            //bool IsTrans = false;
            DBParams retVal = new DBParams();
            Object retScalarValue;

            SqlCommand cmd = GetCommand(CommandType.StoredProcedure, storedProcName);
            //ravi patel
            //AddErrorParameter(parameterValues);
            retVal = AddParameters(parameterValues, cmd);
            if (objTrans != null)
            {
                PrepareCommand(cmd, true);
            }
            else
            {
                PrepareCommand(cmd, false);
            }
            //cmd.CommandTimeout = objTrans.Connection.ConnectionTimeout;
            retScalarValue = DoExecuteScalar(cmd);
            foreach (SqlParameter pRetVal in retVal)
            {
                pRetVal.Value = this.GetParameterValue(cmd, pRetVal.ParameterName);
            }

            Resultset<Object> retResultset;
            retResultset = new Resultset<Object>(retScalarValue, retVal);
            return retResultset;

        }
        public object ExecuteScalar(string SQLQuery)
        {
            Object retScalarValue;

            SqlCommand cmd = GetCommand(CommandType.Text, SQLQuery);

            if (objTrans != null)
            {
                PrepareCommand(cmd, true);
            }
            else
            {
                PrepareCommand(cmd, false);
            }
            //cmd.CommandTimeout = objTrans.Connection.ConnectionTimeout;
            retScalarValue = DoExecuteScalar(cmd);

            return retScalarValue;

        }
        public Resultset<DataSet> ExecuteDataSet(string storedProcName, DBParams parameterValues)
        {
            DataSet retResultset = new DataSet();
            DBParams retVal;

            SqlCommand cmd = GetCommand(CommandType.StoredProcedure, storedProcName);
            cmd.CommandTimeout = 43200000;

           // AddErrorParameter(parameterValues);
            retVal = AddParameters(parameterValues, cmd);

            if (objTrans != null)
            {
                PrepareCommand(cmd, true);
            }
            else
            {
                PrepareCommand(cmd, false);
            }
            DoLoadDataSet(cmd, retResultset, new string[] { "Table" });

            foreach (SqlParameter pRetVal in retVal)
            {
                pRetVal.Value = GetParameterValue(cmd, pRetVal.ParameterName);
            }

            Resultset<DataSet> objReturnValue;
            objReturnValue = new Resultset<DataSet>(retResultset, retVal);
            return objReturnValue;
        }
        public DataSet ExecuteDataSet(string SQLQuery)
        {
            DataSet retResultset = new DataSet();
            SqlCommand cmd = GetCommand(CommandType.Text, SQLQuery);
            cmd.CommandTimeout = 43200000;

            if (objTrans != null)
            {
                PrepareCommand(cmd, true);
            }
            else
            {
                PrepareCommand(cmd, false);
            }
            DoLoadDataSet(cmd, retResultset, new string[] { "Table" });

            return retResultset;
        }
        public Resultset<SqlDataReader> ExecuteDataReader(string storedProcName, DBParams parameterValues)
        {
            SqlDataReader oDataReader;
            DBParams retParams;

            SqlCommand cmd = GetCommand(CommandType.StoredProcedure, storedProcName);
            cmd.CommandTimeout = 43200000;
            //ravi patel
            //AddErrorParameter(parameterValues);
            retParams = AddParameters(parameterValues, cmd);

            if (objTrans != null)
            {
                PrepareCommand(cmd, true);
            }
            else
            {
                PrepareCommand(cmd, false);
            }
            //if (Transaction.Current != null)
            if (objTrans != null)
            {
                oDataReader = DoExecuteReader(cmd, CommandBehavior.Default);
            }
            else
            {
                oDataReader = DoExecuteReader(cmd, CommandBehavior.Default);
            }
            //CloseConnection();
            foreach (SqlParameter pRetVal in retParams)
            {
                pRetVal.Value = GetParameterValue(cmd, pRetVal.ParameterName);
            }

            Resultset<SqlDataReader> objReturnValue;
            objReturnValue = new Resultset<SqlDataReader>(oDataReader, retParams);
            return objReturnValue;
        }
        public SqlDataReader ExecuteDataReader(string SQL)
        {
            SqlDataReader oDataReader;
            DBParams retParams;

            SqlCommand cmd = GetCommand(CommandType.Text, SQL);
            cmd.CommandTimeout = 43200000;

            if (objTrans != null)
            {
                PrepareCommand(cmd, true);
            }
            else
            {
                PrepareCommand(cmd, false);
            }

            if (objTrans != null)
            {
                oDataReader = DoExecuteReader(cmd, CommandBehavior.Default);
            }
            else
            {
                oDataReader = DoExecuteReader(cmd, CommandBehavior.Default);
            }

            return oDataReader;
        }

        private object DoExecuteScalar(SqlCommand command)
        {
            try
            {
                //DateTime startTime = DateTime.Now;
                object returnValue = command.ExecuteScalar();
                //instrumentationProvider.FireCommandExecutedEvent(startTime);
                return returnValue;
            }
            catch (Exception)
            {
                RollbackTransaction();
                //instrumentationProvider.FireCommandFailedEvent(command.CommandText, ConnectionStringNoCredentials, e);
                throw;
            }
        }
        private SqlDataReader DoExecuteReader(SqlCommand command, CommandBehavior cmdBehavior)
        {
            try
            {
                //DateTime startTime = DateTime.Now;
                SqlDataReader reader = command.ExecuteReader(cmdBehavior);
                //instrumentationProvider.FireCommandExecutedEvent(startTime);
                return reader;
            }
            catch (Exception)
            {
                RollbackTransaction();
                //instrumentationProvider.FireCommandFailedEvent(command.CommandText, ConnectionStringNoCredentials, e);
                throw;
            }
        }
        private int DoExecuteNonQuery(SqlCommand command)
        {
            try
            {
                //DateTime startTime = DateTime.Now;
                int rowsAffected = command.ExecuteNonQuery();
                //instrumentationProvider.FireCommandExecutedEvent(startTime);
                return rowsAffected;
            }
            catch (Exception)
            {
                //instrumentationProvider.FireCommandFailedEvent(command.CommandText, ConnectionStringNoCredentials, e);
                RollbackTransaction();
                throw;
            }
        }
        private void DoLoadDataSet(SqlCommand command, DataSet dataSet, string[] tableNames)
        {
            if (tableNames == null) throw new ArgumentNullException("tableNames");
            if (tableNames.Length == 0)
            {
                throw new ArgumentNullException("tableNames");// new ArgumentException(System.Resources.ExceptionTableNameArrayEmpty, "tableNames");
            }
            for (int i = 0; i < tableNames.Length; i++)
            {
                if (string.IsNullOrEmpty(tableNames[i])) throw new ArgumentNullException("tableNames");//new ArgumentException(Resources.ExceptionNullOrEmptyString, string.Concat("tableNames[", i, "]"));

            }

            using (SqlDataAdapter adapter = new SqlDataAdapter())
            {
                //((IDbDataAdapter)adapter).SelectCommand = command;
                adapter.SelectCommand = command;
                try
                {
                    adapter.Fill(dataSet);  
                }
                catch (Exception)
                {
                    RollbackTransaction();
                    //instrumentationProvider.FireCommandFailedEvent(command.CommandText, ConnectionStringNoCredentials, e);
                    throw;
                }
            }
        }

        //ravi patel
        //private void AddErrorParameter(DBParams oParams)
        //{
        //    bool IsAddErrParam;
        //    IsAddErrParam = false;
        //    if (oParams.Count > 0)
        //    {
        //        if (oParams[0].ParameterName.ToUpper() != "@ERRNO")
        //        {
        //            IsAddErrParam = true;
        //        }
        //    }
        //    else
        //    {
        //        IsAddErrParam = true;
        //    }

        //    if (IsAddErrParam == true)
        //    {
        //        SqlParameter sp = new SqlParameter();
        //        sp.ParameterName = "@ErrNo";
        //        sp.Value = 0;
        //        //sp.DbType = System.Data.DbType.Int32;
        //        sp.SqlDbType = SqlDbType.Int;
        //        sp.Direction = System.Data.ParameterDirection.Output;
        //        oParams.AddAt(sp, 0);
        //    }
        //}

        private void AddParameter(SqlParameter oParam, SqlCommand command)
        {
            SqlParameter sp = new SqlParameter();
            sp.ParameterName = oParam.ParameterName;
            sp.Value = oParam.Value;
            //sp.DbType = oParam.DbType;
            sp.SqlDbType = oParam.SqlDbType;
            sp.Direction = oParam.Direction;
            sp.Size = oParam.Size;
            sp.IsNullable = true;
            command.Parameters.Add(sp);
        }

        private SqlCommand GetCommand(CommandType commandType, string commandText)
        {
            SqlCommand command = new SqlCommand();
            command.CommandType = commandType;
            command.CommandText = commandText;
            return command;
        }
        private object GetParameterValue(SqlCommand command, string name)
        {
            return command.Parameters[name].Value;
        }
        private void PrepareCommand(SqlCommand command, bool IsTrans)
        {
            OpenConnection();
            if (IsTrans == true)
            {
                BeginTransaction();
                PrepareCommand(command, objTrans);
            }
            else
            {
                PrepareCommand(command, objConn);
            }
        }
        private void PrepareCommand(SqlCommand command, SqlTransaction transaction)
        {
            if (command == null) throw new ArgumentNullException("command");
            if (transaction == null) throw new ArgumentNullException("transaction");
            PrepareCommand(command, transaction.Connection);
            command.Transaction = transaction;
        }
        private void PrepareCommand(SqlCommand command, SqlConnection connection)
        {
            if (command == null) throw new ArgumentNullException("command");
            if (connection == null) throw new ArgumentNullException("connection");
            command.Connection = connection;
            //command.CommandTimeout = connection.ConnectionTimeout;
        }
        private DBParams AddParameters(DBParams parameterValues, SqlCommand cmd)
        {
            DBParams retVal = new DBParams();

            string parameterprefix = ConfigurationManager.AppSettings["ParameterPrefix"].ToString();
            foreach (SqlParameter pval in parameterValues)
            {
                pval.ParameterName = pval.ParameterName.Replace("@", parameterprefix);
                switch (pval.Direction)
                {
                    case ParameterDirection.Input:
                        //cmd.Parameters.Add(pval);
                        AddParameter(pval, cmd);
                        break;
                    case ParameterDirection.InputOutput:
                        //cmd.Parameters.Add(pval);
                        AddParameter(pval, cmd);
                        retVal.Add(pval);
                        break;
                    case ParameterDirection.Output:
                        //cmd.Parameters.Add(pval);
                        AddParameter(pval, cmd);
                        retVal.Add(pval);
                        break;
                    case ParameterDirection.ReturnValue:
                        //cmd.Parameters.Add(pval);
                        AddParameter(pval, cmd);
                        retVal.Add(pval);
                        break;
                    default:
                        break;
                }
            }
            return retVal;
        }

    }
}
