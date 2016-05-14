using Maticsoft.CodeHelper;
using Maticsoft.IDBO;
using Maticsoft.Utility;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;
namespace Maticsoft.DbObjects.MySQL
{
	public class DbObject : IDbObject
	{
		private string cmcfgfile = Application.StartupPath + "\\cmcfg.ini";
		private INIFile cfgfile;
		private bool isdbosp;
		private string _dbconnectStr;
		private MySqlConnection connect = new MySqlConnection();
		public string DbType
		{
			get
			{
				return "MySQL";
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
			this.IsDboSp();
		}
		public DbObject(string DbConnectStr)
		{
			this._dbconnectStr = DbConnectStr;
			this.connect.ConnectionString = DbConnectStr;
		}
		public DbObject(bool SSPI, string Ip, string User, string Pass)
		{
			this.connect = new MySqlConnection();
			if (SSPI)
			{
				this._dbconnectStr = string.Format("server={0};user id={1}; password={2}; database=mysql; pooling=false", Ip, User, Pass);
			}
			else
			{
				this._dbconnectStr = string.Format("server={0};user id={1}; password={2}; database=mysql; pooling=false", Ip, User, Pass);
			}
			this.connect.ConnectionString = this._dbconnectStr;
		}
		private bool IsDboSp()
		{
			if (File.Exists(this.cmcfgfile))
			{
				this.cfgfile = new INIFile(this.cmcfgfile);
				string text = this.cfgfile.IniReadValue("dbo", "dbosp");
				if (text.Trim() == "1")
				{
					this.isdbosp = true;
				}
			}
			return this.isdbosp;
		}
		private MySqlCommand OpenDB(string DbName)
		{
			MySqlCommand result;
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
				MySqlCommand mySqlCommand = new MySqlCommand();
				mySqlCommand.Connection = this.connect;
				if (this.connect.State == ConnectionState.Closed)
				{
					this.connect.Open();
				}
				mySqlCommand.CommandText = "use " + DbName;
				mySqlCommand.ExecuteNonQuery();
				result = mySqlCommand;
			}
			catch (Exception ex)
			{
				string arg_A9_0 = ex.Message;
				result = null;
			}
			return result;
		}
		public int ExecuteSql(string DbName, string SQLString)
		{
			MySqlCommand mySqlCommand = this.OpenDB(DbName);
			mySqlCommand.CommandText = SQLString;
			return mySqlCommand.ExecuteNonQuery();
		}
		public DataSet Query(string DbName, string SQLString)
		{
			DataSet dataSet = new DataSet();
			try
			{
				this.OpenDB(DbName);
				MySqlDataAdapter mySqlDataAdapter = new MySqlDataAdapter(SQLString, this.connect);
				mySqlDataAdapter.Fill(dataSet, "ds");
			}
			catch (MySqlException ex)
			{
				throw new Exception(ex.Message);
			}
			return dataSet;
		}
		public MySqlDataReader ExecuteReader(string DbName, string strSQL)
		{
			MySqlDataReader result;
			try
			{
				this.OpenDB(DbName);
				MySqlCommand mySqlCommand = new MySqlCommand(strSQL, this.connect);
				MySqlDataReader mySqlDataReader = mySqlCommand.ExecuteReader(CommandBehavior.CloseConnection);
				result = mySqlDataReader;
			}
			catch (MySqlException ex)
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
				MySqlCommand mySqlCommand = this.OpenDB(DbName);
				mySqlCommand.CommandText = SQLString;
				object obj = mySqlCommand.ExecuteScalar();
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
		public DataSet RunProcedure(string DbName, string storedProcName, IDataParameter[] parameters, string tableName)
		{
			this.OpenDB(DbName);
			DataSet dataSet = new DataSet();
			new MySqlDataAdapter
			{
				SelectCommand = this.BuildQueryCommand(this.connect, storedProcName, parameters)
			}.Fill(dataSet, tableName);
			return dataSet;
		}
		private MySqlCommand BuildQueryCommand(MySqlConnection connection, string storedProcName, IDataParameter[] parameters)
		{
			MySqlCommand mySqlCommand = new MySqlCommand(storedProcName, connection);
			mySqlCommand.CommandType = CommandType.StoredProcedure;
			for (int i = 0; i < parameters.Length; i++)
			{
				MySqlParameter mySqlParameter = (MySqlParameter)parameters[i];
				if (mySqlParameter != null)
				{
					if ((mySqlParameter.Direction == ParameterDirection.InputOutput || mySqlParameter.Direction == ParameterDirection.Input) && mySqlParameter.Value == null)
					{
						mySqlParameter.Value = DBNull.Value;
					}
					mySqlCommand.Parameters.Add(mySqlParameter);
				}
			}
			return mySqlCommand;
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
			List<string> list = new List<string>();
			string strSQL = "SHOW DATABASES";
			MySqlDataReader mySqlDataReader = this.ExecuteReader("mysql", strSQL);
			while (mySqlDataReader.Read())
			{
				list.Add(mySqlDataReader.GetString(0));
			}
			mySqlDataReader.Close();
			list.Sort(new Comparison<string>(this.CompareStrByOrder));
			return list;
		}
		public List<string> GetTables(string DbName)
		{
			string strSQL = "SHOW TABLES";
			List<string> list = new List<string>();
			MySqlDataReader mySqlDataReader = this.ExecuteReader(DbName, strSQL);
			while (mySqlDataReader.Read())
			{
				list.Add(mySqlDataReader.GetString(0));
			}
			mySqlDataReader.Close();
			list.Sort(new Comparison<string>(this.CompareStrByOrder));
			return list;
		}
		public DataTable GetTablesSP(string DbName)
		{
			DataTable result;
			try
			{
				MySqlParameter[] array = new MySqlParameter[]
				{
					new MySqlParameter("@table_name", MySqlDbType.VarChar, 384),
					new MySqlParameter("@table_owner", MySqlDbType.VarChar, 384),
					new MySqlParameter("@table_qualifier", MySqlDbType.VarChar, 384),
					new MySqlParameter("@table_type", MySqlDbType.VarChar, 100)
				};
				array[0].Value = null;
				array[1].Value = null;
				array[2].Value = null;
				array[3].Value = "'TABLE'";
				DataSet dataSet = this.RunProcedure(DbName, "sp_tables", array, "ds");
				if (dataSet.Tables.Count > 0)
				{
					DataTable dataTable = dataSet.Tables[0];
					dataTable.Columns["TABLE_QUALIFIER"].ColumnName = "db";
					dataTable.Columns["TABLE_OWNER"].ColumnName = "cuser";
					dataTable.Columns["TABLE_NAME"].ColumnName = "name";
					dataTable.Columns["TABLE_TYPE"].ColumnName = "type";
					dataTable.Columns["REMARKS"].ColumnName = "remarks";
					result = dataTable;
				}
				else
				{
					result = null;
				}
			}
			catch
			{
				result = null;
			}
			return result;
		}
		public DataTable GetVIEWs(string DbName)
		{
			return this.GetVIEWsSP(DbName);
		}
		public DataTable GetVIEWsSP(string DbName)
		{
			DataTable result;
			try
			{
				MySqlParameter[] array = new MySqlParameter[]
				{
					new MySqlParameter("@table_name", MySqlDbType.VarChar, 384),
					new MySqlParameter("@table_owner", MySqlDbType.VarChar, 384),
					new MySqlParameter("@table_qualifier", MySqlDbType.VarChar, 384),
					new MySqlParameter("@table_type", MySqlDbType.VarChar, 100)
				};
				array[0].Value = null;
				array[1].Value = null;
				array[2].Value = null;
				array[3].Value = "'VIEW'";
				DataSet dataSet = this.RunProcedure(DbName, "sp_tables", array, "ds");
				if (dataSet.Tables.Count > 0)
				{
					DataTable dataTable = dataSet.Tables[0];
					dataTable.Columns["TABLE_QUALIFIER"].ColumnName = "db";
					dataTable.Columns["TABLE_OWNER"].ColumnName = "cuser";
					dataTable.Columns["TABLE_NAME"].ColumnName = "name";
					dataTable.Columns["TABLE_TYPE"].ColumnName = "type";
					dataTable.Columns["REMARKS"].ColumnName = "remarks";
					result = dataTable;
				}
				else
				{
					result = null;
				}
			}
			catch
			{
				result = null;
			}
			return result;
		}
		public List<string> GetTableViews(string DbName)
		{
			List<string> tables = this.GetTables(DbName);
			DataTable vIEWsSP = this.GetVIEWsSP(DbName);
			if (vIEWsSP != null)
			{
				foreach (DataRow dataRow in vIEWsSP.Rows)
				{
					string item = dataRow["name"].ToString();
					tables.Add(item);
				}
			}
			return tables;
		}
		public DataTable GetTabViews(string DbName)
		{
			string sQLString = "SHOW TABLES";
			DataTable dataTable = this.Query(DbName, sQLString).Tables[0];
			dataTable.Columns[0].ColumnName = "name";
			return dataTable;
		}
		public DataTable GetTabViewsSP(string DbName)
		{
			return null;
		}
		public List<string> GetProcs(string DbName)
		{
			string sQLString = "show procedure status where db='" + DbName + "'";
			DataTable dataTable = this.Query(DbName, sQLString).Tables[0];
			dataTable.Columns[0].ColumnName = "name";
			List<string> list = new List<string>();
			if (dataTable != null)
			{
				DataRow[] array = dataTable.Select("", "name ASC");
				DataRow[] array2 = array;
				for (int i = 0; i < array2.Length; i++)
				{
					DataRow dataRow = array2[i];
					list.Add(dataRow["name"].ToString());
				}
			}
			return list;
		}
		public List<TableInfo> GetTablesInfo(string DbName)
		{
			List<TableInfo> list = new List<TableInfo>();
			string strSQL = "SHOW TABLE STATUS";
			MySqlDataReader mySqlDataReader = this.ExecuteReader(DbName, strSQL);
			while (mySqlDataReader.Read())
			{
				TableInfo tableInfo = new TableInfo();
				tableInfo.TabName = mySqlDataReader.GetString("Name");
				try
				{
					if (mySqlDataReader["Create_time"] != null)
					{
						tableInfo.TabDate = mySqlDataReader.GetString("Create_time");
					}
				}
				catch
				{
				}
				tableInfo.TabType = "U";
				tableInfo.TabUser = "dbo";
				list.Add(tableInfo);
			}
			mySqlDataReader.Close();
			return list;
		}
		public DataTable GetTablesExProperty(string DbName)
		{
			return null;
		}
		public List<TableInfo> GetVIEWsInfo(string DbName)
		{
			return null;
		}
		public List<TableInfo> GetTabViewsInfo(string DbName)
		{
			List<TableInfo> list = new List<TableInfo>();
			string strSQL = "SHOW TABLE STATUS";
			MySqlDataReader mySqlDataReader = this.ExecuteReader(DbName, strSQL);
			while (mySqlDataReader.Read())
			{
				TableInfo tableInfo = new TableInfo();
				tableInfo.TabName = mySqlDataReader.GetString("Name");
				try
				{
					if (mySqlDataReader["Create_time"] != null)
					{
						tableInfo.TabDate = mySqlDataReader.GetString("Create_time");
					}
				}
				catch
				{
				}
				tableInfo.TabType = "U";
				tableInfo.TabUser = "dbo";
				list.Add(tableInfo);
			}
			mySqlDataReader.Close();
			return list;
		}
		public List<TableInfo> GetProcInfo(string DbName)
		{
			return null;
		}
		public string GetObjectInfo(string DbName, string objName)
		{
			return "";
		}
		public List<ColumnInfo> GetColumnList(string DbName, string TableName)
		{
			return this.GetColumnInfoList(DbName, TableName);
		}
		public List<ColumnInfo> GetColumnListSP(string DbName, string TableName)
		{
			return this.GetColumnInfoList(DbName, TableName);
		}
		public List<ColumnInfo> GetColumnInfoList(string DbName, string TableName)
		{
			List<ColumnInfo> result;
			try
			{
				string strSQL = "SHOW COLUMNS FROM " + TableName;
				List<ColumnInfo> list = new List<ColumnInfo>();
				MySqlDataReader mySqlDataReader = this.ExecuteReader(DbName, strSQL);
				int num = 1;
				while (mySqlDataReader.Read())
				{
					ColumnInfo columnInfo = new ColumnInfo();
					columnInfo.ColumnOrder = num.ToString();
					if (!object.Equals(mySqlDataReader["Field"], null) && !object.Equals(mySqlDataReader["Field"], DBNull.Value))
					{
						string name = mySqlDataReader["Field"].GetType().Name;
						string a;
						if ((a = name) != null)
						{
							if (a == "Byte[]")
							{
								columnInfo.ColumnName = Encoding.Default.GetString((byte[])mySqlDataReader["Field"]);
								goto IL_D7;
							}
							if (a == "")
							{
								goto IL_D7;
							}
						}
						columnInfo.ColumnName = mySqlDataReader["Field"].ToString();
					}
					IL_D7:
					if (!object.Equals(mySqlDataReader["Type"], null) && !object.Equals(mySqlDataReader["Type"], DBNull.Value))
					{
						string name2 = mySqlDataReader["Type"].GetType().Name;
						string a2;
						if ((a2 = name2) != null)
						{
							if (a2 == "Byte[]")
							{
								columnInfo.TypeName = Encoding.Default.GetString((byte[])mySqlDataReader["Type"]);
								goto IL_178;
							}
							if (a2 == "")
							{
								goto IL_178;
							}
						}
						columnInfo.TypeName = mySqlDataReader["Type"].ToString();
					}
					IL_178:
					string typeName = columnInfo.TypeName;
					string length = "";
					string precision = "";
					string scale = "";
					this.TypeNameProcess(columnInfo.TypeName, out typeName, out length, out precision, out scale);
					columnInfo.TypeName = typeName;
					columnInfo.Length = length;
					columnInfo.Precision = precision;
					columnInfo.Scale = scale;
					if (!object.Equals(mySqlDataReader["Key"], null) && !object.Equals(mySqlDataReader["Key"], DBNull.Value))
					{
						string text = "";
						string name3 = mySqlDataReader["Key"].GetType().Name;
						string a3;
						if ((a3 = name3) == null)
						{
							goto IL_25A;
						}
						if (!(a3 == "Byte[]"))
						{
							if (!(a3 == ""))
							{
								goto IL_25A;
							}
						}
						else
						{
							text = Encoding.Default.GetString((byte[])mySqlDataReader["Key"]);
						}
						IL_26C:
						columnInfo.IsPrimaryKey = (text.Trim() == "PRI");
						goto IL_289;
						IL_25A:
						text = mySqlDataReader["Key"].ToString();
						goto IL_26C;
					}
					IL_289:
					if (!object.Equals(mySqlDataReader["Null"], null) && !object.Equals(mySqlDataReader["Null"], DBNull.Value))
					{
						string text2 = "";
						string name4 = mySqlDataReader["Null"].GetType().Name;
						string a4;
						if ((a4 = name4) == null)
						{
							goto IL_31A;
						}
						if (!(a4 == "Byte[]"))
						{
							if (!(a4 == ""))
							{
								goto IL_31A;
							}
						}
						else
						{
							text2 = Encoding.Default.GetString((byte[])mySqlDataReader["Null"]);
						}
						IL_32C:
						columnInfo.Nullable = (text2.Trim() == "YES");
						goto IL_349;
						IL_31A:
						text2 = mySqlDataReader["Null"].ToString();
						goto IL_32C;
					}
					IL_349:
					if (!object.Equals(mySqlDataReader["Default"], null) && !object.Equals(mySqlDataReader["Default"], DBNull.Value))
					{
						string name5 = mySqlDataReader["Default"].GetType().Name;
						string a5;
						if ((a5 = name5) != null)
						{
							if (a5 == "Byte[]")
							{
								columnInfo.DefaultVal = Encoding.Default.GetString((byte[])mySqlDataReader["Default"]);
								goto IL_3EA;
							}
							if (a5 == "")
							{
								goto IL_3EA;
							}
						}
						columnInfo.DefaultVal = mySqlDataReader["Default"].ToString();
					}
					IL_3EA:
					columnInfo.IsIdentity = false;
					if (!object.Equals(mySqlDataReader["Extra"], null) && !object.Equals(mySqlDataReader["Extra"], DBNull.Value))
					{
						string name6 = mySqlDataReader["Extra"].GetType().Name;
						string a6;
						if ((a6 = name6) == null)
						{
							goto IL_47F;
						}
						if (!(a6 == "Byte[]"))
						{
							if (!(a6 == ""))
							{
								goto IL_47F;
							}
						}
						else
						{
							columnInfo.Description = Encoding.Default.GetString((byte[])mySqlDataReader["Extra"]);
						}
						IL_495:
						if (columnInfo.Description.Trim() == "auto_increment")
						{
							columnInfo.IsIdentity = true;
							goto IL_4B3;
						}
						goto IL_4B3;
						IL_47F:
						columnInfo.Description = mySqlDataReader["Extra"].ToString();
						goto IL_495;
					}
					IL_4B3:
					list.Add(columnInfo);
					num++;
				}
				mySqlDataReader.Close();
				result = list;
			}
			catch (Exception ex)
			{
				throw new Exception("获取列数据失败" + ex.Message);
			}
			return result;
		}
		private void TypeNameProcess(string strName, out string TypeName, out string Length, out string Preci, out string Scale)
		{
			TypeName = strName;
			int num = strName.IndexOf("(");
			Length = "";
			Preci = "";
			Scale = "";
			if (num > 0)
			{
				TypeName = strName.Substring(0, num);
				string key;
				switch (key = TypeName.Trim().ToUpper())
				{
				case "TINYINT":
				case "SMALLINT":
				case "MEDIUMINT":
				case "INT":
				case "INTEGER":
				case "BIGINT":
				case "TIMESTAMP":
				case "CHAR":
				case "VARCHAR":
				{
					int num3 = strName.IndexOf(")");
					Length = strName.Substring(num + 1, num3 - num - 1);
					return;
				}
				case "FLOAT":
				case "DOUBLE":
				case "REAL":
				case "DECIMAL":
				case "DEC":
				case "NUMERIC":
				{
					int num4 = strName.IndexOf(")");
					string text = strName.Substring(num + 1, num4 - num - 1);
					int num5 = text.IndexOf(",");
					Length = text.Substring(0, num5);
					Scale = text.Substring(num5 + 1);
					break;
				}
				case "ENUM":
				case "SET":
					break;

					return;
				}
			}
		}
		public DataTable GetColumnInfoListSP(string DbName, string TableName)
		{
			return null;
		}
		public DataTable CreateColumnTable()
		{
			DataTable dataTable = new DataTable();
			DataColumn dataColumn = new DataColumn();
			dataColumn.DataType = Type.GetType("System.String");
			dataColumn.ColumnName = "colorder";
			dataTable.Columns.Add(dataColumn);
			dataColumn = new DataColumn();
			dataColumn.DataType = Type.GetType("System.String");
			dataColumn.ColumnName = "ColumnName";
			dataTable.Columns.Add(dataColumn);
			dataColumn = new DataColumn();
			dataColumn.DataType = Type.GetType("System.String");
			dataColumn.ColumnName = "TypeName";
			dataTable.Columns.Add(dataColumn);
			dataColumn = new DataColumn();
			dataColumn.DataType = Type.GetType("System.String");
			dataColumn.ColumnName = "Length";
			dataTable.Columns.Add(dataColumn);
			dataColumn = new DataColumn();
			dataColumn.DataType = Type.GetType("System.String");
			dataColumn.ColumnName = "Preci";
			dataTable.Columns.Add(dataColumn);
			dataColumn = new DataColumn();
			dataColumn.DataType = Type.GetType("System.String");
			dataColumn.ColumnName = "Scale";
			dataTable.Columns.Add(dataColumn);
			dataColumn = new DataColumn();
			dataColumn.DataType = Type.GetType("System.String");
			dataColumn.ColumnName = "IsIdentity";
			dataTable.Columns.Add(dataColumn);
			dataColumn = new DataColumn();
			dataColumn.DataType = Type.GetType("System.String");
			dataColumn.ColumnName = "isPK";
			dataTable.Columns.Add(dataColumn);
			dataColumn = new DataColumn();
			dataColumn.DataType = Type.GetType("System.String");
			dataColumn.ColumnName = "cisNull";
			dataTable.Columns.Add(dataColumn);
			dataColumn = new DataColumn();
			dataColumn.DataType = Type.GetType("System.String");
			dataColumn.ColumnName = "defaultVal";
			dataTable.Columns.Add(dataColumn);
			dataColumn = new DataColumn();
			dataColumn.DataType = Type.GetType("System.String");
			dataColumn.ColumnName = "deText";
			dataTable.Columns.Add(dataColumn);
			return dataTable;
		}
		public DataTable GetKeyName(string DbName, string TableName)
		{
			DataTable dataTable = this.CreateColumnTable();
			List<ColumnInfo> columnInfoList = this.GetColumnInfoList(DbName, TableName);
			DataTable columnInfoDt = CodeCommon.GetColumnInfoDt(columnInfoList);
			DataRow[] array = columnInfoDt.Select(" isPK='√' or IsIdentity='√' ");
			DataRow[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				DataRow dataRow = array2[i];
				DataRow dataRow2 = dataTable.NewRow();
				dataRow2["colorder"] = dataRow["colorder"];
				dataRow2["ColumnName"] = dataRow["ColumnName"];
				dataRow2["TypeName"] = dataRow["TypeName"];
				dataRow2["Length"] = dataRow["Length"];
				dataRow2["Preci"] = dataRow["Preci"];
				dataRow2["Scale"] = dataRow["Scale"];
				dataRow2["IsIdentity"] = dataRow["IsIdentity"];
				dataRow2["isPK"] = dataRow["isPK"];
				dataRow2["cisNull"] = dataRow["cisNull"];
				dataRow2["defaultVal"] = dataRow["defaultVal"];
				dataRow2["deText"] = dataRow["deText"];
				dataTable.Rows.Add(dataRow2);
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
				MySqlCommand mySqlCommand = this.OpenDB(DbName);
				mySqlCommand.CommandText = "RENAME TABLE " + OldName + " TO " + NewName;
				mySqlCommand.ExecuteNonQuery();
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
				MySqlCommand mySqlCommand = this.OpenDB(DbName);
				mySqlCommand.CommandText = "DROP TABLE " + TableName;
				mySqlCommand.ExecuteNonQuery();
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
			string result;
			try
			{
				string sQLString = "execute master..sp_msgetversion ";
				result = this.Query("master", sQLString).Tables[0].Rows[0][0].ToString();
			}
			catch
			{
				result = "";
			}
			return result;
		}
		public string GetTableScript(string DbName, string TableName)
		{
			string result = "";
			string strSQL = "SHOW CREATE TABLE " + TableName;
			MySqlDataReader mySqlDataReader = this.ExecuteReader(DbName, strSQL);
			while (mySqlDataReader.Read())
			{
				result = mySqlDataReader.GetString(1);
			}
			mySqlDataReader.Close();
			return result;
		}
	}
}
