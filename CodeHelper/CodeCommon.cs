using Maticsoft.CmConfig;
using Maticsoft.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
namespace Maticsoft.CodeHelper
{
	public class CodeCommon
	{
		private static string datatypefile = AppDomain.CurrentDomain.BaseDirectory.TrimEnd(new char[]
		{
			'\\'
		}) + "\\DatatypeMap.cfg";
		private static char[] hexDigits = new char[]
		{
			'0',
			'1',
			'2',
			'3',
			'4',
			'5',
			'6',
			'7',
			'8',
			'9',
			'A',
			'B',
			'C',
			'D',
			'E',
			'F'
		};
		public static string Space(int num)
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < num; i++)
			{
				stringBuilder.Append("\t");
			}
			return stringBuilder.ToString();
		}
		public static string DbTypeToCS(string dbtype)
		{
			string result = "string";
			if (File.Exists(CodeCommon.datatypefile))
			{
				string valueFromCfg = DatatypeMap.GetValueFromCfg(CodeCommon.datatypefile, "DbToCS", dbtype.ToLower().Trim());
				if (valueFromCfg == "")
				{
					result = dbtype.ToLower().Trim();
				}
				else
				{
					result = valueFromCfg;
				}
			}
			return result;
		}
		public static bool isValueType(string cstype)
		{
			bool result = false;
			if (File.Exists(CodeCommon.datatypefile))
			{
				string valueFromCfg = DatatypeMap.GetValueFromCfg(CodeCommon.datatypefile, "ValueType", cstype.Trim());
				if (valueFromCfg == "true" || valueFromCfg == "是")
				{
					result = true;
				}
			}
			return result;
		}
		public static string DbTypeLength(string dbtype, string datatype, string Length)
		{
			string result = "";
			switch (dbtype)
			{
			case "SQL2000":
			case "SQL2005":
			case "SQL2008":
			case "SQL2012":
				result = CodeCommon.DbTypeLengthSQL(dbtype, datatype, Length);
				break;
			case "Oracle":
				result = CodeCommon.DbTypeLengthOra(datatype, Length);
				break;
			case "MySQL":
				result = CodeCommon.DbTypeLengthMySQL(datatype, Length);
				break;
			case "OleDb":
				result = CodeCommon.DbTypeLengthOleDb(datatype, Length);
				break;
			case "SQLite":
				result = CodeCommon.DbTypeLengthSQLite(datatype, Length);
				break;
			}
			return result;
		}
		public static string GetDataTypeLenVal(string datatype, string Length)
		{
			string result = "";
			string key;
			switch (key = datatype.Trim())
			{
			case "int":
				if (Length == "")
				{
					result = "4";
					return result;
				}
				result = Length;
				return result;
			case "char":
				if (Length.Trim() == "")
				{
					result = "10";
					return result;
				}
				result = Length;
				return result;
			case "nchar":
				result = Length;
				if (Length.Trim() == "")
				{
					result = "10";
					return result;
				}
				return result;
			case "varchar":
			case "nvarchar":
			case "varbinary":
				result = Length;
				if (Length.Length == 0 || Length == "0" || Length == "-1")
				{
					result = "MAX";
					return result;
				}
				return result;
			case "bit":
				result = "1";
				return result;
			case "float":
			case "numeric":
			case "decimal":
			case "money":
			case "smallmoney":
			case "binary":
			case "smallint":
			case "bigint":
				result = Length;
				return result;
			case "image":
			case "datetime":
			case "smalldatetime":
			case "ntext":
			case "text":
				return result;
			}
			result = Length;
			return result;
		}
		private static string DbTypeLengthSQL(string dbtype, string datatype, string Length)
		{
			string text = CodeCommon.GetDataTypeLenVal(datatype, Length);
			string result;
			if (text != "")
			{
				if (text == "MAX")
				{
					text = "-1";
				}
				result = CodeCommon.CSToProcType(dbtype, datatype) + "," + text;
			}
			else
			{
				result = CodeCommon.CSToProcType(dbtype, datatype);
			}
			return result;
		}
		private static string DbTypeLengthOra(string datatype, string Length)
		{
			string text = "";
			string key;
			switch (key = datatype.Trim().ToLower())
			{
			case "number":
				if (Length == "")
				{
					text = "4";
					goto IL_139;
				}
				text = Length;
				goto IL_139;
			case "varchar2":
				if (Length == "")
				{
					text = "50";
					goto IL_139;
				}
				text = Length;
				goto IL_139;
			case "char":
				if (Length == "")
				{
					text = "50";
					goto IL_139;
				}
				text = Length;
				goto IL_139;
			case "date":
			case "nchar":
			case "nvarchar2":
			case "long":
			case "long raw":
			case "bfile":
			case "blob":
				goto IL_139;
			}
			text = Length;
			IL_139:
			if (text != "")
			{
				text = CodeCommon.CSToProcType("Oracle", datatype) + "," + text;
			}
			else
			{
				text = CodeCommon.CSToProcType("Oracle", datatype);
			}
			return text;
		}
		private static string DbTypeLengthMySQL(string datatype, string Length)
		{
			string text = "";
			string key;
			switch (key = datatype.Trim().ToLower())
			{
			case "number":
				if (Length == "")
				{
					text = "4";
					goto IL_139;
				}
				text = Length;
				goto IL_139;
			case "varchar2":
				if (Length == "")
				{
					text = "50";
					goto IL_139;
				}
				text = Length;
				goto IL_139;
			case "char":
				if (Length == "")
				{
					text = "50";
					goto IL_139;
				}
				text = Length;
				goto IL_139;
			case "date":
			case "nchar":
			case "nvarchar2":
			case "long":
			case "long raw":
			case "bfile":
			case "blob":
				goto IL_139;
			}
			text = Length;
			IL_139:
			if (text != "")
			{
				text = CodeCommon.CSToProcType("MySQL", datatype) + "," + text;
			}
			else
			{
				text = CodeCommon.CSToProcType("MySQL", datatype);
			}
			return text;
		}
		private static string DbTypeLengthOleDb(string datatype, string Length)
		{
			string text = "";
			string key;
			switch (key = datatype.Trim())
			{
			case "int":
				if (Length == "")
				{
					text = "4";
					goto IL_1DC;
				}
				text = Length;
				goto IL_1DC;
			case "varchar":
				if (Length == "")
				{
					text = "50";
					goto IL_1DC;
				}
				text = Length;
				goto IL_1DC;
			case "char":
				if (Length == "")
				{
					text = "50";
					goto IL_1DC;
				}
				text = Length;
				goto IL_1DC;
			case "bit":
				text = "1";
				goto IL_1DC;
			case "float":
			case "numeric":
			case "decimal":
			case "money":
			case "smallmoney":
			case "binary":
			case "smallint":
			case "bigint":
				text = Length;
				goto IL_1DC;
			case "image":
			case "datetime":
			case "smalldatetime":
			case "nchar":
			case "nvarchar":
			case "ntext":
			case "text":
				goto IL_1DC;
			}
			text = Length;
			IL_1DC:
			if (text != "")
			{
				text = CodeCommon.CSToProcType("OleDb", datatype) + "," + text;
			}
			else
			{
				text = CodeCommon.CSToProcType("OleDb", datatype);
			}
			return text;
		}
		private static string DbTypeLengthSQLite(string datatype, string Length)
		{
			string text = "";
			string key;
			switch (key = datatype.Trim())
			{
			case "int":
			case "integer":
				if (Length == "")
				{
					text = "4";
					goto IL_231;
				}
				text = Length;
				goto IL_231;
			case "varchar":
				if (Length == "")
				{
					text = "50";
					goto IL_231;
				}
				text = Length;
				goto IL_231;
			case "char":
				if (Length == "")
				{
					text = "50";
					goto IL_231;
				}
				text = Length;
				goto IL_231;
			case "bit":
				text = "1";
				goto IL_231;
			case "float":
			case "numeric":
			case "decimal":
			case "money":
			case "smallmoney":
			case "binary":
			case "smallint":
			case "bigint":
			case "blob":
				text = Length;
				goto IL_231;
			case "image":
			case "datetime":
			case "smalldatetime":
			case "nchar":
			case "nvarchar":
			case "ntext":
			case "text":
			case "time":
			case "date":
			case "boolean":
				goto IL_231;
			}
			text = Length;
			IL_231:
			if (text != "")
			{
				text = CodeCommon.CSToProcType("SQLite", datatype) + "," + text;
			}
			else
			{
				text = CodeCommon.CSToProcType("SQLite", datatype);
			}
			return text;
		}
		public static string CSToProcType(string DbType, string cstype)
		{
			string result = cstype;
			switch (DbType)
			{
			case "SQL2000":
			case "SQL2005":
			case "SQL2008":
			case "SQL2012":
				result = CodeCommon.CSToProcTypeSQL(cstype);
				break;
			case "Oracle":
				result = CodeCommon.CSToProcTypeOra(cstype);
				break;
			case "MySQL":
				result = CodeCommon.CSToProcTypeMySQL(cstype);
				break;
			case "OleDb":
				result = CodeCommon.CSToProcTypeOleDb(cstype);
				break;
			case "SQLite":
				result = CodeCommon.CSToProcTypeSQLite(cstype);
				break;
			}
			return result;
		}
		private static string CSToProcTypeSQL(string cstype)
		{
			string result = cstype;
			if (File.Exists(CodeCommon.datatypefile))
			{
				string valueFromCfg = DatatypeMap.GetValueFromCfg(CodeCommon.datatypefile, "SQLDbType", cstype.ToLower().Trim());
				if (valueFromCfg == "")
				{
					result = cstype.ToLower().Trim();
				}
				else
				{
					result = valueFromCfg;
				}
			}
			return result;
		}
		private static string CSToProcTypeOra(string cstype)
		{
			string result = cstype;
			if (File.Exists(CodeCommon.datatypefile))
			{
				string valueFromCfg = DatatypeMap.GetValueFromCfg(CodeCommon.datatypefile, "OraDbType", cstype.ToLower().Trim());
				if (valueFromCfg == "")
				{
					result = cstype.ToLower().Trim();
				}
				else
				{
					result = valueFromCfg;
				}
			}
			return result;
		}
		private static string CSToProcTypeMySQL(string cstype)
		{
			string result = cstype;
			if (File.Exists(CodeCommon.datatypefile))
			{
				string valueFromCfg = DatatypeMap.GetValueFromCfg(CodeCommon.datatypefile, "MySQLDbType", cstype.ToLower().Trim());
				if (valueFromCfg == "")
				{
					result = cstype.ToLower().Trim();
				}
				else
				{
					result = valueFromCfg;
				}
			}
			return result;
		}
		private static string CSToProcTypeOleDb(string cstype)
		{
			string result = cstype;
			if (File.Exists(CodeCommon.datatypefile))
			{
				string valueFromCfg = DatatypeMap.GetValueFromCfg(CodeCommon.datatypefile, "OleDbDbType", cstype.ToLower().Trim());
				if (valueFromCfg == "")
				{
					result = cstype.ToLower().Trim();
				}
				else
				{
					result = valueFromCfg;
				}
			}
			return result;
		}
		private static string CSToProcTypeSQLite(string cstype)
		{
			string result = cstype;
			if (File.Exists(CodeCommon.datatypefile))
			{
				string valueFromCfg = DatatypeMap.GetValueFromCfg(CodeCommon.datatypefile, "SQLiteType", cstype.ToLower().Trim());
				if (valueFromCfg == "")
				{
					result = cstype.ToLower().Trim();
				}
				else
				{
					result = valueFromCfg;
				}
			}
			return result;
		}
		public static bool IsAddMark(string columnType)
		{
			bool result = false;
			if (File.Exists(CodeCommon.datatypefile))
			{
				string valueFromCfg = DatatypeMap.GetValueFromCfg(CodeCommon.datatypefile, "AddMark", columnType.ToLower().Trim());
				if (valueFromCfg == "true" || valueFromCfg == "是")
				{
					result = true;
				}
			}
			return result;
		}
		public static string ToHexString(byte[] bytes)
		{
			char[] array = new char[bytes.Length * 2];
			for (int i = 0; i < bytes.Length; i++)
			{
				int num = (int)bytes[i];
				array[i * 2] = CodeCommon.hexDigits[num >> 4];
				array[i * 2 + 1] = CodeCommon.hexDigits[num & 15];
			}
			string text = new string(array);
			return "0x" + text.Substring(0, bytes.Length);
		}
		public static List<ColumnInfo> GetColumnInfos(DataTable dt)
		{
			List<ColumnInfo> list = new List<ColumnInfo>();
			if (dt != null)
			{
				ArrayList arrayList = new ArrayList();
				foreach (DataRow dataRow in dt.Rows)
				{
					string columnOrder = dataRow["Colorder"].ToString();
					string text = dataRow["ColumnName"].ToString();
					string typeName = dataRow["TypeName"].ToString();
					string a = dataRow["IsIdentity"].ToString();
					string a2 = dataRow["IsPK"].ToString();
					string length = dataRow["Length"].ToString();
					string precision = dataRow["Preci"].ToString();
					string scale = dataRow["Scale"].ToString();
					string a3 = dataRow["cisNull"].ToString();
					string defaultVal = dataRow["DefaultVal"].ToString();
					string description = dataRow["DeText"].ToString();
					ColumnInfo columnInfo = new ColumnInfo();
					columnInfo.ColumnOrder = columnOrder;
					columnInfo.ColumnName = text;
					columnInfo.TypeName = typeName;
					columnInfo.IsIdentity = (a == "√");
					columnInfo.IsPrimaryKey = (a2 == "√");
					columnInfo.Length = length;
					columnInfo.Precision = precision;
					columnInfo.Scale = scale;
					columnInfo.Nullable = (a3 == "√");
					columnInfo.DefaultVal = defaultVal;
					columnInfo.Description = description;
					if (!arrayList.Contains(text))
					{
						list.Add(columnInfo);
						arrayList.Add(text);
					}
				}
				return list;
			}
			return null;
		}
		public static DataTable GetColumnInfoDt(List<ColumnInfo> collist)
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
			foreach (ColumnInfo current in collist)
			{
				DataRow dataRow = dataTable.NewRow();
				dataRow["colorder"] = current.ColumnOrder;
				dataRow["ColumnName"] = current.ColumnName;
				dataRow["TypeName"] = current.TypeName;
				dataRow["Length"] = current.Length;
				dataRow["Preci"] = current.Precision;
				dataRow["Scale"] = current.Scale;
				dataRow["IsIdentity"] = (current.IsIdentity ? "√" : "");
				dataRow["isPK"] = (current.IsPrimaryKey ? "√" : "");
				dataRow["cisNull"] = (current.Nullable ? "√" : "");
				dataRow["defaultVal"] = current.DefaultVal;
				dataRow["deText"] = current.Description;
				dataTable.Rows.Add(dataRow);
			}
			return dataTable;
		}
		public static ColumnInfo GetIdentityKey(List<ColumnInfo> keys)
		{
			foreach (ColumnInfo current in keys)
			{
				if (current.IsIdentity)
				{
					return current;
				}
			}
			return null;
		}
		public static bool HasNoIdentityKey(List<ColumnInfo> keys)
		{
			foreach (ColumnInfo current in keys)
			{
				if (current.IsPrimaryKey && !current.IsIdentity)
				{
					return true;
				}
			}
			return false;
		}
		public static string DbParaHead(string DbType)
		{
			switch (DbType)
			{
			case "SQL2000":
			case "SQL2005":
			case "SQL2008":
			case "SQL2012":
				return "Sql";
			case "Oracle":
				return "Oracle";
			case "MySQL":
				return "MySql";
			case "OleDb":
				return "OleDb";
			case "SQLite":
				return "SQLite";
			}
			return "Sql";
		}
		public static string DbParaDbType(string DbType)
		{
			switch (DbType)
			{
			case "SQL2000":
			case "SQL2005":
			case "SQL2008":
			case "SQL2012":
				return "SqlDbType";
			case "Oracle":
				return "OracleType";
			case "OleDb":
				return "OleDbType";
			case "MySQL":
				return "MySqlDbType";
			case "SQLite":
				return "DbType";
			}
			return "SqlDbType";
		}
		public static string preParameter(string DbType)
		{
			string result = "@";
			if (File.Exists(CodeCommon.datatypefile))
			{
				string valueFromCfg = DatatypeMap.GetValueFromCfg(CodeCommon.datatypefile, "ParamePrefix", DbType.ToUpper().Trim());
				if (valueFromCfg == "")
				{
					result = DbType.ToUpper().Trim();
				}
				else
				{
					result = valueFromCfg;
				}
			}
			return result;
		}
		public static bool IsHasIdentity(List<ColumnInfo> Keys)
		{
			bool result = false;
			if (Keys.Count > 0)
			{
				foreach (ColumnInfo current in Keys)
				{
					if (current.IsIdentity)
					{
						result = true;
					}
				}
			}
			return result;
		}
		public static string GetWhereParameterExpression(List<ColumnInfo> keys, bool IdentityisPrior, string DbType)
		{
			StringPlus stringPlus = new StringPlus();
			ColumnInfo identityKey = CodeCommon.GetIdentityKey(keys);
			bool flag = CodeCommon.HasNoIdentityKey(keys);
			if ((IdentityisPrior && identityKey != null) || (!flag && identityKey != null))
			{
				stringPlus.Append(identityKey.ColumnName + "=" + CodeCommon.preParameter(DbType) + identityKey.ColumnName);
			}
			else
			{
				foreach (ColumnInfo current in keys)
				{
					if (current.IsPrimaryKey || !current.IsIdentity)
					{
						stringPlus.Append(string.Concat(new string[]
						{
							current.ColumnName,
							"=",
							CodeCommon.preParameter(DbType),
							current.ColumnName,
							" and "
						}));
					}
				}
				stringPlus.DelLastChar("and");
			}
			return stringPlus.Value;
		}
		public static string GetPreParameter(List<ColumnInfo> keys, bool IdentityisPrior, string DbType)
		{
			StringPlus stringPlus = new StringPlus();
			StringPlus stringPlus2 = new StringPlus();
			stringPlus.AppendSpaceLine(3, CodeCommon.DbParaHead(DbType) + "Parameter[] parameters = {");
			ColumnInfo identityKey = CodeCommon.GetIdentityKey(keys);
			bool flag = CodeCommon.HasNoIdentityKey(keys);
			if ((IdentityisPrior && identityKey != null) || (!flag && identityKey != null))
			{
				stringPlus.AppendSpaceLine(5, string.Concat(new string[]
				{
					"new ",
					CodeCommon.DbParaHead(DbType),
					"Parameter(\"",
					CodeCommon.preParameter(DbType),
					identityKey.ColumnName,
					"\", ",
					CodeCommon.DbParaDbType(DbType),
					".",
					CodeCommon.DbTypeLength(DbType, identityKey.TypeName, ""),
					")"
				}));
				stringPlus2.AppendSpaceLine(3, "parameters[0].Value = " + identityKey.ColumnName + ";");
			}
			else
			{
				int num = 0;
				foreach (ColumnInfo current in keys)
				{
					if (current.IsPrimaryKey || !current.IsIdentity)
					{
						stringPlus.AppendSpaceLine(5, string.Concat(new string[]
						{
							"new ",
							CodeCommon.DbParaHead(DbType),
							"Parameter(\"",
							CodeCommon.preParameter(DbType),
							current.ColumnName,
							"\", ",
							CodeCommon.DbParaDbType(DbType),
							".",
							CodeCommon.DbTypeLength(DbType, current.TypeName, current.Length),
							"),"
						}));
						stringPlus2.AppendSpaceLine(3, string.Concat(new string[]
						{
							"parameters[",
							num.ToString(),
							"].Value = ",
							current.ColumnName,
							";"
						}));
						num++;
					}
				}
				stringPlus.DelLastComma();
			}
			stringPlus.AppendSpaceLine(3, "};");
			stringPlus.Append(stringPlus2.Value);
			return stringPlus.Value;
		}
		public static string GetInParameter(List<ColumnInfo> keys, bool IdentityisPrior)
		{
			StringPlus stringPlus = new StringPlus();
			ColumnInfo identityKey = CodeCommon.GetIdentityKey(keys);
			if (IdentityisPrior && identityKey != null)
			{
				stringPlus.Append(CodeCommon.DbTypeToCS(identityKey.TypeName) + " " + identityKey.ColumnName);
			}
			else
			{
				foreach (ColumnInfo current in keys)
				{
					if (current.IsPrimaryKey || !current.IsIdentity)
					{
						stringPlus.Append(CodeCommon.DbTypeToCS(current.TypeName) + " " + current.ColumnName + ",");
					}
				}
				stringPlus.DelLastComma();
			}
			return stringPlus.Value;
		}
		public static string GetFieldstrlist(List<ColumnInfo> keys, bool IdentityisPrior)
		{
			StringPlus stringPlus = new StringPlus();
			ColumnInfo identityKey = CodeCommon.GetIdentityKey(keys);
			if (IdentityisPrior && identityKey != null)
			{
				stringPlus.Append(identityKey.ColumnName);
			}
			else
			{
				foreach (ColumnInfo current in keys)
				{
					if (current.IsPrimaryKey || !current.IsIdentity)
					{
						stringPlus.Append(current.ColumnName + ",");
					}
				}
				stringPlus.DelLastComma();
			}
			return stringPlus.Value;
		}
		public static string GetFieldstrlistAdd(List<ColumnInfo> keys, bool IdentityisPrior)
		{
			StringPlus stringPlus = new StringPlus();
			ColumnInfo identityKey = CodeCommon.GetIdentityKey(keys);
			if (IdentityisPrior && identityKey != null)
			{
				stringPlus.Append(identityKey.ColumnName);
			}
			else
			{
				foreach (ColumnInfo current in keys)
				{
					if (current.IsPrimaryKey || !current.IsIdentity)
					{
						stringPlus.Append(current.ColumnName + "+");
					}
				}
				stringPlus.DelLastChar("+");
			}
			return stringPlus.Value;
		}
		public static string GetWhereExpression(List<ColumnInfo> keys, bool IdentityisPrior)
		{
			StringPlus stringPlus = new StringPlus();
			ColumnInfo identityKey = CodeCommon.GetIdentityKey(keys);
			if (IdentityisPrior && identityKey != null)
			{
				if (CodeCommon.IsAddMark(identityKey.TypeName))
				{
					stringPlus.Append(identityKey.ColumnName + "='\"+" + identityKey.ColumnName + "+\"'");
				}
				else
				{
					stringPlus.Append(identityKey.ColumnName + "=\"+" + identityKey.ColumnName + "+\"");
				}
			}
			else
			{
				foreach (ColumnInfo current in keys)
				{
					if (current.IsPrimaryKey || !current.IsIdentity)
					{
						if (CodeCommon.IsAddMark(current.TypeName))
						{
							stringPlus.Append(current.ColumnName + "='\"+" + current.ColumnName + "+\"' and ");
						}
						else
						{
							stringPlus.Append(current.ColumnName + "=\"+" + current.ColumnName + "+\" and ");
						}
					}
				}
				stringPlus.DelLastChar("and");
			}
			return stringPlus.Value;
		}
		public static string GetModelWhereExpression(List<ColumnInfo> keys, bool IdentityisPrior)
		{
			StringPlus stringPlus = new StringPlus();
			ColumnInfo identityKey = CodeCommon.GetIdentityKey(keys);
			if (IdentityisPrior && identityKey != null)
			{
				if (CodeCommon.IsAddMark(identityKey.TypeName))
				{
					stringPlus.Append(identityKey.ColumnName + "='\"+ model." + identityKey.ColumnName + "+\"'");
				}
				else
				{
					stringPlus.Append(identityKey.ColumnName + "=\"+ model." + identityKey.ColumnName + "+\"");
				}
			}
			else
			{
				foreach (ColumnInfo current in keys)
				{
					if (current.IsPrimaryKey || !current.IsIdentity)
					{
						if (CodeCommon.IsAddMark(current.TypeName))
						{
							stringPlus.Append(current.ColumnName + "='\"+ model." + current.ColumnName + "+\"' and ");
						}
						else
						{
							stringPlus.Append(current.ColumnName + "=\"+ model." + current.ColumnName + "+\" and ");
						}
					}
				}
				stringPlus.DelLastChar("and");
			}
			return stringPlus.Value;
		}
		public static string CutDescText(string descText, int cutLen, string ReplaceText)
		{
			string result;
			if (descText.Trim().Length > 0)
			{
				int val = descText.IndexOf(";");
				int val2 = descText.IndexOf("，");
				int val3 = descText.IndexOf(",");
				int num = Math.Min(val, val2);
				if (num < 0)
				{
					num = Math.Max(val, val2);
				}
				num = Math.Min(num, val3);
				if (num < 0)
				{
					num = Math.Max(val, val2);
				}
				if (num > 0)
				{
					result = descText.Trim().Substring(0, num);
				}
				else
				{
					if (descText.Trim().Length > cutLen)
					{
						result = descText.Trim().Substring(0, cutLen);
					}
					else
					{
						result = descText.Trim();
					}
				}
			}
			else
			{
				result = ReplaceText;
			}
			return result;
		}
		public static int CompareByintOrder(ColumnInfo x, ColumnInfo y)
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
		public static int CompareByOrder(ColumnInfo x, ColumnInfo y)
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
	}
}
