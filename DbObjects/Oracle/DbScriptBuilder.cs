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
namespace Maticsoft.DbObjects.Oracle
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
				string text = " ";
				if (isIdentity && output)
				{
					text = " out ";
				}
				string key;
				if ((key = typeName.ToLower()) == null)
				{
					goto IL_1AC;
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
					goto IL_1AC;
				}
				switch (num)
				{
				case 0:
				case 1:
					stringPlus.Append(string.Concat(new string[]
					{
						columnName,
						"_in",
						text,
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
					stringPlus.Append(string.Concat(new string[]
					{
						columnName,
						"_in",
						text,
						typeName,
						"(",
						length,
						")"
					}));
					break;
				default:
					goto IL_1AC;
				}
				IL_1C1:
				if (!isIdentity || !output)
				{
					stringPlus.AppendLine(",");
					continue;
				}
				continue;
				IL_1AC:
				stringPlus.Append(columnName + "_in" + text + typeName);
				goto IL_1C1;
			}
			stringPlus.DelLastComma();
			return stringPlus.Value;
		}
		public string GetWhereExpression(List<ColumnInfo> keys)
		{
			StringPlus stringPlus = new StringPlus();
			foreach (ColumnInfo current in keys)
			{
				stringPlus.Append(current.ColumnName + "= " + current.ColumnName + "_in and ");
			}
			stringPlus.DelLastChar("and");
			return stringPlus.Value;
		}
		public bool IsKeys(string columnName)
		{
			bool result = false;
			foreach (ColumnInfo current in this.Keys)
			{
				if (current.ColumnName.Trim() == columnName.Trim())
				{
					result = true;
				}
			}
			return result;
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
			stringPlus.AppendLine("CREATE TABLE \"" + tablename + "\" (");
			if (columnInfoDt != null)
			{
				if (this.Fieldlist.Count > 0)
				{
					columnInfoDt.Select("ColumnName in (" + this.Fields + ")", "colorder asc");
				}
				else
				{
					columnInfoDt.Select();
				}
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
						"\"",
						text,
						"\" ",
						text2,
						" "
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
					if (text5 != "")
					{
						stringPlus.Append(" DEFAULT " + text5);
					}
					if (a2 == "√")
					{
						stringPlus.Append(" NULL");
					}
					else
					{
						stringPlus.Append(" NOT NULL");
					}
					stringPlus.AppendLine(",");
					hashtable.Add(text, text2);
					stringPlus2.Append("\"" + text + "\",");
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
							goto IL_47C;
						}
						string text9;
						if (!(a4 == "BLOB"))
						{
							if (!(a4 == "bit"))
							{
								goto IL_47C;
							}
							text9 = ((dataRow2[text7].ToString().ToLower() == "true") ? "1" : "0");
						}
						else
						{
							byte[] bytes = (byte[])dataRow2[text7];
							text9 = CodeCommon.ToHexString(bytes);
						}
						IL_491:
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
							stringPlus3.Append(text7 + ",");
						}
						i++;
						continue;
						IL_47C:
						text9 = dataRow2[text7].ToString().Trim();
						goto IL_491;
					}
					stringPlus3.DelLastComma();
					stringPlus4.DelLastComma();
					stringPlus.Append("INSERT \"" + tablename + "\" (");
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
			stringPlus.AppendLine("CREATE TABLE \"" + tablename + "\" (");
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
						"\"",
						columnName,
						"\" ",
						typeName,
						" "
					}));
					string a;
					if ((a = typeName.Trim()) != null)
					{
						if (!(a == "CHAR") && !(a == "VARCHAR2") && !(a == "NCHAR") && !(a == "NVARCHAR2"))
						{
							if (a == "NUMBER")
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
					stringPlus2.Append("\"" + columnName + "\",");
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
					"ALTER TABLE \"",
					tablename,
					"\" WITH NOCHECK ADD  CONSTRAINT [PK_",
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
							goto IL_481;
						}
						string text5;
						if (!(a2 == "BLOB"))
						{
							if (!(a2 == "bit"))
							{
								goto IL_481;
							}
							text5 = ((dataRow[text3].ToString().ToLower() == "true") ? "1" : "0");
						}
						else
						{
							byte[] bytes = (byte[])dataRow[text3];
							text5 = CodeCommon.ToHexString(bytes);
						}
						IL_496:
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
						i++;
						continue;
						IL_481:
						text5 = dataRow[text3].ToString().Trim();
						goto IL_496;
					}
					stringPlus4.DelLastComma();
					stringPlus5.DelLastComma();
					stringPlus3.Append("INSERT \"" + tablename + "\" (");
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
			this.dbobj.DbConnectStr = this._dbconnectStr;
			DataTable tabViews = this.dbobj.GetTabViews(dbname);
			StringPlus stringPlus = new StringPlus();
			if (tabViews != null)
			{
				foreach (DataRow dataRow in tabViews.Rows)
				{
					string tablename = dataRow["name"].ToString();
					stringPlus.AppendLine(this.GetPROCCode(dbname, tablename));
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
			stringPlus.AppendLine("* Made by Codematic");
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
							stringPlus.AppendLine("CREATE PROCEDURE " + this.ProcPrefix + this._tablename + "_GetMaxId (");
							stringPlus.AppendLine(")");
							stringPlus.AppendLine("IS");
							stringPlus.AppendLine("TempID Number;");
							stringPlus.AppendLine("BEGIN");
							stringPlus.AppendSpaceLine(1, "SELECT max(" + columnName + ") into TempID FROM " + this._tablename);
							stringPlus.AppendSpaceLine(1, "IF NVL(TempID) then");
							stringPlus.AppendSpaceLine(2, "RETURN 1;");
							stringPlus.AppendSpaceLine(1, "ELSE");
							stringPlus.AppendSpaceLine(2, "RETURN TempID;");
							stringPlus.AppendSpaceLine(1, "end IF;");
							stringPlus.AppendLine("END;");
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
			stringPlus.AppendLine("CREATE PROCEDURE " + this.ProcPrefix + this._tablename + "_Exists (");
			stringPlus.AppendLine(this.GetInParameter(this.Keys, false));
			stringPlus.AppendLine(")");
			stringPlus.AppendLine("AS");
			stringPlus.AppendLine("TempID Number;");
			stringPlus.AppendLine("BEGIN");
			stringPlus.AppendSpaceLine(1, "SELECT count(1) into TempID  FROM " + this._tablename + " WHERE " + this.GetWhereExpression(this.Keys));
			stringPlus.AppendSpaceLine(1, "IF TempID = 0 then");
			stringPlus.AppendSpaceLine(2, "RETURN 0;");
			stringPlus.AppendSpaceLine(1, "ELSE");
			stringPlus.AppendSpaceLine(2, "RETURN 1;");
			stringPlus.AppendSpaceLine(1, "end IF;");
			stringPlus.AppendLine("END;");
			stringPlus.AppendLine();
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
			stringPlus.AppendLine("CREATE PROCEDURE " + this.ProcPrefix + this._tablename + "_ADD (");
			stringPlus.AppendLine(this.GetInParameter(this.Fieldlist, true));
			stringPlus.AppendLine(")");
			foreach (ColumnInfo current in this.Fieldlist)
			{
				string columnName = current.ColumnName;
				string arg_E5_0 = current.TypeName;
				bool arg_EC_0 = current.IsIdentity;
				bool arg_F3_0 = current.IsPrimaryKey;
				string arg_FA_0 = current.Length;
				string arg_101_0 = current.Precision;
				string arg_108_0 = current.Scale;
				stringPlus2.Append(columnName + ",");
				stringPlus3.Append(columnName + "_in ,");
			}
			stringPlus2.DelLastComma();
			stringPlus3.DelLastComma();
			stringPlus.AppendLine(" AS ");
			stringPlus.AppendLine("BEGIN");
			stringPlus.AppendSpaceLine(1, "INSERT INTO " + this._tablename + "(");
			stringPlus.AppendSpaceLine(1, stringPlus2.Value);
			stringPlus.AppendSpaceLine(1, ")VALUES(");
			stringPlus.AppendSpaceLine(1, stringPlus3.Value);
			stringPlus.AppendSpaceLine(1, ");");
			stringPlus.AppendLine("COMMIT;");
			stringPlus.AppendLine("END;");
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
				bool arg_C9_0 = current.IsIdentity;
				bool arg_D0_0 = current.IsPrimaryKey;
				string length = current.Length;
				string precision = current.Precision;
				string scale = current.Scale;
				string key;
				if ((key = typeName.ToLower()) == null)
				{
					goto IL_226;
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
					goto IL_226;
				}
				switch (num)
				{
				case 0:
				case 1:
					stringPlus.AppendLine(string.Concat(new string[]
					{
						columnName,
						"_in ",
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
					stringPlus.AppendLine(string.Concat(new string[]
					{
						columnName,
						"_in ",
						typeName,
						"(",
						length,
						"),"
					}));
					break;
				default:
					goto IL_226;
				}
				IL_23F:
				if (!this.IsKeys(columnName))
				{
					stringPlus2.Append(columnName + " = " + columnName + "_in ,");
					continue;
				}
				continue;
				IL_226:
				stringPlus.AppendLine(columnName + "_in " + typeName + ",");
				goto IL_23F;
			}
			stringPlus.DelLastComma();
			stringPlus2.DelLastComma();
			stringPlus.AppendLine("");
			stringPlus.AppendLine(" AS ");
			stringPlus.AppendLine("BEGIN");
			stringPlus.AppendSpaceLine(1, "UPDATE " + this._tablename + " SET ");
			stringPlus.AppendSpaceLine(1, stringPlus2.Value);
			stringPlus.AppendSpaceLine(1, "WHERE " + this.GetWhereExpression(this.Keys));
			stringPlus.AppendLine("");
			stringPlus.AppendLine("COMMIT;");
			stringPlus.AppendLine("END;");
			return stringPlus.Value;
		}
		public string CreatPROCDelete()
		{
			StringPlus stringPlus = new StringPlus();
			new StringPlus();
			stringPlus.AppendLine("------------------------------------");
			stringPlus.AppendLine("--用途：删除一条记录 ");
			stringPlus.AppendLine("--项目名称：" + this.ProjectName);
			stringPlus.AppendLine("--说明：");
			stringPlus.AppendLine("--时间：" + DateTime.Now.ToString());
			stringPlus.AppendLine("------------------------------------");
			stringPlus.AppendLine("CREATE PROCEDURE " + this.ProcPrefix + this._tablename + "_Delete");
			stringPlus.AppendLine(this.GetInParameter(this.Keys, false));
			stringPlus.AppendLine("BEGIN");
			stringPlus.AppendLine(" AS ");
			stringPlus.AppendSpaceLine(1, "DELETE " + this._tablename);
			stringPlus.AppendSpaceLine(1, " WHERE " + this.GetWhereExpression(this.Keys));
			stringPlus.AppendLine("COMMIT;");
			stringPlus.AppendLine("END;");
			return stringPlus.Value;
		}
		public string CreatPROCGetModel()
		{
			StringPlus stringPlus = new StringPlus();
			new StringPlus();
			stringPlus.AppendLine("------------------------------------");
			stringPlus.AppendLine("--用途：得到实体对象的详细信息 ");
			stringPlus.AppendLine("--项目名称：" + this.ProjectName);
			stringPlus.AppendLine("--说明：");
			stringPlus.AppendLine("--时间：" + DateTime.Now.ToString());
			stringPlus.AppendLine("------------------------------------");
			stringPlus.AppendLine("CREATE PROCEDURE " + this.ProcPrefix + this._tablename + "_GetModel");
			stringPlus.AppendLine(this.GetInParameter(this.Keys, false));
			stringPlus.AppendLine(" AS ");
			stringPlus.AppendLine("BEGIN");
			stringPlus.AppendSpaceLine(1, "SELECT ");
			stringPlus.AppendSpaceLine(1, this.Fieldstrlist);
			stringPlus.AppendSpaceLine(1, " FROM " + this._tablename);
			stringPlus.AppendSpaceLine(1, " WHERE " + this.GetWhereExpression(this.Keys));
			stringPlus.AppendLine("COMMIT;");
			stringPlus.AppendLine("END;");
			return stringPlus.Value;
		}
		public string CreatPROCGetList()
		{
			StringPlus stringPlus = new StringPlus();
			new StringPlus();
			stringPlus.AppendLine("------------------------------------");
			stringPlus.AppendLine("--用途：查询记录信息 ");
			stringPlus.AppendLine("--项目名称：" + this.ProjectName);
			stringPlus.AppendLine("--说明：");
			stringPlus.AppendLine("--时间：" + DateTime.Now.ToString());
			stringPlus.AppendLine("------------------------------------");
			stringPlus.AppendLine("CREATE PROCEDURE " + this.ProcPrefix + this._tablename + "_GetList");
			stringPlus.AppendLine(" AS ");
			stringPlus.AppendLine("BEGIN");
			stringPlus.AppendSpaceLine(1, "SELECT ");
			stringPlus.AppendSpaceLine(1, this.Fieldstrlist);
			stringPlus.AppendSpaceLine(1, " FROM " + this._tablename);
			stringPlus.AppendLine("COMMIT;");
			stringPlus.AppendLine("END;");
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
					string columnName = current.ColumnName;
					stringPlus.Append(columnName + ",");
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
					stringPlus.AppendLine(columnName + " = <" + columnName + ">,");
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
					stringPlus.AppendLine(columnName + " ,");
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
