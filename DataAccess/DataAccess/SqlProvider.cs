using System;
using System.Data;
using System.Data.SqlClient;
using YH.DataBase;
using System.Collections;
using System.Data.Common;

namespace DataAccess
{
    /// <summary>
    /// 数据访问实现类(基于SqlServer) 
    /// 创建信息 JHB: ON 2015-04-30
    /// </summary>
    public class SqlProvider : DataProvider
    {
        public SqlProvider(string connString)
            : base(connString)
        { }

        #region 必须实现的常用方法(抽象方法)

        /// <summary>
        /// 执行一个SQL语句
        /// </summary>
        /// <param name="noneQuery">要执行的语句</param>
        public override void Execute(String noneQuery)
        {
            if (noneQuery == null) throw new ArgumentNullException("noneQuery");
            SqlHelper.Execute(this.ConnectionString, noneQuery, null, CommandType.Text);
        }

        /// <summary>
        /// 查询首行首列
        /// </summary>
        /// <param name="selectSql">要执行的查询语句</param>
        /// <returns>返回查询结果的首行首列</returns>
        public override Object GetScalar(String selectSql)
        {
            if (selectSql == null) throw new ArgumentNullException("selectSql");
            return SqlHelper.ExecuteScalar(this.ConnectionString, selectSql, null, CommandType.Text);
        }

        /// <summary>
        /// 查询数据集
        /// </summary>
        /// <param name="selectSql">要执行的查询语句</param>
        /// <returns>返回查询结果数据集</returns>
        public override DataSet GetDataset(String selectSql)
        {
            if (selectSql == null) throw new ArgumentNullException("selectSql");
            return SqlHelper.ExecuteDataSet(this.ConnectionString, selectSql, null, CommandType.Text);
        }

        /// <summary>
        /// 查询关联的DataReader实例
        /// </summary>
        /// <param name="selectSql">要执行的查询语句</param>
        /// <returns>返回关联的DataReader实例</returns>
        public override IDataReader GetReader(String selectSql)
        {
            if (selectSql == null) throw new ArgumentNullException("selectSql");
            return SqlHelper.ExecuteReader(this.ConnectionString, selectSql, null, CommandType.Text);
        }

        /// <summary>
        /// 更新数据表
        /// </summary>
        /// <param name="dataTable">数据表</param>
        /// <param name="selectText">创建的数据表的查询语句</param>
        public override void CommitDataTable(DataTable dataTable, String selectText)
        {
            if (dataTable == null) throw new ArgumentNullException("dataTable");
            if (selectText == null) throw new ArgumentNullException("selectText");
            SqlHelper.UpdateDatatable(this.ConnectionString, selectText, dataTable.GetChanges());
        }

        #endregion 必须实现的常用方法(抽象方法)

        #region SQL传参

        /// <summary>
        /// 执行一个带参数的SQL语句
        /// </summary>
        /// <param name="noneQuery">要执行的语句</param>
        /// <param name="para">参数</param>
        public override void Execute(String noneQuery, DbParameter[] para)
        {
            if (noneQuery == null) throw new ArgumentNullException("noneQuery");
            if (para == null) throw new ArgumentNullException("Parameter");
            SqlParameter[] SqlPara = new SqlParameter[para.Length];
            for (int i = 0; i < para.Length; i++)
            {
                SqlPara[i] = (SqlParameter)para[i];
            }
            SqlHelper.Execute(this.ConnectionString, noneQuery, SqlPara, CommandType.Text);
        }

        /// <summary>
        /// 查询首行首列
        /// </summary>
        /// <param name="selectSql">要执行的查询语句</param>
        /// <param name="para">参数</param>
        /// <returns>返回查询结果的首行首列</returns>
        public override Object GetScalar(String selectSql, DbParameter[] para)
        {
            if (selectSql == null) throw new ArgumentNullException("selectSql");
            if (para == null) throw new ArgumentNullException("Parameter");
            SqlParameter[] SqlPara = new SqlParameter[para.Length];
            for (int i = 0; i < para.Length; i++)
            {
                SqlPara[i] = (SqlParameter)para[i];
            }
            return SqlHelper.ExecuteScalar(this.ConnectionString, selectSql, SqlPara, CommandType.Text);
        }

        /// <summary>
        /// 查询数据集
        /// </summary>
        /// <param name="selectSql">要执行的查询语句</param>
        /// <param name="para">参数</param>
        /// <returns>返回查询结果数据集</returns>
        public override DataSet GetDataset(String selectSql, DbParameter[] para)
        {
            if (selectSql == null) throw new ArgumentNullException("selectSql");
            if (para == null) throw new ArgumentNullException("Parameter");
            SqlParameter[] SqlPara = new SqlParameter[para.Length];
            for (int i = 0; i < para.Length; i++)
            {
                SqlPara[i] = (SqlParameter)para[i];
            }
            return SqlHelper.ExecuteDataSet(this.ConnectionString, selectSql, SqlPara, CommandType.Text);
        }

