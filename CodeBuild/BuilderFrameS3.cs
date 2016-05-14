using Maticsoft.BuilderModel;
using Maticsoft.CodeHelper;
using Maticsoft.IBuilder;
using Maticsoft.IDBO;
using Maticsoft.Utility;
using System;
using System.Collections.Generic;
namespace Maticsoft.CodeBuild
{
	public class BuilderFrameS3 : BuilderFrame
	{
		private IBuilderDAL idal;
		private IBuilderDALTran idaltran;
		private IBuilderDALMTran idalmtran;
		private IBuilderBLL ibll;
		public new string DALpath
		{
			get
			{
				string text = base.NameSpace + ".DAL";
				if (base.Folder.Trim() != "")
				{
					text = text + "." + base.Folder;
				}
				return text;
			}
		}
		public new string DALSpace
		{
			get
			{
				return this.DALpath + "." + base.DALName;
			}
		}
		public BuilderFrameS3(IDbObject idbobj, string dbName, string tableName, string tableDescription, string modelName, string bllName, string dalName, List<ColumnInfo> fieldlist, List<ColumnInfo> keys, string nameSpace, string folder, string dbHelperName)
		{
			this.dbobj = idbobj;
			this._dbtype = idbobj.DbType;
			base.DbName = dbName;
			base.TableName = tableName;
			base.TableDescription = tableDescription;
			base.ModelName = modelName;
			base.BLLName = bllName;
			base.DALName = dalName;
			base.NameSpace = nameSpace;
			base.DbHelperName = dbHelperName;
			base.Folder = folder;
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
		public BuilderFrameS3(IDbObject idbobj, string dbName, string nameSpace, string folder, string dbHelperName)
		{
			this.dbobj = idbobj;
			this._dbtype = idbobj.DbType;
			base.DbName = dbName;
			base.NameSpace = nameSpace;
			base.DbHelperName = dbHelperName;
			base.Folder = folder;
		}
		public string GetModelCode()
		{
			return new Maticsoft.BuilderModel.BuilderModel
			{
				ModelName = base.ModelName,
				NameSpace = base.NameSpace,
				Fieldlist = base.Fieldlist,
				Modelpath = base.Modelpath,
				TableDescription = base.TableDescription
			}.CreatModel();
		}
		public string GetModelCode(string tableNameParent, string modelNameParent, List<ColumnInfo> FieldlistP, string tableNameSon, string modelNameSon, List<ColumnInfo> FieldlistS)
		{
			if (modelNameParent == "")
			{
				modelNameParent = tableNameParent;
			}
			if (modelNameSon == "")
			{
				modelNameSon = tableNameSon;
			}
			StringPlus stringPlus = new StringPlus();
			new StringPlus();
			new StringPlus();
			stringPlus.AppendLine("using System;");
			stringPlus.AppendLine("using System.Collections.Generic;");
			stringPlus.AppendLine("namespace " + base.Modelpath);
			stringPlus.AppendLine("{");
			BuilderModelT builderModelT = new BuilderModelT();
			builderModelT.ModelName = modelNameParent;
			builderModelT.NameSpace = base.NameSpace;
			builderModelT.Fieldlist = FieldlistP;
			builderModelT.Modelpath = base.Modelpath;
			builderModelT.ModelNameSon = modelNameSon;
			stringPlus.AppendSpaceLine(1, "/// <summary>");
			stringPlus.AppendSpaceLine(1, "/// 实体类" + modelNameParent + " 。(属性说明自动提取数据库字段的描述信息)");
			stringPlus.AppendSpaceLine(1, "/// </summary>");
			stringPlus.AppendSpaceLine(1, "[Serializable]");
			stringPlus.AppendSpaceLine(1, "public class " + modelNameParent);
			stringPlus.AppendSpaceLine(1, "{");
			stringPlus.AppendSpaceLine(2, "public " + modelNameParent + "()");
			stringPlus.AppendSpaceLine(2, "{}");
			stringPlus.AppendLine(builderModelT.CreatModelMethodT());
			stringPlus.AppendSpaceLine(1, "}");
            Maticsoft.BuilderModel.BuilderModel builderModel = new Maticsoft.BuilderModel.BuilderModel();
			builderModel.ModelName = modelNameSon;
			builderModel.NameSpace = base.NameSpace;
			builderModel.Fieldlist = FieldlistS;
			builderModel.Modelpath = base.Modelpath;
			stringPlus.AppendSpaceLine(1, "/// <summary>");
			stringPlus.AppendSpaceLine(1, "/// 实体类" + modelNameSon + " 。(属性说明自动提取数据库字段的描述信息)");
			stringPlus.AppendSpaceLine(1, "/// </summary>");
			stringPlus.AppendSpaceLine(1, "[Serializable]");
			stringPlus.AppendSpaceLine(1, "public class " + modelNameSon);
			stringPlus.AppendSpaceLine(1, "{");
			stringPlus.AppendSpaceLine(2, "public " + modelNameSon + "()");
			stringPlus.AppendSpaceLine(2, "{}");
			stringPlus.AppendLine(builderModel.CreatModelMethod());
			stringPlus.AppendSpaceLine(1, "}");
			stringPlus.AppendLine("}");
			stringPlus.AppendLine("");
			return stringPlus.ToString();
		}
		public string GetDALCode(string AssemblyGuid, bool Maxid, bool Exists, bool Add, bool Update, bool Delete, bool GetModel, bool List, string procPrefix)
		{
			this.idal = BuilderFactory.CreateDALObj(AssemblyGuid);
			if (this.idal == null)
			{
				return "//请选择有效的数据层代码组件类型！";
			}
			this.idal.DbObject = this.dbobj;
			this.idal.DbName = base.DbName;
			this.idal.TableName = base.TableName;
			this.idal.Fieldlist = base.Fieldlist;
			this.idal.Keys = base.Keys;
			this.idal.NameSpace = base.NameSpace;
			this.idal.Folder = base.Folder;
			this.idal.Modelpath = base.Modelpath;
			this.idal.ModelName = base.ModelName;
			this.idal.DALpath = this.DALpath;
			this.idal.DALName = base.DALName;
			this.idal.IDALpath = "";
			this.idal.IClass = "";
			this.idal.DbHelperName = base.DbHelperName;
			this.idal.ProcPrefix = procPrefix;
			return this.idal.GetDALCode(Maxid, Exists, Add, Update, Delete, GetModel, List);
		}
		public string GetDALCodeTran(string AssemblyGuid, bool Maxid, bool Exists, bool Add, bool Update, bool Delete, bool GetModel, bool List, string procPrefix, string tableNameParent, string tableNameSon, string modelNameParent, string modelNameSon, List<ColumnInfo> fieldlistParent, List<ColumnInfo> fieldlistSon, List<ColumnInfo> keysParent, List<ColumnInfo> keysSon, string DALNameParent, string DALNameSon)
		{
			this.idaltran = BuilderFactory.CreateDALTranObj(AssemblyGuid);
			if (this.idaltran == null)
			{
				return "//请选择有效的数据层代码组件类型！";
			}
			this.idaltran.DbObject = this.dbobj;
			this.idaltran.DbName = base.DbName;
			this.idaltran.TableNameParent = tableNameParent;
			this.idaltran.TableNameSon = tableNameSon;
			this.idaltran.FieldlistParent = fieldlistParent;
			this.idaltran.FieldlistSon = fieldlistSon;
			this.idaltran.KeysParent = keysParent;
			this.idaltran.KeysSon = keysSon;
			this.idaltran.NameSpace = base.NameSpace;
			this.idaltran.Folder = base.Folder;
			this.idaltran.Modelpath = base.Modelpath;
			this.idaltran.ModelNameParent = modelNameParent;
			this.idaltran.ModelNameSon = modelNameSon;
			this.idaltran.DALpath = this.DALpath;
			this.idaltran.DALNameParent = DALNameParent;
			this.idaltran.DALNameSon = DALNameSon;
			this.idaltran.IDALpath = "";
			this.idaltran.IClass = "";
			this.idaltran.DbHelperName = base.DbHelperName;
			this.idaltran.ProcPrefix = procPrefix;
			return this.idaltran.GetDALCode(Maxid, Exists, Add, Update, Delete, GetModel, List);
		}
		public string GetDALCodeMTran(string AssemblyGuid, List<ModelTran> modelTranlist)
		{
			this.idalmtran = BuilderFactory.CreateDALMTranObj(AssemblyGuid);
			if (this.idalmtran == null)
			{
				return "//请选择有效的数据层代码组件类型！";
			}
			this.idalmtran.DbObject = this.dbobj;
			this.idalmtran.DbName = base.DbName;
			this.idalmtran.NameSpace = base.NameSpace;
			this.idalmtran.Folder = base.Folder;
			this.idalmtran.DbHelperName = base.DbHelperName;
			this.idalmtran.Modelpath = base.Modelpath;
			this.idalmtran.DALpath = this.DALpath;
			this.idalmtran.ModelTranList = modelTranlist;
			this.idalmtran.IDALpath = "";
			this.idalmtran.IClass = "";
			return this.idalmtran.GetDALCode();
		}
		public string GetBLLCode(string AssemblyGuid, bool Maxid, bool Exists, bool Add, bool Update, bool Delete, bool GetModel, bool GetModelByCache, bool List)
		{
			this.ibll = BuilderFactory.CreateBLLObj(AssemblyGuid);
			if (this.ibll == null)
			{
				return "//请选择有效的业务层代码组件类型！";
			}
			this.ibll.Fieldlist = base.Fieldlist;
			this.ibll.Keys = base.Keys;
			this.ibll.NameSpace = base.NameSpace;
			this.ibll.Folder = base.Folder;
			this.ibll.Modelpath = base.Modelpath;
			this.ibll.ModelName = base.ModelName;
			this.ibll.BLLpath = base.BLLpath;
			this.ibll.BLLName = base.BLLName;
			this.ibll.TableDescription = CodeCommon.CutDescText(base.TableDescription, 10, base.BLLName);
			this.ibll.Factorypath = "";
			this.ibll.IDALpath = "";
			this.ibll.IClass = "";
			this.ibll.DALpath = this.DALpath;
			this.ibll.DALName = base.DALName;
			this.ibll.IsHasIdentity = base.IsHasIdentity;
			this.ibll.DbType = this.dbobj.DbType;
			return this.ibll.GetBLLCode(Maxid, Exists, Add, Update, Delete, GetModel, GetModelByCache, List);
		}
	}
}
