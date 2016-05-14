using Maticsoft.CodeHelper;
using Maticsoft.IDBO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Text;
namespace Maticsoft.DbObjects.Oracle
{
	public class DbObject : IDbObject
	{
		private string _dbconnectStr;
		private OracleConnection connect = new OracleConnection();
		public string DbType
		{
			get
			{
				return "Oracle";
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
			this.connect = new OracleConnection();
			this._dbconnectStr = string.Concat(new string[]
			{
				"Data Source=",
				server,
				"; user id=",
				User,
				";password=",
				Pass
			});
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
			return new OracleCommand(SQLString, this.connect)
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
				OracleDataAdapter oracleDataAdapter = new OracleDataAdapter(SQLString, this.connect);
				oracleDataAdapter.Fill(dataSet, "ds");
			}
			catch (OracleException ex)
			{
				throw new Exception(ex.Message);
			}
			return dataSet;
		}
		public OracleDataReader ExecuteReader(string strSQL)
		{
			OracleDataReader result;
			try
			{
				this.OpenDB();
				OracleCommand oracleCommand = new OracleCommand(strSQL, this.connect);
				OracleDataReader oracleDataReader = oracleCommand.ExecuteReader();
				result = oracleDataReader;
			}
			catch (OracleException ex)
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
				OracleCommand oracleCommand = new OracleCommand(SQLString, this.connect);
				object obj = oracleCommand.ExecuteScalar();
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
		public List<string> GetDBList()
		{
			return null;
		}
		public List<string> GetTables(string DbName)
		{
			string strSQL = "select TNAME name from tab where TABTYPE='TABLE'";
			List<string> list = new List<string>();
			OracleDataReader oracleDataReader = this.ExecuteReader(strSQL);
			while (oracleDataReader.Read())
			{
				list.Add(oracleDataReader.GetString(0));
			}
			oracleDataReader.Close();
			return list;
		}
		public DataTable GetVIEWs(string DbName)
		{
			string sQLString = "select TNAME name from tab where TABTYPE='VIEW'";
			return this.Query("", sQLString).Tables[0];
		}
		public List<string> GetTableViews(string DbName)
		{
			string strSQL = "select TNAME name from tab where TABTYPE='TABLE' or TABTYPE='VIEW'";
			List<string> list = new List<string>();
			OracleDataReader oracleDataReader = this.ExecuteReader(strSQL);
			while (oracleDataReader.Read())
			{
				list.Add(oracleDataReader.GetString(0));
			}
			oracleDataReader.Close();
			return list;
		}
		public DataTable GetTabViews(string DbName)
		{
			string sQLString = "select TNAME name,TABTYPE type from tab where TABTYPE='TABLE' or TABTYPE='VIEW'";
			return this.Query("", sQLString).Tables[0];
		}
		public List<string> GetProcs(string DbName)
		{
			string sQLString = "SELECT * FROM ALL_SOURCE  where TYPE='PROCEDURE'  ";
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
			string strSQL = "select TNAME name,'dbo' cuser,TABTYPE type,'' dates from tab where TABTYPE='TABLE'";
			List<TableInfo> list = new List<TableInfo>();
			OracleDataReader oracleDataReader = this.ExecuteReader(strSQL);
			while (oracleDataReader.Read())
			{
				list.Add(new TableInfo
				{
					TabName = oracleDataReader.GetString(0),
					TabDate = oracleDataReader.GetValue(3).ToString(),
					TabType = oracleDataReader.GetString(2),
					TabUser = oracleDataReader.GetString(1)
				});
			}
			oracleDataReader.Close();
			return list;
		}
		public DataTable GetTablesExProperty(string DbName)
		{
			DataTable result;
			try
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append(" select t.TNAME objname,c.TABLE_NAME name,c.comments value");
				stringBuilder.Append(" from tab t,user_tab_comments c  ");
				stringBuilder.Append(" where t.TABTYPE='TABLE' and t.TNAME =c.TABLE_NAME ");
				result = this.Query(DbName, stringBuilder.ToString()).Tables[0];
			}
			catch
			{
				result = new DataTable();
			}
			return result;
		}
		public List<TableInfo> GetVIEWsInfo(string DbName)
		{
			string strSQL = "select TNAME name,'dbo' cuser,TABTYPE type,'' dates from tab where TABTYPE='VIEW'";
			List<TableInfo> list = new List<TableInfo>();
			OracleDataReader oracleDataReader = this.ExecuteReader(strSQL);
			while (oracleDataReader.Read())
			{
				list.Add(new TableInfo
				{
					TabName = oracleDataReader.GetString(0),
					TabDate = oracleDataReader.GetValue(3).ToString(),
					TabType = oracleDataReader.GetString(2),
					TabUser = oracleDataReader.GetString(1)
				});
			}
			oracleDataReader.Close();
			return list;
		}
		public List<TableInfo> GetTabViewsInfo(string DbName)
		{
			string strSQL = "select TNAME name,'dbo' cuser,TABTYPE type,'' dates from tab ";
			List<TableInfo> list = new List<TableInfo>();
			OracleDataReader oracleDataReader = this.ExecuteReader(strSQL);
			while (oracleDataReader.Read())
			{
				list.Add(new TableInfo
				{
					TabName = oracleDataReader.GetString(0),
					TabDate = oracleDataReader.GetValue(3).ToString(),
					TabType = oracleDataReader.GetString(2),
					TabUser = oracleDataReader.GetString(1)
				});
			}
			oracleDataReader.Close();
			return list;
		}
		public List<TableInfo> GetProcInfo(string DbName)
		{
			string strSQL = "SELECT * FROM ALL_SOURCE  where TYPE='PROCEDURE'  ";
			List<TableInfo> list = new List<TableInfo>();
			OracleDataReader oracleDataReader = this.ExecuteReader(strSQL);
			while (oracleDataReader.Read())
			{
				list.Add(new TableInfo
				{
					TabName = oracleDataReader.GetString(0),
					TabDate = oracleDataReader.GetValue(3).ToString(),
					TabType = oracleDataReader.GetString(2),
					TabUser = oracleDataReader.GetString(1)
				});
			}
			oracleDataReader.Close();
			return list;
		}
		public string GetObjectInfo(string DbName, string objName)
		{
			return "";
		}
		public List<ColumnInfo> GetColumnList(string DbName, string TableName)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select ");
			stringBuilder.Append("COLUMN_ID as colorder,");
			stringBuilder.Append("COLUMN_NAME as ColumnName,");
			stringBuilder.Append("DATA_TYPE as TypeName, ");
			stringBuilder.Append("DECODE(DATA_TYPE, 'NUMBER',DATA_PRECISION, DATA_LENGTH) as Length ");
			stringBuilder.Append(" from USER_TAB_COLUMNS ");
			stringBuilder.Append(" where TABLE_NAME='" + TableName + "'");
			stringBuilder.Append(" order by COLUMN_ID");
			List<ColumnInfo> list = new List<ColumnInfo>();
			OracleDataReader oracleDataReader = this.ExecuteReader(stringBuilder.ToString());
			while (oracleDataReader.Read())
			{
				list.Add(new ColumnInfo
				{
					ColumnOrder = oracleDataReader.GetValue(0).ToString(),
					ColumnName = oracleDataReader.GetString(1),
					TypeName = oracleDataReader.GetString(2),
					Length = oracleDataReader.GetValue(3).ToString(),
					Precision = "",
					Scale = "",
					IsPrimaryKey = false,
					Nullable = false,
					DefaultVal = "",
					IsIdentity = false,
					Description = ""
				});
			}
			oracleDataReader.Close();
			return list;
		}
		public List<ColumnInfo> GetColumnInfoList(string DbName, string TableName)
		{
			List<ColumnInfo> result;
			try
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("select ");
				stringBuilder.Append("COL.COLUMN_ID as colorder,");
				stringBuilder.Append("COL.COLUMN_NAME as ColumnName,");
				stringBuilder.Append("COL.DATA_TYPE as TypeName, ");
				stringBuilder.Append("DECODE(COL.DATA_TYPE, 'NUMBER',COL.DATA_PRECISION, COL.DATA_LENGTH) as Length,");
				stringBuilder.Append("COL.DATA_PRECISION as Preci,");
				stringBuilder.Append("COL.DATA_SCALE as Scale,");
				stringBuilder.Append("'' as IsIdentity,");
				stringBuilder.Append("case when PKCOL.COLUMN_POSITION >0  then '√'else '' end as isPK,");
				stringBuilder.Append("case when COL.NULLABLE='Y'  then '√'else '' end as cisNull, ");
				stringBuilder.Append("COL.DATA_DEFAULT as defaultVal, ");
				stringBuilder.Append("CCOM.COMMENTS as deText,");
				stringBuilder.Append("COL.NUM_DISTINCT as NUM_DISTINCT ");
				stringBuilder.Append(" FROM USER_TAB_COLUMNS COL,USER_COL_COMMENTS CCOM, ");
				stringBuilder.Append(" ( SELECT AA.TABLE_NAME, AA.INDEX_NAME, AA.COLUMN_NAME, AA.COLUMN_POSITION");
				stringBuilder.Append(" FROM USER_IND_COLUMNS AA, USER_CONSTRAINTS BB");
				stringBuilder.Append(" WHERE BB.CONSTRAINT_TYPE = 'P'");
				stringBuilder.Append(" AND AA.TABLE_NAME = BB.TABLE_NAME");
				stringBuilder.Append(" AND AA.INDEX_NAME = BB.CONSTRAINT_NAME");
				stringBuilder.Append(" AND AA.TABLE_NAME IN ('" + TableName + "')");
				stringBuilder.Append(") PKCOL");
				stringBuilder.Append(" WHERE COL.TABLE_NAME = CCOM.TABLE_NAME");
				stringBuilder.Append(" AND COL.COLUMN_NAME = CCOM.COLUMN_NAME");
				stringBuilder.Append(" AND COL.TABLE_NAME ='" + TableName + "'");
				stringBuilder.Append(" AND COL.COLUMN_NAME = PKCOL.COLUMN_NAME(+)");
				stringBuilder.Append(" AND COL.TABLE_NAME = PKCOL.TABLE_NAME(+)");
				stringBuilder.Append(" ORDER BY COL.COLUMN_ID ");
				Hashtable foreignKey = this.GetForeignKey(DbName, TableName);
				List<ColumnInfo> list = new List<ColumnInfo>();
				OracleDataReader oracleDataReader = this.ExecuteReader(stringBuilder.ToString());
				while (oracleDataReader.Read())
				{
					ColumnInfo columnInfo = new ColumnInfo();
					columnInfo.ColumnOrder = oracleDataReader.GetValue(0).ToString();
					columnInfo.ColumnName = oracleDataReader.GetValue(1).ToString();
					columnInfo.TypeName = oracleDataReader.GetValue(2).ToString();
					columnInfo.Length = oracleDataReader.GetValue(3).ToString();
					columnInfo.Precision = oracleDataReader.GetValue(4).ToString();
					columnInfo.Scale = oracleDataReader.GetValue(5).ToString();
					columnInfo.IsIdentity = (oracleDataReader.GetValue(6).ToString() == "√");
					columnInfo.IsPrimaryKey = (oracleDataReader.GetValue(7).ToString() == "√");
					columnInfo.IsForeignKey = false;
					if (foreignKey[TableName] != null && foreignKey[TableName].ToString() == columnInfo.ColumnName)
					{
						columnInfo.IsForeignKey = true;
					}
					columnInfo.Nullable = (oracleDataReader.GetValue(8).ToString() == "√");
					columnInfo.DefaultVal = oracleDataReader.GetValue(9).ToString();
					columnInfo.Description = oracleDataReader.GetValue(10).ToString();
					list.Add(columnInfo);
				}
				oracleDataReader.Close();
				result = list;
			}
			catch (Exception ex)
			{
				throw new Exception("获取列数据失败" + ex.Message);
			}
			return result;
		}
		public DataTable GetKeyName(string DbName, string TableName)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("select ");
			stringBuilder.Append("COL.COLUMN_ID as colorder,");
			stringBuilder.Append("COL.COLUMN_NAME as ColumnName,");
			stringBuilder.Append("COL.DATA_TYPE as TypeName, ");
			stringBuilder.Append("COL.DATA_LENGTH as Length,");
			stringBuilder.Append("COL.DATA_PRECISION as Preci,");
			stringBuilder.Append("COL.DATA_SCALE as Scale,");
			stringBuilder.Append("'' as IsIdentity,");
			stringBuilder.Append("case when PKCOL.COLUMN_POSITION >0  then '√'else '' end as isPK,");
			stringBuilder.Append("case when COL.NULLABLE='Y'  then '√'else '' end as cisNull, ");
			stringBuilder.Append("COL.DATA_DEFAULT as defaultVal, ");
			stringBuilder.Append("CCOM.COMMENTS as deText,");
			stringBuilder.Append("COL.NUM_DISTINCT as NUM_DISTINCT ");
			stringBuilder.Append(" FROM USER_TAB_COLUMNS COL,USER_COL_COMMENTS CCOM, ");
			stringBuilder.Append(" ( SELECT AA.TABLE_NAME, AA.INDEX_NAME, AA.COLUMN_NAME, AA.COLUMN_POSITION");
			stringBuilder.Append(" FROM USER_IND_COLUMNS AA, USER_CONSTRAINTS BB");
			stringBuilder.Append(" WHERE BB.CONSTRAINT_TYPE = 'P'");
			stringBuilder.Append(" AND AA.TABLE_NAME = BB.TABLE_NAME");
			stringBuilder.Append(" AND AA.INDEX_NAME = BB.CONSTRAINT_NAME");
			stringBuilder.Append(" AND AA.TABLE_NAME IN ('" + TableName + "')");
			stringBuilder.Append(") PKCOL");
			stringBuilder.Append(" WHERE COL.TABLE_NAME = CCOM.TABLE_NAME");
			stringBuilder.Append(" AND PKCOL.COLUMN_POSITION >0");
			stringBuilder.Append(" AND COL.COLUMN_NAME = CCOM.COLUMN_NAME");
			stringBuilder.Append(" AND COL.TABLE_NAME ='" + TableName + "'");
			stringBuilder.Append(" AND COL.COLUMN_NAME = PKCOL.COLUMN_NAME(+)");
			stringBuilder.Append(" AND COL.TABLE_NAME = PKCOL.TABLE_NAME(+)");
			stringBuilder.Append(" ORDER BY COL.COLUMN_ID ");
			return this.Query("", stringBuilder.ToString()).Tables[0];
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
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("select distinct(col.column_name),r.table_name,r.column_name  ");
				stringBuilder.Append("from user_constraints con,user_cons_columns col,  ");
				stringBuilder.Append("(select t2.table_name,t2.column_name,t1.r_constraint_name  ");
				stringBuilder.Append(" from user_constraints t1,user_cons_columns t2  ");
				stringBuilder.Append(" where t1.r_constraint_name=t2.constraint_name  ");
				stringBuilder.Append(" and t1.table_name='" + TableName + "' ");
				stringBuilder.Append(" ) r  ");
				stringBuilder.Append("where con.constraint_name=col.constraint_name  ");
				stringBuilder.Append("and con.r_constraint_name=r.r_constraint_name  ");
				stringBuilder.Append("and con.table_name='" + TableName + "'  ");
				OracleDataReader oracleDataReader = this.ExecuteReader(stringBuilder.ToString());
				while (oracleDataReader.Read())
				{
					string value = oracleDataReader.GetValue(0).ToString();
					string key = oracleDataReader.GetValue(1).ToString();
					if (!hashtable.Contains(key))
					{
						hashtable.Add(key, value);
					}
				}
				oracleDataReader.Close();
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
			stringBuilder.Append("select * from " + TableName);
			return this.Query("", stringBuilder.ToString()).Tables[0];
		}
		public DataTable GetTabDataBySQL(string DbName, string strSQL)
		{
			return this.Query("", strSQL).Tables[0];
		}
		public bool RenameTable(string DbName, string OldName, string NewName)
		{
			bool result;
			try
			{
				string sQLString = "RENAME " + OldName + " TO " + NewName;
				this.ExecuteSql(DbName, sQLString);
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
				string sQLString = "DROP TABLE " + TableName;
				this.ExecuteSql(DbName, sQLString);
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
