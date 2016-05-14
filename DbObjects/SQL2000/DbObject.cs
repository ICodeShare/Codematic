using Maticsoft.CodeHelper;
using Maticsoft.IDBO;
using Maticsoft.Utility;
using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Windows.Forms;
namespace Maticsoft.DbObjects.SQL2000
{
	public class DbObject : IDbObject
	{
		private string cmcfgfile = Application.StartupPath + "\\cmcfg.ini";
		private INIFile cfgfile;
		private bool isdbosp;
		private string _dbconnectStr;
		private SqlConnection connect = new SqlConnection();
		public string DbType
		{
			get
			{
				return "SQL2000";
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
			this.connect = new SqlConnection();
			if (SSPI)
			{
				this._dbconnectStr = "Integrated Security=SSPI;Data Source=" + Ip + ";Initial Catalog=master";
			}
			else
			{
				if (Pass == "")
				{
					this._dbconnectStr = string.Concat(new string[]
					{
						"user id=",
						User,
						";initial catalog=master;data source=",
						Ip,
						";Connect Timeout=30"
					});
				}
				else
				{
					this._dbconnectStr = string.Concat(new string[]
					{
						"user id=",
						User,
						";password=",
						Pass,
						";initial catalog=master;data source=",
						Ip,
						";Connect Timeout=30"
					});
				}
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
		private SqlCommand OpenDB(string DbName)
		{
			SqlCommand result;
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
				SqlCommand sqlCommand = new SqlCommand();
				sqlCommand.Connection = this.connect;
				if (this.connect.State == ConnectionState.Closed)
				{
					this.connect.Open();
				}
				sqlCommand.CommandText = "use [" + DbName + "]";
				sqlCommand.ExecuteNonQuery();
				result = sqlCommand;
			}
			catch (Exception ex)
			{
				string arg_AE_0 = ex.Message;
				result = null;
			}
			return result;
		}
		public int ExecuteSql(string DbName, string SQLString)
		{
			SqlCommand sqlCommand = this.OpenDB(DbName);
			sqlCommand.CommandText = SQLString;
			return sqlCommand.ExecuteNonQuery();
		}
		public DataSet Query(string DbName, string SQLString)
		{
			DataSet dataSet = new DataSet();
			try
			{
				this.OpenDB(DbName);
				SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(SQLString, this.connect);
				sqlDataAdapter.Fill(dataSet, "ds");
			}
			catch (SqlException ex)
			{
				throw new Exception(ex.Message);
			}
			return dataSet;
		}
		public SqlDataReader ExecuteReader(string DbName, string strSQL)
		{
			SqlDataReader result;
			try
			{
				this.OpenDB(DbName);
				SqlCommand sqlCommand = new SqlCommand(strSQL, this.connect);
				SqlDataReader sqlDataReader = sqlCommand.ExecuteReader(CommandBehavior.CloseConnection);
				result = sqlDataReader;
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
				SqlCommand sqlCommand = this.OpenDB(DbName);
				sqlCommand.CommandText = SQLString;
				object obj = sqlCommand.ExecuteScalar();
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
			new SqlDataAdapter
			{
				SelectCommand = this.BuildQueryCommand(this.connect, storedProcName, parameters)
			}.Fill(dataSet, tableName);
			return dataSet;
		}
		private SqlCommand BuildQueryCommand(SqlConnection connection, string storedProcName, IDataParameter[] parameters)
		{
			SqlCommand sqlCommand = new SqlCommand(storedProcName, connection);
			sqlCommand.CommandType = CommandType.StoredProcedure;
			for (int i = 0; i < parameters.Length; i++)
			{
				SqlParameter sqlParameter = (SqlParameter)parameters[i];
				if (sqlParameter != null)
				{
					if ((sqlParameter.Direction == ParameterDirection.InputOutput || sqlParameter.Direction == ParameterDirection.Input) && sqlParameter.Value == null)
					{
						sqlParameter.Value = DBNull.Value;
					}
					sqlCommand.Parameters.Add(sqlParameter);
				}
			}
			return sqlCommand;
		}
		public List<string> GetDBList()
		{
			string strSQL = "select name from sysdatabases";
			List<string> list = new List<string>();
			SqlDataReader sqlDataReader = this.ExecuteReader("master", strSQL);
			while (sqlDataReader.Read())
			{
				list.Add(sqlDataReader.GetString(0));
			}
			sqlDataReader.Close();
			return list;
		}
		public List<string> GetTables(string DbName)
		{
			if (this.isdbosp)
			{
				return this.GetTablesSP(DbName);
			}
			string strSQL = "select [name] from sysobjects where xtype='U'and [name]<>'dtproperties' order by [name]";
			List<string> list = new List<string>();
			using (SqlDataReader sqlDataReader = this.ExecuteReader(DbName, strSQL))
			{
				while (sqlDataReader.Read())
				{
					list.Add(sqlDataReader.GetString(0));
				}
				sqlDataReader.Close();
			}
			return list;
		}
		public List<string> GetTablesSP(string DbName)
		{
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@table_name", SqlDbType.NVarChar, 384),
				new SqlParameter("@table_owner", SqlDbType.NVarChar, 384),
				new SqlParameter("@table_qualifier", SqlDbType.NVarChar, 384),
				new SqlParameter("@table_type", SqlDbType.VarChar, 100)
			};
			array[0].Value = null;
			array[1].Value = null;
			array[2].Value = null;
			array[3].Value = "'TABLE'";
			DataSet dataSet = this.RunProcedure(DbName, "sp_tables", array, "ds");
			List<string> list = new List<string>();
			if (dataSet.Tables.Count > 0)
			{
				DataTable dataTable = dataSet.Tables[0];
				for (int i = 0; i < dataTable.Rows.Count; i++)
				{
					list.Add(dataTable.Rows[i]["TABLE_NAME"].ToString());
				}
				return list;
			}
			return null;
		}
		public DataTable GetVIEWs(string DbName)
		{
			if (this.isdbosp)
			{
				return this.GetVIEWsSP(DbName);
			}
			string sQLString = "select [name] from sysobjects where xtype='V' and [name]<>'syssegments' and [name]<>'sysconstraints' order by [name]";
			return this.Query(DbName, sQLString).Tables[0];
		}
		public DataTable GetVIEWsSP(string DbName)
		{
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@table_name", SqlDbType.NVarChar, 384),
				new SqlParameter("@table_owner", SqlDbType.NVarChar, 384),
				new SqlParameter("@table_qualifier", SqlDbType.NVarChar, 384),
				new SqlParameter("@table_type", SqlDbType.VarChar, 100)
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
				return dataTable;
			}
			return null;
		}
		public List<string> GetTableViews(string DbName)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select [name],sysobjects.xtype type from sysobjects ");
			stringBuilder.Append("where (xtype='U' or xtype='V' ) ");
			stringBuilder.Append("and [name]<>'dtproperties' and [name]<>'syssegments' and [name]<>'sysconstraints' ");
			stringBuilder.Append("order by xtype,[name]");
			List<string> list = new List<string>();
			using (SqlDataReader sqlDataReader = this.ExecuteReader(DbName, stringBuilder.ToString()))
			{
				while (sqlDataReader.Read())
				{
					list.Add(sqlDataReader.GetString(0));
				}
				sqlDataReader.Close();
			}
			return list;
		}
		public DataTable GetTabViews(string DbName)
		{
			if (this.isdbosp)
			{
				return this.GetTabViewsSP(DbName);
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select [name],sysobjects.xtype type from sysobjects ");
			stringBuilder.Append("where (xtype='U' or xtype='V' or xtype='P') ");
			stringBuilder.Append("and [name]<>'dtproperties' and [name]<>'syssegments' and [name]<>'sysconstraints' ");
			stringBuilder.Append("order by xtype,[name]");
			return this.Query(DbName, stringBuilder.ToString()).Tables[0];
		}
		public DataTable GetTabViewsSP(string DbName)
		{
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@table_name", SqlDbType.NVarChar, 384),
				new SqlParameter("@table_owner", SqlDbType.NVarChar, 384),
				new SqlParameter("@table_qualifier", SqlDbType.NVarChar, 384),
				new SqlParameter("@table_type", SqlDbType.VarChar, 100)
			};
			array[0].Value = null;
			array[1].Value = null;
			array[2].Value = null;
			array[3].Value = "'TABLE','VIEW'";
			DataSet dataSet = this.RunProcedure(DbName, "sp_tables", array, "ds");
			if (dataSet.Tables.Count > 0)
			{
				DataTable dataTable = dataSet.Tables[0];
				dataTable.Columns["TABLE_QUALIFIER"].ColumnName = "db";
				dataTable.Columns["TABLE_OWNER"].ColumnName = "cuser";
				dataTable.Columns["TABLE_NAME"].ColumnName = "name";
				dataTable.Columns["TABLE_TYPE"].ColumnName = "type";
				dataTable.Columns["REMARKS"].ColumnName = "remarks";
				return dataTable;
			}
			return null;
		}
		public List<string> GetProcs(string DbName)
		{
			string sQLString = "select [name] from sysobjects where xtype='P'and [name]<>'dtproperties' order by [name]";
			DataTable dataTable = this.Query(DbName, sQLString).Tables[0];
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
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select sysobjects.[name] name,sysusers.name cuser,");
			stringBuilder.Append("sysobjects.xtype type,sysobjects.crdate dates ");
			stringBuilder.Append("from sysobjects,sysusers ");
			stringBuilder.Append("where sysusers.uid=sysobjects.uid ");
			stringBuilder.Append("and sysobjects.xtype='U' ");
			stringBuilder.Append("and  sysobjects.[name]<>'dtproperties' ");
			stringBuilder.Append("order by sysobjects.id");
			List<TableInfo> list = new List<TableInfo>();
			using (SqlDataReader sqlDataReader = this.ExecuteReader(DbName, stringBuilder.ToString()))
			{
				while (sqlDataReader.Read())
				{
					list.Add(new TableInfo
					{
						TabName = sqlDataReader.GetString(0),
						TabDate = sqlDataReader.GetValue(3).ToString(),
						TabType = sqlDataReader.GetString(2),
						TabUser = sqlDataReader.GetString(1)
					});
				}
				sqlDataReader.Close();
			}
			return list;
		}
		public DataTable GetTablesExProperty(string DbName)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("SELECT objname,name,value ");
			stringBuilder.Append("FROM ::fn_listextendedproperty (NULL, 'user', 'dbo', 'table',NULL, NULL, default)  ");
			return this.Query(DbName, stringBuilder.ToString()).Tables[0];
		}
		public List<TableInfo> GetVIEWsInfo(string DbName)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select sysobjects.[name] name,sysusers.name cuser,");
			stringBuilder.Append("sysobjects.xtype type,sysobjects.crdate dates ");
			stringBuilder.Append("from sysobjects,sysusers ");
			stringBuilder.Append("where sysusers.uid=sysobjects.uid ");
			stringBuilder.Append("and sysobjects.xtype='V' ");
			stringBuilder.Append("and sysobjects.[name]<>'syssegments' and sysobjects.[name]<>'sysconstraints'  ");
			stringBuilder.Append("order by sysobjects.id");
			List<TableInfo> list = new List<TableInfo>();
			using (SqlDataReader sqlDataReader = this.ExecuteReader(DbName, stringBuilder.ToString()))
			{
				while (sqlDataReader.Read())
				{
					list.Add(new TableInfo
					{
						TabName = sqlDataReader.GetString(0),
						TabDate = sqlDataReader.GetValue(3).ToString(),
						TabType = sqlDataReader.GetString(2),
						TabUser = sqlDataReader.GetString(1)
					});
				}
				sqlDataReader.Close();
			}
			return list;
		}
		public List<TableInfo> GetTabViewsInfo(string DbName)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select sysobjects.[name] name,sysusers.name cuser,");
			stringBuilder.Append("sysobjects.xtype type,sysobjects.crdate dates ");
			stringBuilder.Append("from sysobjects,sysusers ");
			stringBuilder.Append("where sysusers.uid=sysobjects.uid ");
			stringBuilder.Append("and (sysobjects.xtype='U' or sysobjects.xtype='V' or sysobjects.xtype='P') ");
			stringBuilder.Append("and sysobjects.[name]<>'dtproperties' and sysobjects.[name]<>'syssegments' and sysobjects.[name]<>'sysconstraints'  ");
			stringBuilder.Append("order by sysobjects.xtype,sysobjects.[name]");
			List<TableInfo> list = new List<TableInfo>();
			using (SqlDataReader sqlDataReader = this.ExecuteReader(DbName, stringBuilder.ToString()))
			{
				while (sqlDataReader.Read())
				{
					list.Add(new TableInfo
					{
						TabName = sqlDataReader.GetString(0),
						TabDate = sqlDataReader.GetValue(3).ToString(),
						TabType = sqlDataReader.GetString(2),
						TabUser = sqlDataReader.GetString(1)
					});
				}
				sqlDataReader.Close();
			}
			return list;
		}
		public List<TableInfo> GetProcInfo(string DbName)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select sysobjects.[name] name,sysusers.name cuser,");
			stringBuilder.Append("sysobjects.xtype type,sysobjects.crdate dates ");
			stringBuilder.Append("from sysobjects,sysusers ");
			stringBuilder.Append("where sysusers.uid=sysobjects.uid ");
			stringBuilder.Append("and sysobjects.xtype='P' ");
			stringBuilder.Append("order by sysobjects.id");
			List<TableInfo> list = new List<TableInfo>();
			using (SqlDataReader sqlDataReader = this.ExecuteReader(DbName, stringBuilder.ToString()))
			{
				while (sqlDataReader.Read())
				{
					list.Add(new TableInfo
					{
						TabName = sqlDataReader.GetString(0),
						TabDate = sqlDataReader.GetValue(3).ToString(),
						TabType = sqlDataReader.GetString(2),
						TabUser = sqlDataReader.GetString(1)
					});
				}
				sqlDataReader.Close();
			}
			return list;
		}
		public string GetObjectInfo(string DbName, string objName)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select b.text ");
			stringBuilder.Append("from sysobjects a, syscomments b  ");
			stringBuilder.Append("where a.id = b.id  ");
			stringBuilder.Append(" and a.name= '" + objName + "'");
			object single = this.GetSingle(DbName, stringBuilder.ToString());
			if (single != null)
			{
				return single.ToString();
			}
			return "";
		}
		public List<ColumnInfo> GetColumnList(string DbName, string TableName)
		{
			List<ColumnInfo> result;
			try
			{
				if (this.isdbosp)
				{
					result = this.GetColumnListSP(DbName, TableName);
				}
				else
				{
					StringBuilder stringBuilder = new StringBuilder();
					stringBuilder.Append("Select ");
					stringBuilder.Append("a.colorder as colorder,");
					stringBuilder.Append("a.name as ColumnName,");
					stringBuilder.Append("b.name as TypeName, ");
					stringBuilder.Append("Length=CASE WHEN b.name='nchar' THEN a.length/2 WHEN b.name='nvarchar' THEN a.length/2 ELSE a.length END, ");
					stringBuilder.Append("a.isoutparam as isoutparam ");
					stringBuilder.Append(" from syscolumns a, systypes b, sysobjects c ");
					stringBuilder.Append(" where a.xtype = b.xusertype ");
					stringBuilder.Append("and a.id = c.id ");
					stringBuilder.Append("and c.name ='" + TableName + "'");
					stringBuilder.Append(" order by a.colorder");
					ArrayList arrayList = new ArrayList();
					List<ColumnInfo> list = new List<ColumnInfo>();
					using (SqlDataReader sqlDataReader = this.ExecuteReader(DbName, stringBuilder.ToString()))
					{
						while (sqlDataReader.Read())
						{
							ColumnInfo columnInfo = new ColumnInfo();
							columnInfo.ColumnOrder = sqlDataReader.GetValue(0).ToString();
							columnInfo.ColumnName = sqlDataReader.GetString(1);
							columnInfo.TypeName = sqlDataReader.GetString(2);
							columnInfo.Length = sqlDataReader.GetValue(3).ToString();
							columnInfo.Precision = "";
							columnInfo.Scale = "";
							columnInfo.IsPrimaryKey = false;
							columnInfo.Nullable = true;
							columnInfo.DefaultVal = "";
							columnInfo.IsIdentity = false;
							columnInfo.Description = ((sqlDataReader.GetValue(4).ToString() == "1") ? "isoutparam" : "");
							list.Add(columnInfo);
							if (!arrayList.Contains(columnInfo.ColumnName))
							{
								list.Add(columnInfo);
								arrayList.Add(columnInfo.ColumnName);
							}
						}
						sqlDataReader.Close();
					}
					result = list;
				}
			}
			catch
			{
				result = null;
			}
			return result;
		}
		public List<ColumnInfo> GetColumnListSP(string DbName, string TableName)
		{
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@table_name", SqlDbType.NVarChar, 384),
				new SqlParameter("@table_owner", SqlDbType.NVarChar, 384),
				new SqlParameter("@table_qualifier", SqlDbType.NVarChar, 384),
				new SqlParameter("@column_name", SqlDbType.VarChar, 100)
			};
			array[0].Value = TableName;
			array[1].Value = null;
			array[2].Value = null;
			array[3].Value = null;
			DataSet dataSet = this.RunProcedure(DbName, "sp_columns", array, "ds");
			int count = dataSet.Tables.Count;
			if (count > 0)
			{
				DataTable dataTable = dataSet.Tables[0];
				int count2 = dataTable.Rows.Count;
				DataTable dataTable2 = this.CreateColumnTable();
				for (int i = 0; i < count2; i++)
				{
					DataRow dataRow = dataTable2.NewRow();
					dataRow["colorder"] = dataTable.Rows[i]["ORDINAL_POSITION"];
					dataRow["ColumnName"] = dataTable.Rows[i]["COLUMN_NAME"];
					string text = dataTable.Rows[i]["TYPE_NAME"].ToString().Trim();
					dataRow["TypeName"] = ((text == "int identity") ? "int" : text);
					dataRow["Length"] = dataTable.Rows[i]["LENGTH"];
					dataRow["Preci"] = dataTable.Rows[i]["PRECISION"];
					dataRow["Scale"] = dataTable.Rows[i]["SCALE"];
					dataRow["IsIdentity"] = ((text == "int identity") ? "√" : "");
					dataRow["isPK"] = "";
					dataRow["cisNull"] = ((dataTable.Rows[i]["NULLABLE"].ToString().Trim() == "1") ? "√" : "");
					dataRow["defaultVal"] = dataTable.Rows[i]["COLUMN_DEF"].ToString().Replace("(", "").Replace(")", "");
					dataRow["deText"] = dataTable.Rows[i]["REMARKS"];
					dataTable2.Rows.Add(dataRow);
				}
				return CodeCommon.GetColumnInfos(dataTable2);
			}
			return null;
		}
		public List<ColumnInfo> GetColumnInfoList(string DbName, string TableName)
		{
			if (this.isdbosp)
			{
				return this.GetColumnInfoListSP(DbName, TableName);
			}
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("SELECT ");
			stringBuilder.Append("colorder=a.colorder,");
			stringBuilder.Append("ColumnName=a.name,");
			stringBuilder.Append("TypeName=b.name, ");
			stringBuilder.Append("Length=CASE WHEN b.name='nchar' THEN a.length/2 WHEN b.name='nvarchar' THEN a.length/2 ELSE a.length END,");
			stringBuilder.Append("Preci=COLUMNPROPERTY(a.id,a.name,'PRECISION'), ");
			stringBuilder.Append("Scale=isnull(COLUMNPROPERTY(a.id,a.name,'Scale'),0), ");
			stringBuilder.Append("IsIdentity=case when COLUMNPROPERTY( a.id,a.name,'IsIdentity')=1 then '√'else '' end,");
			stringBuilder.Append("isPK=case when exists(SELECT 1 FROM sysobjects where xtype='PK' and name in (");
			stringBuilder.Append("SELECT name FROM sysindexes WHERE indid in( ");
			stringBuilder.Append("SELECT indid FROM sysindexkeys WHERE id = a.id AND colid=a.colid ");
			stringBuilder.Append("))) then '√' else '' end, ");
			stringBuilder.Append("cisNull=case when a.isnullable=1 then '√'else '' end, ");
			stringBuilder.Append("defaultVal=isnull(e.text,''), ");
			stringBuilder.Append("deText=isnull(g.[value],'') ");
			stringBuilder.Append("FROM syscolumns a ");
			stringBuilder.Append("left join systypes b on a.xusertype=b.xusertype ");
			stringBuilder.Append("inner join sysobjects d on a.id=d.id  and (d.xtype='U' or d.xtype='V') and  d.name<>'dtproperties' ");
			stringBuilder.Append("left join syscomments e on a.cdefault=e.id ");
			stringBuilder.Append("left join sysproperties g on a.id=g.id and a.colid=g.smallid ");
			stringBuilder.Append("where d.name='" + TableName + "' ");
			stringBuilder.Append("order by a.id,a.colorder ");
			List<ColumnInfo> list = new List<ColumnInfo>();
			using (SqlDataReader sqlDataReader = this.ExecuteReader(DbName, stringBuilder.ToString()))
			{
				while (sqlDataReader.Read())
				{
					list.Add(new ColumnInfo
					{
						ColumnOrder = sqlDataReader.GetValue(0).ToString(),
						ColumnName = sqlDataReader.GetString(1),
						TypeName = sqlDataReader.GetString(2),
						Length = sqlDataReader.GetValue(3).ToString(),
						Precision = sqlDataReader.GetValue(4).ToString(),
						Scale = sqlDataReader.GetValue(5).ToString(),
						IsIdentity = sqlDataReader.GetString(6) == "√",
						IsPrimaryKey = sqlDataReader.GetString(7) == "√",
						Nullable = sqlDataReader.GetString(8) == "√",
						DefaultVal = sqlDataReader.GetString(9).Replace("(", "").Replace(")", ""),
						Description = sqlDataReader.GetString(10)
					});
				}
				sqlDataReader.Close();
			}
			return list;
		}
		public List<ColumnInfo> GetColumnInfoListSP(string DbName, string TableName)
		{
			SqlParameter[] array = new SqlParameter[]
			{
				new SqlParameter("@table_name", SqlDbType.NVarChar, 384),
				new SqlParameter("@table_owner", SqlDbType.NVarChar, 384),
				new SqlParameter("@table_qualifier", SqlDbType.NVarChar, 384),
				new SqlParameter("@column_name", SqlDbType.VarChar, 100)
			};
			array[0].Value = TableName;
			array[1].Value = null;
			array[2].Value = null;
			array[3].Value = null;
			DataSet dataSet = this.RunProcedure(DbName, "sp_columns", array, "ds");
			int count = dataSet.Tables.Count;
			if (count > 0)
			{
				DataTable dataTable = dataSet.Tables[0];
				int count2 = dataTable.Rows.Count;
				DataTable dataTable2 = this.CreateColumnTable();
				for (int i = 0; i < count2; i++)
				{
					DataRow dataRow = dataTable2.NewRow();
					dataRow["colorder"] = dataTable.Rows[i]["ORDINAL_POSITION"];
					dataRow["ColumnName"] = dataTable.Rows[i]["COLUMN_NAME"];
					string text = dataTable.Rows[i]["TYPE_NAME"].ToString().Trim();
					dataRow["TypeName"] = ((text == "int identity") ? "int" : text);
					dataRow["Length"] = dataTable.Rows[i]["LENGTH"];
					dataRow["Preci"] = dataTable.Rows[i]["PRECISION"];
					dataRow["Scale"] = dataTable.Rows[i]["SCALE"];
					dataRow["IsIdentity"] = ((text == "int identity") ? "√" : "");
					dataRow["isPK"] = "";
					dataRow["cisNull"] = ((dataTable.Rows[i]["NULLABLE"].ToString().Trim() == "1") ? "√" : "");
					dataRow["defaultVal"] = dataTable.Rows[i]["COLUMN_DEF"].ToString().Replace("(", "").Replace(")", "");
					dataRow["deText"] = dataTable.Rows[i]["REMARKS"];
					dataTable2.Rows.Add(dataRow);
				}
				return CodeCommon.GetColumnInfos(dataTable2);
			}
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
				dataRow2["defaultVal"] = dataRow["defaultVal"].ToString().Replace("(", "").Replace(")", "");
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
			stringBuilder.Append("select * from [" + TableName + "]");
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
				SqlCommand sqlCommand = this.OpenDB(DbName);
				sqlCommand.CommandText = string.Concat(new string[]
				{
					"EXEC sp_rename '",
					OldName,
					"', '",
					NewName,
					"'"
				});
				sqlCommand.ExecuteNonQuery();
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
				SqlCommand sqlCommand = this.OpenDB(DbName);
				sqlCommand.CommandText = "DROP TABLE [" + TableName + "]";
				sqlCommand.ExecuteNonQuery();
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
			catch (Exception ex)
			{
				string arg_3D_0 = ex.Message;
				result = "";
			}
			return result;
		}
	}
}
