using Maticsoft.CodeHelper;
using System;
using System.Collections.Generic;
namespace Maticsoft.IBuilder
{
	public interface IBuilderIDAL
	{
		string DbType
		{
			get;
			set;
		}
		string TableDescription
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
		bool IsHasIdentity
		{
			get;
			set;
		}
		string GetIDALCode(bool Maxid, bool Exists, bool Add, bool Update, bool Delete, bool GetModel, bool List);
		string CreatGetMaxID();
		string CreatExists();
		string CreatAdd();
		string CreatUpdate();
		string CreatDelete();
		string CreatGetModel();
		string CreatGetList();
	}
}
