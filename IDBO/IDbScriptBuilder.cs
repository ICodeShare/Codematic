using Maticsoft.CodeHelper;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
namespace Maticsoft.IDBO
{
	public interface IDbScriptBuilder
	{
		string DbConnectStr
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
		string ProcPrefix
		{
			get;
			set;
		}
		string ProjectName
		{
			get;
			set;
		}
		List<ColumnInfo> Fieldlist
		{
			get;
			set;
		}
		string Fields
		{
			get;
		}
		List<ColumnInfo> Keys
		{
			get;
			set;
		}
		string CreateDBTabScript(string dbname);
		string CreateTabScript(string dbname, string tablename);
		string CreateTabScriptBySQL(string dbname, string strSQL);
		void CreateTabScript(string dbname, string tablename, string filename, ProgressBar progressBar);
		string CreatPROCGetMaxID();
		string CreatPROCIsHas();
		string CreatPROCADD();
		string CreatPROCUpdate();
		string CreatPROCDelete();
		string CreatPROCGetModel();
		string CreatPROCGetList();
		string GetPROCCode(bool Maxid, bool Ishas, bool Add, bool Update, bool Delete, bool GetModel, bool List);
		string GetPROCCode(string dbname, string tablename);
		string GetPROCCode(string dbname);
		string GetSQLSelect(string dbname, string tablename);
		string GetSQLUpdate(string dbname, string tablename);
		string GetSQLDelete(string dbname, string tablename);
		string GetSQLInsert(string dbname, string tablename);
	}
}
