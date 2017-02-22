using System;
using System.Data;
using System.Data.Common;
using System.Collections;

namespace DataAccess
{
    /// <summary>
    /// 数据库访问抽象类
    /// 创建信息 JHB: ON 2015-04-30
    /// </summary>
    public abstract class DataProvider
    {
        protected string _strConnString = string.Empty;

        public DataProvider(string connString)
        {
            _strConnString = connString;
        }

        /// <summary>
        /// 获取数据库链接串
        /// </summary>
        public virtual string ConnectionString
        {
            get
            {
                return _strConnString;
            }
        }

        //常用方法
        public abstract void Execute(String noneQuery);
        public abstract Object GetScalar(String selectSql);
        public abstract DataSet GetDataset(String selectSql);
        public abstract IDataReader GetReader(String selectSql);
        public abstract void CommitDataTable(DataTable dataTable, String selectText);

        //SQL传参
        public virtual void Execute(String noneQuery, DbParameter[] para) { }
        public virtual Object GetScalar(String selectSql, DbParameter[] para) { return null; }
        public virtual DataSet GetDataset(String selectSql, DbParameter[] para) { return null; }
        public virtual IDataReader GetReader(String selectSql, DbParameter[] para) { return null; }

        //数据库事务
        public virtual void ExecuteTran(ArrayList sqlStringList) { }
        public virtual void CommitDataTableTran(DataTable dataTable, String selectText) { }

        //执行存储过程
        public virtual void ExecuteSp(String spName) { }
        public virtual void ExecuteSp(String spName, DbParameter[] para) { }
        public virtual Object GetScalarBySp(String spName) { return null; }
        public virtual Object GetScalarBySp(String spName, DbParameter[] para) { return null; }
        public virtual DataSet GetDatasetBySp(String spName) { return null; }
        public virtual DataSet GetDatasetBySp(String spName, DbParameter[] para) { return null; }
        public virtual IDataReader GetReaderBySp(String spName) { return null; }
        public virtual IDataReader GetReaderBySp(String spName, DbParameter[] para) { return null; }
    }

}
