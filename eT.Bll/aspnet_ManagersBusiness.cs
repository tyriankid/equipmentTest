using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using eT.Common;
using eT.Dal;
using eT.Model;
using Utility;

namespace eT.Bll
{
	/// <summary>
	/// -业务操作类
	/// </summary>
	public class aspnet_ManagersBusiness
	{
		/// <summary>
		/// 根据主键加载数据集
		/// </summary>
		public static DataTable LoadData(int ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			if (Globals.GetMasterSettings().OpenCacheServer)
			{
				return null;    //后续扩冲： 开启缓存服务器后，从缓存服务器拿取数据
			}
			else
			{
				return aspnet_ManagersManager.LoadData(ID, currDbName);
			}
		}

		/// <summary>
		/// 根据主键加载实体
		/// </summary>
		public static aspnet_ManagersEntity LoadEntity(int ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
				return aspnet_ManagersManager.LoadEntity(ID, currDbName);
		}

		/// <summary>
		/// 根据条件查询数据集
		/// </summary>
		public static DataTable GetListData(string where = null, string selectFields ="*", string orderby = null, int top = 0, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			return aspnet_ManagersManager.SelectListData(where,selectFields,orderby,top, currDbName);
		}

		/// <summary>
		/// 根据条件查询首行首列
		/// </summary>
		public static object GetScalar(string where = null, string selectFields ="*", string orderby = null, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			return aspnet_ManagersManager.SelectScalar(where,selectFields,orderby, currDbName);
		}

		/// <summary>
		/// 根据条件查询数据实体
		/// </summary>
		public static IList<aspnet_ManagersEntity> GetListEntity(string where = null, string selectFields ="*", string orderby = null, int top = 0, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
				return aspnet_ManagersManager.SelectListEntity(where,selectFields,orderby,top, currDbName);
		}

		/// <summary>
		/// 根据主键删除
		/// </summary>
		public static void Del(int ID, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			aspnet_ManagersManager.Del(ID, currDbName);
		}

		/// <summary>
		/// 根据条件删除
		/// </summary>
		public static void DelListData(string where = null, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			aspnet_ManagersManager.DelListData(where, currDbName);
		}

		/// <summary>
		/// 保存
		/// </summary>
		public static bool SaveEntity(aspnet_ManagersEntity entity, bool isAdd, DbServers.DbServerName currDbName = DbServers.DbServerName.LatestDB)
		{
			return aspnet_ManagersManager.SaveEntity(entity, isAdd, currDbName);
		}

	}
}
