using Maticsoft.BuilderModel;
using Maticsoft.CodeHelper;
using Maticsoft.IDBO;
using Maticsoft.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;
namespace Maticsoft.CodeBuild
{
	public class BuilderFrameOne : BuilderFrame
	{
        private Dictionary<string, int> dtCollection;
		private string cmcfgfile = Application.StartupPath + "\\cmcfg.ini";
		private INIFile cfgfile;
		private string _procprefix;
		public string preParameter
		{
			get
			{
				string dbType;
				if ((dbType = this.dbobj.DbType) != null)
				{
					if (dbType == "SQL2000" || dbType == "SQL2005" || dbType == "SQL2008" || dbType == "SQL2012")
					{
						return "@";
					}
					if (dbType == "Oracle")
					{
						return ":";
					}
				}
				return "@";
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
		public Hashtable Languagelist
		{
			get
			{
				return Language.LoadFromCfg("BuilderFrameOne.lan");
			}
		}
		public BuilderFrameOne(IDbObject idbobj, string dbName, string tableName, string modelName, List<ColumnInfo> fieldlist, List<ColumnInfo> keys, string nameSpace, string folder, string dbHelperName)
		{
			this.dbobj = idbobj;
			base.DbName = dbName;
			base.TableName = tableName;
			base.ModelName = modelName;
			base.NameSpace = nameSpace;
			base.Folder = folder;
			base.DbHelperName = dbHelperName;
			this._dbtype = idbobj.DbType;
			base.Fieldlist = fieldlist;
			base.Keys = keys;
			foreach (ColumnInfo current in keys)
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
		}
		public string GetWhereExpression(List<ColumnInfo> keys)
		{
			StringPlus stringPlus = new StringPlus();
			foreach (ColumnInfo current in keys)
			{
				stringPlus.Append(string.Concat(new string[]
				{
					current.ColumnName,
					"=",
					this.preParameter,
					current.ColumnName,
					" and "
				}));
			}
			stringPlus.DelLastChar("and");
			return stringPlus.Value;
		}
		public string GetPreParameter(List<ColumnInfo> keys)
		{
			StringPlus stringPlus = new StringPlus();
			StringPlus stringPlus2 = new StringPlus();
			stringPlus.AppendSpaceLine(3, base.DbParaHead + "Parameter[] parameters = {");
			int num = 0;
			foreach (ColumnInfo current in keys)
			{
				stringPlus.AppendSpaceLine(5, string.Concat(new string[]
				{
					"new ",
					base.DbParaHead,
					"Parameter(\"",
					this.preParameter,
					current.ColumnName,
					"\", ",
					base.DbParaDbType,
					".",
					CodeCommon.DbTypeLength(this.dbobj.DbType, current.TypeName, ""),
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
			stringPlus.DelLastComma();
			stringPlus.AppendLine("};");
			stringPlus.Append(stringPlus2.Value);
			return stringPlus.Value;
		}
		public string GetCode(string DALtype, bool Maxid, bool Exists, bool Add, bool Update, bool Delete, bool GetModel, bool List, string procPrefix)
		{
			this.cfgfile = new INIFile(this.cmcfgfile);
			DALtype = "Param";
			string text = this.cfgfile.IniReadValue("BuilderOne", DALtype.Trim());
			if (text != null && text != "")
			{
				DALtype = text;
			}
			this.ProcPrefix = procPrefix;
			StringPlus stringPlus = new StringPlus();
			stringPlus.AppendLine("using System;");
			stringPlus.AppendLine("using System.Data;");
			stringPlus.AppendLine("using System.Text;");
			string dbType;
			switch (dbType = this.dbobj.DbType)
			{
			case "SQL2005":
			case "SQL2000":
			case "SQL2008":
			case "SQL2012":
				stringPlus.AppendLine("using System.Data.SqlClient;");
				break;
			case "Oracle":
				stringPlus.AppendLine("using System.Data.OracleClient;");
				break;
			case "MySQL":
				stringPlus.AppendLine("using MySql.Data.MySqlClient;");
				break;
			case "OleDb":
				stringPlus.AppendLine("using System.Data.OleDb;");
				break;
			}
			stringPlus.AppendLine("using Maticsoft.DBUtility;//Please add references");
			stringPlus.AppendLine("namespace " + base.NameSpace);
			stringPlus.AppendLine("{");
			stringPlus.AppendSpaceLine(1, "/// <summary>");
			stringPlus.AppendSpaceLine(1, "/// 类" + base.ModelName + "。");
			stringPlus.AppendSpaceLine(1, "/// </summary>");
			stringPlus.AppendSpaceLine(1, "[Serializable]");
			stringPlus.AppendSpaceLine(1, "public partial class " + base.ModelName);
			stringPlus.AppendSpaceLine(1, "{");
			stringPlus.AppendSpaceLine(2, "public " + base.ModelName + "()");
			stringPlus.AppendSpaceLine(2, "{}");
			stringPlus.AppendLine(new Maticsoft.BuilderModel.BuilderModel
            {
				ModelName = base.ModelName,
				NameSpace = base.NameSpace,
				Fieldlist = base.Fieldlist,
				Modelpath = base.Modelpath
			}.CreatModelMethod());
			stringPlus.AppendLine("");
			stringPlus.AppendSpaceLine(2, "#region  Method");
			string a;
			if ((a = DALtype) != null)
			{
				if (!(a == "sql"))
				{
					if (!(a == "Param"))
					{
						if (a == "Proc")
						{
							stringPlus.Append(this.CreatConstructorProc() + "\r\n");
							if (Maxid)
							{
								stringPlus.Append(this.CreatGetMaxIDProc() + "\r\n");
							}
							if (Exists)
							{
								stringPlus.Append(this.CreatExistsProc() + "\r\n");
							}
							if (Add)
							{
								stringPlus.Append(this.CreatAddProc() + "\r\n");
							}
							if (Update)
							{
								stringPlus.Append(this.CreatUpdateProc() + "\r\n");
							}
							if (Delete)
							{
								stringPlus.Append(this.CreatDeleteProc() + "\r\n");
							}
							if (GetModel)
							{
								stringPlus.Append(this.CreatGetModelProc() + "\r\n");
							}
							if (List)
							{
								stringPlus.Append(this.CreatGetListProc() + "\r\n");
							}
							if (List)
							{
								stringPlus.Append(this.CreatGetListByPageProc() + "\r\n");
								goto IL_518;
							}
							goto IL_518;
						}
					}
					else
					{
						stringPlus.Append(this.CreatConstructorParam() + "\r\n");
						if (Maxid)
						{
							stringPlus.Append(this.CreatGetMaxIDParam() + "\r\n");
						}
						if (Exists)
						{
							stringPlus.Append(this.CreatExistsParam() + "\r\n");
						}
						if (Add)
						{
							stringPlus.Append(this.CreatAddParam() + "\r\n");
						}
						if (Update)
						{
							stringPlus.Append(this.CreatUpdateParam() + "\r\n");
						}
						if (Delete)
						{
							stringPlus.Append(this.CreatDeleteParam() + "\r\n");
						}
						if (GetModel)
						{
							stringPlus.Append(this.CreatGetModelParam() + "\r\n");
						}
						if (List)
						{
							stringPlus.Append(this.CreatGetListParam() + "\r\n");
							goto IL_518;
						}
						goto IL_518;
					}
				}
				else
				{
					stringPlus.AppendLine(this.CreatConstructorSQL());
					if (Maxid)
					{
						stringPlus.AppendLine(this.CreatGetMaxIDSQL());
					}
					if (Exists)
					{
						stringPlus.AppendLine(this.CreatExistsSQL());
					}
					if (Add)
					{
						stringPlus.AppendLine(this.CreatAddSQL());
					}
					if (Update)
					{
						stringPlus.AppendLine(this.CreatUpdateSQL());
					}
					if (Delete)
					{
						stringPlus.AppendLine(this.CreatDeleteSQL());
					}
					if (GetModel)
					{
						stringPlus.AppendLine(this.CreatGetModelSQL());
					}
					if (List)
					{
						stringPlus.AppendLine(this.CreatGetListSQL());
						goto IL_518;
					}
					goto IL_518;
				}
			}
			stringPlus.AppendSpaceLine(2, "//暂不支持该方式。\r\n");
			IL_518:
			stringPlus.AppendSpaceLine(2, "#endregion  Method");
			stringPlus.AppendSpaceLine(1, "}");
			stringPlus.AppendLine("}");
			stringPlus.AppendLine("");
			return stringPlus.ToString();
		}
		private string CreatGetMaxIDSQL()
		{
			StringPlus stringPlus = new StringPlus();
			if (base.Keys.Count > 0)
			{
				foreach (ColumnInfo current in base.Keys)
				{
					if (CodeCommon.DbTypeToCS(current.TypeName) == "int")
					{
						string columnName = current.ColumnName;
						if (current.IsPrimaryKey)
						{
							stringPlus.AppendLine("");
							stringPlus.AppendSpaceLine(2, "/// <summary>");
							stringPlus.AppendSpaceLine(2, "/// " + this.Languagelist["summaryGetMaxId"].ToString());
							stringPlus.AppendSpaceLine(2, "/// </summary>");
							stringPlus.AppendSpaceLine(2, "public int GetMaxId()");
							stringPlus.AppendSpaceLine(2, "{\r\n");
							stringPlus.AppendSpaceLine(2, string.Concat(new string[]
							{
								"return ",
								base.DbHelperName,
								".GetMaxID(\"",
								columnName,
								"\", \"",
								base.TableName,
								"\"); "
							}));
							stringPlus.AppendSpaceLine(2, "}");
							break;
						}
					}
				}
			}
			return stringPlus.ToString();
		}
		private string CreatExistsSQL()
		{
			StringPlus stringPlus = new StringPlus();
			if (base.Keys.Count > 0)
			{
				stringPlus.AppendLine("");
				stringPlus.AppendSpaceLine(2, "/// <summary>");
				stringPlus.AppendSpaceLine(2, "/// " + this.Languagelist["summaryExists"].ToString());
				stringPlus.AppendSpaceLine(2, "/// </summary>");
				stringPlus.AppendSpaceLine(2, "public bool Exists(" + CodeCommon.GetInParameter(base.Keys, false) + ")");
				stringPlus.AppendSpaceLine(2, "{");
				stringPlus.AppendSpaceLine(3, "StringBuilder strSql=new StringBuilder();");
				stringPlus.AppendSpace(3, "strSql.Append(\"select count(1) from [" + base.TableName + "]");
				stringPlus.AppendSpaceLine(0, " where " + CodeCommon.GetWhereExpression(base.Keys, false) + "\" );");
				stringPlus.AppendSpaceLine(3, "return " + base.DbHelperName + ".Exists(strSql.ToString());");
				stringPlus.AppendSpace(2, "}");
			}
			return stringPlus.ToString();
		}
		private string CreatAddSQL()
		{
			StringBuilder stringBuilder = new StringBuilder();
			StringBuilder stringBuilder2 = new StringBuilder();
			StringBuilder stringBuilder3 = new StringBuilder();
			stringBuilder.Append("\r\n");
			stringBuilder.Append(base.Space(2) + "/// <summary>\r\n");
			stringBuilder.Append(base.Space(2) + "/// " + this.Languagelist["summaryadd"].ToString() + "\r\n");
			stringBuilder.Append(base.Space(2) + "/// </summary>\r\n");
			string str = "void";
			if ((this.dbobj.DbType == "SQL2000" || this.dbobj.DbType == "SQL2005" || this.dbobj.DbType == "SQL2008" || this.dbobj.DbType == "SQL2012") && base.IsHasIdentity)
			{
				str = "int";
			}
			stringBuilder.Append(base.Space(2) + "public " + str + " Add()\r\n");
			stringBuilder.Append(base.Space(2) + "{\r\n");
			stringBuilder.Append(base.Space(3) + "StringBuilder strSql=new StringBuilder();\r\n");
			stringBuilder.Append(base.Space(3) + "strSql.Append(\"insert into [" + base.TableName + "](\");\r\n");
			stringBuilder2.Append(base.Space(3) + "strSql.Append(\"");
			foreach (ColumnInfo current in base.Fieldlist)
			{
				string columnName = current.ColumnName;
				string typeName = current.TypeName;
				bool arg_1AD_0 = current.IsIdentity;
				if (current.IsIdentity)
				{
					str = CodeCommon.DbTypeToCS(typeName);
				}
				else
				{
					stringBuilder2.Append(columnName + ",");
					if (CodeCommon.IsAddMark(typeName.Trim()))
					{
						stringBuilder3.Append(base.Space(3) + "strSql.Append(\"'\"+" + columnName + "+\"',\");\r\n");
					}
					else
					{
						stringBuilder3.Append(base.Space(3) + "strSql.Append(\"\"+" + columnName + "+\",\");\r\n");
					}
				}
			}
			stringBuilder2.Remove(stringBuilder2.Length - 1, 1);
			stringBuilder3.Remove(stringBuilder3.Length - 6, 1);
			stringBuilder2.Append("\");\r\n");
			stringBuilder.Append(stringBuilder2.ToString());
			stringBuilder.Append(base.Space(3) + "strSql.Append(\")\");\r\n");
			stringBuilder.Append(base.Space(3) + "strSql.Append(\" values (\");\r\n");
			stringBuilder.Append(stringBuilder3.ToString());
			stringBuilder.Append(base.Space(3) + "strSql.Append(\")\");\r\n");
			if ((this.dbobj.DbType == "SQL2000" || this.dbobj.DbType == "SQL2005" || this.dbobj.DbType == "SQL2008" || this.dbobj.DbType == "SQL2012") && base.IsHasIdentity)
			{
				stringBuilder.Append(CodeCommon.Space(3) + "strSql.Append(\";select @@IDENTITY\");\r\n");
				stringBuilder.Append(CodeCommon.Space(3) + "object obj = " + base.DbHelperName + ".GetSingle(strSql.ToString());\r\n");
				stringBuilder.Append(CodeCommon.Space(3) + "if (obj == null)\r\n");
				stringBuilder.Append(CodeCommon.Space(3) + "{\r\n");
				stringBuilder.Append(CodeCommon.Space(4) + "return 0;\r\n");
				stringBuilder.Append(CodeCommon.Space(3) + "}\r\n");
				stringBuilder.Append(CodeCommon.Space(3) + "else\r\n");
				stringBuilder.Append(CodeCommon.Space(3) + "{\r\n");
				stringBuilder.Append(CodeCommon.Space(4) + "return Convert.ToInt32(obj);\r\n");
				stringBuilder.Append(CodeCommon.Space(3) + "}\r\n");
			}
			else
			{
				stringBuilder.Append(CodeCommon.Space(3) + base.DbHelperName + ".ExecuteSql(strSql.ToString());\r\n");
			}
			stringBuilder.Append(base.Space(2) + "}");
			return stringBuilder.ToString();
		}
		private string CreatUpdateSQL()
		{
			StringPlus stringPlus = new StringPlus();
			stringPlus.AppendLine();
			stringPlus.AppendSpaceLine(2, "/// <summary>");
			stringPlus.AppendSpaceLine(2, "/// " + this.Languagelist["summaryUpdate"].ToString());
			stringPlus.AppendSpaceLine(2, "/// </summary>");
			stringPlus.AppendSpaceLine(2, "public bool Update()");
			stringPlus.AppendSpaceLine(2, "{");
			stringPlus.AppendSpaceLine(3, "StringBuilder strSql=new StringBuilder();");
			stringPlus.AppendSpaceLine(3, "strSql.Append(\"update [" + base.TableName + "] set \");");
			foreach (ColumnInfo current in base.Fieldlist)
			{
				string columnName = current.ColumnName;
				string typeName = current.TypeName;
				string arg_C0_0 = current.Length;
				bool arg_C7_0 = current.IsIdentity;
				bool arg_CE_0 = current.IsPrimaryKey;
				if (!current.IsIdentity && !current.IsPrimaryKey && !base.Keys.Contains(current))
				{
					if (this.dbobj.DbType == "Oracle" && (typeName.ToLower() == "date" || typeName.ToLower() == "datetime"))
					{
						stringPlus.AppendSpaceLine(3, string.Concat(new string[]
						{
							"strSql.Append(\"",
							columnName,
							"=to_date('\" + ",
							columnName,
							".ToString() + \"','YYYY-MM-DD HH24:MI:SS'),\");"
						}));
					}
					else
					{
						if (typeName.ToLower() == "bit")
						{
							stringPlus.AppendSpaceLine(3, string.Concat(new string[]
							{
								"strSql.Append(\"",
								columnName,
								"=\"+ (",
								columnName,
								"? 1 : 0) +\",\");"
							}));
						}
						else
						{
							if (CodeCommon.IsAddMark(typeName.Trim()))
							{
								stringPlus.AppendSpaceLine(3, string.Concat(new string[]
								{
									"strSql.Append(\"",
									columnName,
									"='\"+",
									columnName,
									"+\"',\");"
								}));
							}
							else
							{
								stringPlus.AppendSpaceLine(3, string.Concat(new string[]
								{
									"strSql.Append(\"",
									columnName,
									"=\"+",
									columnName,
									"+\",\");"
								}));
							}
						}
					}
				}
			}
			int start = stringPlus.Value.LastIndexOf(",");
			stringPlus.Remove(start, 1);
			stringPlus.AppendSpaceLine(3, "strSql.Append(\" where " + CodeCommon.GetWhereExpression(base.Keys, true) + "\");");
			stringPlus.AppendSpaceLine(3, "int rows=" + base.DbHelperName + ".ExecuteSql(strSql.ToString());");
			stringPlus.AppendSpaceLine(3, "if (rows > 0)");
			stringPlus.AppendSpaceLine(3, "{");
			stringPlus.AppendSpaceLine(4, "return true;");
			stringPlus.AppendSpaceLine(3, "}");
			stringPlus.AppendSpaceLine(3, "else");
			stringPlus.AppendSpaceLine(3, "{");
			stringPlus.AppendSpaceLine(4, "return false;");
			stringPlus.AppendSpaceLine(3, "}");
			stringPlus.AppendSpaceLine(2, "}");
			return stringPlus.ToString();
		}
		private string CreatDeleteSQL()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("\r\n");
			stringBuilder.Append(base.Space(2) + "/// <summary>\r\n");
			stringBuilder.Append(base.Space(2) + "/// " + this.Languagelist["summaryDelete"].ToString() + "\r\n");
			stringBuilder.Append(base.Space(2) + "/// </summary>\r\n");
			stringBuilder.Append(base.Space(2) + "public bool Delete(" + CodeCommon.GetInParameter(base.Keys, true) + ")\r\n");
			stringBuilder.Append(base.Space(2) + "{\r\n");
			stringBuilder.Append(base.Space(3) + "StringBuilder strSql=new StringBuilder();\r\n");
			stringBuilder.Append(base.Space(3) + "strSql.Append(\"delete from [" + base.TableName + "] \");\r\n");
			if (CodeCommon.GetWhereExpression(base.Keys, true).Length > 0)
			{
				stringBuilder.Append(base.Space(3) + "strSql.Append(\" where " + CodeCommon.GetWhereExpression(base.Keys, true) + "\");\r\n");
			}
			else
			{
				stringBuilder.Append(base.Space(3) + "//strSql.Append(\" where 条件);\r\n");
			}
			stringBuilder.Append(base.Space(3) + "int rows=" + base.DbHelperName + ".ExecuteSql(strSql.ToString());\r\n");
			stringBuilder.Append(base.Space(3) + "if (rows > 0)");
			stringBuilder.Append(base.Space(3) + "{");
			stringBuilder.Append(base.Space(4) + "return true;");
			stringBuilder.Append(base.Space(3) + "}");
			stringBuilder.Append(base.Space(3) + "else");
			stringBuilder.Append(base.Space(3) + "{");
			stringBuilder.Append(base.Space(4) + "return false;");
			stringBuilder.Append(base.Space(3) + "}");
			stringBuilder.Append(base.Space(2) + "}");
			return stringBuilder.ToString();
		}
		private string CreatConstructorSQL()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("\r\n");
			stringBuilder.Append(base.Space(2) + "/// <summary>\r\n");
			stringBuilder.Append(base.Space(2) + "/// " + this.Languagelist["summaryGetModel"].ToString() + "\r\n");
			stringBuilder.Append(base.Space(2) + "/// </summary>\r\n");
			stringBuilder.Append(string.Concat(new string[]
			{
				base.Space(2),
				"public ",
				base.ModelName,
				"(",
				CodeCommon.GetInParameter(base.Keys, true),
				")\r\n"
			}));
			stringBuilder.Append(base.Space(2) + "{\r\n");
			stringBuilder.Append(base.Space(3) + "StringBuilder strSql=new StringBuilder();\r\n");
			stringBuilder.Append(base.Space(3) + "strSql.Append(\"select  \");\r\n");
			stringBuilder.Append(base.Space(3) + "strSql.Append(\"" + base.Fieldstrlist + " \");\r\n");
			stringBuilder.Append(base.Space(3) + "strSql.Append(\" from [" + base.TableName + "] \");\r\n");
			if (CodeCommon.GetWhereExpression(base.Keys, true).Length > 0)
			{
				stringBuilder.Append(base.Space(3) + "strSql.Append(\" where " + CodeCommon.GetWhereExpression(base.Keys, true) + "\");\r\n");
			}
			else
			{
				stringBuilder.Append(base.Space(3) + "//strSql.Append(\" where 条件);\r\n");
			}
			stringBuilder.Append(base.Space(3) + "DataSet ds=" + base.DbHelperName + ".Query(strSql.ToString());\r\n");
			stringBuilder.Append(base.Space(3) + "if(ds.Tables[0].Rows.Count>0)\r\n");
			stringBuilder.Append(base.Space(3) + "{\r\n");
			foreach (ColumnInfo current in base.Fieldlist)
			{
				string columnName = current.ColumnName;
				string typeName = current.TypeName;
				stringBuilder.Append(string.Concat(new string[]
				{
					base.Space(4),
					"if(ds.Tables[0].Rows[0][\"",
					columnName,
					"\"]!=null && ds.Tables[0].Rows[0][\"",
					columnName,
					"\"].ToString()!=\"\")\r\n"
				}));
				stringBuilder.Append(base.Space(4) + "{\r\n");
				string key;
				if ((key = CodeCommon.DbTypeToCS(typeName)) == null)
				{
					goto IL_5A0;
				}
				if (dtCollection == null)
				{
                    dtCollection = new Dictionary<string, int>(6)
					{

						{
							"int",
							0
						},

						{
							"decimal",
							1
						},

						{
							"DateTime",
							2
						},

						{
							"string",
							3
						},

						{
							"bool",
							4
						},

						{
							"byte[]",
							5
						}
					};
				}
				int num;
				if (!dtCollection.TryGetValue(key, out num))
				{
					goto IL_5A0;
				}
				switch (num)
				{
				case 0:
					stringBuilder.Append(string.Concat(new string[]
					{
						base.Space(5),
						"this.",
						columnName,
						"=int.Parse(ds.Tables[0].Rows[0][\"",
						columnName,
						"\"].ToString());\r\n"
					}));
					break;
				case 1:
					stringBuilder.Append(string.Concat(new string[]
					{
						base.Space(5),
						"this.",
						columnName,
						"=decimal.Parse(ds.Tables[0].Rows[0][\"",
						columnName,
						"\"].ToString());\r\n"
					}));
					break;
				case 2:
					stringBuilder.Append(string.Concat(new string[]
					{
						base.Space(5),
						"this.",
						columnName,
						"=DateTime.Parse(ds.Tables[0].Rows[0][\"",
						columnName,
						"\"].ToString());\r\n"
					}));
					break;
				case 3:
					stringBuilder.Append(string.Concat(new string[]
					{
						base.Space(5),
						"this.",
						columnName,
						"=ds.Tables[0].Rows[0][\"",
						columnName,
						"\"].ToString();\r\n"
					}));
					break;
				case 4:
					stringBuilder.Append(string.Concat(new string[]
					{
						base.Space(5),
						"if((ds.Tables[0].Rows[0][\"",
						columnName,
						"\"].ToString()==\"1\")||(ds.Tables[0].Rows[0][\"",
						columnName,
						"\"].ToString().ToLower()==\"true\"))\r\n"
					}));
					stringBuilder.Append(base.Space(5) + "{\r\n");
					stringBuilder.Append(base.Space(6) + "this." + columnName + "=true;\r\n");
					stringBuilder.Append(base.Space(5) + "}\r\n");
					stringBuilder.Append(base.Space(5) + "else\r\n");
					stringBuilder.Append(base.Space(5) + "{\r\n");
					stringBuilder.Append(base.Space(6) + "this." + columnName + "=false;\r\n");
					stringBuilder.Append(base.Space(5) + "}\r\n");
					break;
				case 5:
					stringBuilder.Append(string.Concat(new string[]
					{
						base.Space(5),
						"this.",
						columnName,
						"=(byte[])ds.Tables[0].Rows[0][\"",
						columnName,
						"\"];\r\n"
					}));
					break;
				default:
					goto IL_5A0;
				}
				IL_5E6:
				stringBuilder.Append(base.Space(4) + "}\r\n");
				continue;
				IL_5A0:
				stringBuilder.Append(string.Concat(new string[]
				{
					base.Space(5),
					"this.",
					columnName,
					"=ds.Tables[0].Rows[0][\"",
					columnName,
					"\"].ToString();\r\n"
				}));
				goto IL_5E6;
			}
			stringBuilder.Append(base.Space(3) + "}\r\n");
			stringBuilder.Append(base.Space(2) + "}");
			return stringBuilder.ToString();
		}
		private string CreatGetModelSQL()
		{
			StringPlus stringPlus = new StringPlus();
			stringPlus.AppendLine();
			stringPlus.AppendSpaceLine(2, "/// <summary>");
			stringPlus.AppendSpaceLine(2, "/// " + this.Languagelist["summaryGetModel"].ToString());
			stringPlus.AppendSpaceLine(2, "/// </summary>");
			stringPlus.AppendSpaceLine(2, "public void GetModel(" + CodeCommon.GetInParameter(base.Keys, true) + ")");
			stringPlus.AppendSpaceLine(2, "{\r\n");
			stringPlus.AppendSpaceLine(3, "StringBuilder strSql=new StringBuilder();");
			stringPlus.AppendSpace(3, "strSql.Append(\"select ");
			if (this.dbobj.DbType == "SQL2005" || this.dbobj.DbType == "SQL2000" || this.dbobj.DbType == "SQL2008" || this.dbobj.DbType == "SQL2012")
			{
				stringPlus.Append(" top 1 ");
			}
			stringPlus.AppendLine(" \");");
			stringPlus.AppendSpaceLine(3, "strSql.Append(\" " + base.Fieldstrlist + " \");");
			stringPlus.AppendSpaceLine(3, "strSql.Append(\" from [" + base.TableName + "] \");");
			if (CodeCommon.GetWhereExpression(base.Keys, true).Length > 0)
			{
				stringPlus.AppendSpaceLine(3, "strSql.Append(\" where " + CodeCommon.GetWhereExpression(base.Keys, true) + "\" );");
			}
			else
			{
				stringPlus.AppendSpaceLine(3, "//strSql.Append(\" where 条件);");
			}
			stringPlus.AppendSpaceLine(3, "DataSet ds=" + base.DbHelperName + ".Query(strSql.ToString());");
			stringPlus.AppendSpaceLine(3, "if(ds.Tables[0].Rows.Count>0)");
			stringPlus.AppendSpaceLine(3, "{");
			foreach (ColumnInfo current in base.Fieldlist)
			{
				string columnName = current.ColumnName;
				string typeName = current.TypeName;
				string key;
				switch (key = CodeCommon.DbTypeToCS(typeName))
				{
				case "int":
					stringPlus.AppendSpaceLine(4, string.Concat(new string[]
					{
						"if(ds.Tables[0].Rows[0][\"",
						columnName,
						"\"]!=null && ds.Tables[0].Rows[0][\"",
						columnName,
						"\"].ToString()!=\"\")"
					}));
					stringPlus.AppendSpaceLine(4, "{");
					stringPlus.AppendSpaceLine(5, columnName + "=int.Parse(ds.Tables[0].Rows[0][\"" + columnName + "\"].ToString());");
					stringPlus.AppendSpaceLine(4, "}");
					continue;
				case "long":
					stringPlus.AppendSpaceLine(4, string.Concat(new string[]
					{
						"if(ds.Tables[0].Rows[0][\"",
						columnName,
						"\"]!=null && ds.Tables[0].Rows[0][\"",
						columnName,
						"\"].ToString()!=\"\")"
					}));
					stringPlus.AppendSpaceLine(4, "{");
					stringPlus.AppendSpaceLine(5, columnName + "=long.Parse(ds.Tables[0].Rows[0][\"" + columnName + "\"].ToString());");
					stringPlus.AppendSpaceLine(4, "}");
					continue;
				case "decimal":
					stringPlus.AppendSpaceLine(4, string.Concat(new string[]
					{
						"if(ds.Tables[0].Rows[0][\"",
						columnName,
						"\"]!=null && ds.Tables[0].Rows[0][\"",
						columnName,
						"\"].ToString()!=\"\")"
					}));
					stringPlus.AppendSpaceLine(4, "{");
					stringPlus.AppendSpaceLine(5, columnName + "=decimal.Parse(ds.Tables[0].Rows[0][\"" + columnName + "\"].ToString());");
					stringPlus.AppendSpaceLine(4, "}");
					continue;
				case "float":
					stringPlus.AppendSpaceLine(4, string.Concat(new string[]
					{
						"if(ds.Tables[0].Rows[0][\"",
						columnName,
						"\"]!=null && ds.Tables[0].Rows[0][\"",
						columnName,
						"\"].ToString()!=\"\")"
					}));
					stringPlus.AppendSpaceLine(4, "{");
					stringPlus.AppendSpaceLine(5, columnName + "=float.Parse(ds.Tables[0].Rows[0][\"" + columnName + "\"].ToString());");
					stringPlus.AppendSpaceLine(4, "}");
					continue;
				case "DateTime":
					stringPlus.AppendSpaceLine(4, string.Concat(new string[]
					{
						"if(ds.Tables[0].Rows[0][\"",
						columnName,
						"\"]!=null && ds.Tables[0].Rows[0][\"",
						columnName,
						"\"].ToString()!=\"\")"
					}));
					stringPlus.AppendSpaceLine(4, "{");
					stringPlus.AppendSpaceLine(5, columnName + "=DateTime.Parse(ds.Tables[0].Rows[0][\"" + columnName + "\"].ToString());");
					stringPlus.AppendSpaceLine(4, "}");
					continue;
				case "string":
					stringPlus.AppendSpaceLine(4, "if(ds.Tables[0].Rows[0][\"" + columnName + "\"]!=null)");
					stringPlus.AppendSpaceLine(4, "{");
					stringPlus.AppendSpaceLine(5, columnName + "=ds.Tables[0].Rows[0][\"" + columnName + "\"].ToString();");
					stringPlus.AppendSpaceLine(4, "}");
					continue;
				case "bool":
					stringPlus.AppendSpaceLine(4, string.Concat(new string[]
					{
						"if(ds.Tables[0].Rows[0][\"",
						columnName,
						"\"]!=null && ds.Tables[0].Rows[0][\"",
						columnName,
						"\"].ToString()!=\"\")"
					}));
					stringPlus.AppendSpaceLine(4, "{");
					stringPlus.AppendSpaceLine(5, string.Concat(new string[]
					{
						"if((ds.Tables[0].Rows[0][\"",
						columnName,
						"\"].ToString()==\"1\")||(ds.Tables[0].Rows[0][\"",
						columnName,
						"\"].ToString().ToLower()==\"true\"))"
					}));
					stringPlus.AppendSpaceLine(5, "{");
					stringPlus.AppendSpaceLine(6, columnName + "=true;");
					stringPlus.AppendSpaceLine(5, "}");
					stringPlus.AppendSpaceLine(5, "else");
					stringPlus.AppendSpaceLine(5, "{");
					stringPlus.AppendSpaceLine(6, columnName + "=false;");
					stringPlus.AppendSpaceLine(5, "}");
					stringPlus.AppendSpaceLine(4, "}");
					continue;
				case "byte[]":
					stringPlus.AppendSpaceLine(4, string.Concat(new string[]
					{
						"if(ds.Tables[0].Rows[0][\"",
						columnName,
						"\"]!=null && ds.Tables[0].Rows[0][\"",
						columnName,
						"\"].ToString()!=\"\")"
					}));
					stringPlus.AppendSpaceLine(4, "{");
					stringPlus.AppendSpaceLine(5, columnName + "=(byte[])ds.Tables[0].Rows[0][\"" + columnName + "\"];");
					stringPlus.AppendSpaceLine(4, "}");
					continue;
				case "Guid":
					stringPlus.AppendSpaceLine(4, string.Concat(new string[]
					{
						"if(ds.Tables[0].Rows[0][\"",
						columnName,
						"\"]!=null && ds.Tables[0].Rows[0][\"",
						columnName,
						"\"].ToString()!=\"\")"
					}));
					stringPlus.AppendSpaceLine(4, "{");
					stringPlus.AppendSpaceLine(5, columnName + "=new Guid(ds.Tables[0].Rows[0][\"" + columnName + "\"].ToString());");
					stringPlus.AppendSpaceLine(4, "}");
					continue;
				}
				stringPlus.AppendSpaceLine(5, string.Concat(new string[]
				{
					"//",
					columnName,
					"=ds.Tables[0].Rows[0][\"",
					columnName,
					"\"].ToString();"
				}));
			}
			stringPlus.AppendSpaceLine(3, "}");
			stringPlus.AppendSpaceLine(2, "}");
			return stringPlus.ToString();
		}
		private string CreatGetListSQL()
		{
			List<ColumnInfo> columnList = this.dbobj.GetColumnList(base.DbName, base.TableName);
			DataTable columnInfoDt = CodeCommon.GetColumnInfoDt(columnList);
			StringPlus stringPlus = new StringPlus();
			base.GetFieldslist(columnInfoDt);
			stringPlus.AppendSpaceLine(2, "/// <summary>");
			stringPlus.AppendSpaceLine(2, "/// " + this.Languagelist["summaryGetList"].ToString());
			stringPlus.AppendSpaceLine(2, "/// </summary>");
			stringPlus.AppendSpaceLine(2, "public DataSet GetList(string strWhere)");
			stringPlus.AppendSpaceLine(2, "{");
			stringPlus.AppendSpaceLine(3, "StringBuilder strSql=new StringBuilder();");
			stringPlus.AppendSpace(3, "strSql.Append(\"select ");
			stringPlus.AppendLine("* \");");
			stringPlus.AppendSpaceLine(3, "strSql.Append(\" FROM [" + base.TableName + "] \");");
			stringPlus.AppendSpaceLine(3, "if(strWhere.Trim()!=\"\")");
			stringPlus.AppendSpaceLine(3, "{\r\n");
			stringPlus.AppendSpaceLine(4, "strSql.Append(\" where \"+strWhere);");
			stringPlus.AppendSpaceLine(3, "}");
			stringPlus.AppendSpaceLine(3, "return " + base.DbHelperName + ".Query(strSql.ToString());");
			stringPlus.AppendSpaceLine(2, "}");
			return stringPlus.Value;
		}
		private string CreatGetMaxIDProc()
		{
			StringPlus stringPlus = new StringPlus();
			if (base.Keys.Count > 0)
			{
				foreach (ColumnInfo current in base.Keys)
				{
					if (CodeCommon.DbTypeToCS(current.TypeName) == "int")
					{
						string columnName = current.ColumnName;
						if (current.IsPrimaryKey)
						{
							stringPlus.AppendLine("");
							stringPlus.AppendSpaceLine(2, "/// <summary>");
							stringPlus.AppendSpaceLine(2, "/// " + this.Languagelist["summaryGetMaxId"].ToString());
							stringPlus.AppendSpaceLine(2, "/// </summary>");
							stringPlus.AppendSpaceLine(2, "public int GetMaxId()");
							stringPlus.AppendSpaceLine(2, "{\r\n");
							stringPlus.AppendSpaceLine(2, string.Concat(new string[]
							{
								"return ",
								base.DbHelperName,
								".GetMaxID(\"",
								columnName,
								"\", \"",
								base.TableName,
								"\"); "
							}));
							stringPlus.AppendSpaceLine(2, "}");
							break;
						}
					}
				}
			}
			return stringPlus.ToString();
		}
		private string CreatExistsProc()
		{
			StringPlus stringPlus = new StringPlus();
			stringPlus.AppendSpaceLine(2, "/// <summary>");
			stringPlus.AppendSpaceLine(2, "/// " + this.Languagelist["summaryExists"].ToString());
			stringPlus.AppendSpaceLine(2, "/// </summary>");
			stringPlus.AppendSpaceLine(2, "public bool Exists(" + CodeCommon.GetInParameter(base.Keys, false) + ")");
			stringPlus.AppendSpaceLine(2, "{");
			stringPlus.AppendSpaceLine(3, "int rowsAffected;");
			stringPlus.AppendLine(this.GetPreParameter(base.Keys));
			stringPlus.AppendSpaceLine(3, string.Concat(new string[]
			{
				"int result= ",
				base.DbHelperName,
				".RunProcedure(\"UP_",
				base.TableName,
				"_Exists\",parameters,out rowsAffected);"
			}));
			stringPlus.AppendSpaceLine(3, "if(result==1)");
			stringPlus.AppendSpaceLine(3, "{");
			stringPlus.AppendSpaceLine(4, "return true;");
			stringPlus.AppendSpaceLine(3, "}");
			stringPlus.AppendSpaceLine(3, "else");
			stringPlus.AppendSpaceLine(3, "{");
			stringPlus.AppendSpaceLine(4, "return false;");
			stringPlus.AppendSpaceLine(3, "}");
			stringPlus.AppendSpaceLine(2, "}");
			return stringPlus.Value;
		}
		private string CreatAddProc()
		{
			StringPlus stringPlus = new StringPlus();
			StringPlus stringPlus2 = new StringPlus();
			stringPlus.AppendSpaceLine(2, "/// <summary>");
			stringPlus.AppendSpaceLine(2, "///  " + this.Languagelist["summaryadd"].ToString());
			stringPlus.AppendSpaceLine(2, "/// </summary>");
			string str = "void";
			if ((this.dbobj.DbType == "SQL2000" || this.dbobj.DbType == "SQL2005" || this.dbobj.DbType == "SQL2008" || this.dbobj.DbType == "SQL2012") && base.IsHasIdentity)
			{
				str = "int";
			}
			stringPlus.AppendSpaceLine(2, "public " + str + " Add()");
			stringPlus.AppendSpaceLine(2, "{");
			stringPlus.AppendSpaceLine(3, "int rowsAffected;");
			stringPlus.AppendSpaceLine(3, base.DbParaHead + "Parameter[] parameters = {");
			int num = 0;
			int num2 = 0;
			foreach (ColumnInfo current in base.Fieldlist)
			{
				string columnName = current.ColumnName;
				string typeName = current.TypeName;
				bool arg_140_0 = current.IsIdentity;
				string length = current.Length;
				stringPlus.AppendSpaceLine(5, string.Concat(new string[]
				{
					"new ",
					base.DbParaHead,
					"Parameter(\"",
					this.preParameter,
					columnName,
					"\", ",
					base.DbParaDbType,
					".",
					CodeCommon.DbTypeLength(this._dbtype, typeName, length),
					"),"
				}));
				if (current.IsIdentity)
				{
					num = num2;
					stringPlus2.AppendSpaceLine(3, "parameters[" + num2 + "].Direction = ParameterDirection.Output;");
					num2++;
				}
				else
				{
					stringPlus2.AppendSpaceLine(3, string.Concat(new object[]
					{
						"parameters[",
						num2,
						"].Value = ",
						columnName,
						";"
					}));
					num2++;
				}
			}
			if (stringPlus.Value.IndexOf(",") > 0)
			{
				stringPlus.DelLastComma();
			}
			stringPlus.AppendLine("};");
			stringPlus.AppendLine(stringPlus2.Value);
			stringPlus.AppendSpaceLine(3, string.Concat(new string[]
			{
				base.DbHelperName,
				".RunProcedure(\"",
				this.ProcPrefix,
				base.TableName,
				"_ADD\",parameters,out rowsAffected);"
			}));
			if ((this.dbobj.DbType == "SQL2000" || this.dbobj.DbType == "SQL2005" || this.dbobj.DbType == "SQL2008" || this.dbobj.DbType == "SQL2012") && base.IsHasIdentity)
			{
				stringPlus.AppendSpaceLine(3, string.Concat(new object[]
				{
					this._key,
					"= (",
					this._keyType,
					")parameters[",
					num,
					"].Value;"
				}));
			}
			stringPlus.AppendSpaceLine(2, "}");
			return stringPlus.Value;
		}
		private string CreatUpdateProc()
		{
			StringPlus stringPlus = new StringPlus();
			StringPlus stringPlus2 = new StringPlus();
			stringPlus.AppendSpaceLine(2, "/// <summary>");
			stringPlus.AppendSpaceLine(2, "///  " + this.Languagelist["summaryUpdate"].ToString());
			stringPlus.AppendSpaceLine(2, "/// </summary>");
			stringPlus.AppendSpaceLine(2, "public bool Update()");
			stringPlus.AppendSpaceLine(2, "{");
			stringPlus.AppendSpaceLine(3, "int rowsAffected;");
			stringPlus.AppendSpaceLine(3, base.DbParaHead + "Parameter[] parameters = {");
			int num = 0;
			foreach (ColumnInfo current in base.Fieldlist)
			{
				string columnName = current.ColumnName;
				string typeName = current.TypeName;
				string length = current.Length;
				stringPlus.AppendSpaceLine(5, string.Concat(new string[]
				{
					"new ",
					base.DbParaHead,
					"Parameter(\"",
					this.preParameter,
					columnName,
					"\", ",
					base.DbParaDbType,
					".",
					CodeCommon.DbTypeLength(this._dbtype, typeName, length),
					"),"
				}));
				stringPlus2.AppendSpaceLine(3, string.Concat(new object[]
				{
					"parameters[",
					num,
					"].Value = ",
					columnName,
					";"
				}));
				num++;
			}
			if (stringPlus.Value.IndexOf(",") > 0)
			{
				stringPlus.DelLastComma();
			}
			stringPlus.AppendLine("};");
			stringPlus.AppendLine(stringPlus2.Value);
			stringPlus.AppendSpaceLine(3, string.Concat(new string[]
			{
				base.DbHelperName,
				".RunProcedure(\"",
				this.ProcPrefix,
				base.TableName,
				"_Update\",parameters,out rowsAffected);"
			}));
			stringPlus.AppendSpaceLine(3, "if (rowsAffected > 0)");
			stringPlus.AppendSpaceLine(3, "{");
			stringPlus.AppendSpaceLine(4, "return true;");
			stringPlus.AppendSpaceLine(3, "}");
			stringPlus.AppendSpaceLine(3, "else");
			stringPlus.AppendSpaceLine(3, "{");
			stringPlus.AppendSpaceLine(4, "return false;");
			stringPlus.AppendSpaceLine(3, "}");
			stringPlus.AppendSpaceLine(2, "}");
			return stringPlus.Value;
		}
		private string CreatDeleteProc()
		{
			StringPlus stringPlus = new StringPlus();
			stringPlus.AppendSpaceLine(2, "/// <summary>");
			stringPlus.AppendSpaceLine(2, "/// " + this.Languagelist["summaryDelete"].ToString());
			stringPlus.AppendSpaceLine(2, "/// </summary>");
			stringPlus.AppendSpaceLine(2, "public bool Delete(" + CodeCommon.GetInParameter(base.Keys, true) + ")");
			stringPlus.AppendSpaceLine(2, "{\r\n");
			stringPlus.AppendSpaceLine(3, "int rowsAffected;");
			stringPlus.AppendLine(this.GetPreParameter(base.Keys));
			stringPlus.AppendSpaceLine(3, string.Concat(new string[]
			{
				base.DbHelperName,
				".RunProcedure(\"",
				this.ProcPrefix,
				base.TableName,
				"_Delete\",parameters,out rowsAffected);"
			}));
			stringPlus.AppendSpaceLine(3, "if (rowsAffected > 0)");
			stringPlus.AppendSpaceLine(3, "{");
			stringPlus.AppendSpaceLine(4, "return true;");
			stringPlus.AppendSpaceLine(3, "}");
			stringPlus.AppendSpaceLine(3, "else");
			stringPlus.AppendSpaceLine(3, "{");
			stringPlus.AppendSpaceLine(4, "return false;");
			stringPlus.AppendSpaceLine(3, "}");
			stringPlus.AppendSpaceLine(2, "}");
			return stringPlus.Value;
		}
		private string CreatConstructorProc()
		{
			StringPlus stringPlus = new StringPlus();
			stringPlus.AppendSpaceLine(2, "/// <summary>");
			stringPlus.AppendSpaceLine(2, "/// " + this.Languagelist["summaryGetModel"].ToString());
			stringPlus.AppendSpaceLine(2, "/// </summary>");
			stringPlus.AppendSpaceLine(2, string.Concat(new string[]
			{
				"public ",
				base.ModelName,
				"(",
				CodeCommon.GetInParameter(base.Keys, true),
				")"
			}));
			stringPlus.AppendSpaceLine(2, "{");
			stringPlus.AppendLine(this.GetPreParameter(base.Keys));
			stringPlus.AppendSpaceLine(3, string.Concat(new string[]
			{
				"DataSet ds= ",
				base.DbHelperName,
				".RunProcedure(\"UP_",
				base.TableName,
				"_GetModel\",parameters,\"ds\");"
			}));
			stringPlus.AppendSpaceLine(3, "if(ds.Tables[0].Rows.Count>0)");
			stringPlus.AppendSpaceLine(3, "{");
			foreach (ColumnInfo current in base.Fieldlist)
			{
				string columnName = current.ColumnName;
				string typeName = current.TypeName;
				string key;
				switch (key = CodeCommon.DbTypeToCS(typeName))
				{
				case "int":
					stringPlus.AppendSpaceLine(4, string.Concat(new string[]
					{
						"if(ds.Tables[0].Rows[0][\"",
						columnName,
						"\"]!=null && ds.Tables[0].Rows[0][\"",
						columnName,
						"\"].ToString()!=\"\")"
					}));
					stringPlus.AppendSpaceLine(4, "{");
					stringPlus.AppendSpaceLine(5, string.Concat(new string[]
					{
						"this.",
						columnName,
						"=int.Parse(ds.Tables[0].Rows[0][\"",
						columnName,
						"\"].ToString());"
					}));
					stringPlus.AppendSpaceLine(4, "}");
					continue;
				case "decimal":
					stringPlus.AppendSpaceLine(4, string.Concat(new string[]
					{
						"if(ds.Tables[0].Rows[0][\"",
						columnName,
						"\"]!=null && ds.Tables[0].Rows[0][\"",
						columnName,
						"\"].ToString()!=\"\")"
					}));
					stringPlus.AppendSpaceLine(4, "{");
					stringPlus.AppendSpaceLine(5, string.Concat(new string[]
					{
						"this.",
						columnName,
						"=decimal.Parse(ds.Tables[0].Rows[0][\"",
						columnName,
						"\"].ToString());"
					}));
					stringPlus.AppendSpaceLine(4, "}");
					continue;
				case "DateTime":
					stringPlus.AppendSpaceLine(4, string.Concat(new string[]
					{
						"if(ds.Tables[0].Rows[0][\"",
						columnName,
						"\"]!=null && ds.Tables[0].Rows[0][\"",
						columnName,
						"\"].ToString()!=\"\")"
					}));
					stringPlus.AppendSpaceLine(4, "{\r\n");
					stringPlus.AppendSpaceLine(5, string.Concat(new string[]
					{
						"this.",
						columnName,
						"=DateTime.Parse(ds.Tables[0].Rows[0][\"",
						columnName,
						"\"].ToString());"
					}));
					stringPlus.AppendSpaceLine(4, "}");
					continue;
				case "string":
					stringPlus.AppendSpaceLine(4, string.Concat(new string[]
					{
						"if(ds.Tables[0].Rows[0][\"",
						columnName,
						"\"]!=null && ds.Tables[0].Rows[0][\"",
						columnName,
						"\"].ToString()!=\"\")"
					}));
					stringPlus.AppendSpaceLine(4, "{\r\n");
					stringPlus.AppendSpaceLine(5, string.Concat(new string[]
					{
						"this.",
						columnName,
						"=ds.Tables[0].Rows[0][\"",
						columnName,
						"\"].ToString();"
					}));
					stringPlus.AppendSpaceLine(4, "}");
					continue;
				case "bool":
					stringPlus.AppendSpaceLine(4, string.Concat(new string[]
					{
						"if(ds.Tables[0].Rows[0][\"",
						columnName,
						"\"]!=null && ds.Tables[0].Rows[0][\"",
						columnName,
						"\"].ToString()!=\"\")"
					}));
					stringPlus.Append(base.Space(4) + "{\r\n");
					stringPlus.Append(string.Concat(new string[]
					{
						base.Space(5),
						"if((ds.Tables[0].Rows[0][\"",
						columnName,
						"\"].ToString()==\"1\")||(ds.Tables[0].Rows[0][\"",
						columnName,
						"\"].ToString().ToLower()==\"true\"))\r\n"
					}));
					stringPlus.Append(base.Space(5) + "{\r\n");
					stringPlus.Append(base.Space(6) + "this." + columnName + "=true;\r\n");
					stringPlus.Append(base.Space(5) + "}\r\n");
					stringPlus.Append(base.Space(5) + "else\r\n");
					stringPlus.Append(base.Space(5) + "{\r\n");
					stringPlus.Append(base.Space(6) + "this." + columnName + "=false;\r\n");
					stringPlus.Append(base.Space(5) + "}\r\n");
					stringPlus.Append(base.Space(4) + "}\r\n\r\n");
					continue;
				case "byte[]":
					stringPlus.AppendSpaceLine(4, string.Concat(new string[]
					{
						"if(ds.Tables[0].Rows[0][\"",
						columnName,
						"\"]!=null && ds.Tables[0].Rows[0][\"",
						columnName,
						"\"].ToString()!=\"\")"
					}));
					stringPlus.Append(base.Space(4) + "{\r\n");
					stringPlus.Append(string.Concat(new string[]
					{
						base.Space(5),
						"this.",
						columnName,
						"=(byte[])ds.Tables[0].Rows[0][\"",
						columnName,
						"\"];\r\n"
					}));
					stringPlus.Append(base.Space(4) + "}\r\n");
					continue;
				}
				stringPlus.AppendSpaceLine(5, columnName + "=ds.Tables[0].Rows[0][\"" + columnName + "\"].ToString();");
			}
			stringPlus.AppendSpaceLine(3, "}");
			stringPlus.AppendSpaceLine(2, "}");
			return stringPlus.Value;
		}
		private string CreatGetModelProc()
		{
			StringPlus stringPlus = new StringPlus();
			stringPlus.AppendSpaceLine(2, "/// <summary>");
			stringPlus.AppendSpaceLine(2, "/// " + this.Languagelist["summaryGetModel"].ToString());
			stringPlus.AppendSpaceLine(2, "/// </summary>");
			stringPlus.AppendSpaceLine(2, "public void GetModel(" + CodeCommon.GetInParameter(base.Keys, true) + ")");
			stringPlus.AppendSpaceLine(2, "{");
			stringPlus.AppendLine(this.GetPreParameter(base.Keys));
			stringPlus.AppendSpaceLine(3, string.Concat(new string[]
			{
				"DataSet ds= ",
				base.DbHelperName,
				".RunProcedure(\"",
				this.ProcPrefix,
				base.TableName,
				"_GetModel\",parameters,\"ds\");"
			}));
			stringPlus.AppendSpaceLine(3, "if(ds.Tables[0].Rows.Count>0)");
			stringPlus.AppendSpaceLine(3, "{");
			foreach (ColumnInfo current in base.Fieldlist)
			{
				string columnName = current.ColumnName;
				string typeName = current.TypeName;
				string key;
				switch (key = CodeCommon.DbTypeToCS(typeName))
				{
				case "int":
					stringPlus.AppendSpaceLine(4, string.Concat(new string[]
					{
						"if(ds.Tables[0].Rows[0][\"",
						columnName,
						"\"]!=null && ds.Tables[0].Rows[0][\"",
						columnName,
						"\"].ToString()!=\"\")"
					}));
					stringPlus.AppendSpaceLine(4, "{");
					stringPlus.AppendSpaceLine(5, string.Concat(new string[]
					{
						"this.",
						columnName,
						"=int.Parse(ds.Tables[0].Rows[0][\"",
						columnName,
						"\"].ToString());"
					}));
					stringPlus.AppendSpaceLine(4, "}");
					continue;
				case "long":
					stringPlus.AppendSpaceLine(4, string.Concat(new string[]
					{
						"if(ds.Tables[0].Rows[0][\"",
						columnName,
						"\"]!=null && ds.Tables[0].Rows[0][\"",
						columnName,
						"\"].ToString()!=\"\")"
					}));
					stringPlus.AppendSpaceLine(4, "{");
					stringPlus.AppendSpaceLine(5, string.Concat(new string[]
					{
						"this.",
						columnName,
						"=long.Parse(ds.Tables[0].Rows[0][\"",
						columnName,
						"\"].ToString());"
					}));
					stringPlus.AppendSpaceLine(4, "}");
					continue;
				case "decimal":
					stringPlus.AppendSpaceLine(4, string.Concat(new string[]
					{
						"if(ds.Tables[0].Rows[0][\"",
						columnName,
						"\"]!=null && ds.Tables[0].Rows[0][\"",
						columnName,
						"\"].ToString()!=\"\")"
					}));
					stringPlus.AppendSpaceLine(4, "{");
					stringPlus.AppendSpaceLine(5, string.Concat(new string[]
					{
						"this.",
						columnName,
						"=decimal.Parse(ds.Tables[0].Rows[0][\"",
						columnName,
						"\"].ToString());"
					}));
					stringPlus.AppendSpaceLine(4, "}");
					continue;
				case "float":
					stringPlus.AppendSpaceLine(4, string.Concat(new string[]
					{
						"if(ds.Tables[0].Rows[0][\"",
						columnName,
						"\"]!=null && ds.Tables[0].Rows[0][\"",
						columnName,
						"\"].ToString()!=\"\")"
					}));
					stringPlus.AppendSpaceLine(4, "{");
					stringPlus.AppendSpaceLine(5, string.Concat(new string[]
					{
						"this.",
						columnName,
						"=float.Parse(ds.Tables[0].Rows[0][\"",
						columnName,
						"\"].ToString());"
					}));
					stringPlus.AppendSpaceLine(4, "}");
					continue;
				case "DateTime":
					stringPlus.AppendSpaceLine(4, string.Concat(new string[]
					{
						"if(ds.Tables[0].Rows[0][\"",
						columnName,
						"\"]!=null && ds.Tables[0].Rows[0][\"",
						columnName,
						"\"].ToString()!=\"\")"
					}));
					stringPlus.AppendSpaceLine(4, "{");
					stringPlus.AppendSpaceLine(5, string.Concat(new string[]
					{
						"this.",
						columnName,
						"=DateTime.Parse(ds.Tables[0].Rows[0][\"",
						columnName,
						"\"].ToString());"
					}));
					stringPlus.AppendSpaceLine(4, "}");
					continue;
				case "string":
					stringPlus.AppendSpaceLine(4, "if(ds.Tables[0].Rows[0][\"" + columnName + "\"]!=null )");
					stringPlus.AppendSpaceLine(4, "{");
					stringPlus.AppendSpaceLine(5, string.Concat(new string[]
					{
						"this.",
						columnName,
						"=ds.Tables[0].Rows[0][\"",
						columnName,
						"\"].ToString();"
					}));
					stringPlus.AppendSpaceLine(4, "}");
					continue;
				case "bool":
					stringPlus.AppendSpaceLine(4, string.Concat(new string[]
					{
						"if(ds.Tables[0].Rows[0][\"",
						columnName,
						"\"]!=null && ds.Tables[0].Rows[0][\"",
						columnName,
						"\"].ToString()!=\"\")"
					}));
					stringPlus.AppendSpaceLine(4, "{");
					stringPlus.AppendSpaceLine(5, string.Concat(new string[]
					{
						"if((ds.Tables[0].Rows[0][\"",
						columnName,
						"\"].ToString()==\"1\")||(ds.Tables[0].Rows[0][\"",
						columnName,
						"\"].ToString().ToLower()==\"true\"))"
					}));
					stringPlus.AppendSpaceLine(5, "{");
					stringPlus.AppendSpaceLine(6, columnName + "=true;");
					stringPlus.AppendSpaceLine(5, "}");
					stringPlus.AppendSpaceLine(5, "else");
					stringPlus.AppendSpaceLine(5, "{");
					stringPlus.AppendSpaceLine(6, columnName + "=false;");
					stringPlus.AppendSpaceLine(5, "}");
					stringPlus.AppendSpaceLine(4, "}");
					continue;
				case "byte[]":
					stringPlus.AppendSpaceLine(4, string.Concat(new string[]
					{
						"if(ds.Tables[0].Rows[0][\"",
						columnName,
						"\"]!=null && ds.Tables[0].Rows[0][\"",
						columnName,
						"\"].ToString()!=\"\")"
					}));
					stringPlus.AppendSpaceLine(4, "{");
					stringPlus.AppendSpaceLine(5, string.Concat(new string[]
					{
						"this.",
						columnName,
						"=(byte[])ds.Tables[0].Rows[0][\"",
						columnName,
						"\"];"
					}));
					stringPlus.AppendSpaceLine(4, "}");
					continue;
				case "Guid":
					stringPlus.AppendSpaceLine(4, string.Concat(new string[]
					{
						"if(ds.Tables[0].Rows[0][\"",
						columnName,
						"\"]!=null && ds.Tables[0].Rows[0][\"",
						columnName,
						"\"].ToString()!=\"\")"
					}));
					stringPlus.AppendSpaceLine(4, "{");
					stringPlus.AppendSpaceLine(5, string.Concat(new string[]
					{
						"this.",
						columnName,
						"=new Guid(ds.Tables[0].Rows[0][\"",
						columnName,
						"\"].ToString());"
					}));
					stringPlus.AppendSpaceLine(4, "}");
					continue;
				}
				stringPlus.AppendSpaceLine(5, string.Concat(new string[]
				{
					"//this.",
					columnName,
					"=ds.Tables[0].Rows[0][\"",
					columnName,
					"\"].ToString();"
				}));
			}
			stringPlus.AppendSpaceLine(3, "}");
			stringPlus.AppendSpaceLine(2, "}");
			return stringPlus.Value;
		}
		private string CreatGetListProc()
		{
			StringPlus stringPlus = new StringPlus();
			stringPlus.AppendSpaceLine(2, "/// <summary>");
			stringPlus.AppendSpaceLine(2, "/// " + this.Languagelist["summaryGetList"].ToString());
			stringPlus.AppendSpaceLine(2, "/// </summary>");
			stringPlus.AppendSpaceLine(2, "public DataSet GetList()");
			stringPlus.AppendSpaceLine(2, "{");
			stringPlus.AppendSpaceLine(3, string.Concat(new string[]
			{
				"return ",
				base.DbHelperName,
				".RunProcedure(\"",
				this.ProcPrefix,
				base.TableName,
				"_GetList\",new SqlParameter[] { null },\"ds\");"
			}));
			stringPlus.AppendSpaceLine(2, "}");
			return stringPlus.Value;
		}
		private string CreatGetListByPageProc()
		{
			StringPlus stringPlus = new StringPlus();
			stringPlus.AppendSpaceLine(2, "/*");
			stringPlus.AppendSpaceLine(2, "/// <summary>");
			stringPlus.AppendSpaceLine(2, "/// " + this.Languagelist["summaryGetList3"].ToString());
			stringPlus.AppendSpaceLine(2, "/// </summary>");
			stringPlus.AppendSpaceLine(2, "public DataSet GetList(int PageSize,int PageIndex,string strWhere)");
			stringPlus.AppendSpaceLine(2, "{");
			stringPlus.AppendSpaceLine(3, base.DbParaHead + "Parameter[] parameters = {");
			stringPlus.AppendSpaceLine(5, string.Concat(new string[]
			{
				"new ",
				base.DbParaHead,
				"Parameter(\"@tblName\", ",
				base.DbParaDbType,
				".VarChar, 255),"
			}));
			stringPlus.AppendSpaceLine(5, string.Concat(new string[]
			{
				"new ",
				base.DbParaHead,
				"Parameter(\"@fldName\", ",
				base.DbParaDbType,
				".VarChar, 255),"
			}));
			stringPlus.AppendSpaceLine(5, string.Concat(new string[]
			{
				"new ",
				base.DbParaHead,
				"Parameter(\"@PageSize\", ",
				base.DbParaDbType,
				".",
				CodeCommon.CSToProcType(this._dbtype, "int"),
				"),"
			}));
			stringPlus.AppendSpaceLine(5, string.Concat(new string[]
			{
				"new ",
				base.DbParaHead,
				"Parameter(\"@PageIndex\", ",
				base.DbParaDbType,
				".",
				CodeCommon.CSToProcType(this._dbtype, "int"),
				"),"
			}));
			stringPlus.AppendSpaceLine(5, string.Concat(new string[]
			{
				"new ",
				base.DbParaHead,
				"Parameter(\"@IsReCount\", ",
				base.DbParaDbType,
				".",
				CodeCommon.CSToProcType(this._dbtype, "bit"),
				"),"
			}));
			stringPlus.AppendSpaceLine(5, string.Concat(new string[]
			{
				"new ",
				base.DbParaHead,
				"Parameter(\"@OrderType\", ",
				base.DbParaDbType,
				".",
				CodeCommon.CSToProcType(this._dbtype, "bit"),
				"),"
			}));
			stringPlus.AppendSpaceLine(5, string.Concat(new string[]
			{
				"new ",
				base.DbParaHead,
				"Parameter(\"@strWhere\", ",
				base.DbParaDbType,
				".VarChar,1000),"
			}));
			stringPlus.AppendSpaceLine(5, "};");
			stringPlus.AppendSpaceLine(3, "parameters[0].Value = \"" + base.TableName + "\";");
			stringPlus.AppendSpaceLine(3, "parameters[1].Value = \"" + this._key + "\";");
			stringPlus.AppendSpaceLine(3, "parameters[2].Value = PageSize;");
			stringPlus.AppendSpaceLine(3, "parameters[3].Value = PageIndex;");
			stringPlus.AppendSpaceLine(3, "parameters[4].Value = 0;");
			stringPlus.AppendSpaceLine(3, "parameters[5].Value = 0;");
			stringPlus.AppendSpaceLine(3, "parameters[6].Value = strWhere;\t");
			stringPlus.AppendSpaceLine(3, "return " + base.DbHelperName + ".RunProcedure(\"UP_GetRecordByPage\",parameters,\"ds\");");
			stringPlus.AppendSpaceLine(2, "}*/");
			return stringPlus.Value;
		}
		private string CreatGetMaxIDParam()
		{
			StringPlus stringPlus = new StringPlus();
			if (base.Keys.Count > 0)
			{
				foreach (ColumnInfo current in base.Keys)
				{
					if (CodeCommon.DbTypeToCS(current.TypeName) == "int")
					{
						string columnName = current.ColumnName;
						if (current.IsPrimaryKey)
						{
							stringPlus.AppendLine("");
							stringPlus.AppendSpaceLine(2, "/// <summary>");
							stringPlus.AppendSpaceLine(2, "/// " + this.Languagelist["summaryGetMaxId"].ToString());
							stringPlus.AppendSpaceLine(2, "/// </summary>");
							stringPlus.AppendSpaceLine(2, "public int GetMaxId()");
							stringPlus.AppendSpaceLine(2, "{\r\n");
							stringPlus.AppendSpaceLine(2, string.Concat(new string[]
							{
								"return ",
								base.DbHelperName,
								".GetMaxID(\"",
								columnName,
								"\", \"",
								base.TableName,
								"\"); "
							}));
							stringPlus.AppendSpaceLine(2, "}");
							break;
						}
					}
				}
			}
			return stringPlus.ToString();
		}
		private string CreatExistsParam()
		{
			StringPlus stringPlus = new StringPlus();
			stringPlus.AppendSpaceLine(2, "/// <summary>");
			stringPlus.AppendSpaceLine(2, "/// " + this.Languagelist["summaryExists"].ToString());
			stringPlus.AppendSpaceLine(2, "/// </summary>");
			stringPlus.AppendSpaceLine(2, "public bool Exists(" + CodeCommon.GetInParameter(base.Keys, false) + ")");
			stringPlus.AppendSpaceLine(2, "{");
			stringPlus.AppendSpaceLine(3, "StringBuilder strSql=new StringBuilder();");
			stringPlus.AppendSpaceLine(3, "strSql.Append(\"select count(1) from [" + base.TableName + "]\");");
			if (this.GetWhereExpression(base.Keys).Length > 0)
			{
				stringPlus.AppendSpaceLine(3, "strSql.Append(\" where " + this.GetWhereExpression(base.Keys) + "\");\r\n");
			}
			else
			{
				stringPlus.AppendSpaceLine(3, "//strSql.Append(\" where 条件);\r\n");
			}
			stringPlus.AppendLine(this.GetPreParameter(base.Keys));
			stringPlus.Append(CodeCommon.Space(3) + "return " + base.DbHelperName + ".Exists(strSql.ToString(),parameters);\r\n");
			stringPlus.AppendSpaceLine(2, "}");
			return stringPlus.Value;
		}
		private string CreatAddParam()
		{
			StringBuilder stringBuilder = new StringBuilder();
			StringBuilder stringBuilder2 = new StringBuilder();
			StringBuilder stringBuilder3 = new StringBuilder();
			StringPlus stringPlus = new StringPlus();
			StringPlus stringPlus2 = new StringPlus();
			stringBuilder.Append("\r\n");
			stringBuilder.Append(base.Space(2) + "/// <summary>\r\n");
			stringBuilder.Append(base.Space(2) + "/// " + this.Languagelist["summaryadd"].ToString() + "\r\n");
			stringBuilder.Append(base.Space(2) + "/// </summary>\r\n");
			string str = "void";
			if ((this.dbobj.DbType == "SQL2000" || this.dbobj.DbType == "SQL2005" || this.dbobj.DbType == "SQL2008" || this.dbobj.DbType == "SQL2012") && base.IsHasIdentity)
			{
				str = "int";
			}
			stringBuilder.Append(base.Space(2) + "public " + str + " Add()\r\n");
			stringBuilder.Append(base.Space(2) + "{\r\n");
			stringBuilder.Append(base.Space(3) + "StringBuilder strSql=new StringBuilder();\r\n");
			stringBuilder.Append(base.Space(3) + "strSql.Append(\"insert into [" + base.TableName + "] (\");\r\n");
			stringBuilder2.Append(base.Space(3) + "strSql.Append(\"");
			int num = 0;
			foreach (ColumnInfo current in base.Fieldlist)
			{
				string columnName = current.ColumnName;
				string typeName = current.TypeName;
				bool arg_1C0_0 = current.IsIdentity;
				string length = current.Length;
				if (current.IsIdentity)
				{
					str = CodeCommon.DbTypeToCS(typeName);
				}
				else
				{
					stringPlus.AppendSpaceLine(5, string.Concat(new string[]
					{
						"new ",
						base.DbParaHead,
						"Parameter(\"",
						this.preParameter,
						columnName,
						"\", ",
						base.DbParaDbType,
						".",
						CodeCommon.DbTypeLength(this._dbtype, typeName, length),
						"),"
					}));
					stringBuilder2.Append(columnName + ",");
					stringBuilder3.Append(this.preParameter + columnName + ",");
					stringPlus2.AppendSpaceLine(3, string.Concat(new object[]
					{
						"parameters[",
						num,
						"].Value = ",
						columnName,
						";"
					}));
					num++;
				}
			}
			stringBuilder2.Remove(stringBuilder2.Length - 1, 1);
			stringBuilder3.Remove(stringBuilder3.Length - 1, 1);
			if (stringPlus.Value.IndexOf(",") > 0)
			{
				stringPlus.DelLastComma();
			}
			stringBuilder2.Append(")\");\r\n");
			stringBuilder.Append(stringBuilder2.ToString());
			stringBuilder.Append(base.Space(3) + "strSql.Append(\" values (\");\r\n");
			stringBuilder.Append(base.Space(3) + "strSql.Append(\"" + stringBuilder3.ToString() + ")\");\r\n");
			if ((this.dbobj.DbType == "SQL2000" || this.dbobj.DbType == "SQL2005" || this.dbobj.DbType == "SQL2008" || this.dbobj.DbType == "SQL2012") && base.IsHasIdentity)
			{
				stringBuilder.Append(CodeCommon.Space(3) + "strSql.Append(\";select @@IDENTITY\");\r\n");
			}
			stringBuilder.Append(base.Space(3) + base.DbParaHead + "Parameter[] parameters = {\r\n");
			stringBuilder.Append(stringPlus.Value);
			stringBuilder.Append("};\r\n");
			stringBuilder.Append(stringPlus2.Value + "\r\n");
			if ((this.dbobj.DbType == "SQL2000" || this.dbobj.DbType == "SQL2005" || this.dbobj.DbType == "SQL2008" || this.dbobj.DbType == "SQL2012") && base.IsHasIdentity)
			{
				stringBuilder.Append(CodeCommon.Space(3) + "object obj = " + base.DbHelperName + ".GetSingle(strSql.ToString(),parameters);\r\n");
				stringBuilder.Append(CodeCommon.Space(3) + "if (obj == null)\r\n");
				stringBuilder.Append(CodeCommon.Space(3) + "{\r\n");
				stringBuilder.Append(CodeCommon.Space(4) + "return 0;\r\n");
				stringBuilder.Append(CodeCommon.Space(3) + "}\r\n");
				stringBuilder.Append(CodeCommon.Space(3) + "else\r\n");
				stringBuilder.Append(CodeCommon.Space(3) + "{\r\n");
				stringBuilder.Append(CodeCommon.Space(4) + "return Convert.ToInt32(obj);\r\n");
				stringBuilder.Append(CodeCommon.Space(3) + "}\r\n");
			}
			else
			{
				stringBuilder.Append(CodeCommon.Space(3) + base.DbHelperName + ".ExecuteSql(strSql.ToString(),parameters);\r\n");
			}
			stringBuilder.Append(base.Space(2) + "}");
			return stringBuilder.ToString();
		}
		private string CreatUpdateParam()
		{
			StringPlus stringPlus = new StringPlus();
			StringPlus stringPlus2 = new StringPlus();
			StringPlus stringPlus3 = new StringPlus();
			stringPlus.AppendSpaceLine(2, "/// <summary>");
			stringPlus.AppendSpaceLine(2, "/// " + this.Languagelist["summaryUpdate"].ToString());
			stringPlus.AppendSpaceLine(2, "/// </summary>");
			stringPlus.AppendSpaceLine(2, "public bool Update()");
			stringPlus.AppendSpaceLine(2, "{");
			stringPlus.AppendSpaceLine(3, "StringBuilder strSql=new StringBuilder();");
			stringPlus.AppendSpaceLine(3, "strSql.Append(\"update [" + base.TableName + "] set \");");
			int num = 0;
			if (base.Fieldlist.Count == 0)
			{
				base.Fieldlist = base.Keys;
			}
			List<ColumnInfo> list = new List<ColumnInfo>();
			foreach (ColumnInfo current in base.Fieldlist)
			{
				string columnName = current.ColumnName;
				string typeName = current.TypeName;
				string length = current.Length;
				bool arg_F6_0 = current.IsIdentity;
				bool arg_FE_0 = current.IsPrimaryKey;
				if (current.IsIdentity || current.IsPrimaryKey || base.Keys.Contains(current))
				{
					list.Add(current);
				}
				else
				{
					stringPlus2.AppendSpaceLine(5, string.Concat(new string[]
					{
						"new ",
						base.DbParaHead,
						"Parameter(\"",
						this.preParameter,
						columnName,
						"\", ",
						base.DbParaDbType,
						".",
						CodeCommon.DbTypeLength(this._dbtype, typeName, length),
						"),"
					}));
					stringPlus3.AppendSpaceLine(3, string.Concat(new object[]
					{
						"parameters[",
						num,
						"].Value = ",
						columnName,
						";"
					}));
					num++;
					stringPlus.AppendSpaceLine(3, string.Concat(new string[]
					{
						"strSql.Append(\"",
						columnName,
						"=",
						this.preParameter,
						columnName,
						",\");"
					}));
				}
			}
			foreach (ColumnInfo current2 in list)
			{
				string columnName2 = current2.ColumnName;
				string typeName2 = current2.TypeName;
				string length2 = current2.Length;
				bool arg_28E_0 = current2.IsIdentity;
				bool arg_296_0 = current2.IsPrimaryKey;
				stringPlus2.AppendSpaceLine(5, string.Concat(new string[]
				{
					"new ",
					base.DbParaHead,
					"Parameter(\"",
					this.preParameter,
					columnName2,
					"\", ",
					base.DbParaDbType,
					".",
					CodeCommon.DbTypeLength(this._dbtype, typeName2, length2),
					"),"
				}));
				stringPlus3.AppendSpaceLine(3, string.Concat(new object[]
				{
					"parameters[",
					num,
					"].Value = ",
					columnName2,
					";"
				}));
				num++;
			}
			if (stringPlus.Value.IndexOf(",") > 0)
			{
				stringPlus.DelLastComma();
			}
			stringPlus.AppendLine("\");");
			if (this.GetWhereExpression(base.Keys).Length > 0)
			{
				stringPlus.AppendSpace(3, "strSql.Append(\" where " + this.GetWhereExpression(base.Keys) + "\");\r\n");
			}
			else
			{
				stringPlus.AppendSpace(3, "//strSql.Append(\" where 条件);\r\n");
			}
			stringPlus.AppendSpaceLine(3, base.DbParaHead + "Parameter[] parameters = {");
			if (stringPlus2.Value.IndexOf(",") > 0)
			{
				stringPlus2.DelLastComma();
			}
			stringPlus.Append(stringPlus2.Value);
			stringPlus.AppendLine("};");
			stringPlus.AppendLine(stringPlus3.Value);
			stringPlus.AppendSpaceLine(3, "int rows=" + base.DbHelperName + ".ExecuteSql(strSql.ToString(),parameters);");
			stringPlus.AppendSpaceLine(3, "if (rows > 0)");
			stringPlus.AppendSpaceLine(3, "{");
			stringPlus.AppendSpaceLine(4, "return true;");
			stringPlus.AppendSpaceLine(3, "}");
			stringPlus.AppendSpaceLine(3, "else");
			stringPlus.AppendSpaceLine(3, "{");
			stringPlus.AppendSpaceLine(4, "return false;");
			stringPlus.AppendSpaceLine(3, "}");
			stringPlus.AppendSpaceLine(2, "}");
			return stringPlus.ToString();
		}
		private string CreatDeleteParam()
		{
			StringPlus stringPlus = new StringPlus();
			stringPlus.AppendSpaceLine(2, "/// <summary>");
			stringPlus.AppendSpaceLine(2, "/// " + this.Languagelist["summaryDelete"].ToString());
			stringPlus.AppendSpaceLine(2, "/// </summary>");
			stringPlus.AppendSpaceLine(2, "public bool Delete(" + CodeCommon.GetInParameter(base.Keys, true) + ")");
			stringPlus.AppendSpaceLine(2, "{");
			stringPlus.AppendSpaceLine(3, "StringBuilder strSql=new StringBuilder();");
			stringPlus.AppendSpaceLine(3, "strSql.Append(\"delete from [" + base.TableName + "] \");");
			if (this.GetWhereExpression(base.Keys).Length > 0)
			{
				stringPlus.AppendSpace(3, "strSql.Append(\" where " + this.GetWhereExpression(base.Keys) + "\");\r\n");
			}
			else
			{
				stringPlus.AppendSpace(3, "//strSql.Append(\" where 条件);\r\n");
			}
			stringPlus.AppendLine(this.GetPreParameter(base.Keys));
			stringPlus.AppendSpaceLine(3, "int rows=" + base.DbHelperName + ".ExecuteSql(strSql.ToString(),parameters);");
			stringPlus.AppendSpaceLine(3, "if (rows > 0)");
			stringPlus.AppendSpaceLine(3, "{");
			stringPlus.AppendSpaceLine(4, "return true;");
			stringPlus.AppendSpaceLine(3, "}");
			stringPlus.AppendSpaceLine(3, "else");
			stringPlus.AppendSpaceLine(3, "{");
			stringPlus.AppendSpaceLine(4, "return false;");
			stringPlus.AppendSpaceLine(3, "}");
			stringPlus.AppendSpaceLine(2, "}");
			return stringPlus.Value;
		}
		private string CreatConstructorParam()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("\r\n");
			stringBuilder.Append(base.Space(2) + "/// <summary>\r\n");
			stringBuilder.Append(base.Space(2) + "/// " + this.Languagelist["summaryGetModel"].ToString() + "\r\n");
			stringBuilder.Append(base.Space(2) + "/// </summary>\r\n");
			stringBuilder.Append(string.Concat(new string[]
			{
				base.Space(2),
				"public ",
				base.ModelName,
				"(",
				CodeCommon.GetInParameter(base.Keys, true),
				")\r\n"
			}));
			stringBuilder.Append(base.Space(2) + "{\r\n");
			stringBuilder.Append(base.Space(3) + "StringBuilder strSql=new StringBuilder();\r\n");
			stringBuilder.Append(base.Space(3) + "strSql.Append(\"select ");
			stringBuilder.Append(base.Fieldstrlist + " \");\r\n");
			stringBuilder.Append(base.Space(3) + "strSql.Append(\" FROM [" + base.TableName + "] \");\r\n");
			if (this.GetWhereExpression(base.Keys).Length > 0)
			{
				stringBuilder.Append(base.Space(3) + "strSql.Append(\" where " + this.GetWhereExpression(base.Keys) + "\");\r\n");
			}
			else
			{
				stringBuilder.Append(base.Space(3) + "//strSql.Append(\" where 条件);\r\n");
			}
			stringBuilder.AppendLine(this.GetPreParameter(base.Keys));
			stringBuilder.Append(base.Space(3) + "DataSet ds=" + base.DbHelperName + ".Query(strSql.ToString(),parameters);\r\n");
			stringBuilder.Append(base.Space(3) + "if(ds.Tables[0].Rows.Count>0)\r\n");
			stringBuilder.Append(base.Space(3) + "{\r\n");
			foreach (ColumnInfo current in base.Fieldlist)
			{
				string columnName = current.ColumnName;
				string typeName = current.TypeName;
				string key;
				switch (key = CodeCommon.DbTypeToCS(typeName))
				{
				case "int":
					stringBuilder.Append(string.Concat(new string[]
					{
						base.Space(4),
						"if(ds.Tables[0].Rows[0][\"",
						columnName,
						"\"]!=null && ds.Tables[0].Rows[0][\"",
						columnName,
						"\"].ToString()!=\"\")\r\n"
					}));
					stringBuilder.Append(base.Space(4) + "{\r\n");
					stringBuilder.Append(string.Concat(new string[]
					{
						base.Space(5),
						"this.",
						columnName,
						"=int.Parse(ds.Tables[0].Rows[0][\"",
						columnName,
						"\"].ToString());\r\n"
					}));
					stringBuilder.Append(base.Space(4) + "}\r\n");
					continue;
				case "decimal":
					stringBuilder.Append(string.Concat(new string[]
					{
						base.Space(4),
						"if(ds.Tables[0].Rows[0][\"",
						columnName,
						"\"]!=null && ds.Tables[0].Rows[0][\"",
						columnName,
						"\"].ToString()!=\"\")\r\n"
					}));
					stringBuilder.Append(base.Space(4) + "{\r\n");
					stringBuilder.Append(string.Concat(new string[]
					{
						base.Space(5),
						"this.",
						columnName,
						"=decimal.Parse(ds.Tables[0].Rows[0][\"",
						columnName,
						"\"].ToString());\r\n"
					}));
					stringBuilder.Append(base.Space(4) + "}\r\n");
					continue;
				case "DateTime":
					stringBuilder.Append(string.Concat(new string[]
					{
						base.Space(4),
						"if(ds.Tables[0].Rows[0][\"",
						columnName,
						"\"]!=null && ds.Tables[0].Rows[0][\"",
						columnName,
						"\"].ToString()!=\"\")\r\n"
					}));
					stringBuilder.Append(base.Space(4) + "{\r\n");
					stringBuilder.Append(string.Concat(new string[]
					{
						base.Space(5),
						"this.",
						columnName,
						"=DateTime.Parse(ds.Tables[0].Rows[0][\"",
						columnName,
						"\"].ToString());\r\n"
					}));
					stringBuilder.Append(base.Space(4) + "}\r\n");
					continue;
				case "string":
					stringBuilder.Append(base.Space(4) + "if(ds.Tables[0].Rows[0][\"" + columnName + "\"]!=null)\r\n");
					stringBuilder.Append(base.Space(4) + "{\r\n");
					stringBuilder.Append(string.Concat(new string[]
					{
						base.Space(5),
						"this.",
						columnName,
						"=ds.Tables[0].Rows[0][\"",
						columnName,
						"\"].ToString();\r\n"
					}));
					stringBuilder.Append(base.Space(4) + "}\r\n");
					continue;
				case "bool":
					stringBuilder.Append(string.Concat(new string[]
					{
						base.Space(4),
						"if(ds.Tables[0].Rows[0][\"",
						columnName,
						"\"]!=null && ds.Tables[0].Rows[0][\"",
						columnName,
						"\"].ToString()!=\"\")\r\n"
					}));
					stringBuilder.Append(base.Space(4) + "{\r\n");
					stringBuilder.Append(string.Concat(new string[]
					{
						base.Space(5),
						"if((ds.Tables[0].Rows[0][\"",
						columnName,
						"\"].ToString()==\"1\")||(ds.Tables[0].Rows[0][\"",
						columnName,
						"\"].ToString().ToLower()==\"true\"))\r\n"
					}));
					stringBuilder.Append(base.Space(5) + "{\r\n");
					stringBuilder.Append(base.Space(6) + "this." + columnName + "=true;\r\n");
					stringBuilder.Append(base.Space(5) + "}\r\n");
					stringBuilder.Append(base.Space(5) + "else\r\n");
					stringBuilder.Append(base.Space(5) + "{\r\n");
					stringBuilder.Append(base.Space(6) + "this." + columnName + "=false;\r\n");
					stringBuilder.Append(base.Space(5) + "}\r\n");
					stringBuilder.Append(base.Space(4) + "}\r\n\r\n");
					continue;
				case "byte[]":
					stringBuilder.Append(string.Concat(new string[]
					{
						base.Space(4),
						"if(ds.Tables[0].Rows[0][\"",
						columnName,
						"\"]!=null && ds.Tables[0].Rows[0][\"",
						columnName,
						"\"].ToString()!=\"\")\r\n"
					}));
					stringBuilder.Append(base.Space(4) + "{\r\n");
					stringBuilder.Append(string.Concat(new string[]
					{
						base.Space(5),
						"this.",
						columnName,
						"=(byte[])ds.Tables[0].Rows[0][\"",
						columnName,
						"\"];\r\n"
					}));
					stringBuilder.Append(base.Space(4) + "}\r\n");
					continue;
				}
				stringBuilder.Append(string.Concat(new string[]
				{
					base.Space(5),
					"this.",
					columnName,
					"=ds.Tables[0].Rows[0][\"",
					columnName,
					"\"].ToString();\r\n"
				}));
			}
			stringBuilder.Append(base.Space(3) + "}\r\n");
			stringBuilder.Append(base.Space(2) + "}");
			return stringBuilder.ToString();
		}
		private string CreatGetModelParam()
		{
			StringPlus stringPlus = new StringPlus();
			stringPlus.AppendLine();
			stringPlus.AppendSpaceLine(2, "/// <summary>");
			stringPlus.AppendSpaceLine(2, "/// " + this.Languagelist["summaryGetModel"].ToString());
			stringPlus.AppendSpaceLine(2, "/// </summary>");
			stringPlus.AppendSpaceLine(2, "public void GetModel(" + CodeCommon.GetInParameter(base.Keys, true) + ")");
			stringPlus.AppendSpaceLine(2, "{");
			stringPlus.AppendSpaceLine(3, "StringBuilder strSql=new StringBuilder();");
			stringPlus.AppendSpace(3, "strSql.Append(\"select ");
			stringPlus.AppendLine(base.Fieldstrlist + " \");");
			stringPlus.AppendSpaceLine(3, "strSql.Append(\" FROM [" + base.TableName + "] \");");
			if (this.GetWhereExpression(base.Keys).Length > 0)
			{
				stringPlus.AppendSpaceLine(3, "strSql.Append(\" where " + this.GetWhereExpression(base.Keys) + "\");");
			}
			else
			{
				stringPlus.AppendSpaceLine(3, "//strSql.Append(\" where 条件);");
			}
			stringPlus.AppendLine(this.GetPreParameter(base.Keys));
			stringPlus.AppendSpaceLine(3, "DataSet ds=" + base.DbHelperName + ".Query(strSql.ToString(),parameters);");
			stringPlus.AppendSpaceLine(3, "if(ds.Tables[0].Rows.Count>0)");
			stringPlus.AppendSpaceLine(3, "{");
			foreach (ColumnInfo current in base.Fieldlist)
			{
				string columnName = current.ColumnName;
				string typeName = current.TypeName;
				string key;
				switch (key = CodeCommon.DbTypeToCS(typeName))
				{
				case "int":
					stringPlus.AppendSpaceLine(4, string.Concat(new string[]
					{
						"if(ds.Tables[0].Rows[0][\"",
						columnName,
						"\"]!=null && ds.Tables[0].Rows[0][\"",
						columnName,
						"\"].ToString()!=\"\")"
					}));
					stringPlus.AppendSpaceLine(4, "{");
					stringPlus.AppendSpaceLine(5, string.Concat(new string[]
					{
						"this.",
						columnName,
						"=int.Parse(ds.Tables[0].Rows[0][\"",
						columnName,
						"\"].ToString());"
					}));
					stringPlus.AppendSpaceLine(4, "}");
					continue;
				case "long":
					stringPlus.AppendSpaceLine(4, string.Concat(new string[]
					{
						"if(ds.Tables[0].Rows[0][\"",
						columnName,
						"\"]!=null && ds.Tables[0].Rows[0][\"",
						columnName,
						"\"].ToString()!=\"\")"
					}));
					stringPlus.AppendSpaceLine(4, "{");
					stringPlus.AppendSpaceLine(5, string.Concat(new string[]
					{
						"this.",
						columnName,
						"=long.Parse(ds.Tables[0].Rows[0][\"",
						columnName,
						"\"].ToString());"
					}));
					stringPlus.AppendSpaceLine(4, "}");
					continue;
				case "decimal":
					stringPlus.AppendSpaceLine(4, string.Concat(new string[]
					{
						"if(ds.Tables[0].Rows[0][\"",
						columnName,
						"\"]!=null && ds.Tables[0].Rows[0][\"",
						columnName,
						"\"].ToString()!=\"\")"
					}));
					stringPlus.AppendSpaceLine(4, "{");
					stringPlus.AppendSpaceLine(5, string.Concat(new string[]
					{
						"this.",
						columnName,
						"=decimal.Parse(ds.Tables[0].Rows[0][\"",
						columnName,
						"\"].ToString());"
					}));
					stringPlus.AppendSpaceLine(4, "}");
					continue;
				case "float":
					stringPlus.AppendSpaceLine(4, string.Concat(new string[]
					{
						"if(ds.Tables[0].Rows[0][\"",
						columnName,
						"\"]!=null && ds.Tables[0].Rows[0][\"",
						columnName,
						"\"].ToString()!=\"\")"
					}));
					stringPlus.AppendSpaceLine(4, "{");
					stringPlus.AppendSpaceLine(5, string.Concat(new string[]
					{
						"this.",
						columnName,
						"=float.Parse(ds.Tables[0].Rows[0][\"",
						columnName,
						"\"].ToString());"
					}));
					stringPlus.AppendSpaceLine(4, "}");
					continue;
				case "DateTime":
					stringPlus.AppendSpaceLine(4, string.Concat(new string[]
					{
						"if(ds.Tables[0].Rows[0][\"",
						columnName,
						"\"]!=null && ds.Tables[0].Rows[0][\"",
						columnName,
						"\"].ToString()!=\"\")"
					}));
					stringPlus.AppendSpaceLine(4, "{");
					stringPlus.AppendSpaceLine(5, string.Concat(new string[]
					{
						"this.",
						columnName,
						"=DateTime.Parse(ds.Tables[0].Rows[0][\"",
						columnName,
						"\"].ToString());"
					}));
					stringPlus.AppendSpaceLine(4, "}");
					continue;
				case "string":
					stringPlus.AppendSpaceLine(4, "if(ds.Tables[0].Rows[0][\"" + columnName + "\"]!=null )");
					stringPlus.AppendSpaceLine(4, "{");
					stringPlus.AppendSpaceLine(5, string.Concat(new string[]
					{
						"this.",
						columnName,
						"=ds.Tables[0].Rows[0][\"",
						columnName,
						"\"].ToString();"
					}));
					stringPlus.AppendSpaceLine(4, "}");
					continue;
				case "bool":
					stringPlus.AppendSpaceLine(4, string.Concat(new string[]
					{
						"if(ds.Tables[0].Rows[0][\"",
						columnName,
						"\"]!=null && ds.Tables[0].Rows[0][\"",
						columnName,
						"\"].ToString()!=\"\")"
					}));
					stringPlus.AppendSpaceLine(4, "{");
					stringPlus.AppendSpaceLine(5, string.Concat(new string[]
					{
						"if((ds.Tables[0].Rows[0][\"",
						columnName,
						"\"].ToString()==\"1\")||(ds.Tables[0].Rows[0][\"",
						columnName,
						"\"].ToString().ToLower()==\"true\"))"
					}));
					stringPlus.AppendSpaceLine(5, "{");
					stringPlus.AppendSpaceLine(6, "this." + columnName + "=true;");
					stringPlus.AppendSpaceLine(5, "}");
					stringPlus.AppendSpaceLine(5, "else");
					stringPlus.AppendSpaceLine(5, "{");
					stringPlus.AppendSpaceLine(6, "this." + columnName + "=false;");
					stringPlus.AppendSpaceLine(5, "}");
					stringPlus.AppendSpaceLine(4, "}");
					continue;
				case "byte[]":
					stringPlus.AppendSpaceLine(4, string.Concat(new string[]
					{
						"if(ds.Tables[0].Rows[0][\"",
						columnName,
						"\"]!=null && ds.Tables[0].Rows[0][\"",
						columnName,
						"\"].ToString()!=\"\")"
					}));
					stringPlus.AppendSpaceLine(4, "{");
					stringPlus.AppendSpaceLine(5, string.Concat(new string[]
					{
						"this.",
						columnName,
						"=(byte[])ds.Tables[0].Rows[0][\"",
						columnName,
						"\"];"
					}));
					stringPlus.AppendSpaceLine(4, "}");
					continue;
				case "Guid":
					stringPlus.AppendSpaceLine(4, string.Concat(new string[]
					{
						"if(ds.Tables[0].Rows[0][\"",
						columnName,
						"\"]!=null && ds.Tables[0].Rows[0][\"",
						columnName,
						"\"].ToString()!=\"\")"
					}));
					stringPlus.AppendSpaceLine(4, "{");
					stringPlus.AppendSpaceLine(5, string.Concat(new string[]
					{
						"this.",
						columnName,
						"=new Guid(ds.Tables[0].Rows[0][\"",
						columnName,
						"\"].ToString());"
					}));
					stringPlus.AppendSpaceLine(4, "}");
					continue;
				}
				stringPlus.AppendSpaceLine(5, string.Concat(new string[]
				{
					"//this.",
					columnName,
					"=ds.Tables[0].Rows[0][\"",
					columnName,
					"\"].ToString();"
				}));
			}
			stringPlus.AppendSpaceLine(3, "}");
			stringPlus.AppendSpaceLine(2, "}");
			return stringPlus.ToString();
		}
		private string CreatGetListParam()
		{
			StringPlus stringPlus = new StringPlus();
			stringPlus.AppendSpaceLine(2, "/// <summary>");
			stringPlus.AppendSpaceLine(2, "/// " + this.Languagelist["summaryGetList"].ToString());
			stringPlus.AppendSpaceLine(2, "/// </summary>");
			stringPlus.AppendSpaceLine(2, "public DataSet GetList(string strWhere)");
			stringPlus.AppendSpaceLine(2, "{");
			stringPlus.AppendSpaceLine(3, "StringBuilder strSql=new StringBuilder();");
			stringPlus.AppendSpace(3, "strSql.Append(\"select ");
			stringPlus.AppendLine("* \");");
			stringPlus.AppendSpaceLine(3, "strSql.Append(\" FROM [" + base.TableName + "] \");");
			stringPlus.AppendSpaceLine(3, "if(strWhere.Trim()!=\"\")");
			stringPlus.AppendSpaceLine(3, "{");
			stringPlus.AppendSpaceLine(4, "strSql.Append(\" where \"+strWhere);");
			stringPlus.AppendSpaceLine(3, "}");
			stringPlus.AppendSpaceLine(3, "return " + base.DbHelperName + ".Query(strSql.ToString());");
			stringPlus.AppendSpaceLine(2, "}");
			return stringPlus.Value;
		}
	}
}
