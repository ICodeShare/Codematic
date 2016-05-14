using Maticsoft.CodeHelper;
using Maticsoft.IDBO;
using Maticsoft.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
namespace Maticsoft.CodeBuild
{
	public class BuilderFrame
	{
		protected IDbObject dbobj;
		protected string _dbtype;
		protected string _key = "ID";
		protected string _keyType = "int";
		private string _dbname;
		private string _tablename;
		private string _tabledescription = "";
		private string _modelname;
		private string _bllname;
		private string _dalname;
		private string _namespace = "Maticsoft";
		private string _folder;
		private string _dbhelperName;
		private List<ColumnInfo> _keys;
		private List<ColumnInfo> _fieldlist;
		private string _modelpath;
		private string _dalpath;
		private string _idalpath;
		private string _bllpath;
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
		public string Fields
		{
			get
			{
				StringPlus stringPlus = new StringPlus();
				foreach (object current in this.Fieldlist)
				{
					stringPlus.Append("'" + current.ToString() + "',");
				}
				stringPlus.DelLastComma();
				return stringPlus.Value;
			}
		}
		public string Modelpath
		{
			get
			{
				this._modelpath = this._namespace + ".Model";
				if (this._folder.Trim() != "")
				{
					this._modelpath = this._modelpath + "." + this._folder;
				}
				return this._modelpath;
			}
			set
			{
				this._modelpath = value;
			}
		}
		public string ModelSpace
		{
			get
			{
				return this.Modelpath + "." + this.ModelName;
			}
		}
		public string DALpath
		{
			get
			{
				string str = this._dbtype;
				if (this._dbtype == "SQL2000" || this._dbtype == "SQL2005" || this._dbtype == "SQL2008" || this._dbtype == "SQL2012")
				{
					str = "SQLServer";
				}
				this._dalpath = this._namespace + "." + str + "DAL";
				if (this._folder.Trim() != "")
				{
					this._dalpath = this._dalpath + "." + this._folder;
				}
				return this._dalpath;
			}
			set
			{
				this._dalpath = value;
			}
		}
		public string DALSpace
		{
			get
			{
				return this.DALpath + "." + this.DALName;
			}
		}
		public string IDALpath
		{
			get
			{
				this._idalpath = this._namespace + ".IDAL";
				if (this._folder.Trim() != "")
				{
					this._idalpath = this._idalpath + "." + this._folder;
				}
				return this._idalpath;
			}
		}
		public string IClass
		{
			get
			{
				return "I" + this.DALName;
			}
		}
		public string BLLpath
		{
			get
			{
				string text = this._namespace + ".BLL";
				if (this._folder.Trim() != "")
				{
					text = text + "." + this._folder;
				}
				return text;
			}
			set
			{
				this._bllpath = value;
			}
		}
		public string BLLSpace
		{
			get
			{
				return this.BLLpath + "." + this.BLLName;
			}
		}
		public string Factorypath
		{
			get
			{
				return this._namespace + ".DALFactory";
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
				case "MySQL":
					return "MySqlDbType";
				case "OleDb":
					return "OleDbType";
				}
				return "SqlDbType";
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
		public bool IsHasIdentity
		{
			get
			{
				bool result = false;
				foreach (ColumnInfo current in this.Keys)
				{
					this._key = current.ColumnName;
					this._keyType = current.TypeName;
					if (current.IsIdentity)
					{
						this._key = current.ColumnName;
						this._keyType = CodeCommon.DbTypeToCS(current.TypeName);
						result = true;
						break;
					}
				}
				return result;
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
		public string GetkeyParalist(Hashtable Keys)
		{
			StringPlus stringPlus = new StringPlus();
			foreach (DictionaryEntry dictionaryEntry in Keys)
			{
				stringPlus.Append(CodeCommon.DbTypeToCS(dictionaryEntry.Value.ToString()) + " " + dictionaryEntry.Key.ToString() + ",");
			}
			if (stringPlus.Value.IndexOf(",") > 0)
			{
				stringPlus.DelLastComma();
			}
			return stringPlus.Value;
		}
		public string GetkeyWherelist(Hashtable Keys)
		{
			StringPlus stringPlus = new StringPlus();
			int num = 0;
			foreach (DictionaryEntry dictionaryEntry in Keys)
			{
				num++;
				if (CodeCommon.IsAddMark(dictionaryEntry.Value.ToString()))
				{
					stringPlus.Append(dictionaryEntry.Key.ToString() + "='\"+" + dictionaryEntry.Key.ToString() + "+\"'\"");
				}
				else
				{
					stringPlus.Append(dictionaryEntry.Key.ToString() + "=\"+" + dictionaryEntry.Key.ToString() + "+\"");
					if (num == Keys.Count)
					{
						stringPlus.Append("\"");
					}
				}
				stringPlus.Append(" and ");
			}
			if (stringPlus.Value.IndexOf("and") > 0)
			{
				stringPlus.DelLastChar("and");
			}
			return stringPlus.Value;
		}
		public string GetkeyWherelistProc(Hashtable Keys)
		{
			StringPlus stringPlus = new StringPlus();
			foreach (DictionaryEntry dictionaryEntry in Keys)
			{
				stringPlus.Append(dictionaryEntry.Key.ToString() + "=@" + dictionaryEntry.Key.ToString());
				stringPlus.Append(" and ");
			}
			if (stringPlus.Value.IndexOf("and") > 0)
			{
				stringPlus.DelLastChar("and");
			}
			return stringPlus.Value;
		}
		public string GetFieldslist(DataTable dt)
		{
			StringPlus stringPlus = new StringPlus();
			foreach (DataRow dataRow in dt.Rows)
			{
				string str = dataRow["ColumnName"].ToString();
				stringPlus.Append("[" + str + "],");
			}
			stringPlus.DelLastComma();
			return stringPlus.Value;
		}
		public string Space(int num)
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < num; i++)
			{
				stringBuilder.Append("\t");
			}
			return stringBuilder.ToString();
		}
	}
}
