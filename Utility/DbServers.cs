using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;

namespace Utility
{
    public class DbServers
    {
        /// <summary>
        /// 获取数据库服务器，一般为主数据库
        /// </summary>
        public static string GetCurrentDB(DbServerName dbServerName)
        {
            string dbservername=null;
            switch (dbServerName)
            { 
                case DbServerName.LatestDB:
                    break;
                case DbServerName.WriteDB:
                    break;
                case DbServerName.ReadHistoryDB:
                    dbservername = "ConnectString_Branch";
                    break;
            }
            return dbservername;
        }

        

        /// <summary>
        /// 获取数据库服务器
        /// </summary>
        public enum DbServerName
        {
            /// <summary>
            /// 获取最新数据时的数据库服务器，一般为主数据库
            /// </summary>
            LatestDB,
            /// <summary>
            /// 获取写入数据时的数据库服务器，一般为主数据库
            /// </summary>
            WriteDB,
            /// <summary>
            /// 获取读取历史数据时的数据库服务器，一般为从数据库
            /// </summary>
            ReadHistoryDB
        }

        /// <summary>
        /// 获取 接口交换数据时的密钥,可定期更改
        /// </summary>
        public static string APIKey
        {
            get { return ConfigurationManager.AppSettings["APIKey"]; }
        }

    }

}
