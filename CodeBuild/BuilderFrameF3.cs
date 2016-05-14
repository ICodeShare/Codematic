using Maticsoft.BuilderModel;
using Maticsoft.CodeHelper;
using Maticsoft.IBuilder;
using Maticsoft.IDBO;
using Maticsoft.Utility;
using System;
using System.Collections.Generic;
namespace Maticsoft.CodeBuild
{
	public class BuilderFrameF3 : BuilderFrame
	{
		private IBuilderDAL idal;
		private IBuilderBLL ibll;
		private IBuilderDALTran idaltran;
		private IBuilderIDAL iidal;
		public BuilderFrameF3(IDbObject idbobj, string dbName, string tableName, string tableDescription, string modelName, string bllName, string dalName, List<ColumnInfo> fieldlist, List<ColumnInfo> keys, string nameSpace, string folder, string dbHelperName)
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
		public BuilderFrameF3(IDbObject idbobj, string dbName, string nameSpace, string folder, string dbHelperName)
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
			this.idal.DALpath = base.DALpath;
			this.idal.DALName = base.DALName;
			this.idal.IDALpath = base.IDALpath;
			this.idal.IClass = base.IClass;
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
			this.idaltran.DALpath = base.DALpath;
			this.idaltran.DALNameParent = DALNameParent;
			this.idaltran.DALNameSon = DALNameSon;
			this.idaltran.IDALpath = base.IDALpath;
			this.idaltran.IClass = base.IClass;
			this.idaltran.DbHelperName = base.DbHelperName;
			this.idaltran.ProcPrefix = procPrefix;
			return this.idaltran.GetDALCode(Maxid, Exists, Add, Update, Delete, GetModel, List);
		}
		public string GetIDALCode(bool Maxid, bool Exists, bool Add, bool Update, bool Delete, bool GetModel, bool List, bool ListProc)
		{
			this.iidal = BuilderFactory.CreateIDALObj();
			if (this.iidal == null)
			{
				return "//请选择有效的接口层代码组件类型！";
			}
			this.iidal.Fieldlist = base.Fieldlist;
			this.iidal.Keys = base.Keys;
			this.iidal.NameSpace = base.NameSpace;
			this.iidal.Folder = base.Folder;
			this.iidal.Modelpath = base.Modelpath;
			this.iidal.ModelName = base.ModelName;
			this.iidal.TableDescription = CodeCommon.CutDescText(base.TableDescription, 10, base.BLLName);
			this.iidal.IDALpath = base.IDALpath;
			this.iidal.IClass = base.IClass;
			this.iidal.IsHasIdentity = base.IsHasIdentity;
			this.iidal.DbType = this.dbobj.DbType;
			return this.iidal.GetIDALCode(Maxid, Exists, Add, Update, Delete, GetModel, List);
		}
		public string GetDALFactoryCode()
		{
			StringPlus stringPlus = new StringPlus();
			stringPlus.AppendLine("using System;");
			stringPlus.AppendLine("using System.Reflection;");
			stringPlus.AppendLine("using System.Configuration;");
			stringPlus.AppendLine("using " + base.IDALpath + ";");
			stringPlus.AppendLine("namespace " + base.Factorypath);
			stringPlus.AppendLine("{");
			stringPlus.AppendSpaceLine(1, "/// <summary>");
			stringPlus.AppendSpaceLine(1, "/// 抽象工厂模式创建DAL。");
			stringPlus.AppendSpaceLine(1, "/// web.config 需要加入配置：(利用工厂模式+反射机制+缓存机制,实现动态创建不同的数据层对象接口) ");
			stringPlus.AppendSpaceLine(1, "/// DataCache类在导出代码的文件夹里");
			stringPlus.AppendSpaceLine(1, "/// <appSettings> ");
			stringPlus.AppendSpaceLine(1, "/// <add key=\"DAL\" value=\"" + base.DALpath + "\" /> (这里的命名空间根据实际情况更改为自己项目的命名空间)");
			stringPlus.AppendSpaceLine(1, "/// </appSettings> ");
			stringPlus.AppendSpaceLine(1, "/// </summary>");
			stringPlus.AppendSpaceLine(1, "public sealed class DataAccess//<t>");
			stringPlus.AppendSpaceLine(1, "{");
			stringPlus.AppendSpaceLine(2, "private static readonly string AssemblyPath = ConfigurationManager.AppSettings[\"DAL\"];");
			stringPlus.AppendSpaceLine(2, "/// <summary>");
			stringPlus.AppendSpaceLine(2, "/// 创建对象或从缓存获取");
			stringPlus.AppendSpaceLine(2, "/// </summary>");
			stringPlus.AppendSpaceLine(2, "public static object CreateObject(string AssemblyPath,string ClassNamespace)");
			stringPlus.AppendSpaceLine(2, "{");
			stringPlus.AppendSpaceLine(3, "object objType = DataCache.GetCache(ClassNamespace);//从缓存读取");
			stringPlus.AppendSpaceLine(3, "if (objType == null)");
			stringPlus.AppendSpaceLine(3, "{");
			stringPlus.AppendSpaceLine(4, "try");
			stringPlus.AppendSpaceLine(4, "{");
			stringPlus.AppendSpaceLine(5, "objType = Assembly.Load(AssemblyPath).CreateInstance(ClassNamespace);//反射创建");
			stringPlus.AppendSpaceLine(5, "DataCache.SetCache(ClassNamespace, objType);// 写入缓存");
			stringPlus.AppendSpaceLine(4, "}");
			stringPlus.AppendSpaceLine(4, "catch");
			stringPlus.AppendSpaceLine(4, "{}");
			stringPlus.AppendSpaceLine(3, "}");
			stringPlus.AppendSpaceLine(3, "return objType;");
			stringPlus.AppendSpaceLine(2, "}");
			stringPlus.AppendSpaceLine(2, "/// <summary>");
			stringPlus.AppendSpaceLine(2, "/// 创建数据层接口");
			stringPlus.AppendSpaceLine(2, "/// </summary>");
			stringPlus.AppendSpaceLine(2, "//public static t Create(string ClassName)");
			stringPlus.AppendSpaceLine(2, "//{");
			stringPlus.AppendSpaceLine(3, "//string ClassNamespace = AssemblyPath +\".\"+ ClassName;");
			stringPlus.AppendSpaceLine(3, "//object objType = CreateObject(AssemblyPath, ClassNamespace);");
			stringPlus.AppendSpaceLine(3, "//return (t)objType;");
			stringPlus.AppendSpaceLine(2, "//}");
			stringPlus.AppendLine(this.GetDALFactoryMethodCode());
			stringPlus.AppendSpaceLine(1, "}");
			stringPlus.AppendLine("}");
			return stringPlus.ToString();
		}
		public string GetDALFactoryMethodCode()
		{
			StringPlus stringPlus = new StringPlus();
			stringPlus.AppendSpaceLine(2, "/// <summary>");
			stringPlus.AppendSpaceLine(2, "/// 创建" + base.DALName + "数据层接口。" + CodeCommon.CutDescText(base.TableDescription, 10, ""));
			stringPlus.AppendSpaceLine(2, "/// </summary>");
			stringPlus.AppendSpaceLine(2, string.Concat(new string[]
			{
				"public static ",
				base.IDALpath,
				".",
				base.IClass,
				" Create",
				base.DALName,
				"()"
			}));
			stringPlus.AppendSpaceLine(2, "{\r\n");
			if (base.Folder != "")
			{
				stringPlus.AppendSpaceLine(3, string.Concat(new string[]
				{
					"string ClassNamespace = AssemblyPath +\".",
					base.Folder,
					".",
					base.DALName,
					"\";"
				}));
			}
			else
			{
				stringPlus.AppendSpaceLine(3, "string ClassNamespace = AssemblyPath +\"." + base.DALName + "\";");
			}
			stringPlus.AppendSpaceLine(3, "object objType=CreateObject(AssemblyPath,ClassNamespace);");
			stringPlus.AppendSpaceLine(3, string.Concat(new string[]
			{
				"return (",
				base.IDALpath,
				".",
				base.IClass,
				")objType;"
			}));
			stringPlus.AppendSpaceLine(2, "}");
			return stringPlus.Value;
		}
		public string GetBLLCode(string AssemblyGuid, bool Maxid, bool Exists, bool Add, bool Update, bool Delete, bool GetModel, bool GetModelByCache, bool List, bool ListProc)
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
			this.ibll.TableDescription = CodeCommon.CutDescText(base.TableDescription, 10, base.BLLName);
			this.ibll.BLLpath = base.BLLpath;
			this.ibll.BLLName = base.BLLName;
			this.ibll.Factorypath = base.Factorypath;
			this.ibll.IDALpath = base.IDALpath;
			this.ibll.IClass = base.IClass;
			this.ibll.DALpath = base.DALpath;
			this.ibll.DALName = base.DALName;
			this.ibll.IsHasIdentity = base.IsHasIdentity;
			this.ibll.DbType = this.dbobj.DbType;
			return this.ibll.GetBLLCode(Maxid, Exists, Add, Update, Delete, GetModel, GetModelByCache, List);
		}
	}
}
