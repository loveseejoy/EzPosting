using System;
using System.Data;
using System.Data.SqlClient;

namespace EZPost.ServiceComm
{
    public class SqlHelper
    {
        private SqlConnection _connection;
        private SqlTransaction _dbTran;
        private static string _connectionString = "";
        public SqlHelper(string connectionStrings)
        {
            if (_connection == null)
            {
                _connection = new SqlConnection(connectionStrings);
            }
            _connectionString = connectionStrings;
        }

        #region Connection
        public void OpenConncetion(string connectionString)
        {
            if (_connection == null)
            {
                _connection = new SqlConnection(connectionString);
                _connection.Open();
            }
            else if (_connection.State == System.Data.ConnectionState.Closed)
            {
                _connection.Open();
            }
            else if (_connection.State == System.Data.ConnectionState.Broken && _connection.State == System.Data.ConnectionState.Connecting)
            {
                _connection.Close();
                _connection.Open();
            }
        }

        public void OpenConnection()
        {

        }

        public void CloseConnection()
        {
            if (_connection != null)
            {
                _connection.Close();
            }
        }
        #endregion

        #region DB Operate

        public int ExecuteCommand(string connectionString, string safeSql)
        {
            OpenConncetion(connectionString);
            SqlCommand cmd = new SqlCommand(safeSql, _connection);
            cmd.CommandTimeout = 300;
            int result = cmd.ExecuteNonQuery();
            CloseConnection();
            return result;
        }

        public int ExecuteCommand(string connectionString, string sql, params SqlParameter[] values)
        {
            OpenConncetion(connectionString);
            SqlCommand cmd = new SqlCommand(sql, _connection);
            cmd.Parameters.AddRange(values);
            int result = cmd.ExecuteNonQuery();
            CloseConnection();
            return result;
        }


        public int GetScalar(string connectionString, string safeSql)
        {
            OpenConncetion(connectionString);
            SqlCommand cmd = new SqlCommand(safeSql, _connection);
            int result = Convert.ToInt32(cmd.ExecuteScalar());
            CloseConnection();
            return result;
        }

        public int GetScalar(string connectionString, string sql, params SqlParameter[] values)
        {
            OpenConncetion(connectionString);
            SqlCommand cmd = new SqlCommand(sql, _connection);
            cmd.Parameters.AddRange(values);
            int result = Convert.ToInt32(cmd.ExecuteScalar());
            CloseConnection();
            return result;
        }

        public SqlDataReader GetReader(string connectionString, string safeSql)
        {
            OpenConncetion(connectionString);
            SqlCommand cmd = new SqlCommand(safeSql, _connection);
            SqlDataReader reader = cmd.ExecuteReader();
            return reader;
        }

        public SqlDataReader GetReader(string connectionString, string sql, params SqlParameter[] values)
        {
            OpenConncetion(connectionString);
            SqlCommand cmd = new SqlCommand(sql, _connection);
            cmd.Parameters.AddRange(values);
            SqlDataReader reader = cmd.ExecuteReader();
            return reader;
        }

        public DataTable GetDataTable(string safeSql)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand(safeSql, _connection);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            return ds.Tables[0];
        }

