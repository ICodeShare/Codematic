using Maticsoft.CodeHelper;
using Maticsoft.IDBO;
using System;
using System.Collections.Generic;
namespace Maticsoft.IBuilder
{
	public interface IBuilderDAL
	{
		IDbObject DbObject
		{
			get;
			set;
		}
		string DbName
		{
			get;
			set;
		}
		string TableName
		{
			get;
			set;
		}
		List<ColumnInfo> Fieldlist
		{
			get;
			set;
		}
		List<ColumnInfo> Keys
		{
			get;
			set;
		}
		string NameSpace
		{
			get;
			set;
		}
		string Folder
		{
			get;
			set;
		}
		string Modelpath
		{
			get;
			set;
		}
		string ModelName
		{
			get;
			set;
		}
		string DALpath
		{
			get;
			set;
		}
		string DALName
		{
			get;
			set;
		}
		string IDALpath
		{
			get;
			set;
		}
		string IClass
		{
			get;
			set;
		}
		string DbHelperName
		{
			get;
			set;
		}
		string ProcPrefix
		{
			get;
			set;
		}
		string GetDALCode(bool Maxid, bool Exists, bool Add, bool Update, bool Delete, bool GetModel, bool List);
		string CreatGetMaxID();
		string CreatExists();
		string CreatAdd();
		string CreatUpdate();
		string CreatDelete();
		string CreatGetModel();
		string CreatGetList();
	}
}
