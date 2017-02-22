
using eT.Dal;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Utility;

namespace eT.Business
{
    /// <summary>
    /// 自定义数据管理类
    /// </summary>
    public partial class CustomsBusiness
    {

        /// <summary>
        /// 批量提交数据表
        /// </summary>
        public static bool CommitDataTable(DataTable dtData, string selectSql, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
        {
            return new CustomManager().CommitDataTable(dtData, selectSql, currDbName);
        }

        /// <summary>
        /// 批量提交数据集
        /// </summary>
        public static bool CommitDataSet(DataSet dsData, string[] arraySelectSql, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
        {
            return new CustomManager().CommitDataSet(dsData, arraySelectSql, currDbName);
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
        public static DataTable GetPageData(string tablename, string orderFields, string selectFields, int currPage, int pagesize, string where, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
        {
            return new CustomManager().SelectPageData(tablename, orderFields, selectFields, currPage, pagesize, where, currDbName);
        }

        /// <summary>
        /// 获取总记录数
        /// </summary>
        /// <param name="tablename">表名</param>
        /// <param name="where">查询条件</param>
        /// <param name="dbname">使用数据库服务名</param>
        public static int GetDataCount(string tablename, string where = "", DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
        {
            return new CustomManager().SelectDataCount(tablename, where, currDbName);
        }

        /// <summary>
        /// 根据条件查询数据集
        /// </summary>
        public static DataTable GetListData(string tablename, string where = null, string selectFields = "*", string orderby = null, int top = 0, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
        {
            return new CustomManager().SelectListData(tablename, where, selectFields, orderby, top, currDbName);
        }

        /// <summary>
        /// 根据条件查询首行首列
        /// </summary>
        public static object GetScalar(string tablename, string where = null, string selectFields = "*", string orderby = null, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
        {
            return new CustomManager().SelectScalar(tablename, where, selectFields, orderby, currDbName);
        }

        /// <summary>
        /// 执行自定义SQL
        /// </summary>
        public static void ExecuteSql(string execSql, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
        {
            new CustomManager().ExecuteSql(execSql, currDbName);
        }

        /// <summary>
        /// 查询自定义SQL
        /// </summary>
        public static DataSet GetSql(string execSql, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
        {
            return new CustomManager().SelectSql(execSql, currDbName);
        }
        /// <summary>
        /// 调用存储过程创建订单
        /// </summary>
        public static int[] CreateOrder(string orderid, int uid, int businessId, int quantity, int redpaper, int q_end_time_second, string ip, string pay_type, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
        {
            return new CustomManager().CreateOrder(orderid, uid, businessId, quantity, redpaper, q_end_time_second, ip, pay_type, currDbName);
        }

        /// <summary>
        /// 调用存储过程创建直购订单
        /// </summary>
        public static int CreateQuanOrder(string orderid, int uid, int businessId, int redpaper, string ip, string pay_type, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
        {
            return new CustomManager().CreateQuanOrder(orderid, uid, businessId, redpaper, ip, pay_type, currDbName);
        }

        /// <summary>
        /// 调用存储过程创建团购订单
        /// </summary>
        public static int[] CreateOrderTuan(string orderid, int uid, int businessId, int typeid, int q_end_time_second, string recordcode, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
        {
            return new CustomManager().CreateOrderTuan(orderid, uid, businessId, typeid, q_end_time_second, recordcode, currDbName);
        }
    }
}
