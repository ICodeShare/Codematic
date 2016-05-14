using System;
using System.Collections.Generic;
namespace Maticsoft.CodeHelper
{
	public class ModelTran
	{
		private string dbName;
		private string tableName;
		private string modelName;
		private string action;
		private List<ColumnInfo> _fieldlist;
		private List<ColumnInfo> _keys;
		public string DbName
		{
			get
			{
				return this.dbName;
			}
			set
			{
				this.dbName = value;
			}
		}
		public string TableName
		{
			get
			{
				return this.tableName;
			}
			set
			{
				this.tableName = value;
			}
		}
		public string ModelName
		{
			get
			{
				return this.modelName;
			}
			set
			{
				this.modelName = value;
			}
		}
		public string Action
		{
			get
			{
				return this.action;
			}
			set
			{
				this.action = value;
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
			}
		}
	}
}
