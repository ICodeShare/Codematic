using Maticsoft.CodeHelper;
using Maticsoft.IBuilder;
using Maticsoft.IDBO;
using Maticsoft.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
namespace Maticsoft.BuilderDALParam
{
	public class BuilderDAL : IBuilderDAL
	{
		protected string _IdentityKey = "";
		protected string _IdentityKeyType = "int";
		private IDbObject dbobj;
		private string _dbname;
		private string _tablename;
		private string _modelname;
		private string _dalname;
		private List<ColumnInfo> _fieldlist;
		private List<ColumnInfo> _keys;
		private string _namespace;
		private string _folder;
		private string _dbhelperName;
		private string _modelpath;
		private string _dalpath;
		private string _idalpath;
		private string _iclass;
		private string _procprefix;
		public IDbObject DbObject
		{
			get
			{
				return this.dbobj;
			}
			set
			{
				this.dbobj = value;
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
				foreach (ColumnInfo current in this._keys)
				{
					this._IdentityKey = current.ColumnName;
					this._IdentityKeyType = current.TypeName;
					if (current.IsIdentity)
					{
						this._IdentityKey = current.ColumnName;
						this._IdentityKeyType = CodeCommon.DbTypeToCS(current.TypeName);
						break;
					}
				}
			}
		}
		public string NameSpace
		{
			get
			{
				return this._namespace;
			}
			set
			{
				this._namespace = value;
			}
		}
		public string Folder
		{
			get
			{
				return this._folder;
			}
			set
			{
				this._folder = value;
			}
		}
		public string Modelpath
		{
			get
			{
				return this._modelpath;
			}
			set
			{
				this._modelpath = value;
			}
		}
		public string ModelName
		{
			get
			{
				return this._modelname;
			}
			set
			{
				this._modelname = value;
			}
		}
		public string ModelSpace
		{
			get
			{
				return this.Modelpath + "." + this.ModelName;
			}
		}
		public string DALpath
		{
			get
			{
				return this._dalpath;
			}
			set
			{
				this._dalpath = value;
			}
		}
		public string DALName
		{
			get
			{
				return this._dalname;
			}
			set
			{
				this._dalname = value;
			}
		}
		public string IDALpath
		{
			get
			{
				return this._idalpath;
			}
			set
			{
				this._idalpath = value;
			}
		}
		public string IClass
		{
			get
			{
				return this._iclass;
			}
			set
			{
				this._iclass = value;
			}
		}
		public string DbHelperName
		{
			get
			{
				return this._dbhelperName;
			}
			set
			{
				this._dbhelperName = value;
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
		public string DbParaHead
		{
			get
			{
				return CodeCommon.DbParaHead(this.dbobj.DbType);
			}
		}
		public string DbParaDbType
		{
			get
			{
				return CodeCommon.DbParaDbType(this.dbobj.DbType);
			}
		}
		public string preParameter
		{
			get
			{
				return CodeCommon.preParameter(this.dbobj.DbType);
			}
		}
		public bool IsHasIdentity
		{
			get
			{
				return CodeCommon.IsHasIdentity(this._keys);
			}
		}
		private string KeysNullTip
		{
			get
			{
				if (this._keys.Count == 0)
				{
					return "//该表无主键信息，请自定义主键/条件字段";
				}
				return "";
			}
		}
		public Hashtable Languagelist
		{
			get
			{
				return Language.LoadFromCfg("BuilderDALParam.lan");
			}
		}
		public BuilderDAL()
		{
		}
		public BuilderDAL(IDbObject idbobj)
		{
			this.dbobj = idbobj;
		}
		public BuilderDAL(IDbObject idbobj, string dbname, string tablename, string modelname, string dalName, List<ColumnInfo> fieldlist, List<ColumnInfo> keys, string namepace, string folder, string dbherlpername, string modelpath, string dalpath, string idalpath, string iclass)
		{
			this.dbobj = idbobj;
			this._dbname = dbname;
			this._tablename = tablename;
			this._modelname = modelname;
			this._dalname = dalName;
			this._namespace = namepace;
			this._folder = folder;
			this._dbhelperName = dbherlpername;
			this._modelpath = modelpath;
			this._dalpath = dalpath;
			this._idalpath = idalpath;
			this._iclass = iclass;
			this.Fieldlist = fieldlist;
			this.Keys = keys;
			foreach (ColumnInfo current in this._keys)
			{
				this._IdentityKey = current.ColumnName;
				this._IdentityKeyType = current.TypeName;
				if (current.IsIdentity)
				{
					this._IdentityKey = current.ColumnName;
					this._IdentityKeyType = CodeCommon.DbTypeToCS(current.TypeName);
					break;
				}
			}
		}
		public string GetDALCode(bool Maxid, bool Exists, bool Add, bool Update, bool Delete, bool GetModel, bool List)
		{
			StringPlus stringPlus = new StringPlus();
			stringPlus.AppendLine("using System;");
			stringPlus.AppendLine("using System.Data;");
			stringPlus.AppendLine("using System.Text;");
			string dbType;
			switch (dbType = this.dbobj.DbType)
			{
			case "SQL2008":
			case "SQL2005":
			case "SQL2000":
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
			case "SQLite":
				stringPlus.AppendLine("using System.Data.SQLite;");
				break;
			}
			if (this.IDALpath != "")
			{
				stringPlus.AppendLine("using " + this.IDALpath + ";");
			}
			stringPlus.AppendLine("using Maticsoft.DBUtility;//Please add references");
			stringPlus.AppendLine("namespace " + this.DALpath);
			stringPlus.AppendLine("{");
			stringPlus.AppendSpaceLine(1, "/// <summary>");
			stringPlus.AppendSpaceLine(1, "/// " + this.Languagelist["summary"].ToString() + ":" + this.DALName);
			stringPlus.AppendSpaceLine(1, "/// </summary>");
			stringPlus.AppendSpace(1, "public partial class " + this.DALName);
			if (this.IClass != "")
			{
				stringPlus.Append(":" + this.IClass);
			}
			stringPlus.AppendLine("");
			stringPlus.AppendSpaceLine(1, "{");
			stringPlus.AppendSpaceLine(2, "public " + this.DALName + "()");
			stringPlus.AppendSpaceLine(2, "{}");
			stringPlus.AppendSpaceLine(2, "#region  BasicMethod");
			if (Maxid)
			{
				stringPlus.AppendLine(this.CreatGetMaxID());
			}
			if (Exists)
			{
				stringPlus.AppendLine(this.CreatExists());
			}
			if (Add)
			{
				stringPlus.AppendLine(this.CreatAdd());
			}
			if (Update)
			{
				stringPlus.AppendLine(this.CreatUpdate());
			}
			if (Delete)
			{
				stringPlus.AppendLine(this.CreatDelete());
			}
			if (GetModel)
			{
				stringPlus.AppendLine(this.CreatGetModel());
				stringPlus.AppendLine(this.CreatDataRowToModel());
			}
			if (List)
			{
				stringPlus.AppendLine(this.CreatGetList());
				stringPlus.AppendLine(this.CreatGetListByPage());
				stringPlus.AppendLine(this.CreatGetListByPageProc());
			}
			stringPlus.AppendSpaceLine(2, "#endregion  BasicMethod");
			stringPlus.AppendSpaceLine(2, "#region  ExtensionMethod");
			stringPlus.AppendLine("");
			stringPlus.AppendSpaceLine(2, "#endregion  ExtensionMethod");
			stringPlus.AppendSpaceLine(1, "}");
			stringPlus.AppendLine("}");
			stringPlus.AppendLine("");
			return stringPlus.ToString();
		}
		public string CreatGetMaxID()
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
							stringPlus.AppendLine("");
							stringPlus.AppendSpaceLine(2, "/// <summary>");
							stringPlus.AppendSpaceLine(2, "/// " + this.Languagelist["summaryGetMaxId"].ToString());
							stringPlus.AppendSpaceLine(2, "/// </summary>");
							stringPlus.AppendSpaceLine(2, "public int GetMaxId()");
							stringPlus.AppendSpaceLine(2, "{");
							stringPlus.AppendSpaceLine(2, string.Concat(new string[]
							{
								"return ",
								this.DbHelperName,
								".GetMaxID(\"",
								columnName,
								"\", \"",
								this._tablename,
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
		public string CreatExists()
		{
			StringPlus stringPlus = new StringPlus();
			if (this._keys.Count > 0)
			{
				string inParameter = CodeCommon.GetInParameter(this.Keys, false);
				if (!string.IsNullOrEmpty(inParameter))
				{
					stringPlus.AppendSpaceLine(2, "/// <summary>");
					stringPlus.AppendSpaceLine(2, "/// " + this.Languagelist["summaryExists"].ToString());
					stringPlus.AppendSpaceLine(2, "/// </summary>");
					stringPlus.AppendSpaceLine(2, "public bool Exists(" + inParameter + ")");
					stringPlus.AppendSpaceLine(2, "{");
					stringPlus.AppendSpaceLine(3, "StringBuilder strSql=new StringBuilder();");
					stringPlus.AppendSpaceLine(3, "strSql.Append(\"select count(1) from " + this._tablename + "\");");
					stringPlus.AppendSpaceLine(3, "strSql.Append(\" where " + CodeCommon.GetWhereParameterExpression(this.Keys, false, this.dbobj.DbType) + "\");");
					stringPlus.AppendLine(CodeCommon.GetPreParameter(this.Keys, false, this.dbobj.DbType));
					stringPlus.AppendSpaceLine(3, "return " + this.DbHelperName + ".Exists(strSql.ToString(),parameters);");
					stringPlus.AppendSpaceLine(2, "}");
				}
			}
			return stringPlus.Value;
		}
		public string CreatAdd()
		{
            if (ModelSpace == "")
            {
                //ModelSpace = "ModelClassName"; ;
            }
			StringPlus stringPlus = new StringPlus();
			StringPlus stringPlus2 = new StringPlus();
			StringPlus stringPlus3 = new StringPlus();
			StringPlus stringPlus4 = new StringPlus();
			StringPlus stringPlus5 = new StringPlus();
			stringPlus.AppendLine();
			stringPlus.AppendSpaceLine(2, "/// <summary>");
			stringPlus.AppendSpaceLine(2, "/// " + this.Languagelist["summaryadd"].ToString());
			stringPlus.AppendSpaceLine(2, "/// </summary>");
			string text = "bool";
			if ((this.dbobj.DbType == "SQL2000" || this.dbobj.DbType == "SQL2005" || this.dbobj.DbType == "SQL2008" || this.dbobj.DbType == "SQL2012" || this.dbobj.DbType == "SQLite") && this.IsHasIdentity)
			{
				text = "int";
				if (this._IdentityKeyType != "int")
				{
					text = this._IdentityKeyType;
				}
			}
			string text2 = string.Concat(new string[]
			{
				CodeCommon.Space(2),
				"public ",
				text,
				" Add(",
				this.ModelSpace,
				" model)"
			});
			stringPlus.AppendLine(text2);
			stringPlus.AppendSpaceLine(2, "{");
			stringPlus.AppendSpaceLine(3, "StringBuilder strSql=new StringBuilder();");
			stringPlus.AppendSpaceLine(3, "strSql.Append(\"insert into " + this._tablename + "(\");");
			stringPlus2.AppendSpace(3, "strSql.Append(\"");
			int num = 0;
			foreach (ColumnInfo current in this.Fieldlist)
			{
				string columnName = current.ColumnName;
				string typeName = current.TypeName;
				bool arg_1E5_0 = current.IsIdentity;
				string length = current.Length;
				bool arg_1F6_0 = current.Nullable;
				if (!current.IsIdentity)
				{
					stringPlus4.AppendSpaceLine(5, string.Concat(new string[]
					{
						"new ",
						this.DbParaHead,
						"Parameter(\"",
						this.preParameter,
						columnName,
						"\", ",
						this.DbParaDbType,
						".",
						CodeCommon.DbTypeLength(this.dbobj.DbType, typeName, length),
						"),"
					}));
					stringPlus2.Append(columnName + ",");
					stringPlus3.Append(this.preParameter + columnName + ",");
					if ("uniqueidentifier" == typeName.ToLower())
					{
						stringPlus5.AppendSpaceLine(3, "parameters[" + num + "].Value = Guid.NewGuid();");
					}
					else
					{
						stringPlus5.AppendSpaceLine(3, string.Concat(new object[]
						{
							"parameters[",
							num,
							"].Value = model.",
							columnName,
							";"
						}));
					}
					num++;
				}
			}
			stringPlus2.DelLastComma();
			stringPlus3.DelLastComma();
			stringPlus4.DelLastComma();
			stringPlus2.AppendLine(")\");");
			stringPlus.Append(stringPlus2.ToString());
			stringPlus.AppendSpaceLine(3, "strSql.Append(\" values (\");");
			stringPlus.AppendSpaceLine(3, "strSql.Append(\"" + stringPlus3.ToString() + ")\");");
			if ((this.dbobj.DbType == "SQL2000" || this.dbobj.DbType == "SQL2005" || this.dbobj.DbType == "SQL2008" || this.dbobj.DbType == "SQL2012") && this.IsHasIdentity)
			{
				stringPlus.AppendSpaceLine(3, "strSql.Append(\";select @@IDENTITY\");");
			}
			if (this.dbobj.DbType == "SQLite" && this.IsHasIdentity)
			{
				stringPlus.AppendSpaceLine(3, "strSql.Append(\";select LAST_INSERT_ROWID()\");");
			}
			stringPlus.AppendSpaceLine(3, this.DbParaHead + "Parameter[] parameters = {");
			stringPlus.Append(stringPlus4.Value);
			stringPlus.AppendLine("};");
			stringPlus.AppendLine(stringPlus5.Value);
			if (text == "void")
			{
				stringPlus.AppendSpaceLine(3, this.DbHelperName + ".ExecuteSql(strSql.ToString(),parameters);");
			}
			else
			{
				if (text == "bool")
				{
					stringPlus.AppendSpaceLine(3, "int rows=" + this.DbHelperName + ".ExecuteSql(strSql.ToString(),parameters);");
					stringPlus.AppendSpaceLine(3, "if (rows > 0)");
					stringPlus.AppendSpaceLine(3, "{");
					stringPlus.AppendSpaceLine(4, "return true;");
					stringPlus.AppendSpaceLine(3, "}");
					stringPlus.AppendSpaceLine(3, "else");
					stringPlus.AppendSpaceLine(3, "{");
					stringPlus.AppendSpaceLine(4, "return false;");
					stringPlus.AppendSpaceLine(3, "}");
				}
				else
				{
					stringPlus.AppendSpaceLine(3, "object obj = " + this.DbHelperName + ".GetSingle(strSql.ToString(),parameters);");
					stringPlus.AppendSpaceLine(3, "if (obj == null)");
					stringPlus.AppendSpaceLine(3, "{");
					stringPlus.AppendSpaceLine(4, "return 0;");
					stringPlus.AppendSpaceLine(3, "}");
					stringPlus.AppendSpaceLine(3, "else");
					stringPlus.AppendSpaceLine(3, "{");
					string a;
					if ((a = text) != null)
					{
						if (!(a == "int"))
						{
							if (!(a == "long"))
							{
								if (a == "decimal")
								{
									stringPlus.AppendSpaceLine(4, "return Convert.ToDecimal(obj);");
								}
							}
							else
							{
								stringPlus.AppendSpaceLine(4, "return Convert.ToInt64(obj);");
							}
						}
						else
						{
							stringPlus.AppendSpaceLine(4, "return Convert.ToInt32(obj);");
						}
					}
					stringPlus.AppendSpaceLine(3, "}");
				}
			}
			stringPlus.AppendSpace(2, "}");
			return stringPlus.ToString();
		}
		public string CreatUpdate()
		{
            if (ModelSpace == "")
            {
                //ModelSpace = "ModelClassName"; ;
            }
			StringPlus stringPlus = new StringPlus();
			StringPlus stringPlus2 = new StringPlus();
			StringPlus stringPlus3 = new StringPlus();
			StringPlus stringPlus4 = new StringPlus();
			stringPlus.AppendSpaceLine(2, "/// <summary>");
			stringPlus.AppendSpaceLine(2, "/// " + this.Languagelist["summaryUpdate"].ToString());
			stringPlus.AppendSpaceLine(2, "/// </summary>");
			stringPlus.AppendSpaceLine(2, "public bool Update(" + this.ModelSpace + " model)");
			stringPlus.AppendSpaceLine(2, "{");
			stringPlus.AppendSpaceLine(3, "StringBuilder strSql=new StringBuilder();");
			stringPlus.AppendSpaceLine(3, "strSql.Append(\"update " + this._tablename + " set \");");
			int num = 0;
			if (this.Fieldlist.Count == 0)
			{
				this.Fieldlist = this.Keys;
			}
			List<ColumnInfo> list = new List<ColumnInfo>();
			foreach (ColumnInfo current in this.Fieldlist)
			{
				string columnName = current.ColumnName;
				string typeName = current.TypeName;
				string length = current.Length;
				bool arg_11E_0 = current.IsIdentity;
				bool arg_126_0 = current.IsPrimaryKey;
				if (current.IsIdentity || current.IsPrimaryKey || this.Keys.Contains(current))
				{
					list.Add(current);
				}
				else
				{
					if (!(typeName.ToLower() == "timestamp"))
					{
						stringPlus2.AppendSpaceLine(5, string.Concat(new string[]
						{
							"new ",
							this.DbParaHead,
							"Parameter(\"",
							this.preParameter,
							columnName,
							"\", ",
							this.DbParaDbType,
							".",
							CodeCommon.DbTypeLength(this.dbobj.DbType, typeName, length),
							"),"
						}));
						stringPlus3.AppendSpaceLine(3, string.Concat(new object[]
						{
							"parameters[",
							num,
							"].Value = model.",
							columnName,
							";"
						}));
						num++;
						stringPlus4.AppendSpaceLine(3, string.Concat(new string[]
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
			}
			foreach (ColumnInfo current2 in list)
			{
				string columnName2 = current2.ColumnName;
				string typeName2 = current2.TypeName;
				string length2 = current2.Length;
				bool arg_2D4_0 = current2.IsIdentity;
				bool arg_2DC_0 = current2.IsPrimaryKey;
				stringPlus2.AppendSpaceLine(5, string.Concat(new string[]
				{
					"new ",
					this.DbParaHead,
					"Parameter(\"",
					this.preParameter,
					columnName2,
					"\", ",
					this.DbParaDbType,
					".",
					CodeCommon.DbTypeLength(this.dbobj.DbType, typeName2, length2),
					"),"
				}));
				stringPlus3.AppendSpaceLine(3, string.Concat(new object[]
				{
					"parameters[",
					num,
					"].Value = model.",
					columnName2,
					";"
				}));
				num++;
			}
			if (stringPlus4.Value.Length > 0)
			{
				stringPlus4.DelLastComma();
				stringPlus4.AppendLine("\");");
			}
			else
			{
				stringPlus4.AppendLine("#warning 系统发现缺少更新的字段，请手工确认如此更新是否正确！ ");
				foreach (ColumnInfo current3 in this.Fieldlist)
				{
					string columnName3 = current3.ColumnName;
					stringPlus4.AppendSpaceLine(3, string.Concat(new string[]
					{
						"strSql.Append(\"",
						columnName3,
						"=",
						this.preParameter,
						columnName3,
						",\");"
					}));
				}
				if (this.Fieldlist.Count > 0)
				{
					stringPlus4.DelLastComma();
					stringPlus4.AppendLine("\");");
				}
			}
			stringPlus.Append(stringPlus4.Value);
			stringPlus.AppendSpaceLine(3, "strSql.Append(\" where " + CodeCommon.GetWhereParameterExpression(this.Keys, true, this.dbobj.DbType) + "\");");
			stringPlus.AppendSpaceLine(3, this.DbParaHead + "Parameter[] parameters = {");
			stringPlus2.DelLastComma();
			stringPlus.Append(stringPlus2.Value);
			stringPlus.AppendLine("};");
			stringPlus.AppendLine(stringPlus3.Value);
			stringPlus.AppendSpaceLine(3, "int rows=" + this.DbHelperName + ".ExecuteSql(strSql.ToString(),parameters);");
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
		public string CreatDelete()
		{
			StringPlus stringPlus = new StringPlus();
			stringPlus.AppendSpaceLine(2, "/// <summary>");
			stringPlus.AppendSpaceLine(2, "/// " + this.Languagelist["summaryDelete"].ToString());
			stringPlus.AppendSpaceLine(2, "/// </summary>");
			stringPlus.AppendSpaceLine(2, "public bool Delete(" + CodeCommon.GetInParameter(this.Keys, true) + ")");
			stringPlus.AppendSpaceLine(2, "{");
			stringPlus.AppendSpaceLine(3, this.KeysNullTip);
			stringPlus.AppendSpaceLine(3, "StringBuilder strSql=new StringBuilder();");
			stringPlus.AppendSpaceLine(3, "strSql.Append(\"delete from " + this._tablename + " \");");
			stringPlus.AppendSpaceLine(3, "strSql.Append(\" where " + CodeCommon.GetWhereParameterExpression(this.Keys, true, this.dbobj.DbType) + "\");");
			stringPlus.AppendLine(CodeCommon.GetPreParameter(this.Keys, true, this.dbobj.DbType));
			stringPlus.AppendSpaceLine(3, "int rows=" + this.DbHelperName + ".ExecuteSql(strSql.ToString(),parameters);");
			stringPlus.AppendSpaceLine(3, "if (rows > 0)");
			stringPlus.AppendSpaceLine(3, "{");
			stringPlus.AppendSpaceLine(4, "return true;");
			stringPlus.AppendSpaceLine(3, "}");
			stringPlus.AppendSpaceLine(3, "else");
			stringPlus.AppendSpaceLine(3, "{");
			stringPlus.AppendSpaceLine(4, "return false;");
			stringPlus.AppendSpaceLine(3, "}");
			stringPlus.AppendSpaceLine(2, "}");
			if (CodeCommon.HasNoIdentityKey(this.Keys) && CodeCommon.GetIdentityKey(this.Keys) != null)
			{
				stringPlus.AppendSpaceLine(2, "/// <summary>");
				stringPlus.AppendSpaceLine(2, "/// " + this.Languagelist["summaryDelete"].ToString());
				stringPlus.AppendSpaceLine(2, "/// </summary>");
				stringPlus.AppendSpaceLine(2, "public bool Delete(" + CodeCommon.GetInParameter(this.Keys, false) + ")");
				stringPlus.AppendSpaceLine(2, "{");
				stringPlus.AppendSpaceLine(3, this.KeysNullTip);
				stringPlus.AppendSpaceLine(3, "StringBuilder strSql=new StringBuilder();");
				stringPlus.AppendSpaceLine(3, "strSql.Append(\"delete from " + this._tablename + " \");");
				stringPlus.AppendSpaceLine(3, "strSql.Append(\" where " + CodeCommon.GetWhereParameterExpression(this.Keys, false, this.dbobj.DbType) + "\");");
				stringPlus.AppendLine(CodeCommon.GetPreParameter(this.Keys, false, this.dbobj.DbType));
				stringPlus.AppendSpaceLine(3, "int rows=" + this.DbHelperName + ".ExecuteSql(strSql.ToString(),parameters);");
				stringPlus.AppendSpaceLine(3, "if (rows > 0)");
				stringPlus.AppendSpaceLine(3, "{");
				stringPlus.AppendSpaceLine(4, "return true;");
				stringPlus.AppendSpaceLine(3, "}");
				stringPlus.AppendSpaceLine(3, "else");
				stringPlus.AppendSpaceLine(3, "{");
				stringPlus.AppendSpaceLine(4, "return false;");
				stringPlus.AppendSpaceLine(3, "}");
				stringPlus.AppendSpaceLine(2, "}");
			}
			string text = "";
			if (this.Keys.Count == 1)
			{
				text = this.Keys[0].ColumnName;
			}
			else
			{
				foreach (ColumnInfo current in this.Keys)
				{
					if (current.IsIdentity)
					{
						text = current.ColumnName;
						break;
					}
				}
			}
			if (text.Trim().Length > 0)
			{
				stringPlus.AppendSpaceLine(2, "/// <summary>");
				stringPlus.AppendSpaceLine(2, "/// " + this.Languagelist["summaryDeletelist"].ToString());
				stringPlus.AppendSpaceLine(2, "/// </summary>");
				stringPlus.AppendSpaceLine(2, "public bool DeleteList(string " + text + "list )");
				stringPlus.AppendSpaceLine(2, "{");
				stringPlus.AppendSpaceLine(3, "StringBuilder strSql=new StringBuilder();");
				stringPlus.AppendSpaceLine(3, "strSql.Append(\"delete from " + this._tablename + " \");");
				stringPlus.AppendSpaceLine(3, string.Concat(new string[]
				{
					"strSql.Append(\" where ",
					text,
					" in (\"+",
					text,
					"list + \")  \");"
				}));
				stringPlus.AppendSpaceLine(3, "int rows=" + this.DbHelperName + ".ExecuteSql(strSql.ToString());");
				stringPlus.AppendSpaceLine(3, "if (rows > 0)");
				stringPlus.AppendSpaceLine(3, "{");
				stringPlus.AppendSpaceLine(4, "return true;");
				stringPlus.AppendSpaceLine(3, "}");
				stringPlus.AppendSpaceLine(3, "else");
				stringPlus.AppendSpaceLine(3, "{");
				stringPlus.AppendSpaceLine(4, "return false;");
				stringPlus.AppendSpaceLine(3, "}");
				stringPlus.AppendSpaceLine(2, "}");
			}
			return stringPlus.Value;
		}
		public string CreatGetModel()
		{
            if (ModelSpace == "")
            {
                //ModelSpace = "ModelClassName"; ;
            }
			StringPlus stringPlus = new StringPlus();
			stringPlus.AppendLine();
			stringPlus.AppendSpaceLine(2, "/// <summary>");
			stringPlus.AppendSpaceLine(2, "/// " + this.Languagelist["summaryGetModel"].ToString());
			stringPlus.AppendSpaceLine(2, "/// </summary>");
			stringPlus.AppendSpaceLine(2, string.Concat(new string[]
			{
				"public ",
				this.ModelSpace,
				" GetModel(",
				CodeCommon.GetInParameter(this.Keys, true),
				")"
			}));
			stringPlus.AppendSpaceLine(2, "{");
			stringPlus.AppendSpaceLine(3, this.KeysNullTip);
			stringPlus.AppendSpaceLine(3, "StringBuilder strSql=new StringBuilder();");
			stringPlus.AppendSpace(3, "strSql.Append(\"select ");
			if (this.dbobj.DbType == "SQL2005" || this.dbobj.DbType == "SQL2000" || this.dbobj.DbType == "SQL2008" || this.dbobj.DbType == "SQL2012")
			{
				stringPlus.Append(" top 1 ");
			}
			stringPlus.AppendLine(this.Fieldstrlist + " from " + this._tablename + " \");");
			stringPlus.AppendSpaceLine(3, "strSql.Append(\" where " + CodeCommon.GetWhereParameterExpression(this.Keys, true, this.dbobj.DbType) + "\");");
			stringPlus.AppendLine(CodeCommon.GetPreParameter(this.Keys, true, this.dbobj.DbType));
			stringPlus.AppendSpaceLine(3, this.ModelSpace + " model=new " + this.ModelSpace + "();");
			stringPlus.AppendSpaceLine(3, "DataSet ds=" + this.DbHelperName + ".Query(strSql.ToString(),parameters);");
			stringPlus.AppendSpaceLine(3, "if(ds.Tables[0].Rows.Count>0)");
			stringPlus.AppendSpaceLine(3, "{");
			stringPlus.AppendSpaceLine(4, "return DataRowToModel(ds.Tables[0].Rows[0]);");
			stringPlus.AppendSpaceLine(3, "}");
			stringPlus.AppendSpaceLine(3, "else");
			stringPlus.AppendSpaceLine(3, "{");
			stringPlus.AppendSpaceLine(4, "return null;");
			stringPlus.AppendSpaceLine(3, "}");
			stringPlus.AppendSpaceLine(2, "}");
			return stringPlus.ToString();
		}
		public string CreatDataRowToModel()
		{
            if (ModelSpace == "")
            {
                //ModelSpace = "ModelClassName"; ;
            }
			StringPlus stringPlus = new StringPlus();
			stringPlus.AppendLine();
			stringPlus.AppendSpaceLine(2, "/// <summary>");
			stringPlus.AppendSpaceLine(2, "/// " + this.Languagelist["summaryGetModel"].ToString());
			stringPlus.AppendSpaceLine(2, "/// </summary>");
			stringPlus.AppendSpaceLine(2, "public " + this.ModelSpace + " DataRowToModel(DataRow row)");
			stringPlus.AppendSpaceLine(2, "{");
			stringPlus.AppendSpaceLine(3, this.ModelSpace + " model=new " + this.ModelSpace + "();");
			stringPlus.AppendSpaceLine(3, "if (row != null)");
			stringPlus.AppendSpaceLine(3, "{");
			foreach (ColumnInfo current in this.Fieldlist)
			{
				string columnName = current.ColumnName;
				string typeName = current.TypeName;
				string key;
				switch (key = CodeCommon.DbTypeToCS(typeName))
				{
				case "int":
					stringPlus.AppendSpaceLine(4, string.Concat(new string[]
					{
						"if(row[\"",
						columnName,
						"\"]!=null && row[\"",
						columnName,
						"\"].ToString()!=\"\")"
					}));
					stringPlus.AppendSpaceLine(4, "{");
					stringPlus.AppendSpaceLine(5, string.Concat(new string[]
					{
						"model.",
						columnName,
						"=int.Parse(row[\"",
						columnName,
						"\"].ToString());"
					}));
					stringPlus.AppendSpaceLine(4, "}");
					continue;
				case "long":
					stringPlus.AppendSpaceLine(4, string.Concat(new string[]
					{
						"if(row[\"",
						columnName,
						"\"]!=null && row[\"",
						columnName,
						"\"].ToString()!=\"\")"
					}));
					stringPlus.AppendSpaceLine(4, "{");
					stringPlus.AppendSpaceLine(5, string.Concat(new string[]
					{
						"model.",
						columnName,
						"=long.Parse(row[\"",
						columnName,
						"\"].ToString());"
					}));
					stringPlus.AppendSpaceLine(4, "}");
					continue;
				case "decimal":
					stringPlus.AppendSpaceLine(4, string.Concat(new string[]
					{
						"if(row[\"",
						columnName,
						"\"]!=null && row[\"",
						columnName,
						"\"].ToString()!=\"\")"
					}));
					stringPlus.AppendSpaceLine(4, "{");
					stringPlus.AppendSpaceLine(5, string.Concat(new string[]
					{
						"model.",
						columnName,
						"=decimal.Parse(row[\"",
						columnName,
						"\"].ToString());"
					}));
					stringPlus.AppendSpaceLine(4, "}");
					continue;
				case "float":
					stringPlus.AppendSpaceLine(4, string.Concat(new string[]
					{
						"if(row[\"",
						columnName,
						"\"]!=null && row[\"",
						columnName,
						"\"].ToString()!=\"\")"
					}));
					stringPlus.AppendSpaceLine(4, "{");
					stringPlus.AppendSpaceLine(5, string.Concat(new string[]
					{
						"model.",
						columnName,
						"=float.Parse(row[\"",
						columnName,
						"\"].ToString());"
					}));
					stringPlus.AppendSpaceLine(4, "}");
					continue;
				case "DateTime":
					stringPlus.AppendSpaceLine(4, string.Concat(new string[]
					{
						"if(row[\"",
						columnName,
						"\"]!=null && row[\"",
						columnName,
						"\"].ToString()!=\"\")"
					}));
					stringPlus.AppendSpaceLine(4, "{");
					stringPlus.AppendSpaceLine(5, string.Concat(new string[]
					{
						"model.",
						columnName,
						"=DateTime.Parse(row[\"",
						columnName,
						"\"].ToString());"
					}));
					stringPlus.AppendSpaceLine(4, "}");
					continue;
				case "string":
					stringPlus.AppendSpaceLine(4, "if(row[\"" + columnName + "\"]!=null)");
					stringPlus.AppendSpaceLine(4, "{");
					stringPlus.AppendSpaceLine(5, string.Concat(new string[]
					{
						"model.",
						columnName,
						"=row[\"",
						columnName,
						"\"].ToString();"
					}));
					stringPlus.AppendSpaceLine(4, "}");
					continue;
				case "bool":
					stringPlus.AppendSpaceLine(4, string.Concat(new string[]
					{
						"if(row[\"",
						columnName,
						"\"]!=null && row[\"",
						columnName,
						"\"].ToString()!=\"\")"
					}));
					stringPlus.AppendSpaceLine(4, "{");
					stringPlus.AppendSpaceLine(5, string.Concat(new string[]
					{
						"if((row[\"",
						columnName,
						"\"].ToString()==\"1\")||(row[\"",
						columnName,
						"\"].ToString().ToLower()==\"true\"))"
					}));
					stringPlus.AppendSpaceLine(5, "{");
					stringPlus.AppendSpaceLine(6, "model." + columnName + "=true;");
					stringPlus.AppendSpaceLine(5, "}");
					stringPlus.AppendSpaceLine(5, "else");
					stringPlus.AppendSpaceLine(5, "{");
					stringPlus.AppendSpaceLine(6, "model." + columnName + "=false;");
					stringPlus.AppendSpaceLine(5, "}");
					stringPlus.AppendSpaceLine(4, "}");
					continue;
				case "byte[]":
					stringPlus.AppendSpaceLine(4, string.Concat(new string[]
					{
						"if(row[\"",
						columnName,
						"\"]!=null && row[\"",
						columnName,
						"\"].ToString()!=\"\")"
					}));
					stringPlus.AppendSpaceLine(4, "{");
					stringPlus.AppendSpaceLine(5, string.Concat(new string[]
					{
						"model.",
						columnName,
						"=(byte[])row[\"",
						columnName,
						"\"];"
					}));
					stringPlus.AppendSpaceLine(4, "}");
					continue;
				case "uniqueidentifier":
				case "Guid":
					stringPlus.AppendSpaceLine(4, string.Concat(new string[]
					{
						"if(row[\"",
						columnName,
						"\"]!=null && row[\"",
						columnName,
						"\"].ToString()!=\"\")"
					}));
					stringPlus.AppendSpaceLine(4, "{");
					stringPlus.AppendSpaceLine(5, string.Concat(new string[]
					{
						"model.",
						columnName,
						"= new Guid(row[\"",
						columnName,
						"\"].ToString());"
					}));
					stringPlus.AppendSpaceLine(4, "}");
					continue;
				}
				stringPlus.AppendSpaceLine(5, string.Concat(new string[]
				{
					"//model.",
					columnName,
					"=row[\"",
					columnName,
					"\"].ToString();"
				}));
			}
			stringPlus.AppendSpaceLine(3, "}");
			stringPlus.AppendSpaceLine(3, "return model;");
			stringPlus.AppendSpaceLine(2, "}");
			return stringPlus.ToString();
		}
		public string CreatGetList()
		{
			StringPlus stringPlus = new StringPlus();
			stringPlus.AppendSpaceLine(2, "/// <summary>");
			stringPlus.AppendSpaceLine(2, "/// " + this.Languagelist["summaryGetList"].ToString());
			stringPlus.AppendSpaceLine(2, "/// </summary>");
			stringPlus.AppendSpaceLine(2, "public DataSet GetList(string strWhere)");
			stringPlus.AppendSpaceLine(2, "{");
			stringPlus.AppendSpaceLine(3, "StringBuilder strSql=new StringBuilder();");
			stringPlus.AppendSpace(3, "strSql.Append(\"select ");
			stringPlus.AppendLine(this.Fieldstrlist + " \");");
			stringPlus.AppendSpaceLine(3, "strSql.Append(\" FROM " + this.TableName + " \");");
			stringPlus.AppendSpaceLine(3, "if(strWhere.Trim()!=\"\")");
			stringPlus.AppendSpaceLine(3, "{");
			stringPlus.AppendSpaceLine(4, "strSql.Append(\" where \"+strWhere);");
			stringPlus.AppendSpaceLine(3, "}");
			stringPlus.AppendSpaceLine(3, "return " + this.DbHelperName + ".Query(strSql.ToString());");
			stringPlus.AppendSpaceLine(2, "}");
			if (this.dbobj.DbType == "SQL2000" || this.dbobj.DbType == "SQL2005" || this.dbobj.DbType == "SQL2008" || this.dbobj.DbType == "SQL2012")
			{
				stringPlus.AppendLine();
				stringPlus.AppendSpaceLine(2, "/// <summary>");
				stringPlus.AppendSpaceLine(2, "/// " + this.Languagelist["summaryGetList2"].ToString());
				stringPlus.AppendSpaceLine(2, "/// </summary>");
				stringPlus.AppendSpaceLine(2, "public DataSet GetList(int Top,string strWhere,string filedOrder)");
				stringPlus.AppendSpaceLine(2, "{");
				stringPlus.AppendSpaceLine(3, "StringBuilder strSql=new StringBuilder();");
				stringPlus.AppendSpaceLine(3, "strSql.Append(\"select \");");
				stringPlus.AppendSpaceLine(3, "if(Top>0)");
				stringPlus.AppendSpaceLine(3, "{");
				stringPlus.AppendSpaceLine(4, "strSql.Append(\" top \"+Top.ToString());");
				stringPlus.AppendSpaceLine(3, "}");
				stringPlus.AppendSpaceLine(3, "strSql.Append(\" " + this.Fieldstrlist + " \");");
				stringPlus.AppendSpaceLine(3, "strSql.Append(\" FROM " + this.TableName + " \");");
				stringPlus.AppendSpaceLine(3, "if(strWhere.Trim()!=\"\")");
				stringPlus.AppendSpaceLine(3, "{");
				stringPlus.AppendSpaceLine(4, "strSql.Append(\" where \"+strWhere);");
				stringPlus.AppendSpaceLine(3, "}");
				stringPlus.AppendSpaceLine(3, "strSql.Append(\" order by \" + filedOrder);");
				stringPlus.AppendSpaceLine(3, "return " + this.DbHelperName + ".Query(strSql.ToString());");
				stringPlus.AppendSpaceLine(2, "}");
			}
			return stringPlus.Value;
		}
		public string CreatGetListByPage()
		{
			StringPlus stringPlus = new StringPlus();
			stringPlus.AppendSpaceLine(2, "/// <summary>");
			stringPlus.AppendSpaceLine(2, "/// " + this.Languagelist["GetRecordCount"].ToString());
			stringPlus.AppendSpaceLine(2, "/// </summary>");
			stringPlus.AppendSpaceLine(2, "public int GetRecordCount(string strWhere)");
			stringPlus.AppendSpaceLine(2, "{");
			stringPlus.AppendSpaceLine(3, "StringBuilder strSql=new StringBuilder();");
			stringPlus.AppendSpaceLine(3, "strSql.Append(\"select count(1) FROM " + this.TableName + " \");");
			stringPlus.AppendSpaceLine(3, "if(strWhere.Trim()!=\"\")");
			stringPlus.AppendSpaceLine(3, "{");
			stringPlus.AppendSpaceLine(4, "strSql.Append(\" where \"+strWhere);");
			stringPlus.AppendSpaceLine(3, "}");
			stringPlus.AppendSpaceLine(3, "object obj = DbHelperSQL.GetSingle(strSql.ToString());");
			stringPlus.AppendSpaceLine(3, "if (obj == null)");
			stringPlus.AppendSpaceLine(3, "{");
			stringPlus.AppendSpaceLine(4, "return 0;");
			stringPlus.AppendSpaceLine(3, "}");
			stringPlus.AppendSpaceLine(3, "else");
			stringPlus.AppendSpaceLine(3, "{");
			stringPlus.AppendSpaceLine(4, "return Convert.ToInt32(obj);");
			stringPlus.AppendSpaceLine(3, "}");
			stringPlus.AppendSpaceLine(2, "}");
			stringPlus.AppendSpaceLine(2, "/// <summary>");
			stringPlus.AppendSpaceLine(2, "/// " + this.Languagelist["summaryGetList3"].ToString());
			stringPlus.AppendSpaceLine(2, "/// </summary>");
			stringPlus.AppendSpaceLine(2, "public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)");
			stringPlus.AppendSpaceLine(2, "{");
			stringPlus.AppendSpaceLine(3, "StringBuilder strSql=new StringBuilder();");
			stringPlus.AppendSpaceLine(3, "strSql.Append(\"SELECT * FROM ( \");");
			stringPlus.AppendSpaceLine(3, "strSql.Append(\" SELECT ROW_NUMBER() OVER (\");");
			stringPlus.AppendSpaceLine(3, "if (!string.IsNullOrEmpty(orderby.Trim()))");
			stringPlus.AppendSpaceLine(3, "{");
			stringPlus.AppendSpaceLine(4, "strSql.Append(\"order by T.\" + orderby );");
			stringPlus.AppendSpaceLine(3, "}");
			stringPlus.AppendSpaceLine(3, "else");
			stringPlus.AppendSpaceLine(3, "{");
			stringPlus.AppendSpaceLine(4, "strSql.Append(\"order by T." + this._IdentityKey + " desc\");");
			stringPlus.AppendSpaceLine(3, "}");
			stringPlus.AppendSpaceLine(3, "strSql.Append(\")AS Row, T.*  from " + this.TableName + " T \");");
			stringPlus.AppendSpaceLine(3, "if (!string.IsNullOrEmpty(strWhere.Trim()))");
			stringPlus.AppendSpaceLine(3, "{");
			stringPlus.AppendSpaceLine(4, "strSql.Append(\" WHERE \" + strWhere);");
			stringPlus.AppendSpaceLine(3, "}");
			stringPlus.AppendSpaceLine(3, "strSql.Append(\" ) TT\");");
			stringPlus.AppendSpaceLine(3, "strSql.AppendFormat(\" WHERE TT.Row between {0} and {1}\", startIndex, endIndex);");
			stringPlus.AppendSpaceLine(3, "return " + this.DbHelperName + ".Query(strSql.ToString());");
			stringPlus.AppendSpaceLine(2, "}");
			return stringPlus.Value;
		}
		public string CreatGetListByPageProc()
		{
			StringPlus stringPlus = new StringPlus();
			stringPlus.AppendSpaceLine(2, "/*");
			stringPlus.AppendSpaceLine(2, "/// <summary>");
			stringPlus.AppendSpaceLine(2, "/// " + this.Languagelist["summaryGetList3"].ToString());
			stringPlus.AppendSpaceLine(2, "/// </summary>");
			stringPlus.AppendSpaceLine(2, "public DataSet GetList(int PageSize,int PageIndex,string strWhere)");
			stringPlus.AppendSpaceLine(2, "{");
			stringPlus.AppendSpaceLine(3, this.DbParaHead + "Parameter[] parameters = {");
			stringPlus.AppendSpaceLine(5, string.Concat(new string[]
			{
				"new ",
				this.DbParaHead,
				"Parameter(\"",
				this.preParameter,
				"tblName\", ",
				this.DbParaDbType,
				".VarChar, 255),"
			}));
			stringPlus.AppendSpaceLine(5, string.Concat(new string[]
			{
				"new ",
				this.DbParaHead,
				"Parameter(\"",
				this.preParameter,
				"fldName\", ",
				this.DbParaDbType,
				".VarChar, 255),"
			}));
			stringPlus.AppendSpaceLine(5, string.Concat(new string[]
			{
				"new ",
				this.DbParaHead,
				"Parameter(\"",
				this.preParameter,
				"PageSize\", ",
				this.DbParaDbType,
				".",
				CodeCommon.CSToProcType(this.dbobj.DbType, "int"),
				"),"
			}));
			stringPlus.AppendSpaceLine(5, string.Concat(new string[]
			{
				"new ",
				this.DbParaHead,
				"Parameter(\"",
				this.preParameter,
				"PageIndex\", ",
				this.DbParaDbType,
				".",
				CodeCommon.CSToProcType(this.dbobj.DbType, "int"),
				"),"
			}));
			stringPlus.AppendSpaceLine(5, string.Concat(new string[]
			{
				"new ",
				this.DbParaHead,
				"Parameter(\"",
				this.preParameter,
				"IsReCount\", ",
				this.DbParaDbType,
				".",
				CodeCommon.CSToProcType(this.dbobj.DbType, "bit"),
				"),"
			}));
			stringPlus.AppendSpaceLine(5, string.Concat(new string[]
			{
				"new ",
				this.DbParaHead,
				"Parameter(\"",
				this.preParameter,
				"OrderType\", ",
				this.DbParaDbType,
				".",
				CodeCommon.CSToProcType(this.dbobj.DbType, "bit"),
				"),"
			}));
			stringPlus.AppendSpaceLine(5, string.Concat(new string[]
			{
				"new ",
				this.DbParaHead,
				"Parameter(\"",
				this.preParameter,
				"strWhere\", ",
				this.DbParaDbType,
				".VarChar,1000),"
			}));
			stringPlus.AppendSpaceLine(5, "};");
			stringPlus.AppendSpaceLine(3, "parameters[0].Value = \"" + this.TableName + "\";");
			stringPlus.AppendSpaceLine(3, "parameters[1].Value = \"" + this._IdentityKey + "\";");
			stringPlus.AppendSpaceLine(3, "parameters[2].Value = PageSize;");
			stringPlus.AppendSpaceLine(3, "parameters[3].Value = PageIndex;");
			stringPlus.AppendSpaceLine(3, "parameters[4].Value = 0;");
			stringPlus.AppendSpaceLine(3, "parameters[5].Value = 0;");
			stringPlus.AppendSpaceLine(3, "parameters[6].Value = strWhere;\t");
			stringPlus.AppendSpaceLine(3, "return " + this.DbHelperName + ".RunProcedure(\"UP_GetRecordByPage\",parameters,\"ds\");");
			stringPlus.AppendSpaceLine(2, "}*/");
			return stringPlus.Value;
		}
	}
}
