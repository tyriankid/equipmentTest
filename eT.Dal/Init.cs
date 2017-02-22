using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using eT.Model;
using DataAccess;
using Utility;

namespace eT.Dal
{
    /// <summary>
    /// 初始化_数据管理类
    /// </summary>
    public class Init
    {

        /// <summary>
        /// 获取当前数据库服务连接字符串
        /// </summary>
        public static string GetConnectionString(DbServers.DbServerName currDbName = DbServers.DbServerName.ReadHistoryDB)
        {
            return DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).ConnectionString;

        }
    }
}
