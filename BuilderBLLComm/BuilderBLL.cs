using Maticsoft.CodeHelper;
using Maticsoft.IBuilder;
using Maticsoft.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
namespace Maticsoft.BuilderBLLComm
{
	public class BuilderBLL : IBuilderBLL
	{
		protected string _key = "ID";
		protected string _keyType = "int";
		private List<ColumnInfo> _fieldlist;
		private List<ColumnInfo> _keys;
		private string _namespace;
		private string _folder;
		private string _modelspace;
		private string _modelname;
		protected string _tabledescription = "";
		private string _bllname;
		private string _dalname;
		private string _modelpath;
		private string _bllpath;
		private string _factorypath;
		private string _idalpath;
		private string _iclass;
		private string _dalpath;
		private string _dalspace;
		private bool isHasIdentity;
		private string dbType;
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
		public string TableDescription
		{
			get
			{
				return this._tabledescription;
			}
			set
			{
				this._tabledescription = value;
			}
		}
		public string BLLpath
		{
			get
			{
				return this._bllpath;
			}
			set
			{
				this._bllpath = value;
			}
		}
		public string BLLName
		{
			get
			{
				return this._bllname;
			}
			set
			{
				this._bllname = value;
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
		public string DALSpace
		{
			get
			{
				return this.DALpath + "." + this.DALName;
			}
		}
		public string Factorypath
		{
			get
			{
				return this._factorypath;
			}
			set
			{
				this._factorypath = value;
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
			set
			{
				this.isHasIdentity = value;
			}
		}
		public string DbType
		{
			get
			{
				return this.dbType;
			}
			set
			{
				this.dbType = value;
			}
		}
		public string Key
		{
			get
			{
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
				return this._key;
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
				return Language.LoadFromCfg("BuilderBLLComm.lan");
			}
		}
		public BuilderBLL()
		{
		}
		public BuilderBLL(List<ColumnInfo> keys, string modelspace)
		{
			this._modelspace = modelspace;
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
		public string GetBLLCode(bool Maxid, bool Exists, bool Add, bool Update, bool Delete, bool GetModel, bool GetModelByCache, bool List)
		{
			StringPlus stringPlus = new StringPlus();
			stringPlus.AppendLine("using System;");
			stringPlus.AppendLine("using System.Data;");
			stringPlus.AppendLine("using System.Collections.Generic;");
			if (GetModelByCache)
			{
				stringPlus.AppendLine("using Maticsoft.Common;");
			}
			stringPlus.AppendLine("using " + this.Modelpath + ";");
			if (this.Factorypath != "" && this.Factorypath != null)
			{
				stringPlus.AppendLine("using " + this.Factorypath + ";");
			}
			if (this.IDALpath != "" && this.IDALpath != null)
			{
				stringPlus.AppendLine("using " + this.IDALpath + ";");
			}
			stringPlus.AppendLine("namespace " + this.BLLpath);
			stringPlus.AppendLine("{");
			stringPlus.AppendSpaceLine(1, "/// <summary>");
			if (this.TableDescription.Length > 0)
			{
				stringPlus.AppendSpaceLine(1, "/// " + this.TableDescription.Replace("\r\n", "\r\n\t///"));
			}
			else
			{
				stringPlus.AppendSpaceLine(1, "/// " + this.BLLName + ":" + this.Languagelist["summary"].ToString());
			}
			stringPlus.AppendSpaceLine(1, "/// </summary>");
			stringPlus.AppendSpaceLine(1, "public partial class " + this.BLLName);
			stringPlus.AppendSpaceLine(1, "{");
			if (this.IClass != "" && this.IClass != null)
			{
				stringPlus.AppendSpaceLine(2, string.Concat(new string[]
				{
					"private readonly ",
					this.IClass,
					" dal=DataAccess.Create",
					this.DALName,
					"();"
				}));
			}
			else
			{
				stringPlus.AppendSpaceLine(2, string.Concat(new string[]
				{
					"private readonly ",
					this.DALSpace,
					" dal=new ",
					this.DALSpace,
					"();"
				}));
			}
			stringPlus.AppendSpaceLine(2, "public " + this.BLLName + "()");
			stringPlus.AppendSpaceLine(2, "{}");
			stringPlus.AppendSpaceLine(2, "#region  BasicMethod");
			if (Maxid && this.Keys.Count > 0)
			{
				foreach (ColumnInfo current in this.Keys)
				{
					if (CodeCommon.DbTypeToCS(current.TypeName) == "int" && current.IsPrimaryKey)
					{
						stringPlus.AppendLine(this.CreatBLLGetMaxID());
						break;
					}
				}
			}
			if (Exists)
			{
				stringPlus.AppendLine(this.CreatBLLExists());
			}
			if (Add)
			{
				stringPlus.AppendLine(this.CreatBLLADD());
			}
			if (Update)
			{
				stringPlus.AppendLine(this.CreatBLLUpdate());
			}
			if (Delete)
			{
				stringPlus.AppendLine(this.CreatBLLDelete());
			}
			if (GetModel)
			{
				stringPlus.AppendLine(this.CreatBLLGetModel());
			}
			if (GetModelByCache)
			{
				stringPlus.AppendLine(this.CreatBLLGetModelByCache(this.ModelName));
			}
			if (List)
			{
				stringPlus.AppendLine(this.CreatBLLGetList());
				stringPlus.AppendLine(this.CreatBLLGetAllList());
				stringPlus.AppendLine(this.CreatBLLGetListByPage());
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
		public string CreatBLLGetMaxID()
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
							stringPlus.AppendSpaceLine(2, "public int GetMaxId()");
							stringPlus.AppendSpaceLine(2, "{");
							stringPlus.AppendSpaceLine(3, "return dal.GetMaxId();");
							stringPlus.AppendSpaceLine(2, "}");
							break;
						}
					}
				}
			}
			return stringPlus.ToString();
		}
		public string CreatBLLExists()
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
					stringPlus.AppendSpaceLine(3, "return dal.Exists(" + CodeCommon.GetFieldstrlist(this.Keys, false) + ");");
					stringPlus.AppendSpaceLine(2, "}");
				}
			}
			return stringPlus.ToString();
		}
		public string CreatBLLADD()
		{
			StringPlus stringPlus = new StringPlus();
			stringPlus.AppendSpaceLine(2, "/// <summary>");
			stringPlus.AppendSpaceLine(2, "/// " + this.Languagelist["summaryadd"].ToString());
			stringPlus.AppendSpaceLine(2, "/// </summary>");
			string text = "bool";
			if ((this.DbType == "SQL2000" || this.DbType == "SQL2005" || this.DbType == "SQL2008" || this.DbType == "SQL2012" || this.DbType == "SQLite") && this.IsHasIdentity)
			{
				text = "int ";
				if (this._keyType != "int")
				{
					text = this._keyType;
				}
			}
			stringPlus.AppendSpaceLine(2, string.Concat(new string[]
			{
				"public ",
				text,
				" Add(",
				this.ModelSpace,
				" model)"
			}));
			stringPlus.AppendSpaceLine(2, "{");
			if (text == "void")
			{
				stringPlus.AppendSpaceLine(3, "dal.Add(model);");
			}
			else
			{
				stringPlus.AppendSpaceLine(3, "return dal.Add(model);");
			}
			stringPlus.AppendSpaceLine(2, "}");
			return stringPlus.ToString();
		}
		public string CreatBLLUpdate()
		{
			StringPlus stringPlus = new StringPlus();
			stringPlus.AppendSpaceLine(2, "/// <summary>");
			stringPlus.AppendSpaceLine(2, "/// " + this.Languagelist["summaryUpdate"].ToString());
			stringPlus.AppendSpaceLine(2, "/// </summary>");
			stringPlus.AppendSpaceLine(2, "public bool Update(" + this.ModelSpace + " model)");
			stringPlus.AppendSpaceLine(2, "{");
			stringPlus.AppendSpaceLine(3, "return dal.Update(model);");
			stringPlus.AppendSpaceLine(2, "}");
			return stringPlus.ToString();
		}
		public string CreatBLLDelete()
		{
			StringPlus stringPlus = new StringPlus();
			stringPlus.AppendSpaceLine(2, "/// <summary>");
			stringPlus.AppendSpaceLine(2, "/// " + this.Languagelist["summaryDelete"].ToString());
			stringPlus.AppendSpaceLine(2, "/// </summary>");
			stringPlus.AppendSpaceLine(2, "public bool Delete(" + CodeCommon.GetInParameter(this.Keys, true) + ")");
			stringPlus.AppendSpaceLine(2, "{");
			stringPlus.AppendSpaceLine(3, this.KeysNullTip);
			stringPlus.AppendSpaceLine(3, "return dal.Delete(" + CodeCommon.GetFieldstrlist(this.Keys, true) + ");");
			stringPlus.AppendSpaceLine(2, "}");
			if (CodeCommon.HasNoIdentityKey(this.Keys) && CodeCommon.GetIdentityKey(this.Keys) != null)
			{
				stringPlus.AppendSpaceLine(2, "/// <summary>");
				stringPlus.AppendSpaceLine(2, "/// " + this.Languagelist["summaryDelete"].ToString());
				stringPlus.AppendSpaceLine(2, "/// </summary>");
				stringPlus.AppendSpaceLine(2, "public bool Delete(" + CodeCommon.GetInParameter(this.Keys, false) + ")");
				stringPlus.AppendSpaceLine(2, "{");
				stringPlus.AppendSpaceLine(3, this.KeysNullTip);
				stringPlus.AppendSpaceLine(3, "return dal.Delete(" + CodeCommon.GetFieldstrlist(this.Keys, false) + ");");
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
				stringPlus.AppendSpaceLine(2, "/// " + this.Languagelist["summaryDelete"].ToString());
				stringPlus.AppendSpaceLine(2, "/// </summary>");
				stringPlus.AppendSpaceLine(2, "public bool DeleteList(string " + text + "list )");
				stringPlus.AppendSpaceLine(2, "{");
				stringPlus.AppendSpaceLine(3, "return dal.DeleteList(Maticsoft.Common.PageValidate.SafeLongFilter(" + text + "list,0) );");
				stringPlus.AppendSpaceLine(2, "}");
			}
			return stringPlus.ToString();
		}
		public string CreatBLLGetModel()
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
			stringPlus.AppendSpaceLine(3, this.KeysNullTip);
			stringPlus.AppendSpaceLine(3, "return dal.GetModel(" + CodeCommon.GetFieldstrlist(this.Keys, true) + ");");
			stringPlus.AppendSpaceLine(2, "}");
			return stringPlus.ToString();
		}
		public string CreatBLLGetModelByCache(string ModelName)
		{
			StringPlus stringPlus = new StringPlus();
			stringPlus.AppendSpaceLine(2, "/// <summary>");
			stringPlus.AppendSpaceLine(2, "/// " + this.Languagelist["summaryGetModelByCache"].ToString());
			stringPlus.AppendSpaceLine(2, "/// </summary>");
			stringPlus.AppendSpaceLine(2, string.Concat(new string[]
			{
				"public ",
				this.ModelSpace,
				" GetModelByCache(",
				CodeCommon.GetInParameter(this.Keys, true),
				")"
			}));
			stringPlus.AppendSpaceLine(2, "{");
			stringPlus.AppendSpaceLine(3, this.KeysNullTip);
			string text = "";
			if (this.Keys.Count > 0)
			{
				text = "+ " + CodeCommon.GetFieldstrlistAdd(this.Keys, true);
			}
			stringPlus.AppendSpaceLine(3, string.Concat(new string[]
			{
				"string CacheKey = \"",
				ModelName,
				"Model-\" ",
				text,
				";"
			}));
			stringPlus.AppendSpaceLine(3, "object objModel = Maticsoft.Common.DataCache.GetCache(CacheKey);");
			stringPlus.AppendSpaceLine(3, "if (objModel == null)");
			stringPlus.AppendSpaceLine(3, "{");
			stringPlus.AppendSpaceLine(4, "try");
			stringPlus.AppendSpaceLine(4, "{");
			stringPlus.AppendSpaceLine(5, "objModel = dal.GetModel(" + CodeCommon.GetFieldstrlist(this.Keys, true) + ");");
			stringPlus.AppendSpaceLine(5, "if (objModel != null)");
			stringPlus.AppendSpaceLine(5, "{");
			stringPlus.AppendSpaceLine(6, "int ModelCache = Maticsoft.Common.ConfigHelper.GetConfigInt(\"ModelCache\");");
			stringPlus.AppendSpaceLine(6, "Maticsoft.Common.DataCache.SetCache(CacheKey, objModel, DateTime.Now.AddMinutes(ModelCache), TimeSpan.Zero);");
			stringPlus.AppendSpaceLine(5, "}");
			stringPlus.AppendSpaceLine(4, "}");
			stringPlus.AppendSpaceLine(4, "catch{}");
			stringPlus.AppendSpaceLine(3, "}");
			stringPlus.AppendSpaceLine(3, "return (" + this.ModelSpace + ")objModel;");
			stringPlus.AppendSpaceLine(2, "}");
			return stringPlus.Value;
		}
		public string CreatBLLGetList()
		{
			StringPlus stringPlus = new StringPlus();
			stringPlus.AppendSpaceLine(2, "/// <summary>");
			stringPlus.AppendSpaceLine(2, "/// " + this.Languagelist["summaryGetList"].ToString());
			stringPlus.AppendSpaceLine(2, "/// </summary>");
			stringPlus.AppendSpaceLine(2, "public DataSet GetList(string strWhere)");
			stringPlus.AppendSpaceLine(2, "{");
			stringPlus.AppendSpaceLine(3, "return dal.GetList(strWhere);");
			stringPlus.AppendSpaceLine(2, "}");
			string text = "";
			foreach (ColumnInfo current in this.Fieldlist)
			{
				string arg_9D_0 = current.ColumnName;
				string arg_A4_0 = current.TypeName;
				string arg_AB_0 = current.Description;
				bool arg_B2_0 = current.IsPrimaryKey;
				bool arg_B9_0 = current.IsIdentity;
			}
			if (text.Length > 1)
			{
				stringPlus.AppendSpaceLine(2, "/// <summary>");
				stringPlus.AppendSpaceLine(2, "/// " + this.Languagelist["summaryGetList"].ToString());
				stringPlus.AppendSpaceLine(2, "/// </summary>");
				stringPlus.AppendSpaceLine(2, "public DataSet GetList(string strWhere" + text + ")");
				stringPlus.AppendSpaceLine(2, "{");
				stringPlus.AppendSpaceLine(3, "return dal.GetList(strWhere);");
				stringPlus.AppendSpaceLine(2, "}");
			}
			if (this.DbType == "SQL2000" || this.DbType == "SQL2005" || this.DbType == "SQL2008" || this.DbType == "SQL2012")
			{
				stringPlus.AppendSpaceLine(2, "/// <summary>");
				stringPlus.AppendSpaceLine(2, "/// " + this.Languagelist["summaryGetList2"].ToString());
				stringPlus.AppendSpaceLine(2, "/// </summary>");
				stringPlus.AppendSpaceLine(2, "public DataSet GetList(int Top,string strWhere,string filedOrder)");
				stringPlus.AppendSpaceLine(2, "{");
				stringPlus.AppendSpaceLine(3, "return dal.GetList(Top,strWhere,filedOrder);");
				stringPlus.AppendSpaceLine(2, "}");
			}
			stringPlus.AppendSpaceLine(2, "/// <summary>");
			stringPlus.AppendSpaceLine(2, "/// " + this.Languagelist["summaryGetList"].ToString());
			stringPlus.AppendSpaceLine(2, "/// </summary>");
			stringPlus.AppendSpaceLine(2, "public List<" + this.ModelSpace + "> GetModelList(string strWhere)");
			stringPlus.AppendSpaceLine(2, "{");
			stringPlus.AppendSpaceLine(3, "DataSet ds = dal.GetList(strWhere);");
			stringPlus.AppendSpaceLine(3, "return DataTableToList(ds.Tables[0]);");
			stringPlus.AppendSpaceLine(2, "}");
			stringPlus.AppendSpaceLine(2, "/// <summary>");
			stringPlus.AppendSpaceLine(2, "/// " + this.Languagelist["summaryGetList"].ToString());
			stringPlus.AppendSpaceLine(2, "/// </summary>");
			stringPlus.AppendSpaceLine(2, "public List<" + this.ModelSpace + "> DataTableToList(DataTable dt)");
			stringPlus.AppendSpaceLine(2, "{");
			stringPlus.AppendSpaceLine(3, string.Concat(new string[]
			{
				"List<",
				this.ModelSpace,
				"> modelList = new List<",
				this.ModelSpace,
				">();"
			}));
			stringPlus.AppendSpaceLine(3, "int rowsCount = dt.Rows.Count;");
			stringPlus.AppendSpaceLine(3, "if (rowsCount > 0)");
			stringPlus.AppendSpaceLine(3, "{");
			stringPlus.AppendSpaceLine(4, this.ModelSpace + " model;");
			stringPlus.AppendSpaceLine(4, "for (int n = 0; n < rowsCount; n++)");
			stringPlus.AppendSpaceLine(4, "{");
			stringPlus.AppendSpaceLine(5, "model = dal.DataRowToModel(dt.Rows[n]);");
			stringPlus.AppendSpaceLine(5, "if (model != null)");
			stringPlus.AppendSpaceLine(5, "{");
			stringPlus.AppendSpaceLine(6, "modelList.Add(model);");
			stringPlus.AppendSpaceLine(5, "}");
			stringPlus.AppendSpaceLine(4, "}");
			stringPlus.AppendSpaceLine(3, "}");
			stringPlus.AppendSpaceLine(3, "return modelList;");
			stringPlus.AppendSpaceLine(2, "}");
			return stringPlus.ToString();
		}
		public string CreatBLLGetAllList()
		{
			StringPlus stringPlus = new StringPlus();
			stringPlus.AppendSpaceLine(2, "/// <summary>");
			stringPlus.AppendSpaceLine(2, "/// " + this.Languagelist["summaryGetList"].ToString());
			stringPlus.AppendSpaceLine(2, "/// </summary>");
			stringPlus.AppendSpaceLine(2, "public DataSet GetAllList()");
			stringPlus.AppendSpaceLine(2, "{");
			stringPlus.AppendSpaceLine(3, "return GetList(\"\");");
			stringPlus.AppendSpaceLine(2, "}");
			return stringPlus.ToString();
		}
		public string CreatBLLGetListByPage()
		{
			StringPlus stringPlus = new StringPlus();
			stringPlus.AppendSpaceLine(2, "/// <summary>");
			stringPlus.AppendSpaceLine(2, "/// " + this.Languagelist["summaryGetList3"].ToString());
			stringPlus.AppendSpaceLine(2, "/// </summary>");
			stringPlus.AppendSpaceLine(2, "public int GetRecordCount(string strWhere)");
			stringPlus.AppendSpaceLine(2, "{");
			stringPlus.AppendSpaceLine(3, "return dal.GetRecordCount(strWhere);");
			stringPlus.AppendSpaceLine(2, "}");
			stringPlus.AppendSpaceLine(2, "/// <summary>");
			stringPlus.AppendSpaceLine(2, "/// " + this.Languagelist["summaryGetList3"].ToString());
			stringPlus.AppendSpaceLine(2, "/// </summary>");
			stringPlus.AppendSpaceLine(2, "public DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex)");
			stringPlus.AppendSpaceLine(2, "{");
			stringPlus.AppendSpaceLine(3, "return dal.GetListByPage( strWhere,  orderby,  startIndex,  endIndex);");
			stringPlus.AppendSpaceLine(2, "}");
			stringPlus.AppendSpaceLine(2, "/// <summary>");
			stringPlus.AppendSpaceLine(2, "/// " + this.Languagelist["summaryGetList3"].ToString());
			stringPlus.AppendSpaceLine(2, "/// </summary>");
			stringPlus.AppendSpaceLine(2, "//public DataSet GetList(int PageSize,int PageIndex,string strWhere)");
			stringPlus.AppendSpaceLine(2, "//{");
			stringPlus.AppendSpaceLine(3, "//return dal.GetList(PageSize,PageIndex,strWhere);");
			stringPlus.AppendSpaceLine(2, "//}");
			return stringPlus.ToString();
		}
	}
}
