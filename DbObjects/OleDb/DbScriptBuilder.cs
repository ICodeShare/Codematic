using Maticsoft.CodeHelper;
using Maticsoft.IDBO;
using Maticsoft.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Windows.Forms;
namespace Maticsoft.DbObjects.OleDb
{
	public class DbScriptBuilder : IDbScriptBuilder
	{
        private Dictionary<string, int> dtCollection;
		protected string _key = "ID";
		protected string _keyType = "int";
		private string _dbconnectStr;
		private string _dbname;
		private string _tablename;
		private string _procprefix;
		private string _projectname;
		private List<ColumnInfo> _fieldlist;
		private List<ColumnInfo> _keys;
		private DbObject dbobj = new DbObject();
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
		public string DbName
		{
			get
			{
				return this._dbname;
			}
			set
			{
				this._dbname = value;
			}
		}
		public string TableName
		{
			get
			{
				return this._tablename;
			}
			set
			{
				this._tablename = value;
			}
		}
		public string ProcPrefix
		{
			get
			{
				return this._procprefix;
			}
			set
			{
				this._procprefix = value;
			}
		}
		public string ProjectName
		{
			get
			{
				return this._projectname;
			}
			set
			{
				this._projectname = value;
			}
		}
		public List<ColumnInfo> Fieldlist
		{
			get
			{
				return this._fieldlist;
			}
			set
			{
				this._fieldlist = value;
			}
		}
		public List<ColumnInfo> Keys
		{
			get
			{
				return this._keys;
			}
			set
			{
				this._keys = value;
			}
		}
		public string Fields
		{
			get
			{
				StringPlus stringPlus = new StringPlus();
				foreach (ColumnInfo current in this.Fieldlist)
				{
					stringPlus.Append("'" + current.ColumnName + "',");
				}
				stringPlus.DelLastComma();
				return stringPlus.Value;
			}
		}
		public string Fieldstrlist
		{
			get
			{
				StringPlus stringPlus = new StringPlus();
				foreach (ColumnInfo current in this.Fieldlist)
				{
					stringPlus.Append(current.ColumnName + ",");
				}
				stringPlus.DelLastComma();
				return stringPlus.Value;
			}
		}
		public bool IsHasIdentity
		{
			get
			{
				bool result = false;
				if (this._keys.Count > 0)
				{
					foreach (ColumnInfo current in this._keys)
					{
						if (current.IsIdentity)
						{
							result = true;
						}
					}
				}
				return result;
			}
		}
		public string GetInParameter(List<ColumnInfo> keys, bool output)
		{
			StringPlus stringPlus = new StringPlus();
			foreach (ColumnInfo current in this.Keys)
			{
				string columnName = current.ColumnName;
				string typeName = current.TypeName;
				bool isIdentity = current.IsIdentity;
				bool arg_3C_0 = current.IsPrimaryKey;
				string length = current.Length;
				string precision = current.Precision;
				string scale = current.Scale;
				string key;
				if ((key = typeName.ToLower()) == null)
				{
					goto IL_1A2;
				}
				if (dtCollection == null)
				{
					dtCollection = new Dictionary<string, int>(6)
					{

						{
							"decimal",
							0
						},

						{
							"numeric",
							1
						},

						{
							"varchar",
							2
						},

						{
							"nvarchar",
							3
						},

						{
							"char",
							4
						},

						{
							"nchar",
							5
						}
					};
				}
				int num;
				if (!dtCollection.TryGetValue(key, out num))
				{
					goto IL_1A2;
				}
				switch (num)
				{
				case 0:
				case 1:
					stringPlus.AppendLine(string.Concat(new string[]
					{
						"@",
						columnName,
						" ",
						typeName,
						"(",
						precision,
						",",
						scale,
						")"
					}));
					break;
				case 2:
				case 3:
				case 4:
				case 5:
					stringPlus.AppendLine(string.Concat(new string[]
					{
						"@",
						columnName,
						" ",
						typeName,
						"(",
						length,
						")"
					}));
					break;
				default:
					goto IL_1A2;
				}
				IL_1BA:
				if (isIdentity && output)
				{
					stringPlus.AppendLine(" output,");
					continue;
				}
				stringPlus.AppendLine(",");
				continue;
				IL_1A2:
				stringPlus.AppendLine("@" + columnName + " " + typeName);
				goto IL_1BA;
			}
			stringPlus.DelLastComma();
			return stringPlus.Value;
		}
		public string GetWhereExpression(List<ColumnInfo> keys)
		{
			StringPlus stringPlus = new StringPlus();
			foreach (ColumnInfo current in keys)
			{
				stringPlus.Append(current.ColumnName + "=@" + current.ColumnName + " and ");
			}
			stringPlus.DelLastChar("and");
			return stringPlus.Value;
		}
		public string CreateDBTabScript(string dbname)
		{
			this.dbobj.DbConnectStr = this.DbConnectStr;
			StringPlus stringPlus = new StringPlus();
			List<string> tables = this.dbobj.GetTables(dbname);
			if (tables.Count > 0)
			{
				foreach (string current in tables)
				{
					stringPlus.AppendLine(this.CreateTabScript(dbname, current));
				}
			}
			return stringPlus.Value;
		}
		public string CreateTabScript(string dbname, string tablename)
		{
			this.dbobj.DbConnectStr = this._dbconnectStr;
			List<ColumnInfo> columnInfoList = this.dbobj.GetColumnInfoList(dbname, tablename);
			DataTable columnInfoDt = CodeCommon.GetColumnInfoDt(columnInfoList);
			StringPlus stringPlus = new StringPlus();
			string a = "";
			new StringPlus();
			Hashtable hashtable = new Hashtable();
			StringPlus stringPlus2 = new StringPlus();
			stringPlus.AppendLine("");
			stringPlus.AppendLine("CREATE TABLE [" + tablename + "] (");
			if (columnInfoDt != null)
			{
				foreach (DataRow dataRow in columnInfoDt.Rows)
				{
					string text = dataRow["ColumnName"].ToString();
					string text2 = dataRow["TypeName"].ToString();
					string str = dataRow["Length"].ToString();
					string text3 = dataRow["Preci"].ToString();
					string text4 = dataRow["Scale"].ToString();
					dataRow["isPK"].ToString();
					string a2 = dataRow["cisNull"].ToString();
					string text5 = dataRow["defaultVal"].ToString();
					stringPlus.Append(string.Concat(new string[]
					{
						"[",
						text,
						"] [",
						text2,
						"] "
					}));
					string a3;
					if ((a3 = text2.Trim()) != null)
					{
						if (!(a3 == "CHAR") && !(a3 == "VARCHAR2") && !(a3 == "NCHAR") && !(a3 == "NVARCHAR2"))
						{
							if (a3 == "NUMBER")
							{
								stringPlus.Append(string.Concat(new string[]
								{
									" (",
									text3,
									",",
									text4,
									")"
								}));
							}
						}
						else
						{
							stringPlus.Append(" (" + str + ")");
						}
					}
					if (a2 == "√")
					{
						stringPlus.Append(" NULL");
					}
					else
					{
						stringPlus.Append(" NOT NULL");
					}
					if (text5 != "")
					{
						stringPlus.Append(" DEFAULT " + text5);
					}
					stringPlus.AppendLine(",");
					hashtable.Add(text, text2);
					stringPlus2.Append("[" + text + "],");
					if (a == "")
					{
						a = text;
					}
				}
			}
			stringPlus.DelLastComma();
			stringPlus2.DelLastComma();
			stringPlus.AppendLine(")");
			stringPlus.AppendLine("");
			DataTable tabData = this.dbobj.GetTabData(dbname, tablename);
			if (tabData != null)
			{
				foreach (DataRow dataRow2 in tabData.Rows)
				{
					StringPlus stringPlus3 = new StringPlus();
					StringPlus stringPlus4 = new StringPlus();
					string[] array = stringPlus2.Value.Split(new char[]
					{
						','
					});
					string[] array2 = array;
					int i = 0;
					while (i < array2.Length)
					{
						string text6 = array2[i];
						string text7 = text6.Substring(1, text6.Length - 2);
						string text8 = "";
						foreach (DictionaryEntry dictionaryEntry in hashtable)
						{
							if (dictionaryEntry.Key.ToString() == text7)
							{
								text8 = dictionaryEntry.Value.ToString();
							}
						}
						string a4;
						if ((a4 = text8) == null)
						{
							goto IL_444;
						}
						string text9;
						if (!(a4 == "BLOB"))
						{
							if (!(a4 == "bit"))
							{
								goto IL_444;
							}
							text9 = ((dataRow2[text7].ToString().ToLower() == "true") ? "1" : "0");
						}
						else
						{
							byte[] bytes = (byte[])dataRow2[text7];
							text9 = CodeCommon.ToHexString(bytes);
						}
						IL_459:
						if (text9 != "")
						{
							if (CodeCommon.IsAddMark(text8))
							{
								stringPlus4.Append("'" + text9.Replace("'", "''") + "',");
							}
							else
							{
								stringPlus4.Append(text9 + ",");
							}
							stringPlus3.Append("[" + text7 + "],");
						}
						i++;
						continue;
						IL_444:
						text9 = dataRow2[text7].ToString().Trim();
						goto IL_459;
					}
					stringPlus3.DelLastComma();
					stringPlus4.DelLastComma();
					stringPlus.Append("INSERT [" + tablename + "] (");
					stringPlus.Append(stringPlus3.Value);
					stringPlus.Append(") VALUES ( ");
					stringPlus.Append(stringPlus4.Value);
					stringPlus.AppendLine(")");
				}
			}
			return stringPlus.Value;
		}
		public string CreateTabScriptBySQL(string dbname, string strSQL)
		{
			this.dbobj.DbConnectStr = this._dbconnectStr;
			string text = "TableName";
			StringPlus stringPlus = new StringPlus();
			int num = strSQL.IndexOf(" from ");
			if (num > 0)
			{
				string text2 = strSQL.Substring(num + 5).Trim();
				int num2 = text2.IndexOf(" ");
				if (text2.Length > 0)
				{
					if (num2 > 0)
					{
						text = text2.Substring(0, num2).Trim();
					}
					else
					{
						text = text2.Substring(0).Trim();
					}
				}
			}
			text = text.Replace("[", "").Replace("]", "");
			DataTable tabDataBySQL = this.dbobj.GetTabDataBySQL(dbname, strSQL);
			if (tabDataBySQL != null)
			{
				DataColumnCollection columns = tabDataBySQL.Columns;
				foreach (DataRow dataRow in tabDataBySQL.Rows)
				{
					StringPlus stringPlus2 = new StringPlus();
					StringPlus stringPlus3 = new StringPlus();
					foreach (DataColumn dataColumn in columns)
					{
						string columnName = dataColumn.ColumnName;
						string name = dataColumn.DataType.Name;
						bool arg_11D_0 = dataColumn.AutoIncrement;
						string a;
						if ((a = name.ToLower()) == null)
						{
							goto IL_1C6;
						}
						string text3;
						if (!(a == "binary") && !(a == "byte[]") && !(a == "blob"))
						{
							if (!(a == "bit") && !(a == "boolean"))
							{
								goto IL_1C6;
							}
							text3 = ((dataRow[columnName].ToString().ToLower() == "true") ? "1" : "0");
						}
						else
						{
							byte[] bytes = (byte[])dataRow[columnName];
							text3 = CodeCommon.ToHexString(bytes);
						}
						IL_1DB:
						if (text3 != "")
						{
							if (CodeCommon.IsAddMark(name))
							{
								stringPlus3.Append("'" + text3.Replace("'", "''") + "',");
							}
							else
							{
								stringPlus3.Append(text3 + ",");
							}
							stringPlus2.Append(columnName + ",");
							continue;
						}
						continue;
						IL_1C6:
						text3 = dataRow[columnName].ToString().Trim();
						goto IL_1DB;
					}
					stringPlus2.DelLastComma();
					stringPlus3.DelLastComma();
					stringPlus.Append("INSERT " + text + " (");
					stringPlus.Append(stringPlus2.Value);
					stringPlus.Append(") VALUES ( ");
					stringPlus.Append(stringPlus3.Value);
					stringPlus.AppendLine(")");
				}
			}
			return stringPlus.Value;
		}
		public void CreateTabScript(string dbname, string tablename, string filename, ProgressBar progressBar)
		{
			StreamWriter streamWriter = new StreamWriter(filename, true, Encoding.Default);
			this.dbobj.DbConnectStr = this._dbconnectStr;
			List<ColumnInfo> columnInfoList = this.dbobj.GetColumnInfoList(dbname, tablename);
			StringPlus stringPlus = new StringPlus();
			string text = "";
			new StringPlus();
			Hashtable hashtable = new Hashtable();
			StringPlus stringPlus2 = new StringPlus();
			stringPlus.AppendLine("");
			stringPlus.AppendLine("CREATE TABLE [" + tablename + "] (");
			if (columnInfoList != null && columnInfoList.Count > 0)
			{
				foreach (ColumnInfo current in columnInfoList)
				{
					string columnName = current.ColumnName;
					string typeName = current.TypeName;
					bool arg_B0_0 = current.IsIdentity;
					string length = current.Length;
					string precision = current.Precision;
					string scale = current.Scale;
					bool arg_D3_0 = current.IsPrimaryKey;
					bool nullable = current.Nullable;
					string defaultVal = current.DefaultVal;
					stringPlus.Append(string.Concat(new string[]
					{
						"[",
						columnName,
						"] [",
						typeName,
						"] "
					}));
					string a;
					if ((a = typeName.Trim()) != null)
					{
						if (!(a == "char") && !(a == "varchar") && !(a == "nchar") && !(a == "nvarchar"))
						{
							if (a == "float")
							{
								stringPlus.Append(string.Concat(new string[]
								{
									" (",
									precision,
									",",
									scale,
									")"
								}));
							}
						}
						else
						{
							stringPlus.Append(" (" + length + ")");
						}
					}
					if (nullable)
					{
						stringPlus.Append(" NULL");
					}
					else
					{
						stringPlus.Append(" NOT NULL");
					}
					if (defaultVal != "")
					{
						stringPlus.Append(" DEFAULT " + defaultVal);
					}
					stringPlus.AppendLine(",");
					hashtable.Add(columnName, typeName);
					stringPlus2.Append("[" + columnName + "],");
					if (text == "")
					{
						text = columnName;
					}
				}
			}
			stringPlus.DelLastComma();
			stringPlus2.DelLastComma();
			stringPlus.AppendLine(")");
			stringPlus.AppendLine("");
			if (text != "")
			{
				stringPlus.Append(string.Concat(new string[]
				{
					"ALTER TABLE [",
					tablename,
					"] WITH NOCHECK ADD  CONSTRAINT [PK_",
					tablename,
					"] PRIMARY KEY  NONCLUSTERED ( [",
					text,
					"] )"
				}));
			}
			streamWriter.Write(stringPlus.Value);
			DataTable tabData = this.dbobj.GetTabData(dbname, tablename);
			if (tabData != null)
			{
				int num = 0;
				progressBar.Maximum = tabData.Rows.Count;
				foreach (DataRow dataRow in tabData.Rows)
				{
					progressBar.Value = num;
					num++;
					StringPlus stringPlus3 = new StringPlus();
					StringPlus stringPlus4 = new StringPlus();
					StringPlus stringPlus5 = new StringPlus();
					string[] array = stringPlus2.Value.Split(new char[]
					{
						','
					});
					string[] array2 = array;
					int i = 0;
					while (i < array2.Length)
					{
						string text2 = array2[i];
						string text3 = text2.Substring(1, text2.Length - 2);
						string text4 = "";
						foreach (DictionaryEntry dictionaryEntry in hashtable)
						{
							if (dictionaryEntry.Key.ToString() == text3)
							{
								text4 = dictionaryEntry.Value.ToString();
							}
						}
						string a2;
						if ((a2 = text4) == null)
						{
							goto IL_48F;
						}
						string text5;
						if (!(a2 == "binary"))
						{
							if (!(a2 == "bit") && !(a2 == "bool"))
							{
								goto IL_48F;
							}
							text5 = ((dataRow[text3].ToString().ToLower() == "true") ? "1" : "0");
						}
						else
						{
							byte[] bytes = (byte[])dataRow[text3];
							text5 = CodeCommon.ToHexString(bytes);
						}
						IL_4A4:
						if (text5 != "")
						{
							if (CodeCommon.IsAddMark(text4))
							{
								stringPlus5.Append("'" + text5.Replace("'", "''") + "',");
							}
							else
							{
								stringPlus5.Append(text5 + ",");
							}
							stringPlus4.Append("[" + text3 + "],");
						}
						i++;
						continue;
						IL_48F:
						text5 = dataRow[text3].ToString().Trim();
						goto IL_4A4;
					}
					stringPlus4.DelLastComma();
					stringPlus5.DelLastComma();
					stringPlus3.Append("INSERT [" + tablename + "] (");
					stringPlus3.Append(stringPlus4.Value);
					stringPlus3.Append(") VALUES ( ");
					stringPlus3.Append(stringPlus5.Value);
					stringPlus3.AppendLine(")");
					streamWriter.Write(stringPlus3.Value);
				}
			}
			streamWriter.Flush();
			streamWriter.Close();
		}
		public string GetPROCCode(string dbname)
		{
			return "";
		}
		public string GetPROCCode(string dbname, string tablename)
		{
			return "";
		}
		public string GetPROCCode(bool Maxid, bool Ishas, bool Add, bool Update, bool Delete, bool GetModel, bool List)
		{
			return "";
		}
		public string CreatPROCGetMaxID()
		{
			return "";
		}
		public string CreatPROCIsHas()
		{
			return "";
		}
		public string CreatPROCADD()
		{
			return "";
		}
		public string CreatPROCUpdate()
		{
			return "";
		}
		public string CreatPROCDelete()
		{
			return "";
		}
		public string CreatPROCGetModel()
		{
			return "";
		}
		public string CreatPROCGetList()
		{
			return "";
		}
		public string GetSQLSelect(string dbname, string tablename)
		{
			this.dbobj.DbConnectStr = this._dbconnectStr;
			List<ColumnInfo> columnList = this.dbobj.GetColumnList(dbname, tablename);
			this.DbName = dbname;
			this.TableName = tablename;
			StringPlus stringPlus = new StringPlus();
			stringPlus.Append("select ");
			if (columnList != null && columnList.Count > 0)
			{
				foreach (ColumnInfo current in columnList)
				{
					string columnName = current.ColumnName;
					stringPlus.Append("[" + columnName + "],");
				}
				stringPlus.DelLastComma();
			}
			stringPlus.Append(" from " + tablename);
			return stringPlus.Value;
		}
		public string GetSQLUpdate(string dbname, string tablename)
		{
			this.dbobj.DbConnectStr = this._dbconnectStr;
			List<ColumnInfo> columnList = this.dbobj.GetColumnList(dbname, tablename);
			this.DbName = dbname;
			this.TableName = tablename;
			StringPlus stringPlus = new StringPlus();
			stringPlus.AppendLine("update " + tablename + " set ");
			if (columnList != null && columnList.Count > 0)
			{
				foreach (ColumnInfo current in columnList)
				{
					string columnName = current.ColumnName;
					stringPlus.AppendLine(string.Concat(new string[]
					{
						"[",
						columnName,
						"] = <",
						columnName,
						">,"
					}));
				}
				stringPlus.DelLastComma();
			}
			stringPlus.Append(" where <搜索条件>");
			return stringPlus.Value;
		}
		public string GetSQLDelete(string dbname, string tablename)
		{
			this.dbobj.DbConnectStr = this._dbconnectStr;
			this.DbName = dbname;
			this.TableName = tablename;
			StringPlus stringPlus = new StringPlus();
			stringPlus.AppendLine("delete from " + tablename);
			stringPlus.Append(" where  <搜索条件>");
			return stringPlus.Value;
		}
		public string GetSQLInsert(string dbname, string tablename)
		{
			this.dbobj.DbConnectStr = this._dbconnectStr;
			List<ColumnInfo> columnList = this.dbobj.GetColumnList(dbname, tablename);
			this.DbName = dbname;
			this.TableName = tablename;
			StringPlus stringPlus = new StringPlus();
			StringPlus stringPlus2 = new StringPlus();
			stringPlus.AppendLine("INSERT INTO " + tablename + " ( ");
			if (columnList != null && columnList.Count > 0)
			{
				foreach (ColumnInfo current in columnList)
				{
					string columnName = current.ColumnName;
					string typeName = current.TypeName;
					stringPlus.AppendLine("[" + columnName + "] ,");
					if (CodeCommon.IsAddMark(typeName))
					{
						stringPlus2.Append("'" + columnName + "',");
					}
					else
					{
						stringPlus2.Append(columnName + ",");
					}
				}
				stringPlus.DelLastComma();
				stringPlus2.DelLastComma();
			}
			stringPlus.Append(") VALUES (" + stringPlus2.Value + ")");
			return stringPlus.Value;
		}
	}
}
