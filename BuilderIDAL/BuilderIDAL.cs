using Maticsoft.CodeHelper;
using Maticsoft.IBuilder;
using Maticsoft.Utility;
using System;
using System.Collections.Generic;
namespace Maticsoft.BuilderIDAL
{
	public class BuilderIDAL : IBuilderIDAL
	{
		protected string _IdentityKey = "";
		protected string _IdentityKeyType = "int";
		private string dbType;
		private string _modelname;
		private List<ColumnInfo> _fieldlist;
		private List<ColumnInfo> _keys;
		private string _namespace;
		private string _folder;
		private string _modelpath;
		private string _idalpath;
		private string _iclass;
		protected string _tabledescription = "";
		private bool isHasIdentity;
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
		public bool IsHasIdentity
		{
			get
			{
				return this.isHasIdentity;
			}
			set
			{
				this.isHasIdentity = value;
			}
		}
		public string GetIDALCode(bool Maxid, bool Exists, bool Add, bool Update, bool Delete, bool GetModel, bool List)
		{
			StringPlus stringPlus = new StringPlus();
			stringPlus.AppendLine("using System;");
			stringPlus.AppendLine("using System.Data;");
			stringPlus.AppendLine("namespace " + this.IDALpath);
			stringPlus.AppendLine("{");
			stringPlus.AppendSpaceLine(1, "/// <summary>");
			stringPlus.AppendSpaceLine(1, "/// 接口层" + this.TableDescription);
			stringPlus.AppendSpaceLine(1, "/// </summary>");
			stringPlus.AppendSpaceLine(1, "public interface " + this.IClass);
			stringPlus.AppendSpaceLine(1, "{");
			stringPlus.AppendSpaceLine(2, "#region  成员方法");
			if (Maxid)
			{
				stringPlus.Append(this.CreatGetMaxID());
			}
			if (Exists)
			{
				stringPlus.Append(this.CreatExists());
			}
			if (Add)
			{
				stringPlus.Append(this.CreatAdd());
			}
			if (Update)
			{
				stringPlus.Append(this.CreatUpdate());
			}
			if (Delete)
			{
				stringPlus.Append(this.CreatDelete());
			}
			if (GetModel)
			{
				stringPlus.Append(this.CreatGetModel());
			}
			if (List)
			{
				stringPlus.Append(this.CreatGetList());
			}
			stringPlus.AppendSpaceLine(2, "#endregion  成员方法");
			stringPlus.AppendSpaceLine(2, "#region  MethodEx");
			stringPlus.AppendLine("");
			stringPlus.AppendSpaceLine(2, "#endregion  MethodEx");
			stringPlus.AppendLine("\t} ");
			stringPlus.AppendLine("}");
			return stringPlus.ToString();
		}
		public string CreatGetMaxID()
		{
			StringPlus stringPlus = new StringPlus();
			if (this.Keys.Count > 0)
			{
				foreach (ColumnInfo current in this.Keys)
				{
					if (CodeCommon.DbTypeToCS(current.TypeName) == "int" && current.IsPrimaryKey)
					{
						stringPlus.AppendSpaceLine(2, "/// <summary>");
						stringPlus.AppendSpaceLine(2, "/// 得到最大ID");
						stringPlus.AppendSpaceLine(2, "/// </summary>");
						stringPlus.AppendLine("\t\tint GetMaxId();");
						break;
					}
				}
			}
			return stringPlus.ToString();
		}
		public string CreatExists()
		{
			StringPlus stringPlus = new StringPlus();
			if (this.Keys.Count > 0)
			{
				string inParameter = CodeCommon.GetInParameter(this.Keys, false);
				if (!string.IsNullOrEmpty(inParameter))
				{
					stringPlus.AppendSpaceLine(2, "/// <summary>");
					stringPlus.AppendSpaceLine(2, "/// 是否存在该记录");
					stringPlus.AppendSpaceLine(2, "/// </summary>");
					stringPlus.AppendSpaceLine(2, "bool Exists(" + inParameter + ");");
				}
			}
			return stringPlus.ToString();
		}
		public string CreatAdd()
		{
			StringPlus stringPlus = new StringPlus();
			stringPlus.AppendSpaceLine(2, "/// <summary>");
			stringPlus.AppendSpaceLine(2, "/// 增加一条数据");
			stringPlus.AppendSpaceLine(2, "/// </summary>");
			string str = "bool";
			if ((this.DbType == "SQL2000" || this.DbType == "SQL2005" || this.DbType == "SQL2008" || this.DbType == "SQL2012" || this.DbType == "SQLite") && this.IsHasIdentity)
			{
				str = "int";
				if (this._IdentityKeyType != "int")
				{
					str = this._IdentityKeyType;
				}
			}
			stringPlus.AppendSpaceLine(2, str + " Add(" + this.ModelSpace + " model);");
			return stringPlus.ToString();
		}
		public string CreatUpdate()
		{
			StringPlus stringPlus = new StringPlus();
			stringPlus.AppendSpaceLine(2, "/// <summary>");
			stringPlus.AppendSpaceLine(2, "/// 更新一条数据");
			stringPlus.AppendSpaceLine(2, "/// </summary>");
			stringPlus.AppendSpaceLine(2, "bool Update(" + this.ModelSpace + " model);");
			return stringPlus.ToString();
		}
		public string CreatDelete()
		{
			StringPlus stringPlus = new StringPlus();
			stringPlus.AppendSpaceLine(2, "/// <summary>");
			stringPlus.AppendSpaceLine(2, "/// 删除一条数据");
			stringPlus.AppendSpaceLine(2, "/// </summary>");
			stringPlus.AppendSpaceLine(2, "bool Delete(" + CodeCommon.GetInParameter(this.Keys, true) + ");");
			if (CodeCommon.HasNoIdentityKey(this.Keys) && CodeCommon.GetIdentityKey(this.Keys) != null)
			{
				stringPlus.AppendSpaceLine(2, "/// <summary>");
				stringPlus.AppendSpaceLine(2, "/// 删除一条数据");
				stringPlus.AppendSpaceLine(2, "/// </summary>");
				stringPlus.AppendSpaceLine(2, "bool Delete(" + CodeCommon.GetInParameter(this.Keys, false) + ");");
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
				stringPlus.AppendSpaceLine(2, "bool DeleteList(string " + text + "list );");
			}
			return stringPlus.ToString();
		}
		public string CreatGetModel()
		{
			StringPlus stringPlus = new StringPlus();
			stringPlus.AppendSpaceLine(2, "/// <summary>");
			stringPlus.AppendSpaceLine(2, "/// 得到一个对象实体");
			stringPlus.AppendSpaceLine(2, "/// </summary>");
			stringPlus.AppendSpaceLine(2, this.ModelSpace + " GetModel(" + CodeCommon.GetInParameter(this.Keys, true) + ");");
			stringPlus.AppendSpaceLine(2, this.ModelSpace + " DataRowToModel(DataRow row);");
			return stringPlus.ToString();
		}
		public string CreatGetList()
		{
			StringPlus stringPlus = new StringPlus();
			stringPlus.AppendSpaceLine(2, "/// <summary>");
			stringPlus.AppendSpaceLine(2, "/// 获得数据列表");
			stringPlus.AppendSpaceLine(2, "/// </summary>");
			stringPlus.AppendSpaceLine(2, "DataSet GetList(string strWhere);");
			if (this.DbType == "SQL2000" || this.DbType == "SQL2005" || this.DbType == "SQL2008" || this.DbType == "SQL2012")
			{
				stringPlus.AppendSpaceLine(2, "/// <summary>");
				stringPlus.AppendSpaceLine(2, "/// 获得前几行数据");
				stringPlus.AppendSpaceLine(2, "/// </summary>");
				stringPlus.AppendSpaceLine(2, "DataSet GetList(int Top,string strWhere,string filedOrder);");
				stringPlus.AppendSpaceLine(2, "int GetRecordCount(string strWhere);");
				stringPlus.AppendSpaceLine(2, "DataSet GetListByPage(string strWhere, string orderby, int startIndex, int endIndex);");
			}
			stringPlus.AppendSpaceLine(2, "/// <summary>");
			stringPlus.AppendSpaceLine(2, "/// 根据分页获得数据列表");
			stringPlus.AppendSpaceLine(2, "/// </summary>");
			stringPlus.AppendSpaceLine(2, "//DataSet GetList(int PageSize,int PageIndex,string strWhere);");
			return stringPlus.ToString();
		}
	}
}
