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
	/// -数据库操作类
	/// </summary>
	public class aspnet_ManagersManager
	{

		/// <summary>
		/// 根据主键查询数据集
		/// </summary>
		public static DataTable LoadData(int ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			string selectSql = string.Format(@"Select * From aspnet_Managers Where UserId={0}", ID);
			DataSet ds = DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetDataset(selectSql);
			ds.Tables[0].TableName = "aspnet_Managers";
			ds.Tables[0].PrimaryKey = new DataColumn[] { ds.Tables[0].Columns["UserId"] };
			return ds.Tables[0];
		}

		/// <summary>
		/// 根据主键查询数据实体
		/// </summary>
		public static aspnet_ManagersEntity LoadEntity(int ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			string selectSql = string.Format(@"Select * From aspnet_Managers Where UserId={0}", ID);
			using (IDataReader reader = DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetReader(selectSql))
			{
				return ReaderConvert.ReaderToModel<aspnet_ManagersEntity>(reader);
			}
		}

		/// <summary>
		/// 根据条件查询数据集
		/// </summary>
		public static DataTable SelectListData(string where = null, string selectFields ="*", string orderby = null, int top = 0, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			if (!string.IsNullOrEmpty(where)) where = " Where " + where;
			string selectSql = string.Format(@"Select {2} {1} From aspnet_Managers {0}", where, selectFields, top == 0 ? "" : "top " + top);
			if (!string.IsNullOrEmpty(orderby)) selectSql += " Order By " + orderby;
			DataSet ds = DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetDataset(selectSql);
			ds.Tables[0].TableName = "aspnet_Managers";
			ds.Tables[0].PrimaryKey = new DataColumn[] { ds.Tables[0].Columns["UserId"] };
			return ds.Tables[0];
		}

		/// <summary>
		/// 根据条件查询首行首列
		/// </summary>
		public static object SelectScalar(string where = null, string selectFields ="*", string orderby = null, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			if (!string.IsNullOrEmpty(where)) where = " Where " + where;
			string selectSql = string.Format(@"Select {1} From aspnet_Managers {0}", where, selectFields);
			if (!string.IsNullOrEmpty(orderby)) selectSql += " Order By " + orderby;
			return DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetScalar(selectSql);
		}

		/// <summary>
		/// 根据条件查询数据实体
		/// </summary>
		public static IList<aspnet_ManagersEntity> SelectListEntity(string where = null, string selectFields ="*", string orderby = null, int top = 0, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			if (!string.IsNullOrEmpty(where)) where = " Where " + where;
			string selectSql = string.Format(@"Select {2} {1} From aspnet_Managers {0}", where, selectFields, top == 0 ? "" : "top " + top);
			if (!string.IsNullOrEmpty(orderby)) selectSql += " Order By " + orderby;
			using (IDataReader reader = DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).GetReader(selectSql))
			{
				return ReaderConvert.ReaderToList<aspnet_ManagersEntity>(reader);
			}
		}

		/// <summary>
		/// 根据主键删除
		/// </summary>
		public static void Del(int ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			string deleteSql = string.Format(@"Delete From aspnet_Managers Where UserId={0}", ID);
			DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).Execute(deleteSql);
		}

		/// <summary>
		/// 根据条件删除
		/// </summary>
		public static void DelListData(string where = null, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			if (!string.IsNullOrEmpty(where)) where = " Where " + where;
			string deleteSql = string.Format(@"Delete From aspnet_Managers {0}", where);
			DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).Execute(deleteSql);
		}

		/// <summary>
		/// 保存数据
		/// </summary>
		public static bool SaveEntity(aspnet_ManagersEntity entity, bool isAdd, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			try
			{
				string execSql = (isAdd) ?
				"Insert Into aspnet_Managers(RoleId,ClientUserId,state,CreateDate,UserName,Password,Email,AgentName,poi_id)values(@RoleId,@ClientUserId,@state,@CreateDate,@UserName,@Password,@Email,@AgentName,@poi_id)" :
				"Update aspnet_Managers Set RoleId=@RoleId,ClientUserId=@ClientUserId,state=@state,CreateDate=@CreateDate,UserName=@UserName,Password=@Password,Email=@Email,AgentName=@AgentName,poi_id=@poi_id Where UserId=@UserId";
				SqlParameter[] para = new SqlParameter[]
				{
					new SqlParameter("@UserId",entity.UserId),
					new SqlParameter("@RoleId",entity.RoleId),
					new SqlParameter("@ClientUserId",entity.ClientUserId),
					new SqlParameter("@state",entity.State),
					(entity.CreateDate==null || entity.CreateDate==DateTime.MinValue)?new SqlParameter("@CreateDate",DBNull.Value):new SqlParameter("@CreateDate",entity.CreateDate),
					(entity.UserName==null)?new SqlParameter("@UserName",DBNull.Value):new SqlParameter("@UserName",entity.UserName),
					(entity.Password==null)?new SqlParameter("@Password",DBNull.Value):new SqlParameter("@Password",entity.Password),
					(entity.Email==null)?new SqlParameter("@Email",DBNull.Value):new SqlParameter("@Email",entity.Email),
					(entity.AgentName==null)?new SqlParameter("@AgentName",DBNull.Value):new SqlParameter("@AgentName",entity.AgentName),
					(entity.Poi_id==null)?new SqlParameter("@poi_id",DBNull.Value):new SqlParameter("@poi_id",entity.Poi_id),
				};
				DataAccessFactory.GetDataProvider(DbServers.GetCurrentDB(currDbName)).Execute(execSql, para);
				return true;
			}
			catch
			{
				return false;
			}
		}

	}
}
