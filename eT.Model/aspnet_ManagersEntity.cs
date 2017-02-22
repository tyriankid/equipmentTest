using System;
using System.Data;
using System.Collections;

namespace eT.Model {
	/// <summary>
	/// -实体类
	/// </summary>
	[Serializable]
	public  class aspnet_ManagersEntity {

		#region 字段名
		public static string FieldUserId = "UserId";
		public static string FieldRoleId = "RoleId";
		public static string FieldUserName = "UserName";
		public static string FieldPassword = "Password";
		public static string FieldEmail = "Email";
		public static string FieldCreateDate = "CreateDate";
		public static string FieldClientUserId = "ClientUserId";
		public static string FieldAgentName = "AgentName";
		public static string FieldState = "State";
		public static string FieldPoi_id = "Poi_id";
		#endregion

		#region 属性
		private int  _userId;
		public int  UserId
		{
			get{ return _userId;}
			set{ _userId=value;}
		}
		private int  _roleId;
		public int  RoleId
		{
			get{ return _roleId;}
			set{ _roleId=value;}
		}
		private string  _userName;
		public string  UserName
		{
			get{ return _userName;}
			set{ _userName=value;}
		}
		private string  _password;
		public string  Password
		{
			get{ return _password;}
			set{ _password=value;}
		}
		private string  _email;
		public string  Email
		{
			get{ return _email;}
			set{ _email=value;}
		}
		private DateTime  _createDate;
		public DateTime  CreateDate
		{
			get{ return _createDate;}
			set{ _createDate=value;}
		}
		private int  _clientUserId;
		public int  ClientUserId
		{
			get{ return _clientUserId;}
			set{ _clientUserId=value;}
		}
		private string  _agentName;
		public string  AgentName
		{
			get{ return _agentName;}
			set{ _agentName=value;}
		}
		private int  _state;
		public int  State
		{
			get{ return _state;}
			set{ _state=value;}
		}
		private string  _poi_id;
		public string  Poi_id
		{
			get{ return _poi_id;}
			set{ _poi_id=value;}
		}
		#endregion

		#region 构造函数
		public aspnet_ManagersEntity(){}

		public aspnet_ManagersEntity(DataRow dr)
		{
			if (dr[FieldUserId] != DBNull.Value)
			{
			_userId = (int )dr[FieldUserId];
			}
			if (dr[FieldRoleId] != DBNull.Value)
			{
			_roleId = (int )dr[FieldRoleId];
			}
			if (dr[FieldUserName] != DBNull.Value)
			{
			_userName = (string )dr[FieldUserName];
			}
			if (dr[FieldPassword] != DBNull.Value)
			{
			_password = (string )dr[FieldPassword];
			}
			if (dr[FieldEmail] != DBNull.Value)
			{
			_email = (string )dr[FieldEmail];
			}
			if (dr[FieldCreateDate] != DBNull.Value)
			{
			_createDate = (DateTime )dr[FieldCreateDate];
			}
			if (dr[FieldClientUserId] != DBNull.Value)
			{
			_clientUserId = (int )dr[FieldClientUserId];
			}
			if (dr[FieldAgentName] != DBNull.Value)
			{
			_agentName = (string )dr[FieldAgentName];
			}
			if (dr[FieldState] != DBNull.Value)
			{
			_state = (int )dr[FieldState];
			}
			if (dr[FieldPoi_id] != DBNull.Value)
			{
			_poi_id = (string )dr[FieldPoi_id];
			}
		}
		#endregion

	}
}
