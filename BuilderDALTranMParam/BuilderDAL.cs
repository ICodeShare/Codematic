using Maticsoft.CodeHelper;
using Maticsoft.IBuilder;
using Maticsoft.IDBO;
using Maticsoft.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
namespace Maticsoft.BuilderDALTranMParam
{
	public class BuilderDAL : IBuilderDALMTran
	{
		protected string _key = "ID";
		protected string _keyType = "int";
		private IDbObject dbobj;
		private string _dbname;
		private string _namespace;
		private string _folder;
		private string _modelpath;
		private string _dalpath;
		private string _idalpath;
		private string _iclass;
		private string _dbhelperName;
		private List<ModelTran> _modelTranList;
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
		public List<ModelTran> ModelTranList
		{
			get
			{
				return this._modelTranList;
			}
			set
			{
				this._modelTranList = value;
			}
		}
		public Hashtable Languagelist
		{
			get
			{
				return Language.LoadFromCfg("BuilderDALTranParam.lan");
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
		public string ModelSpace(string ModelName)
		{
			return this.Modelpath + "." + ModelName;
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
			this._namespace = namepace;
			this._folder = folder;
			this._dbhelperName = dbherlpername;
			this._modelpath = modelpath;
			this._dalpath = dalpath;
			this._idalpath = idalpath;
			this._iclass = iclass;
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
		public string GetDALCode()
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
			stringPlus.AppendSpaceLine(1, "/// " + this.Languagelist["summary"].ToString());
			stringPlus.AppendSpaceLine(1, "/// </summary>");
			stringPlus.AppendSpace(1, "public partial class Class1");
			if (this.IClass != "")
			{
				stringPlus.Append(":" + this.IClass);
			}
			stringPlus.AppendLine("");
			stringPlus.AppendSpaceLine(1, "{");
			if (this.ModelTranList.Count == 0)
			{
				return "";
			}
			StringPlus stringPlus2 = new StringPlus();
			foreach (ModelTran current in this.ModelTranList)
			{
				if (current.Action.ToLower() == "delete")
				{
					using (List<ColumnInfo>.Enumerator enumerator2 = current.Keys.GetEnumerator())
					{
						while (enumerator2.MoveNext())
						{
							ColumnInfo current2 = enumerator2.Current;
							if (current2.IsPrimaryKey || !current2.IsIdentity)
							{
								stringPlus2.Append(CodeCommon.DbTypeToCS(current2.TypeName) + " " + current2.ColumnName + ",");
							}
						}
						continue;
					}
				}
				stringPlus2.Append(this.ModelSpace(current.ModelName) + " model" + current.ModelName + ",");
			}
			stringPlus2.DelLastComma();
			stringPlus.AppendSpaceLine(2, "public void TranMethod(" + stringPlus2.Value + " )");
			stringPlus.AppendSpaceLine(2, "{");
			stringPlus.AppendSpaceLine(3, "List<CommandInfo> cmdlist = new List<CommandInfo>();");
			stringPlus.AppendLine();
			int num2 = 1;
			foreach (ModelTran current3 in this.ModelTranList)
			{
				string a;
				if ((a = current3.Action.ToLower()) != null)
				{
					if (!(a == "insert"))
					{
						if (!(a == "update"))
						{
							if (a == "delete")
							{
								stringPlus.Append(this.CreatDelete(current3.TableName, current3.ModelName, current3.Fieldlist, current3.Keys, num2));
								stringPlus.AppendSpaceLine(3, string.Concat(new object[]
								{
									"CommandInfo cmd",
									num2,
									" = new CommandInfo(strSql",
									num2,
									".ToString(), parameters",
									num2,
									");"
								}));
								stringPlus.AppendSpaceLine(3, "cmdlist.Add(cmd" + num2 + ");");
								stringPlus.AppendLine();
							}
						}
						else
						{
							stringPlus.Append(this.CreatUpdate(current3.TableName, current3.ModelName, current3.Fieldlist, current3.Keys, num2));
							stringPlus.AppendSpaceLine(3, string.Concat(new object[]
							{
								"CommandInfo cmd",
								num2,
								" = new CommandInfo(strSql",
								num2,
								".ToString(), parameters",
								num2,
								");"
							}));
							stringPlus.AppendSpaceLine(3, "cmdlist.Add(cmd" + num2 + ");");
							stringPlus.AppendLine();
						}
					}
					else
					{
						stringPlus.Append(this.CreatAdd(current3.TableName, current3.ModelName, current3.Fieldlist, num2));
						stringPlus.AppendSpaceLine(3, string.Concat(new object[]
						{
							"CommandInfo cmd",
							num2,
							" = new CommandInfo(strSql",
							num2,
							".ToString(), parameters",
							num2,
							");"
						}));
						stringPlus.AppendSpaceLine(3, "cmdlist.Add(cmd" + num2 + ");");
						stringPlus.AppendLine();
					}
				}
				num2++;
			}
			stringPlus.AppendSpaceLine(3, "DbHelperSQL.ExecuteSqlTran(cmdlist);");
			stringPlus.AppendSpaceLine(2, "}");
			stringPlus.AppendSpaceLine(1, "}");
			stringPlus.AppendLine("}");
			stringPlus.AppendLine("");
			return stringPlus.ToString();
		}
		public string CreatAdd(string tabName, string ModelName, List<ColumnInfo> Fieldlist, int num)
		{
			StringPlus stringPlus = new StringPlus();
			StringPlus stringPlus2 = new StringPlus();
			StringPlus stringPlus3 = new StringPlus();
			StringPlus stringPlus4 = new StringPlus();
			StringPlus stringPlus5 = new StringPlus();
			stringPlus.AppendSpaceLine(3, "StringBuilder strSql" + num + "=new StringBuilder();");
			stringPlus.AppendSpaceLine(3, string.Concat(new object[]
			{
				"strSql",
				num,
				".Append(\"insert into ",
				tabName,
				"(\");"
			}));
			stringPlus2.AppendSpace(3, "strSql" + num + ".Append(\"");
			int num2 = 0;
			foreach (ColumnInfo current in Fieldlist)
			{
				string columnName = current.ColumnName;
				string typeName = current.TypeName;
				bool arg_CF_0 = current.IsIdentity;
				string length = current.Length;
				bool arg_E0_0 = current.Nullable;
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
						stringPlus5.AppendSpaceLine(3, string.Concat(new object[]
						{
							"parameters",
							num,
							"[",
							num2,
							"].Value = Guid.NewGuid();"
						}));
					}
					else
					{
						stringPlus5.AppendSpaceLine(3, string.Concat(new object[]
						{
							"parameters",
							num,
							"[",
							num2,
							"].Value = model",
							ModelName,
							".",
							columnName,
							";"
						}));
					}
					num2++;
				}
			}
			stringPlus2.DelLastComma();
			stringPlus3.DelLastComma();
			stringPlus4.DelLastComma();
			stringPlus2.AppendLine(")\");");
			stringPlus.Append(stringPlus2.ToString());
			stringPlus.AppendSpaceLine(3, "strSql.Append(\" values (\");");
			stringPlus.AppendSpaceLine(3, "strSql.Append(\"" + stringPlus3.ToString() + ")\");");
			stringPlus.AppendSpaceLine(3, string.Concat(new object[]
			{
				this.DbParaHead,
				"Parameter[] parameters",
				num,
				" = {"
			}));
			stringPlus.Append(stringPlus4.Value);
			stringPlus.AppendLine("};");
			stringPlus.AppendLine(stringPlus5.Value);
			return stringPlus.ToString();
		}
		public string CreatUpdate(string tabName, string ModelName, List<ColumnInfo> Fieldlist, List<ColumnInfo> Keys, int num)
		{
			StringPlus stringPlus = new StringPlus();
			StringPlus stringPlus2 = new StringPlus();
			StringPlus stringPlus3 = new StringPlus();
			StringPlus stringPlus4 = new StringPlus();
			stringPlus.AppendSpaceLine(3, "StringBuilder strSql" + num + "=new StringBuilder();");
			stringPlus.AppendSpaceLine(3, string.Concat(new object[]
			{
				"strSql",
				num,
				".Append(\"update ",
				tabName,
				" set \");"
			}));
			int num2 = 0;
			if (Fieldlist.Count == 0)
			{
				Fieldlist = Keys;
			}
			List<ColumnInfo> list = new List<ColumnInfo>();
			foreach (ColumnInfo current in Fieldlist)
			{
				string columnName = current.ColumnName;
				string typeName = current.TypeName;
				string length = current.Length;
				bool arg_C6_0 = current.IsIdentity;
				bool arg_CE_0 = current.IsPrimaryKey;
				if (current.IsIdentity || current.IsPrimaryKey || Keys.Contains(current))
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
							"parameters",
							num,
							"[",
							num2,
							"].Value = model.",
							columnName,
							";"
						}));
						num2++;
						stringPlus4.AppendSpaceLine(3, string.Concat(new object[]
						{
							"strSql",
							num,
							".Append(\"",
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
				bool arg_2A0_0 = current2.IsIdentity;
				bool arg_2A8_0 = current2.IsPrimaryKey;
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
					"parameters",
					num,
					"[",
					num2,
					"].Value = model",
					ModelName,
					".",
					columnName2,
					";"
				}));
				num2++;
			}
			if (stringPlus4.Value.Length > 0)
			{
				stringPlus4.DelLastComma();
				stringPlus4.AppendLine("\");");
			}
			else
			{
				stringPlus4.AppendLine("#warning 系统发现缺少更新的字段，请手工确认如此更新是否正确！ ");
				foreach (ColumnInfo current3 in Fieldlist)
				{
					string columnName3 = current3.ColumnName;
					stringPlus4.AppendSpaceLine(3, string.Concat(new object[]
					{
						"strSql",
						num,
						".Append(\"",
						columnName3,
						"=",
						this.preParameter,
						columnName3,
						",\");"
					}));
				}
				if (Fieldlist.Count > 0)
				{
					stringPlus4.DelLastComma();
					stringPlus4.AppendLine("\");");
				}
			}
			stringPlus.Append(stringPlus4.Value);
			stringPlus.AppendSpaceLine(3, string.Concat(new object[]
			{
				"strSql",
				num,
				".Append(\" where ",
				CodeCommon.GetWhereParameterExpression(Keys, true, this.dbobj.DbType),
				"\");"
			}));
			stringPlus.AppendSpaceLine(3, string.Concat(new object[]
			{
				this.DbParaHead,
				"Parameter[] parameters",
				num,
				" = {"
			}));
			stringPlus2.DelLastComma();
			stringPlus.Append(stringPlus2.Value);
			stringPlus.AppendLine("};");
			stringPlus.AppendLine(stringPlus3.Value);
			return stringPlus.ToString();
		}
		public string CreatDelete(string tabName, string ModelName, List<ColumnInfo> Fieldlist, List<ColumnInfo> Keys, int num)
		{
			StringPlus stringPlus = new StringPlus();
			stringPlus.AppendSpaceLine(3, "StringBuilder strSql" + num + "=new StringBuilder();");
			stringPlus.AppendSpaceLine(3, string.Concat(new object[]
			{
				"strSql",
				num,
				".Append(\"delete from ",
				tabName,
				" \");"
			}));
			stringPlus.AppendSpaceLine(3, string.Concat(new object[]
			{
				"strSql",
				num,
				".Append(\" where ",
				CodeCommon.GetWhereParameterExpression(Keys, true, this.dbobj.DbType),
				"\");"
			}));
			stringPlus.AppendLine(CodeCommon.GetPreParameter(Keys, true, this.dbobj.DbType));
			if (CodeCommon.HasNoIdentityKey(Keys) && CodeCommon.GetIdentityKey(Keys) != null)
			{
				stringPlus.AppendSpaceLine(3, "StringBuilder strSql" + num + "=new StringBuilder();");
				stringPlus.AppendSpaceLine(3, string.Concat(new object[]
				{
					"strSql",
					num,
					".Append(\"delete from ",
					tabName,
					" \");"
				}));
				stringPlus.AppendSpaceLine(3, string.Concat(new object[]
				{
					"strSql",
					num,
					".Append(\" where ",
					CodeCommon.GetWhereParameterExpression(Keys, false, this.dbobj.DbType),
					"\");"
				}));
				stringPlus.AppendLine(CodeCommon.GetPreParameter(Keys, false, this.dbobj.DbType));
			}
			return stringPlus.Value;
		}
	}
}
