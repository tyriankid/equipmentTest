using DataAccess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Utility;

namespace eT.Dal
{
    /// <summary>
    /// 自定义数据管理类
    /// </summary>
    public partial class CustomManager
    {

        /// <summary>
        /// 批量提交数据表
        /// </summary>
        public bool CommitDataTable(DataTable dtData, string selectSql, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
        {
            using (SqlConnection connection = new SqlConnection(DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).ConnectionString))
            {
                connection.Open();
                using (SqlCommand cmd = new SqlCommand(selectSql, connection))
                {
                    using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                    {
                        int returnValue = 0;
                        try
                        {
                            SqlCommandBuilder builder = new SqlCommandBuilder(da);
                            returnValue = da.Update(dtData);
                        }
                        catch (Exception ex)
                        {
                            returnValue = -1;
                        }
                        return (returnValue != -1) ? true : false;
                    }
                }
            }
        }

        /// <summary>
        /// 批量提交数据集
        /// </summary>
        public bool CommitDataSet(DataSet dsData, string[] arraySelectSql, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
        {
            int execCount = arraySelectSql.Length;
            using (SqlConnection connection = new SqlConnection(DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).ConnectionString))
            {
                SqlTransaction tran = null;
                try
                {
                    connection.Open();
                    tran = connection.BeginTransaction();
                    for (int i = 0; i < dsData.Tables.Count; i++)
                    {
                        SqlCommand cmd = new SqlCommand(arraySelectSql[i], connection, tran);
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        SqlCommandBuilder builder = new SqlCommandBuilder(da);
                        da.Update(dsData.Tables[i]);
                        execCount--;
                    }
                    if (execCount == 0)
                        tran.Commit();
                }
                catch
                {
                    if (tran != null)
                    {
                        tran.Rollback();
                    }
                }
                finally
                {
                    if (connection.State == ConnectionState.Open)
                    {
                        connection.Close();
                    }
                }
            }
            return (execCount == 0) ? true : false;
        }

        /// <summary>
        /// 获取分页数据(oracle里有row_number虚列。此方法是MS效仿oracle而增加的内置游标分标取虚列方法，分页快)
        /// </summary>
        /// <param name="tablename">表名</param>
        /// <param name="orderFields">排序字段</param>
        /// <param name="selectFields">查询字段</param>
        /// <param name="currPage">当前页码</param>
        /// <param name="pagesize">页容量</param>
        /// <param name="where">查询条件</param>
        /// <param name="dbname">使用数据库服务名</param>
        public DataTable SelectPageData(string tablename, string orderFields, string selectFields, int currPage, int pagesize, string where, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
        {
            string selectSql = @"Select * From (
                                    Select ROW_NUMBER() over(order by {1} ) as rid ,{2} From {0} {5}
                                )t Where t.rid>={3} And t.rid<{4}";
            int startIndex = (currPage - 1) * pagesize + 1;
            int endIndex = startIndex + pagesize;
            selectSql = string.Format(selectSql, tablename, orderFields, selectFields, startIndex, endIndex, where);
            return DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetDataset(selectSql).Tables[0];
        }


