using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace DataAccess
{
    /// <summary>
    /// 数据库访问工厂类
    /// 创建信息 JHB: ON 2015-04-30
    /// </summary>
    public class DataAccessFactory
    {
        private static DataProvider provider = null;
        private static DataProvider provider2 = null;

        /// <summary>
        /// 得到默认的数据提供接口(配置文件)
        /// </summary>
        /// <returns></returns>
        public static DataProvider GetDataProvider()
        {
            if (provider == null)
            {
                string defaultDataProvider = ConfigurationManager.AppSettings["DefaultDataProvider"];
                string dbConnectString = ConfigurationManager.AppSettings["ConnectString"];
                if (defaultDataProvider == null) defaultDataProvider = "SqlServer";
                if (dbConnectString == null) throw new ArgumentNullException("ConnectString Is Not Exist");
                if (!Enum.IsDefined(typeof(DbProviderType), defaultDataProvider)) throw new ArgumentNullException("DefaultDataProvider Is Null");

                DbProviderType dbType = (DbProviderType)Enum.Parse(typeof(DbProviderType), defaultDataProvider);
                provider = GetDataProvider(dbType, dbConnectString, true);
            }
            return provider;
        }

        /// <summary>
        /// 得到数据提供接口(传参)
        /// </summary>
        /// <param name="dbType">数据库类型枚举</param>
        /// <param name="dbConnectString">数据库链接字符串</param>
        /// <param name="isForce">是否强制创建新实例</param>
        /// <returns>返回数据提供接口</returns>
        public static DataProvider GetDataProvider(DbProviderType dbType, string dbConnectString, bool isForce)
        {
            if (provider == null || isForce == false)
            {
                switch (dbType)
                {
                    case DbProviderType.SqlServer:
                        provider = new SqlProvider(dbConnectString);
                        break;
                    case DbProviderType.OleDb:
                        provider = new OleDbProvider(dbConnectString);
                        break;
                    case DbProviderType.SqlServerCe:
                        //provider = new SqlCeProvider(dbConnectString);
                        break;
                }

            }
            return provider;
        }

        /// <summary>
        /// 得到数据提供接口(传参)
        /// </summary>
        public static DataProvider GetDataProvider(string configName)
        {
            if (string.IsNullOrEmpty(configName))
            {
                return GetDataProvider();
            }
            if (provider2 == null)
            {
                string defaultDataProvider = ConfigurationManager.AppSettings["DefaultDataProvider"];
                string dbConnectString = ConfigurationManager.AppSettings[configName];
                if (defaultDataProvider == null) defaultDataProvider = "SqlServer";
                if (dbConnectString == null) throw new ArgumentNullException("ConnectString Is Not Exist");
                if (!Enum.IsDefined(typeof(DbProviderType), defaultDataProvider)) throw new ArgumentNullException("DefaultDataProvider Is Null");

                DbProviderType dbType = (DbProviderType)Enum.Parse(typeof(DbProviderType), defaultDataProvider);
                provider2 = GetDataProvider(dbType, dbConnectString, true);
            }
            return provider2;
        }

        /// <summary>
        /// 数据库类型枚举
        /// </summary>
        public enum DbProviderType : byte
        {
            SqlServer,
            SqlServerCe,
            OleDb,
            MySql,
            SQLite,
            Oracle,
            ODBC,
            Firebird,
            PostgreSql,
            DB2,
            Informix
        }

        /// <summary>
        /// 释放资源
        /// </summary>
        public static void Dispose()
        {
            provider = null;
            provider2 = null;
        }

    }
}
