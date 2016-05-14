using Maticsoft.CodeHelper;
using System;
using System.Collections.Generic;
namespace Maticsoft.IBuilder
{
	public interface IBuilderBLL
	{
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
		string TableDescription
		{
			get;
			set;
		}
		string BLLpath
		{
			get;
			set;
		}
		string BLLName
		{
			get;
			set;
		}
		string Factorypath
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
		bool IsHasIdentity
		{
			get;
			set;
		}
		string DbType
		{
			get;
			set;
		}
		string GetBLLCode(bool Maxid, bool Exists, bool Add, bool Update, bool Delete, bool GetModel, bool GetModelByCache, bool List);
		string CreatBLLGetMaxID();
		string CreatBLLExists();
		string CreatBLLADD();
		string CreatBLLUpdate();
		string CreatBLLDelete();
		string CreatBLLGetModel();
		string CreatBLLGetList();
	}
}
