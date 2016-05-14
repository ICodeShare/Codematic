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
namespace Maticsoft.DbObjects.MySQL
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
		public string GetInParameter(List<ColumnInfo> fieldlist, bool output)
		{
			StringPlus stringPlus = new StringPlus();
			foreach (ColumnInfo current in fieldlist)
			{
				string columnName = current.ColumnName;
				string typeName = current.TypeName;
				bool isIdentity = current.IsIdentity;
				bool arg_37_0 = current.IsPrimaryKey;
				string length = current.Length;
				string precision = current.Precision;
				string scale = current.Scale;
				string key;
				if ((key = typeName.ToLower()) == null)
				{
					goto IL_1C8;
				}
				if (dtCollection == null)
				{
					dtCollection = new Dictionary<string, int>(8)
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
							"char",
							2
						},

						{
							"varchar",
							3
						},

						{
							"varbinary",
							4
						},

						{
							"binary",
							5
						},

						{
							"nchar",
							6
						},

						{
							"nvarchar",
							7
						}
					};
				}
				int num;
				if (!dtCollection.TryGetValue(key, out num))
				{
					goto IL_1C8;
				}
				switch (num)
				{
				case 0:
				case 1:
					stringPlus.Append(string.Concat(new string[]
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
				case 6:
				case 7:
					stringPlus.Append(string.Concat(new string[]
					{
						"@",
						columnName,
						" ",
						typeName,
						"(",
						CodeCommon.GetDataTypeLenVal(typeName.ToLower(), length),
						")"
					}));
					break;
				default:
					goto IL_1C8;
				}
				IL_1E0:
				if (isIdentity && output)
				{
					stringPlus.AppendLine(" output,");
					continue;
				}
				stringPlus.AppendLine(",");
				continue;
				IL_1C8:
				stringPlus.Append("@" + columnName + " " + typeName);
				goto IL_1E0;
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
			new StringPlus();
			Hashtable hashtable = new Hashtable();
			StringPlus stringPlus2 = new StringPlus();
			stringPlus.AppendLine("");
			stringPlus.AppendLine(this.dbobj.GetTableScript(dbname, tablename));
			stringPlus.AppendLine(";");
			if (columnInfoDt != null)
			{
				DataRow[] array;
				if (this.Fieldlist.Count > 0)
				{
					array = columnInfoDt.Select("ColumnName in (" + this.Fields + ")", "colorder asc");
				}
				else
				{
					array = columnInfoDt.Select();
				}
				DataRow[] array2 = array;
				for (int i = 0; i < array2.Length; i++)
				{
					DataRow dataRow = array2[i];
					string text = dataRow["ColumnName"].ToString();
					string value = dataRow["TypeName"].ToString();
					string a = dataRow["IsIdentity"].ToString();
					if (!(a == "√"))
					{
						hashtable.Add(text, value);
						stringPlus2.Append(text + ",");
					}
				}
			}
			stringPlus2.DelLastComma();
			DataTable tabData = this.dbobj.GetTabData(dbname, tablename);
			if (tabData != null)
			{
				foreach (DataRow dataRow2 in tabData.Rows)
				{
					StringPlus stringPlus3 = new StringPlus();
					StringPlus stringPlus4 = new StringPlus();
					string[] array3 = stringPlus2.Value.Split(new char[]
					{
						','
					});
					string[] array4 = array3;
					int j = 0;
					while (j < array4.Length)
					{
						string text2 = array4[j];
						string text3 = text2;
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
							goto IL_287;
						}
						string text5;
						if (!(a2 == "binary"))
						{
							if (!(a2 == "bit"))
							{
								goto IL_287;
							}
							text5 = ((dataRow2[text3].ToString().ToLower() == "true") ? "1" : "0");
						}
						else
						{
							byte[] bytes = (byte[])dataRow2[text3];
							text5 = CodeCommon.ToHexString(bytes);
						}
						IL_29C:
						if (text5 != "")
						{
							if (CodeCommon.IsAddMark(text4))
							{
								stringPlus4.Append("'" + text5.Replace("'", "''") + "',");
							}
							else
							{
								stringPlus4.Append(text5 + ",");
							}
							stringPlus3.Append(text3 + ",");
						}
						j++;
						continue;
						IL_287:
						text5 = dataRow2[text3].ToString().Trim();
						goto IL_29C;
					}
					stringPlus3.DelLastComma();
					stringPlus4.DelLastComma();
					stringPlus.Append("INSERT " + tablename + " (");
					stringPlus.Append(stringPlus3.Value);
					stringPlus.Append(") VALUES ( ");
					stringPlus.Append(stringPlus4.Value);
					stringPlus.AppendLine(");");
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
			DataTable columnInfoDt = CodeCommon.GetColumnInfoDt(columnInfoList);
			StringPlus stringPlus = new StringPlus();
			new StringPlus();
			Hashtable hashtable = new Hashtable();
			StringPlus stringPlus2 = new StringPlus();
			stringPlus.AppendLine("");
			stringPlus.AppendLine(this.dbobj.GetTableScript(dbname, tablename));
			stringPlus.AppendLine(";");
			if (columnInfoDt != null)
			{
				DataRow[] array;
				if (this.Fieldlist.Count > 0)
				{
					array = columnInfoDt.Select("ColumnName in (" + this.Fields + ")", "colorder asc");
				}
				else
				{
					array = columnInfoDt.Select();
				}
				DataRow[] array2 = array;
				for (int i = 0; i < array2.Length; i++)
				{
					DataRow dataRow = array2[i];
					string text = dataRow["ColumnName"].ToString();
					string value = dataRow["TypeName"].ToString();
					string a = dataRow["IsIdentity"].ToString();
					if (!(a == "√"))
					{
						hashtable.Add(text, value);
						stringPlus2.Append(text + ",");
					}
				}
			}
			stringPlus2.DelLastComma();
			streamWriter.Write(stringPlus.Value);
			DataTable tabData = this.dbobj.GetTabData(dbname, tablename);
			if (tabData != null)
			{
				int num = 0;
				progressBar.Maximum = tabData.Rows.Count;
				foreach (DataRow dataRow2 in tabData.Rows)
				{
					num++;
					progressBar.Value = num;
					StringPlus stringPlus3 = new StringPlus();
					StringPlus stringPlus4 = new StringPlus();
					StringPlus stringPlus5 = new StringPlus();
					string[] array3 = stringPlus2.Value.Split(new char[]
					{
						','
					});
					string[] array4 = array3;
					int j = 0;
					while (j < array4.Length)
					{
						string text2 = array4[j];
						string text3 = text2;
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
							goto IL_2CF;
						}
						string text5;
						if (!(a2 == "binary"))
						{
							if (!(a2 == "bit"))
							{
								goto IL_2CF;
							}
							text5 = ((dataRow2[text3].ToString().ToLower() == "true") ? "1" : "0");
						}
						else
						{
							byte[] bytes = (byte[])dataRow2[text3];
							text5 = CodeCommon.ToHexString(bytes);
						}
						IL_2E4:
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
							stringPlus4.Append(text3 + ",");
						}
						j++;
						continue;
						IL_2CF:
						text5 = dataRow2[text3].ToString().Trim();
						goto IL_2E4;
					}
					stringPlus4.DelLastComma();
					stringPlus5.DelLastComma();
					stringPlus3.Append("INSERT " + tablename + " (");
					stringPlus3.Append(stringPlus4.Value);
					stringPlus3.Append(") VALUES ( ");
					stringPlus3.Append(stringPlus5.Value);
					stringPlus3.AppendLine(");");
					streamWriter.Write(stringPlus3.Value);
				}
			}
			streamWriter.Flush();
			streamWriter.Close();
		}
		public string GetPROCCode(string dbname)
		{
			this.dbobj.DbConnectStr = this.DbConnectStr;
			StringPlus stringPlus = new StringPlus();
			List<string> tables = this.dbobj.GetTables(dbname);
			if (tables.Count > 0)
			{
				foreach (string current in tables)
				{
					stringPlus.AppendLine(this.GetPROCCode(dbname, current));
				}
			}
			return stringPlus.Value;
		}
		public string GetPROCCode(string dbname, string tablename)
		{
			this.dbobj.DbConnectStr = this._dbconnectStr;
			this.Fieldlist = this.dbobj.GetColumnInfoList(dbname, tablename);
			DataTable keyName = this.dbobj.GetKeyName(dbname, tablename);
			this.DbName = dbname;
			this.TableName = tablename;
			this.Keys = CodeCommon.GetColumnInfos(keyName);
			foreach (ColumnInfo current in this.Keys)
			{
				this._key = current.ColumnName;
				this._keyType = current.TypeName;
				if (current.IsIdentity)
				{
					this._key = current.ColumnName;
					this._keyType = CodeCommon.DbTypeToCS(current.TypeName);
					break;
				}
			}
			return this.GetPROCCode(true, true, true, true, true, true, true);
		}
		public string GetPROCCode(bool Maxid, bool Ishas, bool Add, bool Update, bool Delete, bool GetModel, bool List)
		{
			StringPlus stringPlus = new StringPlus();
			stringPlus.AppendLine("/******************************************************************");
			stringPlus.AppendLine("* 表名：" + this._tablename);
			stringPlus.AppendLine("* 时间：" + DateTime.Now.ToString());
			stringPlus.AppendLine("******************************************************************/");
			stringPlus.AppendLine("");
			if (Maxid)
			{
				stringPlus.AppendLine(this.CreatPROCGetMaxID());
			}
			if (Ishas)
			{
				stringPlus.AppendLine(this.CreatPROCIsHas());
			}
			if (Add)
			{
				stringPlus.AppendLine(this.CreatPROCADD());
			}
			if (Update)
			{
				stringPlus.AppendLine(this.CreatPROCUpdate());
			}
			if (Delete)
			{
				stringPlus.AppendLine(this.CreatPROCDelete());
			}
			if (GetModel)
			{
				stringPlus.AppendLine(this.CreatPROCGetModel());
			}
			if (List)
			{
				stringPlus.AppendLine(this.CreatPROCGetList());
			}
			return stringPlus.Value;
		}
		public string CreatPROCGetMaxID()
		{
			StringPlus stringPlus = new StringPlus();
			if (this._keys.Count > 0)
			{
				foreach (ColumnInfo current in this._keys)
				{
					if (CodeCommon.DbTypeToCS(current.TypeName) == "int")
					{
						string columnName = current.ColumnName;
						if (current.IsPrimaryKey)
						{
							stringPlus.AppendLine("------------------------------------");
							stringPlus.AppendLine("--用途：得到主键字段最大值 ");
							stringPlus.AppendLine("--项目名称：" + this.ProjectName);
							stringPlus.AppendLine("--说明：");
							stringPlus.AppendLine("--时间：" + DateTime.Now.ToString());
							stringPlus.AppendLine("------------------------------------");
							stringPlus.AppendLine("CREATE PROCEDURE " + this.ProcPrefix + this._tablename + "_GetMaxId");
							stringPlus.AppendLine("AS");
							stringPlus.AppendSpaceLine(1, "DECLARE @TempID int");
							stringPlus.AppendSpaceLine(1, "SELECT @TempID = max([" + columnName + "])+1 FROM " + this._tablename);
							stringPlus.AppendSpaceLine(1, "IF @TempID IS NULL");
							stringPlus.AppendSpaceLine(2, "RETURN 1");
							stringPlus.AppendSpaceLine(1, "ELSE");
							stringPlus.AppendSpaceLine(2, "RETURN @TempID");
							stringPlus.AppendLine("");
							stringPlus.AppendLine("GO");
							break;
						}
					}
				}
			}
			return stringPlus.ToString();
		}
		public string CreatPROCIsHas()
		{
			StringPlus stringPlus = new StringPlus();
			stringPlus.AppendLine("------------------------------------");
			stringPlus.AppendLine("--用途：是否已经存在 ");
			stringPlus.AppendLine("--项目名称：" + this.ProjectName);
			stringPlus.AppendLine("--说明：");
			stringPlus.AppendLine("--时间：" + DateTime.Now.ToString());
			stringPlus.AppendLine("------------------------------------");
			stringPlus.AppendLine("CREATE PROCEDURE " + this.ProcPrefix + this._tablename + "_Exists");
			stringPlus.AppendLine(this.GetInParameter(this.Keys, false));
			stringPlus.AppendLine("AS");
			stringPlus.AppendSpaceLine(1, "DECLARE @TempID int");
			stringPlus.AppendSpaceLine(1, "SELECT @TempID = count(1) FROM " + this._tablename + " WHERE " + this.GetWhereExpression(this.Keys));
			stringPlus.AppendSpaceLine(1, "IF @TempID = 0");
			stringPlus.AppendSpaceLine(2, "RETURN 0");
			stringPlus.AppendSpaceLine(1, "ELSE");
			stringPlus.AppendSpaceLine(2, "RETURN 1");
			stringPlus.AppendLine("");
			stringPlus.AppendLine("GO");
			return stringPlus.Value;
		}
		public string CreatPROCADD()
		{
			StringPlus stringPlus = new StringPlus();
			StringPlus stringPlus2 = new StringPlus();
			StringPlus stringPlus3 = new StringPlus();
			stringPlus.AppendLine("------------------------------------");
			stringPlus.AppendLine("--用途：增加一条记录 ");
			stringPlus.AppendLine("--项目名称：" + this.ProjectName);
			stringPlus.AppendLine("--说明：");
			stringPlus.AppendLine("--时间：" + DateTime.Now.ToString());
			stringPlus.AppendLine("------------------------------------");
			stringPlus.AppendLine("CREATE PROCEDURE " + this.ProcPrefix + this._tablename + "_ADD");
			stringPlus.AppendLine(this.GetInParameter(this.Fieldlist, true));
			foreach (ColumnInfo current in this.Fieldlist)
			{
				string columnName = current.ColumnName;
				string typeName = current.TypeName;
				bool isIdentity = current.IsIdentity;
				bool arg_E9_0 = current.IsPrimaryKey;
				string arg_F0_0 = current.Length;
				string arg_F7_0 = current.Precision;
				string arg_FE_0 = current.Scale;
				if (isIdentity)
				{
					this._key = columnName;
					this._keyType = typeName;
				}
				else
				{
					stringPlus2.Append(columnName + ",");
					stringPlus3.Append("@" + columnName + ",");
				}
			}
			stringPlus2.DelLastComma();
			stringPlus3.DelLastComma();
			stringPlus.AppendLine("");
			stringPlus.AppendLine(" AS ");
			stringPlus.AppendSpaceLine(1, "INSERT INTO " + this._tablename + "(");
			stringPlus.AppendSpaceLine(1, stringPlus2.Value);
			stringPlus.AppendSpaceLine(1, ")VALUES(");
			stringPlus.AppendSpaceLine(1, stringPlus3.Value);
			stringPlus.AppendSpaceLine(1, ")");
			if (this.IsHasIdentity)
			{
				stringPlus.AppendSpaceLine(1, "SET @" + this._key + " = @@IDENTITY");
			}
			stringPlus.AppendLine("");
			stringPlus.AppendLine("GO");
			return stringPlus.Value;
		}
		public string CreatPROCUpdate()
		{
			StringPlus stringPlus = new StringPlus();
			StringPlus stringPlus2 = new StringPlus();
			stringPlus.AppendLine("------------------------------------");
			stringPlus.AppendLine("--用途：修改一条记录 ");
			stringPlus.AppendLine("--项目名称：" + this.ProjectName);
			stringPlus.AppendLine("--说明：");
			stringPlus.AppendLine("--时间：" + DateTime.Now.ToString());
			stringPlus.AppendLine("------------------------------------");
			stringPlus.AppendLine("CREATE PROCEDURE " + this.ProcPrefix + this._tablename + "_Update");
			foreach (ColumnInfo current in this.Fieldlist)
			{
				string columnName = current.ColumnName;
				string typeName = current.TypeName;
				bool isIdentity = current.IsIdentity;
				bool isPrimaryKey = current.IsPrimaryKey;
				string length = current.Length;
				string precision = current.Precision;
				string scale = current.Scale;
				string key;
				if ((key = typeName.ToLower()) == null)
				{
					goto IL_26E;
				}
				if (dtCollection== null)
				{
					dtCollection= new Dictionary<string, int>(8)
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
							"char",
							3
						},

						{
							"nchar",
							4
						},

						{
							"binary",
							5
						},

						{
							"nvarchar",
							6
						},

						{
							"varbinary",
							7
						}
					};
				}
				int num;
				if (!dtCollection.TryGetValue(key, out num))
				{
					goto IL_26E;
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
						"),"
					}));
					break;
				case 2:
				case 3:
				case 4:
				case 5:
				case 6:
				case 7:
				{
					string dataTypeLenVal = CodeCommon.GetDataTypeLenVal(typeName.Trim(), length);
					stringPlus.AppendLine(string.Concat(new string[]
					{
						"@",
						columnName,
						" ",
						typeName,
						"(",
						dataTypeLenVal,
						"),"
					}));
					break;
				}
				default:
					goto IL_26E;
				}
				IL_2AA:
				if (!isIdentity && !isPrimaryKey)
				{
					stringPlus2.Append(columnName + " = @" + columnName + ",");
					continue;
				}
				continue;
				IL_26E:
				stringPlus.AppendLine(string.Concat(new string[]
				{
					"@",
					columnName,
					" ",
					typeName,
					","
				}));
				goto IL_2AA;
			}
			stringPlus.DelLastComma();
			stringPlus2.DelLastComma();
			stringPlus.AppendLine();
			stringPlus.AppendLine(" AS ");
			stringPlus.AppendSpaceLine(1, "UPDATE " + this._tablename + " SET ");
			stringPlus.AppendSpaceLine(1, stringPlus2.Value);
			stringPlus.AppendSpaceLine(1, "WHERE " + this.GetWhereExpression(this.Keys));
			stringPlus.AppendLine();
			stringPlus.AppendLine("GO");
			return stringPlus.Value;
		}
		public string CreatPROCDelete()
		{
			StringPlus stringPlus = new StringPlus();
			stringPlus.AppendLine("------------------------------------");
			stringPlus.AppendLine("--用途：删除一条记录 ");
			stringPlus.AppendLine("--项目名称：" + this.ProjectName);
			stringPlus.AppendLine("--说明：");
			stringPlus.AppendLine("--时间：" + DateTime.Now.ToString());
			stringPlus.AppendLine("------------------------------------");
			stringPlus.AppendLine("CREATE PROCEDURE " + this.ProcPrefix + this._tablename + "_Delete");
			stringPlus.AppendLine(this.GetInParameter(this.Keys, false));
			stringPlus.AppendLine(" AS ");
			stringPlus.AppendSpaceLine(1, "DELETE " + this._tablename);
			stringPlus.AppendSpaceLine(1, " WHERE " + this.GetWhereExpression(this.Keys));
			stringPlus.AppendLine("");
			stringPlus.AppendLine("GO");
			return stringPlus.Value;
		}
		public string CreatPROCGetModel()
		{
			StringPlus stringPlus = new StringPlus();
			stringPlus.AppendLine("------------------------------------");
			stringPlus.AppendLine("--用途：得到实体对象的详细信息 ");
			stringPlus.AppendLine("--项目名称：" + this.ProjectName);
			stringPlus.AppendLine("--说明：");
			stringPlus.AppendLine("--时间：" + DateTime.Now.ToString());
			stringPlus.AppendLine("------------------------------------");
			stringPlus.AppendLine("CREATE PROCEDURE " + this.ProcPrefix + this._tablename + "_GetModel");
			stringPlus.AppendLine(this.GetInParameter(this.Keys, false));
			stringPlus.AppendLine(" AS ");
			stringPlus.AppendSpaceLine(1, "SELECT ");
			stringPlus.AppendSpaceLine(1, this.Fieldstrlist);
			stringPlus.AppendSpaceLine(1, " FROM " + this._tablename);
			stringPlus.AppendSpaceLine(1, " WHERE " + this.GetWhereExpression(this.Keys));
			stringPlus.AppendLine("");
			stringPlus.AppendLine("GO");
			return stringPlus.Value;
		}
		public string CreatPROCGetList()
		{
			StringPlus stringPlus = new StringPlus();
			stringPlus.AppendLine("------------------------------------");
			stringPlus.AppendLine("--用途：查询记录信息 ");
			stringPlus.AppendLine("--项目名称：" + this.ProjectName);
			stringPlus.AppendLine("--说明：");
			stringPlus.AppendLine("--时间：" + DateTime.Now.ToString());
			stringPlus.AppendLine("------------------------------------");
			stringPlus.AppendLine("CREATE PROCEDURE " + this.ProcPrefix + this._tablename + "_GetList");
			stringPlus.AppendLine(" AS ");
			stringPlus.AppendSpaceLine(1, "SELECT ");
			stringPlus.AppendSpaceLine(1, this.Fieldstrlist);
			stringPlus.AppendSpaceLine(1, " FROM " + this._tablename);
			stringPlus.AppendLine("");
			stringPlus.AppendLine("GO");
			return stringPlus.Value;
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
					stringPlus.Append(current.ColumnName + ",");
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
					stringPlus.AppendLine(current.ColumnName + " = <" + current.ColumnName + ">,");
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
					stringPlus.AppendLine(current.ColumnName + " ,");
					if (CodeCommon.IsAddMark(current.TypeName))
					{
						stringPlus2.Append("'" + current.ColumnName + "',");
					}
					else
					{
						stringPlus2.Append(current.ColumnName + ",");
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