        /// <summary>
        /// 查询关联的DataReader实例
        /// </summary>
        /// <param name="selectSql">要执行的查询语句</param>
        /// <param name="para">参数</param>
        /// <returns>返回关联的DataReader实例</returns>
        public override IDataReader GetReader(String selectSql, DbParameter[] para)
        {
            if (selectSql == null) throw new ArgumentNullException("selectSql");
            if (para == null) throw new ArgumentNullException("Parameter");
            SqlParameter[] SqlPara = new SqlParameter[para.Length];
            for (int i = 0; i < para.Length; i++)
            {
                SqlPara[i] = (SqlParameter)para[i];
            }
            return SqlHelper.ExecuteReader(this.ConnectionString, selectSql, SqlPara, CommandType.Text);
        }

        #endregion SQL传参

        #region 数据库事务处理

        /// <summary>
        /// 用事务执行一组SQL语句
        /// </summary>
        public override void ExecuteTran(ArrayList sqlStringList)
        {
            if (sqlStringList == null) throw new ArgumentNullException("sqlStringList");
            SqlHelper.ExecuteTran(this.ConnectionString, sqlStringList);
        }

        /// <summary>
        /// 更新数据表(事务提交方式)
        /// </summary>
        /// <param name="dataTable">数据表</param>
        /// <param name="selectText">创建的数据表的查询语句</param>
        public override void CommitDataTableTran(DataTable dataTable, String selectText)
        {
            if (dataTable == null) throw new ArgumentNullException("dataTable");
            if (selectText == null) throw new ArgumentNullException("selectText");
            SqlHelper.UpdateDataTableTran(this.ConnectionString, selectText, dataTable);
        }

        #endregion 数据库事务处理

        #region 执行存储过程

        /// <summary>
        /// 执行一个存储过程
        /// </summary>
        public override void ExecuteSp(String spName)
        {
            if (spName == null) throw new ArgumentNullException("spName");
            SqlHelper.Execute(this.ConnectionString, spName, null, CommandType.StoredProcedure);
        }

        /// <summary>
        /// 执行一个存储过程
        /// </summary>
        public override void ExecuteSp(String spName, DbParameter[] para)
        {
            if (spName == null) throw new ArgumentNullException("spName");
            if (para == null) throw new ArgumentNullException("Parameter");
            SqlParameter[] SqlPara = new SqlParameter[para.Length];
            for (int i = 0; i < para.Length; i++)
            {
                SqlPara[i] = (SqlParameter)para[i];
            }
            SqlHelper.Execute(this.ConnectionString, spName, SqlPara, CommandType.StoredProcedure);
        }

        /// <summary>
        /// 查询首行首列
        /// </summary>
        public override Object GetScalarBySp(String spName)
        {
            if (spName == null) throw new ArgumentNullException("spName");
            return SqlHelper.ExecuteScalar(this.ConnectionString, spName, null, CommandType.StoredProcedure);
        }

        /// <summary>
        /// 查询首行首列
        /// </summary>
        public override Object GetScalarBySp(String spName, DbParameter[] para)
        {
            if (spName == null) throw new ArgumentNullException("spName");
            if (para == null) throw new ArgumentNullException("Parameter");
            SqlParameter[] SqlPara = new SqlParameter[para.Length];
            for (int i = 0; i < para.Length; i++)
            {
                SqlPara[i] = (SqlParameter)para[i];
            }
            return SqlHelper.ExecuteScalar(this.ConnectionString, spName, SqlPara, CommandType.StoredProcedure);
        }

        /// <summary>
        /// 查询数据集
        /// </summary>
        public override DataSet GetDatasetBySp(String spName)
        {
            if (spName == null) throw new ArgumentNullException("spName");
            return SqlHelper.ExecuteDataSet(this.ConnectionString, spName, null, CommandType.StoredProcedure);
        }

        /// <summary>
        /// 查询数据集
        /// </summary>
        public override DataSet GetDatasetBySp(String spName, DbParameter[] para)
        {
            if (spName == null) throw new ArgumentNullException("spName");
            if (para == null) throw new ArgumentNullException("Parameter");
            SqlParameter[] SqlPara = new SqlParameter[para.Length];
            for (int i = 0; i < para.Length; i++)
            {
                SqlPara[i] = (SqlParameter)para[i];
            }
            return SqlHelper.ExecuteDataSet(this.ConnectionString, spName, SqlPara, CommandType.StoredProcedure);
        }

        /// <summary>
        /// 查询关联的DataReader实例
        /// </summary>
        public override IDataReader GetReaderBySp(String spName)
        {
            if (spName == null) throw new ArgumentNullException("spName");
            return SqlHelper.ExecuteReader(this.ConnectionString, spName, null, CommandType.StoredProcedure);
        }

        /// <summary>
        /// 查询关联的DataReader实例
        /// </summary>
        public override IDataReader GetReaderBySp(String spName, DbParameter[] para)
        {
            if (spName == null) throw new ArgumentNullException("spName");
            if (para == null) throw new ArgumentNullException("Parameter");
            SqlParameter[] SqlPara = new SqlParameter[para.Length];
            for (int i = 0; i < para.Length; i++)
            {
                SqlPara[i] = (SqlParameter)para[i];
            }
            return SqlHelper.ExecuteReader(this.ConnectionString, spName, SqlPara, CommandType.StoredProcedure);
        }

        #endregion 执行存储过程


    }
}
