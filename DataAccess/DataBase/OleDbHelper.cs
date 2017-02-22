using System;
using System.Data;
using System.Data.OleDb;
using System.Collections;

namespace YH.DataBase
{
    /// <summary>
    /// 数据访问基础类(基于OleDb) 
    /// 创建信息 JHB: ON 2015-04-30
    /// </summary>
    public class OleDbHelper
    {

        /// <summary>
        /// 创建一个OleDbCommand对象
        /// </summary>
        /// <param name="connStr">数据库链接字符串</param>   
        /// <param name="sql">要执行的查询语句</param>   
        /// <param name="parameters">执行SQL查询语句所需要的参数</param>
        /// <param name="commandType">执行的SQL语句的类型</param>
        private static OleDbCommand CreateDbCommand(string connStr, string sql, OleDbParameter[] parameters, CommandType commandType)
        {
            OleDbConnection connection = new OleDbConnection(connStr);
            OleDbCommand command = new OleDbCommand(sql, connection);
            command.CommandType = commandType;
            command.Connection = connection;
            if (!(parameters == null || parameters.Length == 0))
            {
                foreach (OleDbParameter parameter in parameters)
                {
                    command.Parameters.Add(parameter);
                }
            }
            return command;
        }

        /// <summary>
        /// 执行一个查询语句
        /// </summary>
        public static int Execute(string conn, String selectText, OleDbParameter[] parameters, CommandType commandType)
        {
            using (OleDbCommand command = CreateDbCommand(conn, selectText, parameters, commandType))
            {
                command.Connection.Open();
                int rows = command.ExecuteNonQuery();
                command.Connection.Close();
                return rows;
            }
        }

        /// <summary>
        /// 查询首行首列
        /// </summary>
        public static Object ExecuteScalar(string conn, String selectText, OleDbParameter[] parameters, CommandType commandType)
        {
            using (OleDbCommand command = CreateDbCommand(conn, selectText, parameters, commandType))
            {
                command.Connection.Open();
                object result = command.ExecuteScalar();
                command.Connection.Close();
                return result;
            }
        }

        /// <summary>
        /// 查询数据集
        /// </summary>
        public static DataSet ExecuteDataSet(string conn, String selectText, OleDbParameter[] parameters, CommandType commandType)
        {
            using (OleDbCommand command = CreateDbCommand(conn, selectText, parameters, commandType))
            {
                using (OleDbDataAdapter adapter = new OleDbDataAdapter())
                {
                    adapter.SelectCommand = command;
                    DataSet dsData = new DataSet();
                    adapter.Fill(dsData);
                    return dsData;
                }
            }
        }

        /// <summary>
        /// 查询关联的DataReader实例
        /// </summary>
        public static OleDbDataReader ExecuteReader(string conn, String selectText, OleDbParameter[] parameters, CommandType commandType)
        {
            using (OleDbCommand command = CreateDbCommand(conn, selectText, parameters, commandType))
            {
                command.Connection.Open();
                OleDbDataReader sqlDataReader = command.ExecuteReader(CommandBehavior.CloseConnection);
                return sqlDataReader;
            }
        }


        /// <summary>
        /// 更新数据表
        /// </summary>
        public static void UpdateDatatable(string conn, string selectText, DataTable dataTable)
        {
            using (OleDbCommand command = CreateDbCommand(conn, selectText, null, CommandType.Text))
            {
                using (OleDbDataAdapter adapter = new OleDbDataAdapter())
                {
                    adapter.SelectCommand = command;
                    OleDbCommandBuilder builder = new OleDbCommandBuilder(adapter);
                    adapter.Update(dataTable);
                    dataTable.AcceptChanges();
                }
            }
        }

        /// <summary>
        /// 更新数据表(事务提交方式)
        /// </summary>
        public static void UpdateDataTableTran(string conn, string selectText, DataTable dataTable)
        {
            using (OleDbConnection connection = new OleDbConnection(conn))
            {
                connection.Open();
                OleDbCommand command = connection.CreateCommand();
                OleDbTransaction transaction = connection.BeginTransaction();
                command.Connection = connection;
                command.Transaction = transaction;
                try
                {
                    command.CommandText = selectText;
                    OleDbDataAdapter adapter = new OleDbDataAdapter(command);
                    OleDbCommandBuilder builder = new OleDbCommandBuilder(adapter);
                    adapter.Update(dataTable);
                    transaction.Commit();
                    dataTable.AcceptChanges();
                }
                catch (Exception ex)
                {
                    try
                    {
                        transaction.Rollback();
                    }
                    catch (Exception ex2)
                    {
                        throw new Exception(ex2.Message);
                    }
                    throw new Exception(ex.Message);
                }
            }
        }


        /// <summary>
        /// 执行多条SQL语句(事务提交方式)
        /// </summary>
        public static void ExecuteTran(string conn, ArrayList sqlStringList)
        {
            using (OleDbConnection connection = new OleDbConnection(conn))
            {
                connection.Open();
                OleDbCommand command = connection.CreateCommand();
                OleDbTransaction transaction = connection.BeginTransaction();
                command.Connection = connection;
                command.Transaction = transaction;
                try
                {
                    for (int n = 0; n < sqlStringList.Count; n++)
                    {
                        string strsql = sqlStringList[n].ToString();
                        if (strsql.Trim().Length > 1)
                        {
                            command.CommandText = strsql;
                            command.ExecuteNonQuery();
                        }
                    }
                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    try
                    {
                        transaction.Rollback();
                    }
                    catch (Exception ex2)
                    {
                        throw new Exception(ex2.Message);
                    }
                    throw new Exception(ex.Message);
                }
            }
        }

    }
}
