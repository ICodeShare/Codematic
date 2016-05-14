using Maticsoft.CodeHelper;
using Maticsoft.IDBO;
using System;
using System.Collections.Generic;
namespace Maticsoft.IBuilder
{
	public interface IBuilderDALTran
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
		string TableNameParent
		{
			get;
			set;
		}
		string TableNameSon
		{
			get;
			set;
		}
		List<ColumnInfo> FieldlistParent
		{
			get;
			set;
		}
		List<ColumnInfo> FieldlistSon
		{
			get;
			set;
		}
		List<ColumnInfo> KeysParent
		{
			get;
			set;
		}
		List<ColumnInfo> KeysSon
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
		string ModelNameParent
		{
			get;
			set;
		}
		string ModelNameSon
		{
			get;
			set;
		}
		string DALpath
		{
			get;
			set;
		}
		string DALNameParent
		{
			get;
			set;
		}
		string DALNameSon
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
