using Maticsoft.CodeHelper;
using Maticsoft.IDBO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Text;
namespace Maticsoft.DbObjects.SQLite
{
	public class DbObject : IDbObject
	{
		private string _dbconnectStr;
		private SQLiteConnection connect = new SQLiteConnection();
		public string DbType
		{
			get
			{
				return "SQLite";
			}
		}
		public string DbConnectStr
		{
			get
			{
				return this._dbconnectStr;
			}
			set
			{
				this._dbconnectStr = value;
			}
		}
		public DbObject()
		{
		}
		public DbObject(string DbConnectStr)
		{
			this._dbconnectStr = DbConnectStr;
			this.connect.ConnectionString = DbConnectStr;
		}
		public DbObject(bool SSPI, string Ip, string User, string Pass)
		{
			this.connect = new SQLiteConnection();
			if (SSPI)
			{
				this._dbconnectStr = string.Format("Data Source={0}; Password={1}", Ip, Pass);
			}
			else
			{
				this._dbconnectStr = string.Format("Data Source={0};Password={1}", Ip, Pass);
			}
			this.connect.ConnectionString = this._dbconnectStr;
		}
		private SQLiteCommand OpenDB()
		{
			SQLiteCommand result;
			try
			{
				if (this.connect.ConnectionString == "")
				{
					this.connect.ConnectionString = this._dbconnectStr;
				}
				if (this.connect.ConnectionString != this._dbconnectStr)
				{
					this.connect.Close();
					this.connect.ConnectionString = this._dbconnectStr;
				}
				SQLiteCommand sQLiteCommand = new SQLiteCommand();
				sQLiteCommand.Connection = this.connect;
				if (this.connect.State == ConnectionState.Closed)
				{
					this.connect.Open();
				}
				result = sQLiteCommand;
			}
			catch (Exception ex)
			{
				string arg_91_0 = ex.Message;
				result = null;
			}
			return result;
		}
		public int ExecuteSql(string DbName, string SQLString)
		{
			SQLiteCommand sQLiteCommand = this.OpenDB();
			sQLiteCommand.CommandText = SQLString;
			return sQLiteCommand.ExecuteNonQuery();
		}
		public DataSet Query(string DbName, string SQLString)
		{
			DataSet dataSet = new DataSet();
			try
			{
				this.OpenDB();
				SQLiteDataAdapter sQLiteDataAdapter = new SQLiteDataAdapter(SQLString, this.connect);
				sQLiteDataAdapter.Fill(dataSet, "ds");
			}
			catch (SQLiteException ex)
			{
				throw new Exception(ex.Message);
			}
			return dataSet;
		}
		public SQLiteDataReader ExecuteReader(string DbName, string strSQL)
		{
			SQLiteDataReader result;
			try
			{
				this.OpenDB();
				SQLiteCommand sQLiteCommand = new SQLiteCommand(strSQL, this.connect);
				SQLiteDataReader sQLiteDataReader = sQLiteCommand.ExecuteReader(CommandBehavior.CloseConnection);
				result = sQLiteDataReader;
			}
			catch (SQLiteException ex)
			{
				throw ex;
			}
			return result;
		}
		public object GetSingle(string DbName, string SQLString)
		{
			object result;
			try
			{
				SQLiteCommand sQLiteCommand = this.OpenDB();
				sQLiteCommand.CommandText = SQLString;
				object obj = sQLiteCommand.ExecuteScalar();
				if (object.Equals(obj, null) || object.Equals(obj, DBNull.Value))
				{
					result = null;
				}
				else
				{
					result = obj;
				}
			}
			catch
			{
				result = null;
			}
			return result;
		}
		private int CompareStrByOrder(string x, string y)
		{
			if (x == "")
			{
				if (y == "")
				{
					return 0;
				}
				return -1;
			}
			else
			{
				if (y == "")
				{
					return 1;
				}
				return x.CompareTo(y);
			}
		}
		private int CompareDinosByintOrder(ColumnInfo x, ColumnInfo y)
		{
			if (x == null)
			{
				if (y == null)
				{
					return 0;
				}
				return -1;
			}
			else
			{
				if (y == null)
				{
					return 1;
				}
				int num = 0;
				int num2 = 0;
				try
				{
					num = Convert.ToInt32(x.ColumnOrder);
				}
				catch
				{
					int result = -1;
					return result;
				}
				try
				{
					num2 = Convert.ToInt32(y.ColumnOrder);
				}
				catch
				{
					int result = 1;
					return result;
				}
				if (num < num2)
				{
					return -1;
				}
				if (x == y)
				{
					return 0;
				}
				return 1;
			}
		}
		private int CompareDinosByOrder(ColumnInfo x, ColumnInfo y)
		{
			if (x == null)
			{
				if (y == null)
				{
					return 0;
				}
				return -1;
			}
			else
			{
				if (y == null)
				{
					return 1;
				}
				return x.ColumnOrder.CompareTo(y.ColumnOrder);
			}
		}
		public List<string> GetDBList()
		{
			return null;
		}
		private DataTable Tab2Tab(DataTable sTable)
		{
			DataTable dataTable = new DataTable();
			dataTable.Columns.Add("name");
			dataTable.Columns.Add("cuser");
			dataTable.Columns.Add("type");
			dataTable.Columns.Add("dates");
			foreach (DataRow dataRow in sTable.Rows)
			{
				DataRow dataRow2 = dataTable.NewRow();
				dataRow2["name"] = dataRow[2].ToString();
				dataRow2["cuser"] = "dbo";
				dataRow2["type"] = dataRow[3].ToString();
				dataRow2["dates"] = dataRow[6].ToString();
				dataTable.Rows.Add(dataRow2);
			}
			return dataTable;
		}
		public List<string> GetTables(string DbName)
		{
			string strSQL = "select name from sqlite_master where type='table' AND name NOT LIKE 'sqlite_%' order by name";
			List<string> list = new List<string>();
			SQLiteDataReader sQLiteDataReader = this.ExecuteReader(DbName, strSQL);
			while (sQLiteDataReader.Read())
			{
				list.Add(sQLiteDataReader.GetString(0));
			}
			sQLiteDataReader.Close();
			return list;
		}
		public DataTable GetVIEWs(string DbName)
		{
			string sQLString = "select name from sqlite_master WHERE type='view' AND name NOT LIKE 'sqlite_%' order by name";
			DataTable dataTable = this.Query(DbName, sQLString).Tables[0];
			dataTable.Columns[0].ColumnName = "name";
			return dataTable;
		}
		public List<string> GetTableViews(string DbName)
		{
			string strSQL = "select name from sqlite_master WHERE type IN ('table','view') AND name NOT LIKE 'sqlite_%' order by name";
			List<string> list = new List<string>();
			SQLiteDataReader sQLiteDataReader = this.ExecuteReader(DbName, strSQL);
			while (sQLiteDataReader.Read())
			{
				list.Add(sQLiteDataReader.GetString(0));
			}
			sQLiteDataReader.Close();
			return list;
		}
		public DataTable GetTabViews(string DbName)
		{
			string sQLString = "select name from sqlite_master WHERE type IN ('table','view') AND name NOT LIKE 'sqlite_%' order by name";
			DataTable dataTable = this.Query(DbName, sQLString).Tables[0];
			dataTable.Columns[0].ColumnName = "name";
			return dataTable;
		}
		public List<string> GetProcs(string DbName)
		{
			return new List<string>();
		}
		public List<TableInfo> GetTablesInfo(string DbName)
		{
			List<TableInfo> list = new List<TableInfo>();
			string strSQL = "select * from sqlite_master where type='table' AND name NOT LIKE 'sqlite_%' order by name";
			SQLiteDataReader sQLiteDataReader = this.ExecuteReader(DbName, strSQL);
			while (sQLiteDataReader.Read())
			{
				list.Add(new TableInfo
				{
					TabName = sQLiteDataReader["Name"].ToString(),
					TabType = "U",
					TabUser = ""
				});
			}
			sQLiteDataReader.Close();
			return list;
		}
		public DataTable GetTablesExProperty(string DbName)
		{
			return null;
		}
		public List<TableInfo> GetVIEWsInfo(string DbName)
		{
			List<TableInfo> list = new List<TableInfo>();
			string strSQL = "select * from sqlite_master where type='view' AND name NOT LIKE 'sqlite_%' order by name";
			SQLiteDataReader sQLiteDataReader = this.ExecuteReader(DbName, strSQL);
			while (sQLiteDataReader.Read())
			{
				list.Add(new TableInfo
				{
					TabName = sQLiteDataReader["Name"].ToString(),
					TabType = "U",
					TabUser = ""
				});
			}
			sQLiteDataReader.Close();
			return list;
		}
		public List<TableInfo> GetTabViewsInfo(string DbName)
		{
			List<TableInfo> list = new List<TableInfo>();
			string strSQL = "select * from sqlite_master WHERE type IN ('table','view') AND name NOT LIKE 'sqlite_%' order by name";
			SQLiteDataReader sQLiteDataReader = this.ExecuteReader(DbName, strSQL);
			while (sQLiteDataReader.Read())
			{
				list.Add(new TableInfo
				{
					TabName = sQLiteDataReader["Name"].ToString(),
					TabType = "U",
					TabUser = ""
				});
			}
			sQLiteDataReader.Close();
			return list;
		}
		public List<TableInfo> GetProcInfo(string DbName)
		{
			return null;
		}
		public string GetObjectInfo(string DbName, string objName)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select sql ");
			stringBuilder.Append("from sqlite_master   ");
			stringBuilder.Append("where name= '" + objName + "'");
			object single = this.GetSingle(DbName, stringBuilder.ToString());
			if (single != null)
			{
				return single.ToString();
			}
			return "";
		}
		private DataTable Tab2Colum(DataTable sTable)
		{
			DataTable dataTable = new DataTable();
			dataTable.Columns.Add("colorder");
			dataTable.Columns.Add("ColumnName");
			dataTable.Columns.Add("TypeName");
			dataTable.Columns.Add("Length");
			dataTable.Columns.Add("Preci");
			dataTable.Columns.Add("Scale");
			dataTable.Columns.Add("IsIdentity");
			dataTable.Columns.Add("isPK");
			dataTable.Columns.Add("cisNull");
			dataTable.Columns.Add("defaultVal");
			dataTable.Columns.Add("deText");
			int num = 0;
			DataRow[] array = sTable.Select("", "ORDINAL_POSITION asc");
			for (int i = 0; i < array.Length; i++)
			{
				DataRow dataRow = array[i];
				DataRow dataRow2 = dataTable.NewRow();
				dataRow2["colorder"] = dataRow[6].ToString();
				dataRow2["ColumnName"] = dataRow[3].ToString();
				dataRow2["TypeName"] = dataRow[11].ToString();
				dataRow2["Length"] = dataRow[13].ToString();
				dataRow2["Preci"] = dataRow[15].ToString();
				dataRow2["Scale"] = dataRow[16].ToString();
				dataRow2["IsIdentity"] = "";
				dataRow2["isPK"] = "";
				if (dataRow[10].ToString().ToLower() == "true")
				{
					dataRow2["cisNull"] = "";
				}
				else
				{
					dataRow2["cisNull"] = "√";
				}
				dataRow2["defaultVal"] = dataRow[8].ToString();
				dataRow2["deText"] = "";
				dataTable.Rows.Add(dataRow2);
				num++;
			}
			return dataTable;
		}
		public List<ColumnInfo> GetColumnList(string DbName, string TableName)
		{
			return this.GetColumnInfoList(DbName, TableName);
		}
		public List<ColumnInfo> GetColumnInfoList(string DbName, string TableName)
		{
			this.OpenDB();
			List<ColumnInfo> list = new List<ColumnInfo>();
			try
			{
				DataTable schema = this.connect.GetSchema("Columns", new string[]
				{
					null,
					null,
					TableName
				});
				foreach (DataRow dataRow in schema.Rows)
				{
					ColumnInfo columnInfo = new ColumnInfo();
					try
					{
						columnInfo.ColumnOrder = dataRow["ORDINAL_POSITION"].ToString();
						columnInfo.ColumnName = dataRow["COLUMN_NAME"].ToString();
						columnInfo.TypeName = dataRow["DATA_TYPE"].ToString();
						columnInfo.Length = dataRow["CHARACTER_MAXIMUM_LENGTH"].ToString();
						columnInfo.Precision = dataRow["NUMERIC_PRECISION"].ToString();
						columnInfo.Scale = dataRow["NUMERIC_SCALE"].ToString();
						if (dataRow["IS_NULLABLE"].ToString().ToLower() == "true")
						{
							columnInfo.Nullable = true;
						}
						else
						{
							columnInfo.Nullable = false;
						}
						if (dataRow["COLUMN_HASDEFAULT"].ToString().ToLower() == "true")
						{
							columnInfo.DefaultVal = dataRow["COLUMN_DEFAULT"].ToString();
						}
						columnInfo.Description = dataRow["DESCRIPTION"].ToString();
						columnInfo.IsPrimaryKey = false;
						if (dataRow["PRIMARY_KEY"].ToString().ToLower() == "true")
						{
							columnInfo.IsPrimaryKey = true;
						}
						dataRow["COLUMN_FLAGS"].ToString().Trim();
						columnInfo.IsIdentity = false;
						if (dataRow["AUTOINCREMENT"].ToString().ToLower() == "true")
						{
							columnInfo.IsIdentity = true;
						}
					}
					catch
					{
					}
					list.Add(columnInfo);
				}
				list.Sort(new Comparison<ColumnInfo>(this.CompareDinosByintOrder));
			}
			catch
			{
			}
			return list;
		}
		public DataTable GetColumnInfoListSP(string DbName, string TableName)
		{
			return null;
		}
		public DataTable GetKeyName(string DbName, string TableName)
		{
			DataTable dataTable = new DataTable();
			try
			{
				this.OpenDB();
				DataTable schema = this.connect.GetSchema("Columns", new string[]
				{
					null,
					null,
					TableName
				});
				dataTable.Columns.Add("colorder");
				dataTable.Columns.Add("ColumnName");
				dataTable.Columns.Add("TypeName");
				dataTable.Columns.Add("Length");
				dataTable.Columns.Add("Preci");
				dataTable.Columns.Add("Scale");
				dataTable.Columns.Add("IsIdentity");
				dataTable.Columns.Add("isPK");
				dataTable.Columns.Add("cisNull");
				dataTable.Columns.Add("defaultVal");
				dataTable.Columns.Add("deText");
				foreach (DataRow dataRow in schema.Rows)
				{
					if (dataRow["PRIMARY_KEY"] != null && dataRow["PRIMARY_KEY"].ToString().ToLower() == "true")
					{
						DataRow dataRow2 = dataTable.NewRow();
						dataRow2["colorder"] = dataRow["ORDINAL_POSITION"].ToString();
						dataRow2["ColumnName"] = dataRow["COLUMN_NAME"].ToString();
						dataRow2["TypeName"] = dataRow["DATA_TYPE"].ToString();
						dataRow2["Length"] = dataRow["CHARACTER_MAXIMUM_LENGTH"].ToString();
						dataRow2["Preci"] = dataRow["NUMERIC_PRECISION"].ToString();
						dataRow2["Scale"] = dataRow["NUMERIC_SCALE"].ToString();
						dataRow2["IsIdentity"] = ((dataRow["AUTOINCREMENT"].ToString().ToLower() == "true") ? "√" : "");
						dataRow2["isPK"] = "√";
						dataRow2["cisNull"] = ((dataRow["IS_NULLABLE"].ToString().ToLower() == "true") ? "√" : "");
						dataRow2["deText"] = dataRow["DESCRIPTION"].ToString();
						dataTable.Rows.Add(dataRow2);
					}
				}
			}
			catch (Exception ex)
			{
				string arg_2B9_0 = ex.Message;
			}
			return dataTable;
		}
		public List<ColumnInfo> GetKeyList(string DbName, string TableName)
		{
			List<ColumnInfo> columnInfoList = this.GetColumnInfoList(DbName, TableName);
			List<ColumnInfo> list = new List<ColumnInfo>();
			foreach (ColumnInfo current in columnInfoList)
			{
				if (current.IsPrimaryKey || current.IsIdentity)
				{
					list.Add(current);
				}
			}
			return list;
		}
		public List<ColumnInfo> GetFKeyList(string DbName, string TableName)
		{
			List<ColumnInfo> columnInfoList = this.GetColumnInfoList(DbName, TableName);
			List<ColumnInfo> list = new List<ColumnInfo>();
			foreach (ColumnInfo current in columnInfoList)
			{
				if (current.IsForeignKey)
				{
					list.Add(current);
				}
			}
			return list;
		}
		public DataTable GetTabData(string DbName, string TableName)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select * from " + TableName);
			return this.Query(DbName, stringBuilder.ToString()).Tables[0];
		}
		public DataTable GetTabDataBySQL(string DbName, string strSQL)
		{
			return this.Query(DbName, strSQL).Tables[0];
		}
		public bool RenameTable(string DbName, string OldName, string NewName)
		{
			bool result;
			try
			{
				SQLiteCommand sQLiteCommand = this.OpenDB();
				sQLiteCommand.CommandText = "RENAME TABLE " + OldName + " TO " + NewName;
				sQLiteCommand.ExecuteNonQuery();
				result = true;
			}
			catch
			{
				result = false;
			}
			return result;
		}
		public bool DeleteTable(string DbName, string TableName)
		{
			bool result;
			try
			{
				SQLiteCommand sQLiteCommand = this.OpenDB();
				sQLiteCommand.CommandText = "DROP TABLE " + TableName;
				sQLiteCommand.ExecuteNonQuery();
				result = true;
			}
			catch
			{
				result = false;
			}
			return result;
		}
		public string GetVersion()
		{
			return "";
		}
	}
}
