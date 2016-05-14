using Maticsoft.CodeHelper;
using Maticsoft.IBuilder;
using Maticsoft.IDBO;
using Maticsoft.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
namespace Maticsoft.BuilderDALTranParam
{
	public class BuilderDAL : IBuilderDALTran
	{
        protected Dictionary<string, int> dtCollection;
		protected string _key = "ID";
		protected string _keyType = "int";
		private IDbObject dbobj;
		private string _dbname;
		private string _tablenameparent;
		private string _tablenameson;
		private List<ColumnInfo> _fieldlistparent;
		private List<ColumnInfo> _keysparent;
		private List<ColumnInfo> _fieldlistson;
		private List<ColumnInfo> _keysson;
		private string _namespace;
		private string _folder;
		private string _modelpath;
		private string _modelnameparent;
		private string _modelnameson;
		private string _dalpath;
		private string _dalnameparent;
		private string _dalnameson;
		private string _idalpath;
		private string _iclass;
		private string _dbhelperName;
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
		public string TableNameParent
		{
			get
			{
				return this._tablenameparent;
			}
			set
			{
				this._tablenameparent = value;
			}
		}
		public string TableNameSon
		{
			get
			{
				return this._tablenameson;
			}
			set
			{
				this._tablenameson = value;
			}
		}
		public List<ColumnInfo> FieldlistParent
		{
			get
			{
				return this._fieldlistparent;
			}
			set
			{
				this._fieldlistparent = value;
			}
		}
		public List<ColumnInfo> FieldlistSon
		{
			get
			{
				return this._fieldlistson;
			}
			set
			{
				this._fieldlistson = value;
			}
		}
		public List<ColumnInfo> KeysParent
		{
			get
			{
				return this._keysparent;
			}
			set
			{
				this._keysparent = value;
				foreach (ColumnInfo current in this._keysparent)
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
		}
		public List<ColumnInfo> KeysSon
		{
			get
			{
				return this._keysson;
			}
			set
			{
				this._keysson = value;
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
		public string ModelNameParent
		{
			get
			{
				return this._modelnameparent;
			}
			set
			{
				this._modelnameparent = value;
			}
		}
		public string ModelNameSon
		{
			get
			{
				return this._modelnameson;
			}
			set
			{
				this._modelnameson = value;
			}
		}
		public string ModelSpaceParent
		{
			get
			{
				return this.Modelpath + "." + this.ModelNameParent;
			}
		}
		public string ModelSpaceSon
		{
			get
			{
				return this.Modelpath + "." + this.ModelNameSon;
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
		public string DALNameParent
		{
			get
			{
				return this._dalnameparent;
			}
			set
			{
				this._dalnameparent = value;
			}
		}
		public string DALNameSon
		{
			get
			{
				return this._dalnameson;
			}
			set
			{
				this._dalnameson = value;
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
		public Hashtable Languagelist
		{
			get
			{
				return Language.LoadFromCfg("BuilderDALTranParam.lan");
			}
		}
		public string Fieldstrlist
		{
			get
			{
				StringPlus stringPlus = new StringPlus();
				foreach (ColumnInfo current in this.FieldlistParent)
				{
					stringPlus.Append(current.ColumnName + ",");
				}
				stringPlus.DelLastComma();
				return stringPlus.Value;
			}
		}
		public string FieldstrlistSon
		{
			get
			{
				StringPlus stringPlus = new StringPlus();
				foreach (ColumnInfo current in this.FieldlistSon)
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
				return CodeCommon.IsHasIdentity(this._keysparent);
			}
		}
		public BuilderDAL()
		{
		}
		public BuilderDAL(IDbObject idbobj)
		{
			this.dbobj = idbobj;
		}
		public BuilderDAL(IDbObject idbobj, string dbname, string tablename, string modelname, List<ColumnInfo> fieldlist, List<ColumnInfo> keys, string namepace, string folder, string dbherlpername, string modelpath, string dalpath, string idalpath, string iclass)
		{
			this.dbobj = idbobj;
			this._dbname = dbname;
			this._tablenameparent = tablename;
			this._modelnameparent = modelname;
			this._namespace = namepace;
			this._folder = folder;
			this._dbhelperName = dbherlpername;
			this._modelpath = modelpath;
			this._dalpath = dalpath;
			this._idalpath = idalpath;
			this._iclass = iclass;
			this.FieldlistParent = fieldlist;
			this.KeysParent = keys;
			foreach (ColumnInfo current in this._keysparent)
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
		public string GetPreParameter(List<ColumnInfo> keys, string numPara)
		{
			StringPlus stringPlus = new StringPlus();
			StringPlus stringPlus2 = new StringPlus();
			stringPlus.AppendSpaceLine(3, this.DbParaHead + "Parameter[] parameters" + numPara + " = {");
			int num = 0;
			foreach (ColumnInfo current in keys)
			{
				stringPlus.AppendSpaceLine(5, string.Concat(new string[]
				{
					"new ",
					this.DbParaHead,
					"Parameter(\"",
					this.preParameter,
					current.ColumnName,
					"\", ",
					this.DbParaDbType,
					".",
					CodeCommon.DbTypeLength(this.dbobj.DbType, current.TypeName, ""),
					"),"
				}));
				stringPlus2.AppendSpaceLine(3, string.Concat(new string[]
				{
					"parameters",
					numPara,
					"[",
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
		public string GetDALCode(bool Maxid, bool Exists, bool Add, bool Update, bool Delete, bool GetModel, bool List)
		{
			StringPlus stringPlus = new StringPlus();
			stringPlus.AppendLine("using System;");
			stringPlus.AppendLine("using System.Data;");
			stringPlus.AppendLine("using System.Text;");
			stringPlus.AppendLine("using System.Collections.Generic;");
			string dbType;
			switch (dbType = this.dbobj.DbType)
			{
			case "SQL2005":
			case "SQL2008":
			case "SQL2012":
				stringPlus.AppendLine("using System.Data.SqlClient;");
				break;
			case "SQL2000":
				stringPlus.AppendLine("using System.Data.SqlClient;");
				break;
			case "Oracle":
				stringPlus.AppendLine("using System.Data.OracleClient;");
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
			stringPlus.AppendSpaceLine(1, "/// " + this.Languagelist["summary"].ToString() + ":" + this.DALNameParent);
			stringPlus.AppendSpaceLine(1, "/// </summary>");
			stringPlus.AppendSpace(1, "public partial class " + this.DALNameParent);
			if (this.IClass != "")
			{
				stringPlus.Append(":" + this.IClass);
			}
			stringPlus.AppendLine("");
			stringPlus.AppendSpaceLine(1, "{");
			stringPlus.AppendSpaceLine(2, "public " + this.DALNameParent + "()");
			stringPlus.AppendSpaceLine(2, "{}");
			stringPlus.AppendSpaceLine(2, "#region  Method");
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
				stringPlus.AppendLine(this.CreatGetListByPageProc());
			}
			stringPlus.AppendSpaceLine(2, "#endregion  Method");
			stringPlus.AppendSpaceLine(1, "}");
			stringPlus.AppendLine("}");
			stringPlus.AppendLine("");
			return stringPlus.ToString();
		}
		public string CreatGetMaxID()
		{
			StringPlus stringPlus = new StringPlus();
			if (this._keysparent.Count > 0)
			{
				foreach (ColumnInfo current in this._keysparent)
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
								this._tablenameparent,
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
			if (this._keysparent.Count > 0)
			{
				string inParameter = CodeCommon.GetInParameter(this._keysparent, false);
				if (!string.IsNullOrEmpty(inParameter))
				{
					stringPlus.AppendSpaceLine(2, "/// <summary>");
					stringPlus.AppendSpaceLine(2, "/// " + this.Languagelist["summaryExists"].ToString());
					stringPlus.AppendSpaceLine(2, "/// </summary>");
					stringPlus.AppendSpaceLine(2, "public bool Exists(" + inParameter + ")");
					stringPlus.AppendSpaceLine(2, "{");
					stringPlus.AppendSpaceLine(3, "StringBuilder strSql=new StringBuilder();");
					stringPlus.AppendSpaceLine(3, "strSql.Append(\"select count(1) from " + this._tablenameparent + "\");");
					stringPlus.AppendSpaceLine(3, "strSql.Append(\" where " + CodeCommon.GetWhereParameterExpression(this.KeysParent, false, this.dbobj.DbType) + "\");");
					stringPlus.AppendLine(CodeCommon.GetPreParameter(this.KeysParent, false, this.dbobj.DbType));
					stringPlus.AppendSpaceLine(3, "return " + this.DbHelperName + ".Exists(strSql.ToString(),parameters);");
					stringPlus.AppendSpaceLine(2, "}");
				}
			}
			return stringPlus.Value;
		}
		public string CreatAdd()
		{
			StringPlus stringPlus = new StringPlus();
			StringPlus stringPlus2 = new StringPlus();
			StringPlus stringPlus3 = new StringPlus();
			StringPlus stringPlus4 = new StringPlus();
			StringPlus stringPlus5 = new StringPlus();
			stringPlus.AppendLine();
			stringPlus.AppendSpaceLine(2, "/// <summary>");
			stringPlus.AppendSpaceLine(2, "/// 增加一条数据,及其子表数据");
			stringPlus.AppendSpaceLine(2, "/// </summary>");
			string text = "void";
			if ((this.dbobj.DbType == "SQL2000" || this.dbobj.DbType == "SQL2005" || this.dbobj.DbType == "SQL2008" || this.dbobj.DbType == "SQL2012") && this.IsHasIdentity)
			{
				text = "int";
			}
			string text2 = string.Concat(new string[]
			{
				CodeCommon.Space(2),
				"public ",
				text,
				" Add(",
				this.ModelSpaceParent,
				" model)"
			});
			stringPlus.AppendLine(text2);
			stringPlus.AppendSpaceLine(2, "{");
			stringPlus.AppendSpaceLine(3, "StringBuilder strSql=new StringBuilder();");
			stringPlus.AppendSpaceLine(3, "strSql.Append(\"insert into " + this._tablenameparent + "(\");");
			stringPlus2.AppendSpace(3, "strSql.Append(\"");
			int num = 0;
			int num2 = 0;
			foreach (ColumnInfo current in this.FieldlistParent)
			{
				string columnName = current.ColumnName;
				string typeName = current.TypeName;
				bool arg_18C_0 = current.IsIdentity;
				string length = current.Length;
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
					stringPlus5.AppendSpaceLine(3, string.Concat(new object[]
					{
						"parameters[",
						num,
						"].Value = model.",
						columnName,
						";"
					}));
					num++;
				}
			}
			if ((this.dbobj.DbType == "SQL2000" || this.dbobj.DbType == "SQL2005" || this.dbobj.DbType == "SQL2008" || this.dbobj.DbType == "SQL2012") && this.IsHasIdentity)
			{
				num2 = num;
				stringPlus4.AppendSpaceLine(5, "new SqlParameter(\"@ReturnValue\",SqlDbType.Int),");
				stringPlus5.AppendSpaceLine(3, "parameters[" + num2.ToString() + "].Direction = ParameterDirection.Output;");
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
				stringPlus.AppendSpaceLine(3, "strSql.Append(\";set @ReturnValue= @@IDENTITY\");");
			}
			stringPlus.AppendSpaceLine(3, this.DbParaHead + "Parameter[] parameters = {");
			stringPlus.Append(stringPlus4.Value);
			stringPlus.AppendLine("};");
			stringPlus.AppendLine(stringPlus5.Value);
			stringPlus.AppendSpaceLine(3, "List<CommandInfo> sqllist = new List<CommandInfo>();");
			stringPlus.AppendSpaceLine(3, "CommandInfo cmd = new CommandInfo(strSql.ToString(), parameters);");
			stringPlus.AppendSpaceLine(3, "sqllist.Add(cmd);");
			stringPlus.AppendSpaceLine(3, "StringBuilder strSql2;");
			stringPlus.AppendSpaceLine(3, string.Concat(new string[]
			{
				"foreach (",
				this.ModelSpaceSon,
				" models in model.",
				this.ModelNameSon,
				"s)"
			}));
			stringPlus.AppendSpaceLine(3, "{");
			StringPlus stringPlus6 = new StringPlus();
			StringPlus stringPlus7 = new StringPlus();
			StringPlus stringPlus8 = new StringPlus();
			StringPlus stringPlus9 = new StringPlus();
			stringPlus.AppendSpaceLine(4, "strSql2=new StringBuilder();");
			stringPlus.AppendSpaceLine(4, "strSql2.Append(\"insert into " + this._tablenameson + "(\");");
			stringPlus6.AppendSpace(4, "strSql2.Append(\"");
			int num3 = 0;
			foreach (ColumnInfo current2 in this.FieldlistSon)
			{
				string columnName2 = current2.ColumnName;
				string typeName2 = current2.TypeName;
				bool arg_561_0 = current2.IsIdentity;
				string length2 = current2.Length;
				if (!current2.IsIdentity)
				{
					stringPlus8.AppendSpaceLine(6, string.Concat(new string[]
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
					stringPlus6.Append(columnName2 + ",");
					stringPlus7.Append(this.preParameter + columnName2 + ",");
					stringPlus9.AppendSpaceLine(4, string.Concat(new object[]
					{
						"parameters2[",
						num3,
						"].Value = models.",
						columnName2,
						";"
					}));
					num3++;
				}
			}
			stringPlus6.DelLastComma();
			stringPlus7.DelLastComma();
			stringPlus8.DelLastComma();
			stringPlus6.AppendLine(")\");");
			stringPlus.Append(stringPlus6.ToString());
			stringPlus.AppendSpaceLine(4, "strSql2.Append(\" values (\");");
			stringPlus.AppendSpaceLine(4, "strSql2.Append(\"" + stringPlus7.ToString() + ")\");");
			stringPlus.AppendSpaceLine(4, this.DbParaHead + "Parameter[] parameters2 = {");
			stringPlus.Append(stringPlus8.Value);
			stringPlus.AppendLine("};");
			stringPlus.AppendLine(stringPlus9.Value);
			stringPlus.AppendSpaceLine(4, "cmd = new CommandInfo(strSql2.ToString(), parameters2);");
			stringPlus.AppendSpaceLine(4, "sqllist.Add(cmd);");
			stringPlus.AppendSpaceLine(3, "}");
			if ((this.dbobj.DbType == "SQL2000" || this.dbobj.DbType == "SQL2005" || this.dbobj.DbType == "SQL2008" || this.dbobj.DbType == "SQL2012") && this.IsHasIdentity)
			{
				stringPlus.AppendSpaceLine(3, this.DbHelperName + ".ExecuteSqlTranWithIndentity(sqllist);");
				stringPlus.AppendSpaceLine(3, string.Concat(new object[]
				{
					"return (",
					this._keyType,
					")parameters[",
					num2,
					"].Value;"
				}));
			}
			else
			{
				stringPlus.AppendSpaceLine(3, this.DbHelperName + ".ExecuteSqlTran(sqllist);");
			}
			stringPlus.AppendSpace(2, "}");
			return stringPlus.ToString();
		}
		public string CreatUpdate()
		{
			StringPlus stringPlus = new StringPlus();
			StringPlus stringPlus2 = new StringPlus();
			StringPlus stringPlus3 = new StringPlus();
			stringPlus.AppendSpaceLine(2, "/// <summary>");
			stringPlus.AppendSpaceLine(2, "/// " + this.Languagelist["summaryUpdate"].ToString());
			stringPlus.AppendSpaceLine(2, "/// </summary>");
			stringPlus.AppendSpaceLine(2, "public bool Update(" + this.ModelSpaceParent + " model)");
			stringPlus.AppendSpaceLine(2, "{");
			stringPlus.AppendSpaceLine(3, "StringBuilder strSql=new StringBuilder();");
			stringPlus.AppendSpaceLine(3, "strSql.Append(\"update " + this._tablenameparent + " set \");");
			int num = 0;
			if (this.FieldlistParent.Count == 0)
			{
				this.FieldlistParent = this.KeysParent;
			}
			foreach (ColumnInfo current in this.FieldlistParent)
			{
				string columnName = current.ColumnName;
				string typeName = current.TypeName;
				string length = current.Length;
				bool arg_FF_0 = current.IsIdentity;
				bool arg_107_0 = current.IsPrimaryKey;
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
				if (!current.IsIdentity && !current.IsPrimaryKey && !this.KeysParent.Contains(current))
				{
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
			stringPlus.DelLastComma();
			stringPlus.AppendLine("\");");
			stringPlus.AppendSpaceLine(3, "strSql.Append(\" where " + CodeCommon.GetWhereParameterExpression(this.KeysParent, true, this.dbobj.DbType) + "\");");
			stringPlus.AppendSpaceLine(3, this.DbParaHead + "Parameter[] parameters = {");
			stringPlus2.DelLastComma();
			stringPlus.Append(stringPlus2.Value);
			stringPlus.AppendLine("};");
			stringPlus.AppendLine(stringPlus3.Value);
			stringPlus.AppendSpaceLine(3, "int rowsAffected=" + this.DbHelperName + ".ExecuteSql(strSql.ToString(),parameters);");
			stringPlus.AppendSpaceLine(3, "if (rowsAffected > 0)");
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
			stringPlus.AppendSpaceLine(2, "/// 删除一条数据，及子表所有相关数据");
			stringPlus.AppendSpaceLine(2, "/// </summary>");
			stringPlus.AppendSpaceLine(2, "public bool Delete(" + CodeCommon.GetInParameter(this.KeysParent, true) + ")");
			stringPlus.AppendSpaceLine(2, "{");
			stringPlus.AppendSpaceLine(3, "List<CommandInfo> sqllist = new List<CommandInfo>();");
			stringPlus.AppendSpaceLine(3, "StringBuilder strSql2=new StringBuilder();");
			if (this.dbobj.DbType != "OleDb")
			{
				stringPlus.AppendSpaceLine(3, "strSql2.Append(\"delete " + this._tablenameson + " \");");
			}
			else
			{
				stringPlus.AppendSpaceLine(3, "strSql2.Append(\"delete from " + this._tablenameson + " \");");
			}
			stringPlus.AppendSpaceLine(3, "strSql2.Append(\" where " + CodeCommon.GetWhereParameterExpression(this.KeysSon, true, this.dbobj.DbType) + "\");");
			stringPlus.AppendLine(this.GetPreParameter(this.KeysSon, "2"));
			stringPlus.AppendSpaceLine(3, "CommandInfo cmd = new CommandInfo(strSql2.ToString(), parameters2);");
			stringPlus.AppendSpaceLine(3, "sqllist.Add(cmd);");
			stringPlus.AppendSpaceLine(3, "StringBuilder strSql=new StringBuilder();");
			if (this.dbobj.DbType != "OleDb")
			{
				stringPlus.AppendSpaceLine(3, "strSql.Append(\"delete " + this._tablenameparent + " \");");
			}
			else
			{
				stringPlus.AppendSpaceLine(3, "strSql.Append(\"delete from " + this._tablenameparent + " \");");
			}
			stringPlus.AppendSpaceLine(3, "strSql.Append(\" where " + CodeCommon.GetWhereParameterExpression(this.KeysParent, true, this.dbobj.DbType) + "\");");
			stringPlus.AppendLine(CodeCommon.GetPreParameter(this.KeysParent, true, this.dbobj.DbType));
			stringPlus.AppendSpaceLine(3, "cmd = new CommandInfo(strSql.ToString(), parameters);");
			stringPlus.AppendSpaceLine(3, "sqllist.Add(cmd);");
			stringPlus.AppendSpaceLine(3, "int rowsAffected=" + this.DbHelperName + ".ExecuteSqlTran(sqllist);");
			stringPlus.AppendSpaceLine(3, "if (rowsAffected > 0)");
			stringPlus.AppendSpaceLine(3, "{");
			stringPlus.AppendSpaceLine(4, "return true;");
			stringPlus.AppendSpaceLine(3, "}");
			stringPlus.AppendSpaceLine(3, "else");
			stringPlus.AppendSpaceLine(3, "{");
			stringPlus.AppendSpaceLine(4, "return false;");
			stringPlus.AppendSpaceLine(3, "}");
			stringPlus.AppendSpaceLine(2, "}");
			string text = "";
			if (this.KeysParent.Count == 1)
			{
				text = this.KeysParent[0].ColumnName;
			}
			else
			{
				foreach (ColumnInfo current in this.KeysParent)
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
				stringPlus.AppendSpaceLine(2, "/// " + this.Languagelist["summaryDelete"].ToString());
				stringPlus.AppendSpaceLine(2, "/// </summary>");
				stringPlus.AppendSpaceLine(2, "public bool DeleteList(string " + text + "list )");
				stringPlus.AppendSpaceLine(2, "{");
				stringPlus.AppendSpaceLine(3, "List<string> sqllist = new List<string>();");
				stringPlus.AppendSpaceLine(3, "StringBuilder strSql2=new StringBuilder();");
				stringPlus.AppendSpaceLine(3, "strSql2.Append(\"delete from " + this._tablenameson + " \");");
				stringPlus.AppendSpaceLine(3, string.Concat(new string[]
				{
					"strSql2.Append(\" where ",
					this.KeysSon[0].ColumnName,
					" in (\"+",
					text,
					"list + \")  \");"
				}));
				stringPlus.AppendSpaceLine(3, "sqllist.Add(strSql2.ToString());");
				stringPlus.AppendSpaceLine(3, "StringBuilder strSql=new StringBuilder();");
				stringPlus.AppendSpaceLine(3, "strSql.Append(\"delete from " + this._tablenameparent + " \");");
				stringPlus.AppendSpaceLine(3, string.Concat(new string[]
				{
					"strSql.Append(\" where ",
					text,
					" in (\"+",
					text,
					"list + \")  \");"
				}));
				stringPlus.AppendSpaceLine(3, "sqllist.Add(strSql.ToString());");
				stringPlus.AppendSpaceLine(3, "int rowsAffected=" + this.DbHelperName + ".ExecuteSqlTran(sqllist);");
				stringPlus.AppendSpaceLine(3, "if (rowsAffected > 0)");
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
			StringPlus stringPlus = new StringPlus();
			stringPlus.AppendLine();
			stringPlus.AppendSpaceLine(2, "/// <summary>");
			stringPlus.AppendSpaceLine(2, "/// " + this.Languagelist["summaryGetModel"].ToString());
			stringPlus.AppendSpaceLine(2, "/// </summary>");
			stringPlus.AppendSpaceLine(2, string.Concat(new string[]
			{
				"public ",
				this.ModelSpaceParent,
				" GetModel(",
				CodeCommon.GetInParameter(this.KeysParent, true),
				")"
			}));
			stringPlus.AppendSpaceLine(2, "{");
			stringPlus.AppendSpaceLine(3, "StringBuilder strSql=new StringBuilder();");
			stringPlus.AppendSpaceLine(3, string.Concat(new string[]
			{
				"strSql.Append(\"select ",
				this.Fieldstrlist,
				" from ",
				this._tablenameparent,
				" \");"
			}));
			stringPlus.AppendSpaceLine(3, "strSql.Append(\" where " + CodeCommon.GetWhereParameterExpression(this.KeysParent, true, this.dbobj.DbType) + "\");");
			stringPlus.AppendLine(CodeCommon.GetPreParameter(this.KeysParent, true, this.dbobj.DbType));
			stringPlus.AppendSpaceLine(3, this.ModelSpaceParent + " model=new " + this.ModelSpaceParent + "();");
			stringPlus.AppendSpaceLine(3, "DataSet ds=" + this.DbHelperName + ".Query(strSql.ToString(),parameters);");
			stringPlus.AppendSpaceLine(3, "if(ds.Tables[0].Rows.Count>0)");
			stringPlus.AppendSpaceLine(3, "{");
			stringPlus.AppendSpaceLine(4, "#region  父表信息");
			foreach (ColumnInfo current in this.FieldlistParent)
			{
				string columnName = current.ColumnName;
				string typeName = current.TypeName;
				stringPlus.AppendSpaceLine(4, string.Concat(new string[]
				{
					"if(ds.Tables[0].Rows[0][\"",
					columnName,
					"\"]!=null && ds.Tables[0].Rows[0][\"",
					columnName,
					"\"].ToString()!=\"\")"
				}));
				stringPlus.AppendSpaceLine(4, "{");
				string key;
				if ((key = CodeCommon.DbTypeToCS(typeName)) == null)
				{
					goto IL_5A8;
				}
                if (dtCollection == null)
				{
                    dtCollection = new Dictionary<string, int>(9)
					{

						{
							"int",
							0
						},

						{
							"long",
							1
						},

						{
							"decimal",
							2
						},

						{
							"float",
							3
						},

						{
							"DateTime",
							4
						},

						{
							"string",
							5
						},

						{
							"bool",
							6
						},

						{
							"byte[]",
							7
						},

						{
							"Guid",
							8
						}
					};
				}
				int num;
                if (!dtCollection.TryGetValue(key, out num))
				{
					goto IL_5A8;
				}
				switch (num)
				{
				case 0:
					stringPlus.AppendSpaceLine(5, string.Concat(new string[]
					{
						"model.",
						columnName,
						"=int.Parse(ds.Tables[0].Rows[0][\"",
						columnName,
						"\"].ToString());"
					}));
					break;
				case 1:
					stringPlus.AppendSpaceLine(5, string.Concat(new string[]
					{
						"model.",
						columnName,
						"=long.Parse(ds.Tables[0].Rows[0][\"",
						columnName,
						"\"].ToString());"
					}));
					break;
				case 2:
					stringPlus.AppendSpaceLine(5, string.Concat(new string[]
					{
						"model.",
						columnName,
						"=decimal.Parse(ds.Tables[0].Rows[0][\"",
						columnName,
						"\"].ToString());"
					}));
					break;
				case 3:
					stringPlus.AppendSpaceLine(5, string.Concat(new string[]
					{
						"model.",
						columnName,
						"=float.Parse(ds.Tables[0].Rows[0][\"",
						columnName,
						"\"].ToString());"
					}));
					break;
				case 4:
					stringPlus.AppendSpaceLine(5, string.Concat(new string[]
					{
						"model.",
						columnName,
						"=DateTime.Parse(ds.Tables[0].Rows[0][\"",
						columnName,
						"\"].ToString());"
					}));
					break;
				case 5:
					stringPlus.AppendSpaceLine(5, string.Concat(new string[]
					{
						"model.",
						columnName,
						"=ds.Tables[0].Rows[0][\"",
						columnName,
						"\"].ToString();"
					}));
					break;
				case 6:
					stringPlus.AppendSpaceLine(5, string.Concat(new string[]
					{
						"if((ds.Tables[0].Rows[0][\"",
						columnName,
						"\"].ToString()==\"1\")||(ds.Tables[0].Rows[0][\"",
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
					break;
				case 7:
					stringPlus.AppendSpaceLine(5, string.Concat(new string[]
					{
						"model.",
						columnName,
						"=(byte[])ds.Tables[0].Rows[0][\"",
						columnName,
						"\"];"
					}));
					break;
				case 8:
					stringPlus.AppendSpaceLine(5, string.Concat(new string[]
					{
						"model.",
						columnName,
						"=new Guid(ds.Tables[0].Rows[0][\"",
						columnName,
						"\"].ToString());"
					}));
					break;
				default:
					goto IL_5A8;
				}
				IL_5E4:
				stringPlus.AppendSpaceLine(4, "}");
				continue;
				IL_5A8:
				stringPlus.AppendSpaceLine(5, string.Concat(new string[]
				{
					"//model.",
					columnName,
					"=ds.Tables[0].Rows[0][\"",
					columnName,
					"\"].ToString();"
				}));
				goto IL_5E4;
			}
			stringPlus.AppendSpaceLine(4, "#endregion  父表信息end");
			stringPlus.AppendLine();
			stringPlus.AppendSpaceLine(4, "#region  子表信息");
			stringPlus.AppendSpaceLine(4, "StringBuilder strSql2=new StringBuilder();");
			stringPlus.AppendSpaceLine(4, string.Concat(new string[]
			{
				"strSql2.Append(\"select ",
				this.FieldstrlistSon,
				" from ",
				this._tablenameson,
				" \");"
			}));
			stringPlus.AppendSpaceLine(4, "strSql2.Append(\" where " + CodeCommon.GetWhereParameterExpression(this.KeysSon, true, this.dbobj.DbType) + "\");");
			stringPlus.AppendLine(this.GetPreParameter(this.KeysParent, "2"));
			stringPlus.AppendSpaceLine(4, "DataSet ds2=" + this.DbHelperName + ".Query(strSql2.ToString(),parameters2);");
			stringPlus.AppendSpaceLine(4, "if(ds2.Tables[0].Rows.Count>0)");
			stringPlus.AppendSpaceLine(4, "{");
			stringPlus.AppendSpaceLine(5, "#region  子表字段信息");
			stringPlus.AppendSpaceLine(5, "int i = ds2.Tables[0].Rows.Count;");
			stringPlus.AppendSpaceLine(5, string.Concat(new string[]
			{
				"List<",
				this.ModelSpaceSon,
				"> models = new List<",
				this.ModelSpaceSon,
				">();"
			}));
			stringPlus.AppendSpaceLine(5, this.ModelSpaceSon + " modelt;");
			stringPlus.AppendSpaceLine(5, "for (int n = 0; n < i; n++)");
			stringPlus.AppendSpaceLine(5, "{");
			stringPlus.AppendSpaceLine(6, "modelt = new " + this.ModelSpaceSon + "();");
			foreach (ColumnInfo current2 in this.FieldlistSon)
			{
				string columnName2 = current2.ColumnName;
				string typeName2 = current2.TypeName;
				stringPlus.AppendSpaceLine(6, string.Concat(new string[]
				{
					"if(ds2.Tables[0].Rows[n][\"",
					columnName2,
					"\"]!=null && ds2.Tables[0].Rows[n][\"",
					columnName2,
					"\"].ToString()!=\"\")"
				}));
				stringPlus.AppendSpaceLine(6, "{");
				string key2;
				if ((key2 = CodeCommon.DbTypeToCS(typeName2)) == null)
				{
					goto IL_B1D;
				}
				if (dtCollection == null)
				{
					dtCollection = new Dictionary<string, int>(7)
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
						},

						{
							"Guid",
							6
						}
					};
				}
				int num2;
				if (!dtCollection.TryGetValue(key2, out num2))
				{
					goto IL_B1D;
				}
				switch (num2)
				{
				case 0:
					stringPlus.AppendSpaceLine(7, string.Concat(new string[]
					{
						"modelt.",
						columnName2,
						"=int.Parse(ds2.Tables[0].Rows[n][\"",
						columnName2,
						"\"].ToString());"
					}));
					break;
				case 1:
					stringPlus.AppendSpaceLine(7, string.Concat(new string[]
					{
						"modelt.",
						columnName2,
						"=decimal.Parse(ds2.Tables[0].Rows[n][\"",
						columnName2,
						"\"].ToString());"
					}));
					break;
				case 2:
					stringPlus.AppendSpaceLine(7, string.Concat(new string[]
					{
						"modelt.",
						columnName2,
						"=DateTime.Parse(ds2.Tables[0].Rows[n][\"",
						columnName2,
						"\"].ToString());"
					}));
					break;
				case 3:
					stringPlus.AppendSpaceLine(7, string.Concat(new string[]
					{
						"modelt.",
						columnName2,
						"=ds2.Tables[0].Rows[n][\"",
						columnName2,
						"\"].ToString();"
					}));
					break;
				case 4:
					stringPlus.AppendSpaceLine(7, string.Concat(new string[]
					{
						"if((ds2.Tables[0].Rows[n][\"",
						columnName2,
						"\"].ToString()==\"1\")||(ds2.Tables[0].Rows[n][\"",
						columnName2,
						"\"].ToString().ToLower()==\"true\"))"
					}));
					stringPlus.AppendSpaceLine(7, "{");
					stringPlus.AppendSpaceLine(8, "modelt." + columnName2 + "=true;");
					stringPlus.AppendSpaceLine(7, "}");
					stringPlus.AppendSpaceLine(7, "else");
					stringPlus.AppendSpaceLine(7, "{");
					stringPlus.AppendSpaceLine(8, "modelt." + columnName2 + "=false;");
					stringPlus.AppendSpaceLine(7, "}");
					break;
				case 5:
					stringPlus.AppendSpaceLine(7, string.Concat(new string[]
					{
						"modelt.",
						columnName2,
						"=(byte[])ds2.Tables[0].Rows[n][\"",
						columnName2,
						"\"];"
					}));
					break;
				case 6:
					stringPlus.AppendSpaceLine(7, string.Concat(new string[]
					{
						"modelt.",
						columnName2,
						"=new Guid(ds2.Tables[0].Rows[n][\"",
						columnName2,
						"\"].ToString());"
					}));
					break;
				default:
					goto IL_B1D;
				}
				IL_B5B:
				stringPlus.AppendSpaceLine(6, "}");
				continue;
				IL_B1D:
				stringPlus.AppendSpaceLine(7, string.Concat(new string[]
				{
					"modelt.",
					columnName2,
					"=ds2.Tables[0].Rows[n][\"",
					columnName2,
					"\"].ToString();"
				}));
				goto IL_B5B;
			}
			stringPlus.AppendSpaceLine(6, "models.Add(modelt);");
			stringPlus.AppendSpaceLine(5, "}");
			stringPlus.AppendSpaceLine(5, "model." + this.ModelNameSon + "s = models;");
			stringPlus.AppendSpaceLine(5, "#endregion  子表字段信息end");
			stringPlus.AppendSpaceLine(4, "}");
			stringPlus.AppendSpaceLine(4, "#endregion  子表信息end");
			stringPlus.AppendLine();
			stringPlus.AppendSpaceLine(4, "return model;");
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
			StringPlus stringPlus = new StringPlus();
			stringPlus.AppendLine();
			stringPlus.AppendSpaceLine(2, "/// <summary>");
			stringPlus.AppendSpaceLine(2, "/// " + this.Languagelist["summaryGetModel"].ToString());
			stringPlus.AppendSpaceLine(2, "/// </summary>");
			stringPlus.AppendSpaceLine(2, "public " + this.ModelSpaceParent + " DataRowToModel(DataRow row)");
			stringPlus.AppendSpaceLine(2, "{");
			stringPlus.AppendSpaceLine(3, this.ModelSpaceParent + " model=new " + this.ModelSpaceParent + "();");
			stringPlus.AppendSpaceLine(3, "if (row != null)");
			stringPlus.AppendSpaceLine(3, "{");
			foreach (ColumnInfo current in this.FieldlistParent)
			{
				string columnName = current.ColumnName;
				string typeName = current.TypeName;
				stringPlus.AppendSpaceLine(4, string.Concat(new string[]
				{
					"if(row[\"",
					columnName,
					"\"]!=null && row[\"",
					columnName,
					"\"].ToString()!=\"\")"
				}));
				stringPlus.AppendSpaceLine(4, "{");
				string key;
				if ((key = CodeCommon.DbTypeToCS(typeName)) == null)
				{
					goto IL_4C4;
				}
				if (dtCollection == null)
				{
					dtCollection = new Dictionary<string, int>(10)
					{

						{
							"int",
							0
						},

						{
							"long",
							1
						},

						{
							"decimal",
							2
						},

						{
							"float",
							3
						},

						{
							"DateTime",
							4
						},

						{
							"string",
							5
						},

						{
							"bool",
							6
						},

						{
							"byte[]",
							7
						},

						{
							"uniqueidentifier",
							8
						},

						{
							"Guid",
							9
						}
					};
				}
				int num;
				if (!dtCollection.TryGetValue(key, out num))
				{
					goto IL_4C4;
				}
				switch (num)
				{
				case 0:
					stringPlus.AppendSpaceLine(5, string.Concat(new string[]
					{
						"model.",
						columnName,
						"=int.Parse(row[\"",
						columnName,
						"\"].ToString());"
					}));
					break;
				case 1:
					stringPlus.AppendSpaceLine(5, string.Concat(new string[]
					{
						"model.",
						columnName,
						"=long.Parse(row[\"",
						columnName,
						"\"].ToString());"
					}));
					break;
				case 2:
					stringPlus.AppendSpaceLine(5, string.Concat(new string[]
					{
						"model.",
						columnName,
						"=decimal.Parse(row[\"",
						columnName,
						"\"].ToString());"
					}));
					break;
				case 3:
					stringPlus.AppendSpaceLine(5, string.Concat(new string[]
					{
						"model.",
						columnName,
						"=float.Parse(row[\"",
						columnName,
						"\"].ToString());"
					}));
					break;
				case 4:
					stringPlus.AppendSpaceLine(5, string.Concat(new string[]
					{
						"model.",
						columnName,
						"=DateTime.Parse(row[\"",
						columnName,
						"\"].ToString());"
					}));
					break;
				case 5:
					stringPlus.AppendSpaceLine(5, string.Concat(new string[]
					{
						"model.",
						columnName,
						"=row[\"",
						columnName,
						"\"].ToString();"
					}));
					break;
				case 6:
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
					break;
				case 7:
					stringPlus.AppendSpaceLine(5, string.Concat(new string[]
					{
						"model.",
						columnName,
						"=(byte[])row[\"",
						columnName,
						"\"];"
					}));
					break;
				case 8:
				case 9:
					stringPlus.AppendSpaceLine(5, string.Concat(new string[]
					{
						"model.",
						columnName,
						"= new Guid(row[\"",
						columnName,
						"\"].ToString());"
					}));
					break;
				default:
					goto IL_4C4;
				}
				IL_500:
				stringPlus.AppendSpaceLine(4, "}");
				continue;
				IL_4C4:
				stringPlus.AppendSpaceLine(5, string.Concat(new string[]
				{
					"//model.",
					columnName,
					"=row[\"",
					columnName,
					"\"].ToString();"
				}));
				goto IL_500;
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
			stringPlus.AppendSpaceLine(3, "strSql.Append(\" FROM " + this.TableNameParent + " \");");
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
				stringPlus.AppendSpaceLine(3, "strSql.Append(\" FROM " + this.TableNameParent + " \");");
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
		public string CreatGetListByPageProc()
		{
			StringPlus stringPlus = new StringPlus();
			return stringPlus.Value;
		}
	}
}
