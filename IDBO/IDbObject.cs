using Maticsoft.CodeHelper;
using System;
using System.Collections.Generic;
using System.Data;
namespace Maticsoft.IDBO
{
	public interface IDbObject
	{
		string DbType
		{
			get;
		}
		string DbConnectStr
		{
			get;
			set;
		}
		int ExecuteSql(string DbName, string SQLString);
		DataSet Query(string DbName, string SQLString);
		List<string> GetDBList();
		List<string> GetTables(string DbName);
		DataTable GetVIEWs(string DbName);
		List<string> GetTableViews(string DbName);
		DataTable GetTabViews(string DbName);
		List<string> GetProcs(string DbName);
		List<TableInfo> GetTablesInfo(string DbName);
		DataTable GetTablesExProperty(string DbName);
		List<TableInfo> GetVIEWsInfo(string DbName);
		List<TableInfo> GetTabViewsInfo(string DbName);
		List<TableInfo> GetProcInfo(string DbName);
		string GetObjectInfo(string DbName, string objName);
		List<ColumnInfo> GetColumnList(string DbName, string TableName);
		List<ColumnInfo> GetColumnInfoList(string DbName, string TableName);
		DataTable GetKeyName(string DbName, string TableName);
		List<ColumnInfo> GetKeyList(string DbName, string TableName);
		List<ColumnInfo> GetFKeyList(string DbName, string TableName);
		DataTable GetTabData(string DbName, string TableName);
		bool RenameTable(string DbName, string OldName, string NewName);
		bool DeleteTable(string DbName, string TableName);
		string GetVersion();
	}
}
