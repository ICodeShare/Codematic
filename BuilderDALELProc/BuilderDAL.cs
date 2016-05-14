using Maticsoft.CodeHelper;
using Maticsoft.IBuilder;
using Maticsoft.IDBO;
using Maticsoft.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
namespace Maticsoft.BuilderDALELProc
{
	public class BuilderDAL : IBuilderDAL
	{
		protected string _key = "ID";
		protected string _keyType = "int";
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
		public Hashtable Languagelist
		{
			get
			{
				return Language.LoadFromCfg("BuilderDALELProc.lan");
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
		public string DbParaDbType
		{
			get
			{
				return "DbType";
			}
		}
		public string preParameter
		{
			get
			{
				return "@";
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
			foreach (ColumnInfo current in keys)
			{
				stringPlus.AppendSpaceLine(3, string.Concat(new string[]
				{
					"db.AddInParameter(dbCommand, \"",
					current.ColumnName,
					"\", DbType.",
					BuilderDAL.CSToProcType(current.TypeName),
					",",
					current.ColumnName,
					");"
				}));
			}
			return stringPlus.Value;
		}
		public BuilderDAL()
		{
		}
		public BuilderDAL(IDbObject idbobj)
		{
			this.dbobj = idbobj;
		}
		public BuilderDAL(IDbObject idbobj, string dbname, string tablename, string modelname, string dalName, List<ColumnInfo> fieldlist, List<ColumnInfo> keys, string namepace, string folder, string dbherlpername, string modelpath, string modelspace, string dalpath, string idalpath, string iclass)
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
		public string GetDALCode(bool Maxid, bool Exists, bool Add, bool Update, bool Delete, bool GetModel, bool List)
		{
			StringPlus stringPlus = new StringPlus();
			stringPlus.AppendLine("using System;");
			stringPlus.AppendLine("using System.Data;");
			stringPlus.AppendLine("using System.Text;");
			stringPlus.AppendLine("using System.Collections.Generic;");
			stringPlus.AppendLine("using Microsoft.Practices.EnterpriseLibrary.Data;");
			stringPlus.AppendLine("using Microsoft.Practices.EnterpriseLibrary.Data.Sql;");
			stringPlus.AppendLine("using System.Data.Common;");
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
				stringPlus.AppendLine(this.CreatGetListByPage());
				stringPlus.AppendLine(this.CreatGetListByPageProc());
				stringPlus.AppendLine(this.CreatGetListArray());
				stringPlus.AppendLine(this.CreatReaderBind());
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
							stringPlus.AppendSpaceLine(3, string.Concat(new string[]
							{
								"string strsql = \"select max(",
								columnName,
								")+1 from ",
								this._tablename,
								"\";"
							}));
							stringPlus.AppendSpaceLine(3, "Database db = DatabaseFactory.CreateDatabase();");
							stringPlus.AppendSpaceLine(3, "object obj = db.ExecuteScalar(CommandType.Text, strsql);");
							stringPlus.AppendSpaceLine(3, "if (obj != null && obj != DBNull.Value)");
							stringPlus.AppendSpaceLine(3, "{");
							stringPlus.AppendSpaceLine(4, "return int.Parse(obj.ToString());");
							stringPlus.AppendSpaceLine(3, "}");
							stringPlus.AppendSpaceLine(3, "return 1;");
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
				stringPlus.AppendSpaceLine(2, "/// <summary>");
				stringPlus.AppendSpaceLine(2, "/// " + this.Languagelist["summaryExists"].ToString());
				stringPlus.AppendSpaceLine(2, "/// </summary>");
				stringPlus.AppendSpaceLine(2, "public bool Exists(" + CodeCommon.GetInParameter(this.Keys, false) + ")");
				stringPlus.AppendSpaceLine(2, "{");
				stringPlus.AppendSpaceLine(3, "Database db = DatabaseFactory.CreateDatabase();");
				stringPlus.AppendSpaceLine(3, "DbCommand dbCommand = db.GetStoredProcCommand(\"" + this.ProcPrefix + this._tablename + "_Exists\");");
				stringPlus.Append(this.GetPreParameter(this.Keys));
				stringPlus.AppendSpaceLine(3, "int result;");
				stringPlus.AppendSpaceLine(3, "object obj = db.ExecuteScalar(dbCommand);");
				stringPlus.AppendSpaceLine(3, "int.TryParse(obj.ToString(),out result);");
				stringPlus.AppendSpaceLine(3, "if(result==1)");
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
		public string CreatAdd()
		{
			StringPlus stringPlus = new StringPlus();
			new StringPlus();
			stringPlus.AppendSpaceLine(2, "/// <summary>");
			stringPlus.AppendSpaceLine(2, "///  " + this.Languagelist["summaryadd"].ToString());
			stringPlus.AppendSpaceLine(2, "/// </summary>");
			string text = "bool";
			if ((this.dbobj.DbType == "SQL2000" || this.dbobj.DbType == "SQL2005" || this.dbobj.DbType == "SQL2008" || this.dbobj.DbType == "SQL2012") && this.IsHasIdentity)
			{
				text = "int";
				if (this._keyType != "int")
				{
					text = this._keyType;
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
			stringPlus.AppendSpaceLine(3, "Database db = DatabaseFactory.CreateDatabase();");
			stringPlus.AppendSpaceLine(3, "DbCommand dbCommand = db.GetStoredProcCommand(\"" + this.ProcPrefix + this._tablename + "_ADD\");");
			string text3 = string.Empty;
			foreach (ColumnInfo current in this.Fieldlist)
			{
				string columnName = current.ColumnName;
				string typeName = current.TypeName;
				bool arg_199_0 = current.IsIdentity;
				string length = current.Length;
				if (current.IsIdentity)
				{
					stringPlus.AppendSpaceLine(3, string.Concat(new string[]
					{
						"db.AddOutParameter(dbCommand, \"",
						columnName,
						"\", DbType.",
						BuilderDAL.CSToProcType(typeName),
						", ",
						length,
						");"
					}));
					text3 = columnName;
				}
				else
				{
					stringPlus.AppendSpaceLine(3, string.Concat(new string[]
					{
						"db.AddInParameter(dbCommand, \"",
						columnName,
						"\", DbType.",
						BuilderDAL.CSToProcType(typeName),
						", model.",
						columnName,
						");"
					}));
				}
			}
			stringPlus.AppendSpaceLine(3, "db.ExecuteNonQuery(dbCommand);");
			if ((this.dbobj.DbType == "SQL2000" || this.dbobj.DbType == "SQL2005" || this.dbobj.DbType == "SQL2008" || this.dbobj.DbType == "SQL2012") && this.IsHasIdentity)
			{
				stringPlus.AppendSpaceLine(3, string.Concat(new string[]
				{
					"return (",
					this._keyType,
					")db.GetParameterValue(dbCommand, \"",
					text3,
					"\");"
				}));
			}
			stringPlus.AppendSpaceLine(2, "}");
			return stringPlus.Value;
		}
		public string CreatUpdate()
		{
			StringPlus stringPlus = new StringPlus();
			new StringPlus();
			stringPlus.AppendSpaceLine(2, "/// <summary>");
			stringPlus.AppendSpaceLine(2, "///  " + this.Languagelist["summaryUpdate"].ToString());
			stringPlus.AppendSpaceLine(2, "/// </summary>");
			stringPlus.AppendSpaceLine(2, "public void Update(" + this.ModelSpace + " model)");
			stringPlus.AppendSpaceLine(2, "{");
			stringPlus.AppendSpaceLine(3, "Database db = DatabaseFactory.CreateDatabase();");
			stringPlus.AppendSpaceLine(3, "DbCommand dbCommand = db.GetStoredProcCommand(\"" + this.ProcPrefix + this._tablename + "_Update\");");
			foreach (ColumnInfo current in this.Fieldlist)
			{
				string columnName = current.ColumnName;
				string typeName = current.TypeName;
				string arg_D2_0 = current.Length;
				stringPlus.AppendSpaceLine(3, string.Concat(new string[]
				{
					"db.AddInParameter(dbCommand, \"",
					columnName,
					"\", DbType.",
					BuilderDAL.CSToProcType(typeName),
					", model.",
					columnName,
					");"
				}));
			}
			stringPlus.AppendSpaceLine(3, "db.ExecuteNonQuery(dbCommand);");
			stringPlus.AppendSpaceLine(2, "}");
			return stringPlus.Value;
		}
		public string CreatDelete()
		{
			StringPlus stringPlus = new StringPlus();
			stringPlus.AppendSpaceLine(2, "/// <summary>");
			stringPlus.AppendSpaceLine(2, "/// " + this.Languagelist["summaryDelete"].ToString());
			stringPlus.AppendSpaceLine(2, "/// </summary>");
			stringPlus.AppendSpaceLine(2, "public void Delete(" + CodeCommon.GetInParameter(this.Keys, true) + ")");
			stringPlus.AppendSpaceLine(2, "{");
			stringPlus.AppendSpaceLine(3, "Database db = DatabaseFactory.CreateDatabase();");
			stringPlus.AppendSpaceLine(3, "DbCommand dbCommand = db.GetStoredProcCommand(\"" + this.ProcPrefix + this._tablename + "_Delete\");");
			stringPlus.AppendLine(this.GetPreParameter(this.Keys));
			stringPlus.AppendSpaceLine(3, "int rows=db.ExecuteNonQuery(dbCommand);");
			stringPlus.AppendSpaceLine(3, "if (rows > 0)");
			stringPlus.AppendSpaceLine(3, "{");
			stringPlus.AppendSpaceLine(4, "return true;");
			stringPlus.AppendSpaceLine(3, "}");
			stringPlus.AppendSpaceLine(3, "else");
			stringPlus.AppendSpaceLine(3, "{");
			stringPlus.AppendSpaceLine(4, "return false;");
			stringPlus.AppendSpaceLine(3, "}");
			stringPlus.AppendSpaceLine(2, "}");
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
			StringPlus stringPlus = new StringPlus();
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
			stringPlus.AppendSpaceLine(3, "Database db = DatabaseFactory.CreateDatabase();");
			stringPlus.AppendSpaceLine(3, "DbCommand dbCommand = db.GetStoredProcCommand(\"" + this.ProcPrefix + this._tablename + "_GetModel\");");
			stringPlus.AppendLine(this.GetPreParameter(this.Keys));
			stringPlus.AppendSpaceLine(3, this.ModelSpace + " model=null;");
			stringPlus.AppendSpaceLine(3, "using (IDataReader dataReader = db.ExecuteReader(dbCommand))");
			stringPlus.AppendSpaceLine(3, "{");
			stringPlus.AppendSpaceLine(4, "if(dataReader.Read())");
			stringPlus.AppendSpaceLine(4, "{");
			stringPlus.AppendSpaceLine(5, "model=ReaderBind(dataReader);");
			stringPlus.AppendSpaceLine(4, "}");
			stringPlus.AppendSpaceLine(3, "}");
			stringPlus.AppendSpaceLine(3, "return model;");
			stringPlus.AppendSpaceLine(2, "}");
			return stringPlus.Value;
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
			stringPlus.AppendSpaceLine(3, "Database db = DatabaseFactory.CreateDatabase();");
			stringPlus.AppendSpaceLine(3, "return db.ExecuteDataSet(CommandType.Text, strSql.ToString());");
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
				stringPlus.AppendSpaceLine(3, "Database db = DatabaseFactory.CreateDatabase();");
				stringPlus.AppendSpaceLine(3, "return db.ExecuteDataSet(CommandType.Text, strSql.ToString());");
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
			stringPlus.AppendSpaceLine(4, "strSql.Append(\"order by T." + this._key + " desc\");");
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
			stringPlus.AppendSpaceLine(3, "Database db = DatabaseFactory.CreateDatabase();");
			stringPlus.AppendSpaceLine(3, "DbCommand dbCommand = db.GetStoredProcCommand(\"UP_GetRecordByPage\");");
			stringPlus.AppendSpaceLine(3, "db.AddInParameter(dbCommand, \"tblName\", DbType.AnsiString, \"" + this.TableName + "\");");
			stringPlus.AppendSpaceLine(3, "db.AddInParameter(dbCommand, \"fldName\", DbType.AnsiString, \"" + this._key + "\");");
			stringPlus.AppendSpaceLine(3, "db.AddInParameter(dbCommand, \"PageSize\", DbType.Int32, PageSize);");
			stringPlus.AppendSpaceLine(3, "db.AddInParameter(dbCommand, \"PageIndex\", DbType.Int32, PageIndex);");
			stringPlus.AppendSpaceLine(3, "db.AddInParameter(dbCommand, \"IsReCount\", DbType.Boolean, 0);");
			stringPlus.AppendSpaceLine(3, "db.AddInParameter(dbCommand, \"OrderType\", DbType.Boolean, 0);");
			stringPlus.AppendSpaceLine(3, "db.AddInParameter(dbCommand, \"strWhere\", DbType.AnsiString, strWhere);");
			stringPlus.AppendSpaceLine(3, "return db.ExecuteDataSet(dbCommand);");
			stringPlus.AppendSpaceLine(2, "}*/");
			return stringPlus.Value;
		}
		public string CreatReaderBind()
		{
			StringPlus stringPlus = new StringPlus();
			StringPlus stringPlus2 = new StringPlus();
			stringPlus.AppendLine("");
			stringPlus.AppendSpaceLine(2, "/// <summary>");
			stringPlus.AppendSpaceLine(2, "/// 对象实体绑定数据");
			stringPlus.AppendSpaceLine(2, "/// </summary>");
			stringPlus.AppendSpaceLine(2, "public " + this.ModelSpace + " ReaderBind(IDataReader dataReader)");
			stringPlus.AppendSpaceLine(2, "{");
			stringPlus.AppendSpaceLine(3, this.ModelSpace + " model=new " + this.ModelSpace + "();");
			bool flag = false;
			foreach (ColumnInfo current in this.Fieldlist)
			{
				string columnName = current.ColumnName;
				string typeName = current.TypeName;
				bool arg_BE_0 = current.IsIdentity;
				string arg_C5_0 = current.Length;
				string key;
				switch (key = CodeCommon.DbTypeToCS(typeName))
				{
				case "int":
					flag = true;
					stringPlus2.AppendSpaceLine(3, "ojb = dataReader[\"" + columnName + "\"];");
					stringPlus2.AppendSpaceLine(3, "if(ojb != null && ojb != DBNull.Value)");
					stringPlus2.AppendSpaceLine(3, "{");
					stringPlus2.AppendSpaceLine(4, "model." + columnName + "=(int)ojb;");
					stringPlus2.AppendSpaceLine(3, "}");
					continue;
				case "long":
					flag = true;
					stringPlus2.AppendSpaceLine(3, "ojb = dataReader[\"" + columnName + "\"];");
					stringPlus2.AppendSpaceLine(3, "if(ojb != null && ojb != DBNull.Value)");
					stringPlus2.AppendSpaceLine(3, "{");
					stringPlus2.AppendSpaceLine(4, "model." + columnName + "=(long)ojb;");
					stringPlus2.AppendSpaceLine(3, "}");
					continue;
				case "decimal":
					flag = true;
					stringPlus2.AppendSpaceLine(3, "ojb = dataReader[\"" + columnName + "\"];");
					stringPlus2.AppendSpaceLine(3, "if(ojb != null && ojb != DBNull.Value)");
					stringPlus2.AppendSpaceLine(3, "{");
					stringPlus2.AppendSpaceLine(4, "model." + columnName + "=(decimal)ojb;");
					stringPlus2.AppendSpaceLine(3, "}");
					continue;
				case "DateTime":
					flag = true;
					stringPlus2.AppendSpaceLine(3, "ojb = dataReader[\"" + columnName + "\"];");
					stringPlus2.AppendSpaceLine(3, "if(ojb != null && ojb != DBNull.Value)");
					stringPlus2.AppendSpaceLine(3, "{");
					stringPlus2.AppendSpaceLine(4, "model." + columnName + "=(DateTime)ojb;");
					stringPlus2.AppendSpaceLine(3, "}");
					continue;
				case "string":
					stringPlus2.AppendSpaceLine(3, string.Concat(new string[]
					{
						"model.",
						columnName,
						"=dataReader[\"",
						columnName,
						"\"].ToString();"
					}));
					continue;
				case "bool":
					flag = true;
					stringPlus2.AppendSpaceLine(3, "ojb = dataReader[\"" + columnName + "\"];");
					stringPlus2.AppendSpaceLine(3, "if(ojb != null && ojb != DBNull.Value)");
					stringPlus2.AppendSpaceLine(3, "{");
					stringPlus2.AppendSpaceLine(4, "model." + columnName + "=(bool)ojb;");
					stringPlus2.AppendSpaceLine(3, "}");
					continue;
				case "byte[]":
					flag = true;
					stringPlus2.AppendSpaceLine(3, "ojb = dataReader[\"" + columnName + "\"];");
					stringPlus2.AppendSpaceLine(3, "if(ojb != null && ojb != DBNull.Value)");
					stringPlus2.AppendSpaceLine(3, "{");
					stringPlus2.AppendSpaceLine(4, "model." + columnName + "=(byte[])ojb;");
					stringPlus2.AppendSpaceLine(3, "}");
					continue;
				case "Guid":
					flag = true;
					stringPlus2.AppendSpaceLine(3, "ojb = dataReader[\"" + columnName + "\"];");
					stringPlus2.AppendSpaceLine(3, "if(ojb != null && ojb != DBNull.Value)");
					stringPlus2.AppendSpaceLine(3, "{");
					stringPlus2.AppendSpaceLine(4, "model." + columnName + "= new Guid(ojb.ToString());");
					stringPlus2.AppendSpaceLine(3, "}");
					continue;
				}
				stringPlus2.AppendSpaceLine(3, string.Concat(new string[]
				{
					"model.",
					columnName,
					"=dataReader[\"",
					columnName,
					"\"].ToString();\r\n"
				}));
			}
			if (flag)
			{
				stringPlus.AppendSpaceLine(3, "object ojb; ");
			}
			stringPlus.Append(stringPlus2.ToString());
			stringPlus.AppendSpaceLine(3, "return model;");
			stringPlus.AppendSpaceLine(2, "}");
			return stringPlus.Value;
		}
		public string CreatGetListArray()
		{
			string text = "List<" + this.ModelSpace + ">";
			StringPlus stringPlus = new StringPlus();
			stringPlus.AppendSpaceLine(2, "/// <summary>");
			stringPlus.AppendSpaceLine(2, "/// 获得数据列表（比DataSet效率高，推荐使用）");
			stringPlus.AppendSpaceLine(2, "/// </summary>");
			stringPlus.AppendSpaceLine(2, "public " + text + " GetListArray(string strWhere)");
			stringPlus.AppendSpaceLine(2, "{");
			stringPlus.AppendSpaceLine(3, "StringBuilder strSql=new StringBuilder();");
			stringPlus.AppendSpace(3, "strSql.Append(\"select ");
			stringPlus.AppendLine(this.Fieldstrlist + " \");");
			stringPlus.AppendSpaceLine(3, "strSql.Append(\" FROM " + this.TableName + " \");");
			stringPlus.AppendSpaceLine(3, "if(strWhere.Trim()!=\"\")");
			stringPlus.AppendSpaceLine(3, "{");
			stringPlus.AppendSpaceLine(4, "strSql.Append(\" where \"+strWhere);");
			stringPlus.AppendSpaceLine(3, "}");
			stringPlus.AppendSpaceLine(3, text + " list = new " + text + "();");
			stringPlus.AppendSpaceLine(3, "Database db = DatabaseFactory.CreateDatabase();");
			stringPlus.AppendSpaceLine(3, "using (IDataReader dataReader = db.ExecuteReader(CommandType.Text, strSql.ToString()))");
			stringPlus.AppendSpaceLine(3, "{");
			stringPlus.AppendSpaceLine(4, "while (dataReader.Read())");
			stringPlus.AppendSpaceLine(4, "{");
			stringPlus.AppendSpaceLine(5, "list.Add(ReaderBind(dataReader));");
			stringPlus.AppendSpaceLine(4, "}");
			stringPlus.AppendSpaceLine(3, "}");
			stringPlus.AppendSpaceLine(3, "return list;");
			stringPlus.AppendSpaceLine(2, "}");
			return stringPlus.Value;
		}
		private static string CSToProcType(string cstype)
		{
			string key;
			string result;
			switch (key = cstype.Trim().ToLower())
			{
			case "string":
			case "nvarchar":
			case "nchar":
			case "ntext":
				result = "String";
				return result;
			case "text":
			case "char":
			case "varchar":
				result = "AnsiString";
				return result;
			case "datetime":
			case "smalldatetime":
				result = "DateTime";
				return result;
			case "smallint":
				result = "Int16";
				return result;
			case "tinyint":
				result = "Byte";
				return result;
			case "int":
				result = "Int32";
				return result;
			case "bigint":
			case "long":
				result = "Int64";
				return result;
			case "float":
				result = "Double";
				return result;
			case "real":
			case "numeric":
			case "decimal":
				result = "Decimal";
				return result;
			case "money":
			case "smallmoney":
				result = "Currency";
				return result;
			case "bool":
			case "bit":
				result = "Boolean";
				return result;
			case "binary":
			case "varbinary":
				result = "Binary";
				return result;
			case "image":
				result = "Image";
				return result;
			case "uniqueidentifier":
				result = "Guid";
				return result;
			case "timestamp":
				result = "String";
				return result;
			}
			result = "String";
			return result;
		}
	}
}