        public DataTable GetDataTable(string sql, params SqlParameter[] values)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand(sql, _connection);
            cmd.Parameters.AddRange(values);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            return ds.Tables[0];
        }

        public DataSet GetDataSet(string safeSql)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand(safeSql, _connection);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            return ds;
        }

        public DataSet GetDataSet(string sql, params SqlParameter[] values)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand(sql, _connection);
            cmd.Parameters.AddRange(values);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            return ds;
        }

        #endregion

        #region Procedure
        public DataTable ExecuteProdecure(string spName)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand(spName, _connection);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            return ds.Tables[0];
        }
        public string ExecuteProdecureReturnInt(string sql, params SqlParameter[] values)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand(sql, _connection);
            cmd.Parameters.AddRange(values);
            cmd.CommandType = CommandType.StoredProcedure;
            string result = cmd.ExecuteScalar().ToString();
            CloseConnection();
            return result;
        }


        public DataTable ExecuteProdecure(string sql, params SqlParameter[] values)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand(sql, _connection);
            cmd.Parameters.AddRange(values);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            return ds.Tables[0];
        }

        public DataSet ExecuteProdecureDataSet(string sql, params SqlParameter[] values)
        {
            DataSet ds = new DataSet();
            SqlCommand cmd = new SqlCommand(sql, _connection);
            cmd.CommandTimeout = 600;
            if (values != null)
            {
                cmd.Parameters.AddRange(values);
            }
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(ds);
            return ds;
        }
        public void ExecuteProdecureNoReturn(string spName, params SqlParameter[] values)
        {
            SqlCommand cmd = new SqlCommand(spName, _connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 600;
            cmd.Parameters.AddRange(values);
            OpenConncetion(_connectionString);
            cmd.ExecuteNonQuery();
            CloseConnection();
        }

        #endregion

        #region Transaction

        #region DbTran
        public SqlTransaction DbTran
        {
            get
            {
                return this._dbTran;
            }
        }
        #endregion

        #region ConnectionTransaction
        private SqlConnection _connectionTransaction;
        public SqlConnection ConnectionTransaction
        {
            get
            {
                if (_connectionTransaction == null)
                {
                    _connectionTransaction = new SqlConnection(_connectionString);
                }

                return _connectionTransaction;
            }
        }

        public void OpenTransactionConncetion()
        {
            if (_connectionTransaction == null)
            {
                _connectionTransaction = new SqlConnection(_connectionString);
                _connectionTransaction.Open();
            }
            else if (_connectionTransaction.State == System.Data.ConnectionState.Closed)
            {
                _connectionTransaction.Open();
            }
            else if (_connectionTransaction.State == System.Data.ConnectionState.Broken && _connectionTransaction.State == System.Data.ConnectionState.Connecting)
            {
                _connectionTransaction.Close();
                _connectionTransaction.Open();
            }
        }

        public void CloseConnectionTransaction()
        {
            if (_connectionTransaction != null)
            {
                _connectionTransaction.Close();
            }
        }
        #endregion

        /// <summary>
        /// Begin Transaction.
        /// </summary>
        public void BeginTransaction()
        {
            try
            {
                _connection = new SqlConnection(_connectionString);
                OpenTransactionConncetion();
                //if (DbTran == null)
                {
                    _dbTran = ConnectionTransaction.BeginTransaction();
                }
            }
            catch (Exception e)
            {
            }
        }

        /// <summary>
        /// Begin Transaction.
        /// </summary>
        /// <param name="isolationLevel">IsolationLevel</param>
        public void BeginTransaction(IsolationLevel isolationLevel)
        {
            try
            {
                if (DbTran != null)
                {
                    OpenTransactionConncetion();
                    _dbTran = ConnectionTransaction.BeginTransaction(isolationLevel);
                }
            }
            catch (Exception e)
            {

            }
        }

        /// <summary>
        /// Execute Transaction
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public int ExecuteTransaction(string sql, params SqlParameter[] values)
        {
            SqlCommand cmd = new SqlCommand(sql, ConnectionTransaction);
            cmd.Parameters.AddRange(values);
            cmd.Transaction = _dbTran;
            return cmd.ExecuteNonQuery();
        }
        public int ExecuteTransactionWithSp(string sql, params SqlParameter[] values)
        {
            OpenTransactionConncetion();
            SqlCommand cmd = new SqlCommand(sql, ConnectionTransaction);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddRange(values);
            cmd.Transaction = _dbTran;
            return cmd.ExecuteNonQuery();
        }

        public DataSet GetDataSetTransaction(string sql, params SqlParameter[] values)
        {
            SqlCommand cmd = new SqlCommand(sql, ConnectionTransaction);
            cmd.Parameters.AddRange(values);
            cmd.Transaction = _dbTran;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            return ds;
        }
        public DataSet GetDataSetTransactionWithSp(string sql, params SqlParameter[] values)
        {
            SqlCommand cmd = new SqlCommand(sql, ConnectionTransaction);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddRange(values);
            cmd.Transaction = _dbTran;
            cmd.CommandTimeout = 30000;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            return ds;
        }

        public DataTable GetDataTableTransaction(string sql, params SqlParameter[] values)
        {
            SqlCommand cmd = new SqlCommand(sql, ConnectionTransaction);
            cmd.Parameters.AddRange(values);
            cmd.Transaction = _dbTran;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            return ds.Tables[0];
        }

        public DataTable GetDataTableTransactionWithSp(string sql, params SqlParameter[] values)
        {
            SqlCommand cmd = new SqlCommand(sql, ConnectionTransaction);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddRange(values);
            cmd.Transaction = _dbTran;
            cmd.CommandTimeout = 300;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            return ds.Tables[0];
        }

        public DataSet GetDataSetTransaction(string sql)
        {
            SqlCommand cmd = new SqlCommand(sql, ConnectionTransaction);
            cmd.Transaction = _dbTran;
            cmd.CommandTimeout = 300;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            sda.Fill(ds);
            return ds;
        }

        /// <summary>
        /// Commit Transaction.
        /// </summary>
        public void CommitTransaction()
        {
            try
            {
                if (DbTran != null)
                {
                    DbTran.Commit();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message.ToString());
            }
            finally
            {
                CloseConnectionTransaction();
            }
        }

        /// <summary>
        /// RollBack Transaction.
        /// </summary>
        public void RollBackTransaction()
        {
            try
            {
                if (DbTran != null)
                    DbTran.Rollback();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message.ToString());
            }
        }

        #endregion

    }
}