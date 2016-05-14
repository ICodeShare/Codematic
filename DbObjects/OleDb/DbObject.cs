using Maticsoft.CmConfig;
using Maticsoft.CodeHelper;
using Maticsoft.IDBO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Text;
namespace Maticsoft.DbObjects.OleDb
{
	public class DbObject : IDbObject
	{
		private string datatypefile = AppDomain.CurrentDomain.BaseDirectory.TrimEnd(new char[]
		{
			'\\'
		}) + "\\DatatypeMap.cfg";
		private string _dbconnectStr;
		private OleDbConnection connect = new OleDbConnection();
		public string DbType
		{
			get
			{
				return "OleDb";
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
		public DbObject(bool SSPI, string server, string User, string Pass)
		{
			this.connect = new OleDbConnection();
			this._dbconnectStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + server + ";Persist Security Info=False";
			this.connect.ConnectionString = this._dbconnectStr;
		}
		public void OpenDB()
		{
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
				if (this.connect.State == ConnectionState.Closed)
				{
					this.connect.Open();
				}
			}
			catch
			{
			}
		}
		public int ExecuteSql(string DbName, string SQLString)
		{
			this.OpenDB();
			return new OleDbCommand(SQLString, this.connect)
			{
				CommandText = SQLString
			}.ExecuteNonQuery();
		}
		public DataSet Query(string DbName, string SQLString)
		{
			DataSet dataSet = new DataSet();
			try
			{
				this.OpenDB();
				OleDbDataAdapter oleDbDataAdapter = new OleDbDataAdapter(SQLString, this.connect);
				oleDbDataAdapter.Fill(dataSet, "ds");
			}
			catch (OleDbException ex)
			{
				throw new Exception(ex.Message);
			}
			return dataSet;
		}
		public OleDbDataReader ExecuteReader(string strSQL)
		{
			OleDbDataReader result;
			try
			{
				this.OpenDB();
				OleDbCommand oleDbCommand = new OleDbCommand(strSQL, this.connect);
				OleDbDataReader oleDbDataReader = oleDbCommand.ExecuteReader();
				result = oleDbDataReader;
			}
			catch (OleDbException ex)
			{
				throw new Exception(ex.Message);
			}
			return result;
		}
		public object GetSingle(string DbName, string SQLString)
		{
			object result;
			try
			{
				this.OpenDB();
				OleDbCommand oleDbCommand = new OleDbCommand(SQLString, this.connect);
				object obj = oleDbCommand.ExecuteScalar();
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
			this.OpenDB();
			DataTable oleDbSchemaTable = this.connect.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[]
			{
				null,
				null,
				null,
				"TABLE"
			});
			List<string> list = new List<string>();
			foreach (DataRow dataRow in oleDbSchemaTable.Rows)
			{
				list.Add(dataRow[2].ToString());
			}
			list.Sort(new Comparison<string>(this.CompareStrByOrder));
			return list;
		}
		public DataTable GetVIEWs(string DbName)
		{
			this.OpenDB();
			DataTable oleDbSchemaTable = this.connect.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[]
			{
				null,
				null,
				null,
				"VIEW"
			});
			return this.Tab2Tab(oleDbSchemaTable);
		}
		public List<string> GetTableViews(string DbName)
		{
			return this.GetTables(DbName);
		}
		public DataTable GetTabViews(string DbName)
		{
			this.OpenDB();
			DataTable oleDbSchemaTable = this.connect.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
			return this.Tab2Tab(oleDbSchemaTable);
		}
		public List<string> GetProcs(string DbName)
		{
			return new List<string>();
		}
		public List<TableInfo> GetTablesInfo(string DbName)
		{
			this.OpenDB();
			DataTable oleDbSchemaTable = this.connect.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[]
			{
				null,
				null,
				null,
				"TABLE"
			});
			List<TableInfo> list = new List<TableInfo>();
			foreach (DataRow dataRow in oleDbSchemaTable.Rows)
			{
				list.Add(new TableInfo
				{
					TabName = dataRow[2].ToString(),
					TabUser = "dbo",
					TabType = dataRow[3].ToString(),
					TabDate = dataRow[6].ToString()
				});
			}
			list.Sort(new Comparison<TableInfo>(this.CompareTabByOrder));
			return list;
		}
		public DataTable GetTablesExProperty(string DbName)
		{
			return null;
		}
		public List<TableInfo> GetVIEWsInfo(string DbName)
		{
			this.OpenDB();
			DataTable oleDbSchemaTable = this.connect.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[]
			{
				null,
				null,
				null,
				"VIEW"
			});
			List<TableInfo> list = new List<TableInfo>();
			foreach (DataRow dataRow in oleDbSchemaTable.Rows)
			{
				list.Add(new TableInfo
				{
					TabName = dataRow[2].ToString(),
					TabUser = "dbo",
					TabType = dataRow[3].ToString(),
					TabDate = dataRow[6].ToString()
				});
			}
			list.Sort(new Comparison<TableInfo>(this.CompareTabByOrder));
			return list;
		}
		public List<TableInfo> GetTabViewsInfo(string DbName)
		{
			this.OpenDB();
			DataTable oleDbSchemaTable = this.connect.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
			List<TableInfo> list = new List<TableInfo>();
			foreach (DataRow dataRow in oleDbSchemaTable.Rows)
			{
				list.Add(new TableInfo
				{
					TabName = dataRow[2].ToString(),
					TabUser = "dbo",
					TabType = dataRow[3].ToString(),
					TabDate = dataRow[6].ToString()
				});
			}
			list.Sort(new Comparison<TableInfo>(this.CompareTabByOrder));
			return list;
		}
		public List<TableInfo> GetProcInfo(string DbName)
		{
			return null;
		}
		private int CompareTabByOrder(TableInfo x, TableInfo y)
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
				return x.TabName.CompareTo(y.TabName);
			}
		}
		public string GetObjectInfo(string DbName, string objName)
		{
			return null;
		}
		private string GetColumnType(string typeNum)
		{
			string result = typeNum;
			if (File.Exists(this.datatypefile))
			{
				result = DatatypeMap.GetValueFromCfg(this.datatypefile, "AccessTypeMap", typeNum);
			}
			return result;
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
				string columnType = this.GetColumnType(dataRow[11].ToString());
				dataRow2["TypeName"] = columnType;
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
				OleDbConnection arg_26_0 = this.connect;
				Guid arg_26_1 = OleDbSchemaGuid.Columns;
				object[] array = new object[4];
				array[2] = TableName;
				DataTable oleDbSchemaTable = arg_26_0.GetOleDbSchemaTable(arg_26_1, array);
				Hashtable primaryKey = this.GetPrimaryKey(DbName, TableName);
				Hashtable foreignKey = this.GetForeignKey(DbName, TableName);
				foreach (DataRow dataRow in oleDbSchemaTable.Rows)
				{
					ColumnInfo columnInfo = new ColumnInfo();
					try
					{
						columnInfo.ColumnOrder = dataRow["ORDINAL_POSITION"].ToString();
						columnInfo.ColumnName = dataRow["COLUMN_NAME"].ToString();
						string columnType = this.GetColumnType(dataRow["DATA_TYPE"].ToString());
						columnInfo.TypeName = columnType;
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
						columnInfo.Description = ((dataRow["DESCRIPTION"] == null) ? columnInfo.ColumnName : dataRow["DESCRIPTION"].ToString());
						columnInfo.IsPrimaryKey = false;
						if (primaryKey[TableName] != null && primaryKey[TableName].ToString() == columnInfo.ColumnName)
						{
							columnInfo.IsPrimaryKey = true;
						}
						columnInfo.IsForeignKey = false;
						if (foreignKey[TableName] != null && foreignKey[TableName].ToString() == columnInfo.ColumnName)
						{
							columnInfo.IsForeignKey = true;
						}
						string a = dataRow["COLUMN_FLAGS"].ToString().Trim();
						columnInfo.IsIdentity = false;
						if (dataRow["DATA_TYPE"].ToString().Trim() == "3" && a == "90")
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
		public DataTable GetColumnInfoListSP(string DbName, string ViewName)
		{
			return null;
		}
		public DataTable GetKeyName(string DbName, string TableName)
		{
			DataTable dataTable = new DataTable();
			try
			{
				this.OpenDB();
				OleDbConnection arg_26_0 = this.connect;
				Guid arg_26_1 = OleDbSchemaGuid.Columns;
				object[] array = new object[4];
				array[2] = TableName;
				DataTable oleDbSchemaTable = arg_26_0.GetOleDbSchemaTable(arg_26_1, array);
				Hashtable primaryKey = this.GetPrimaryKey(DbName, TableName);
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
				foreach (DataRow dataRow in oleDbSchemaTable.Rows)
				{
					string a = dataRow["TABLE_NAME"].ToString();
					string b = dataRow["COLUMN_NAME"].ToString();
					if (primaryKey[TableName] != null && a == TableName && primaryKey[TableName].ToString() == b)
					{
						DataRow dataRow2 = dataTable.NewRow();
						dataRow2["colorder"] = dataRow["ORDINAL_POSITION"].ToString();
						dataRow2["ColumnName"] = dataRow["COLUMN_NAME"].ToString();
						dataRow2["TypeName"] = this.GetColumnType(dataRow["DATA_TYPE"].ToString());
						dataRow2["Length"] = dataRow["CHARACTER_MAXIMUM_LENGTH"].ToString();
						dataRow2["Preci"] = dataRow["NUMERIC_PRECISION"].ToString();
						dataRow2["Scale"] = dataRow["NUMERIC_SCALE"].ToString();
						dataRow2["IsIdentity"] = "";
						dataRow2["isPK"] = "√";
						dataRow2["cisNull"] = "";
						dataRow2["deText"] = "";
						if (dataRow["IS_NULLABLE"].ToString().ToLower() == "true")
						{
							dataRow2["cisNull"] = "√";
						}
						if (dataRow["COLUMN_HASDEFAULT"].ToString().ToLower() == "true")
						{
							dataRow2["defaultVal"] = dataRow["COLUMN_DEFAULT"].ToString();
						}
						string a2 = dataRow["COLUMN_FLAGS"].ToString().Trim();
						if (dataRow["DATA_TYPE"].ToString().Trim() == "3" && a2 == "90")
						{
							dataRow2["IsIdentity"] = "√";
						}
						dataTable.Rows.Add(dataRow2);
					}
				}
			}
			catch (Exception ex)
			{
				string arg_35E_0 = ex.Message;
			}
			return dataTable;
		}
		public List<ColumnInfo> GetKeyNamelist(string DbName, string TableName)
		{
			this.OpenDB();
			OleDbConnection arg_20_0 = this.connect;
			Guid arg_20_1 = OleDbSchemaGuid.Columns;
			object[] array = new object[4];
			array[2] = TableName;
			DataTable oleDbSchemaTable = arg_20_0.GetOleDbSchemaTable(arg_20_1, array);
			Hashtable primaryKey = this.GetPrimaryKey(DbName, TableName);
			List<ColumnInfo> list = new List<ColumnInfo>();
			foreach (DataRow dataRow in oleDbSchemaTable.Rows)
			{
				string b = dataRow["COLUMN_NAME"].ToString();
				if (primaryKey[TableName] != null && primaryKey[TableName].ToString() == b)
				{
					ColumnInfo columnInfo = new ColumnInfo();
					columnInfo.ColumnOrder = dataRow["ORDINAL_POSITION"].ToString();
					columnInfo.ColumnName = dataRow["COLUMN_NAME"].ToString();
					string columnType = this.GetColumnType(dataRow["DATA_TYPE"].ToString());
					columnInfo.TypeName = columnType;
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
					columnInfo.Description = ((dataRow["DESCRIPTION"] == null) ? columnInfo.ColumnName : dataRow["DESCRIPTION"].ToString());
					columnInfo.IsPrimaryKey = true;
					string a = dataRow["COLUMN_FLAGS"].ToString().Trim();
					columnInfo.IsIdentity = false;
					if (dataRow["DATA_TYPE"].ToString().Trim() == "3" && a == "90")
					{
						columnInfo.IsIdentity = true;
					}
					list.Add(columnInfo);
				}
			}
			return list;
		}
		private Hashtable GetPrimaryKey(string DbName, string TableName)
		{
			Hashtable hashtable = new Hashtable();
			try
			{
				this.OpenDB();
				DataTable oleDbSchemaTable = this.connect.GetOleDbSchemaTable(OleDbSchemaGuid.Primary_Keys, null);
				foreach (DataRow dataRow in oleDbSchemaTable.Rows)
				{
					string key = dataRow["TABLE_NAME"].ToString();
					string value = dataRow["COLUMN_NAME"].ToString();
					if (!hashtable.Contains(key))
					{
						hashtable.Add(key, value);
					}
				}
			}
			catch
			{
			}
			return hashtable;
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
		private Hashtable GetForeignKey(string DbName, string TableName)
		{
			Hashtable hashtable = new Hashtable();
			try
			{
				this.OpenDB();
				DataTable oleDbSchemaTable = this.connect.GetOleDbSchemaTable(OleDbSchemaGuid.Foreign_Keys, null);
				foreach (DataRow dataRow in oleDbSchemaTable.Rows)
				{
					string key = dataRow["TABLE_NAME"].ToString();
					string value = dataRow["COLUMN_NAME"].ToString();
					if (!hashtable.Contains(key))
					{
						hashtable.Add(key, value);
					}
				}
			}
			catch
			{
			}
			return hashtable;
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
			stringBuilder.Append("select * from [" + TableName + "]");
			return this.Query("", stringBuilder.ToString()).Tables[0];
		}
		public DataTable GetTabDataBySQL(string DbName, string strSQL)
		{
			return this.Query("", strSQL).Tables[0];
		}
		public bool RenameTable(string DbName, string OldName, string NewName)
		{
			return false;
		}
		public bool DeleteTable(string DbName, string TableName)
		{
			bool result;
			try
			{
				string sql="DROP TABLE " + TableName;
                this.ExecuteSql(DbName, sql);
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
