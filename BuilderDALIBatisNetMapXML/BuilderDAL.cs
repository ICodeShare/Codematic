using Maticsoft.CodeHelper;
using Maticsoft.IBuilder;
using Maticsoft.IDBO;
using Maticsoft.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;
namespace Maticsoft.BuilderDALIBatisNetMapXML
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
		public string ModelAssembly
		{
			get
			{
				return this._namespace + ".Model";
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
				return Language.LoadFromCfg("BuilderDALIBatisNetMapXML.lan");
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
				string dbType;
				switch (dbType = this.dbobj.DbType)
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
				}
				return "Sql";
			}
		}
		public string DbParaDbType
		{
			get
			{
				string dbType;
				switch (dbType = this.dbobj.DbType)
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
				}
				return "SqlDbType";
			}
		}
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
			stringPlus.AppendSpaceLine(3, this.DbParaHead + "Parameter[] parameters = {");
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
		public string GetDALCode(bool Maxid, bool Exists, bool Add, bool Update, bool Delete, bool GetModel, bool List)
		{
			StringPlus stringPlus = new StringPlus();
			stringPlus.AppendLine(this.GetMapXMLs());
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
						string arg_50_0 = current.ColumnName;
						if (current.IsPrimaryKey)
						{
							stringPlus.AppendLine("");
							stringPlus.AppendSpaceLine(2, "/// <summary>");
							stringPlus.AppendSpaceLine(2, "/// " + this.Languagelist["summaryGetMaxId"].ToString());
							stringPlus.AppendSpaceLine(2, "/// </summary>");
							stringPlus.AppendSpaceLine(2, "public int GetMaxID()");
							stringPlus.AppendSpaceLine(2, "{");
							stringPlus.AppendSpaceLine(2, "return ExecuteGetMaxID(\"GetMaxID\"); ");
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
				stringPlus.AppendSpaceLine(2, "public bool Exists(object Id)");
				stringPlus.AppendSpaceLine(2, "{");
				stringPlus.AppendSpaceLine(3, "return ExecuteExists(\"Exists\", Id);");
				stringPlus.AppendSpaceLine(2, "}");
			}
			return stringPlus.Value;
		}
		public string CreatAdd()
		{
			StringPlus stringPlus = new StringPlus();
			new StringPlus();
			new StringPlus();
			new StringPlus();
			new StringPlus();
			stringPlus.AppendLine();
			stringPlus.AppendSpaceLine(2, "/// <summary>");
			stringPlus.AppendSpaceLine(2, "/// " + this.Languagelist["summaryadd"].ToString());
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
				this.ModelSpace,
				" model)"
			});
			stringPlus.AppendLine(text2);
			stringPlus.AppendSpaceLine(2, "{");
			if ((this.dbobj.DbType == "SQL2000" || this.dbobj.DbType == "SQL2005" || this.dbobj.DbType == "SQL2008" || this.dbobj.DbType == "SQL2012") && this.IsHasIdentity)
			{
				stringPlus.AppendSpaceLine(3, "return ExecuteInsert(\"Insert" + this.ModelName + "\", model);");
			}
			else
			{
				stringPlus.AppendSpaceLine(3, "ExecuteInsert(\"Insert" + this.ModelName + "\", model);");
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
			new StringPlus();
			new StringPlus();
			stringPlus.AppendSpaceLine(2, "/// <summary>");
			stringPlus.AppendSpaceLine(2, "/// " + this.Languagelist["summaryUpdate"].ToString());
			stringPlus.AppendSpaceLine(2, "/// </summary>");
			stringPlus.AppendSpaceLine(2, "public void Update(" + this.ModelSpace + " model)");
			stringPlus.AppendSpaceLine(2, "{");
			stringPlus.AppendSpaceLine(3, "ExecuteUpdate(\"Update" + this.ModelName + "\", model);");
			stringPlus.AppendSpaceLine(2, "}");
			return stringPlus.ToString();
		}
		public string CreatDelete()
		{
			StringPlus stringPlus = new StringPlus();
			stringPlus.AppendSpaceLine(2, "/// <summary>");
			stringPlus.AppendSpaceLine(2, "/// " + this.Languagelist["summaryDelete"].ToString());
			stringPlus.AppendSpaceLine(2, "/// </summary>");
			stringPlus.AppendSpaceLine(2, "public void Delete(" + this.ModelSpace + " model)");
			stringPlus.AppendSpaceLine(2, "{");
			stringPlus.AppendSpaceLine(3, "ExecuteDelete(\"Delete" + this.ModelName + "\", model);");
			stringPlus.AppendSpaceLine(2, "}");
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
			stringPlus.AppendSpaceLine(2, "public " + this.ModelSpace + " GetModel(object Id)");
			stringPlus.AppendSpaceLine(2, "{");
			stringPlus.AppendSpaceLine(3, this.ModelSpace + " model = ExecuteQueryForObject<" + this.ModelSpace + ">(\"SelectById\", Id);");
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
			stringPlus.AppendSpaceLine(2, "public DataSet GetList(" + this.ModelSpace + " model)");
			stringPlus.AppendSpaceLine(2, "{");
			stringPlus.AppendSpaceLine(3, "IList<" + this.ModelSpace + "> list = null; ");
			stringPlus.AppendSpaceLine(3, string.Concat(new string[]
			{
				"list = ExecuteQueryForList<",
				this.ModelSpace,
				">(\"Select",
				this.ModelName,
				"\", model); "
			}));
			stringPlus.AppendSpaceLine(3, "return list; ");
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
			stringPlus.AppendSpaceLine(3, "parameters[1].Value = \"" + this._key + "\";");
			stringPlus.AppendSpaceLine(3, "parameters[2].Value = PageSize;");
			stringPlus.AppendSpaceLine(3, "parameters[3].Value = PageIndex;");
			stringPlus.AppendSpaceLine(3, "parameters[4].Value = 0;");
			stringPlus.AppendSpaceLine(3, "parameters[5].Value = 0;");
			stringPlus.AppendSpaceLine(3, "parameters[6].Value = strWhere;\t");
			stringPlus.AppendSpaceLine(3, "return " + this.DbHelperName + ".RunProcedure(\"UP_GetRecordByPage\",parameters,\"ds\");");
			stringPlus.AppendSpaceLine(2, "}*/");
			return stringPlus.Value;
		}
		public string GetMapXMLs()
		{
			XmlDocument xmlDocument = new XmlDocument();
			XmlNode newChild = xmlDocument.CreateNode(XmlNodeType.XmlDeclaration, "", "");
			xmlDocument.AppendChild(newChild);
			XmlElement xmlElement = xmlDocument.CreateElement("", "sqlMap", "");
			XmlAttribute xmlAttribute = xmlDocument.CreateAttribute("xmlns");
			xmlAttribute.Value = "http://ibatis.apache.org/mapping";
			xmlElement.Attributes.Append(xmlAttribute);
			xmlAttribute = xmlDocument.CreateAttribute("xmlns:xsi");
			xmlAttribute.Value = "http://www.w3.org/2001/XMLSchema-instance";
			xmlElement.Attributes.Append(xmlAttribute);
			xmlAttribute = xmlDocument.CreateAttribute("namespace");
			xmlAttribute.Value = this.ModelName;
			xmlElement.Attributes.Append(xmlAttribute);
			xmlDocument.AppendChild(xmlElement);
			XmlElement xmlElement2 = xmlDocument.CreateElement("alias");
			XmlElement xmlElement3 = xmlDocument.CreateElement("typeAlias");
			XmlAttribute xmlAttribute2 = xmlDocument.CreateAttribute("alias");
			xmlAttribute2.Value = this.ModelName;
			xmlElement3.Attributes.Append(xmlAttribute2);
			xmlAttribute2 = xmlDocument.CreateAttribute("type");
			xmlAttribute2.Value = this.ModelSpace + "," + this.ModelAssembly;
			xmlElement3.Attributes.Append(xmlAttribute2);
			xmlElement2.AppendChild(xmlElement3);
			xmlElement.AppendChild(xmlElement2);
			xmlElement2 = xmlDocument.CreateElement("resultMaps");
			xmlElement3 = xmlDocument.CreateElement("resultMap");
			xmlAttribute2 = xmlDocument.CreateAttribute("id");
			xmlAttribute2.Value = "SelectAllResult";
			xmlElement3.Attributes.Append(xmlAttribute2);
			xmlAttribute2 = xmlDocument.CreateAttribute("class");
			xmlAttribute2.Value = this.ModelName;
			xmlElement3.Attributes.Append(xmlAttribute2);
			foreach (ColumnInfo current in this.Fieldlist)
			{
				string columnName = current.ColumnName;
				string arg_1CE_0 = current.TypeName;
				XmlElement xmlElement4 = xmlDocument.CreateElement("result");
				XmlAttribute xmlAttribute3 = xmlDocument.CreateAttribute("property");
				xmlAttribute3.Value = columnName;
				xmlElement4.Attributes.Append(xmlAttribute3);
				xmlAttribute3 = xmlDocument.CreateAttribute("column");
				xmlAttribute3.Value = columnName;
				xmlElement4.Attributes.Append(xmlAttribute3);
				xmlElement3.AppendChild(xmlElement4);
			}
			xmlElement2.AppendChild(xmlElement3);
			xmlElement.AppendChild(xmlElement2);
			xmlElement2 = xmlDocument.CreateElement("statements");
			xmlElement3 = xmlDocument.CreateElement("select");
			xmlAttribute2 = xmlDocument.CreateAttribute("id");
			xmlAttribute2.Value = "GetMaxID";
			xmlElement3.Attributes.Append(xmlAttribute2);
			xmlAttribute2 = xmlDocument.CreateAttribute("resultClass");
			xmlAttribute2.Value = "int";
			xmlElement3.Attributes.Append(xmlAttribute2);
			XmlText newChild2 = xmlDocument.CreateTextNode("select max(" + this._key + ") from " + this.TableName);
			xmlElement3.AppendChild(newChild2);
			xmlElement2.AppendChild(xmlElement3);
			xmlElement3 = xmlDocument.CreateElement("select");
			xmlAttribute2 = xmlDocument.CreateAttribute("id");
			xmlAttribute2.Value = "Exists";
			xmlElement3.Attributes.Append(xmlAttribute2);
			xmlAttribute2 = xmlDocument.CreateAttribute("resultClass");
			xmlAttribute2.Value = "int";
			xmlElement3.Attributes.Append(xmlAttribute2);
			xmlAttribute2 = xmlDocument.CreateAttribute("parameterclass");
			xmlAttribute2.Value = this._keyType;
			xmlElement3.Attributes.Append(xmlAttribute2);
			newChild2 = xmlDocument.CreateTextNode(string.Concat(new string[]
			{
				"select count(1) from  ",
				this.TableName,
				" where ",
				this._key,
				" = #value#"
			}));
			xmlElement3.AppendChild(newChild2);
			xmlElement2.AppendChild(xmlElement3);
			xmlElement3 = xmlDocument.CreateElement("insert");
			xmlAttribute2 = xmlDocument.CreateAttribute("id");
			xmlAttribute2.Value = "Insert" + this.ModelName;
			xmlElement3.Attributes.Append(xmlAttribute2);
			xmlAttribute2 = xmlDocument.CreateAttribute("parameterclass");
			xmlAttribute2.Value = this.ModelName;
			xmlElement3.Attributes.Append(xmlAttribute2);
			new StringBuilder();
			new StringBuilder();
			if ((this.dbobj.DbType == "SQL2000" || this.dbobj.DbType == "SQL2005" || this.dbobj.DbType == "SQL2008" || this.dbobj.DbType == "SQL2012") && this.IsHasIdentity)
			{
				XmlElement xmlElement4 = xmlDocument.CreateElement("selectKey");
				XmlAttribute xmlAttribute4 = xmlDocument.CreateAttribute("property");
				xmlAttribute4.Value = this._key;
				xmlElement4.Attributes.Append(xmlAttribute4);
				xmlAttribute4 = xmlDocument.CreateAttribute("type");
				xmlAttribute4.Value = "post";
				xmlElement4.Attributes.Append(xmlAttribute4);
				xmlAttribute4 = xmlDocument.CreateAttribute("resultClass");
				xmlAttribute4.Value = "int";
				xmlElement4.Attributes.Append(xmlAttribute4);
				newChild2 = xmlDocument.CreateTextNode("${selectKey}");
				xmlElement4.AppendChild(newChild2);
				xmlElement3.AppendChild(xmlElement4);
			}
			StringBuilder stringBuilder = new StringBuilder();
			StringPlus stringPlus = new StringPlus();
			StringPlus stringPlus2 = new StringPlus();
			stringBuilder.Append("insert into " + this.TableName + "(");
			foreach (ColumnInfo current2 in this.Fieldlist)
			{
				string columnName2 = current2.ColumnName;
				string arg_5C0_0 = current2.TypeName;
				bool arg_5C8_0 = current2.IsIdentity;
				string arg_5D0_0 = current2.Length;
				if (!current2.IsIdentity)
				{
					stringPlus.Append(columnName2 + ",");
					stringPlus2.Append("#" + columnName2 + "#,");
				}
			}
			stringPlus.DelLastComma();
			stringPlus2.DelLastComma();
			stringBuilder.Append(stringPlus.Value);
			stringBuilder.Append(") values (");
			stringBuilder.Append(stringPlus2.Value + ")");
			newChild2 = xmlDocument.CreateTextNode(stringBuilder.ToString());
			xmlElement3.AppendChild(newChild2);
			xmlElement2.AppendChild(xmlElement3);
			xmlElement3 = xmlDocument.CreateElement("delete");
			xmlAttribute2 = xmlDocument.CreateAttribute("id");
			xmlAttribute2.Value = "Delete" + this.ModelName;
			xmlElement3.Attributes.Append(xmlAttribute2);
			xmlAttribute2 = xmlDocument.CreateAttribute("parameterclass");
			xmlAttribute2.Value = this._keyType;
			xmlElement3.Attributes.Append(xmlAttribute2);
			newChild2 = xmlDocument.CreateTextNode(string.Concat(new string[]
			{
				"delete from  ",
				this.TableName,
				" where ",
				this._key,
				" = #value#"
			}));
			xmlElement3.AppendChild(newChild2);
			xmlElement2.AppendChild(xmlElement3);
			xmlElement3 = xmlDocument.CreateElement("select");
			xmlAttribute2 = xmlDocument.CreateAttribute("id");
			xmlAttribute2.Value = "SelectAll" + this.ModelName;
			xmlElement3.Attributes.Append(xmlAttribute2);
			xmlAttribute2 = xmlDocument.CreateAttribute("resultMap");
			xmlAttribute2.Value = "SelectAllResult";
			xmlElement3.Attributes.Append(xmlAttribute2);
			newChild2 = xmlDocument.CreateTextNode("select * from  " + this.TableName);
			xmlElement3.AppendChild(newChild2);
			xmlElement2.AppendChild(xmlElement3);
			xmlElement3 = xmlDocument.CreateElement("select");
			xmlAttribute2 = xmlDocument.CreateAttribute("id");
			xmlAttribute2.Value = "SelectBy" + this._key;
			xmlElement3.Attributes.Append(xmlAttribute2);
			xmlAttribute2 = xmlDocument.CreateAttribute("resultMap");
			xmlAttribute2.Value = "SelectAllResult";
			xmlElement3.Attributes.Append(xmlAttribute2);
			xmlAttribute2 = xmlDocument.CreateAttribute("resultClass");
			xmlAttribute2.Value = this.ModelName;
			xmlElement3.Attributes.Append(xmlAttribute2);
			xmlAttribute2 = xmlDocument.CreateAttribute("parameterclass");
			xmlAttribute2.Value = this._keyType;
			xmlElement3.Attributes.Append(xmlAttribute2);
			newChild2 = xmlDocument.CreateTextNode(string.Concat(new string[]
			{
				"select * from ",
				this.TableName,
				" where ",
				this._key,
				" = #value#"
			}));
			xmlElement3.AppendChild(newChild2);
			xmlElement2.AppendChild(xmlElement3);
			xmlElement.AppendChild(xmlElement2);
			return xmlDocument.OuterXml;
		}
	}
}