        /// <summary>
        /// 获取总记录数
        /// </summary>
        /// <param name="tablename">表名</param>
        /// <param name="where">查询条件</param>
        /// <param name="dbname">使用数据库服务名</param>
        public int SelectDataCount(string tablename, string where = "", DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
        {
            string selectSql = string.Format("Select count(*) From {0} {1}", tablename, where);
            string strCount = DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetScalar(selectSql).ToString();
            return int.Parse(strCount);
        }

        /// <summary>
        /// 根据条件查询数据集
        /// </summary>
        public DataTable SelectListData(string tablename, string where = null, string selectFields = "*", string orderby = null, int top = 0, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
        {
            if (!string.IsNullOrEmpty(where)) where = " Where " + where;
            string selectSql = string.Format(@"Select {3} {1} From {2} {0}", where, selectFields, tablename, top == 0 ? "" : "top " + top);
            if (!string.IsNullOrEmpty(orderby)) selectSql += " Order By " + orderby;
            DataSet ds = DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetDataset(selectSql);
            ds.Tables[0].TableName = tablename;
            return ds.Tables[0];
        }

        /// <summary>
        /// 根据条件查询首行首列
        /// </summary>
        public object SelectScalar(string tablename, string where = null, string selectFields = "*", string orderby = null, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
        {
            if (!string.IsNullOrEmpty(where)) where = " Where " + where;
            string selectSql = string.Format(@"Select {1} From {2} {0}", where, selectFields, tablename);
            if (!string.IsNullOrEmpty(orderby)) selectSql += " Order By " + orderby;
            return DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetScalar(selectSql);
        }

        /// <summary>
        /// 执行自定义SQL
        /// </summary>
        public void ExecuteSql(string execSql, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
        {
            DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).Execute(execSql);
        }

        /// <summary>
        /// 查询自定义SQL
        /// </summary>
        public DataSet SelectSql(string execSql, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
        {
            return DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetDataset(execSql);
        }
        /// <summary>
        /// 调用存储过程创建订单
        /// </summary>
        public int[] CreateOrder(string orderid, int uid, int businessId, int quantity, int redpaper, int q_end_time_second, string ip, string pay_type, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
        {
            int state = -1;
            int q_uid = -1;
            SqlParameter[] para ={  
                    new SqlParameter("@orderid",SqlDbType.VarChar),  
                    new SqlParameter("@uid",SqlDbType.Int),  
                    new SqlParameter("@businessId",SqlDbType.Int),  
                    new SqlParameter("@quantity",SqlDbType.Int),  
                    new SqlParameter("@redpaper",SqlDbType.Int),  
                    new SqlParameter("@q_end_time_second",SqlDbType.Int),  
                    new SqlParameter("@ip",SqlDbType.VarChar),
                    new SqlParameter("@pay_type",SqlDbType.VarChar),
                    new SqlParameter("@state",SqlDbType.Int),  
                    new SqlParameter("@dataid",SqlDbType.Int)  
            };
            para[0].Value = orderid; para[1].Value = uid; para[2].Value = businessId; para[3].Value = quantity; para[4].Value = redpaper;
            para[5].Value = q_end_time_second; para[6].Value = ip; para[7].Value = pay_type;
            para[8].Direction = ParameterDirection.Output; para[9].Direction = ParameterDirection.Output;  //设定参数的输出方向   
            using (SqlCommand command = CreateDbCommand(DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).ConnectionString, "CreateOrderYuan", para, CommandType.StoredProcedure))
            {
                command.Connection.Open();
                object result = command.ExecuteScalar();
                state = int.Parse(command.Parameters[8].Value.ToString());
                q_uid = int.Parse(command.Parameters[9].Value.ToString());
                command.Connection.Close();
            }
            return new[] { state, q_uid };
        }

        /// <summary>
        /// 调用存储过程创建直购订单
        /// </summary>
        public int CreateQuanOrder(string orderid, int uid, int businessId, int redpaper, string ip, string pay_type, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
        {
            int state = 0;
            SqlParameter[] para ={  
                    new SqlParameter("@orderid",SqlDbType.VarChar),  
                    new SqlParameter("@uid",SqlDbType.Int),  
                    new SqlParameter("@businessId",SqlDbType.Int),  
                    new SqlParameter("@redpaper",SqlDbType.Int),  
                    new SqlParameter("@ip",SqlDbType.VarChar),
                    new SqlParameter("@pay_type",SqlDbType.VarChar),
                    new SqlParameter("@state",SqlDbType.Int),  
            };
            para[0].Value = orderid; para[1].Value = uid; para[2].Value = businessId;
            para[3].Value = redpaper; para[4].Value = ip; para[5].Value = pay_type;
            para[6].Direction = ParameterDirection.Output; //设定参数的输出方向   
            using (SqlCommand command = CreateDbCommand(DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).ConnectionString, "CreateOrderQuan", para, CommandType.StoredProcedure))
            {
                command.Connection.Open();
                object result = command.ExecuteScalar();
                state = int.Parse(command.Parameters[6].Value.ToString());
                command.Connection.Close();
            }
            return state;
        }

        /// <summary>
        /// 调用存储过程创建(团购)订单
        /// </summary>
        public int[] CreateOrderTuan(string orderid, int uid, int businessId, int typeid, int q_end_time_second, string recordcode, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
        {
            int state = -1;
            int businesstype = -1;
            int tid = -1; int tuanlistId = -1;
            SqlParameter[] para ={  
                    new SqlParameter("@orderid",SqlDbType.VarChar),  
                    new SqlParameter("@uid",SqlDbType.Int),  
                    new SqlParameter("@businessId",SqlDbType.Int),  
                    new SqlParameter("@typeid",SqlDbType.Int),  
                    new SqlParameter("@q_end_time_second",SqlDbType.Int),  
                    new SqlParameter("@recordcode",SqlDbType.VarChar),  
                    new SqlParameter("@state",SqlDbType.Int),  
                    new SqlParameter("@businesstype",SqlDbType.Int),
                    new SqlParameter("@tid",SqlDbType.Int),
                    new SqlParameter("@tuanlistId",SqlDbType.Int)  
            };
            para[0].Value = orderid; para[1].Value = uid; para[2].Value = businessId; para[3].Value = typeid; para[4].Value = q_end_time_second; para[5].Value = recordcode;
            para[6].Direction = ParameterDirection.Output; para[7].Direction = ParameterDirection.Output; para[8].Direction = ParameterDirection.Output; para[9].Direction = ParameterDirection.Output;  //设定参数的输出方向   
            using (SqlCommand command = CreateDbCommand(DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).ConnectionString, "CreateOrderTuan", para, CommandType.StoredProcedure))
            {
                command.Connection.Open();
                object result = command.ExecuteScalar();
                state = int.Parse(command.Parameters[6].Value.ToString());
                businesstype = int.Parse(command.Parameters[7].Value.ToString());
                tid = int.Parse(command.Parameters[8].Value.ToString());
                tuanlistId = int.Parse(command.Parameters[9].Value.ToString());
                command.Connection.Close();
            }
            return new int[] { state, businesstype, tid, tuanlistId };
        }

        #region 创建一个SqlCommand对象
        /// <summary>
        /// 创建一个SqlCommand对象
        /// </summary>
        /// <param name="connStr">数据库链接字符串</param>   
        /// <param name="sql">要执行的查询语句</param>   
        /// <param name="parameters">执行SQL查询语句所需要的参数</param>
        /// <param name="commandType">执行的SQL语句的类型</param>
        private SqlCommand CreateDbCommand(string connStr, string sql, SqlParameter[] parameters, CommandType commandType)
        {
            SqlConnection connection = new SqlConnection(connStr);
            SqlCommand command = new SqlCommand(sql, connection);
            command.CommandType = commandType;
            command.Connection = connection;
            if (!(parameters == null || parameters.Length == 0))
            {
                foreach (SqlParameter parameter in parameters)
                {
                    command.Parameters.Add(parameter);
                }
            }
            return command;
        }
        #endregion

    }
}
